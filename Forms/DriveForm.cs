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

        string DriveUserName;
        DatabaseContext context = new DatabaseContext();
        static string ApplicationName = "SqlBackup"; //Drive ile alakalı
        static DriveService service;

        public void Listing()
        {
            var list = context.DriveUsers.ToList();
            dataGridView1.DataSource = list;
        }

        private void btnGoogleUser_Click(object sender, EventArgs e)
        {
            try
            {
                DriveUserName = txtGoogleUser.Text;
                DriveUser driveUser = new DriveUser();
                driveUser.User = DriveUserName;
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
            catch (Exception)
            {
                MessageBox.Show("Olmadı be böyle olmadı");
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
            catch (Exception ess)
            {
                MessageBox.Show(ess.Message);

                return false;
            }
        }

    }
}
