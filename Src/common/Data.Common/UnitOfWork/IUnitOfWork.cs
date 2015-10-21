namespace Data.Common.UnitOfWork
{
    using System;
    using System.Data.Entity;

    public interface IUnitOfWork : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        void Commit();
    }
}
