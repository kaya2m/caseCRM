using caseCRM.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.DataAccess
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
