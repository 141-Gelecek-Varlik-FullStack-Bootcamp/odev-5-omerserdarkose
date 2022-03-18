using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HelenSposa.Core.DataAccess.EntityFramework;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.DataAccess.Context;
using HelenSposa.Entities.Dtos.Claim;

namespace HelenSposa.DataAccess.Concrete.EntityFramework
{
    public class EfUserClaimDal:EfEntityRepositoryBase<UserClaim,HelenSposaDbContext>,IUserClaimDal
    {
        public List<ClaimShowDto> GetClaims(int id)
        {
            using (var context=new HelenSposaDbContext())
            {
                var result = from uc in context.UserClaims
                    join c in context.Claims
                        on uc.ClaimId equals c.Id
                    where uc.UserId == id
                    select new ClaimShowDto
                    {
                        Id = c.Id,
                        ClaimName = c.Name
                    };

                return result.ToList();
            }
            
        }
    }
}
