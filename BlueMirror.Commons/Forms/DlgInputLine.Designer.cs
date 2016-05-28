namespace BlueMirror.Commons.Forms
{
    partial class DlgInputLine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.llPrompt = new System.Windows.Forms.Label();
            this.tbInputLine = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(250, 55);
            this.btnOK.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(331, 55);
            this.btnCancel.TabIndex = 3;
            // 
            // llPrompt
            // 
            this.llPrompt.AutoSize = true;
            this.llPrompt.Location = new System.Drawing.Point(9, 9);
            this.llPrompt.Name = "llPrompt";
            this.llPrompt.Size = new System.Drawing.Size(45, 13);
            this.llPrompt.TabIndex = 0;
            this.llPrompt.Text = "Pytanie:";
            // 
            // tbInputLine
            // 
            this.tbInputLine.Location = new System.Drawing.Point(12, 25);
            this.tbInputLine.Name = "tbInputLine";
            this.tbInputLine.Size = new System.Drawing.Size(394, 20);
            this.tbInputLine.TabIndex = 1;
            // 
            // DlgInputLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(418, 90);
            this.Controls.Add(this.tbInputLine);
            this.Controls.Add(this.llPrompt);
            this.Name = "DlgInputLine";
            this.Controls.SetChildIndex(this.llPrompt, 0);
            this.Controls.SetChildIndex(this.tbInputLine, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label llPrompt;
        private System.Windows.Forms.TextBox tbInputLine;
    }
}
