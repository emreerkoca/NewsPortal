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
    public class TagRepository : ITagRepository
    {
        private readonly NewsContext _context = new NewsContext();

        public int Count()
        {
            return _context.Tag.Count();
        }

        public void Delete(int id)
        {
            Tag tag = GetById(id);
            if (tag != null)
            {
                _context.Tag.Remove(tag);
            }
        }

        public IEnumerable<Data.Model.Tag> GetAll()
        {
            return _context.Tag.Select(x => x); //Get all news
        }

        public Tag GetById(int id)
        {
            return _context.Tag.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Tag obj)
        {
            _context.Tag.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Tag obj)
        {
            _context.Tag.AddOrUpdate(obj);
        }

        public IQueryable<Tag> GetMany(Expression<Func<Tag, bool>> expression)
        {
            return _context.Tag.Where(expression);
        }

        public Tag Get(Expression<Func<Tag, bool>> expression)
        {
            return _context.Tag.FirstOrDefault(expression);
        }

        public IQueryable<Tag> Tags(string[] tags)
        {
            return _context.Tag.Where(x => tags.Contains(x.TagName));
        }

        public void AddTags(int newsID, string tagsStr)
        {
            if(!string.IsNullOrEmpty(tagsStr))
            {
                string[] tags = tagsStr.Split(',');
                foreach (var tagName in tags)
                {
                    Tag tag = this.Get(x => x.TagName.ToLower() == tagName.ToLower().Trim());
                    if(tag == null)
                    {
                        tag = new Tag();
                        tag.TagName = tagName;
                        this.Insert(tag);
                        this.Save();
                    }
                }
                this.AddNewsTag(newsID, tags);
            }
        }

        public void AddNewsTag(int newsID, string[] tags)
        {
            var news = _context.News.FirstOrDefault(x => x.ID == newsID);
            var pullTags = this.Tags(tags);

            news.Tags.Clear();
            pullTags.ToList().ForEach(tag => news.Tags.Add(tag));
            _context.SaveChanges();
        }
    }
}
