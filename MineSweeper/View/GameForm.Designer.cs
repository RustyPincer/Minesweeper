
namespace MineSweeper
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileLoadGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGameEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGameMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGameHard = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolLabelMines = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolLabelPlayer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(650, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNewGame,
            this.menuFileLoadGame,
            this.menuFileSaveGame,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(46, 24);
            this.menuFile.Text = "File";
            // 
            // menuFileNewGame
            // 
            this.menuFileNewGame.Name = "menuFileNewGame";
            this.menuFileNewGame.Size = new System.Drawing.Size(191, 26);
            this.menuFileNewGame.Text = "Új játék";
            this.menuFileNewGame.Click += new System.EventHandler(this.menuFileNewGame_Click);
            // 
            // menuFileLoadGame
            // 
            this.menuFileLoadGame.Name = "menuFileLoadGame";
            this.menuFileLoadGame.Size = new System.Drawing.Size(191, 26);
            this.menuFileLoadGame.Text = "Játék betöltése";
            this.menuFileLoadGame.Click += new System.EventHandler(this.MenuFileLoadGame_Click);
            // 
            // menuFileSaveGame
            // 
            this.menuFileSaveGame.Name = "menuFileSaveGame";
            this.menuFileSaveGame.Size = new System.Drawing.Size(191, 26);
            this.menuFileSaveGame.Text = "Játék mentése";
            this.menuFileSaveGame.Click += new System.EventHandler(this.MenuFileSaveGame_Click);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(191, 26);
            this.menuFileExit.Text = "Kilépés";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGameEasy,
            this.menuGameMedium,
            this.menuGameHard});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(95, 24);
            this.menuSettings.Text = "Beállítások";
            // 
            // menuGameEasy
            // 
            this.menuGameEasy.Name = "menuGameEasy";
            this.menuGameEasy.Size = new System.Drawing.Size(184, 26);
            this.menuGameEasy.Text = "Könnyű játék";
            this.menuGameEasy.Click += new System.EventHandler(this.menuGameEasy_Click);
            // 
            // menuGameMedium
            // 
            this.menuGameMedium.Name = "menuGameMedium";
            this.menuGameMedium.Size = new System.Drawing.Size(184, 26);
            this.menuGameMedium.Text = "Közepes játék";
            this.menuGameMedium.Click += new System.EventHandler(this.menuGameMedium_Click);
            // 
            // menuGameHard
            // 
            this.menuGameHard.Name = "menuGameHard";
            this.menuGameHard.Size = new System.Drawing.Size(184, 26);
            this.menuGameHard.Text = "Nehéz játék";
            this.menuGameHard.Click += new System.EventHandler(this.menuGameHard_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLabelMines,
            this.toolLabelPlayer,
            this.timerStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 684);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(650, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolLabelMines
            // 
            this.toolLabelMines.Name = "toolLabelMines";
            this.toolLabelMines.Size = new System.Drawing.Size(118, 20);
            this.toolLabelMines.Text = "NumberOfMines";
            // 
            // toolLabelPlayer
            // 
            this.toolLabelPlayer.Name = "toolLabelPlayer";
            this.toolLabelPlayer.Size = new System.Drawing.Size(49, 20);
            this.toolLabelPlayer.Text = "Player";
            // 
            // timerStatusLabel
            // 
            this.timerStatusLabel.Name = "timerStatusLabel";
            this.timerStatusLabel.Size = new System.Drawing.Size(47, 20);
            this.timerStatusLabel.Text = "Timer";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Aknakereső tábla (*.stl)|*.stl";
            this.saveFileDialog.Title = "Aknakereső mentése";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Aknakereső tábla (*.stl)|*.stl";
            this.openFileDialog.Title = "Aknakereső betöltése";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 710);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aknakereső";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNewGame;
        private System.Windows.Forms.ToolStripMenuItem menuFileLoadGame;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveGame;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuGameEasy;
        private System.Windows.Forms.ToolStripMenuItem menuGameMedium;
        private System.Windows.Forms.ToolStripMenuItem menuGameHard;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolLabelMines;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolLabelPlayer;
        private System.Windows.Forms.ToolStripStatusLabel timerStatusLabel;
        private System.Windows.Forms.Timer timer;
    }
}

