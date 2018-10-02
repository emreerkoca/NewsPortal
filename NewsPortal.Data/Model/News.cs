using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    [Table("News")]
    public class News : BaseEntity
    {
        //[Key]
        //public int ID { get; set; }

        [Display(Name ="Title:")]
        public string Title { get; set; }

        [Display(Name = "Short Description:")]
        public string ShortDescription { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        public int Reads { get; set; }

        //public bool IsActive { get; set; }

        //public DateTime UploadDate  { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public string ImageStr { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}
