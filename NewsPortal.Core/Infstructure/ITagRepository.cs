using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Core.Infstructure
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> Tags(string[] tags);

        void AddTags(int newsID, string tag);

        void AddNewsTag(int newsID, string[] tags);
     }
}
