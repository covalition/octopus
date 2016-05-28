using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace BlueMirror.Commons
{
    public partial class FileOperations : Component
    {
        public FileOperations() {
            InitializeComponent();
        }

        public FileOperations(IContainer container) {
            container.Add(this);
            InitializeComponent();
        }

        private string currentFilePath = null;

        [DefaultValue(null)]
        public string CurrentFilePath {
            get {
                return currentFilePath;
            }
            set {
                currentFilePath = value;
                OnCurrentFilePathChanged();
            }
        }

        private bool modified = false;

        [DefaultValue(false)]
        public bool Modified {
            get {
                return modified;
            }
            set {
                if (modified != value) {
                    modified = value;
                    OnModifiedChanged();
                }
            }
        }
        
        [DefaultValue(null)]
        public SaveFileDialog SaveFileDialog { get; set; }

        [DefaultValue(null)]
        public OpenFileDialog OpenFileDialog { get; set; }

        public event EventHandler<SaveToFileEventArgs> SaveToFile;

        public event EventHandler<OpenFromFileEventArgs> OpenFromFile;

        public event EventHandler NewFile;

        public event EventHandler ModifiedChanged;

        public event EventHandler CurrentFilePathChanged;

        public event EventHandler FileChanged;

        public void New() {
            if (SaveWithAsk()) {
                OnNewFile();
                CurrentFilePath = null;
                Modified = false;
                OnFileChanged();
            }
        }

        public bool Save() {
            if (CurrentFilePath == null)
                return SaveAs();
            else
                return saveAs(CurrentFilePath);
        }

        public bool SaveAs() {
            SaveFileDialog sfd = SaveFileDialog;
            if (sfd == null)
                sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK) {
                return saveAs(sfd.FileName);
            }
            else
                return false;
        }

        private bool saveAs(string filePath) {
            try {
                OnSaveToFile(filePath);
                CurrentFilePath = filePath;
                Modified = false;
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SaveWithAsk() {
            if (modified) {
                DialogResult dialogResult = MessageBox.Show("Database has been changed. Do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel)
                    return false;
                else
                    if (dialogResult == DialogResult.Yes)
                        return Save();
            }
            return true;
        }

        public bool Open() {
            if (SaveWithAsk()) {
                OpenFileDialog ofd = OpenFileDialog;
                if (ofd == null)
                    ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                    return OpenFile(ofd.FileName);
            }
            return false;
        }

        public bool OpenFile(string filePath) {
            bool fileValid = OnOpenFromFile(filePath);
            if (!fileValid)
                New();
            else {
                CurrentFilePath = filePath;
                Modified = false;
                OnFileChanged();
            }
            return true;
        }

        protected void OnNewFile() {
            if (NewFile != null)
                NewFile(this, EventArgs.Empty);
        }

        protected void OnSaveToFile(string filePath) {
            if (SaveToFile != null)
                SaveToFile(this, new SaveToFileEventArgs(filePath));
        }

        protected bool OnOpenFromFile(string filePath) {
            if (OpenFromFile != null) {
                OpenFromFileEventArgs args = new OpenFromFileEventArgs(filePath);
                OpenFromFile(this, args);
                return args.FileValid;
            }
            else
                return false;
        }

        protected void OnModifiedChanged() {
            if (ModifiedChanged != null)
                ModifiedChanged(this, EventArgs.Empty);
        }

        private void OnCurrentFilePathChanged() {
            if (CurrentFilePathChanged != null)
                CurrentFilePathChanged(this, EventArgs.Empty);
        }

        private void OnFileChanged() {
            if (FileChanged != null)
                FileChanged(this, EventArgs.Empty);
        }
        
    }
}
