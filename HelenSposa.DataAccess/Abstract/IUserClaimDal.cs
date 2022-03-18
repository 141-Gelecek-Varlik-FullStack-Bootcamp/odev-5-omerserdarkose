using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HelenSposa.Core.DataAccess;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Entities.Dtos.Claim;

namespace HelenSposa.DataAccess.Abstract
{
    public interface IUserClaimDal:IEntityRepository<UserClaim>
    {
        List<ClaimShowDto> GetClaims(int id);
    }
}
