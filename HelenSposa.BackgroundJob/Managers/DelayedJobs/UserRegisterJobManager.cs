using HelenSposa.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.BackgroundJob.Managers.DelayedJobs
{
    public class UserRegisterJobManager
    {
        private IMailService _mailService;

        public UserRegisterJobManager(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Process(int userId)
        {
            await _mailService.SendUserWelcomeMail(userId);
        }
    }
}
