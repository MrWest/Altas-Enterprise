using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Properties;
using CompanyName.Atlas.ViewModels;

namespace CompanyName.Atlas.Tests.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ShellViewModelTests : BindableTestsBase<ShellViewModel, ShellViewModel>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        private void SetStatusText(string text)
        {
            var internals = new PrivateObject(TestObject);
            internals.SetProperty("StatusText", text);
        }


        [TestMethod]
        public void StatusText_GivenDifferentValue_SetsIt()
        {
            TestGetsSetValue(x => x.StatusText, () => "123");
        }

        [TestMethod]
        public void StatusText_GivenDifferentValue_NotifiesPropertyChanged()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.StatusText, () => "1", () => "2");
        }

        [TestMethod]
        public void StatusText_GivenSameValue_DoesNotNotifyPropertyChanged()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.StatusText, () => "1");
        }


        [TestMethod]
        public void SignalReady_SetsTheReadyMessageInTheStatusTextProperty()
        {
            // Act
            TestObject.SignalReady();

            // Assert
            Assert.AreEqual(Resources.Ready, TestObject.StatusText);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SignalText_GivenNullText_ThrowsException()
        {
            TestObject.SignalText(null);
        }

        [TestMethod]
        public void SignalText_GivenSeveralTextsBeforeItGetsThePutTheReadyMessage_WritesAllTheTextsAndAtThenPutsTheReadyMessage()
        {
            // Arrange
            var messages = new List<string>();
            TestObject.PropertyChanged += (sender, e) => messages.Add(TestObject.StatusText);

            var internals = new PrivateObject(TestObject);

            // Act
            TestObject.SignalText("My message is about status...");
            TestObject.SignalText("Just another message.");

            // Assert
            Assert.IsTrue(SpinWait.SpinUntil(() => messages.Count == 3, 10000));
            string[] expected = new[] { "My message is about status...", "Just another message.", Resources.Ready };
            string[] actual = messages.ToArray();
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
