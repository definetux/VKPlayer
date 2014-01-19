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

        private System.Windows.Forms.WebBrowser webBrowser;

        private MusicState musicState;

        public MainWindow()
        {
            audios = new List<string>();
            isAuthorization = 0;
            webBrowser = new System.Windows.Forms.WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;

            Authorization auth = new Authorization();

            DialogResult res = auth.ShowDialog();

            if (res == DialogResult.Cancel)
            {
                Environment.Exit(-1);
            }

            webBrowser.Url = new Uri("https://login.vk.com/?act=login&email=" + auth.Login + "&pass=" + auth.Password + "&expire=&vk=");

            webBrowser.Navigated += webBrowser_Navigated;

            isOpen = false;

            musicState = MusicState.Stoped;

            InitializeComponent();
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
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

                Regex reg = new Regex(@"(cs[0-9]+-[0-9|a-z]+\.vk\.me/[0-9|a-z]+/[0-9|a-z]+\.mp3)|" + 
                                         @"(\<span\sclass=" + "\"" + "title" + "\"" + ".+" + @"\<span\sclass=" + "\"" + "user" + "\")",
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
                        string[] text = mat.ToString().Split(new Char[] {'<','>'});
                        lstPlayList.Items.Add(i.ToString() + ". " + text[4] + Environment.NewLine);
                    }
                    
                }
            }
            else
                isAuthorization++;
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
                PlayNext(0);
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
    }
}
