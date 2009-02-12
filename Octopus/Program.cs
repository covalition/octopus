using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
	static class Program
	{

		[STAThread]
		static void Main()
		{
            // Localization test
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
			Application.EnableVisualStyles();
            FrmSplash splash = new FrmSplash();
            splash.Show();
            splash.Refresh();
            FrmMain f;
            try
            {
                f = new FrmMain(splash.llStatus);
            }
            finally
            {
                splash.Hide();
                // splash.Dispose();
            }
            //DlgWelcome dlgWelcome = new DlgWelcome();
            //dlgWelcome.ShowDialog();
			Application.Run(f);
		}
	}
}