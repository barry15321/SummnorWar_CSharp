namespace SummonersWar
{
    partial class AssistantInterface
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssistantInterface));
            this.ToolMenu = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listHwndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summonerWarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoTable = new System.Windows.Forms.GroupBox();
            this.AStatus = new System.Windows.Forms.Label();
            this.BStatus = new System.Windows.Forms.Label();
            this.ATStatusLabel = new System.Windows.Forms.Label();
            this.BStatusLabel = new System.Windows.Forms.Label();
            this.BSRunningTimer = new System.Windows.Forms.Timer(this.components);
            this.CompareImageTimer = new System.Windows.Forms.Timer(this.components);
            this.SimulateClickTimer = new System.Windows.Forms.Timer(this.components);
            this.ToolMenu.SuspendLayout();
            this.InfoTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolMenu
            // 
            this.ToolMenu.BackColor = System.Drawing.Color.Transparent;
            this.ToolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.ToolMenu.Location = new System.Drawing.Point(0, 0);
            this.ToolMenu.Name = "ToolMenu";
            this.ToolMenu.Size = new System.Drawing.Size(370, 24);
            this.ToolMenu.TabIndex = 0;
            this.ToolMenu.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listHwndToolStripMenuItem,
            this.summonerWarToolStripMenuItem,
            this.imageReloadToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.openToolStripMenuItem.Text = "Window";
            // 
            // listHwndToolStripMenuItem
            // 
            this.listHwndToolStripMenuItem.Name = "listHwndToolStripMenuItem";
            this.listHwndToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.listHwndToolStripMenuItem.Text = "Window List";
            this.listHwndToolStripMenuItem.Click += new System.EventHandler(this.listHwndToolStripMenuItem_Click);
            // 
            // summonerWarToolStripMenuItem
            // 
            this.summonerWarToolStripMenuItem.Name = "summonerWarToolStripMenuItem";
            this.summonerWarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.summonerWarToolStripMenuItem.Text = "SummonerWar";
            this.summonerWarToolStripMenuItem.Click += new System.EventHandler(this.summonerWarToolStripMenuItem_Click);
            // 
            // imageReloadToolStripMenuItem
            // 
            this.imageReloadToolStripMenuItem.Name = "imageReloadToolStripMenuItem";
            this.imageReloadToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.imageReloadToolStripMenuItem.Text = "Image Reload";
            this.imageReloadToolStripMenuItem.Click += new System.EventHandler(this.imageReloadToolStripMenuItem_Click);
            // 
            // InfoTable
            // 
            this.InfoTable.Controls.Add(this.AStatus);
            this.InfoTable.Controls.Add(this.BStatus);
            this.InfoTable.Controls.Add(this.ATStatusLabel);
            this.InfoTable.Controls.Add(this.BStatusLabel);
            this.InfoTable.Location = new System.Drawing.Point(11, 27);
            this.InfoTable.Name = "InfoTable";
            this.InfoTable.Size = new System.Drawing.Size(346, 148);
            this.InfoTable.TabIndex = 2;
            this.InfoTable.TabStop = false;
            this.InfoTable.Text = "Info";
            // 
            // AStatus
            // 
            this.AStatus.AutoSize = true;
            this.AStatus.Location = new System.Drawing.Point(149, 66);
            this.AStatus.Name = "AStatus";
            this.AStatus.Size = new System.Drawing.Size(18, 17);
            this.AStatus.TabIndex = 5;
            this.AStatus.Text = "\"\"";
            // 
            // BStatus
            // 
            this.BStatus.AutoSize = true;
            this.BStatus.Location = new System.Drawing.Point(149, 31);
            this.BStatus.Name = "BStatus";
            this.BStatus.Size = new System.Drawing.Size(18, 17);
            this.BStatus.TabIndex = 4;
            this.BStatus.Text = "\"\"";
            // 
            // ATStatusLabel
            // 
            this.ATStatusLabel.AutoSize = true;
            this.ATStatusLabel.Location = new System.Drawing.Point(27, 66);
            this.ATStatusLabel.Name = "ATStatusLabel";
            this.ATStatusLabel.Size = new System.Drawing.Size(113, 17);
            this.ATStatusLabel.TabIndex = 3;
            this.ATStatusLabel.Text = "Assistant Status : ";
            // 
            // BStatusLabel
            // 
            this.BStatusLabel.AutoSize = true;
            this.BStatusLabel.Location = new System.Drawing.Point(27, 31);
            this.BStatusLabel.Name = "BStatusLabel";
            this.BStatusLabel.Size = new System.Drawing.Size(116, 17);
            this.BStatusLabel.TabIndex = 2;
            this.BStatusLabel.Text = "BlueStack Status : ";
            // 
            // BSRunningTimer
            // 
            this.BSRunningTimer.Enabled = true;
            this.BSRunningTimer.Interval = 500;
            this.BSRunningTimer.Tick += new System.EventHandler(this.BSRunningTimer_Tick);
            // 
            // SimulateClickTimer
            // 
            this.SimulateClickTimer.Tick += new System.EventHandler(this.SimulateClickTimer_Tick);
            // 
            // AssistantInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 187);
            this.Controls.Add(this.InfoTable);
            this.Controls.Add(this.ToolMenu);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ToolMenu;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.Name = "AssistantInterface";
            this.Text = " Assistant Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoClick_FormClosing);
            this.ToolMenu.ResumeLayout(false);
            this.ToolMenu.PerformLayout();
            this.InfoTable.ResumeLayout(false);
            this.InfoTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ToolMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listHwndToolStripMenuItem;
        private System.Windows.Forms.GroupBox InfoTable;
        private System.Windows.Forms.Label BStatusLabel;
        private System.Windows.Forms.Label AStatus;
        private System.Windows.Forms.Label BStatus;
        private System.Windows.Forms.Label ATStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem summonerWarToolStripMenuItem;
        private System.Windows.Forms.Timer BSRunningTimer;
        private System.Windows.Forms.Timer CompareImageTimer;
        private System.Windows.Forms.Timer SimulateClickTimer;
        private System.Windows.Forms.ToolStripMenuItem imageReloadToolStripMenuItem;
    }
}

