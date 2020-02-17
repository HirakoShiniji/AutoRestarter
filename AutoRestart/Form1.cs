using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoRestart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = "Server Data Backup Timer > " + -progressBar1.Value;
            Process[] server = Process.GetProcessesByName("server");
            progressBar1.Increment(1);
            if (progressBar1.Value == 16000)
            {
                Process[] processes = Process.GetProcessesByName("server");
                Process game1 = processes[0];

                IntPtr p = game1.MainWindowHandle;

                SetForegroundWindow(p);
                SendKeys.Send("^(c)");

                listBox1.Items.Add("Restarted server to save data " + System.DateTime.Now);
                progressBar1.Value = 0;
            }

            if (server.Length < 1)
            {
                listBox1.Items.Add("Crashed At " + System.DateTime.Now);
                Process.Start("server.exe");
            }
            else
            {

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process.Start("server.exe");

            timer2.Start();
        }
    }
}
