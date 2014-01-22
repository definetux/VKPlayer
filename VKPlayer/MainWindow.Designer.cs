namespace VKPlayer
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.lstPlayList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDonwload = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblExit = new System.Windows.Forms.Label();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAudios = new System.Windows.Forms.Button();
            this.btnMix = new System.Windows.Forms.Button();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.btnMute = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMyPlayList = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtUrl.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtUrl.ForeColor = System.Drawing.SystemColors.Window;
            this.txtUrl.Location = new System.Drawing.Point(24, 135);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(293, 46);
            this.txtUrl.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtUrl, "Enter the artist");
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlay.Location = new System.Drawing.Point(464, 194);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(149, 110);
            this.btnPlay.TabIndex = 9;
            this.btnPlay.Text = "Play";
            this.toolTip1.SetToolTip(this.btnPlay, "Play or resume track");
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.Location = new System.Drawing.Point(640, 194);
            this.btnNext.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(149, 110);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "Next";
            this.toolTip1.SetToolTip(this.btnNext, "Next track");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStop.Location = new System.Drawing.Point(640, 318);
            this.btnStop.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(149, 110);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop";
            this.toolTip1.SetToolTip(this.btnStop, "Stop track");
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPause.Location = new System.Drawing.Point(464, 318);
            this.btnPause.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(149, 110);
            this.btnPause.TabIndex = 12;
            this.btnPause.Text = "Pause";
            this.toolTip1.SetToolTip(this.btnPause, "Pause track");
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lstPlayList
            // 
            this.lstPlayList.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstPlayList.ContextMenuStrip = this.contextMenuStrip1;
            this.lstPlayList.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstPlayList.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lstPlayList.FormattingEnabled = true;
            this.lstPlayList.ItemHeight = 25;
            this.lstPlayList.Location = new System.Drawing.Point(24, 195);
            this.lstPlayList.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.lstPlayList.Name = "lstPlayList";
            this.lstPlayList.Size = new System.Drawing.Size(360, 229);
            this.lstPlayList.TabIndex = 13;
            this.lstPlayList.SelectedIndexChanged += new System.EventHandler(this.lstPlayList_SelectedIndexChanged);
            this.lstPlayList.DoubleClick += new System.EventHandler(this.lstPlayList_DoubleClick);
            this.lstPlayList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPlayList_KeyDown);
            this.lstPlayList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstPlayList_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAdd,
            this.tsDelete,
            this.tsClear,
            this.toolStripSeparator1,
            this.tsDonwload});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(210, 106);
            // 
            // tsAdd
            // 
            this.tsAdd.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsAdd.Size = new System.Drawing.Size(209, 24);
            this.tsAdd.Text = "Add to playlist";
            this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(209, 24);
            this.tsDelete.Text = "Delete track";
            this.tsDelete.Visible = false;
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // tsClear
            // 
            this.tsClear.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsClear.Name = "tsClear";
            this.tsClear.Size = new System.Drawing.Size(209, 24);
            this.tsClear.Text = "Clear all";
            this.tsClear.Visible = false;
            this.tsClear.Click += new System.EventHandler(this.tsClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // tsDonwload
            // 
            this.tsDonwload.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsDonwload.Name = "tsDonwload";
            this.tsDonwload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsDonwload.Size = new System.Drawing.Size(209, 24);
            this.tsDonwload.Text = "Download";
            this.tsDonwload.Click += new System.EventHandler(this.tsDonwload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(17, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 40);
            this.label1.TabIndex = 15;
            this.label1.Text = "Enter the artist:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "VKPlayer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Font = new System.Drawing.Font("Viner Hand ITC", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExit.Location = new System.Drawing.Point(765, -1);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(43, 44);
            this.lblExit.TabIndex = 16;
            this.lblExit.Text = " X";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            this.lblExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblExit_MouseDown);
            this.lblExit.MouseEnter += new System.EventHandler(this.lblExit_MouseEnter);
            this.lblExit.MouseLeave += new System.EventHandler(this.lblExit_MouseLeave);
            // 
            // lblMinimize
            // 
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Font = new System.Drawing.Font("Viner Hand ITC", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.Location = new System.Drawing.Point(735, -1);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Size = new System.Drawing.Size(33, 34);
            this.lblMinimize.TabIndex = 17;
            this.lblMinimize.Text = " _";
            this.lblMinimize.Click += new System.EventHandler(this.lblMinimize_Click);
            this.lblMinimize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblMinimize_MouseDown);
            this.lblMinimize.MouseEnter += new System.EventHandler(this.lblMinimize_MouseEnter);
            this.lblMinimize.MouseLeave += new System.EventHandler(this.lblMinimize_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Viner Hand ITC", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(285, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 47);
            this.label2.TabIndex = 18;
            this.label2.Text = "VKPlayer";
            // 
            // btnAudios
            // 
            this.btnAudios.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAudios.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAudios.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAudios.Location = new System.Drawing.Point(464, 70);
            this.btnAudios.Name = "btnAudios";
            this.btnAudios.Size = new System.Drawing.Size(149, 110);
            this.btnAudios.TabIndex = 19;
            this.btnAudios.Text = "My Audios";
            this.toolTip1.SetToolTip(this.btnAudios, "My audio in VK");
            this.btnAudios.UseVisualStyleBackColor = false;
            this.btnAudios.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMix
            // 
            this.btnMix.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMix.Image = global::VKPlayer.Properties.Resources.Media_player_icons_11_5121;
            this.btnMix.Location = new System.Drawing.Point(317, 135);
            this.btnMix.Name = "btnMix";
            this.btnMix.Size = new System.Drawing.Size(67, 46);
            this.btnMix.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnMix, "Mix tracklist");
            this.btnMix.UseVisualStyleBackColor = true;
            this.btnMix.Click += new System.EventHandler(this.btnMix_Click);
            // 
            // tbVolume
            // 
            this.tbVolume.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbVolume.Location = new System.Drawing.Point(397, 194);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbVolume.Size = new System.Drawing.Size(45, 190);
            this.tbVolume.TabIndex = 21;
            this.tbVolume.TickFrequency = 25;
            this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTip1.SetToolTip(this.tbVolume, "Volume");
            this.tbVolume.Value = 50;
            this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
            // 
            // btnMute
            // 
            this.btnMute.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMute.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMute.Location = new System.Drawing.Point(392, 390);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(59, 32);
            this.btnMute.TabIndex = 22;
            this.btnMute.Text = "Mute";
            this.toolTip1.SetToolTip(this.btnMute, "Mute");
            this.btnMute.UseVisualStyleBackColor = false;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnMyPlayList
            // 
            this.btnMyPlayList.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnMyPlayList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMyPlayList.Font = new System.Drawing.Font("Segoe UI Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMyPlayList.Location = new System.Drawing.Point(640, 71);
            this.btnMyPlayList.Name = "btnMyPlayList";
            this.btnMyPlayList.Size = new System.Drawing.Size(149, 109);
            this.btnMyPlayList.TabIndex = 23;
            this.btnMyPlayList.Text = "My Playlist";
            this.toolTip1.SetToolTip(this.btnMyPlayList, "My playlist on PC");
            this.btnMyPlayList.UseVisualStyleBackColor = false;
            this.btnMyPlayList.Click += new System.EventHandler(this.btnMyPlayList_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.Location = new System.Drawing.Point(0, -1);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(55, 47);
            this.btnLogout.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnLogout, "Logout");
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblId.Location = new System.Drawing.Point(61, 9);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(38, 25);
            this.lblId.TabIndex = 25;
            this.lblId.Text = "ID: ";
            this.toolTip1.SetToolTip(this.lblId, "ID in VK");
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(809, 440);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMyPlayList);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.tbVolume);
            this.Controls.Add(this.btnMix);
            this.Controls.Add(this.btnAudios);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMinimize);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstPlayList);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtUrl);
            this.Font = new System.Drawing.Font("Viner Hand ITC", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VK Player";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ListBox lstPlayList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAudios;
        private System.Windows.Forms.Button btnMix;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsDonwload;
        private System.Windows.Forms.Button btnMyPlayList;
        private System.Windows.Forms.ToolStripMenuItem tsDelete;
        private System.Windows.Forms.ToolStripMenuItem tsClear;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblId;
    }
}

