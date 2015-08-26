using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
namespace Mailer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

              SmtpOptions __Smtp_options = new SmtpOptions();
            try
            {   
               
                __Smtp_options.Email = txbx_email.Text;
                __Smtp_options.Password = txbx_pass.Text;
                __Smtp_options.DetectServer(); 
                __Smtp_options.ServerLize(txbxServer.Text); // set  server and port


                SmtpClient cli = new SmtpClient(__Smtp_options.Server,__Smtp_options.Port);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(txbx_from.Text );
                msg.To.Add(txbx_To.Text);
                msg.Body = txbx_Body.Text;
                msg.Subject = txbx_Sub.Text;

                cli.UseDefaultCredentials = false;
                cli.EnableSsl = true;
                if (txbx_Att.Text != "")
                {
                    if (!txbx_Att.Text.Contains(","))
                    {
                        if (System.IO.File.Exists(txbx_Att.Text))
                            msg.Attachments.Add(new Attachment(txbx_Att.Text));
                    }
                    else
                    {
                        string[] fpaths = txbx_Att.Text.Split(new char[] { ',' });
                        foreach(string fp in fpaths)
                        {
                            if(System.IO.File.Exists(fp))
                                msg.Attachments.Add(new Attachment(fp));
                        }
                    }
                }

                cli.Credentials = __Smtp_options.Credentials();

                cli.Send(msg);

                msg = null;
                MessageBox.Show("sent");
           
            }
            catch(Exception ex)
            {
                string msg = ex.Message + Environment.NewLine + "Check ur crediential" + Environment.NewLine  ;
                msg += (Environment.NewLine+ Environment.NewLine+"Check SMTP security settings");

                if (__Smtp_options.Email.Contains("@gmail"))
                    msg += ("Check google security at https://www.google.com/settings/security/lesssecureapps");
                MessageBox.Show(msg, "Server Error");
            }
           
        }

        private string Smtp(string p)
        {
            if (p.Contains("gmail"))
                return "smtp.gmail.com";
            else

                if (p.Contains("yahoo"))
                    return "smtp.mail.yahoo.com";

            return this.SelectedSmtp;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            if (o.ShowDialog() == DialogResult.OK)
                txbx_Att.Text = Array_(o.FileNames);
        }

        private string Array_(string[] p)
        {
            string x = "";
            foreach (string s in p)
                x += (s + ",");

            return x.Substring(0,x.Length-1);
        }

        private void lnklblPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txbx_pass.PasswordChar == '*')
            {
                txbx_pass.PasswordChar = (char)0;
                lnklblPassword.Text = "Hide";
            }
            else
            {
                txbx_pass.PasswordChar = '*';
                lnklblPassword.Text = "Show";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSMTPServers();
        }

        private void LoadSMTPServers()
        {
            string x = global::Mailer.Properties.Settings.Default.SmtpServers;
            if(x.Contains(","))
            {
                string[] sers = x.Split(new char[] { ',' });
                foreach (string server in sers)
                    if (listBox1.Items.Contains(server))
                        continue;
                    else
                        listBox1.Items.Add(server);
            }
            else
            {
                listBox1.Items.Add(x);
            }
        }

        private void AddNewSmpt_Click(object sender, EventArgs e)
        {
            if (!listBox1.Items.Contains(textBox_newSmtp.Text) && textBox_newSmtp.Text.Contains(":"))
                listBox1.Items.Add(textBox_newSmtp.Text);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
                listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            string x = "";

            if (listBox1.Items.Count > 0)
            {
                foreach (string i in listBox1.Items)
                    x += (i + ",");

                x = x.Substring(0, x.Length - 1);

            }

            global::Mailer.Properties.Settings.Default.SmtpServers = x;
            global::Mailer.Properties.Settings.Default.Save();
        }

        public string SelectedSmtp { get; set; }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectSmtp s = new SelectSmtp();
            if (s.ShowDialog() == DialogResult.OK)
                this.txbxServer.Text = s.Name;
        }

        private void txbx_email_TextChanged(object sender, EventArgs e)
        {
            DetectServer();
        }

        private void DetectServer()
        {
                string x = SmtpOptions.GuessServer(txbx_email.Text);
                if (x != "")
                    txbxServer.Text = x;

        }

        private void txbx_pass_TextChanged(object sender, EventArgs e)
        {
            lnklblPassword.Visible = txbx_pass.Text != "";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("By yasser Gersy");
            System.Diagnostics.Process.Start("https://facebook.com/prince.gersy2");
        }
    }
}
