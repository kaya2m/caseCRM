using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public bool active { get; set; } = true;
        public DateTime? createdat { get; set; } = DateTime.UtcNow;
        virtual public DateTime? updatedat { get; set; } = null;
    }
}
