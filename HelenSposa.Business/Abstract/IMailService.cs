using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Abstract
{
    public interface IMailService
    {
        Task SendUserWelcomeMail(int userId);
    }
}
