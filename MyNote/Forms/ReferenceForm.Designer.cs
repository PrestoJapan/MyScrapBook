namespace MyScrapBook
{
    partial class ReferenceForm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.basepanel = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.拡大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.縮小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.basepanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 220);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // basepanel
            // 
            this.basepanel.AutoScroll = true;
            this.basepanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basepanel.Controls.Add(this.pictureBox1);
            this.basepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basepanel.Location = new System.Drawing.Point(0, 0);
            this.basepanel.Name = "basepanel";
            this.basepanel.Size = new System.Drawing.Size(800, 450);
            this.basepanel.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.拡大ToolStripMenuItem,
            this.縮小ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // 拡大ToolStripMenuItem
            // 
            this.拡大ToolStripMenuItem.Name = "拡大ToolStripMenuItem";
            this.拡大ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.拡大ToolStripMenuItem.Text = "拡大";
            this.拡大ToolStripMenuItem.Click += new System.EventHandler(this.拡大ToolStripMenuItem_Click);
            // 
            // 縮小ToolStripMenuItem
            // 
            this.縮小ToolStripMenuItem.Name = "縮小ToolStripMenuItem";
            this.縮小ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.縮小ToolStripMenuItem.Text = "縮小";
            this.縮小ToolStripMenuItem.Click += new System.EventHandler(this.縮小ToolStripMenuItem_Click);
            // 
            // ReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.basepanel);
            this.Name = "ReferenceForm";
            this.Text = "ReferenceForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.basepanel.ResumeLayout(false);
            this.basepanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel basepanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 拡大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 縮小ToolStripMenuItem;
    }
}