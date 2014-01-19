using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VKPlayer
{
    class MP3Player
    {
        public const int MM_MCINOTIFY = 953;

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hWndCallback);
        [DllImport("winmm.dll")]
        private static extern Int32 mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);

        public static bool OpenPlayer(string sFileName)
        {
            string _command = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, IntPtr.Zero)) != 0)
            {
                mciGetErrorString(err, buffer, 128);
                System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }

        public static bool Play(IntPtr handle)
        {
            string _command = "play MediaFile notify";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, handle)) != 0)
            {
                mciGetErrorString(err, buffer, 128);
                System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }

        public static bool Resume()
        {
            string _command = "resume MediaFile";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, IntPtr.Zero)) != 0)
            {
                mciGetErrorString(err, buffer, 128);
                System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }

        public static bool PausePlayer()
        {
            string _command = "pause MediaFile";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, IntPtr.Zero)) != 0)
            {
                mciGetErrorString(err, buffer, 128);
                System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }

        public static bool ClosePlayer()
        {
            string _command = "close MediaFile";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, IntPtr.Zero)) != 0)
            {
                //mciGetErrorString(err, buffer, 128);
                //System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }

        public static bool StopPlayer()
        {
            string _command = "stop MediaFile";
            Int32 err;
            StringBuilder buffer = new StringBuilder(128);
            if ((err = (Int32)mciSendString(_command, null, 0, IntPtr.Zero)) != 0)
            {
                mciGetErrorString(err, buffer, 128);
                System.Windows.Forms.MessageBox.Show(buffer.ToString());
                return false;
            }
            else
                return true;
        }
    }
}
