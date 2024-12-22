using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Command.User
{
    public class UserCommand:IRequest<Boolean>
    {
        [Required(ErrorMessage ="نام الزامی هست")]
        [MinLength(4)]
        public required string  Name { get; set; }
        public int age { get; set; }
    }
}
