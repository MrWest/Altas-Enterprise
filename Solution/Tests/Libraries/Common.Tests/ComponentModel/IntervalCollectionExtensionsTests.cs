using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.ComponentModel
{
    /// <summary>
    ///     Collection of tests to check the implementation of the
    ///     <see cref="System.ComponentModel.IntervalCollectionExtensions" /> type.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IntervalCollectionExtensionsTests : IIntervalCollection<int>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="Common.Tests.ComponentModel.IntervalCollectionExtensionsTests" />
        ///     type.
        /// </summary>
        public IntervalCollectionExtensionsTests()
        {
            Intervals = new List<IInterval<int>>();
        }


        static void CheckFirstIntervalIsAlwaysCorrect(IIntervalCollection<int> target)
        {
            // The first interval is always correct
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 0,
                End = 5
            });
            target.ValidateIntervals();
        }

        static void CheckIntervalOverlapingsCase1(IIntervalCollection<int> target)
        {
            /* The end limit of this interval clashes with the start limit of the only one already existing in the
             * collection */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = -1,
                End = 0
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase2(IIntervalCollection<int> target)
        {
            /* The end limit of this interval falls inside the already existing interval */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = -5,
                End = 2
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase3(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 0,
                End = 2
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase4(IIntervalCollection<int> target)
        {
            /* The new interval is exactly inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 2,
                End = 3
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.Last(), first);
            Assert.AreEqual(target.Intervals.First(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase5(IIntervalCollection<int> target)
        {
            /* The new interval is exactly inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 0,
                End = 5
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase6(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 0,
                End = 7
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase7(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 3,
                End = 7
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalOverlapingsCase8(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 5,
                End = 7
            });

            Tuple<IInterval<int>, IInterval<int>> intervals = target.ValidateIntervals();
            IInterval<int> first = intervals.Item1, second = intervals.Item2;
            Assert.AreEqual(target.Intervals.First(), first);
            Assert.AreEqual(target.Intervals.Last(), second);

            RemoveLast(target);
        }

        static void CheckIntervalNoOverlapingsCase1(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 6,
                End = 10
            });

            Assert.IsNull(target.ValidateIntervals());
        }

        static void CheckIntervalNoOverlapingsCase2(IIntervalCollection<int> target)
        {
            /* The new interval is inside the already existing interval one */
            target.Intervals.Add(new FakeInterval<int>
            {
                Start = 11,
                End = 16
            });

            Assert.IsNull(target.ValidateIntervals());
        }

        static void RemoveLast(IIntervalCollection<int> target)
        {
            target.Intervals.Remove(target.Intervals.ToArray().Last());
        }


        /// <summary>
        ///     Gets the collection of <see cref="System.ComponentModel.IInterval{TValue}" /> contained in this
        ///     collection.
        /// </summary>
        public ICollection<IInterval<int>> Intervals { get; private set; }


        /// <summary>
        ///     Imitates a fake interval object.
        /// </summary>
        /// <typeparam name="T">The type of the interval limits.</typeparam>
        class FakeInterval<T> : IInterval<T> where T : IComparable<T>
        {
            /// <summary>
            ///     Gets or sets the lower limit of the interval.
            /// </summary>
            public T Start { get; set; }

            /// <summary>
            ///     Gets or sets the upper limit of the interval.
            /// </summary>
            public T End { get; set; }
        }

        /// <summary>
        ///     Checks whether the tested class is able to determine whether there are overlaping intervals in a
        ///     intervals collection.
        /// </summary>
        [TestMethod]
        public void ValidatesThereAreNoOverlapingIntervals()
        {
            var target = new IntervalCollectionExtensionsTests();

            CheckFirstIntervalIsAlwaysCorrect(target);
            CheckIntervalOverlapingsCase1(target);
            CheckIntervalOverlapingsCase2(target);
            CheckIntervalOverlapingsCase3(target);
            CheckIntervalOverlapingsCase4(target);
            CheckIntervalOverlapingsCase5(target);
            CheckIntervalOverlapingsCase6(target);
            CheckIntervalOverlapingsCase7(target);
            CheckIntervalOverlapingsCase8(target);
            CheckIntervalNoOverlapingsCase1(target);
            CheckIntervalNoOverlapingsCase2(target);
        }
    }
}