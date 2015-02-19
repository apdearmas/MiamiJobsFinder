using System;
using System.Linq;

namespace DAL
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
    }
}
