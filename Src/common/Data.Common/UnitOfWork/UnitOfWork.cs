
namespace Data.Common.UnitOfWork
{
    using System.Data.Common;
    using System.Data.Entity;

    public class UnitOfWork : IUnitOfWork
    {
        protected DatabaseContext databaseContext;

        protected virtual DatabaseContext DatabaseContext
        {
            get
            {
                if (databaseContext == null)
                    databaseContext = new DatabaseContext();
                return databaseContext;
            }
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return DatabaseContext.Set<T>();
        }

        public void Commit()
        {
            if (databaseContext == null)
                return;
            databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            if (databaseContext == null)
                return;
            databaseContext.Dispose();
            databaseContext = null;
        }
    }
}
