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
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-br");
			Application.EnableVisualStyles();
            FrmMain f;
            using (FrmSplash splash = new FrmSplash()) {
                splash.Show();
                splash.Refresh();
                f = new FrmMain(splash.llStatus);
            }
			Application.Run(f);
		}
	}
}