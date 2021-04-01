using ApmDbBackupManager.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace ApmDbBackupManager
{
    public partial class DriveForm : Form
    {
        public DriveForm()
        {
            InitializeComponent();

            Listing();
        }

        public bool CheckClient()
        {
            if (Directory.Exists("Client_secret.json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        string DriveUserName;
        DatabaseContext context = new DatabaseContext();
        static string ApplicationName = "SqlBackup"; //Drive ile alakalı
        static DriveService service;

        public void TxtLog(string writeText)
        {
            try
            {
                string fileName = @"Log.txt";

                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();
                File.AppendAllText(fileName, Environment.NewLine + DateTime.Now.ToString() + "=>" + writeText);
            }
            catch (Exception)
            {
                MessageBox.Show("Txt oluştururken hata yapıldı.");
            }
        }

        public void Listing()
        {
            var db = context.DriveUsers.Where(f => f.IsActive == true).ToList();
            dataGridView1.DataSource = db.Select(e => new
            {
                UserName = e.User  
            }).ToList();
        }

        private void btnGoogleUser_Click(object sender, EventArgs e)
        {
            try
            {
                DriveUserName = txtGoogleUser.Text;
                DriveUser driveUser = new DriveUser();
                driveUser.User = DriveUserName;
                driveUser.IsActive = true;
                if (DriveLogin(DriveUserName))
                {
                    context.DriveUsers.Add(driveUser);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Drive'a bağlantı sağlanamadı.");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show("Olmadı be böyle olmadı");
                TxtLog("Hata : " + es.Message + " Google Drive click." + " Google Drive");
            }
            Listing();
        }

        public bool DriveLogin(string username)
        {
            try
            {
                UserCredential credential;//google drive kimlik degişkeni
                using (var stream =
                    new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    //json okunuyor 
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    // kullanıcılar belgelerin yolu

                    // string credPath = System.IO.Directory.GetParent(@".\\").FullName;

                    credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");
                    try
                    {
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { DriveService.Scope.Drive },
                        username,
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        TxtLog("Hata : " + e.Message + " Google Drive." + " Google Drive");
                        return false;
                    }

                    //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    //GoogleClientSecrets.Load(stream).Secrets,
                    //Scopes,
                    //"user",
                    //CancellationToken.None,
                    //new FileDataStore(credPath, true)).Result;

                    //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    //GoogleClientSecrets.Load(stream).Secrets,
                    //new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile },
                    //"LookIAmAUniqueUserr",

                    //CancellationToken.None,
                    //new FileDataStore("Drive.Auth.Store")
                    //).Result;

                }

                service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
                service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
                
                return true;


            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                TxtLog("Hata : " + es.Message + " Google Drive" + " Google Drive");

                return false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show(dr.Cells[0].Value.ToString() + " kaydını silmek istediğinize emin misiniz?", "SİL", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    foreach (var item in context.DriveUsers.ToList())
                    {
                        if (item.User == dr.Cells[0].Value.ToString() && item.IsActive == true)
                        {
                            item.IsActive = false;
                            context.Update(item);
                            context.SaveChanges();
                            break;
                        }
                    }
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silindi");
                }
                else
                {
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silinmedi");
                }
                Listing();
            }
        }
    }
}
