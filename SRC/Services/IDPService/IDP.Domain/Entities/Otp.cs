using IDP.Domain.Entities.BaseEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entities
{
    public class Otp : BaseEntity 
    {
        public string? FullName { get; set; }
        public   string? CodeMeli { get; set; }
         public required string UserName { get; set; }
         public string? Password { get; set; }
         public string? Salt { get; set; }
         public required string MobileNumber { get; set; }

    }
}
