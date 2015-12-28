namespace Salvis_Poiss_PROGII_Project
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblCameras = new System.Windows.Forms.Label();
            this.lstCameras = new System.Windows.Forms.ComboBox();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.grpVisual = new System.Windows.Forms.GroupBox();
            this.optVisualNone = new System.Windows.Forms.RadioButton();
            this.optVisualImage = new System.Windows.Forms.RadioButton();
            this.optVisualName = new System.Windows.Forms.RadioButton();
            this.optVisualFrame = new System.Windows.Forms.RadioButton();
            this.cmbGlyphSize = new System.Windows.Forms.ComboBox();
            this.lblGlyphSize = new System.Windows.Forms.Label();
            this.btnAddGlyph = new System.Windows.Forms.Button();
            this.imgCamera = new System.Windows.Forms.PictureBox();
            this.btnEditGlyphs = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpVisual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCameras
            // 
            this.lblCameras.AutoSize = true;
            this.lblCameras.Location = new System.Drawing.Point(183, 19);
            this.lblCameras.Name = "lblCameras";
            this.lblCameras.Size = new System.Drawing.Size(51, 13);
            this.lblCameras.TabIndex = 0;
            this.lblCameras.Text = "Kameras:";
            // 
            // lstCameras
            // 
            this.lstCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstCameras.FormattingEnabled = true;
            this.lstCameras.Location = new System.Drawing.Point(240, 15);
            this.lstCameras.Name = "lstCameras";
            this.lstCameras.Size = new System.Drawing.Size(160, 21);
            this.lstCameras.TabIndex = 1;
            this.lstCameras.SelectedIndexChanged += new System.EventHandler(this.lstCameras_SelectedIndexChanged);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(406, 14);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(138, 23);
            this.btnRefreshList.TabIndex = 2;
            this.btnRefreshList.Text = "Atjaunot sarakstu";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // grpVisual
            // 
            this.grpVisual.Controls.Add(this.optVisualNone);
            this.grpVisual.Controls.Add(this.optVisualImage);
            this.grpVisual.Controls.Add(this.optVisualName);
            this.grpVisual.Controls.Add(this.optVisualFrame);
            this.grpVisual.Location = new System.Drawing.Point(666, 93);
            this.grpVisual.Name = "grpVisual";
            this.grpVisual.Size = new System.Drawing.Size(160, 119);
            this.grpVisual.TabIndex = 5;
            this.grpVisual.TabStop = false;
            this.grpVisual.Text = "Vizualizācija";
            // 
            // optVisualNone
            // 
            this.optVisualNone.AutoSize = true;
            this.optVisualNone.Location = new System.Drawing.Point(6, 19);
            this.optVisualNone.Name = "optVisualNone";
            this.optVisualNone.Size = new System.Drawing.Size(57, 17);
            this.optVisualNone.TabIndex = 4;
            this.optVisualNone.TabStop = true;
            this.optVisualNone.Text = "0. Nav";
            this.optVisualNone.UseVisualStyleBackColor = true;
            // 
            // optVisualImage
            // 
            this.optVisualImage.AutoSize = true;
            this.optVisualImage.Location = new System.Drawing.Point(6, 88);
            this.optVisualImage.Name = "optVisualImage";
            this.optVisualImage.Size = new System.Drawing.Size(103, 17);
            this.optVisualImage.TabIndex = 3;
            this.optVisualImage.TabStop = true;
            this.optVisualImage.Text = "3. Aizvieto attēlu";
            this.optVisualImage.UseVisualStyleBackColor = true;
            // 
            // optVisualName
            // 
            this.optVisualName.AutoSize = true;
            this.optVisualName.Location = new System.Drawing.Point(6, 65);
            this.optVisualName.Name = "optVisualName";
            this.optVisualName.Size = new System.Drawing.Size(108, 17);
            this.optVisualName.TabIndex = 2;
            this.optVisualName.TabStop = true;
            this.optVisualName.Text = "2. Pievieno vārdu";
            this.optVisualName.UseVisualStyleBackColor = true;
            // 
            // optVisualFrame
            // 
            this.optVisualFrame.AutoSize = true;
            this.optVisualFrame.Location = new System.Drawing.Point(6, 42);
            this.optVisualFrame.Name = "optVisualFrame";
            this.optVisualFrame.Size = new System.Drawing.Size(69, 17);
            this.optVisualFrame.TabIndex = 1;
            this.optVisualFrame.TabStop = true;
            this.optVisualFrame.Text = "1. Ierāmē";
            this.optVisualFrame.UseVisualStyleBackColor = true;
            // 
            // cmbGlyphSize
            // 
            this.cmbGlyphSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGlyphSize.FormattingEnabled = true;
            this.cmbGlyphSize.Location = new System.Drawing.Point(666, 59);
            this.cmbGlyphSize.Name = "cmbGlyphSize";
            this.cmbGlyphSize.Size = new System.Drawing.Size(160, 21);
            this.cmbGlyphSize.TabIndex = 6;
            this.cmbGlyphSize.SelectedIndexChanged += new System.EventHandler(this.cmbGlyphSize_SelectedIndexChanged);
            // 
            // lblGlyphSize
            // 
            this.lblGlyphSize.AutoSize = true;
            this.lblGlyphSize.Location = new System.Drawing.Point(663, 43);
            this.lblGlyphSize.Name = "lblGlyphSize";
            this.lblGlyphSize.Size = new System.Drawing.Size(73, 13);
            this.lblGlyphSize.TabIndex = 7;
            this.lblGlyphSize.Text = "Raksta izmērs";
            // 
            // btnAddGlyph
            // 
            this.btnAddGlyph.Location = new System.Drawing.Point(666, 218);
            this.btnAddGlyph.Name = "btnAddGlyph";
            this.btnAddGlyph.Size = new System.Drawing.Size(160, 23);
            this.btnAddGlyph.TabIndex = 8;
            this.btnAddGlyph.Text = "Pievienot rakstu";
            this.btnAddGlyph.UseVisualStyleBackColor = true;
            this.btnAddGlyph.Click += new System.EventHandler(this.btnAddGlyph_Click);
            // 
            // imgCamera
            // 
            this.imgCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCamera.Location = new System.Drawing.Point(12, 43);
            this.imgCamera.Name = "imgCamera";
            this.imgCamera.Size = new System.Drawing.Size(640, 480);
            this.imgCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCamera.TabIndex = 3;
            this.imgCamera.TabStop = false;
            // 
            // btnEditGlyphs
            // 
            this.btnEditGlyphs.Location = new System.Drawing.Point(666, 247);
            this.btnEditGlyphs.Name = "btnEditGlyphs";
            this.btnEditGlyphs.Size = new System.Drawing.Size(160, 23);
            this.btnEditGlyphs.TabIndex = 9;
            this.btnEditGlyphs.Text = "Labot rakstus";
            this.btnEditGlyphs.UseVisualStyleBackColor = true;
            this.btnEditGlyphs.Click += new System.EventHandler(this.btnEditGlyphs_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(666, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Aizvērt programmu";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 539);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEditGlyphs);
            this.Controls.Add(this.btnAddGlyph);
            this.Controls.Add(this.lblGlyphSize);
            this.Controls.Add(this.cmbGlyphSize);
            this.Controls.Add(this.grpVisual);
            this.Controls.Add(this.imgCamera);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.lstCameras);
            this.Controls.Add(this.lblCameras);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rakstu atpazīšana";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpVisual.ResumeLayout(false);
            this.grpVisual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCameras;
        private System.Windows.Forms.ComboBox lstCameras;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.GroupBox grpVisual;
        private System.Windows.Forms.RadioButton optVisualFrame;
        private System.Windows.Forms.RadioButton optVisualName;
        private System.Windows.Forms.RadioButton optVisualImage;
        private System.Windows.Forms.ComboBox cmbGlyphSize;
        private System.Windows.Forms.Label lblGlyphSize;
        private System.Windows.Forms.Button btnAddGlyph;
        private System.Windows.Forms.PictureBox imgCamera;
        private System.Windows.Forms.RadioButton optVisualNone;
        private System.Windows.Forms.Button btnEditGlyphs;
        private System.Windows.Forms.Button btnClose;
    }
}

