using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Work_Session_Manager_Tool
{
    public partial class MainPage : Form
    {
        private const string directoryPath = @"C:\WorkSessionLogs";

        public MainPage()
        {
            CheckDirectory();

            InitializeComponent();
            
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        /// <summary>
        /// Create a directory if it does not already exist
        /// </summary>
        private void CheckDirectory()
        {
            Directory.CreateDirectory(directoryPath);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string text = $"I just started at: {DateTime.Now.ToLocalTime()} \r\n";
            WriteToLog(text);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            string text = $"I am closing at: {DateTime.Now.ToLocalTime()} \r\n";
            WriteToLog(text);
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                //I left my desk;
                string text = $"I am locking at: {DateTime.Now.ToLocalTime()} \r\n";
                WriteToLog(text);

            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                string text = $"I am unlocking at: {DateTime.Now.ToLocalTime()} \r\n";
                WriteToLog(text);
            }
        }

        private void WriteToLog(string text)
        {
            label1.Text = text;
            string path = $@"{directoryPath}\DailyLog-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Year}.txt";
            File.AppendAllText(path, text);
        }

        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Icon = new Icon(Path.GetFullPath(@"Images\log.ico"));
                notifyIcon1.Text = "System Activity Logger Utlity";
                notifyIcon1.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            Displaynotify();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void MainPage_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                Displaynotify();
                this.ShowInTaskbar = false;
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
