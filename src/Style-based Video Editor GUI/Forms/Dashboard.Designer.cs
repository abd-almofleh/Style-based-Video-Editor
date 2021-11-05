
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
      this.OpenExample = new System.Windows.Forms.ToolStripMenuItem();
      this.VideoPlayer = new LibVLCSharp.WinForms.VideoView();
      this.Tabs = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.ScenesGroup = new System.Windows.Forms.GroupBox();
      this.PlayerGroup = new System.Windows.Forms.GroupBox();
      this.SceneTab = new System.Windows.Forms.TabPage();
      this.label1 = new System.Windows.Forms.Label();
      this.ScenePreviewMessage = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.PlayPause = new System.Windows.Forms.Button();
      this.AutoPlay = new System.Windows.Forms.CheckBox();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).BeginInit();
      this.Tabs.SuspendLayout();
      this.ScenesGroup.SuspendLayout();
      this.PlayerGroup.SuspendLayout();
      this.SceneTab.SuspendLayout();
      this.ScenePreviewMessage.SuspendLayout();
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
      this.OpenFile.Size = new System.Drawing.Size(151, 22);
      this.OpenFile.Text = "Open";
      this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
      // 
      // OpenExample
      // 
      this.OpenExample.Name = "OpenExample";
      this.OpenExample.Size = new System.Drawing.Size(151, 22);
      this.OpenExample.Text = "Open Example";
      this.OpenExample.Click += new System.EventHandler(this.OpenExample_Click);
      // 
      // VideoPlayer
      // 
      this.VideoPlayer.BackColor = System.Drawing.Color.Black;
      this.VideoPlayer.Location = new System.Drawing.Point(7, 19);
      this.VideoPlayer.MediaPlayer = null;
      this.VideoPlayer.Name = "VideoPlayer";
      this.VideoPlayer.Size = new System.Drawing.Size(297, 173);
      this.VideoPlayer.TabIndex = 4;
      this.VideoPlayer.Text = "videoView1";
      // 
      // Tabs
      // 
      this.Tabs.Controls.Add(this.SceneTab);
      this.Tabs.Controls.Add(this.tabPage2);
      this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tabs.Location = new System.Drawing.Point(0, 24);
      this.Tabs.Name = "Tabs";
      this.Tabs.SelectedIndex = 0;
      this.Tabs.Size = new System.Drawing.Size(946, 501);
      this.Tabs.TabIndex = 6;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(938, 475);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // ScenesGroup
      // 
      this.ScenesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ScenesGroup.Controls.Add(this.label1);
      this.ScenesGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScenesGroup.Location = new System.Drawing.Point(9, 6);
      this.ScenesGroup.Name = "ScenesGroup";
      this.ScenesGroup.Size = new System.Drawing.Size(923, 200);
      this.ScenesGroup.TabIndex = 5;
      this.ScenesGroup.TabStop = false;
      this.ScenesGroup.Text = "Scenes List";
      // 
      // PlayerGroup
      // 
      this.PlayerGroup.Controls.Add(this.ScenePreviewMessage);
      this.PlayerGroup.Controls.Add(this.AutoPlay);
      this.PlayerGroup.Controls.Add(this.VideoPlayer);
      this.PlayerGroup.Controls.Add(this.PlayPause);
      this.PlayerGroup.Location = new System.Drawing.Point(9, 212);
      this.PlayerGroup.Name = "PlayerGroup";
      this.PlayerGroup.Size = new System.Drawing.Size(310, 250);
      this.PlayerGroup.TabIndex = 4;
      this.PlayerGroup.TabStop = false;
      this.PlayerGroup.Text = "SceneVideoPreview";
      // 
      // SceneTab
      // 
      this.SceneTab.Controls.Add(this.PlayerGroup);
      this.SceneTab.Controls.Add(this.ScenesGroup);
      this.SceneTab.Location = new System.Drawing.Point(4, 22);
      this.SceneTab.Name = "SceneTab";
      this.SceneTab.Padding = new System.Windows.Forms.Padding(3);
      this.SceneTab.Size = new System.Drawing.Size(938, 475);
      this.SceneTab.TabIndex = 0;
      this.SceneTab.Text = "ScenesTab";
      this.SceneTab.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(296, 86);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(342, 29);
      this.label1.TabIndex = 0;
      this.label1.Text = "Open a file to see the scenes...";
      // 
      // ScenePreviewMessage
      // 
      this.ScenePreviewMessage.Controls.Add(this.label2);
      this.ScenePreviewMessage.Location = new System.Drawing.Point(7, 19);
      this.ScenePreviewMessage.Name = "ScenePreviewMessage";
      this.ScenePreviewMessage.Size = new System.Drawing.Size(297, 225);
      this.ScenePreviewMessage.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(24, 102);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(258, 25);
      this.label2.TabIndex = 0;
      this.label2.Text = "Select scene to preview...";
      // 
      // PlayPause
      // 
      this.PlayPause.BackColor = System.Drawing.Color.Transparent;
      this.PlayPause.BackgroundImage = global::Style_based_Video_Editor_GUI.Properties.Resources.play_button;
      this.PlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.PlayPause.FlatAppearance.BorderSize = 0;
      this.PlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.PlayPause.Location = new System.Drawing.Point(136, 198);
      this.PlayPause.Name = "PlayPause";
      this.PlayPause.Size = new System.Drawing.Size(38, 37);
      this.PlayPause.TabIndex = 3;
      this.PlayPause.UseVisualStyleBackColor = false;
      this.PlayPause.Click += new System.EventHandler(this.PlayPause_Click);
      // 
      // AutoPlay
      // 
      this.AutoPlay.AutoSize = true;
      this.AutoPlay.Checked = true;
      this.AutoPlay.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AutoPlay.Location = new System.Drawing.Point(7, 218);
      this.AutoPlay.Name = "AutoPlay";
      this.AutoPlay.Size = new System.Drawing.Size(71, 17);
      this.AutoPlay.TabIndex = 5;
      this.AutoPlay.Text = "Auto Play";
      this.AutoPlay.UseVisualStyleBackColor = true;
      // 
      // Dashboard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(946, 525);
      this.Controls.Add(this.Tabs);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Dashboard";
      this.Text = "Style-based Video Editor";
      this.Load += new System.EventHandler(this.Dashboard_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).EndInit();
      this.Tabs.ResumeLayout(false);
      this.ScenesGroup.ResumeLayout(false);
      this.ScenesGroup.PerformLayout();
      this.PlayerGroup.ResumeLayout(false);
      this.PlayerGroup.PerformLayout();
      this.SceneTab.ResumeLayout(false);
      this.ScenePreviewMessage.ResumeLayout(false);
      this.ScenePreviewMessage.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem OpenFile;
    private LibVLCSharp.WinForms.VideoView VideoPlayer;
    private System.Windows.Forms.ToolStripMenuItem OpenExample;
    private System.Windows.Forms.TabControl Tabs;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage SceneTab;
    private System.Windows.Forms.GroupBox PlayerGroup;
    private System.Windows.Forms.GroupBox ScenesGroup;
    private System.Windows.Forms.Button PlayPause;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel ScenePreviewMessage;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox AutoPlay;
  }
}