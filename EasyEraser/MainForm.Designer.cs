namespace EasyEraser
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.MinButton = new System.Windows.Forms.Label();
            this.IconLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ProgressTimer = new System.Windows.Forms.Timer(this.components);
            this.MainLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainLayoutPanel.BackgroundImage = global::EasyEraser.UnityResource.Background;
            this.MainLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainLayoutPanel.ColumnCount = 3;
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.MainLayoutPanel.Controls.Add(this.TitleLabel, 0, 0);
            this.MainLayoutPanel.Controls.Add(this.MinButton, 2, 0);
            this.MainLayoutPanel.Controls.Add(this.IconLabel, 0, 1);
            this.MainLayoutPanel.Controls.Add(this.MessageLabel, 1, 1);
            this.MainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.RowCount = 2;
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayoutPanel.Size = new System.Drawing.Size(300, 120);
            this.MainLayoutPanel.TabIndex = 1;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.MainLayoutPanel.SetColumnSpan(this.TitleLabel, 2);
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(206)))), ((int)(((byte)(130)))));
            this.TitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.TitleLabel.Size = new System.Drawing.Size(268, 30);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "正在擦除目录和文件...";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MinButton
            // 
            this.MinButton.BackColor = System.Drawing.Color.Transparent;
            this.MinButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinButton.Image = global::EasyEraser.UnityResource.Min_0;
            this.MinButton.Location = new System.Drawing.Point(268, 0);
            this.MinButton.Margin = new System.Windows.Forms.Padding(0);
            this.MinButton.Name = "MinButton";
            this.MinButton.Size = new System.Drawing.Size(32, 30);
            this.MinButton.TabIndex = 2;
            // 
            // IconLabel
            // 
            this.IconLabel.BackColor = System.Drawing.Color.Transparent;
            this.IconLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IconLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IconLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.IconLabel.Image = global::EasyEraser.UnityResource.ProgressBar_0;
            this.IconLabel.Location = new System.Drawing.Point(0, 30);
            this.IconLabel.Margin = new System.Windows.Forms.Padding(0);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Padding = new System.Windows.Forms.Padding(5);
            this.IconLabel.Size = new System.Drawing.Size(90, 90);
            this.IconLabel.TabIndex = 6;
            this.IconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoEllipsis = true;
            this.MessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.MainLayoutPanel.SetColumnSpan(this.MessageLabel, 2);
            this.MessageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MessageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(206)))), ((int)(((byte)(130)))));
            this.MessageLabel.Location = new System.Drawing.Point(90, 30);
            this.MessageLabel.Margin = new System.Windows.Forms.Padding(0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Padding = new System.Windows.Forms.Padding(5);
            this.MessageLabel.Size = new System.Drawing.Size(210, 90);
            this.MessageLabel.TabIndex = 4;
            this.MessageLabel.Text = "正在擦除目录和文件...";
            this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgressTimer
            // 
            this.ProgressTimer.Tick += new System.EventHandler(this.ProgressTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(300, 120);
            this.Controls.Add(this.MainLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "简易擦除工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MainLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel MainLayoutPanel;
        private System.Windows.Forms.Label MinButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Label IconLabel;
        private System.Windows.Forms.Timer ProgressTimer;
    }
}

