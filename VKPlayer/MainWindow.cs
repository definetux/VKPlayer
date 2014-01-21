using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Threading;

namespace VKPlayer
{
    public partial class MainWindow : Form
    {
        public enum MusicState
        {
            Played, Paused, Stoped
        }

        private int nextAudio = 0;

        private List<string> audios;

        private bool isOpen;

        private int isAuthorization;

        private int isLogin;

        private System.Windows.Forms.WebBrowser webBrowser;

        private MusicState musicState;

        private Point downPoint;

        private bool isDragMode = false;

        private string userId;

        public MainWindow()
        {
            audios = new List<string>();
            isAuthorization = 0;
            isLogin = 0;

            this.TopMost = false;

            webBrowser = new System.Windows.Forms.WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;

            webBrowser.Navigated += webBrowser_Navigated;

            //this.Controls.Add(webBrowser);

            webBrowser.Navigate("https://login.vk.com/?act=login&email=&pass=&expire=&vk=");

            isOpen = false;

            musicState = MusicState.Stoped;

            InitializeComponent();  
        }

        public void Authorization()
        {
            Authorization auth = new Authorization();

            DialogResult res = auth.ShowDialog();

            if (res == DialogResult.Cancel)
            {
                Environment.Exit(-1);
            }

            webBrowser.Navigate("https://login.vk.com/?act=login&email=" + auth.Login + "&pass=" + auth.Password + "&expire=&vk=");
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowser.Url.AbsoluteUri.Contains("login.php"))
            {

                if (isLogin == 1)
                {
                    this.TopMost = false;
                    Authorization();
                }
                else if (isLogin > 1)
                {
                    MessageBox.Show("Error!", "Invalid login or password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isLogin = 0;
                }
                isLogin++;
            }
            else
            {
                if (userId == null)
                {
                    Regex reg = new Regex( "\"" + @"user_id" + "\"" + @"\:[0-9]+",                                
                                            RegexOptions.IgnoreCase);
                    MatchCollection mc = reg.Matches(webBrowser.DocumentText);
                    string uri = "";
                    foreach (Match mat in mc)
                     uri = mat.ToString();
                    userId = uri.Remove(0, uri.LastIndexOf(':') + 1);
                }

                if (isAuthorization == 2)
                {
                    audios.Clear();
                    nextAudio = 0;
                    lstPlayList.Items.Clear();

                    Encoding enc = Encoding.GetEncoding("windows-1251");
                    string HTMLText;

                    Stream stream = webBrowser.DocumentStream;
                    StreamReader sr = new StreamReader(stream, enc);
                    HTMLText = sr.ReadToEnd();
                    stream.Close();

                    Regex reg = new Regex(@"(cs.+\.mp3)|" +
                                            @"(\<div\sclass=" + "\"" + @"title_wrap\sfl_l" + "\"" + ".+" + @"\<\/div\>)",
                                            RegexOptions.IgnoreCase);
                    MatchCollection mc = reg.Matches(HTMLText);

                    int i = 0;
                    foreach (Match mat in mc)
                    {
                        if (mat.ToString().Contains("vk.me"))
                            audios.Add("http:\\\\" + mat.ToString());
                        else
                        {
                            i++;
                            string[] text = mat.ToString().Replace("&", "").Replace("#", "").Replace("$", "").Split(new Char[] { '<', '>' });
                            string track = "";
                            foreach (string currentPart in text)
                            {
                                if (!(currentPart.Contains("div") 
                                    || currentPart.Contains("span") 
                                    || currentPart.Contains("onclick") 
                                    || currentPart.Contains("ndash")))
                                    track += currentPart + ' ';
                            }
                            lstPlayList.Items.Add(i.ToString() + ". " + track.Replace("/a", "").Replace("/b", " - ").Replace("  ", " ").Replace(" b ", " "));
                        }

                    }
                }

                else
                    isAuthorization++;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://vk.com/search?c%5Bq%5D=" + txtUrl.Text + "&c%5Bsection%5D=audio");       
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isOpen == false && audios.Count > 0)
            {               
                if (MP3Player.OpenPlayer(audios[nextAudio]) == false)
                    return;
                if (MP3Player.Play(this.Handle) == false)
                    return;
                isOpen = true;
                musicState = MusicState.Played;
                string notify = lstPlayList.Items[nextAudio].ToString();
                notifyIcon1.Text = notify.Substring(0, (notify.Length > 63) ? 63 : notify.Length);
            }
            else
                if (musicState == MusicState.Paused)
                {

                    if (MP3Player.Resume() == false)
                        return;
                    musicState = MusicState.Played;
                }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MP3Player.MM_MCINOTIFY)
            {
                if (musicState == MusicState.Played)
                    PlayNext();
            }

