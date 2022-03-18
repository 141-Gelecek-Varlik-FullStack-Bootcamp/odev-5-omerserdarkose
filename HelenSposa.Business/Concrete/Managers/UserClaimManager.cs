using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HelenSposa.Business.Abstract;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Dtos.Claim;

namespace HelenSposa.Business.Concrete.Managers
{
    public class UserClaimManager:IUserClaimService
    {
        private IUserClaimDal _userClaimDal;
        private IMapper _mapper;

        public UserClaimManager(IUserClaimDal userClaimDal, IMapper mapper)
        {
            _userClaimDal = userClaimDal;
            _mapper = mapper;
        }

        public List<ClaimShowDto> GetClaimsById(int id)
        {
            var userClaimList = _userClaimDal.GetClaims(id);
            return userClaimList;
        }
    }
}
