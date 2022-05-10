using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClassLibrary.Repo
{

    public interface IReponsitory<T> where T : class
    {
        List<T> getAll();
        T getById(object id);
        void insert(T obj);
        void update(T obj);
        void delete(object id);
        void save();
    }

}
