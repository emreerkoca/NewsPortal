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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.News.Count();
        }

        public void Delete(int id)
        {
            Category category = GetById(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }
        }

        public IEnumerable<Data.Model.Category> GetAll()
        {
            return _context.Category.Select(x => x); //Get all news
        }

        public Category GetById(int id)
        {
            return _context.Category.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Category obj)
        {
            _context.Category.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Category obj)
        {
            _context.Category.AddOrUpdate(obj);
        }

        public IQueryable<Category> GetMany(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.Where(expression);
        }

        public Category Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.FirstOrDefault(expression);
        }
    }
}
