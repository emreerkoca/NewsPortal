using NewsPortal.Core.Infstructure;
using NewsPortal.Data.DataContext;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace NewsPortal.Core.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.Image.Count();
        }

        public void Delete(int id)
        {
            var image = GetById(id);
            if (image != null)
            {
                _context.Image.Remove(image);
            }
        }

        public IEnumerable<Data.Model.Image> GetAll()
        {
            return _context.Image.Select(x => x); //Get all images
        }

        public Image GetById(int id)
        {
            return _context.Image.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Image obj)
        {
            _context.Image.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Image obj)
        {
            _context.Image.AddOrUpdate(obj);
        }

        public IQueryable<Image> GetMany(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.Where(expression);
        }

        public Image Get(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.FirstOrDefault(expression);
        }
    }
}
