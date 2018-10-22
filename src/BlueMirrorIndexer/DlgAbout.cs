using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Igorary.Forms.Forms;

namespace BlueMirrorIndexer
{
    public partial class DlgAbout : FormDialogList
    {
        
        public DlgAbout() {
            InitializeComponent();
            llCopyright.Text = assemblyCopyright;
            llVersion.Text = String.Format(llVersion.Text, assemblyVersion);
            llTitle.Text = ProductName;
            tbHistory.Text = Properties.Resources.History;
            tbLicense.Text = Properties.Resources.License;
        }

        private void llBugs_Click(object sender, EventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "https://github.com/covalition/octopus/issues";
            navigate.Start();
        }

        private void llGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "https://github.com/covalition/octopus";
            navigate.Start();
        }

        private static string assemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private static string assemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        //private string assemblyTitle {
        //    get {
        //        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        //        if (attributes.Length == 0)
        //            return "";
        //        return ((AssemblyTitleAttribute)attributes[0]).Title;
        //    }
        //}

        private void llGithubPages_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process navigate = new Process();
            navigate.StartInfo.FileName = "https://covalition.github.io/octopus/";
            navigate.Start();
        }

    }
}

