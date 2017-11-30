using System;
using System.Collections.Generic;

namespace ProjectBase.Logic.Services
{
    public interface IService<T>
    {
        List<T> GetAll();
        T Find(Guid Id);
        void Create(T item);
        void Edit(T item);
        void Delete(Guid Id);
    }
}
