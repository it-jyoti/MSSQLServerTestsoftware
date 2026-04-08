using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using MimeKit;

namespace MSSQLServerTest
{
    public partial class Form1 : Form
    {
        private bool _sysInfoLoaded;

        public Form1() => InitializeComponent();

        // ═══════════════════════════════════════════════════════════════════════
        //  FORM LOAD
        // ═══════════════════════════════════════════════════════════════════════

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadIcon();
            radioWindows.Checked = true;
            SetAuthFieldsState(enabled: false);
            lblStatus.Text = string.Empty;
            ApplyTheme();
            SetGlobalStatus("Ready", false);
        }

        private void LoadIcon()
        {
            string[] candidates =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLICON1.ico"),
                @"D:\TICKET\MS SQL Server Test\SQLICON1.ico"
            };
            foreach (var p in candidates)
                if (File.Exists(p)) { Icon = new Icon(p); break; }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  THEME
        // ═══════════════════════════════════════════════════════════════════════

        private void BtnTheme_Click(object sender, EventArgs e)  => ToggleTheme();
        private void MnuTheme_Click(object sender, EventArgs e)  => ToggleTheme();

        private void ToggleTheme()
        {
            ThemeManager.Toggle();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            bool isNight = ThemeManager.Current == AppTheme.Night;

            // Form
            BackColor = ThemeManager.FormBack;

            // Gradient header
            pnlHeader.GradientStart = ThemeManager.HeaderGradientStart;
            pnlHeader.GradientEnd   = ThemeManager.HeaderGradientEnd;
            pnlHeader.Invalidate();
            btnTheme.Text = isNight ? "\u2600  Day Mode" : "\uD83C\uDF19  Night Mode";
            btnTheme.FlatAppearance.BorderColor =
                isNight ? Color.FromArgb(100, 160, 220) : Color.White;
            btnCheckUpdates.FlatAppearance.BorderColor = btnTheme.FlatAppearance.BorderColor;

            // Menu
            if (isNight)
            {
                menuStrip.Renderer  = new DarkMenuRenderer();
                menuStrip.BackColor = ThemeManager.MenuBack;
            }
            else
            {
                menuStrip.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                menuStrip.BackColor  = ThemeManager.MenuBack;
            }
            ApplyMenuForeColor(menuStrip.Items, ThemeManager.MenuFore);
            mnuTheme.Text = isNight ? "Day Mode" : "Night Mode";

            // Tab control + pages
            tabControl.BackColor = ThemeManager.TabBack;
            foreach (TabPage tp in tabControl.TabPages)
                tp.BackColor = ThemeManager.TabBack;

            // ── SQL Server tab ─────────────────────────────────────────────
            StyleGroup(grpNetworkDiscovery);
            StyleGroup(grpAuth);
            StyleGroup(grpDatabases);

            StyleLabel(lblServer);
            cmbServer.BackColor = ThemeManager.InputBack;
            cmbServer.ForeColor = ThemeManager.ForeColor;

            StyleSecondaryButton(btnRefresh);
            StyleSecondaryButton(btnScanNetwork);

            lblScanMsg.BackColor = ThemeManager.TabBack;
            lblScanMsg.ForeColor = ThemeManager.ForeColor;

            // DataGridView
            StyleDataGridView(dgvServers);

            foreach (var rb in new[] { radioWindows, radioSqlServer, radioAzureAD })
            {
                rb.BackColor = ThemeManager.TabBack;
                rb.ForeColor = ThemeManager.ForeColor;
            }

            bool sqlEnabled = radioSqlServer.Checked || radioAzureAD.Checked;
            StyleLabel(lblLogin,    sqlEnabled);
            StyleLabel(lblPassword, sqlEnabled);
            txtLogin.BackColor    = ThemeManager.InputBack;
            txtLogin.ForeColor    = ThemeManager.ForeColor;
            txtPassword.BackColor = ThemeManager.InputBack;
            txtPassword.ForeColor = ThemeManager.ForeColor;

            btnTestConnection.BackColor = ThemeManager.AccentColor;
            btnTestConnection.ForeColor = Color.White;

            lblStatus.BackColor = ThemeManager.TabBack;

            listDatabases.BackColor = ThemeManager.InputBack;
            listDatabases.ForeColor = ThemeManager.ForeColor;

            // ── System & SMTP tab ──────────────────────────────────────────
            StyleGroup(grpSysInfo);
            StyleGroup(grpSMTP);
            StyleGroup(grpSmtpLog);

            foreach (var lbl in new[] { lblHostnameLbl, lblPublicIPLbl })
                StyleLabel(lbl);
            txtHostname.BackColor = ThemeManager.InputBack;
            txtHostname.ForeColor = ThemeManager.ForeColor;
            txtPublicIP.BackColor = ThemeManager.InputBack;
            txtPublicIP.ForeColor = ThemeManager.ForeColor;
            StyleSecondaryButton(btnCopyHostname);
            StyleSecondaryButton(btnCopyPublicIP);
            StyleSecondaryButton(btnRefreshSysInfo);

            foreach (var lbl in new[] { lblSmtpServerLbl, lblSmtpPortLbl, lblSmtpFromLbl,
                                        lblSmtpUserLbl, lblSmtpPassLbl, lblSmtpToLbl, lblSmtpSubjectLbl })
                StyleLabel(lbl);

            foreach (var tb in new[] { txtSmtpServer, txtSmtpPort, txtSmtpFrom,
                                       txtSmtpUsername, txtSmtpPassword, txtSmtpTo, txtSmtpSubject })
            {
                tb.BackColor = ThemeManager.InputBack;
                tb.ForeColor = ThemeManager.ForeColor;
            }

            chkSmtpSSL.BackColor = ThemeManager.TabBack;
            chkSmtpSSL.ForeColor = ThemeManager.ForeColor;

            btnTestSMTP.BackColor = ThemeManager.AccentColor;
            btnTestSMTP.ForeColor = Color.White;

            rtbSmtpLog.BackColor = ThemeManager.InputBack;
            rtbSmtpLog.ForeColor = ThemeManager.ForeColor;

            // Status strip
            statusStrip.BackColor = ThemeManager.StatusBarBack;
            tsslMain.ForeColor    = ThemeManager.ForeColor;

            // Footer
            pnlFooter.BackColor = ThemeManager.HeaderBack;

            Invalidate(true);
            Update();
        }

