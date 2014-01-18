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

namespace VKPlayer
{
    public partial class Form1 : Form
    {
        public enum MusicState
        {
            Played, Paused, Stoped
        }

        private int nextAudio = 0;
        private List<string> audios;

        private bool isOpen;

        private System.Windows.Forms.WebBrowser webBrowser;

        private MusicState musicState;

        public Form1()
        {
            audios = new List<string>();
            webBrowser = new System.Windows.Forms.WebBrowser();

         

            webBrowser.Navigated += webBrowser_Navigated;

            isOpen = false;

            musicState = MusicState.Stoped;

            InitializeComponent();
        }

        void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            audios.Clear();
            txtAudio.Text = "";

            Regex reg = new Regex(@"cs1-[0-9|a-z]+\.vk\.me/[0-9|a-z]+/[0-9|a-z]+\.mp3", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(webBrowser.DocumentText);

            int i = 0;
            foreach (Match mat in mc)
            {
                i++;
                txtAudio.Text += i.ToString() + ". http:\\\\" + mat.ToString() + Environment.NewLine;
                audios.Add("http:\\\\" + mat.ToString());
            }

            /*
            Regex titles = new Regex(@"(this\,\sevent\)\;" + "\"" + @"\>[0-9|\s|a-z|&|-|\.|\)|\(|:|;|#]+)|" + txtUrl.Text, RegexOptions.IgnoreCase);

            //webBrowser.DocumentText = Encoding.GetEncoding(1251).GetString(Encoding.GetEncoding(1251).GetBytes(webBrowser.DocumentText));

            MatchCollection match = titles.Matches(webBrowser.DocumentText);
            string song;
            i = 0;
            foreach (Match mat in match)
            {
                if (mat.ToString().ToLower() != txtUrl.Text)
                    song = mat.ToString().Remove(0, 15);
                else
                    song = mat.ToString();
                txtAudio.Lines[i] += song + Environment.NewLine;
                i++;
            }
             * */
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {         
            webBrowser.Navigate("http://vk.com/search?c%5Bq%5D=" + txtUrl.Text + "&c%5Bsection%5D=audio");
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                MP3Player.OpenPlayer(audios[nextAudio]);
                MP3Player.Play(this.Handle);
                isOpen = true;
                musicState = MusicState.Played;
            }
            else
                if (musicState == MusicState.Paused)
                {
                    musicState = MusicState.Played;
                    MP3Player.Resume();
                }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MP3Player.MM_MCINOTIFY)
            {
                if (musicState == MusicState.Played)
                    btnNext_Click(null, null);
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
            if (isOpen == true)
            {
                musicState = MusicState.Played;
                MP3Player.ClosePlayer();
                nextAudio++;
                MP3Player.OpenPlayer(audios[nextAudio]);
                MP3Player.Play(this.Handle);
                isOpen = true;            
            }
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser.Navigate("http://vk.com/search?c%5Bq%5D=" + txtUrl.Text + "&c%5Bsection%5D=audio");
            }
        }

        
    }
}
