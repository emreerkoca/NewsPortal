using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Core.Infstructure
{
    public interface IRepository<T> where T : class  
    {
        //Get all data
        IEnumerable<T> GetAll();

        //Get data by id
        T GetById(int id);

        T Get(Expression<Func<T, bool>> expression);

        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);

        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        int Count();

        void Save();



    }
}
