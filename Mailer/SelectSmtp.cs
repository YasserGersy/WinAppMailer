using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mailer
{
    public partial class SelectSmtp : Form
    {
        public SelectSmtp()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = (listBoxSmtps.SelectedIndex != -1);
        }

        private void SelectSmtp_Load(object sender, EventArgs e)
        {

            LoadSMTPServers();
        }

        private void LoadSMTPServers()
        {
            string x = global::Mailer.Properties.Settings.Default.SmtpServers;
            if (x.Contains(","))
            {
                string[] sers = x.Split(new char[] { ',' });
                foreach (string server in sers)
                    if (listBoxSmtps.Items.Contains(server))
                        continue;
                    else
                        listBoxSmtps.Items.Add(server);
            }
            else
            {
                listBoxSmtps.Items.Add(x);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Name = listBoxSmtps.SelectedItem.ToString();
        }
    }
}
