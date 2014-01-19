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
            webBrowser.Url = new Uri("http://login.vk.com/?act=login&email=+380950331759&pass=rammstein1989&expire=&vk=");

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
                txtAudio.Text = "";

                Encoding enc = Encoding.GetEncoding("windows-1251");
                string HTMLText;

                Stream stream = webBrowser.DocumentStream;
                StreamReader sr = new StreamReader(stream, enc);
                HTMLText = sr.ReadToEnd();
                stream.Close();

                Regex reg = new Regex(@"(cs1-[0-9|a-z]+\.vk\.me/[0-9|a-z]+/[0-9|a-z]+\.mp3)|" + 
                                         @"(\<span\sclass=" + "\"" + "title" + "\"" + ".+" + @"\<span\sclass=" + "\"" + "user" + "\")",
                                          RegexOptions.IgnoreCase);

                //Regex reg = new Regex(@"\<span\sclass=" + "\"" + "title" + "\"" + ".+" + @"\<span\sclass=" + "\"" + "user" + "\"", RegexOptions.IgnoreCase);

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
                        txtAudio.Text += i.ToString() + ". " + text[4] + Environment.NewLine;
                    }
                    
                }

                
                /*
                ///* TODO: Add russian language later  return cancelEvent(event);">
                Regex titles = new Regex(@"(this\,\sevent\)\;" + "\"" + @"\>[0-9|\s|a-z|а-я|&|-|\.|\)|\(|:|;|#]+)|" + txtUrl.Text, RegexOptions.IgnoreCase);

                //Regex titles = new Regex(@"return\scancelEvent\(event\)\;" + "\"" + @"\>[0-9|\s|a-z|а-я|&|-|\.|\)|\(|:|;|#|\\]+", RegexOptions.IgnoreCase);

                //Regex titles = new Regex(@"\<tr\>.+\<div\>", RegexOptions.IgnoreCase);

                MatchCollection match = titles.Matches(HTMLText);
                string song;
                foreach (Match mat in match)
                {
                    if (mat.ToString().ToLower() != txtUrl.Text)
                        song = mat.ToString();//.Remove(0, 28);
                    else
                        song = mat.ToString();
                    txtAudio.Text += song + Environment.NewLine;
                }*/
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
            if (isOpen == false)
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
            if (isOpen == true)
            {
                
                PlayNext();
            }
        }

        private void PlayNext()
        {
            musicState = MusicState.Played;
            if (MP3Player.ClosePlayer() == false)
                return;
            nextAudio++;
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
    }
}
