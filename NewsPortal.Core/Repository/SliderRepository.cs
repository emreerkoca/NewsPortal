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
    public class SliderRepository : ISliderRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.News.Count();
        }

        public void Delete(int id)
        {
            Slider category = GetById(id);
            if (category != null)
            {
                _context.Slider.Remove(category);
            }
        }

        public IEnumerable<Data.Model.Slider> GetAll()
        {
            return _context.Slider.Select(x => x); //Get all news
        }

        public Slider GetById(int id)
        {
            return _context.Slider.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Slider obj)
        {
            _context.Slider.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Slider obj)
        {
            _context.Slider.AddOrUpdate(obj);
        }

        public IQueryable<Slider> GetMany(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.Where(expression);
        }

        public Slider Get(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.FirstOrDefault(expression);
        }
    }
}
