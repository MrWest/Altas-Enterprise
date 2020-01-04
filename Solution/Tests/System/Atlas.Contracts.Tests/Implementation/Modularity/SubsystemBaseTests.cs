using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Implementation.Modularity;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Moq;
using System.Reflection;
using Moq.Protected;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Modularity
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SubsystemBaseTests : MockedTestBase<SubsystemBase>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Name_ReturnsCorrectModuleName()
        {
            // Arrange
            const string expectedName = "SampleSubsystem";
            var module = new SampleSubsystem();

            // Act
            string actualName = module.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }


        [TestMethod]
        public void Version_ReturnsCorrectAssemblyVersion()
        {
            // Arrange
            Version expectedVersion = GetType().Assembly.GetName().Version;
            var module = new SampleSubsystem();

            // Act
            Version actualVersion = module.Version;

            // Assert
            Assert.AreEqual(expectedVersion, actualVersion);
        }


        [TestMethod]
        public void InitializeAndLoadViewModel_ReturnsDefinedInstanceOfDesiredViewModel()
        {
            // Arrange
            var expectedViewModel = new Mock<ICrudViewModel>();
            ServiceLocatorMock.Setup(x => x.GetInstance<ICrudViewModel>()).Returns(expectedViewModel.Object);

            // Act
            var actualViewModel = InvokeGeneric<ICrudViewModel>("CreateAndInitialize", new[] { typeof(ICrudViewModel) });

            // Assert
            Assert.AreSame(expectedViewModel.Object, actualViewModel);
        }

        [TestMethod]
        public void InitializeAndLoadViewModel_ReturnsAnInitializedInstanceOfDesiredViewModel()
        {
            // Arrange
            var expectedViewModel = new Mock<ICrudViewModel>();
            ServiceLocatorMock.Setup(x => x.GetInstance<ICrudViewModel>()).Returns(expectedViewModel.Object);

            // Act
            var actualViewModel = InvokeGeneric<ICrudViewModel>("CreateAndInitialize", new[] { typeof(ICrudViewModel) });

            // Assert
            expectedViewModel.Verify(x => x.Load());
        }

        [TestMethod]
        public void InitializeAndLoadViewModel_IfErrorInTheProcess_NotifiesUser()
        {
            // Arrange
            var expectedViewModel = new Mock<ICrudViewModel>();
            var interactionRequestedEventArgs = new InteractionRequestedEventArgs(null, null);
            expectedViewModel.Setup(x => x.Load()).Raises(x => x.Raised += null, interactionRequestedEventArgs);
            ServiceLocatorMock.Setup(x => x.GetInstance<ICrudViewModel>()).Returns(expectedViewModel.Object);

            // Act
            var actualViewModel = InvokeGeneric<ICrudViewModel>("CreateAndInitialize", new[] { typeof(ICrudViewModel) });

            // Assert
            TestMock.Protected().Verify("OnInteractionRequested", Times.Once(), ItExpr.IsAny<object>(), interactionRequestedEventArgs);
        }


        [Module(ModuleName = "SampleSubsystem")]
        private class SampleSubsystem : SubsystemBase
        {
            public override void Initialize()
            {
            }
        }
    }
}
