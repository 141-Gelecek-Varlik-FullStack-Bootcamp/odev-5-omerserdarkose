using HelenSposa.Business.Abstract;
using HelenSposa.Business.Aspects.Autofac;
using HelenSposa.Business.Constant;
using HelenSposa.Core.Aspects.Autofac;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Dtos.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HelenSposa.Business.ValidationRules.FluentValidation;
using HelenSposa.Core.Extensions;
using HelenSposa.Core.Utilities.Security.Hashing;
using HelenSposa.Core.Utilities.Security.PasswordCreator;
using HelenSposa.Entities.Dtos.User;
using Microsoft.AspNetCore.Http;


namespace HelenSposa.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserClaimService _userClaimManager;

        public UserManager(IUserDal userDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserClaimService userClaimManager)
        {
            _userDal = userDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userClaimManager = userClaimManager;
        }

        //[ValidationAspect(typeof(UserAddValidator))]
        public IDataResult<List<UserShowDto>> GetAll()
        {
            var userList = _userDal.GetList();

            var mapUserList = _mapper.Map<List<UserShowDto>>(userList);
            return new SuccessDataResult<List<UserShowDto>>(mapUserList);
        }

        public IResult Add(UserAddDto userAddDto)
        {

            byte[] passwordHash, passwordSalt;

            var password = PasswordHelper.CreatePassword();

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var newUser = _mapper.Map<User>(userAddDto);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.IsActive = true;
            newUser.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newUser.Idate = DateTime.Now;

            _userDal.Add(newUser);
            return new SuccessResult(Messages.UserAdded);
        }

        //[SecuredOperation(roles:"admin")]
        //[CacheRemoveAscpect("IUserService.Get")]
        public IResult Delete(int id)
        {
            _userDal.Delete(new User { Id=id});
            return new SuccessResult();
        }

        //[CacheAspect(duration: 5)]
        public User GetByMail(string eMail)
        {
            var user=_userDal.Get(u => u.Email == eMail);
            return user;
        }

        //[CacheAspect(duration: 5)]
        public IDataResult<List<ClaimShowDto>> GetUserClaims(int id)
        {
            var claims =_userClaimManager.GetClaimsById(id);
            return new SuccessDataResult<List<ClaimShowDto>>(claims);
        }

        //[CacheAspect(duration: 5)]
        public IResult UserNotExists(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            if (user is not null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            return new SuccessResult();
        }

    }
}
