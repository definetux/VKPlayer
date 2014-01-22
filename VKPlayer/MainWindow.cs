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

        private const int SIZE_OF_BYTE = 8;

        private const string pathPlaylist = "playlist.pl";

        private int nextAudio = 0;

        private List<string> audios;

        private bool isOpen;

        private int isAuthorization;

        private int isLogin;

        private System.Windows.Forms.WebBrowser webBrowser;

        private MusicState musicState;

        private Point downPoint;

        private bool isDragMode = false;

        private string userId = "none";

        private int lastVolume;

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

            tbVolume.Value = MP3Player.GetVolume();
        }

        public void Authorization()
        {
            Authorization auth = new Authorization();

            DialogResult res = auth.ShowDialog();

            if (res == DialogResult.Cancel)
            {
                Environment.Exit(-1);
            }
            else if (res == DialogResult.OK)
            {
                webBrowser.Navigate("https://login.vk.com/?act=login&email=" + auth.Login + "&pass=" + auth.Password + "&expire=&vk=");
            }
            else
            {
                lblId.Text = "ID: " + userId;
                btnAudios.Enabled = false;
                txtUrl.Enabled = false;
            }
            
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowser.Url.AbsoluteUri.Contains("login"))
            {

                if (isLogin == 1)
                {
                    this.TopMost = false;
                    Authorization();
                }
                else if (isLogin > 1)
                {
                    MessageBox.Show("Invalid login or password!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isLogin = 0;
                }
                isLogin++;
            }
            else
            {
                Encoding enc = Encoding.GetEncoding("windows-1251");
                string HTMLText;

                Stream stream = webBrowser.DocumentStream;
                StreamReader sr = new StreamReader(stream, enc);
                HTMLText = sr.ReadToEnd();
                stream.Close();

                if (userId == "none")
                {

                    Regex reg = new Regex( "id=" + "\"" + @"head_music" + "\""
                                            + @"\shref=" + "\"" + @"\/audios[0-9]+" + @"\?",                                 
                                            RegexOptions.IgnoreCase);

                    MatchCollection mc = reg.Matches(HTMLText);

                    string uri = "";
                    foreach (Match mat in mc)
                    {
                        uri = mat.ToString();
                    }
                    if (uri != "")
                        userId = uri.Split('"')[3].Remove(0, 7).TrimEnd('?');
                    lblId.Text = "ID: " + userId;
                    btnAudios.Enabled = true;
                    txtUrl.Enabled = true;
                }
               
                if (isAuthorization == 2)
                {
                    tsDelete.Visible = false;
                    tsClear.Visible = false;
                    tsAdd.Visible = true;  

                    audios.Clear();
                    nextAudio = 0;
                    lstPlayList.Items.Clear();

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
                            lstPlayList.Items.Add(i.ToString() + ". " + track.Replace(" /a ", " ").Replace(" /b ", " - ").Replace("  ", " ").Replace(" b ", " ").TrimEnd(' '));
                        }

                    }
                    if (lstPlayList.Items.Count != audios.Count)
                        audios.RemoveAt(audios.Count - 1);
                    if (lstPlayList.Items.Count != 0)
                        lstPlayList.SelectedIndex = 0;
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
                string notify = lstPlayList.Items[nextAudio].ToString(); // CHANGE LSTPLAYLIST
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
            if (audios.Count == 0)
                return;

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

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;                    
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
            if (userId != "") 
                webBrowser.Navigate("http://vk.com/audios" + userId);
        }

        private void btnMix_Click(object sender, EventArgs e)
        {
            if (audios.Count == 0)
                return;

            uint []bitmap = new uint[audios.Count / (sizeof(uint) * SIZE_OF_BYTE) + 1];
            for (int i = 0; i < bitmap.Length; i++)
                bitmap[i] = 0;

            int[] sequence = new int[audios.Count];
            for (int i = 0; i < sequence.Length; i++)
                sequence[i] = -1;

            Random rand = new Random();

            bool isSequenceFull = false;

            int sequenceIndex = 0;

            while (isSequenceFull == false)
            {
                int newElement = rand.Next(0, audios.Count);
                if ((bitmap[(newElement / (sizeof(uint) * SIZE_OF_BYTE))] >> (newElement % (sizeof(uint) * SIZE_OF_BYTE)) & 1) == 0)
                {
                    sequence[sequenceIndex] = newElement;
                    sequenceIndex++;
                    bitmap[(newElement / (sizeof(uint) * SIZE_OF_BYTE))] |= (uint)(1 << (newElement % (sizeof(uint) * SIZE_OF_BYTE)));
                }
                isSequenceFull = true;
                for (int i = 0; i < audios.Count; i++)
                {
                    if (((bitmap[(i / (sizeof(uint) * SIZE_OF_BYTE))] >> (i % (sizeof(uint) * SIZE_OF_BYTE))) & 1) == 0)
                    {	
                        isSequenceFull = false;
                        break;
                    }
                }
            }

            List<string> newPlayList = new List<string>();
            ListBox newTrackList = new ListBox();
            for (int i = 0; i < sequence.Length; i++)
            {
                newPlayList.Add(audios[sequence[i]]);
                newTrackList.Items.Add(lstPlayList.Items[sequence[i]]);
            }

            audios = newPlayList;
            lstPlayList.Items.Clear();

            int id = 0;
            foreach (string track in newTrackList.Items)
            {
                id++;
                lstPlayList.Items.Add(id.ToString() + track.Remove(0, track.IndexOf('.')));
            }
            lstPlayList.SelectedIndex = 0;
        }

        private void lstPlayList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayList.SelectedItem != null)
                toolTip1.SetToolTip(lstPlayList, lstPlayList.SelectedItem.ToString());
            else
                toolTip1.SetToolTip(lstPlayList, "");
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            MP3Player.SetVolume(tbVolume.Value);
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (tbVolume.Value == 0)
                tbVolume.Value = lastVolume;
            else
            {
                lastVolume = tbVolume.Value;
                tbVolume.Value = 0;
            }
            MP3Player.SetVolume(tbVolume.Value);
        }

        private void tsDonwload_Click(object sender, EventArgs e)
        {
            int index = lstPlayList.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Choose a track");
                return;
            }

            string path = lstPlayList.Items[index].ToString().Remove(0, 3).Trim(new Char[] { '\r', '\n' }).TrimStart(' ');
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

        private void lstPlayList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ((ListBox)sender).SelectedIndex = ((ListBox)sender).IndexFromPoint(e.Location);
            }
        }

        private void tsAdd_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText(pathPlaylist);

            sw.WriteLine(audios[lstPlayList.SelectedIndex] + '|' + lstPlayList.SelectedItem.ToString().Remove(0, 3).TrimStart(' '));
            sw.Close();
        }

        private void btnMyPlayList_Click(object sender, EventArgs e)
        {
            tsDelete.Visible = true;
            tsClear.Visible = true;
            tsAdd.Visible = false;

            audios.Clear();
            lstPlayList.Items.Clear();
            nextAudio = 0;

            try
            {
                StreamReader sr = File.OpenText(pathPlaylist);
                int lstIndex = 0;

                string song = "";

                while ((song = sr.ReadLine()) != null)
                {

                    string[] parts = song.Split('|');

                    lstIndex++;
                    audios.Add(parts[0]);
                    lstPlayList.Items.Add(lstIndex.ToString() + ". " + parts[1]);
                }

                sr.Close();

                if (lstPlayList.Items.Count != 0)
                    lstPlayList.SelectedIndex = 0;
            }
            catch (IOException)
            {

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void lstPlayList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PlayNext(lstPlayList.SelectedIndex);
            }
        }

        private void tsClear_Click(object sender, EventArgs e)
        {
            audios.Clear();
            lstPlayList.Items.Clear();

            File.Delete(pathPlaylist);
            File.Create(pathPlaylist);
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(pathPlaylist);
            
            int index = lstPlayList.SelectedIndex;

            try
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(pathPlaylist, FileMode.Create, FileAccess.Write, FileShare.None)))
                {
                    foreach (string line in lines)
                    {
                        if (!line.Contains(audios[index]))
                        {
                            sw.Write(line + "\r\n");
                        }
                    }
                    sw.Close();
                }

                audios.RemoveAt(index);
                lstPlayList.Items.RemoveAt(index);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isAuthorization != 0)
            {
                Regex reg = new Regex(@"id=" + "\"" + @"logout_link" + "\"" + @"\shref="
                                        + "\"" + ".+" + @"vk\.com",
                                                RegexOptions.IgnoreCase);

                MatchCollection mc = reg.Matches(webBrowser.DocumentText);

                string uri = "";
                foreach (Match mat in mc)
                {
                    uri = mat.ToString().Split('"')[3];
                }

                isLogin = 1;
                isAuthorization = 0;
                audios.Clear();
                lstPlayList.Items.Clear();

                lblId.Text = "ID: ";

                userId = "none";
                webBrowser.Navigate(uri);
            }
            else
            {
                isLogin = 0;
                webBrowser.Navigate("https://login.vk.com/?act=login&email=&pass=&expire=&vk=");
            }
        }
    }
}
