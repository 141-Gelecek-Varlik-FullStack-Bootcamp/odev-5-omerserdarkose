using HelenSposa.Business.Abstract;
using HelenSposa.Business.Aspects.Autofac;
using HelenSposa.Business.Constant;
using HelenSposa.Core.Aspects.Autofac;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Dtos.OperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User addUser)
        {
            _userDal.Add(addUser);
            return new SuccessResult();
        }

        [SecuredOperation(roles:"admin")]
        //[CacheRemoveAscpect("IUserService.Get")]
        public IResult Delete(int id)
        {
            _userDal.Delete(new User { Id=id});
            return new SuccessResult();
        }

        [CacheAspect(duration: 5)]
        public User GetByMail(string eMail)
        {
            var user=_userDal.Get(u => u.Email == eMail);
            return user;
        }

        [CacheAspect(duration: 5)]
        public IDataResult<List<OperationClaimShowDto>> GetClaims(User user)
        {
            var claims = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaimShowDto>>(claims);
        }
    }
}
