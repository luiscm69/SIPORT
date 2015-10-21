namespace Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Data.Common.UnitOfWork;

    using Domain.Common;
    using Domain.Common.Contracts;

    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDbSet<T> set;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.set = unitOfWork.Set<T>();
        }

        public T Get(long id)
        {
            return this.set.SingleOrDefault(x => x.ID == id);
        }

        public void Add(T entity)
        {
            this.set.Add(entity);
        }

        public void Delete(T entity)
        {
            this.set.Remove(entity);
        }

        public IList<T> GetAll()
        {
            return this.set.ToList();
        }

        public IList<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this.set.Where(predicate).ToList();
        }

        public void Commit()
        {
            this.unitOfWork.Commit();
        }
    }
}
