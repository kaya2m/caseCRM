using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using caseCRM.Entities.Common;

namespace caseCRM.Entities.Entity
{
    [Table("customer")]
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string lastname { get; set; }

        [Required]
        [MaxLength(255)]
        public string email { get; set; }

        [Required]
        [MaxLength(50)]
        public string region { get; set; }
    }
}
