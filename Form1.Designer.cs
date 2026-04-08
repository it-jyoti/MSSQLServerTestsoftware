using System.Drawing;
using System.Windows.Forms;

namespace MSSQLServerTest
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ── Menu ──────────────────────────────────────────────────────────
            menuStrip   = new MenuStrip();
            mnuFile     = new ToolStripMenuItem();
            mnuExit     = new ToolStripMenuItem();
            mnuView     = new ToolStripMenuItem();
            mnuTheme    = new ToolStripMenuItem();
            mnuHelp     = new ToolStripMenuItem();
            mnuUpdate   = new ToolStripMenuItem();
            mnuSep1     = new ToolStripSeparator();
            mnuAbout    = new ToolStripMenuItem();

            // ── Header (gradient) ─────────────────────────────────────────────
            pnlHeader       = new GradientPanel();
            lblAppTitle     = new Label();
            btnTheme        = new Button();
            btnCheckUpdates = new Button();

            // ── TabControl ────────────────────────────────────────────────────
            tabControl   = new TabControl();
            tabSqlServer = new TabPage();
            tabSysSmtp   = new TabPage();

            // ── Tab 1: Network Discovery group ────────────────────────────────
            grpNetworkDiscovery = new GroupBox();
            lblServer           = new Label();
            cmbServer           = new ComboBox();
            btnRefresh          = new Button();
            btnScanNetwork      = new Button();
            lblScanMsg          = new Label();
            dgvServers          = new DataGridView();

            // ── Tab 1: Auth group ─────────────────────────────────────────────
            grpAuth        = new GroupBox();
            radioWindows   = new RadioButton();
            radioSqlServer = new RadioButton();
            radioAzureAD   = new RadioButton();
            lblLogin       = new Label();
            txtLogin       = new TextBox();
            lblPassword    = new Label();
            txtPassword    = new TextBox();

            // ── Tab 1: Connection button + status + databases ──────────────────
            btnTestConnection = new Button();
            lblStatus         = new Label();
            grpDatabases      = new GroupBox();
            listDatabases     = new ListView();
            colName           = new ColumnHeader();
            colSize           = new ColumnHeader();

            // ── Tab 2: System Info ────────────────────────────────────────────
            grpSysInfo       = new GroupBox();
            lblHostnameLbl   = new Label();
            txtHostname      = new TextBox();
            btnCopyHostname  = new Button();
            lblPublicIPLbl   = new Label();
            txtPublicIP      = new TextBox();
            btnCopyPublicIP  = new Button();
            btnRefreshSysInfo = new Button();

            // ── Tab 2: SMTP ───────────────────────────────────────────────────
            grpSMTP          = new GroupBox();
            lblSmtpServerLbl = new Label();
            txtSmtpServer    = new TextBox();
            lblSmtpPortLbl   = new Label();
            txtSmtpPort      = new TextBox();
            lblSmtpFromLbl   = new Label();
            txtSmtpFrom      = new TextBox();
            lblSmtpUserLbl   = new Label();
            txtSmtpUsername  = new TextBox();
            lblSmtpPassLbl   = new Label();
            txtSmtpPassword  = new TextBox();
            lblSmtpToLbl     = new Label();
            txtSmtpTo        = new TextBox();
            lblSmtpSubjectLbl = new Label();
            txtSmtpSubject   = new TextBox();
            chkSmtpSSL       = new CheckBox();
            btnTestSMTP      = new Button();

            // ── Tab 2: SMTP Log ───────────────────────────────────────────────
            grpSmtpLog = new GroupBox();
            rtbSmtpLog = new RichTextBox();

            // ── Status strip + footer ─────────────────────────────────────────
            statusStrip  = new StatusStrip();
            tsslMain     = new ToolStripStatusLabel();
            tsspProgress = new ToolStripProgressBar();
            pnlFooter    = new Panel();
            lblFooter    = new Label();

            // ── ToolTip ───────────────────────────────────────────────────────
            toolTip = new ToolTip(components);

            // ═══ SUSPEND LAYOUTS ══════════════════════════════════════════════
            menuStrip.SuspendLayout();
            pnlHeader.SuspendLayout();
            tabControl.SuspendLayout();
            tabSqlServer.SuspendLayout();
            tabSysSmtp.SuspendLayout();
            grpNetworkDiscovery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvServers).BeginInit();
            grpAuth.SuspendLayout();
            grpDatabases.SuspendLayout();
            grpSysInfo.SuspendLayout();
            grpSMTP.SuspendLayout();
            grpSmtpLog.SuspendLayout();
            statusStrip.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ═══ MENU STRIP ═══════════════════════════════════════════════════
            menuStrip.Items.AddRange(new ToolStripItem[] { mnuFile, mnuView, mnuHelp });
            menuStrip.GripStyle = ToolStripGripStyle.Hidden;
            menuStrip.Dock      = DockStyle.Top;

            mnuFile.Text = "&File";
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuExit });
            mnuExit.Text = "E&xit";
            mnuExit.ShortcutKeys = Keys.Alt | Keys.F4;
            mnuExit.Click += (s, e) => Application.Exit();

            mnuView.Text = "&View";
            mnuView.DropDownItems.AddRange(new ToolStripItem[] { mnuTheme });
            mnuTheme.Text         = "Night Mode";
            mnuTheme.ShortcutKeys = Keys.Control | Keys.T;
            mnuTheme.Click       += MnuTheme_Click;

            mnuHelp.Text = "&Help";
            mnuHelp.DropDownItems.AddRange(new ToolStripItem[]
                { mnuUpdate, mnuSep1, mnuAbout });
            mnuUpdate.Text  = "Check for &Updates…";
            mnuUpdate.Click += MnuUpdate_Click;
            mnuAbout.Text   = "&About";
            mnuAbout.Click  += MnuAbout_Click;

            // ═══ HEADER (GradientPanel) ════════════════════════════════════════
            // Dock=Top → sits directly below menuStrip
            pnlHeader.Dock          = DockStyle.Top;
            pnlHeader.Height        = 52;
            pnlHeader.GradientStart = Color.FromArgb(28, 100, 175);
            pnlHeader.GradientEnd   = Color.FromArgb(16, 60, 100);
            pnlHeader.Controls.Add(lblAppTitle);
            pnlHeader.Controls.Add(btnCheckUpdates);
            pnlHeader.Controls.Add(btnTheme);

            lblAppTitle.Text      = "MS SQL Server Connection Test  —  v1.0.0";
            lblAppTitle.Location  = new Point(14, 16);
            lblAppTitle.AutoSize  = true;
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Theme toggle button (right side)
            btnTheme.Text      = "\uD83C\uDF19  Night Mode";
            btnTheme.Location  = new Point(462, 13);
            btnTheme.Size      = new Size(128, 26);
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.FlatAppearance.BorderColor = Color.White;
            btnTheme.ForeColor = Color.White;
            btnTheme.BackColor = Color.Transparent;
            btnTheme.Cursor    = Cursors.Hand;
            btnTheme.Click    += BtnTheme_Click;
            toolTip.SetToolTip(btnTheme, "Toggle Day / Night theme  (Ctrl+T)");

            // Check Updates button (right of theme)
            btnCheckUpdates.Text      = "\u2B06 Check Updates";
            btnCheckUpdates.Location  = new Point(598, 13);
            btnCheckUpdates.Size      = new Size(130, 26);
            btnCheckUpdates.FlatStyle = FlatStyle.Flat;
            btnCheckUpdates.FlatAppearance.BorderColor = Color.White;
            btnCheckUpdates.ForeColor = Color.White;
            btnCheckUpdates.BackColor = Color.Transparent;
            btnCheckUpdates.Cursor    = Cursors.Hand;
            btnCheckUpdates.Click    += BtnCheckUpdates_Click;
            toolTip.SetToolTip(btnCheckUpdates, "Check for software updates");

            // ═══ TAB CONTROL ══════════════════════════════════════════════════
            tabControl.Dock          = DockStyle.Fill;
            tabControl.Font          = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            tabControl.Padding       = new Point(14, 4);
            tabControl.TabPages.AddRange(new TabPage[] { tabSqlServer, tabSysSmtp });
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            tabSqlServer.Text        = "  SQL Server  ";
            tabSqlServer.BackColor   = Color.FromArgb(245, 245, 245);
            tabSqlServer.Padding     = new Padding(0);

            tabSysSmtp.Text          = "  System & SMTP  ";
            tabSysSmtp.BackColor     = Color.FromArgb(245, 245, 245);
            tabSysSmtp.Padding       = new Padding(0);

            // ═══ TAB 1 — SQL SERVER ════════════════════════════════════════════

            // ── Group: SQL Server Instances (Network Discovery) ────────────────
            // y=8, height=210
            grpNetworkDiscovery.Text     = "SQL Server Instances";
            grpNetworkDiscovery.Location = new Point(8, 8);
            grpNetworkDiscovery.Size     = new Size(718, 210);
            grpNetworkDiscovery.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            lblServer.Text     = "Server Instance:";
            lblServer.Location = new Point(10, 28);
            lblServer.AutoSize = true;
            lblServer.Font     = new Font("Segoe UI", 9F);
            grpNetworkDiscovery.Controls.Add(lblServer);

            cmbServer.Location           = new Point(118, 24);
            cmbServer.Size               = new Size(456, 23);
            cmbServer.DropDownStyle      = ComboBoxStyle.DropDown;
            cmbServer.AutoCompleteMode   = AutoCompleteMode.SuggestAppend;
            cmbServer.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbServer.Font               = new Font("Segoe UI", 9F);
            grpNetworkDiscovery.Controls.Add(cmbServer);

            btnRefresh.Text     = "\u21BB  Refresh";
            btnRefresh.Location = new Point(582, 23);
            btnRefresh.Size     = new Size(126, 26);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Cursor   = Cursors.Hand;
            btnRefresh.Font     = new Font("Segoe UI", 9F);
            btnRefresh.Click   += btnRefresh_Click;
            grpNetworkDiscovery.Controls.Add(btnRefresh);
            toolTip.SetToolTip(btnRefresh, "Quickly refresh the SQL Server instance list");

            btnScanNetwork.Text     = "\uD83D\uDD0D  Scan Network";
            btnScanNetwork.Location = new Point(10, 58);
            btnScanNetwork.Size     = new Size(150, 28);
            btnScanNetwork.FlatStyle = FlatStyle.Flat;
            btnScanNetwork.Cursor   = Cursors.Hand;
            btnScanNetwork.Font     = new Font("Segoe UI", 9F);
            btnScanNetwork.Click   += btnScanNetwork_Click;
            grpNetworkDiscovery.Controls.Add(btnScanNetwork);
            toolTip.SetToolTip(btnScanNetwork,
                "Scan network for SQL Server instances and probe their version");

            lblScanMsg.Text     = "Click 'Scan Network' to discover instances";
            lblScanMsg.Location = new Point(170, 63);
            lblScanMsg.Size     = new Size(540, 18);
            lblScanMsg.AutoSize = false;
            lblScanMsg.Font     = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            grpNetworkDiscovery.Controls.Add(lblScanMsg);

            // DataGridView
            dgvServers.Location          = new Point(10, 94);
            dgvServers.Size              = new Size(698, 106);
            dgvServers.ReadOnly          = true;
            dgvServers.RowHeadersVisible  = false;
            dgvServers.AllowUserToAddRows = false;
            dgvServers.MultiSelect        = false;
            dgvServers.SelectionMode      = DataGridViewSelectionMode.FullRowSelect;
            dgvServers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvServers.BorderStyle        = BorderStyle.FixedSingle;
            dgvServers.ScrollBars         = ScrollBars.Vertical;
            dgvServers.Font               = new Font("Segoe UI", 8.5F);
            dgvServers.EnableHeadersVisualStyles = false;
            dgvServers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor       = Color.FromArgb(70, 130, 180),
                ForeColor       = Color.White,
                Font            = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(70, 130, 180),
                SelectionForeColor = Color.White,
                Alignment       = DataGridViewContentAlignment.MiddleLeft
            };
            dgvServers.RowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor          = Color.White,
                ForeColor          = Color.FromArgb(30, 30, 30),
                SelectionBackColor = Color.FromArgb(173, 216, 230),
                SelectionForeColor = Color.Black
            };
            dgvServers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor          = Color.FromArgb(245, 249, 255),
                ForeColor          = Color.FromArgb(30, 30, 30),
                SelectionBackColor = Color.FromArgb(173, 216, 230),
                SelectionForeColor = Color.Black
            };

            var colDgvServer   = new DataGridViewTextBoxColumn { Name = "colDgvServer",   HeaderText = "Server Name", FillWeight = 28 };
            var colDgvInstance = new DataGridViewTextBoxColumn { Name = "colDgvInstance", HeaderText = "Instance",    FillWeight = 20 };
            var colDgvVersion  = new DataGridViewTextBoxColumn { Name = "colDgvVersion",  HeaderText = "Version",     FillWeight = 38 };
            var colDgvStatus   = new DataGridViewTextBoxColumn { Name = "colDgvStatus",   HeaderText = "Status",      FillWeight = 14 };
            dgvServers.Columns.AddRange(colDgvServer, colDgvInstance, colDgvVersion, colDgvStatus);

            dgvServers.SelectionChanged  += DgvServers_SelectionChanged;
            dgvServers.CellFormatting    += DgvServers_CellFormatting;
            grpNetworkDiscovery.Controls.Add(dgvServers);

            tabSqlServer.Controls.Add(grpNetworkDiscovery);

            // ── Group: Authentication ─────────────────────────────────────────
            // y=226 (8+210+8), height=148
            grpAuth.Text     = "Authentication";
            grpAuth.Location = new Point(8, 226);
            grpAuth.Size     = new Size(718, 148);
            grpAuth.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            radioWindows.Text     = "Windows Authentication";
            radioWindows.Location = new Point(14, 24);
            radioWindows.AutoSize = true;
            radioWindows.Font     = new Font("Segoe UI", 9F);
            radioWindows.CheckedChanged += radioWindows_CheckedChanged;
            grpAuth.Controls.Add(radioWindows);

            radioSqlServer.Text     = "SQL Server Authentication";
            radioSqlServer.Location = new Point(14, 50);
            radioSqlServer.AutoSize = true;
            radioSqlServer.Font     = new Font("Segoe UI", 9F);
            radioSqlServer.CheckedChanged += radioSqlServer_CheckedChanged;
            grpAuth.Controls.Add(radioSqlServer);

            radioAzureAD.Text     = "Azure Active Directory";
            radioAzureAD.Location = new Point(14, 76);
            radioAzureAD.AutoSize = true;
            radioAzureAD.Font     = new Font("Segoe UI", 9F);
            radioAzureAD.CheckedChanged += RadioAzureAD_CheckedChanged;
            grpAuth.Controls.Add(radioAzureAD);
            toolTip.SetToolTip(radioAzureAD,
                "Authenticate using Azure Active Directory (Integrated or Password)");

            lblLogin.Text     = "Login ID:";
            lblLogin.Location = new Point(14, 104);
            lblLogin.Size     = new Size(90, 20);
            lblLogin.Font     = new Font("Segoe UI", 9F);
            grpAuth.Controls.Add(lblLogin);

            txtLogin.Location        = new Point(110, 100);
            txtLogin.Size            = new Size(268, 23);
            txtLogin.PlaceholderText = "Enter Login ID";
            txtLogin.Font            = new Font("Segoe UI", 9F);
            grpAuth.Controls.Add(txtLogin);

            lblPassword.Text     = "Password:";
            lblPassword.Location = new Point(14, 128);
            lblPassword.Size     = new Size(90, 20);
            lblPassword.Font     = new Font("Segoe UI", 9F);
            grpAuth.Controls.Add(lblPassword);

            txtPassword.Location        = new Point(110, 124);
            txtPassword.Size            = new Size(268, 23);
            txtPassword.PasswordChar    = '\u2022';
            txtPassword.PlaceholderText = "Enter Password";
            txtPassword.Font            = new Font("Segoe UI", 9F);
            grpAuth.Controls.Add(txtPassword);

            tabSqlServer.Controls.Add(grpAuth);

            // ── Test Connection button ─────────────────────────────────────────
            // y=382 (226+148+8), height=34
            btnTestConnection.Text      = "\uD83D\uDD17  Test Connection";
            btnTestConnection.Location  = new Point(8, 382);
            btnTestConnection.Size      = new Size(718, 36);
            btnTestConnection.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTestConnection.BackColor = Color.SteelBlue;
            btnTestConnection.ForeColor = Color.White;
            btnTestConnection.FlatStyle = FlatStyle.Flat;
            btnTestConnection.FlatAppearance.BorderSize = 0;
            btnTestConnection.Cursor    = Cursors.Hand;
            btnTestConnection.Click    += btnTestConnection_Click;
            tabSqlServer.Controls.Add(btnTestConnection);
            toolTip.SetToolTip(btnTestConnection, "Test the database connection with current settings");

            // ── Status label ──────────────────────────────────────────────────
            // y=426 (382+36+8), height=22
            lblStatus.Location  = new Point(8, 426);
            lblStatus.Size      = new Size(718, 22);
            lblStatus.AutoSize  = false;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Font      = new Font("Segoe UI", 9F, FontStyle.Italic);
            tabSqlServer.Controls.Add(lblStatus);

            // ── Group: Available Databases ────────────────────────────────────
            // y=456 (426+22+8), height=136
            grpDatabases.Text     = "Available Databases";
            grpDatabases.Location = new Point(8, 456);
            grpDatabases.Size     = new Size(718, 136);
            grpDatabases.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            colName.Text  = "Database Name";
            colName.Width = 490;
            colSize.Text      = "Size (MB)";
            colSize.Width     = 180;
            colSize.TextAlign = HorizontalAlignment.Right;

            listDatabases.Location      = new Point(10, 22);
            listDatabases.Size          = new Size(698, 104);
            listDatabases.View          = View.Details;
            listDatabases.FullRowSelect = true;
            listDatabases.GridLines     = true;
            listDatabases.MultiSelect   = false;
            listDatabases.BorderStyle   = BorderStyle.FixedSingle;
            listDatabases.Font          = new Font("Segoe UI", 9F);
            listDatabases.Columns.AddRange(new ColumnHeader[] { colName, colSize });
            grpDatabases.Controls.Add(listDatabases);
            tabSqlServer.Controls.Add(grpDatabases);

            // ═══ TAB 2 — SYSTEM & SMTP ═════════════════════════════════════════

            // ── Group: System Information ─────────────────────────────────────
            // y=8, height=118
            grpSysInfo.Text     = "System Information";
            grpSysInfo.Location = new Point(8, 8);
            grpSysInfo.Size     = new Size(718, 118);
            grpSysInfo.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            lblHostnameLbl.Text     = "Hostname:";
            lblHostnameLbl.Location = new Point(10, 30);
            lblHostnameLbl.Size     = new Size(82, 20);
            lblHostnameLbl.Font     = new Font("Segoe UI", 9F);
            grpSysInfo.Controls.Add(lblHostnameLbl);

            txtHostname.Location  = new Point(100, 26);
            txtHostname.Size      = new Size(470, 23);
            txtHostname.ReadOnly  = true;
            txtHostname.Font      = new Font("Segoe UI", 9F);
            grpSysInfo.Controls.Add(txtHostname);

            btnCopyHostname.Text     = "\uD83D\uDCCB  Copy";
            btnCopyHostname.Location = new Point(580, 25);
            btnCopyHostname.Size     = new Size(128, 26);
            btnCopyHostname.FlatStyle = FlatStyle.Flat;
            btnCopyHostname.Cursor   = Cursors.Hand;
            btnCopyHostname.Font     = new Font("Segoe UI", 9F);
            btnCopyHostname.Click   += BtnCopyHostname_Click;
            grpSysInfo.Controls.Add(btnCopyHostname);
            toolTip.SetToolTip(btnCopyHostname, "Copy hostname to clipboard");

            lblPublicIPLbl.Text     = "Public IP:";
            lblPublicIPLbl.Location = new Point(10, 60);
            lblPublicIPLbl.Size     = new Size(82, 20);
            lblPublicIPLbl.Font     = new Font("Segoe UI", 9F);
            grpSysInfo.Controls.Add(lblPublicIPLbl);

            txtPublicIP.Location  = new Point(100, 56);
            txtPublicIP.Size      = new Size(470, 23);
            txtPublicIP.ReadOnly  = true;
            txtPublicIP.Font      = new Font("Segoe UI", 9F);
            grpSysInfo.Controls.Add(txtPublicIP);

            btnCopyPublicIP.Text     = "\uD83D\uDCCB  Copy";
            btnCopyPublicIP.Location = new Point(580, 55);
            btnCopyPublicIP.Size     = new Size(128, 26);
            btnCopyPublicIP.FlatStyle = FlatStyle.Flat;
            btnCopyPublicIP.Cursor   = Cursors.Hand;
            btnCopyPublicIP.Font     = new Font("Segoe UI", 9F);
            btnCopyPublicIP.Click   += BtnCopyPublicIP_Click;
            grpSysInfo.Controls.Add(btnCopyPublicIP);
            toolTip.SetToolTip(btnCopyPublicIP, "Copy public IP address to clipboard");

            btnRefreshSysInfo.Text     = "\u21BB  Refresh";
            btnRefreshSysInfo.Location = new Point(10, 86);
            btnRefreshSysInfo.Size     = new Size(110, 24);
            btnRefreshSysInfo.FlatStyle = FlatStyle.Flat;
            btnRefreshSysInfo.Cursor   = Cursors.Hand;
            btnRefreshSysInfo.Font     = new Font("Segoe UI", 9F);
            btnRefreshSysInfo.Click   += BtnRefreshSysInfo_Click;
            grpSysInfo.Controls.Add(btnRefreshSysInfo);

            tabSysSmtp.Controls.Add(grpSysInfo);

            // ── Group: SMTP Configuration ─────────────────────────────────────
            // y=134 (8+118+8), height=294
            grpSMTP.Text     = "SMTP Configuration";
            grpSMTP.Location = new Point(8, 134);
            grpSMTP.Size     = new Size(718, 294);
            grpSMTP.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Row 1 — Server + Port
            AddSmtpLabel(grpSMTP, lblSmtpServerLbl, "SMTP Server:", 10, 28);
            txtSmtpServer.Location        = new Point(110, 24);
            txtSmtpServer.Size            = new Size(344, 23);
            txtSmtpServer.PlaceholderText = "smtp.gmail.com";
            txtSmtpServer.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpServer);

            AddSmtpLabel(grpSMTP, lblSmtpPortLbl, "Port:", 464, 28);
            txtSmtpPort.Location        = new Point(502, 24);
            txtSmtpPort.Size            = new Size(64, 23);
            txtSmtpPort.Text            = "587";
            txtSmtpPort.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpPort);

            // Row 2 — From
            AddSmtpLabel(grpSMTP, lblSmtpFromLbl, "From Email:", 10, 58);
            txtSmtpFrom.Location        = new Point(110, 54);
            txtSmtpFrom.Size            = new Size(456, 23);
            txtSmtpFrom.PlaceholderText = "sender@example.com";
            txtSmtpFrom.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpFrom);

            // Row 3 — Username
            AddSmtpLabel(grpSMTP, lblSmtpUserLbl, "Username:", 10, 88);
            txtSmtpUsername.Location        = new Point(110, 84);
            txtSmtpUsername.Size            = new Size(456, 23);
            txtSmtpUsername.PlaceholderText = "SMTP login (leave blank if not required)";
            txtSmtpUsername.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpUsername);

            // Row 4 — Password
            AddSmtpLabel(grpSMTP, lblSmtpPassLbl, "Password:", 10, 118);
            txtSmtpPassword.Location     = new Point(110, 114);
            txtSmtpPassword.Size         = new Size(456, 23);
            txtSmtpPassword.PasswordChar = '\u2022';
            txtSmtpPassword.PlaceholderText = "SMTP password / app password";
            txtSmtpPassword.Font         = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpPassword);

            // Row 5 — To
            AddSmtpLabel(grpSMTP, lblSmtpToLbl, "To Email:", 10, 148);
            txtSmtpTo.Location        = new Point(110, 144);
            txtSmtpTo.Size            = new Size(456, 23);
            txtSmtpTo.PlaceholderText = "recipient@example.com";
            txtSmtpTo.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpTo);

            // Row 6 — Subject
            AddSmtpLabel(grpSMTP, lblSmtpSubjectLbl, "Subject:", 10, 178);
            txtSmtpSubject.Location        = new Point(110, 174);
            txtSmtpSubject.Size            = new Size(456, 23);
            txtSmtpSubject.PlaceholderText = "SMTP Test — MSSQLServerTest v1.0.0";
            txtSmtpSubject.Font            = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(txtSmtpSubject);

            // SSL checkbox
            chkSmtpSSL.Text     = "Enable SSL / TLS";
            chkSmtpSSL.Location = new Point(10, 208);
            chkSmtpSSL.AutoSize = true;
            chkSmtpSSL.Font     = new Font("Segoe UI", 9F);
            grpSMTP.Controls.Add(chkSmtpSSL);
            toolTip.SetToolTip(chkSmtpSSL,
                "Enable SSL/TLS. Use port 465 for SSL, 587 for STARTTLS.");

            // Test SMTP button
            btnTestSMTP.Text      = "\u2709  Send Test Email";
            btnTestSMTP.Location  = new Point(10, 244);
            btnTestSMTP.Size      = new Size(200, 36);
            btnTestSMTP.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTestSMTP.BackColor = Color.SteelBlue;
            btnTestSMTP.ForeColor = Color.White;
            btnTestSMTP.FlatStyle = FlatStyle.Flat;
            btnTestSMTP.FlatAppearance.BorderSize = 0;
            btnTestSMTP.Cursor    = Cursors.Hand;
            btnTestSMTP.Click    += BtnTestSMTP_Click;
            grpSMTP.Controls.Add(btnTestSMTP);
            toolTip.SetToolTip(btnTestSMTP,
                "Send a test email using the SMTP settings above");

            tabSysSmtp.Controls.Add(grpSMTP);

            // ── Group: SMTP Test Log ──────────────────────────────────────────
            // y=436 (134+294+8), height=156
            grpSmtpLog.Text     = "Test Log";
            grpSmtpLog.Location = new Point(8, 436);
            grpSmtpLog.Size     = new Size(718, 156);
            grpSmtpLog.Font     = new Font("Segoe UI", 9F, FontStyle.Bold);

            rtbSmtpLog.Location        = new Point(10, 22);
            rtbSmtpLog.Size            = new Size(698, 124);
            rtbSmtpLog.ReadOnly        = true;
            rtbSmtpLog.BackColor       = Color.White;
            rtbSmtpLog.ForeColor       = Color.FromArgb(30, 30, 30);
            rtbSmtpLog.BorderStyle     = BorderStyle.FixedSingle;
            rtbSmtpLog.Font            = new Font("Consolas", 8.5F);
            rtbSmtpLog.ScrollBars      = RichTextBoxScrollBars.Vertical;
            grpSmtpLog.Controls.Add(rtbSmtpLog);
            tabSysSmtp.Controls.Add(grpSmtpLog);

            // ═══ STATUS STRIP ════════════════════════════════════════════════
            tsslMain.Text      = "Ready";
            tsslMain.Spring    = true;
            tsslMain.TextAlign = ContentAlignment.MiddleLeft;

            tsspProgress.Style                  = ProgressBarStyle.Marquee;
            tsspProgress.MarqueeAnimationSpeed  = 30;
            tsspProgress.Size                   = new Size(160, 16);
            tsspProgress.Visible                = false;

            statusStrip.Items.AddRange(new ToolStripItem[] { tsslMain, tsspProgress });
            statusStrip.Dock      = DockStyle.Bottom;
            statusStrip.SizingGrip = false;

            // ═══ FOOTER ══════════════════════════════════════════════════════
            lblFooter.Text      = "Developed by Bitan Bhattachirjee";
            lblFooter.Dock      = DockStyle.Fill;
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            lblFooter.ForeColor = Color.White;
            lblFooter.Font      = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            pnlFooter.Dock      = DockStyle.Bottom;
            pnlFooter.Height    = 28;
            pnlFooter.BackColor = Color.SteelBlue;
            pnlFooter.Controls.Add(lblFooter);

            // ═══ FORM ════════════════════════════════════════════════════════
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(740, 756);
            FormBorderStyle     = FormBorderStyle.FixedSingle;
            MaximizeBox         = false;
            StartPosition       = FormStartPosition.CenterScreen;
            Text                = "MS SQL Server Connection Test  v1.0.0";
            Font                = new Font("Segoe UI", 9F);
            MainMenuStrip       = menuStrip;
            Load               += Form1_Load;

            // ═══ ADD TO FORM (docking order matters) ══════════════════════════
            // Processing order: highest index first.
            // menuStrip   [4] → very top   (Dock=Top)
            // pnlHeader   [3] → below menu (Dock=Top)
            // pnlFooter   [2] → very bottom(Dock=Bottom)
            // statusStrip [1] → above footer(Dock=Bottom)
            // tabControl  [0] → fills rest  (Dock=Fill)
            Controls.Add(tabControl);    // index 0
            Controls.Add(statusStrip);   // index 1
            Controls.Add(pnlFooter);     // index 2
            Controls.Add(pnlHeader);     // index 3
            Controls.Add(menuStrip);     // index 4

            // ═══ RESUME LAYOUTS ═══════════════════════════════════════════════
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpNetworkDiscovery.ResumeLayout(false);
            grpNetworkDiscovery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvServers).EndInit();
            grpAuth.ResumeLayout(false);
            grpAuth.PerformLayout();
            grpDatabases.ResumeLayout(false);
            grpSysInfo.ResumeLayout(false);
            grpSysInfo.PerformLayout();
            grpSMTP.ResumeLayout(false);
            grpSMTP.PerformLayout();
            grpSmtpLog.ResumeLayout(false);
            tabSqlServer.ResumeLayout(false);
            tabSysSmtp.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        // Helper: add a right-aligned label inside an SMTP group box
        private static void AddSmtpLabel(GroupBox parent, Label lbl, string text, int x, int y)
        {
            lbl.Text      = text;
            lbl.Location  = new Point(x, y);
            lbl.Size      = new Size(96, 20);
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.Font      = new Font("Segoe UI", 9F);
            parent.Controls.Add(lbl);
        }

        #endregion

        // ── Designer fields ────────────────────────────────────────────────────
        private MenuStrip          menuStrip;
        private ToolStripMenuItem  mnuFile, mnuExit, mnuView, mnuTheme;
        private ToolStripMenuItem  mnuHelp, mnuUpdate, mnuAbout;
        private ToolStripSeparator mnuSep1;

        private GradientPanel      pnlHeader;
        private Label              lblAppTitle;
        private Button             btnTheme, btnCheckUpdates;

        private TabControl         tabControl;
        private TabPage            tabSqlServer, tabSysSmtp;

        // Tab 1
        private GroupBox           grpNetworkDiscovery;
        private Label              lblServer, lblScanMsg;
        private ComboBox           cmbServer;
        private Button             btnRefresh, btnScanNetwork;
        private DataGridView       dgvServers;

        private GroupBox           grpAuth;
        private RadioButton        radioWindows, radioSqlServer, radioAzureAD;
        private Label              lblLogin, lblPassword;
        private TextBox            txtLogin, txtPassword;

        private Button             btnTestConnection;
        private Label              lblStatus;

        private GroupBox           grpDatabases;
        private ListView           listDatabases;
        private ColumnHeader       colName, colSize;

        // Tab 2 – System Info
        private GroupBox           grpSysInfo;
        private Label              lblHostnameLbl, lblPublicIPLbl;
        private TextBox            txtHostname, txtPublicIP;
        private Button             btnCopyHostname, btnCopyPublicIP, btnRefreshSysInfo;

        // Tab 2 – SMTP
        private GroupBox           grpSMTP;
        private Label              lblSmtpServerLbl, lblSmtpPortLbl, lblSmtpFromLbl;
        private Label              lblSmtpUserLbl, lblSmtpPassLbl, lblSmtpToLbl, lblSmtpSubjectLbl;
        private TextBox            txtSmtpServer, txtSmtpPort, txtSmtpFrom;
        private TextBox            txtSmtpUsername, txtSmtpPassword, txtSmtpTo, txtSmtpSubject;
        private CheckBox           chkSmtpSSL;
        private Button             btnTestSMTP;

        private GroupBox           grpSmtpLog;
        private RichTextBox        rtbSmtpLog;

        // Status / Footer
        private StatusStrip            statusStrip;
        private ToolStripStatusLabel   tsslMain;
        private ToolStripProgressBar   tsspProgress;
        private Panel                  pnlFooter;
        private Label                  lblFooter;

        private ToolTip                toolTip;
    }
}
