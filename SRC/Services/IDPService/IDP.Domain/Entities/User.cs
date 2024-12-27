using IDP.Domain.Entities.BaseEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entities
{
    public class User: BaseEntity 
    {
        public string? FullName { get; set; }
        public  required string CodeMeli { get; set; }
         public required string UserName { get; set; }
         public required string Password { get; set; }
         public required string Salt { get; set; }
         public required string MobileNumber { get; set; }

    }
}
