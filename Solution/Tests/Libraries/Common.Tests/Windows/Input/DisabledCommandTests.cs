using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Windows.Input
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DisabledCommandTests
    {
        [TestMethod]
        public void CanExecuted_ReturnsFalse()
        {
            // Arrange
            var command = new DisabledCommand();

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }
    }
}
