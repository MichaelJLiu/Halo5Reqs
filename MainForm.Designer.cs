namespace Halo5Reqs
{
	partial class MainForm
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.packsTabPage = new System.Windows.Forms.TabPage();
			this.openPackButton = new System.Windows.Forms.Button();
			this.packListView = new System.Windows.Forms.ListView();
			this.reqImageList = new System.Windows.Forms.ImageList(this.components);
			this.packTypePictureBox = new System.Windows.Forms.PictureBox();
			this.packTreeView = new System.Windows.Forms.TreeView();
			this.reqsTabPage = new System.Windows.Forms.TabPage();
			this.reqListView = new System.Windows.Forms.ListView();
			this.reqNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.reqRarityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.reqTreeView = new System.Windows.Forms.TreeView();
			this.reqPanel = new System.Windows.Forms.Panel();
			this.sellButton = new System.Windows.Forms.Button();
			this.unconsumedLabel = new System.Windows.Forms.Label();
			this.priceLabel2 = new System.Windows.Forms.Label();
			this.unconsumedLabel2 = new System.Windows.Forms.Label();
			this.priceLabel = new System.Windows.Forms.Label();
			this.reqFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.reqNameLabel = new System.Windows.Forms.Label();
			this.reqDescriptionLabel = new System.Windows.Forms.Label();
			this.reqPictureBox = new System.Windows.Forms.PictureBox();
			this.buyPackButton = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.packsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.packTypePictureBox)).BeginInit();
			this.reqsTabPage.SuspendLayout();
			this.reqPanel.SuspendLayout();
			this.reqFlowLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.reqPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.packsTabPage);
			this.tabControl.Controls.Add(this.reqsTabPage);
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(864, 600);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// packsTabPage
			// 
			this.packsTabPage.Controls.Add(this.buyPackButton);
			this.packsTabPage.Controls.Add(this.openPackButton);
			this.packsTabPage.Controls.Add(this.packListView);
			this.packsTabPage.Controls.Add(this.packTypePictureBox);
			this.packsTabPage.Controls.Add(this.packTreeView);
			this.packsTabPage.Location = new System.Drawing.Point(4, 25);
			this.packsTabPage.Name = "packsTabPage";
			this.packsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.packsTabPage.Size = new System.Drawing.Size(856, 571);
			this.packsTabPage.TabIndex = 0;
			this.packsTabPage.Text = "Packs";
			this.packsTabPage.UseVisualStyleBackColor = true;
			// 
			// openPackButton
			// 
			this.openPackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.openPackButton.Location = new System.Drawing.Point(35, 523);
			this.openPackButton.Name = "openPackButton";
			this.openPackButton.Size = new System.Drawing.Size(187, 40);
			this.openPackButton.TabIndex = 9;
			this.openPackButton.Text = "&Open Req Pack";
			this.openPackButton.UseVisualStyleBackColor = true;
			this.openPackButton.Visible = false;
			this.openPackButton.Click += new System.EventHandler(this.openPackButton_Click);
			// 
			// packListView
			// 
			this.packListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.packListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.packListView.HideSelection = false;
			this.packListView.LargeImageList = this.reqImageList;
			this.packListView.Location = new System.Drawing.Point(253, 3);
			this.packListView.MultiSelect = false;
			this.packListView.Name = "packListView";
			this.packListView.Size = new System.Drawing.Size(600, 565);
			this.packListView.TabIndex = 1;
			this.packListView.UseCompatibleStateImageBehavior = false;
			this.packListView.SelectedIndexChanged += new System.EventHandler(this.packListView_SelectedIndexChanged);
			this.packListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packListView_KeyDown);
			// 
			// reqImageList
			// 
			this.reqImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.reqImageList.ImageSize = new System.Drawing.Size(94, 130);
			this.reqImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// packTypePictureBox
			// 
			this.packTypePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.packTypePictureBox.Location = new System.Drawing.Point(51, 315);
			this.packTypePictureBox.Name = "packTypePictureBox";
			this.packTypePictureBox.Size = new System.Drawing.Size(153, 200);
			this.packTypePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.packTypePictureBox.TabIndex = 2;
			this.packTypePictureBox.TabStop = false;
			// 
			// packTreeView
			// 
			this.packTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.packTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.packTreeView.HideSelection = false;
			this.packTreeView.Location = new System.Drawing.Point(3, 3);
			this.packTreeView.Name = "packTreeView";
			this.packTreeView.ShowRootLines = false;
			this.packTreeView.Size = new System.Drawing.Size(250, 304);
			this.packTreeView.TabIndex = 0;
			this.packTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.packTreeView_AfterSelect);
			// 
			// reqsTabPage
			// 
			this.reqsTabPage.Controls.Add(this.reqListView);
			this.reqsTabPage.Controls.Add(this.reqTreeView);
			this.reqsTabPage.Location = new System.Drawing.Point(4, 25);
			this.reqsTabPage.Name = "reqsTabPage";
			this.reqsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.reqsTabPage.Size = new System.Drawing.Size(856, 571);
			this.reqsTabPage.TabIndex = 1;
			this.reqsTabPage.Text = "Reqs";
			this.reqsTabPage.UseVisualStyleBackColor = true;
			// 
			// reqListView
			// 
			this.reqListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.reqListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.reqNameColumnHeader,
            this.reqRarityColumnHeader});
			this.reqListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.reqListView.HideSelection = false;
			this.reqListView.LargeImageList = this.reqImageList;
			this.reqListView.Location = new System.Drawing.Point(253, 3);
			this.reqListView.MultiSelect = false;
			this.reqListView.Name = "reqListView";
			this.reqListView.Size = new System.Drawing.Size(600, 565);
			this.reqListView.TabIndex = 1;
			this.reqListView.UseCompatibleStateImageBehavior = false;
			this.reqListView.SelectedIndexChanged += new System.EventHandler(this.reqListView_SelectedIndexChanged);
			this.reqListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.reqListView_KeyDown);
			// 
			// reqNameColumnHeader
			// 
			this.reqNameColumnHeader.Text = "Name";
			this.reqNameColumnHeader.Width = 200;
			// 
			// reqRarityColumnHeader
			// 
			this.reqRarityColumnHeader.Text = "Rarity";
			this.reqRarityColumnHeader.Width = 100;
			// 
			// reqTreeView
			// 
			this.reqTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.reqTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.reqTreeView.HideSelection = false;
			this.reqTreeView.Location = new System.Drawing.Point(3, 3);
			this.reqTreeView.Name = "reqTreeView";
			this.reqTreeView.ShowRootLines = false;
			this.reqTreeView.Size = new System.Drawing.Size(250, 565);
			this.reqTreeView.TabIndex = 0;
			this.reqTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.reqTreeView_AfterSelect);
			// 
			// reqPanel
			// 
			this.reqPanel.Controls.Add(this.sellButton);
			this.reqPanel.Controls.Add(this.unconsumedLabel);
			this.reqPanel.Controls.Add(this.priceLabel2);
			this.reqPanel.Controls.Add(this.unconsumedLabel2);
			this.reqPanel.Controls.Add(this.priceLabel);
			this.reqPanel.Controls.Add(this.reqFlowLayoutPanel);
			this.reqPanel.Controls.Add(this.reqPictureBox);
			this.reqPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.reqPanel.Location = new System.Drawing.Point(872, 0);
			this.reqPanel.Name = "reqPanel";
			this.reqPanel.Size = new System.Drawing.Size(203, 615);
			this.reqPanel.TabIndex = 1;
			this.reqPanel.Visible = false;
			// 
			// sellButton
			// 
			this.sellButton.Location = new System.Drawing.Point(8, 578);
			this.sellButton.Name = "sellButton";
			this.sellButton.Size = new System.Drawing.Size(187, 40);
			this.sellButton.TabIndex = 8;
			this.sellButton.Text = "&Sell 1 Card";
			this.sellButton.UseVisualStyleBackColor = true;
			this.sellButton.Click += new System.EventHandler(this.sellButton_Click);
			// 
			// unconsumedLabel
			// 
			this.unconsumedLabel.AutoSize = true;
			this.unconsumedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.unconsumedLabel.Location = new System.Drawing.Point(139, 528);
			this.unconsumedLabel.Name = "unconsumedLabel";
			this.unconsumedLabel.Size = new System.Drawing.Size(20, 24);
			this.unconsumedLabel.TabIndex = 6;
			this.unconsumedLabel.Text = "0";
			// 
			// priceLabel2
			// 
			this.priceLabel2.AutoSize = true;
			this.priceLabel2.Location = new System.Drawing.Point(9, 552);
			this.priceLabel2.Name = "priceLabel2";
			this.priceLabel2.Size = new System.Drawing.Size(93, 17);
			this.priceLabel2.TabIndex = 5;
			this.priceLabel2.Text = "REQ POINTS";
			// 
			// unconsumedLabel2
			// 
			this.unconsumedLabel2.AutoSize = true;
			this.unconsumedLabel2.Location = new System.Drawing.Point(140, 552);
			this.unconsumedLabel2.Name = "unconsumedLabel2";
			this.unconsumedLabel2.Size = new System.Drawing.Size(55, 17);
			this.unconsumedLabel2.TabIndex = 7;
			this.unconsumedLabel2.Text = "CARDS";
			// 
			// priceLabel
			// 
			this.priceLabel.AutoSize = true;
			this.priceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.priceLabel.Location = new System.Drawing.Point(8, 528);
			this.priceLabel.Name = "priceLabel";
			this.priceLabel.Size = new System.Drawing.Size(20, 24);
			this.priceLabel.TabIndex = 4;
			this.priceLabel.Text = "0";
			// 
			// reqFlowLayoutPanel
			// 
			this.reqFlowLayoutPanel.Controls.Add(this.reqNameLabel);
			this.reqFlowLayoutPanel.Controls.Add(this.reqDescriptionLabel);
			this.reqFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.reqFlowLayoutPanel.Location = new System.Drawing.Point(8, 277);
			this.reqFlowLayoutPanel.Name = "reqFlowLayoutPanel";
			this.reqFlowLayoutPanel.Size = new System.Drawing.Size(187, 240);
			this.reqFlowLayoutPanel.TabIndex = 2;
			this.reqFlowLayoutPanel.WrapContents = false;
			// 
			// reqNameLabel
			// 
			this.reqNameLabel.AutoSize = true;
			this.reqNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.reqNameLabel.Location = new System.Drawing.Point(0, 0);
			this.reqNameLabel.Margin = new System.Windows.Forms.Padding(0);
			this.reqNameLabel.Name = "reqNameLabel";
			this.reqNameLabel.Size = new System.Drawing.Size(65, 24);
			this.reqNameLabel.TabIndex = 2;
			this.reqNameLabel.Text = "Name";
			// 
			// reqDescriptionLabel
			// 
			this.reqDescriptionLabel.AutoSize = true;
			this.reqDescriptionLabel.Location = new System.Drawing.Point(0, 24);
			this.reqDescriptionLabel.Margin = new System.Windows.Forms.Padding(0);
			this.reqDescriptionLabel.Name = "reqDescriptionLabel";
			this.reqDescriptionLabel.Size = new System.Drawing.Size(79, 17);
			this.reqDescriptionLabel.TabIndex = 3;
			this.reqDescriptionLabel.Text = "Description";
			// 
			// reqPictureBox
			// 
			this.reqPictureBox.Location = new System.Drawing.Point(8, 10);
			this.reqPictureBox.Name = "reqPictureBox";
			this.reqPictureBox.Size = new System.Drawing.Size(187, 259);
			this.reqPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.reqPictureBox.TabIndex = 1;
			this.reqPictureBox.TabStop = false;
			// 
			// buyPackButton
			// 
			this.buyPackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buyPackButton.Location = new System.Drawing.Point(35, 523);
			this.buyPackButton.Name = "buyPackButton";
			this.buyPackButton.Size = new System.Drawing.Size(187, 40);
			this.buyPackButton.TabIndex = 10;
			this.buyPackButton.Text = "&Buy Req Pack";
			this.buyPackButton.UseVisualStyleBackColor = true;
			this.buyPackButton.Visible = false;
			this.buyPackButton.Click += new System.EventHandler(this.buyPackButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1075, 615);
			this.Controls.Add(this.reqPanel);
			this.Controls.Add(this.tabControl);
			this.Name = "MainForm";
			this.Text = "Halo 5 Requisitions - {0}";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControl.ResumeLayout(false);
			this.packsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.packTypePictureBox)).EndInit();
			this.reqsTabPage.ResumeLayout(false);
			this.reqPanel.ResumeLayout(false);
			this.reqPanel.PerformLayout();
			this.reqFlowLayoutPanel.ResumeLayout(false);
			this.reqFlowLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.reqPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage packsTabPage;
		private System.Windows.Forms.TreeView packTreeView;
		private System.Windows.Forms.ListView packListView;
		private System.Windows.Forms.TabPage reqsTabPage;
		private System.Windows.Forms.TreeView reqTreeView;
		private System.Windows.Forms.ListView reqListView;
		private System.Windows.Forms.ImageList reqImageList;
		private System.Windows.Forms.ColumnHeader reqNameColumnHeader;
		private System.Windows.Forms.ColumnHeader reqRarityColumnHeader;
		private System.Windows.Forms.Panel reqPanel;
		private System.Windows.Forms.PictureBox reqPictureBox;
		private System.Windows.Forms.FlowLayoutPanel reqFlowLayoutPanel;
		private System.Windows.Forms.Label reqNameLabel;
		private System.Windows.Forms.Label reqDescriptionLabel;
		private System.Windows.Forms.Label priceLabel;
		private System.Windows.Forms.Label priceLabel2;
		private System.Windows.Forms.Label unconsumedLabel;
		private System.Windows.Forms.Label unconsumedLabel2;
		private System.Windows.Forms.Button sellButton;
		private System.Windows.Forms.PictureBox packTypePictureBox;
		private System.Windows.Forms.Button openPackButton;
		private System.Windows.Forms.Button buyPackButton;
	}
}

