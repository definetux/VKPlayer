using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKPlayer
{
    public partial class DownloadManager : Form
    {
        private Thread thrDownload;
        // The stream of data retrieved from the web server
        private Stream strResponse;
        // The stream of data that we write to the harddrive
        private Stream strLocal;
        // The request to the web server for file information
        private HttpWebRequest webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse webResponse;

        private static int PercentProgress;

        private string path;

        private string url;
        // The delegate which we will call from the thread to update the form
        private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);

        public DownloadManager()
        {
            InitializeComponent();
        }

        public void Download(string url, string path)
        {
            // Let the user know we are connecting to the server
            this.url = url;
            this.path = path;
            path = path.Substring(path.LastIndexOf('\\') + 1);

            txtFile.Text = path;
            // Create a new thread that calls the Download() method
            thrDownload = new Thread(Download);
            // Start the thread, and thus call Download()
            thrDownload.Start();
        }

        private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
        {
         // Calculate the download progress in percentages
            PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
         // Make progress on the progress bar
            pbLoading.Value = PercentProgress;
         // Display the current progress on the form
            lblProgress.Text = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
            if (BytesRead == TotalBytes)
            {
                lblProgress.Text = "Download successfull";
                btnOk.Enabled = true;
            }
         }

        private void Download()
        {
            using (WebClient wcDownload = new WebClient())
            {
                try
                {
                    // Create a request to the file we are downloading
                    webRequest = (HttpWebRequest)WebRequest.Create(url);
                    // Set default authentication for retrieving the file
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    // Ask the server for the file size and store it
                    Int64 fileSize = webResponse.ContentLength;

                    // Open the URL for download 
                    strResponse = wcDownload.OpenRead(url);
                    // Create a new file stream where we will be saving the data (local drive)
                    strLocal = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize = 0;
                    // A buffer for storing and writing the data retrieved from the server
                    byte[] downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        // Write the data from the buffer to the local hard drive
                        strLocal.Write(downBuffer, 0, bytesSize);
                        // Invoke the method that updates the form's label and progress bar
                        this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { strLocal.Length, fileSize });
                    }
                }
                catch
                {
                    MessageBox.Show("Download failed");
                }
                finally
                {
                    // When the above code has ended, close the streams
                    strResponse.Close();
                    strLocal.Close();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
