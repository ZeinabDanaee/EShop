using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Command.User
{
    public class UserCommand : IRequest<Boolean>
    {
        public string? FullName { get; set; }
        public  string? CodeMeli { get; set; }
        public required string UserName { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public required string MobileNumber { get; set; }

    }
}