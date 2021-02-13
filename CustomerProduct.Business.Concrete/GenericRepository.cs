using CustomerProduct.Business.Contracts;
using CustomerProduct.Common;
using CustomerProduct.Common.EntityResponseStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerProduct.Business.Concrete
{
    public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly ServiceEntityResponse<T> _serviceEntityResponse;
        private readonly ServicePrimitiveResponse _servicePrimitiveResponse;
        private readonly DbContext Context;
        private bool _disposed;

        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext;
            _serviceEntityResponse = new ServiceEntityResponse<T>();
            _servicePrimitiveResponse = new ServicePrimitiveResponse();
        }

        #region IGenericRepository Members

        public virtual ServiceEntityResponse<T> GetAll()
        {
            if (Context.Set<T>().Any())
            {
                _serviceEntityResponse.EntityDataList = new List<T>(Context.Set<T>());
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            else
            {
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.NoRecordFound;
            }
            return _serviceEntityResponse;
        }

        public virtual ServiceEntityResponse<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            if (Context.Set<T>().Where(predicate).Any())
            {
                _serviceEntityResponse.EntityDataList = Context.Set<T>().Where(predicate).ToList();
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            else
            {
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.NoRecordFound;
            }
            return _serviceEntityResponse;
        }

        public virtual ServiceEntityResponse<T> FindFirstBy(Expression<Func<T, bool>> predicate)
        {
            if (Context.Set<T>().FirstOrDefault(predicate) != null)
            {
                _serviceEntityResponse.EntityData = Context.Set<T>().FirstOrDefault(predicate);
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            else
            {
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.NoRecordFound;
            }
            return _serviceEntityResponse;
        }

        public virtual ServiceEntityResponse<T> Find(int id)
        {
            if (Context.Set<T>().Find(id) != null)
            {
                _serviceEntityResponse.EntityData = Context.Set<T>().Find(id);
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            else
            {
                _serviceEntityResponse.ResponseCode = EntityResponseCodes.NoRecordFound;
            }
            return _serviceEntityResponse;
        }

        public virtual ServicePrimitiveResponse Add(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Set<T>().Add(entity);
                Context.Entry(entity).State = EntityState.Added;
                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                //_logger.Error(ex, "Repository: {@Repository} | Method: {@Method}", nameof(GenericRepository<T>), System.Reflection.MethodBase.GetCurrentMethod().Name);
                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.DbError;
                _servicePrimitiveResponse.InnerException = ex;
            }
            return _servicePrimitiveResponse;
        }

        public virtual ServicePrimitiveResponse Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Set<T>().Remove(entity);
                Context.Entry(entity).State = EntityState.Deleted;
                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                //_logger.Error(ex, "Repository: {@Repository} | Method: {@Method}", nameof(GenericRepository<T>), System.Reflection.MethodBase.GetCurrentMethod().Name);

                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.DbError;
                _servicePrimitiveResponse.InnerException = ex;
            }
            return _servicePrimitiveResponse;
        }

        public virtual void Delete(IQueryable<T> entities)
        {
            try
            {
                if (!entities.Any())
                    throw new ArgumentNullException(nameof(entities));

                foreach (T entity in entities.ToList())
                    Context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                //_logger.Error(ex, "Repository: {@Repository} | Method: {@Method}", nameof(GenericRepository<T>), System.Reflection.MethodBase.GetCurrentMethod().Name);

                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.DbError;
                _servicePrimitiveResponse.InnerException = ex;
            }
        }

        public virtual void Edit(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual ServicePrimitiveResponse Save()
        {
            try
            {
                _servicePrimitiveResponse.EntityPrimaryKey = Context.SaveChanges().ToString();
                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.Successfull;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                //_logger.Error(ex, "Repository: {@Repository} | Method: {@Method}", nameof(GenericRepository<T>), System.Reflection.MethodBase.GetCurrentMethod().Name);

                _servicePrimitiveResponse.ResponseCode = EntityResponseCodes.DbError;
                _servicePrimitiveResponse.InnerException = ex;
            }
            return _servicePrimitiveResponse;
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Context.Dispose();
            }

            _disposed = true;
        }



        ~GenericRepository()
        {
            Dispose(false);
        }

        #endregion

    }
}
