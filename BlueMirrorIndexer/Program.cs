using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlueMirrorIndexer
{
	static class Program
	{

		[STAThread]
		static void Main()
		{
            // Localization test
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
			Application.EnableVisualStyles();
            //FrmMain f;
            //using (FrmSplash splash = new FrmSplash()) {
            //    splash.Show();
            //    splash.Refresh();
            //    f = new FrmMain(splash.llStatus);
            //}
            //Application.Run(f);
            FrmMain.Instance = new FrmMain();
            Application.Run(FrmMain.Instance);
		}
	}
}