using caseCRM.Entities.Common;
using caseCRM.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace caseCRM.DataAccess.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        DbSet<T> Table => _appDbContext.Set<T>();

        DbSet<T> IBaseRepository<T>.Table => _appDbContext.Set<T>();

        public async Task<ResponseDto<T>> AddAsync(T entity)
        {
            try
            {
                await Table.AddAsync(entity);
                await _appDbContext.SaveChangesAsync();
                return ResponseDto<T>.Success(entity, "Entity added successfully", 201);
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.Fail(ex.Message, 500, false);
            }
        }

        public async Task<ResponseDto<IQueryable<T>>> GetAll(bool tracking = true)
        {
            try
            {
                IQueryable<T> query = tracking ? 
                    Table :
                    Table.AsNoTracking();
                return ResponseDto<IQueryable<T>>.Success(query, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<IQueryable<T>>.Fail(ex.Message, 500, false);
            }
        }

        public async Task<ResponseDto<T>> GetByIdAsync(string id, bool tracking = true)
        {
            try
            {
                var entity = tracking ? 
                    await Table.FindAsync(Guid.Parse(id)) :
                    await Table.AsNoTracking().FirstOrDefaultAsync(e => e.id == Guid.Parse(id));

                if (entity == null)
                {
                    return ResponseDto<T>.Fail("Entity not found", 404);
                }

                return ResponseDto<T>.Success(entity, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.Fail(ex.Message, 500, false);
            }
        }

        public ResponseDto<IQueryable<T>> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            try
            {
                IQueryable<T> query = tracking ? 
                    Table.Where(predicate) : 
                    Table.Where(predicate).AsNoTracking();
                return ResponseDto<IQueryable<T>>.Success(query, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<IQueryable<T>>.Fail(ex.Message, 500, false);
            }
        }

        public async Task<ResponseDto<IQueryable<T>>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            try
            {
                IQueryable<T> query = tracking ?
                    Table.Where(predicate) :
                    Table.Where(predicate).AsNoTracking();
                return await Task.FromResult(ResponseDto<IQueryable<T>>.Success(query, 200));
            }
            catch (Exception ex)
            {
                return ResponseDto<IQueryable<T>>.Fail(ex.Message, 500, false);
            }
        }

        public bool HardRemove(T entity)
        {
            try
            {
                Table.Remove(entity);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<ResponseDto<T>> SoftRemoveAsync(Guid id)
        {
            try
            {
                var entity = await Table.FindAsync(id);
                if (entity == null)
                {
                    return ResponseDto<T>.Fail("Entity not found", 404);
                }

                entity.active = false; 
                _appDbContext.Update(entity);
                await _appDbContext.SaveChangesAsync();
                return ResponseDto<T>.Success(entity, "Entity soft deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.Fail(ex.Message, 500, false);
            }
        }

        public bool Update(T entity)
        {
            try
            {
                Table.Update(entity);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseDto<T>> UpdateAsync(T entity)
        {
            try
            {
                Table.Update(entity);
                await _appDbContext.SaveChangesAsync();
                return ResponseDto<T>.Success(entity, "Entity updated successfully", 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.Fail(ex.Message, 500, false);
            }
        }
    }
}
