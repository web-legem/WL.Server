using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace WL.Application.Helpers {

   public static class SendMail {
           
      public static bool Send(string to, string subject, string msg) {
         return Send(new string[] { to }, subject, msg);
      }

      public static bool Send(string[] to, string subject, string msg) {
         var cfg = MailCfg.GetBaseDirectory();
         try {
            var mail = new MailMessage();
            var SmtpServer = new SmtpClient(cfg.smtpClient);

            mail.From = new MailAddress(cfg.from);
            for(var i = 0; i < to.Length; i++) {
               mail.To.Add(to[i]);
            }
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = GetHtmlMsg(cfg, subject, msg);

            SmtpServer.Port = cfg.port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(cfg.from, cfg.password);
            SmtpServer.EnableSsl = cfg.ssl;

            SmtpServer.Send(mail);
            return true;
         } catch(Exception ex) {
            Console.WriteLine(ex);
            return false;
         }
      }

      public static string GetHtmlMsg(MailCfg cfg, string sub, string msg) {
         var html = ReadTemplate();
         html = html.Replace("{headerTitle}", sub);
         html = html.Replace("{Subject}", sub);
         html = html.Replace("[\r\n\t]", "");
         html = html.Replace("{Mesage}", msg);
         return html;
      }

      public static string ReadTemplate() {
         var sb = new StringBuilder();
         try {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Helpers\basicMailTemplate.html");
            using(var fs = File.Open(path, FileMode.Open)) {
               using(var bs = new BufferedStream(fs)) {
                  using(var sr = new StreamReader(bs)) {
                     string str;
                     while((str = sr.ReadLine()) != null) {
                        sb.Append(str);
                     }
                  }
               }
            }
            return sb.ToString();
         } catch(Exception ex) {
            throw new Exception(ex.Message);
         }
      }
   }

   public class MailCfg {
      public string smtpClient { get; set; }
      public string from { get; set; }
      public int port { get; set; }
      public bool ssl { get; set; }
      public string password { get; set; }



      public static MailCfg GetBaseDirectory() {
         var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

         return new MailCfg {
            smtpClient = configuration["email:smtpClient"],
            from = configuration["email:user"],
            password = configuration["email:password"],
            ssl = Convert.ToBoolean(configuration["email:ssl"]),
            port = Convert.ToInt32(configuration["email:port"])
         };
      }
   }
}