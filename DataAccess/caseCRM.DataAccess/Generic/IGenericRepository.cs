using caseCRM.Entities.Common;
using caseCRM.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.DataAccess.Common
{
    public interface IGenericRepository<T> : IBaseRepository<T> where T :BaseEntity
    {
        Task<ResponseDto<IQueryable<T>>> GetAll(bool tracking = true);
        Task<ResponseDto<T>> GetByIdAsync(string id, bool tracking = true);
        Task<ResponseDto<IQueryable<T>>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        ResponseDto<IQueryable<T>> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<ResponseDto<T>> AddAsync(T entity);
        bool Update(T entity);
        Task<ResponseDto<T>> UpdateAsync(T entity);
        bool HardRemove(T entity);
        Task<ResponseDto<T>> SoftRemoveAsync(Guid id);
        Task<int> SaveAsync();
    }
}
