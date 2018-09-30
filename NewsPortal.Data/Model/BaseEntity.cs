using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Data.Model
{
    public class BaseEntity
    {
        public int ID { get; set; }

        private DateTime Date = DateTime.Now;

        private bool ActiveVal = true;

        public DateTime UploadDate
        {
            get { return Date; }
            set { Date = value; }
        }

        public bool IsActive
        {
            get { return ActiveVal; }
            set { ActiveVal = value; }
        }
    }
}
