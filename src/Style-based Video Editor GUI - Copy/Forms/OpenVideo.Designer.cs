
namespace Style_based_Video_Editor_GUI.Forms
{
  partial class OpenVideo
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
      this.Open = new System.Windows.Forms.Button();
      this.StartEditing = new System.Windows.Forms.Button();
      this.VideoPreview = new Vlc.DotNet.Forms.VlcControl();
      this.PathText = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.VideoPreview)).BeginInit();
      this.SuspendLayout();
      // 
      // Open
      // 
      this.Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Open.Location = new System.Drawing.Point(312, 12);
      this.Open.Name = "Open";
      this.Open.Size = new System.Drawing.Size(111, 25);
      this.Open.TabIndex = 1;
      this.Open.Text = "Open video";
      this.Open.UseVisualStyleBackColor = true;
      this.Open.Click += new System.EventHandler(this.Open_Click);
      // 
      // StartEditing
      // 
      this.StartEditing.Enabled = false;
      this.StartEditing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StartEditing.Location = new System.Drawing.Point(429, 12);
      this.StartEditing.Name = "StartEditing";
      this.StartEditing.Size = new System.Drawing.Size(111, 25);
      this.StartEditing.TabIndex = 3;
      this.StartEditing.Text = "Start Editing";
      this.StartEditing.UseVisualStyleBackColor = true;
      this.StartEditing.Click += new System.EventHandler(this.StartEditing_Click);
      // 
      // VideoPreview
      // 
      this.VideoPreview.BackColor = System.Drawing.Color.Black;
      this.VideoPreview.Location = new System.Drawing.Point(12, 41);
      this.VideoPreview.Name = "VideoPreview";
      this.VideoPreview.Size = new System.Drawing.Size(528, 291);
      this.VideoPreview.Spu = -1;
      this.VideoPreview.TabIndex = 4;
      this.VideoPreview.Text = "vlcControl1";
      this.VideoPreview.VlcLibDirectory = null;
      this.VideoPreview.VlcMediaplayerOptions = null;
      this.VideoPreview.VlcLibDirectoryNeeded += new System.EventHandler<Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs>(this.VideoPreview_VlcLibDirectoryNeeded);
      this.VideoPreview.Click += new System.EventHandler(this.VideoPreview_Click);
      // 
      // PathText
      // 
      this.PathText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PathText.HideSelection = false;
      this.PathText.Location = new System.Drawing.Point(13, 13);
      this.PathText.Name = "PathText";
      this.PathText.ReadOnly = true;
      this.PathText.Size = new System.Drawing.Size(293, 22);
      this.PathText.TabIndex = 5;
      // 
      // OpenVideo
      // 
      this.AcceptButton = this.Open;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(552, 346);
      this.Controls.Add(this.PathText);
      this.Controls.Add(this.VideoPreview);
      this.Controls.Add(this.StartEditing);
      this.Controls.Add(this.Open);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "OpenVideo";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Style-based Video Editor - Open";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenVideo_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.VideoPreview)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button Open;
    private System.Windows.Forms.Button StartEditing;
    private Vlc.DotNet.Forms.VlcControl VideoPreview;
    private System.Windows.Forms.TextBox PathText;
  }
}