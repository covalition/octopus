using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
    public class CustomMenuStrip : MenuStrip
    {
        public CustomMenuStrip() {
            // Set renderer
            this.Renderer = new CustomMenuRenderer();
        }

        #region Custom Menu Renderer
        internal class CustomMenuRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) {
                Graphics g = e.Graphics;
                using (Brush b = new SolidBrush(Color.FromArgb(91, 91, 91))) {
                    g.FillRectangle(b, g.ClipBounds);
                }
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
                if (e.Item.Selected) {
                    using (Brush b = new SolidBrush(Color.FromArgb(80, 80, 80))) {
                        e.Graphics.FillRectangle(b, e.Graphics.ClipBounds);
                    }
                }
                else {
                    base.OnRenderMenuItemBackground(e);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
                e.TextColor = Color.White;
                base.OnRenderItemText(e);
            }
        }
        #endregion
    }
}
