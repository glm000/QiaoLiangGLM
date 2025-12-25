using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace JiYiTunnelSystem.MVCSite
{
    public class MailHelp
    {
        public static void SendVerify(int codeLength,string mail)
        {
            string str = string.Empty;
            Random random = new Random();
            char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'e', 'f', 'g', 'h', 'm', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'M' };
            for (int i = 0; i < codeLength; i++)
            {
                str += character[random.Next(character.Length)];
            }
            HttpContext.Current.Session["verifyCode"] = str;
            str = "\t您的验证码是\t" + str + "，5分钟内有效。";
            MailHelp mailHelp = new MailHelp();
            mailHelp.SendMail(mail, str);
        }
        private void SendMail(string toPeople, string bodyTxt)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("2746605116@qq.com");
            mailMessage.To.Add(toPeople);
            mailMessage.Subject = "验证码";
            mailMessage.Body = bodyTxt;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("2746605116@qq.com", "cpvxarlyixjwdfig");
            smtpClient.Host = "smtp.qq.com";
            smtpClient.Port = 587;
            smtpClient.Send(mailMessage);
        }
    }
}