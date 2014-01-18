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

        public static void OpenPlayer(string sFileName)
        {
            string _command = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }

        public static void Play(IntPtr handle)
        {
            string _command = "play MediaFile notify";
            mciSendString(_command, null, 0, handle);
        }

        public static void Resume()
        {
            string _command = "resume MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }

        public static void PausePlayer()
        {
            string _command = "pause MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }

        public static void ClosePlayer()
        {
            string _command = "close MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }

        public static void StopPlayer()
        {
            string _command = "stop MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
        }
    }
}
