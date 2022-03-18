using HelenSposa.Core.DataAccess;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Entities.Dtos.Claim;
using HelenSposa.Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.DataAccess.Abstract
{
    public interface IUserDal: IEntityRepository<User>
    {
    }
}
