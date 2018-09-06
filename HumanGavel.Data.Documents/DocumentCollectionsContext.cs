using HumanGavel.Data.Documents.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Documents
{
    /// <summary>
    /// Wrapper around IOrderedQueryable. Sort of acts like DbSet in EF.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocumentCollectionsContext<T> : IOrderedQueryable<T>
        where T : DocumentBase
    {
        public DocumentCollectionsContext(DocumentDbContext context, String collectionName, IOrderedQueryable<T> collectionQueryable)
        {
            Queryable = collectionQueryable;
            Context = context;
            CollectionName = collectionName;
        }

        #region Queryable

        public Type ElementType
        {
            get
            {
                return Queryable.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return Queryable.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return Queryable.Provider;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        private IOrderedQueryable<T> Queryable { get; set; }
        public String CollectionName { get; private set; }
        internal DocumentDbContext Context { get; private set; }

        #endregion

        public async Task CreateDocumentAsync(T document)
        {
            document.DocumentGuid = Guid.NewGuid();
            await Context.client?.CreateDocumentAsync(CollectionName, document);
        }
    }
}
