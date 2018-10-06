using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    public class NewsTag
    {
        public News News { get; set; }

        public string[] PullTags { get; set; }

        public IEnumerable<Tag> Tags { get; set; } 

        public string TagName { get; set; }
    }
}
