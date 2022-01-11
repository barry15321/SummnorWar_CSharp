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
            this.label1 = new System.Windows.Forms.Label();
            this.movesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.index0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchImgindexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clickIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log_light = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ToolMenu.SuspendLayout();
            this.InfoTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolMenu
            // 
            this.ToolMenu.BackColor = System.Drawing.Color.Transparent;
            this.ToolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.movesToolStripMenuItem});
            this.ToolMenu.Location = new System.Drawing.Point(0, 0);
            this.ToolMenu.Name = "ToolMenu";
            this.ToolMenu.Size = new System.Drawing.Size(498, 24);
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.openToolStripMenuItem.Text = "Setting";
            // 
            // listHwndToolStripMenuItem
            // 
            this.listHwndToolStripMenuItem.Name = "listHwndToolStripMenuItem";
            this.listHwndToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.listHwndToolStripMenuItem.Text = "Window List";
            this.listHwndToolStripMenuItem.Click += new System.EventHandler(this.listHwndToolStripMenuItem_Click);
            // 
            // summonerWarToolStripMenuItem
            // 
            this.summonerWarToolStripMenuItem.Name = "summonerWarToolStripMenuItem";
            this.summonerWarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.summonerWarToolStripMenuItem.Text = "BlueStack load";
            this.summonerWarToolStripMenuItem.Click += new System.EventHandler(this.summonerWarToolStripMenuItem_Click);
            // 
            // imageReloadToolStripMenuItem
            // 
            this.imageReloadToolStripMenuItem.Name = "imageReloadToolStripMenuItem";
            this.imageReloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.imageReloadToolStripMenuItem.Text = "Image dir";
            this.imageReloadToolStripMenuItem.Click += new System.EventHandler(this.imageReloadToolStripMenuItem_Click);
            // 
            // InfoTable
            // 
            this.InfoTable.Controls.Add(this.label2);
            this.InfoTable.Controls.Add(this.label1);
            this.InfoTable.Controls.Add(this.AStatus);
            this.InfoTable.Controls.Add(this.BStatus);
            this.InfoTable.Controls.Add(this.ATStatusLabel);
            this.InfoTable.Controls.Add(this.BStatusLabel);
            this.InfoTable.Location = new System.Drawing.Point(11, 27);
            this.InfoTable.Name = "InfoTable";
            this.InfoTable.Size = new System.Drawing.Size(475, 148);
            this.InfoTable.TabIndex = 2;
            this.InfoTable.TabStop = false;
            this.InfoTable.Text = "Info";
            // 
            // AStatus
            // 
            this.AStatus.AutoSize = true;
            this.AStatus.Location = new System.Drawing.Point(149, 69);
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
            this.ATStatusLabel.Location = new System.Drawing.Point(27, 69);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log";
            // 
            // movesToolStripMenuItem
            // 
            this.movesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureScreenToolStripMenuItem,
            this.scriptStartToolStripMenuItem,
            this.scriptStopToolStripMenuItem,
            this.index0ToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchImgindexToolStripMenuItem,
            this.monitorLogToolStripMenuItem,
            this.clickIndexToolStripMenuItem});
            this.movesToolStripMenuItem.Name = "movesToolStripMenuItem";
            this.movesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.movesToolStripMenuItem.Text = "Moves";
            // 
            // captureScreenToolStripMenuItem
            // 
            this.captureScreenToolStripMenuItem.Name = "captureScreenToolStripMenuItem";
            this.captureScreenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.captureScreenToolStripMenuItem.Text = "Capture Screen";
            this.captureScreenToolStripMenuItem.Click += new System.EventHandler(this.captureScreenToolStripMenuItem_Click);
            // 
            // scriptStartToolStripMenuItem
            // 
            this.scriptStartToolStripMenuItem.Name = "scriptStartToolStripMenuItem";
            this.scriptStartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scriptStartToolStripMenuItem.Text = "Script Start";
            this.scriptStartToolStripMenuItem.Click += new System.EventHandler(this.scriptStartToolStripMenuItem_Click);
            // 
            // scriptStopToolStripMenuItem
            // 
            this.scriptStopToolStripMenuItem.Name = "scriptStopToolStripMenuItem";
            this.scriptStopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scriptStopToolStripMenuItem.Text = "Script Stop";
            this.scriptStopToolStripMenuItem.Click += new System.EventHandler(this.scriptStopToolStripMenuItem_Click);
            // 
            // index0ToolStripMenuItem
            // 
            this.index0ToolStripMenuItem.Name = "index0ToolStripMenuItem";
            this.index0ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.index0ToolStripMenuItem.Text = "Index 0";
            this.index0ToolStripMenuItem.Click += new System.EventHandler(this.index0ToolStripMenuItem_Click);
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.indexToolStripMenuItem.Text = "Index ++";
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.indexToolStripMenuItem_Click);
            // 
            // searchImgindexToolStripMenuItem
            // 
            this.searchImgindexToolStripMenuItem.Name = "searchImgindexToolStripMenuItem";
            this.searchImgindexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.searchImgindexToolStripMenuItem.Text = "Search Img[index]";
            this.searchImgindexToolStripMenuItem.Click += new System.EventHandler(this.searchImgindexToolStripMenuItem_Click);
            // 
            // monitorLogToolStripMenuItem
            // 
            this.monitorLogToolStripMenuItem.Name = "monitorLogToolStripMenuItem";
            this.monitorLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.monitorLogToolStripMenuItem.Text = "Monitor log";
            this.monitorLogToolStripMenuItem.Click += new System.EventHandler(this.monitorLogToolStripMenuItem_Click);
            // 
            // clickIndexToolStripMenuItem
            // 
            this.clickIndexToolStripMenuItem.Name = "clickIndexToolStripMenuItem";
            this.clickIndexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clickIndexToolStripMenuItem.Text = "Click[Index]";
            this.clickIndexToolStripMenuItem.Click += new System.EventHandler(this.clickIndexToolStripMenuItem_Click);
            // 
            // log_light
            // 
            this.log_light.Interval = 3000;
            this.log_light.Tick += new System.EventHandler(this.log_light_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Search_log";
            // 
            // AssistantInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 187);
            this.Controls.Add(this.InfoTable);
            this.Controls.Add(this.ToolMenu);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ToolMenu;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "AssistantInterface";
            this.Text = " Assistant";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem movesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem index0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchImgindexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clickIndexToolStripMenuItem;
        private System.Windows.Forms.Timer log_light;
        private System.Windows.Forms.Label label2;
    }
}

