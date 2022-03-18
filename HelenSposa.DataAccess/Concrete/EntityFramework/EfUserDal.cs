using HelenSposa.Core.DataAccess.EntityFramework;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.DataAccess.Abstract;
using HelenSposa.Entities.Dtos.Claim;
using HelenSposa.Entities.Dtos.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HelenSposa.DataAccess.Context;

namespace HelenSposa.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, HelenSposaDbContext>, IUserDal
    {
    }
}
