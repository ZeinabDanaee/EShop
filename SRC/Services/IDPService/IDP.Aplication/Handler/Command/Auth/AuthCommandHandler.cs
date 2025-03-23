using IDP.Application.Command.Auth;
using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDP.Domain.IRepository.Command;



namespace IDP.Application.Handler.Command.Auth
{
    public class AuthCommandHandler: IRequestHandler<AuthCommand,bool>
    {
        private readonly IOtpRedisRepository _otpRedisRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public AuthCommandHandler(IOtpRedisRepository otpRedisRepository, IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _otpRedisRepository = otpRedisRepository;
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

       

        public async Task<bool> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
           try
            {
                var userobj = _mapper.Map<IDP.Domain.Entities.User>(request);
                var user=await _userQueryRepository.Ge

            }
            catch
            {

            }
        }
    }
}
