using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.connection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelClassLibrary.Repo.impl
{
    public class Reponsitory<T> : IReponsitory<T> where T : class
    {
        protected readonly PermissionContext m_context;
        protected DbSet<T> m_table = null;
        public Reponsitory(PermissionContext context)
        {
            m_context = context;
            m_table = m_context.Set<T>();
        }
        public void delete(object id)
        {
            T obj = m_table.Find(id);
            m_table.Remove(obj);
            save();
        }
        public void insert(T obj)
        {
            //m_table.Add(obj);
            m_context.Add(obj).State = EntityState.Added;
            save();
        }
        public void save()
        {
            m_context.SaveChanges();
        }
        public List<T> getAll()
        {
            return m_table.ToList();
        }
        public T getById(object id)
        {
            return m_table.Find(id);
        }
        public void update(T obj)
        {
            m_table.Update(obj);
            save();
        }
    }
}
