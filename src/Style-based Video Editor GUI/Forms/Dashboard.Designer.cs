
namespace Style_based_Video_Editor_GUI.Forms
{
  partial class Dashboard
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
      this.PlayerGroup = new System.Windows.Forms.GroupBox();
      this.PlayPause = new System.Windows.Forms.Button();
      this.VideoPlayer = new LibVLCSharp.WinForms.VideoView();
      this.OpenExample = new System.Windows.Forms.ToolStripMenuItem();
      this.Scenes = new System.Windows.Forms.GroupBox();
      this.menuStrip1.SuspendLayout();
      this.PlayerGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.BackColor = System.Drawing.Color.Gainsboro;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(946, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.OpenExample});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // OpenFile
      // 
      this.OpenFile.Name = "OpenFile";
      this.OpenFile.Size = new System.Drawing.Size(180, 22);
      this.OpenFile.Text = "Open";
      this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
      // 
      // PlayerGroup
      // 
      this.PlayerGroup.Controls.Add(this.VideoPlayer);
      this.PlayerGroup.Controls.Add(this.PlayPause);
      this.PlayerGroup.Location = new System.Drawing.Point(12, 27);
      this.PlayerGroup.Name = "PlayerGroup";
      this.PlayerGroup.Size = new System.Drawing.Size(310, 279);
      this.PlayerGroup.TabIndex = 4;
      this.PlayerGroup.TabStop = false;
      this.PlayerGroup.Text = "Player";
      // 
      // PlayPause
      // 
      this.PlayPause.BackColor = System.Drawing.Color.Transparent;
      this.PlayPause.BackgroundImage = global::Style_based_Video_Editor_GUI.Properties.Resources.play_button;
      this.PlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.PlayPause.FlatAppearance.BorderSize = 0;
      this.PlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.PlayPause.Location = new System.Drawing.Point(137, 228);
      this.PlayPause.Name = "PlayPause";
      this.PlayPause.Size = new System.Drawing.Size(38, 37);
      this.PlayPause.TabIndex = 3;
      this.PlayPause.UseVisualStyleBackColor = false;
      this.PlayPause.Click += new System.EventHandler(this.PlayPause_Click);
      // 
      // VideoPlayer
      // 
      this.VideoPlayer.BackColor = System.Drawing.Color.Black;
      this.VideoPlayer.Location = new System.Drawing.Point(7, 19);
      this.VideoPlayer.MediaPlayer = null;
      this.VideoPlayer.Name = "VideoPlayer";
      this.VideoPlayer.Size = new System.Drawing.Size(297, 203);
      this.VideoPlayer.TabIndex = 4;
      this.VideoPlayer.Text = "videoView1";
      // 
      // OpenExample
      // 
      this.OpenExample.Name = "OpenExample";
      this.OpenExample.Size = new System.Drawing.Size(180, 22);
      this.OpenExample.Text = "Open Example";
      this.OpenExample.Click += new System.EventHandler(this.OpenExample_Click);
      // 
      // Scenes
      // 
      this.Scenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Scenes.Location = new System.Drawing.Point(13, 313);
      this.Scenes.Name = "Scenes";
      this.Scenes.Size = new System.Drawing.Size(921, 200);
      this.Scenes.TabIndex = 5;
      this.Scenes.TabStop = false;
      this.Scenes.Text = "Scenes";
      // 
      // Dashboard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(946, 525);
      this.Controls.Add(this.Scenes);
      this.Controls.Add(this.PlayerGroup);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Dashboard";
      this.Text = "Style-based Video Editor";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.PlayerGroup.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem OpenFile;
    private System.Windows.Forms.Button PlayPause;
    private System.Windows.Forms.GroupBox PlayerGroup;
    private LibVLCSharp.WinForms.VideoView VideoPlayer;
    private System.Windows.Forms.ToolStripMenuItem OpenExample;
    private System.Windows.Forms.GroupBox Scenes;
  }
}