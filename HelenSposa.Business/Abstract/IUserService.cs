using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Core.Utilities.Result;
using HelenSposa.Entities.Dtos.OperationClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaimShowDto>> GetClaims(User user);

        //IDataResult<List<UserShowDto>> GetAll();

        IResult Add(User addUser);

        User GetByMail(string eMail);

        User GetById(int userId);

        IResult Delete(int id);

        //IResult SetClaims (User user, List<OperationClaim> operationClaims);
        //kullaniciya yetki ilaveleri yapan bir method yazilacak, user ile ilgili diger 2 tablo dahil edildikten sonra
    }
}
