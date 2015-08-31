using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Data.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> Find(FilterDefinition<T> filter);

        /// <summary>
        /// Adds the current element to the collection.
        /// </summary>
        /// <param name="element">The element to be added.</param>
        Task Add(T element);

        /// <summary>
        /// Adds a list of elements to the collection.
        /// </summary>
        /// <param name="elements">The list of elements to be added.</param>
        Task Add(IEnumerable<T> elements);

        /// <summary>
        /// Replaces the element in the collection.
        /// </summary>
        /// <param name="element">The element to be replaced.</param>
        Task Replace(T element, Expression<Func<T, bool>> matchElement);

        /// <summary>
        /// Replaces a list of elements in the collection.
        /// </summary>
        /// <param name="elements">The list of elements to be replaced.</param>
        Task Replace(IEnumerable<T> elements, Expression<Func<T, bool>> matchElement);

        /// <summary>
        /// Updates only a desired set of document properties.
        /// </summary>
        /// <param name="element">Element to be updated</param>
        /// <param name="updateDefinition">The updates to be performed on the element.</param>
        Task Update(T element, Expression<Func<T, bool>> matchElement, UpdateDefinition<T> updateDefinition);

        /// <summary>
        /// Deletes the element from the collection.
        /// </summary>
        /// <param name="element">The element to be deleted.</param>
        /// <returns>The deleted element.</returns>
        Task<T> Remove(T element, Expression<Func<T, bool>> matchElement);

        /// <summary>
        /// Deletes the list of elements from the collection.
        /// </summary>
        /// <param name="elements">The list of elements to be removed.</param>
        /// <returns>The deleted elements.</returns>
        Task<IEnumerable<T>> Remove(IEnumerable<T> elements, Expression<Func<T, bool>> matchElement);
    }
}
