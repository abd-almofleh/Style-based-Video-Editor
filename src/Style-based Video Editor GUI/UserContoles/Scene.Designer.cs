
namespace Style_based_Video_Editor_GUI.UserContoles
{
  partial class Scene
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
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.Title = new System.Windows.Forms.Label();
      this.ImageBox = new System.Windows.Forms.PictureBox();
      this.flowLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.flowLayoutPanel1.Controls.Add(this.Title);
      this.flowLayoutPanel1.Controls.Add(this.ImageBox);
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(72, 118);
      this.flowLayoutPanel1.TabIndex = 0;
      this.flowLayoutPanel1.WrapContents = false;
      // 
      // Title
      // 
      this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Title.Location = new System.Drawing.Point(3, 0);
      this.Title.Name = "Title";
      this.Title.Size = new System.Drawing.Size(64, 18);
      this.Title.TabIndex = 0;
      this.Title.Text = "label1";
      this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ImageBox
      // 
      this.ImageBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.ImageBox.Location = new System.Drawing.Point(3, 21);
      this.ImageBox.Name = "ImageBox";
      this.ImageBox.Size = new System.Drawing.Size(64, 92);
      this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.ImageBox.TabIndex = 1;
      this.ImageBox.TabStop = false;
      // 
      // Scene
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.flowLayoutPanel1);
      this.Name = "Scene";
      this.Size = new System.Drawing.Size(75, 121);
      this.flowLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label Title;
    private System.Windows.Forms.PictureBox ImageBox;
  }
}
