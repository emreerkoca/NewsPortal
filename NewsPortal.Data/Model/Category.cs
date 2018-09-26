using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(2,ErrorMessage ="Category name can' t be less than {0} characters."), MaxLength(150, ErrorMessage = "Category name can' t be more than 150 characters")]
        public string CategoryName { get; set; }

        public int ParentID { get; set; }

        [MinLength(2, ErrorMessage = "Url can' t be less than {0} characters."), MaxLength(150, ErrorMessage = "Url can' t be more than 150 characters")]
        public string URL { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
