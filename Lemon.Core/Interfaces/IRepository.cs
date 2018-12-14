using Lemon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lemon.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
