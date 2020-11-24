using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagementSystem.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void AddOrUpdate(T entity);
        void Delete(int id);
    }
}
