using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.ExceptionHandling.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    [TestClass, ExcludeFromCodeCoverage]
    public class HandleSystemExceptionBehaviorTests : TestBase
    {
        private UnityContainer _container;

        private LogExceptionHandlerStub _logHandler;
        private ReplaceExceptionHandlerStub _replaceHandler;
        private EmailExceptionHandlerStub _emailHandler;

        private Exception _anyException = new Exception();
        private ValidationException _validationException = new ValidationException();
        private IExceptionThrower _thrower;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _container = new UnityContainer();
            _container.AddNewExtension<Interception>();

            _emailHandler = new EmailExceptionHandlerStub();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEmailExceptionHandler>()).Returns(_emailHandler);

            _logHandler = new LogExceptionHandlerStub();
            ServiceLocatorMock.Setup(x => x.GetInstance<ILogExceptionHandler>()).Returns(_logHandler);

            _replaceHandler = new ReplaceExceptionHandlerStub();
            ServiceLocatorMock.Setup(x => x.GetInstance<IReplaceHandler>()).Returns(_replaceHandler);

            InjectionMember[] behaviors =
            {
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<HandleSystemExceptionBehavior>()
            };

            _container.RegisterType<IExceptionThrower, ExceptionThrower>(behaviors);

            _thrower = _container.Resolve<IExceptionThrower>();
            _thrower.AnyException = _anyException;
            _thrower.ValidationException = _validationException;
        }


        [TestMethod]
        public void Invoke_WhenThrownAnExceptionDifferentTheValidationException_LogsTheDetails()
        {
            // Act
            try
            {
                _thrower.ThrowAnyException();
            }
            catch
            {
                Assert.AreSame(_anyException, _logHandler.LastHandledException);
            }
        }

        [TestMethod]
        public void Invoke_WhenThrownAnExceptionDifferentTheValidationException_EmailsTheDetails()
        {
            // Act
            try
            {
                _thrower.ThrowAnyException();
            }
            catch
            {
                Assert.AreSame(_anyException, _emailHandler.LastHandledException);
            }
        }

        [TestMethod]
        public void Invoke_WhenThrownAnExceptionDifferentTheValidationException_ReplacesTheException()
        {
            // Act
            try
            {
                _thrower.ThrowAnyException();
            }
            catch
            {
                Assert.AreSame(_anyException, _replaceHandler.LastHandledException);
            }
        }


        [TestMethod]
        public void Invoke_WhenThrownAValidationException_RethrowsIt()
        {
            // Act
            try
            {
                _thrower.ThrowValidationException();
            }
            catch (Exception e)
            {
                Assert.AreSame(_validationException, e);
            }
        }


        [TestMethod]
        public void Invoke_WhenNotThrownException_NoExceptionIsThrownToTheCaller()
        {
            // Act
            int result = _thrower.DoNoThrowException();

            // Assert
            Assert.AreEqual(900, result);
        }


        #region Stubs

        private class HandlerBaseStub : IExceptionHandler
        {
            public Exception LastHandledException { get; set; }


            public Exception HandleException(Exception exception, Guid handlingInstanceId)
            {
                return (LastHandledException = exception);
            }
        }


        private class LogExceptionHandlerStub : HandlerBaseStub, ILogExceptionHandler
        {
        }


        private class ReplaceExceptionHandlerStub : HandlerBaseStub, IReplaceHandler
        {
        }


        private class EmailExceptionHandlerStub : HandlerBaseStub, IEmailExceptionHandler
        {
        }

        #endregion
    }
}
