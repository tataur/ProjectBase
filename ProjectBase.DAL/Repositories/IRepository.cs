using System;
using System.Collections.Generic;

namespace ProjectBase.DAL.Repositories
{ 
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Find(Guid id);
        void Create(T item);
        void Edit(T item);
        void Delete(Guid id);
    }
}
