/*
 * Octopus CD/DVD Indexer
 * Copyright (C) 2005 - 2009 Roman Kowaliszyn
 * 
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU General Public License version 3 as published by the
 * Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
 * details.
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
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
                splash.Dispose();
            }
            //DlgWelcome dlgWelcome = new DlgWelcome();
            //dlgWelcome.ShowDialog();
			Application.Run(f);
		}
	}
}