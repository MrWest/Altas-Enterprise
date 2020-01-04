using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Collections.Generic
{
    /// <summary>
    /// This class exposes some extensions for collections.
    /// </summary>
    public static class CollectionHelpersFactory
    {
        /// <summary>
        ///     Creates a collection adapter of items of the first type to pass the interactions it receives from its
        ///     callers an underlying collection.
        /// </summary>
        /// <typeparam name="TFrom">The type of the items in the adapted collection.</typeparam>
        /// <typeparam name="TTo">
        ///     The type of the items in teh adaptee collection. This type must me an inheritor of
        ///     <typeparamref name="TFrom" />.
        /// </typeparam>
        /// <param name="collection">The collection to adapt.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        /// <returns>
        ///     A wrapper in form of a collection of items of the deriver, containing the items those of type
        ///     <typeparamref name="TFrom" /> in the given collection
        ///     which will be casted when returned from the result collection to the type
        ///     <typeparamref name="TTo" />.
        /// </returns>
        public static IList<TTo> GetCollectionAdapterFor<TFrom, TTo>(ICollection<TFrom> collection)
            where TTo : TFrom
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            return new CollectionAdapter<TFrom, TTo>(collection);
        }


        /// <summary>
        ///     This is the real adapter collection.
        /// </summary>
        class CollectionAdapter<TFrom, TTo> : IList<TTo> where TTo : TFrom
        {
            readonly ICollection<TFrom> _collection;

            public CollectionAdapter(ICollection<TFrom> collection)
            {
                _collection = collection;
            }


            public int Count
            {
                get { return _collection.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }


            [ExcludeFromCodeCoverage]
            public IEnumerator<TTo> GetEnumerator()
            {
                return _collection.Cast<TTo>().GetEnumerator();
            }

            [ExcludeFromCodeCoverage]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(TTo item)
            {
                _collection.Add(item);
            }

            public void Clear()
            {
                _collection.Clear();
            }

            public bool Contains(TTo item)
            {
                return _collection.Contains(item);
            }

            public void CopyTo(TTo[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                if (array.Length != _collection.Count)
                    throw new ArgumentException(Properties.Resources.ArrayLengthMustEqualTo.EasyFormat(_collection.Count));
                if (arrayIndex < 0 || arrayIndex >= _collection.Count)
                    throw new ArgumentOutOfRangeException("arrayIndex",
                        Properties.Resources.IndexOutOfrange.EasyFormat(0, _collection.Count));

                /* This dirty thing is done because casting is not allowed, so the assigning of items to the given
                 * array is done by force */
                Action<TTo[], TFrom, int> copyTo = (arr, item, index) =>
                {
                    Type arrayType = arr.GetType();
                    MethodInfo property = arrayType.GetMethod("Set");
                    property.Invoke(arr, new object[] { index, item });
                };

                for (int i = arrayIndex; i < _collection.Count; i++) copyTo(array, _collection.ElementAt(i), i);
            }

            public bool Remove(TTo item)
            {
                return _collection.Remove(item);
            }

            public int IndexOf(TTo item)
            {
                throw new NotSupportedException();
            }

            public void Insert(int index, TTo item)
            {
                throw new NotSupportedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotSupportedException();
            }

            public TTo this[int index]
            {
                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }
        }
    }
}