            base.WndProc(ref m);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            musicState = MusicState.Stoped;
            MP3Player.ClosePlayer();
            isOpen = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (musicState == MusicState.Played)
            {
                musicState = MusicState.Paused;
                MP3Player.PausePlayer();              
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                if (musicState == MusicState.Stoped)
                    nextAudio++;
                PlayNext(nextAudio);
            }
            else
                PlayNext();
        }

        private void PlayNext(int index = -1)
        {
            musicState = MusicState.Played;
            if (isOpen == true)
                if (MP3Player.ClosePlayer() == false)
                    return;
            if (index != -1)
                nextAudio = index;
            else
            {
                nextAudio = (nextAudio >= audios.Count - 1) ? 0 : ++nextAudio;
            }
            if (MP3Player.OpenPlayer(audios[nextAudio]) == false)
            {
                nextAudio--;
                return;
            }
            if (MP3Player.Play(this.Handle) == false)
                return;

            lstPlayList.SelectedIndex = nextAudio;
            string notify = lstPlayList.Items[nextAudio].ToString();
            notifyIcon1.Text = notify.Substring(0, (notify.Length > 63) ? 63 : notify.Length);

            isOpen = true;            
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser.Navigate("http://vk.com/search?c%5Bq%5D=" + txtUrl.Text + "&c%5Bsection%5D=audio");
            }
        }

        private void lstPlayList_DoubleClick(object sender, EventArgs e)
        {
            PlayNext(lstPlayList.SelectedIndex);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            int index = lstPlayList.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Choose a track");
                return;
            }

            //string path = audios[index].Remove(0, 7).Replace("/", "");
            string path = lstPlayList.Items[index].ToString().Remove(0, 3).Trim(new Char[] { '\r', '\n' });
            SaveFileDialog sf = new SaveFileDialog();
            sf.RestoreDirectory = true;
            sf.Filter = "Music files (*.mp3)|*.mp3";
            sf.FileName = path;
            if (sf.ShowDialog() == DialogResult.Cancel)
                return;
    
            DownloadManager dm = new DownloadManager();
            dm.Show();          
            dm.Download(audios[index], sf.FileName);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;                      
            } 
        }

        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = SystemColors.ControlLight;
        }

        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = SystemColors.ControlText;
        }

        private void lblExit_MouseDown(object sender, MouseEventArgs e)
        {
            lblExit.ForeColor = SystemColors.HotTrack;
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            MP3Player.ClosePlayer();
            base.Close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblMinimize_MouseEnter(object sender, EventArgs e)
        {
            lblMinimize.ForeColor = SystemColors.ControlLight;
        }

        private void lblMinimize_MouseDown(object sender, MouseEventArgs e)
        {
            lblMinimize.ForeColor = SystemColors.HotTrack;
        }

        private void lblMinimize_MouseLeave(object sender, EventArgs e)
        {
            lblMinimize.ForeColor = SystemColors.ControlText;
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = e.Location;
            isDragMode = true;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragMode)
            {
                Point p = e.Location;
                Point dp = new Point(p.X - downPoint.X, p.Y - downPoint.Y);
                Location = new Point(Location.X + dp.X, Location.Y + dp.Y);
            }
        }

        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            isDragMode = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://vk.com/audios" + userId);
        }
    }
}
