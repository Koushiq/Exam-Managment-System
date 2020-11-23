using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public class UserServices
    {
        public static bool SendVerificationCode(string firstName, string emailAddress, string code, string purpose)
        {
            string mailBody = null, mailSubject = null;
            string companyName = ConfigurationManager.AppSettings["companyName"];
            mailBody = $"<!DOCTYPE html> <html> <body style='font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; margin: 0; padding: 0;' bgcolor='#f6f6f6'> <table role='presentation' border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;' bgcolor='#f6f6f6'> <tr> <td>&nbsp;</td> <td> <div style='box-sizing: border-box; display: block; max-width: 580px; margin: 0 auto; padding: 10px;'> <table role='presentation' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; border-radius: 3px;' bgcolor='#ffffff'> <tr> <td style='box-sizing: border-box; padding: 20px;'> <table role='presentation' border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'> <tr> <td> <h2 style='color: #000000; font-family: sans-serif; font-weight: 400; line-height: 1.4; margin: 0 0 30px;'>Welcome to {companyName}, <b>{firstName}</b> ! </h2> <p>Thanks for signing up for <b>{companyName}</b>. There is only one more step to go. <br> Enter the <b>verification code</b> below in <b>{companyName} App</b> to verify your email address, </p> <table role='presentation' border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;'> <tbody> <tr> <td align='center' style='padding-bottom: 15px;'> <table role='presentation' border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;'> <tbody> <tr> <td> <a style='background-color: #FF3300; border-radius: 5px; box-sizing: border-box; color: #ffffff; cursor: pointer; display: inline-block; font-size: 18px; font-weight: bold; text-decoration: none; text-transform: capitalize; margin: 0; padding: 5px 15px; border: 1px solid #3498db;'>{code}</a> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </table> </td> </tr> </table> <div style='clear: both; margin-top: 10px; width: 100%;' align='center'> <table role='presentation' border='0' cellpadding='0' cellspacing='0' style='border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;'> <tr> <td style='padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px;' align='center'> <span style='color: #999999; font-size: 12px; text-align: center;'>{companyName}</span> <br> &copy; Copyright {DateTime.Now.Year} {companyName} </td> </tr> <tr> <td style='padding-bottom: 10px; padding-top: 10px; color: #999999; font-size: 12px;' align='center'> Powered by {companyName} </td> </tr> </table> </div> </div> </td> <td>&nbsp;</td> </tr> </table> </body> </html>";
            mailSubject = $"{companyName} | {purpose} Code";

            return MailServices.SendEmail(emailAddress, mailSubject, mailBody);
        }

        public static string GenerateRandomNumericCode()
        {
            Random random = new Random();

            int length = int.Parse(ConfigurationManager.AppSettings["verificationCodeLength"]);
            string code = "";
            for(int i = 0; i < length; i++)
            {
                code += (random.Next() % 10);
            }
            return code;
        }
    }
}