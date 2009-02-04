using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Octopus.CDIndex
{
    public partial class DlgAbout : Octopus.CDIndex.BaseDialogs.FormDialogList
    {
        
        public DlgAbout() {
            InitializeComponent();
            llCopyright.Text = assemblyCopyright;
            llVersion.Text = String.Format(llVersion.Text, assemblyVersion);
        }

        private void linkLabel1_Click(object sender, EventArgs e) {
            Process install = new Process();
            install.StartInfo.FileName = "mailto:kowaliszyn@users.sourceforge.net";
            install.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process install = new Process();
            install.StartInfo.FileName = "http://sourceforge.net/projects/octoindex/";
            install.Start();
        }

        private string assemblyCopyright {
            get {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private string assemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}

