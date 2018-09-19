using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Core.Infstructure
{
    public interface INewsRepository : IRepository<News>
    {
    }
}
