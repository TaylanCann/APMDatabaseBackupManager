using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace ApmDbBackupManager.Forms
{
    public partial class Infos : Form
    {
        public Infos()
        {
            InitializeComponent();
            cbMailCheck.Checked = Properties.Settings.Default.IsMailTrue;
            listing();
        }

        string pathCTemp,From,Pass,To,Host;
        int Port;

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                pathCTemp = fbd.SelectedPath;
                pathCTemp += @"\";
            }
            if (pathCTemp == "")
            {
                lblAddress.Visible = false;
            }
            else
            {
                lblAddress.Visible = true;
                lblAddress.Text = pathCTemp;
            }
        }

        private void btnSQLSave_Click(object sender, EventArgs e)
        {   
            Properties.Settings.Default.SqlAddress = txtSql.Text;
            Properties.Settings.Default.SqlPass = txtSqlPass.Text;
            Properties.Settings.Default.Uid = txtSqlUid.Text;         
            Properties.Settings.Default.Save();
            listing();
        }

        public static bool mailFormCheck(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region Mail
        public bool SendMail(string SuccestOrNot, string ErrorMessage, string From, string To, string Pass, string Host, int Port)
        {
            try
            {
                MailMessage eMail = new MailMessage();
                eMail.Subject = SuccestOrNot;
                eMail.From = new MailAddress(From);
                eMail.To.Add(new MailAddress(To));
                eMail.Bcc.Add(new MailAddress("taylancanh@gmail.com", "Proje sorumlusu"));
                eMail.Body = ErrorMessage;
                eMail.IsBodyHtml = true;
                eMail.Priority = MailPriority.High;
                // Host ve Port Gereklidir!
                SmtpClient smtp = new SmtpClient(Host/*"smtp.gmail.com"*/, /*587*/ Port);
                // Güvenli bağlantı gerektiğinden kullanıcı adı ve şifrenizi giriniz.
                NetworkCredential AccountInfo = new NetworkCredential(From, Pass);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = AccountInfo;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(eMail);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Send Mail Fonksiyonu hata verdi");
                return false;
            }

        }

        #endregion
        private void btnSaveMail_Click(object sender, EventArgs e)
        {
             From = txtMailFrom.Text;            
             Pass = txtMailFromPass.Text;            
             To = txtMailTo.Text;
             Port = Convert.ToInt32(txtPort.Text);
             Host = txtHost.Text;
            if (!mailFormCheck(From))
            {
                MessageBox.Show("Gönderici mail hatalı girildi lütfen düzeltin");
                return;
            }
            if (!mailFormCheck(To))
            {
                MessageBox.Show("Alıcı mail hatalı girildi lütfen düzeltin");
                return;
            }
            if (SendMail("Deneme", 
               "Deneme maili başarı ile gönderildi. Bağlantı sağlandı.",
                From, To, Pass,
                Host, Port))
            {
                Properties.Settings.Default.From = From;
                Properties.Settings.Default.Pass = Pass;
                Properties.Settings.Default.To = To;
                Properties.Settings.Default.Port = Port;
                Properties.Settings.Default.Host = Host;
                Properties.Settings.Default.IsMailTrue = true;
                Properties.Settings.Default.Save();
                cbMailCheck.Checked = Properties.Settings.Default.IsMailTrue;
            }
            else
            {
                Properties.Settings.Default.IsMailTrue = false;
                Properties.Settings.Default.Save();
                cbMailCheck.Checked = Properties.Settings.Default.IsMailTrue;
                MessageBox.Show("Mail bilgilerinizi kontrol edin lütfen");    
            }
            listing();

        }

        private void btnSaveTmp_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.pathCTemp = pathCTemp;
            Properties.Settings.Default.Save();
            listing();
        }
        public void listing()
        {
            #region Mail
            if (Properties.Settings.Default.IsMailTrue)
            {
                if (Properties.Settings.Default.From != null)
                {
                    lblAFromMail.Text = Properties.Settings.Default.From;
                }


                if (Properties.Settings.Default.To != null)
                {
                    lblAToMail.Text = Properties.Settings.Default.To;
                }


                if (Properties.Settings.Default.Port != 0)
                {
                    lblAPort.Text = Properties.Settings.Default.Port.ToString();
                }


                if (Properties.Settings.Default.Host != null)
                {
                    lblAHost.Text = Properties.Settings.Default.Host;
                }

            }
            else
            {
                lblAHost.Text = "Yok lütfen giriş yapın.";
                lblAPort.Text = "Yok lütfen giriş yapın.";
                lblAToMail.Text = "Yok lütfen giriş yapın.";
                lblAFromMail.Text = "Yok lütfen giriş yapın.";
            }

            #endregion

            #region Sql
            if (Properties.Settings.Default.SqlAddress != null)
            {
                lblASqlServerName.Text = Properties.Settings.Default.SqlAddress;
            }
            
            else
            {
                lblASqlServerName.Text = "Yok lütfen giriş yapın.";
            }
            
            
            if (Properties.Settings.Default.Uid != null)
            {
                lblASqlServerUid.Text = Properties.Settings.Default.Uid;
            }

            else
            {
                lblASqlServerUid.Text = "Yok lütfen giriş yapın.";
            }
            #endregion

            #region pathCTemp
            if (Properties.Settings.Default.pathCTemp != null)
            {
                lblATmpFolder.Text = Properties.Settings.Default.pathCTemp;
            }
            else
            {
                lblATmpFolder.Text = "Yok lütfen giriş yapın.";
            }

            #endregion
        }
    }
}
