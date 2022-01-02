using HelenSposa.Business.Abstract;
using HelenSposa.Entities.Dtos.Mail;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Concrete.Managers
{
    public class MailManager : IMailService
    {
        private SmtpConfigDto _smtpConfigDto;
        private IUserService _userService;

        public MailManager(IOptions<SmtpConfigDto> options, IUserService userService)
        {
            _smtpConfigDto = options.Value;
            _userService = userService;
        }

        public async Task SendUserWelcomeMail(int userId)
        {
            using (var client = CreateSmtpClient())
            {
                var userInfo = _userService.GetById(userId);

                MailMessageDto mailMessageDto = new MailMessageDto
                {
                    Body = "Sayin " +
                    userInfo.FirstName + " " + userInfo.LastName +
                    " sirketimize hos geldiniz",
                    To = userInfo.Email,
                    Subject = "Hos Geldiniz",
                    From = _smtpConfigDto.User
                };
                MailMessage mailMessage = mailMessageDto.GetMailMessage();
                mailMessage.IsBodyHtml = true;
                await client.SendMailAsync(mailMessage);
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            SmtpClient smtp = new SmtpClient(_smtpConfigDto.Host, _smtpConfigDto.Port);
            smtp.Credentials = new NetworkCredential(_smtpConfigDto.User, _smtpConfigDto.Password);
            smtp.EnableSsl = _smtpConfigDto.UseSsl;
            return smtp;
        }
    }
}
