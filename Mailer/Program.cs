using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mailer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            if (global::Mailer.Properties.Settings.Default.SmtpServers == "")
                global::Mailer.Properties.Settings.Default.SmtpServers = "smtp.gmail.com:465,smtp.live.com:587,smtp.office365.com:587,plus.smtp.mail.yahoo.com:465,smtp.mail.yahoo.co.uk:465,smtp.mail.yahoo.com:465,smtp.mail.yahoo.com.au:465,smtp.o2.ie:25,smtp.o2.co.uk:25,smtp.att.yahoo.com:465,smtp.ntlworld.com:465,pop3.btconnect.com:25,mail.btopenworld.com:25,mail.btinternet.com:25,smtp.orange.net:25,smtp.orange.co.uk:25,smtp.wanadoo.co.uk:25,smtp.live.com:25,mail.o2online.de:25,securesmtp.t-online.de:587,smtp.1and1.com:587,smtp.1und1.de:587,smtp.comcast.net:587,outgoing.verizon.net:465,outgoing.yahoo.verizon.net:587,smtp.zoho.com:465,smtp.mail.com:465,smtp.gmx.com:465";
            global::Mailer.Properties.Settings.Default.Save();
  }
        public static bool IsInt(this string s )
        {
            bool b = false;
            int i = 0;
            b = int.TryParse(s, out i);
            return b;
        }
    }
}
