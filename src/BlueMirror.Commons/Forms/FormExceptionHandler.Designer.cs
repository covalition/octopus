namespace BlueMirror.Commons.Forms
{
    partial class FormExceptionHandler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExceptionHandler));
            this.label1 = new System.Windows.Forms.Label();
            this.tbErrorDetails = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.Font = null;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // tbErrorDetails
            // 
            this.tbErrorDetails.AccessibleDescription = null;
            this.tbErrorDetails.AccessibleName = null;
            resources.ApplyResources(this.tbErrorDetails, "tbErrorDetails");
            this.tbErrorDetails.BackgroundImage = null;
            this.tbErrorDetails.Font = null;
            this.tbErrorDetails.Name = "tbErrorDetails";
            this.tbErrorDetails.ReadOnly = true;
            // 
            // btnSend
            // 
            this.btnSend.AccessibleDescription = null;
            this.btnSend.AccessibleName = null;
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.BackgroundImage = null;
            this.btnSend.Font = null;
            this.btnSend.Name = "btnSend";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // FormExceptionHandler
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbErrorDetails);
            this.Controls.Add(this.btnSend);
            this.Font = null;
            this.Icon = null;
            this.Name = "FormExceptionHandler";
            this.Controls.SetChildIndex(this.btnSend, 0);
            this.Controls.SetChildIndex(this.tbErrorDetails, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbErrorDetails;
        private System.Windows.Forms.Button btnSend;
    }
}
