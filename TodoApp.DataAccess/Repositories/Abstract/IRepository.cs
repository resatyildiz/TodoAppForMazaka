using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoApp.DataAccess.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity:class // Bu yaklaşım interface yapısında kullanılan bir Class Object Parameter yaklaşımı olarak görülebilir.
    {
        TEntity GetById(string id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        void Remove(string id);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
