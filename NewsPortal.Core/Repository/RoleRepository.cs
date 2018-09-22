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
    public class RoleRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.Role.Count();
        }

        public void Delete(int id)
        {
            var role = GetById(id);
            if (role != null)
            {
                _context.Role.Remove(role);
            }
        }

        public IEnumerable<Data.Model.Role> GetAll()
        {
            return _context.Role.Select(x => x); //Get all roles
        }

        public Role GetById(int id)
        {
            return _context.Role.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Role obj)
        {
            _context.Role.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Role obj)
        {
            _context.Role.AddOrUpdate(obj);
        }

        public IQueryable<Role> GetMany(Expression<Func<Role, bool>> expression)
        {
            return _context.Role.Where(expression);
        }

        public Role Get(Expression<Func<Role, bool>> expression)
        {
            return _context.Role.FirstOrDefault(expression);
        }
    }
}
