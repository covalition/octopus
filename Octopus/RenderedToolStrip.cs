using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Octopus.CDIndex
{
    public class RenderedToolStrip : ToolStrip
    {

        class NewRenderer : ToolStripProfessionalRenderer
        {

            public NewRenderer() {
                this.RoundedEdges = false;
            }

            
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
                if (e.Item.Selected && !(e.Item is ToolStripMenuItem)) {
                    e.TextColor = Color.FromArgb(255, 223, 127);
                }
                //if (e.Item.IsOnDropDown) {
                  //  e.
                //}

                base.OnRenderItemText(e);
            }

            
            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
                if (!(e.Item.Selected)) {
                    base.OnRenderButtonBackground(e);
                }
            }

            protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
                //if (!(e.Item.Selected)) {
                //    base.OnRenderDropDownButtonBackground(e);
                //}
                //base.OnRenderDropDownButtonBackground(e);
            }

            //protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
                //base.OnRenderMenuItemBackground(e);
            //}

        }

        public RenderedToolStrip() {
            GripStyle = ToolStripGripStyle.Hidden;
            Renderer = new NewRenderer();
            BackgroundImage = Properties.Resources.BlueHeader; // BlueStripBitmap;
        }
    }
}
