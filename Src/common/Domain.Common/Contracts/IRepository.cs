namespace Domain.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : Entity 
    {
        T Get(long id);

        void Add(T entity);

        void Delete(T entity);

        IList<T> GetAll();

        IList<T> Get(Expression<Func<T, bool>> predicate);

        void Commit();
    }
}
