using IDP.Application.Command.User;
using IDP.Domain.IRepository.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Application.Handler.Command.User
{
    public class UserHandler : IRequestHandler<UserCommand, bool>
    {
      public readonly   IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.Insert(new Domain.Entities.User
            {
                FullName=request.FullName,
                CodeMeli=request.CodeMeli   

            });
            return true;
        }
    }
}
