using caseCRM.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Entities.Entity
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string username { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }

        [Required]
        [MaxLength(22)]
        public string role { get; set; }
    }
}
