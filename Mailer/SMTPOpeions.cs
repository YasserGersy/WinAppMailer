using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mailer
{
    class SmtpOptions
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }


        public SmtpOptions()
        {

        }
        public int Port = 587;

        internal void ServerLize(string p)
        {
            this.Port = 465;
            if(p.Contains(":"))
            {
                string[] serv_and_port = p.Split(new char[] { ':' });
                this.Server = serv_and_port[0].Trim();

                string int_s = serv_and_port[1].Trim();
                if (int_s.IsInt())
                    this.Port = int.Parse(int_s);
            }
        }

        internal System.Net.NetworkCredential Credentials()
        {
          return   new System.Net.NetworkCredential(this.Email,this.Password);
        }

        internal void DetectServer()
        {
            if (this.Email.Contains("@gmail.com"))
            {
                this.Server = "smtp.gmail.com";
                this.Port = 587;
            }
            else   if(this.Email.Contains("@yahoo.com"))
              {
                  this.Server = "smtp.mail.yahoo.com";
                  this.Port = 465;
              }
        }

        internal static string GuessServer(string p)
        {
            if (p.Contains("@gmail.com"))
                return "smtp.gmail.com:587";
            else
                if (p.Contains("@yahoo.com"))
                    return "smtp.mail.yahoo.com:465";
            return "";
        }
    }
}