        private void StyleGroup(GroupBox grp)
        {
            grp.BackColor = ThemeManager.TabBack;
            grp.ForeColor = ThemeManager.ForeColor;
        }

        private static void StyleLabel(Label lbl, bool enabled = true)
        {
            lbl.BackColor = ThemeManager.TabBack;
            lbl.ForeColor = enabled ? ThemeManager.ForeColor : ThemeManager.DisabledFore;
        }

        private static void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = ThemeManager.ButtonSecBack;
            btn.ForeColor = ThemeManager.ButtonSecFore;
            btn.FlatAppearance.BorderColor = ThemeManager.BorderColor;
        }

        private static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor                          = ThemeManager.InputBack;
            dgv.GridColor                                = ThemeManager.BorderColor;
            dgv.DefaultCellStyle.BackColor               = ThemeManager.DgvRowBack;
            dgv.DefaultCellStyle.ForeColor               = ThemeManager.ForeColor;
            dgv.DefaultCellStyle.SelectionBackColor      = ThemeManager.DgvSelectBack;
            dgv.DefaultCellStyle.SelectionForeColor      = ThemeManager.DgvSelectFore;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DgvAltRowBack;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.ForeColor;
            dgv.ColumnHeadersDefaultCellStyle.BackColor  = ThemeManager.DgvHeaderBack;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = ThemeManager.DgvHeaderFore;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.DgvHeaderBack;
            dgv.RowHeadersDefaultCellStyle.BackColor     = ThemeManager.DgvRowBack;
            dgv.RowHeadersDefaultCellStyle.ForeColor     = ThemeManager.ForeColor;
        }

        private static void ApplyMenuForeColor(ToolStripItemCollection items, Color color)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = color;
                if (item is ToolStripMenuItem mi && mi.HasDropDownItems)
                    ApplyMenuForeColor(mi.DropDownItems, color);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  AUTH RADIO BUTTONS
        // ═══════════════════════════════════════════════════════════════════════

        private void radioWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioWindows.Checked) return;
            lblLogin.Text    = "Login ID:";
            lblPassword.Text = "Password:";
            SetAuthFieldsState(enabled: false);
        }

        private void radioSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioSqlServer.Checked) return;
            lblLogin.Text    = "Login ID:";
            lblPassword.Text = "Password:";
            txtLogin.PlaceholderText    = "Enter Login ID";
            txtPassword.PlaceholderText = "Enter Password";
            SetAuthFieldsState(enabled: true);
            txtLogin.Focus();
        }

        private void RadioAzureAD_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioAzureAD.Checked) return;
            lblLogin.Text    = "Azure UPN:";
            lblPassword.Text = "AD Password:";
            txtLogin.PlaceholderText    = "user@domain.com  (blank = Integrated)";
            txtPassword.PlaceholderText = "Azure AD password";
            SetAuthFieldsState(enabled: true);
            txtLogin.Focus();
        }

        private void SetAuthFieldsState(bool enabled)
        {
            txtLogin.Enabled      = enabled;
            txtPassword.Enabled   = enabled;
            lblLogin.ForeColor    = enabled ? ThemeManager.ForeColor : ThemeManager.DisabledFore;
            lblPassword.ForeColor = enabled ? ThemeManager.ForeColor : ThemeManager.DisabledFore;

            if (!enabled)
            {
                txtLogin.Text    = string.Empty;
                txtPassword.Text = string.Empty;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  NETWORK DISCOVERY
        // ═══════════════════════════════════════════════════════════════════════

        private async void btnScanNetwork_Click(object sender, EventArgs e)
        {
            btnScanNetwork.Enabled = false;
            dgvServers.Rows.Clear();
            cmbServer.Items.Clear();
            lblScanMsg.Text = "Scanning network…";
            SetGlobalStatus("Scanning for SQL Server instances…", true);

            try
            {
                // Phase 1: use a dedicated static method so named-tuple names are preserved
                var raw = await Task.Run(DiscoverInstances);
                lblScanMsg.Text = $"{raw.Count} instance(s) found — probing versions…";

                // Phase 2: probe each instance in parallel
                var versionTasks = raw
                    .Select(r => TryGetVersionAsync(r.Server, r.Instance))
                    .ToArray();
                var versions = await Task.WhenAll(versionTasks);

                for (int i = 0; i < raw.Count; i++)
                {
                    dgvServers.Rows.Add(
                        raw[i].Server, raw[i].Instance,
                        versions[i].Version, versions[i].Status);

                    string full = string.IsNullOrEmpty(raw[i].Instance)
                        ? raw[i].Server
                        : $@"{raw[i].Server}\{raw[i].Instance}";
                    cmbServer.Items.Add(full);
                }

                if (cmbServer.Items.Count > 0) cmbServer.SelectedIndex = 0;
                lblScanMsg.Text = $"{raw.Count} instance(s) discovered.";
                SetGlobalStatus($"Scan complete — {raw.Count} instance(s) found.", false);
            }
            catch (Exception ex)
            {
                lblScanMsg.Text = $"Scan error: {ex.Message}";
                SetGlobalStatus("Scan failed.", false);
            }
            finally
            {
                btnScanNetwork.Enabled = true;
            }
        }

        // Dedicated static method — named tuple elements are preserved in the method body
        private static List<(string Server, string Instance)> DiscoverInstances()
        {
            var tbl  = SqlDataSourceEnumerator.Instance.GetDataSources();
            var list = new List<(string Server, string Instance)>();
            foreach (DataRow row in tbl.Rows)
                list.Add((row["ServerName"].ToString()!, row["InstanceName"].ToString()!));
            list.Sort((a, b) =>
                string.Compare(a.Server, b.Server, StringComparison.OrdinalIgnoreCase));
            return list;
        }

        private static async Task<(string Version, string Status)> TryGetVersionAsync(
            string server, string instance)
        {
            // Return type is on the *method*, so names are guaranteed inside Task.Run lambda
            return await Task.Run(() =>
            {
                try
                {
                    string srv = string.IsNullOrEmpty(instance) ? server : $@"{server}\{instance}";
                    var cs = new SqlConnectionStringBuilder
                    {
                        DataSource             = srv,
                        IntegratedSecurity     = true,
                        ConnectTimeout         = 3,
                        TrustServerCertificate = true
                    }.ConnectionString;

                    using var conn = new SqlConnection(cs);
                    conn.Open();
                    using var cmd = new SqlCommand(
                        "SELECT CAST(SERVERPROPERTY('ProductVersion') AS NVARCHAR(50))", conn);
                    string ver = cmd.ExecuteScalar()?.ToString() ?? "?";
                    return (ver, "Online");
                }
                catch (SqlException ex) when (ex.Number is 18456 or 18452)
                {
                    return ("—", "Auth Required");
                }
                catch
                {
                    return ("—", "Offline");
                }
            });
        }

        private void DgvServers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServers.SelectedRows.Count == 0) return;
            var row = dgvServers.SelectedRows[0];
            string srv  = row.Cells[0].Value?.ToString() ?? "";
            string inst = row.Cells[1].Value?.ToString() ?? "";
            if (!string.IsNullOrEmpty(srv))
                cmbServer.Text = string.IsNullOrEmpty(inst) ? srv : $@"{srv}\{inst}";
        }

        private void DgvServers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvServers.Columns[e.ColumnIndex].Name != "colDgvStatus") return;
            if (e.CellStyle == null) return;
            e.CellStyle.ForeColor = e.Value?.ToString() switch
            {
                "Online"        => Color.FromArgb(0,  160, 80),
                "Offline"       => Color.FromArgb(200, 40, 40),
                "Auth Required" => Color.DarkOrange,
                _               => ThemeManager.ForeColor
            };
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  QUICK REFRESH (ComboBox only)
        // ═══════════════════════════════════════════════════════════════════════

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            cmbServer.Items.Clear();
            listDatabases.Items.Clear();
            lblStatus.Text = string.Empty;
            SetGlobalStatus("Searching for SQL Server instances…", true);

            try
            {
                List<string> instances = await Task.Run<List<string>>(() =>
                {
                    var tbl  = SqlDataSourceEnumerator.Instance.GetDataSources();
                    var list = new List<string>();
                    foreach (DataRow row in tbl.Rows)
                    {
                        string srv  = row["ServerName"].ToString()!;
                        string inst = row["InstanceName"].ToString()!;
                        list.Add(string.IsNullOrWhiteSpace(inst) ? srv : $@"{srv}\{inst}");
                    }
                    list.Sort();
                    return list;
                });

                foreach (var s in instances)
                    cmbServer.Items.Add(s);

                if (cmbServer.Items.Count > 0)
                    cmbServer.SelectedIndex = 0;

                SetGlobalStatus($"{instances.Count} instance(s) found.", false);
            }
            catch (Exception ex)
            {
                SetGlobalStatus($"Error: {ex.Message}", false);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  TEST CONNECTION
        // ═══════════════════════════════════════════════════════════════════════

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            string server = cmbServer.Text.Trim();
            if (string.IsNullOrEmpty(server))
            {
                ShowError("Please enter or select a SQL Server instance.");
                return;
            }

            if (radioSqlServer.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtLogin.Text))
                { ShowError("Please enter a Login ID."); txtLogin.Focus(); return; }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                { ShowError("Please enter a Password."); txtPassword.Focus(); return; }
            }

            listDatabases.Items.Clear();
            btnTestConnection.Enabled = false;
            lblStatus.Text            = "Connecting…";
            lblStatus.ForeColor       = ThemeManager.ForeColor;
            SetGlobalStatus("Connecting to SQL Server…", true);

            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource             = server,
                    ConnectTimeout         = 10,
                    TrustServerCertificate = true
                };

                if (radioWindows.Checked)
                {
                    builder.IntegratedSecurity = true;
                }
                else if (radioSqlServer.Checked)
                {
                    builder.IntegratedSecurity = false;
                    builder.UserID             = txtLogin.Text.Trim();
                    builder.Password           = txtPassword.Text;
                }
                else if (radioAzureAD.Checked)
                {
                    builder.IntegratedSecurity = false;
                    if (!string.IsNullOrWhiteSpace(txtLogin.Text))
                    {
                        builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
                        builder.UserID   = txtLogin.Text.Trim();
                        builder.Password = txtPassword.Text;
                    }
                    else
                    {
                        builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryIntegrated;
                    }
                }

                var databases = await Task.Run(() =>
                    GetDatabasesWithSize(builder.ConnectionString));

                lblStatus.Text      = $"\u2714  Connected — {databases.Count} database(s) found.";
                lblStatus.ForeColor = Color.FromArgb(0, 150, 60);
                SetGlobalStatus($"Connected to {server}. {databases.Count} database(s).", false);

                foreach (var (name, sizeMb) in databases)
                {
                    var item = new ListViewItem(name);
                    item.SubItems.Add(sizeMb > 0 ? $"{sizeMb:N2}" : "\u2014");
                    listDatabases.Items.Add(item);
                }
            }
            catch (SqlException ex)
            {
                ShowError($"\u2718  Connection failed: {ex.Message}");
                SetGlobalStatus("Connection failed.", false);
            }
            catch (Exception ex)
            {
                ShowError($"\u2718  Unexpected error: {ex.Message}");
                SetGlobalStatus("Error.", false);
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        private static List<(string Name, double SizeMb)> GetDatabasesWithSize(
            string connectionString)
        {
            var list = new List<(string, double)>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            const string sql = @"
                SELECT d.name,
                       ISNULL(CAST(SUM(mf.size) * 8.0 / 1024 AS DECIMAL(10,2)), 0)
                FROM sys.databases d
                LEFT JOIN sys.master_files mf ON d.database_id = mf.database_id
                WHERE d.state_desc = 'ONLINE'
                GROUP BY d.name
                ORDER BY d.name";

            using var cmd    = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add((reader.GetString(0), (double)reader.GetDecimal(1)));
            return list;
        }

        private void ShowError(string message)
        {
            lblStatus.Text      = message;
            lblStatus.ForeColor = Color.FromArgb(200, 40, 40);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  TAB – SYSTEM INFO & SMTP
        // ═══════════════════════════════════════════════════════════════════════

        private async void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabSysSmtp && !_sysInfoLoaded)
            {
                _sysInfoLoaded = true;
                await LoadSystemInfoAsync();
            }
        }

        private async Task LoadSystemInfoAsync()
        {
            txtHostname.Text = Dns.GetHostName();
            txtPublicIP.Text = "Fetching…";
            SetGlobalStatus("Fetching public IP address…", true);
            try
            {
                using var http = new System.Net.Http.HttpClient
                    { Timeout = TimeSpan.FromSeconds(10) };
                txtPublicIP.Text = (await http.GetStringAsync("https://api.ipify.org")).Trim();
                SetGlobalStatus("System information loaded.", false);
            }
            catch
            {
                txtPublicIP.Text = "Unable to retrieve";
                SetGlobalStatus("Could not fetch public IP.", false);
            }
        }

        private void BtnCopyHostname_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHostname.Text))
            {
                Clipboard.SetText(txtHostname.Text);
                SetGlobalStatus("Hostname copied to clipboard.", false);
            }
        }

        private void BtnCopyPublicIP_Click(object sender, EventArgs e)
        {
            string ip = txtPublicIP.Text;
            if (!string.IsNullOrEmpty(ip) && ip != "Fetching…" && ip != "Unable to retrieve")
            {
                Clipboard.SetText(ip);
                SetGlobalStatus("Public IP copied to clipboard.", false);
            }
        }

        private async void BtnRefreshSysInfo_Click(object sender, EventArgs e)
        {
            _sysInfoLoaded = true;
            await LoadSystemInfoAsync();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  SMTP TEST
        // ═══════════════════════════════════════════════════════════════════════

        private async void BtnTestSMTP_Click(object sender, EventArgs e)
        {
            string smtpSrv = txtSmtpServer.Text.Trim();
            string from    = txtSmtpFrom.Text.Trim();
            string to      = txtSmtpTo.Text.Trim();
            string user    = txtSmtpUsername.Text.Trim();
            string pass    = txtSmtpPassword.Text;
            string subject = txtSmtpSubject.Text.Trim();
            bool   ssl     = chkSmtpSSL.Checked;

            if (!int.TryParse(txtSmtpPort.Text.Trim(), out int port)) port = 587;
            if (string.IsNullOrEmpty(subject))
                subject = $"SMTP Test — MSSQLServerTest v{UpdateChecker.CurrentVersion}";

            if (string.IsNullOrEmpty(smtpSrv))
            { AppendSmtpLog("\u2718 SMTP Server is required.", Color.FromArgb(200,40,40)); return; }
            if (string.IsNullOrEmpty(from))
            { AppendSmtpLog("\u2718 From address is required.", Color.FromArgb(200,40,40)); return; }
            if (string.IsNullOrEmpty(to))
            { AppendSmtpLog("\u2718 To address is required.", Color.FromArgb(200,40,40)); return; }

            btnTestSMTP.Enabled = false;
            AppendSmtpLog($"\u25ba Connecting to {smtpSrv}:{port}  (SSL={ssl})…", ThemeManager.ForeColor);
            SetGlobalStatus($"Sending test email via {smtpSrv}…", true);

            try
            {
                await Task.Run(async () =>
                {
                    var msg = new MimeMessage();
                    msg.From.Add(new MailboxAddress("MSSQLServerTest", from));
                    msg.To.Add(new MailboxAddress(string.Empty, to));
                    msg.Subject = subject;
                    msg.Body = new TextPart("plain")
                    {
                        Text = $"Test email from MS SQL Server Connection Test v{UpdateChecker.CurrentVersion}.\n\n" +
                               $"If you received this, SMTP is configured correctly!\n\n" +
                               $"Sent: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                    };

                    using var smtp = new SmtpClient();
                    smtp.ServerCertificateValidationCallback = (_, _, _, _) => true;
                    await smtp.ConnectAsync(smtpSrv, port,
                        ssl ? SecureSocketOptions.SslOnConnect
                            : SecureSocketOptions.StartTlsWhenAvailable);

                    if (!string.IsNullOrEmpty(user))
                        await smtp.AuthenticateAsync(user, pass);

                    await smtp.SendAsync(msg);
                    await smtp.DisconnectAsync(true);
                });

                AppendSmtpLog($"\u2714 Email sent successfully to {to}", Color.FromArgb(0, 150, 60));
                SetGlobalStatus("Test email sent successfully.", false);
            }
            catch (Exception ex)
            {
                AppendSmtpLog($"\u2718 Failed: {ex.Message}", Color.FromArgb(200, 40, 40));
                SetGlobalStatus("SMTP test failed.", false);
            }
            finally
            {
                btnTestSMTP.Enabled = true;
            }
        }

        private void AppendSmtpLog(string message, Color color)
        {
            rtbSmtpLog.SuspendLayout();
            int start = rtbSmtpLog.TextLength;
            rtbSmtpLog.AppendText($"[{DateTime.Now:HH:mm:ss}]  {message}\n");
            rtbSmtpLog.Select(start, rtbSmtpLog.TextLength - start);
            rtbSmtpLog.SelectionColor = color;
            rtbSmtpLog.SelectionLength = 0;
            rtbSmtpLog.ScrollToCaret();
            rtbSmtpLog.ResumeLayout();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  CHECK UPDATES
        // ═══════════════════════════════════════════════════════════════════════

        private async void BtnCheckUpdates_Click(object sender, EventArgs e)
        {
            btnCheckUpdates.Enabled = false;
            SetGlobalStatus("Checking for updates…", true);
            await RunUpdateCheck();
            btnCheckUpdates.Enabled = true;
        }

        private async void MnuUpdate_Click(object sender, EventArgs e)
        {
            mnuUpdate.Enabled = false;
            SetGlobalStatus("Checking for updates…", true);
            await RunUpdateCheck();
            mnuUpdate.Enabled = true;
        }

        private static async Task RunUpdateCheck()
        {
            var result = await UpdateChecker.CheckAsync();

            if (result.HasUpdate)
            {
                var dlg = MessageBox.Show(result.Message, "Update Available",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dlg == DialogResult.OK && !string.IsNullOrEmpty(result.DownloadUrl))
                    Process.Start(new ProcessStartInfo(result.DownloadUrl)
                        { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show($"Already up to date — Version {UpdateChecker.CurrentVersion}",
                    "Software Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  ABOUT
        // ═══════════════════════════════════════════════════════════════════════

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            string runtime = RuntimeInformation.FrameworkDescription;
            string os      = RuntimeInformation.OSDescription;

            MessageBox.Show(
                $"MS SQL Server Connection Test\n" +
                $"Version {UpdateChecker.CurrentVersion}\n\n" +
                $"Developed by Bitan Bhattachirjee\n" +
                $"\u00A9 2024\u20132026  All Rights Reserved\n\n" +
                $"Runtime : {runtime}\n" +
                $"Platform: {os}",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  STATUS BAR HELPER
        // ═══════════════════════════════════════════════════════════════════════

        private void SetGlobalStatus(string message, bool showProgress)
        {
            tsslMain.Text          = message;
            tsspProgress.Visible   = showProgress;
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  DARK MENU RENDERER
        // ═══════════════════════════════════════════════════════════════════════

        private sealed class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkColorTable()) { }
        }

        private sealed class DarkColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin        => Color.FromArgb(45, 45, 45);
            public override Color MenuStripGradientEnd          => Color.FromArgb(45, 45, 45);
            public override Color MenuItemSelected              => Color.FromArgb(65, 65, 65);
            public override Color MenuItemBorder                => Color.FromArgb(100, 100, 100);
            public override Color MenuBorder                    => Color.FromArgb(80, 80, 80);
            public override Color ToolStripDropDownBackground   => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientBegin      => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientMiddle     => Color.FromArgb(45, 45, 45);
            public override Color ImageMarginGradientEnd        => Color.FromArgb(45, 45, 45);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(65, 65, 65);
            public override Color MenuItemSelectedGradientEnd   => Color.FromArgb(65, 65, 65);
            public override Color MenuItemPressedGradientBegin  => Color.FromArgb(80, 80, 80);
            public override Color MenuItemPressedGradientEnd    => Color.FromArgb(80, 80, 80);
            public override Color SeparatorDark                 => Color.FromArgb(80, 80, 80);
            public override Color SeparatorLight                => Color.FromArgb(60, 60, 60);
        }
    }
}
