using NewsPortal.Core.Infstructure;
using NewsPortal.Data.DataContext;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Core.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.News.Count();
        }

        public void Delete(int id)
        {
            News news = GetById(id);
            if(news != null)
            {
                _context.News.Remove(news);
            }
        }

        public IEnumerable<Data.Model.News> GetAll()
        {
            return _context.News.Select(x => x); //Get all news
        }

        public News GetById(int id)
        {
            return _context.News.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(News obj)
        {
            _context.News.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(News obj)
        {
            _context.News.AddOrUpdate(obj);
        }

        public IQueryable<News> GetMany(Expression<Func<News, bool>> expression)
        {
            return _context.News.Where(expression);
        }

        public News Get(Expression<Func<News, bool>> expression)
        {
            return _context.News.FirstOrDefault(expression);
        }
    }
}
