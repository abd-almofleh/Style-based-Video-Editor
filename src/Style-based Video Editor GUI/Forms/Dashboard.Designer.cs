
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
      this.closeVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.VideoPlayer = new LibVLCSharp.WinForms.VideoView();
      this.Tabs = new System.Windows.Forms.TabControl();
      this.SceneTab = new System.Windows.Forms.TabPage();
      this.SceneInfo = new System.Windows.Forms.GroupBox();
      this.SceneInfoMessage = new System.Windows.Forms.Panel();
      this.label9 = new System.Windows.Forms.Label();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label8 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.SceneNumber = new System.Windows.Forms.Label();
      this.StartTime = new System.Windows.Forms.Label();
      this.EndTime = new System.Windows.Forms.Label();
      this.Length = new System.Windows.Forms.Label();
      this.StartFrame = new System.Windows.Forms.Label();
      this.EndFrame = new System.Windows.Forms.Label();
      this.SceneImagePreview = new System.Windows.Forms.PictureBox();
      this.SceneVideoPreview = new System.Windows.Forms.GroupBox();
      this.ScenePreviewMessage = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.AutoPlay = new System.Windows.Forms.CheckBox();
      this.PlayPause = new System.Windows.Forms.Button();
      this.ScenesGroup = new System.Windows.Forms.GroupBox();
      this.ScenesListMessage = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).BeginInit();
      this.Tabs.SuspendLayout();
      this.SceneTab.SuspendLayout();
      this.SceneInfo.SuspendLayout();
      this.SceneInfoMessage.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SceneImagePreview)).BeginInit();
      this.SceneVideoPreview.SuspendLayout();
      this.ScenePreviewMessage.SuspendLayout();
      this.ScenesGroup.SuspendLayout();
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
            this.OpenExample,
            this.closeVideoToolStripMenuItem});
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
      // OpenExample
      // 
      this.OpenExample.Enabled = false;
      this.OpenExample.Name = "OpenExample";
      this.OpenExample.Size = new System.Drawing.Size(180, 22);
      this.OpenExample.Text = "Open Example";
      this.OpenExample.Click += new System.EventHandler(this.OpenExample_Click);
      // 
      // closeVideoToolStripMenuItem
      // 
      this.closeVideoToolStripMenuItem.Enabled = false;
      this.closeVideoToolStripMenuItem.Name = "closeVideoToolStripMenuItem";
      this.closeVideoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.closeVideoToolStripMenuItem.Text = "Close Video";
      this.closeVideoToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.closeVideoToolStripMenuItem.Click += new System.EventHandler(this.closeVideoToolStripMenuItem_Click);
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
      // SceneTab
      // 
      this.SceneTab.Controls.Add(this.SceneInfo);
      this.SceneTab.Controls.Add(this.SceneVideoPreview);
      this.SceneTab.Controls.Add(this.ScenesGroup);
      this.SceneTab.Location = new System.Drawing.Point(4, 22);
      this.SceneTab.Name = "SceneTab";
      this.SceneTab.Padding = new System.Windows.Forms.Padding(3);
      this.SceneTab.Size = new System.Drawing.Size(938, 475);
      this.SceneTab.TabIndex = 0;
      this.SceneTab.Text = "ScenesTab";
      this.SceneTab.UseVisualStyleBackColor = true;
      // 
      // SceneInfo
      // 
      this.SceneInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.SceneInfo.Controls.Add(this.SceneInfoMessage);
      this.SceneInfo.Controls.Add(this.tableLayoutPanel1);
      this.SceneInfo.Controls.Add(this.SceneImagePreview);
      this.SceneInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SceneInfo.Location = new System.Drawing.Point(326, 213);
      this.SceneInfo.Name = "SceneInfo";
      this.SceneInfo.Size = new System.Drawing.Size(604, 243);
      this.SceneInfo.TabIndex = 6;
      this.SceneInfo.TabStop = false;
      this.SceneInfo.Text = "Scene Information";
      // 
      // SceneInfoMessage
      // 
      this.SceneInfoMessage.Controls.Add(this.label9);
      this.SceneInfoMessage.Location = new System.Drawing.Point(6, 18);
      this.SceneInfoMessage.Name = "SceneInfoMessage";
      this.SceneInfoMessage.Size = new System.Drawing.Size(592, 219);
      this.SceneInfoMessage.TabIndex = 6;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(115, 102);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(364, 25);
      this.label9.TabIndex = 0;
      this.label9.Text = "Select a scene to show information...";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.label7, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.SceneNumber, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.StartTime, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.EndTime, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.Length, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.StartFrame, 1, 4);
      this.tableLayoutPanel1.Controls.Add(this.EndFrame, 1, 5);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(299, 26);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 6;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 208);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(4, 171);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(142, 36);
      this.label8.TabIndex = 5;
      this.label8.Text = "EndFrame";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(4, 103);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(142, 33);
      this.label6.TabIndex = 3;
      this.label6.Text = "Length";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(4, 1);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(142, 33);
      this.label3.TabIndex = 0;
      this.label3.Text = "Scene Number";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(4, 35);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(142, 33);
      this.label4.TabIndex = 1;
      this.label4.Text = "Start Time";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(4, 69);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(142, 33);
      this.label5.TabIndex = 2;
      this.label5.Text = "End Time";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(4, 137);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(142, 33);
      this.label7.TabIndex = 4;
      this.label7.Text = "Start Frame";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // SceneNumber
      // 
      this.SceneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SceneNumber.AutoSize = true;
      this.SceneNumber.Location = new System.Drawing.Point(153, 1);
      this.SceneNumber.Name = "SceneNumber";
      this.SceneNumber.Size = new System.Drawing.Size(142, 33);
      this.SceneNumber.TabIndex = 6;
      this.SceneNumber.Text = "#SceneNumber";
      this.SceneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // StartTime
      // 
      this.StartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StartTime.AutoSize = true;
      this.StartTime.Location = new System.Drawing.Point(153, 35);
      this.StartTime.Name = "StartTime";
      this.StartTime.Size = new System.Drawing.Size(142, 33);
      this.StartTime.TabIndex = 7;
      this.StartTime.Text = "#StartTime";
      this.StartTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // EndTime
      // 
      this.EndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.EndTime.AutoSize = true;
      this.EndTime.Location = new System.Drawing.Point(153, 69);
      this.EndTime.Name = "EndTime";
      this.EndTime.Size = new System.Drawing.Size(142, 33);
      this.EndTime.TabIndex = 8;
      this.EndTime.Text = "#EndTime";
      this.EndTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Length
      // 
      this.Length.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.Length.AutoSize = true;
      this.Length.Location = new System.Drawing.Point(153, 103);
      this.Length.Name = "Length";
      this.Length.Size = new System.Drawing.Size(142, 33);
      this.Length.TabIndex = 9;
      this.Length.Text = "#Length";
      this.Length.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // StartFrame
      // 
      this.StartFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.StartFrame.AutoSize = true;
      this.StartFrame.Location = new System.Drawing.Point(153, 137);
      this.StartFrame.Name = "StartFrame";
      this.StartFrame.Size = new System.Drawing.Size(142, 33);
      this.StartFrame.TabIndex = 10;
      this.StartFrame.Text = "#StartFrame";
      this.StartFrame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // EndFrame
      // 
      this.EndFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.EndFrame.AutoSize = true;
      this.EndFrame.Location = new System.Drawing.Point(153, 171);
      this.EndFrame.Name = "EndFrame";
      this.EndFrame.Size = new System.Drawing.Size(142, 36);
      this.EndFrame.TabIndex = 11;
      this.EndFrame.Text = "#EndFrame";
      this.EndFrame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // SceneImagePreview
      // 
      this.SceneImagePreview.Location = new System.Drawing.Point(6, 25);
      this.SceneImagePreview.Name = "SceneImagePreview";
      this.SceneImagePreview.Size = new System.Drawing.Size(286, 209);
      this.SceneImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.SceneImagePreview.TabIndex = 1;
      this.SceneImagePreview.TabStop = false;
      // 
      // SceneVideoPreview
      // 
      this.SceneVideoPreview.Controls.Add(this.ScenePreviewMessage);
      this.SceneVideoPreview.Controls.Add(this.AutoPlay);
      this.SceneVideoPreview.Controls.Add(this.VideoPlayer);
      this.SceneVideoPreview.Controls.Add(this.PlayPause);
      this.SceneVideoPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SceneVideoPreview.Location = new System.Drawing.Point(9, 212);
      this.SceneVideoPreview.Name = "SceneVideoPreview";
      this.SceneVideoPreview.Size = new System.Drawing.Size(310, 250);
      this.SceneVideoPreview.TabIndex = 4;
      this.SceneVideoPreview.TabStop = false;
      this.SceneVideoPreview.Text = "Scene Preview";
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
      this.label2.Location = new System.Drawing.Point(11, 102);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(276, 25);
      this.label2.TabIndex = 0;
      this.label2.Text = "Select a scene to preview...";
      // 
      // AutoPlay
      // 
      this.AutoPlay.AutoSize = true;
      this.AutoPlay.Checked = true;
      this.AutoPlay.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AutoPlay.Location = new System.Drawing.Point(7, 218);
      this.AutoPlay.Name = "AutoPlay";
      this.AutoPlay.Size = new System.Drawing.Size(95, 24);
      this.AutoPlay.TabIndex = 5;
      this.AutoPlay.Text = "Auto Play";
      this.AutoPlay.UseVisualStyleBackColor = true;
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
      // ScenesGroup
      // 
      this.ScenesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ScenesGroup.Controls.Add(this.ScenesListMessage);
      this.ScenesGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScenesGroup.Location = new System.Drawing.Point(9, 6);
      this.ScenesGroup.Name = "ScenesGroup";
      this.ScenesGroup.Size = new System.Drawing.Size(923, 200);
      this.ScenesGroup.TabIndex = 5;
      this.ScenesGroup.TabStop = false;
      this.ScenesGroup.Text = "Scenes List";
      // 
      // ScenesListMessage
      // 
      this.ScenesListMessage.AutoSize = true;
      this.ScenesListMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScenesListMessage.Location = new System.Drawing.Point(296, 86);
      this.ScenesListMessage.Name = "ScenesListMessage";
      this.ScenesListMessage.Size = new System.Drawing.Size(342, 29);
      this.ScenesListMessage.TabIndex = 0;
      this.ScenesListMessage.Text = "Open a file to see the scenes...";
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
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
      this.Load += new System.EventHandler(this.Dashboard_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).EndInit();
      this.Tabs.ResumeLayout(false);
      this.SceneTab.ResumeLayout(false);
      this.SceneInfo.ResumeLayout(false);
      this.SceneInfoMessage.ResumeLayout(false);
      this.SceneInfoMessage.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SceneImagePreview)).EndInit();
      this.SceneVideoPreview.ResumeLayout(false);
      this.SceneVideoPreview.PerformLayout();
      this.ScenePreviewMessage.ResumeLayout(false);
      this.ScenePreviewMessage.PerformLayout();
      this.ScenesGroup.ResumeLayout(false);
      this.ScenesGroup.PerformLayout();
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
    private System.Windows.Forms.GroupBox SceneVideoPreview;
    private System.Windows.Forms.GroupBox ScenesGroup;
    private System.Windows.Forms.Button PlayPause;
    private System.Windows.Forms.Label ScenesListMessage;
    private System.Windows.Forms.Panel ScenePreviewMessage;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox AutoPlay;
    private System.Windows.Forms.GroupBox SceneInfo;
    private System.Windows.Forms.PictureBox SceneImagePreview;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label SceneNumber;
    private System.Windows.Forms.Label StartTime;
    private System.Windows.Forms.Label EndTime;
    private System.Windows.Forms.Label Length;
    private System.Windows.Forms.Label StartFrame;
    private System.Windows.Forms.Label EndFrame;
    private System.Windows.Forms.Panel SceneInfoMessage;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ToolStripMenuItem closeVideoToolStripMenuItem;
  }
}