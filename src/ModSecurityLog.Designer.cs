namespace ModSecurityLogService
{
    partial class ModSecurityLog
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fsLogWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fsLogWatcher)).BeginInit();
            // 
            // fsLogWatcher
            // 
            this.fsLogWatcher.EnableRaisingEvents = true;
            this.fsLogWatcher.IncludeSubdirectories = true;
            this.fsLogWatcher.Created += new System.IO.FileSystemEventHandler(this.fsLogWatcher_Created);
            // 
            // ModSecurityLog
            // 
            this.ServiceName = "ModSecurityLogService";
            ((System.ComponentModel.ISupportInitialize)(this.fsLogWatcher)).EndInit();

        }

        #endregion

        private System.IO.FileSystemWatcher fsLogWatcher;
    }
}
