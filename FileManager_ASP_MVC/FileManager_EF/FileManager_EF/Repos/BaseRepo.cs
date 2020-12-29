using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager_EF_Models.Models;
using FileManager_EF.EF;
using FileManager_EF_Models.Models.Base;
using System.Data.Entity;

namespace FileManager_EF.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {

        private readonly DbSet<T> _table;
        private readonly FileManagerEntities _db;
        protected FileManagerEntities Context => _db;

        public BaseRepo()
        {
            _db = new FileManagerEntities();
            _table = _db.Set<T>();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return _db.SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return _db.SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return _db.SaveChanges();
        }

        public void Dispose()=> _db?.Dispose();


        public List<T> GetAll() => _table.ToList();


        public T GetOne(int? id) => _table.Find(id);


        public int Save(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return _db.SaveChanges();
        }

       // internal int SaveChanges () => _db.SaveChanges(); //можно добавить обработку ошибок, не реализовано за ненадобностью
    }
}
