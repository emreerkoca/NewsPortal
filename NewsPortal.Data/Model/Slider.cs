using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    [Table("Slider")]
    public class Slider : BaseEntity
    {
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Display(Name ="URL")]
        public string Url { get; set; }

        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name ="Image")]
        [Required(ErrorMessage ="Image is required")]
        public string ImageURL { get; set; }
    }
}
