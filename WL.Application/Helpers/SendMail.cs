using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace WL.Application.Helpers {

   public class SendMail {
      readonly string address;

      public SendMail(string address) {
         this.address = address;
      }

      public bool Send(string to, string subject, string msg) {
         return Send(new string[] { to }, subject, msg);
      }

      public bool Send(string[] to, string subject, string msg) {
         var cfg = new MailCfg(address);
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
      public string smtpClient;
      public string from;
      public int port;
      public bool ssl;
      public string password;
      public string signature;

      public MailCfg(string address) {
         smtpClient = "smtp.live.com";
         from = "andres.9010@hotmail.com";
         port = 587;
         ssl = true;
         password = "homero12996177";
      }
   }
}