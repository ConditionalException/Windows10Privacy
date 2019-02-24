namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Tools = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUninstOneDrive = new System.Windows.Forms.Button();
            this.btnUnPhotoV = new System.Windows.Forms.Button();
            this.btnUninstEdge = new System.Windows.Forms.Button();
            this.btnQuickAccessDis = new System.Windows.Forms.Button();
            this.btnResPhotoV = new System.Windows.Forms.Button();
            this.btnQuickAccessEn = new System.Windows.Forms.Button();
            this.btnResStart = new System.Windows.Forms.Button();
            this.btnUnPin = new System.Windows.Forms.Button();
            this.Apps = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.ShowHiddenApps = new System.Windows.Forms.CheckBox();
            this.lblInstalledCount = new System.Windows.Forms.Label();
            this.lblRemoveCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMvAllLeft = new System.Windows.Forms.Button();
            this.btnMvAllRight = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClearAppsRem = new System.Windows.Forms.Button();
            this.btnMvLeft = new System.Windows.Forms.Button();
            this.btnMvRight = new System.Windows.Forms.Button();
            this.AppsToRemove = new System.Windows.Forms.ListBox();
            this.ListOfAppsInstalled = new System.Windows.Forms.ListBox();
            this.Privacy = new System.Windows.Forms.TabPage();
            this.btnRefreshPriv = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListBox4 = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBox3 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.Start = new System.Windows.Forms.TabPage();
            this.gitURL = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMinimal = new System.Windows.Forms.Button();
            this.btnAutoSet = new System.Windows.Forms.Button();
            this.btnRecommend = new System.Windows.Forms.Button();
            this.btnRemAll = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ProvisionedApps = new System.Windows.Forms.TabPage();
            this.btnProvInfo = new System.Windows.Forms.Button();
            this.ProvAdminWarn = new System.Windows.Forms.Label();
            this.PShowHiddenApps = new System.Windows.Forms.CheckBox();
            this.lblPInstalledCount = new System.Windows.Forms.Label();
            this.lblPRemoveCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPMvAllLeft = new System.Windows.Forms.Button();
            this.btnPMvAllRight = new System.Windows.Forms.Button();
            this.btnPUninstall = new System.Windows.Forms.Button();
            this.btnPRefresh = new System.Windows.Forms.Button();
            this.btnPClearAppsRem = new System.Windows.Forms.Button();
            this.btnPMvLeft = new System.Windows.Forms.Button();
            this.btnPMvRight = new System.Windows.Forms.Button();
            this.PAppsToRemove = new System.Windows.Forms.ListBox();
            this.PListOfAppsInstalled = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Tools.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Apps.SuspendLayout();
            this.Privacy.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Start.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ProvisionedApps.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tools
            // 
            this.Tools.Controls.Add(this.panel2);
            this.Tools.Location = new System.Drawing.Point(4, 22);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(792, 424);
            this.Tools.TabIndex = 2;
            this.Tools.Text = "Tools";
            this.Tools.UseVisualStyleBackColor = true;
            this.Tools.Enter += new System.EventHandler(this.Tools_Enter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUninstOneDrive);
            this.panel2.Controls.Add(this.btnUnPhotoV);
            this.panel2.Controls.Add(this.btnUninstEdge);
            this.panel2.Controls.Add(this.btnQuickAccessDis);
            this.panel2.Controls.Add(this.btnResPhotoV);
            this.panel2.Controls.Add(this.btnQuickAccessEn);
            this.panel2.Controls.Add(this.btnResStart);
            this.panel2.Controls.Add(this.btnUnPin);
            this.panel2.Location = new System.Drawing.Point(153, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(494, 110);
            this.panel2.TabIndex = 8;
            // 
            // btnUninstOneDrive
            // 
            this.btnUninstOneDrive.Location = new System.Drawing.Point(0, 0);
            this.btnUninstOneDrive.Name = "btnUninstOneDrive";
            this.btnUninstOneDrive.Size = new System.Drawing.Size(119, 52);
            this.btnUninstOneDrive.TabIndex = 0;
            this.btnUninstOneDrive.Text = "Uninstall OneDrive";
            this.toolTip1.SetToolTip(this.btnUninstOneDrive, "Uninstalls OneDrive");
            this.btnUninstOneDrive.UseVisualStyleBackColor = true;
            this.btnUninstOneDrive.Click += new System.EventHandler(this.btnUninstOneDrive_Click);
            // 
            // btnUnPhotoV
            // 
            this.btnUnPhotoV.Location = new System.Drawing.Point(125, 58);
            this.btnUnPhotoV.Name = "btnUnPhotoV";
            this.btnUnPhotoV.Size = new System.Drawing.Size(119, 52);
            this.btnUnPhotoV.TabIndex = 7;
            this.btnUnPhotoV.Text = "Uninstall \"Windows Photo Viewer\"";
            this.toolTip1.SetToolTip(this.btnUnPhotoV, "Restores the \"Windows Photo Viewer\" from Windows 7 using the method provided at\r\n" +
        "https://www.tenforums.com/tutorials/14312-restore-windows-photo-viewer-windows-1" +
        "0-a.html ");
            this.btnUnPhotoV.UseVisualStyleBackColor = true;
            this.btnUnPhotoV.Click += new System.EventHandler(this.btnUnPhotoV_Click);
            // 
            // btnUninstEdge
            // 
            this.btnUninstEdge.BackColor = System.Drawing.Color.Wheat;
            this.btnUninstEdge.Location = new System.Drawing.Point(0, 58);
            this.btnUninstEdge.Name = "btnUninstEdge";
            this.btnUninstEdge.Size = new System.Drawing.Size(119, 52);
            this.btnUninstEdge.TabIndex = 3;
            this.btnUninstEdge.Text = "Disable \r\nEdge Browser \r\n(not recommended)\r\n";
            this.btnUninstEdge.UseVisualStyleBackColor = false;
            this.btnUninstEdge.Click += new System.EventHandler(this.btnUninstEdge_Click);
            // 
            // btnQuickAccessDis
            // 
            this.btnQuickAccessDis.Location = new System.Drawing.Point(375, 58);
            this.btnQuickAccessDis.Name = "btnQuickAccessDis";
            this.btnQuickAccessDis.Size = new System.Drawing.Size(119, 52);
            this.btnQuickAccessDis.TabIndex = 6;
            this.btnQuickAccessDis.Text = "Set Explorer Home\r\n to \"Quick Access\"";
            this.btnQuickAccessDis.UseVisualStyleBackColor = true;
            this.btnQuickAccessDis.Click += new System.EventHandler(this.btnQuickAccessDis_Click);
            // 
            // btnResPhotoV
            // 
            this.btnResPhotoV.Location = new System.Drawing.Point(125, 0);
            this.btnResPhotoV.Name = "btnResPhotoV";
            this.btnResPhotoV.Size = new System.Drawing.Size(119, 52);
            this.btnResPhotoV.TabIndex = 4;
            this.btnResPhotoV.Text = "Restore \"Windows Photo Viewer\"";
            this.toolTip1.SetToolTip(this.btnResPhotoV, "Restores the \"Windows Photo Viewer\" from Windows 7 using the method provided at\r\n" +
        "https://www.tenforums.com/tutorials/14312-restore-windows-photo-viewer-windows-1" +
        "0-a.html ");
            this.btnResPhotoV.UseVisualStyleBackColor = true;
            this.btnResPhotoV.Click += new System.EventHandler(this.btnResPhotoV_Click);
            // 
            // btnQuickAccessEn
            // 
            this.btnQuickAccessEn.Location = new System.Drawing.Point(375, 0);
            this.btnQuickAccessEn.Name = "btnQuickAccessEn";
            this.btnQuickAccessEn.Size = new System.Drawing.Size(119, 52);
            this.btnQuickAccessEn.TabIndex = 5;
            this.btnQuickAccessEn.Text = "Set Explorer Home\r\nto \"This PC\"\r\n";
            this.btnQuickAccessEn.UseVisualStyleBackColor = true;
            this.btnQuickAccessEn.Click += new System.EventHandler(this.btnQuickAccessEn_Click);
            // 
            // btnResStart
            // 
            this.btnResStart.Location = new System.Drawing.Point(250, 0);
            this.btnResStart.Name = "btnResStart";
            this.btnResStart.Size = new System.Drawing.Size(119, 52);
            this.btnResStart.TabIndex = 1;
            this.btnResStart.Text = "Reset Start Menu to Default";
            this.btnResStart.UseVisualStyleBackColor = true;
            this.btnResStart.Click += new System.EventHandler(this.btnResStart_Click);
            // 
            // btnUnPin
            // 
            this.btnUnPin.Location = new System.Drawing.Point(250, 58);
            this.btnUnPin.Name = "btnUnPin";
            this.btnUnPin.Size = new System.Drawing.Size(119, 52);
            this.btnUnPin.TabIndex = 2;
            this.btnUnPin.Text = "Unpin all Start Menu Apps";
            this.btnUnPin.UseVisualStyleBackColor = true;
            this.btnUnPin.Click += new System.EventHandler(this.btnUnPin_Click);
            // 
            // Apps
            // 
            this.Apps.Controls.Add(this.label9);
            this.Apps.Controls.Add(this.ShowHiddenApps);
            this.Apps.Controls.Add(this.lblInstalledCount);
            this.Apps.Controls.Add(this.lblRemoveCount);
            this.Apps.Controls.Add(this.label6);
            this.Apps.Controls.Add(this.btnMvAllLeft);
            this.Apps.Controls.Add(this.btnMvAllRight);
            this.Apps.Controls.Add(this.btnUninstall);
            this.Apps.Controls.Add(this.btnRefresh);
            this.Apps.Controls.Add(this.btnClearAppsRem);
            this.Apps.Controls.Add(this.btnMvLeft);
            this.Apps.Controls.Add(this.btnMvRight);
            this.Apps.Controls.Add(this.AppsToRemove);
            this.Apps.Controls.Add(this.ListOfAppsInstalled);
            this.Apps.Location = new System.Drawing.Point(4, 22);
            this.Apps.Name = "Apps";
            this.Apps.Padding = new System.Windows.Forms.Padding(3);
            this.Apps.Size = new System.Drawing.Size(792, 424);
            this.Apps.TabIndex = 1;
            this.Apps.Text = "Uninstall Apps";
            this.Apps.UseVisualStyleBackColor = true;
            this.Apps.Enter += new System.EventHandler(this.Apps_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(533, 408);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(217, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Most apps can be reinstalled using the store.";
            // 
            // ShowHiddenApps
            // 
            this.ShowHiddenApps.AutoSize = true;
            this.ShowHiddenApps.BackColor = System.Drawing.Color.LightGray;
            this.ShowHiddenApps.Location = new System.Drawing.Point(40, 398);
            this.ShowHiddenApps.Name = "ShowHiddenApps";
            this.ShowHiddenApps.Size = new System.Drawing.Size(284, 17);
            this.ShowHiddenApps.TabIndex = 13;
            this.ShowHiddenApps.Text = "Show hidden apps (CAN CAUSE SERIOUS DAMAGE)";
            this.ShowHiddenApps.UseVisualStyleBackColor = false;
            this.ShowHiddenApps.CheckedChanged += new System.EventHandler(this.ShowHiddenApps_CheckedChanged);
            // 
            // lblInstalledCount
            // 
            this.lblInstalledCount.AutoSize = true;
            this.lblInstalledCount.Location = new System.Drawing.Point(40, 18);
            this.lblInstalledCount.Name = "lblInstalledCount";
            this.lblInstalledCount.Size = new System.Drawing.Size(88, 13);
            this.lblInstalledCount.TabIndex = 12;
            this.lblInstalledCount.Text = "Apps to keep [0] ";
            // 
            // lblRemoveCount
            // 
            this.lblRemoveCount.AutoSize = true;
            this.lblRemoveCount.Location = new System.Drawing.Point(521, 15);
            this.lblRemoveCount.Name = "lblRemoveCount";
            this.lblRemoveCount.Size = new System.Drawing.Size(99, 13);
            this.lblRemoveCount.TabIndex = 11;
            this.lblRemoveCount.Text = "Apps to remove [0] ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Firebrick;
            this.label6.Location = new System.Drawing.Point(554, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warning: App Uninstall is permanent\r\n";
            // 
            // btnMvAllLeft
            // 
            this.btnMvAllLeft.Enabled = false;
            this.btnMvAllLeft.Location = new System.Drawing.Point(313, 239);
            this.btnMvAllLeft.Name = "btnMvAllLeft";
            this.btnMvAllLeft.Size = new System.Drawing.Size(175, 23);
            this.btnMvAllLeft.TabIndex = 8;
            this.btnMvAllLeft.Text = "< -- Move All < --";
            this.btnMvAllLeft.UseVisualStyleBackColor = true;
            this.btnMvAllLeft.Click += new System.EventHandler(this.btnMvAllLeft_Click);
            // 
            // btnMvAllRight
            // 
            this.btnMvAllRight.Enabled = false;
            this.btnMvAllRight.Location = new System.Drawing.Point(313, 98);
            this.btnMvAllRight.Name = "btnMvAllRight";
            this.btnMvAllRight.Size = new System.Drawing.Size(175, 23);
            this.btnMvAllRight.TabIndex = 7;
            this.btnMvAllRight.Text = "-- > Move All -- >";
            this.btnMvAllRight.UseVisualStyleBackColor = true;
            this.btnMvAllRight.Click += new System.EventHandler(this.btnMvAllRight_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.Enabled = false;
            this.btnUninstall.Location = new System.Drawing.Point(521, 369);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(164, 23);
            this.btnUninstall.TabIndex = 6;
            this.btnUninstall.Text = "Uninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(125, 369);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClearAppsRem
            // 
            this.btnClearAppsRem.Enabled = false;
            this.btnClearAppsRem.Location = new System.Drawing.Point(691, 369);
            this.btnClearAppsRem.Name = "btnClearAppsRem";
            this.btnClearAppsRem.Size = new System.Drawing.Size(75, 23);
            this.btnClearAppsRem.TabIndex = 4;
            this.btnClearAppsRem.Text = "Clear List";
            this.btnClearAppsRem.UseVisualStyleBackColor = true;
            this.btnClearAppsRem.Click += new System.EventHandler(this.btnClearAppsRem_Click);
            // 
            // btnMvLeft
            // 
            this.btnMvLeft.Enabled = false;
            this.btnMvLeft.Location = new System.Drawing.Point(313, 181);
            this.btnMvLeft.Name = "btnMvLeft";
            this.btnMvLeft.Size = new System.Drawing.Size(175, 23);
            this.btnMvLeft.TabIndex = 3;
            this.btnMvLeft.Text = "< --";
            this.btnMvLeft.UseVisualStyleBackColor = true;
            this.btnMvLeft.Click += new System.EventHandler(this.btnMvLeft_Click);
            // 
            // btnMvRight
            // 
            this.btnMvRight.Enabled = false;
            this.btnMvRight.Location = new System.Drawing.Point(313, 152);
            this.btnMvRight.Name = "btnMvRight";
            this.btnMvRight.Size = new System.Drawing.Size(175, 23);
            this.btnMvRight.TabIndex = 2;
            this.btnMvRight.Text = "-- >";
            this.btnMvRight.UseVisualStyleBackColor = true;
            this.btnMvRight.Click += new System.EventHandler(this.btnMvRight_Click);
            // 
            // AppsToRemove
            // 
            this.AppsToRemove.FormattingEnabled = true;
            this.AppsToRemove.Location = new System.Drawing.Point(521, 34);
            this.AppsToRemove.Name = "AppsToRemove";
            this.AppsToRemove.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.AppsToRemove.Size = new System.Drawing.Size(245, 329);
            this.AppsToRemove.Sorted = true;
            this.AppsToRemove.TabIndex = 1;
            // 
            // ListOfAppsInstalled
            // 
            this.ListOfAppsInstalled.FormattingEnabled = true;
            this.ListOfAppsInstalled.Location = new System.Drawing.Point(40, 34);
            this.ListOfAppsInstalled.Name = "ListOfAppsInstalled";
            this.ListOfAppsInstalled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListOfAppsInstalled.Size = new System.Drawing.Size(245, 329);
            this.ListOfAppsInstalled.Sorted = true;
            this.ListOfAppsInstalled.TabIndex = 0;
            // 
            // Privacy
            // 
            this.Privacy.Controls.Add(this.btnRefreshPriv);
            this.Privacy.Controls.Add(this.label10);
            this.Privacy.Controls.Add(this.panel1);
            this.Privacy.Controls.Add(this.btnApplyChanges);
            this.Privacy.Controls.Add(this.label4);
            this.Privacy.Controls.Add(this.checkedListBox4);
            this.Privacy.Controls.Add(this.groupBox1);
            this.Privacy.Controls.Add(this.label3);
            this.Privacy.Controls.Add(this.label2);
            this.Privacy.Controls.Add(this.checkedListBox3);
            this.Privacy.Controls.Add(this.checkedListBox2);
            this.Privacy.Controls.Add(this.label1);
            this.Privacy.Controls.Add(this.checkedListBox1);
            this.Privacy.Location = new System.Drawing.Point(4, 22);
            this.Privacy.Name = "Privacy";
            this.Privacy.Padding = new System.Windows.Forms.Padding(3);
            this.Privacy.Size = new System.Drawing.Size(792, 424);
            this.Privacy.TabIndex = 0;
            this.Privacy.Text = "Privacy Settings";
            this.Privacy.UseVisualStyleBackColor = true;
            this.Privacy.Enter += new System.EventHandler(this.Privacy_Enter);
            // 
            // btnRefreshPriv
            // 
            this.btnRefreshPriv.Location = new System.Drawing.Point(313, 23);
            this.btnRefreshPriv.Name = "btnRefreshPriv";
            this.btnRefreshPriv.Size = new System.Drawing.Size(174, 23);
            this.btnRefreshPriv.TabIndex = 13;
            this.btnRefreshPriv.Text = "Get Current Settings from OS";
            this.btnRefreshPriv.UseVisualStyleBackColor = true;
            this.btnRefreshPriv.Click += new System.EventHandler(this.btnRefreshPriv_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(290, 344);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(221, 26);
            this.label10.TabIndex = 12;
            this.label10.Text = "1) Uncheck items to disable them.\r\n2) \"Apply Changes\" to save your permissions.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Location = new System.Drawing.Point(680, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 58);
            this.panel1.TabIndex = 11;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(3, 4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(100, 50);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "Deselect All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Location = new System.Drawing.Point(313, 375);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(174, 23);
            this.btnApplyChanges.TabIndex = 8;
            this.btnApplyChanges.Text = "Apply Changes";
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Start Menu";
            // 
            // checkedListBox4
            // 
            this.checkedListBox4.CheckOnClick = true;
            this.checkedListBox4.FormattingEnabled = true;
            this.checkedListBox4.Items.AddRange(new object[] {
            "Cortana Enabled",
            "Safe Search On",
            "Internet Search",
            "Device History"});
            this.checkedListBox4.Location = new System.Drawing.Point(322, 266);
            this.checkedListBox4.Name = "checkedListBox4";
            this.checkedListBox4.Size = new System.Drawing.Size(156, 64);
            this.checkedListBox4.TabIndex = 6;
            this.checkedListBox4.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectButtonText);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(606, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 136);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feedback / Diag";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 115);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(113, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Ask For Feedback";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.SelectButtonText);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(153, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Telemetry Service Enabled";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Tailored Experiences";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.SelectButtonText);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Location = new System.Drawing.Point(6, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(121, 47);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Telemetry Report";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Basic";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(63, 24);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Full";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Location = new System.Drawing.Point(30, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "App Access Control";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Notifications";
            // 
            // checkedListBox3
            // 
            this.checkedListBox3.CheckOnClick = true;
            this.checkedListBox3.FormattingEnabled = true;
            this.checkedListBox3.Items.AddRange(new object[] {
            "Camera",
            "Microphone",
            "Location Service",
            "Radios",
            "Notifications",
            "Inking & Typing Personalization",
            "Account info",
            "Contacts",
            "Calendar",
            "Call History",
            "Email",
            "Tasks",
            "Messaging (SMS)",
            "Bluetooth Syncing",
            "App Diagnostic Information",
            "Background Running"});
            this.checkedListBox3.Location = new System.Drawing.Point(30, 86);
            this.checkedListBox3.Name = "checkedListBox3";
            this.checkedListBox3.Size = new System.Drawing.Size(178, 244);
            this.checkedListBox3.TabIndex = 2;
            this.checkedListBox3.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectButtonText);
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.CheckOnClick = true;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "Tips and Tricks",
            "What\'s New",
            "Notifications on Lockscreen"});
            this.checkedListBox2.Location = new System.Drawing.Point(322, 187);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(157, 49);
            this.checkedListBox2.TabIndex = 2;
            this.checkedListBox2.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectButtonText);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "General";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Advertising ID",
            "Locally relevant content",
            "Suggested content in start",
            "Track app launches"});
            this.checkedListBox1.Location = new System.Drawing.Point(322, 86);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(156, 64);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SelectButtonText);
            // 
            // Start
            // 
            this.Start.Controls.Add(this.gitURL);
            this.Start.Controls.Add(this.label7);
            this.Start.Controls.Add(this.label5);
            this.Start.Controls.Add(this.groupBox3);
            this.Start.Controls.Add(this.btnSaveConfig);
            this.Start.Controls.Add(this.btnLoadConfig);
            this.Start.Location = new System.Drawing.Point(4, 22);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(792, 424);
            this.Start.TabIndex = 5;
            this.Start.Text = "Main Menu";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // gitURL
            // 
            this.gitURL.AutoSize = true;
            this.gitURL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.gitURL.Location = new System.Drawing.Point(250, 394);
            this.gitURL.Name = "gitURL";
            this.gitURL.Size = new System.Drawing.Size(300, 13);
            this.gitURL.TabIndex = 8;
            this.gitURL.TabStop = true;
            this.gitURL.Text = "https://github.com/ConditionalException/Windows10Privacy/";
            this.gitURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.gitURL_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Firebrick;
            this.label7.Location = new System.Drawing.Point(205, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(390, 32);
            this.label7.TabIndex = 7;
            this.label7.Text = "WARNING: This app could damage Windows 10 beyond repair. \r\n                      " +
    "                      Use at your own risk.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(493, 65);
            this.label5.TabIndex = 6;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMinimal);
            this.groupBox3.Controls.Add(this.btnAutoSet);
            this.groupBox3.Controls.Add(this.btnRecommend);
            this.groupBox3.Controls.Add(this.btnRemAll);
            this.groupBox3.Location = new System.Drawing.Point(337, 86);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(126, 259);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Built-in Configs";
            // 
            // btnMinimal
            // 
            this.btnMinimal.Location = new System.Drawing.Point(6, 79);
            this.btnMinimal.Name = "btnMinimal";
            this.btnMinimal.Size = new System.Drawing.Size(114, 54);
            this.btnMinimal.TabIndex = 3;
            this.btnMinimal.Text = "Keep Minimal";
            this.btnMinimal.UseVisualStyleBackColor = true;
            this.btnMinimal.Click += new System.EventHandler(this.btnMinimal_Click);
            // 
            // btnAutoSet
            // 
            this.btnAutoSet.Location = new System.Drawing.Point(6, 199);
            this.btnAutoSet.Name = "btnAutoSet";
            this.btnAutoSet.Size = new System.Drawing.Size(114, 54);
            this.btnAutoSet.TabIndex = 2;
            this.btnAutoSet.Text = "GO";
            this.btnAutoSet.UseVisualStyleBackColor = true;
            this.btnAutoSet.Click += new System.EventHandler(this.btnAutoSet_Click);
            // 
            // btnRecommend
            // 
            this.btnRecommend.Location = new System.Drawing.Point(6, 19);
            this.btnRecommend.Name = "btnRecommend";
            this.btnRecommend.Size = new System.Drawing.Size(114, 54);
            this.btnRecommend.TabIndex = 0;
            this.btnRecommend.Text = "Recommended Settings";
            this.btnRecommend.UseVisualStyleBackColor = true;
            this.btnRecommend.Click += new System.EventHandler(this.btnRecommend_Click);
            // 
            // btnRemAll
            // 
            this.btnRemAll.Location = new System.Drawing.Point(6, 139);
            this.btnRemAll.Name = "btnRemAll";
            this.btnRemAll.Size = new System.Drawing.Size(114, 54);
            this.btnRemAll.TabIndex = 1;
            this.btnRemAll.Text = "Remove All\r\n(Not Recommended)";
            this.btnRemAll.UseVisualStyleBackColor = true;
            this.btnRemAll.Click += new System.EventHandler(this.btnRemAll_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(683, 15);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(100, 23);
            this.btnSaveConfig.TabIndex = 4;
            this.btnSaveConfig.Text = "Save Config File";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Location = new System.Drawing.Point(683, 44);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(100, 23);
            this.btnLoadConfig.TabIndex = 3;
            this.btnLoadConfig.Text = "Load Config File";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Start);
            this.tabControl1.Controls.Add(this.Privacy);
            this.tabControl1.Controls.Add(this.Apps);
            this.tabControl1.Controls.Add(this.ProvisionedApps);
            this.tabControl1.Controls.Add(this.Tools);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // ProvisionedApps
            // 
            this.ProvisionedApps.BackColor = System.Drawing.Color.Transparent;
            this.ProvisionedApps.Controls.Add(this.btnProvInfo);
            this.ProvisionedApps.Controls.Add(this.ProvAdminWarn);
            this.ProvisionedApps.Controls.Add(this.PShowHiddenApps);
            this.ProvisionedApps.Controls.Add(this.lblPInstalledCount);
            this.ProvisionedApps.Controls.Add(this.lblPRemoveCount);
            this.ProvisionedApps.Controls.Add(this.label8);
            this.ProvisionedApps.Controls.Add(this.btnPMvAllLeft);
            this.ProvisionedApps.Controls.Add(this.btnPMvAllRight);
            this.ProvisionedApps.Controls.Add(this.btnPUninstall);
            this.ProvisionedApps.Controls.Add(this.btnPRefresh);
            this.ProvisionedApps.Controls.Add(this.btnPClearAppsRem);
            this.ProvisionedApps.Controls.Add(this.btnPMvLeft);
            this.ProvisionedApps.Controls.Add(this.btnPMvRight);
            this.ProvisionedApps.Controls.Add(this.PAppsToRemove);
            this.ProvisionedApps.Controls.Add(this.PListOfAppsInstalled);
            this.ProvisionedApps.Location = new System.Drawing.Point(4, 22);
            this.ProvisionedApps.Name = "ProvisionedApps";
            this.ProvisionedApps.Padding = new System.Windows.Forms.Padding(3);
            this.ProvisionedApps.Size = new System.Drawing.Size(792, 424);
            this.ProvisionedApps.TabIndex = 7;
            this.ProvisionedApps.Text = "Provisioned Apps";
            this.ProvisionedApps.Enter += new System.EventHandler(this.ProvisionedApps_Enter);
            // 
            // btnProvInfo
            // 
            this.btnProvInfo.BackColor = System.Drawing.Color.Thistle;
            this.btnProvInfo.Location = new System.Drawing.Point(313, 18);
            this.btnProvInfo.Name = "btnProvInfo";
            this.btnProvInfo.Size = new System.Drawing.Size(175, 42);
            this.btnProvInfo.TabIndex = 16;
            this.btnProvInfo.Text = "What are provisioned apps?";
            this.btnProvInfo.UseVisualStyleBackColor = false;
            this.btnProvInfo.Click += new System.EventHandler(this.btnProvInfo_Click);
            // 
            // ProvAdminWarn
            // 
            this.ProvAdminWarn.AutoSize = true;
            this.ProvAdminWarn.BackColor = System.Drawing.Color.Transparent;
            this.ProvAdminWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProvAdminWarn.Location = new System.Drawing.Point(197, 173);
            this.ProvAdminWarn.Name = "ProvAdminWarn";
            this.ProvAdminWarn.Size = new System.Drawing.Size(407, 62);
            this.ProvAdminWarn.TabIndex = 15;
            this.ProvAdminWarn.Text = "Please restart the application as \r\nadministrator to use this tab";
            this.ProvAdminWarn.Visible = false;
            // 
            // PShowHiddenApps
            // 
            this.PShowHiddenApps.AutoSize = true;
            this.PShowHiddenApps.BackColor = System.Drawing.Color.LightGray;
            this.PShowHiddenApps.Location = new System.Drawing.Point(40, 398);
            this.PShowHiddenApps.Name = "PShowHiddenApps";
            this.PShowHiddenApps.Size = new System.Drawing.Size(284, 17);
            this.PShowHiddenApps.TabIndex = 13;
            this.PShowHiddenApps.Text = "Show hidden apps (CAN CAUSE SERIOUS DAMAGE)";
            this.PShowHiddenApps.UseVisualStyleBackColor = false;
            this.PShowHiddenApps.CheckedChanged += new System.EventHandler(this.PShowHiddenApps_CheckedChanged);
            // 
            // lblPInstalledCount
            // 
            this.lblPInstalledCount.AutoSize = true;
            this.lblPInstalledCount.Location = new System.Drawing.Point(40, 18);
            this.lblPInstalledCount.Name = "lblPInstalledCount";
            this.lblPInstalledCount.Size = new System.Drawing.Size(88, 13);
            this.lblPInstalledCount.TabIndex = 12;
            this.lblPInstalledCount.Text = "Apps to keep [0] ";
            // 
            // lblPRemoveCount
            // 
            this.lblPRemoveCount.AutoSize = true;
            this.lblPRemoveCount.Location = new System.Drawing.Point(521, 15);
            this.lblPRemoveCount.Name = "lblPRemoveCount";
            this.lblPRemoveCount.Size = new System.Drawing.Size(99, 13);
            this.lblPRemoveCount.TabIndex = 11;
            this.lblPRemoveCount.Text = "Apps to remove [0] ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Firebrick;
            this.label8.Location = new System.Drawing.Point(554, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Warning: App Uninstall is permanent";
            // 
            // btnPMvAllLeft
            // 
            this.btnPMvAllLeft.Location = new System.Drawing.Point(313, 239);
            this.btnPMvAllLeft.Name = "btnPMvAllLeft";
            this.btnPMvAllLeft.Size = new System.Drawing.Size(175, 23);
            this.btnPMvAllLeft.TabIndex = 8;
            this.btnPMvAllLeft.Text = "< -- Move All < --";
            this.btnPMvAllLeft.UseVisualStyleBackColor = true;
            this.btnPMvAllLeft.Click += new System.EventHandler(this.btnPMvAllLeft_Click);
            // 
            // btnPMvAllRight
            // 
            this.btnPMvAllRight.Location = new System.Drawing.Point(313, 98);
            this.btnPMvAllRight.Name = "btnPMvAllRight";
            this.btnPMvAllRight.Size = new System.Drawing.Size(175, 23);
            this.btnPMvAllRight.TabIndex = 7;
            this.btnPMvAllRight.Text = "-- > Move All -- >";
            this.btnPMvAllRight.UseVisualStyleBackColor = true;
            this.btnPMvAllRight.Click += new System.EventHandler(this.btnPMvAllRight_Click);
            // 
            // btnPUninstall
            // 
            this.btnPUninstall.Location = new System.Drawing.Point(521, 369);
            this.btnPUninstall.Name = "btnPUninstall";
            this.btnPUninstall.Size = new System.Drawing.Size(164, 23);
            this.btnPUninstall.TabIndex = 6;
            this.btnPUninstall.Text = "Uninstall";
            this.btnPUninstall.UseVisualStyleBackColor = true;
            this.btnPUninstall.Click += new System.EventHandler(this.btnPUninstall_Click);
            // 
            // btnPRefresh
            // 
            this.btnPRefresh.Location = new System.Drawing.Point(125, 369);
            this.btnPRefresh.Name = "btnPRefresh";
            this.btnPRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnPRefresh.TabIndex = 5;
            this.btnPRefresh.Text = "Refresh";
            this.btnPRefresh.UseVisualStyleBackColor = true;
            this.btnPRefresh.Click += new System.EventHandler(this.btnPRefresh_Click);
            // 
            // btnPClearAppsRem
            // 
            this.btnPClearAppsRem.Location = new System.Drawing.Point(691, 369);
            this.btnPClearAppsRem.Name = "btnPClearAppsRem";
            this.btnPClearAppsRem.Size = new System.Drawing.Size(75, 23);
            this.btnPClearAppsRem.TabIndex = 4;
            this.btnPClearAppsRem.Text = "Clear List";
            this.btnPClearAppsRem.UseVisualStyleBackColor = true;
            this.btnPClearAppsRem.Click += new System.EventHandler(this.btnPClearAppsRem_Click);
            // 
            // btnPMvLeft
            // 
            this.btnPMvLeft.Location = new System.Drawing.Point(313, 181);
            this.btnPMvLeft.Name = "btnPMvLeft";
            this.btnPMvLeft.Size = new System.Drawing.Size(175, 23);
            this.btnPMvLeft.TabIndex = 3;
            this.btnPMvLeft.Text = "< --";
            this.btnPMvLeft.UseVisualStyleBackColor = true;
            this.btnPMvLeft.Click += new System.EventHandler(this.btnPMvLeft_Click);
            // 
            // btnPMvRight
            // 
            this.btnPMvRight.Location = new System.Drawing.Point(313, 152);
            this.btnPMvRight.Name = "btnPMvRight";
            this.btnPMvRight.Size = new System.Drawing.Size(175, 23);
            this.btnPMvRight.TabIndex = 2;
            this.btnPMvRight.Text = "-- >";
            this.btnPMvRight.UseVisualStyleBackColor = true;
            this.btnPMvRight.Click += new System.EventHandler(this.btnPMvRight_Click);
            // 
            // PAppsToRemove
            // 
            this.PAppsToRemove.FormattingEnabled = true;
            this.PAppsToRemove.Location = new System.Drawing.Point(521, 34);
            this.PAppsToRemove.Name = "PAppsToRemove";
            this.PAppsToRemove.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PAppsToRemove.Size = new System.Drawing.Size(245, 329);
            this.PAppsToRemove.Sorted = true;
            this.PAppsToRemove.TabIndex = 1;
            // 
            // PListOfAppsInstalled
            // 
            this.PListOfAppsInstalled.FormattingEnabled = true;
            this.PListOfAppsInstalled.Location = new System.Drawing.Point(40, 34);
            this.PListOfAppsInstalled.Name = "PListOfAppsInstalled";
            this.PListOfAppsInstalled.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PListOfAppsInstalled.Size = new System.Drawing.Size(245, 329);
            this.PListOfAppsInstalled.Sorted = true;
            this.PListOfAppsInstalled.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows 10 Privacy Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Tools.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Apps.ResumeLayout(false);
            this.Apps.PerformLayout();
            this.Privacy.ResumeLayout(false);
            this.Privacy.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Start.ResumeLayout(false);
            this.Start.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ProvisionedApps.ResumeLayout(false);
            this.ProvisionedApps.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Tools;
        private System.Windows.Forms.Button btnQuickAccessDis;
        private System.Windows.Forms.Button btnQuickAccessEn;
        private System.Windows.Forms.Button btnResPhotoV;
        private System.Windows.Forms.Button btnUninstEdge;
        private System.Windows.Forms.Button btnUnPin;
        private System.Windows.Forms.Button btnResStart;
        private System.Windows.Forms.Button btnUninstOneDrive;
        private System.Windows.Forms.TabPage Apps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMvAllLeft;
        private System.Windows.Forms.Button btnMvAllRight;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClearAppsRem;
        private System.Windows.Forms.Button btnMvLeft;
        private System.Windows.Forms.Button btnMvRight;
        private System.Windows.Forms.ListBox AppsToRemove;
        private System.Windows.Forms.TabPage Privacy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox checkedListBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBox3;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage Start;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Button btnRemAll;
        private System.Windows.Forms.Button btnRecommend;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ListBox ListOfAppsInstalled;
        private System.Windows.Forms.Label lblInstalledCount;
        private System.Windows.Forms.Label lblRemoveCount;
        private System.Windows.Forms.CheckBox ShowHiddenApps;
        private System.Windows.Forms.TabPage ProvisionedApps;
        private System.Windows.Forms.CheckBox PShowHiddenApps;
        private System.Windows.Forms.Label lblPInstalledCount;
        private System.Windows.Forms.Label lblPRemoveCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPMvAllLeft;
        private System.Windows.Forms.Button btnPMvAllRight;
        private System.Windows.Forms.Button btnPUninstall;
        private System.Windows.Forms.Button btnPRefresh;
        private System.Windows.Forms.Button btnPClearAppsRem;
        private System.Windows.Forms.Button btnPMvLeft;
        private System.Windows.Forms.Button btnPMvRight;
        private System.Windows.Forms.ListBox PAppsToRemove;
        private System.Windows.Forms.ListBox PListOfAppsInstalled;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAutoSet;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label ProvAdminWarn;
        private System.Windows.Forms.Button btnProvInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnUnPhotoV;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefreshPriv;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnMinimal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel gitURL;
    }
}