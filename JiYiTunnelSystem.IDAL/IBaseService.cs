using System;
using System.Linq;
using System.Threading.Tasks;
using JiYiTunnelSystem.Models;

namespace JiYiTunnelSystem.IDAL
{
    public interface IBaseService<T>:IDisposable where T:BaseEntity
    {
        Task CreateAsync(T model, bool saved = true);
        Task EditAsync(T model, bool saved = true);
        Task RemoveAsync(T model, bool saved = true);
        Task RemoveAsync(long id, bool saved = true);
        Task Save();
        Task<T> GetOneByIdAsync(long id, sbyte siDeleted = 0);
        IQueryable<T> GetAllAsync(sbyte isDeleted=0);
        IQueryable<T> GetSomeByTimeAsync(DateTime beginTime, DateTime endTime, bool asc = true, sbyte isDeleted = 0);
        IQueryable<T> GetSomeByTimePageOrderAsync(DateTime beginTime, DateTime endTime, int pageSize = 10, int pageIndex = 0, bool asc = true, sbyte isDeleted = 0);
        IQueryable<T> GetAllOrderAsync(bool asc = true, sbyte isDeleted = 0);
        IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true, sbyte isDeleted = 0);
    }
}
