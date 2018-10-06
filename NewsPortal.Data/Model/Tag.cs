using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    [Table("Tag")]
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public virtual ICollection<News> News { get; set; }

    }
}
