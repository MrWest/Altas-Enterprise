using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EventHandlerManagerTests
    {
        private EventHandlerManager<EventArgs> _manager;
        private PrivateObject _internals;


        [TestInitialize]
        public void Initialize()
        {
            _manager = new EventHandlerManager<EventArgs>();
            _internals = new PrivateObject(_manager);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddOperator_GivenNullHandler_ThrowsException()
        {
            _manager += null;
        }

        [TestMethod]
        public void AddOperator_GivenHandler_SubscribesTheHandlers()
        {
            // Arrange
            EventHandler<EventArgs> handler = (sender, e) => { };

            // Act
            _manager += handler;

            // Assert
            var handlers = (IEnumerable<EventHandler<EventArgs>>)_internals.GetField("_handlers");
            CollectionAssert.Contains(handlers.ToArray(), handler);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SubstractOperator_GivenNullHandler_ThrowsException()
        {
            _manager -= null;
        }

        [TestMethod]
        public void SubstractOperator_GivenHandler_SubscribesTheHandlers()
        {
            // Arrange
            EventHandler<EventArgs> handler = (sender, e) => { };

            // Act
            _manager += handler;
            _manager -= handler;

            // Assert
            var handlers = (IEnumerable<EventHandler<EventArgs>>)_internals.GetField("_handlers");
            CollectionAssert.DoesNotContain(handlers.ToArray(), handler);
        }


        [TestMethod]
        public void GetEnumerator_ReturnsHandlersEnumerator()
        {
            // Arrange
            EventHandler<EventArgs> handler1 = (sender, e) => { };
            EventHandler<EventArgs> handler2 = (sender, e) => { };

            // Act
            _manager += handler1;
            _manager += handler2;

            // Assert
            EventHandler<EventArgs>[] expectedHandlers = { handler1, handler2 };
            EventHandler<EventArgs>[] actualHandlers = _manager.ToArray();
            CollectionAssert.AreEquivalent(expectedHandlers, actualHandlers);
        }

        [TestMethod]
        public void GetEnumerator_IsIEnumerableInternfaceImplementation_ReturnsHandlersEnumerator()
        {
            // Arrange
            EventHandler<EventArgs> handler1 = (sender, e) => { };
            EventHandler<EventArgs> handler2 = (sender, e) => { };

            // Act
            _manager += handler1;
            _manager += handler2;

            // Assert
            EventHandler<EventArgs>[] expectedHandlers = { handler1, handler2 };
            var actualHandlers = ((IEnumerable)_manager).Cast<EventHandler<EventArgs>>().ToList();
            CollectionAssert.AreEquivalent(expectedHandlers, actualHandlers);
        }


        [TestMethod]
        public void CallEventHandlers_InvokesAllTheRegisteredEventHandlers()
        {
            // Arrange
            bool handler1Called = false, handler2Called = false;
            var args = new EventArgs();
            EventHandler<EventArgs> handler1 = (sender, e) => handler1Called |= e == args;
            EventHandler<EventArgs> handler2 = (sender, e) => handler2Called |= e == args;

            _manager += handler1;
            _manager += handler2;
            _manager -= handler2;

            // Act
            _manager.CallEventHandlers(this, args);

            // Assert
            Assert.IsTrue(handler1Called && !handler2Called);
        }
    }
}
