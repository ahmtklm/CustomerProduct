using CustomerProduct.Common.EntityResponseStructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerProduct.Data.Contracts
{
    public interface IGenericRepository<T> where T: class
    {
        ServiceEntityResponse<T> GetAll();
        ServiceEntityResponse<T> FindAllBy(Expression<Func<T, bool>> predicate);
        ServiceEntityResponse<T> FindFirstBy(Expression<Func<T, bool>> predicate);
        ServiceEntityResponse<T> Find(int id);
        ServicePrimitiveResponse Add(T entity);
        ServicePrimitiveResponse Delete(T entity);
        void Delete(IQueryable<T> entities);
        void Edit(T entity);
        ServicePrimitiveResponse Save();
    }
}
