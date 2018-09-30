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
    public class UserRepository : IUserRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.User.Count();
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
        }

        public IEnumerable<Data.Model.User> GetAll()
        {
            return _context.User.Select(x => x); //Get all users
        }

        public User GetById(int id)
        {
            return _context.User.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(User obj)
        {
            _context.User.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User obj)
        {
            _context.User.AddOrUpdate(obj);
        }

        public IQueryable<User> GetMany(Expression<Func<User, bool>> expression)
        {
            return _context.User.Where(expression);
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return _context.User.FirstOrDefault(expression);
        }
    }
}
