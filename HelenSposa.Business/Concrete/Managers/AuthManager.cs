using AutoMapper;
using HelenSposa.Business.Abstract;
using HelenSposa.Business.Constant;
using HelenSposa.Business.ValidationRules.FluentValidation;
using HelenSposa.Core.Aspects.Autofac;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.Core.Utilities.Security;
using HelenSposa.Core.Utilities.Security.Hashing;
using HelenSposa.Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Concrete.Managers
{
    public class AuthManager : IAuthService
    {
        private IUserService _userManager;
        private ITokenHelper _tokenHelper;
        private IMapper _mapper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userManager = userService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userManager.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, _mapper.Map<List<OperationClaim>>(claims.Data));

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }


        [ValidationAspect(typeof(UserLoginValidator))]
        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck=_userManager.GetByMail(userLoginDto.Email);
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordNotMatch);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(UserRegisterValidator))]
        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);

            var newUser = _mapper.Map<User>(userRegisterDto);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.IsActive = true;

            _userManager.Add(newUser);

            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }

        [CacheAspect(duration: 5)]
        public IResult UserNotExists(string email)
        {
            if (_userManager.GetByMail(email)!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            return new SuccessResult();
        }
    }
}
