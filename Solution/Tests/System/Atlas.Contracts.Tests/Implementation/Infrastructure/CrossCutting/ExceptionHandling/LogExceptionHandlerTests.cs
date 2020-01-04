using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.Practices.Prism.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LogExceptionHandlerTests : TestBase<LogExceptionHandler>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void HandleException_LogsTheExceptionDetails()
        {
            // Arrange
            Exception exception = null;
            Guid id = Guid.NewGuid();

            try
            {
                throw new Exception("A");
            }
            catch(Exception e)
            {
                // Act
                TestObject.HandleException(e, id);
                exception = e;
            }

            // Assert
            var message = new StringBuilder(Resources.UnexpectedExceptionLogMessage);
            message.AppendLine();
            message.AppendLine("Handling ID: {0}".EasyFormat(id));
            message.AppendLine("Exception: {0}".EasyFormat(exception.GetType().Name));
            message.AppendLine("Message: {0}".EasyFormat(exception.Message));
            message.AppendLine("Target site: {0}".EasyFormat(exception.TargetSite));
            message.AppendLine("Stack trace:");
            message.AppendLine(exception.StackTrace);

            LoggerMock.Verify(x => x.Log(message.ToString(), Category.Exception, Priority.High));
        }
    }
}
