using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.Entities.Dtos.Claim;

namespace HelenSposa.Business.Abstract
{
    public interface IUserClaimService
    {
        List<ClaimShowDto>  GetClaimsById(int id);
    }
}
