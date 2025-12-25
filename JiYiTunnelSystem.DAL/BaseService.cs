using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using JiYiTunnelSystem.Models;
using JiYiTunnelSystem.IDAL;
using System;

namespace JiYiTunnelSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        private readonly JiYiContext _db;
        public BaseService(JiYiContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(T model, bool saved = true)
        {
            _db.Set<T>().Add(model);
            if (saved) await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public IQueryable<T> GetAllAsync(sbyte isDeleted = 0)
        {
            return _db.Set<T>().Where(m=>m.IsDeleted==isDeleted).AsNoTracking();
        }

        public IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true, sbyte isDeleted = 0)
        {
            return GetAllOrderAsync(asc, isDeleted).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllOrderAsync(bool asc = true, sbyte isDeleted = 0)
        {
            var datas = GetAllAsync(isDeleted);
            datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
            return datas;
        }

        public async Task<T> GetOneByIdAsync(long id, sbyte isDeleted = 0)
        {
            return await GetAllAsync(isDeleted).FirstAsync(m => m.Id == id);
        }

        public IQueryable<T> GetSomeByTimeAsync(DateTime beginTime, DateTime endTime, bool asc = true, sbyte isDeleted = 0)
        {
            return GetAllOrderAsync(asc, isDeleted).Where(m => m.CreateTime >= beginTime && m.CreateTime <= endTime);
        }

        public IQueryable<T> GetSomeByTimePageOrderAsync(DateTime beginTime, DateTime endTime, int pageSize = 10, int pageIndex = 0, bool asc = true, sbyte isDeleted = 0)
        {
            return GetAllOrderAsync(asc, isDeleted).Where(m => m.CreateTime >= beginTime && m.CreateTime <= endTime).
                Skip(pageIndex * pageSize).Take(pageSize);
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id, saved);
        }

        public async Task RemoveAsync(long id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsDeleted = 1;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}
