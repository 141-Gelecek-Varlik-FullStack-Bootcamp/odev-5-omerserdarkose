using HelenSposa.BackgroundJob.Managers.DelayedJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.BackgroundJob
{
    public static class DelayedJobs
    {
        public static void SendMailWelcomeJobs (int userId)
        {
            Hangfire.BackgroundJob.Schedule<UserRegisterJobManager>
                (
                job => job.Process(userId), TimeSpan.FromMinutes(1)
                );
        }
    }
}
