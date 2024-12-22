using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entities.BaseEntites
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public Int64 Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
