namespace Salvis_Poiss_PROGII_Project
{
    partial class frmAddGlyph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddGlyph));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpGlyph = new System.Windows.Forms.GroupBox();
            this.glyphEditor = new Salvis_Poiss_PROGII_Project.GlyphEditorControl();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.pnlGlyphColor = new System.Windows.Forms.Panel();
            this.imgGlyphImage = new System.Windows.Forms.PictureBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.cxmGlyphEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.grpGlyph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgGlyphImage)).BeginInit();
            this.cxmGlyphEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Nosaukums:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 25);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 20);
            this.txtName.TabIndex = 2;
            // 
            // grpGlyph
            // 
            this.grpGlyph.Controls.Add(this.glyphEditor);
            this.grpGlyph.Location = new System.Drawing.Point(202, 9);
            this.grpGlyph.Name = "grpGlyph";
            this.grpGlyph.Size = new System.Drawing.Size(303, 315);
            this.grpGlyph.TabIndex = 3;
            this.grpGlyph.TabStop = false;
            this.grpGlyph.Text = "Raksts";
            // 
            // glyphEditor
            // 
            this.glyphEditor.GlyphData = null;
            this.glyphEditor.Location = new System.Drawing.Point(6, 19);
            this.glyphEditor.Name = "glyphEditor";
            this.glyphEditor.Size = new System.Drawing.Size(290, 290);
            this.glyphEditor.TabIndex = 0;
            this.glyphEditor.Text = "glyphEditor";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(12, 54);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(40, 13);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Izmērs:";
            // 
            // cmbSize
            // 
            this.cmbSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(58, 51);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(140, 21);
            this.cmbSize.TabIndex = 5;
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(40, 89);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 6;
            this.lblColor.Text = "Krāsa";
            // 
            // pnlGlyphColor
            // 
            this.pnlGlyphColor.BackColor = System.Drawing.Color.Red;
            this.pnlGlyphColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGlyphColor.Location = new System.Drawing.Point(12, 105);
            this.pnlGlyphColor.Name = "pnlGlyphColor";
            this.pnlGlyphColor.Size = new System.Drawing.Size(90, 90);
            this.pnlGlyphColor.TabIndex = 7;
            this.pnlGlyphColor.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // imgGlyphImage
            // 
            this.imgGlyphImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgGlyphImage.Location = new System.Drawing.Point(108, 105);
            this.imgGlyphImage.Name = "imgGlyphImage";
            this.imgGlyphImage.Size = new System.Drawing.Size(90, 90);
            this.imgGlyphImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgGlyphImage.TabIndex = 8;
            this.imgGlyphImage.TabStop = false;
            this.imgGlyphImage.Click += new System.EventHandler(this.imgGlyphImage_Click);
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(137, 89);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(33, 13);
            this.lblImage.TabIndex = 9;
            this.lblImage.Text = "Attēls";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 201);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Saglabāt";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 293);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(186, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Aizvērt logu";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(10, 230);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(186, 23);
            this.btnSaveAndClose.TabIndex = 12;
            this.btnSaveAndClose.Text = "Saglabāt un aizvērt";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // cxmGlyphEditor
            // 
            this.cxmGlyphEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.mnuPrintPreview});
            this.cxmGlyphEditor.Name = "cxmGlyphEditor";
            this.cxmGlyphEditor.Size = new System.Drawing.Size(197, 48);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(196, 22);
            this.mnuPrint.Text = "Izdrukāt";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuPrintPreview
            // 
            this.mnuPrintPreview.Name = "mnuPrintPreview";
            this.mnuPrintPreview.Size = new System.Drawing.Size(196, 22);
            this.mnuPrintPreview.Text = "Izdrukas priekškatījums";
            this.mnuPrintPreview.Click += new System.EventHandler(this.mnuPrintPreview_Click);
            // 
            // frmAddGlyph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 328);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.imgGlyphImage);
            this.Controls.Add(this.pnlGlyphColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.cmbSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.grpGlyph);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddGlyph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Jauna raksta izveide";
            this.Load += new System.EventHandler(this.frmEditGlyph_Load);
            this.grpGlyph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgGlyphImage)).EndInit();
            this.cxmGlyphEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpGlyph;
        private System.Windows.Forms.ColorDialog colorDialog;
        private GlyphEditorControl glyphEditor;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Panel pnlGlyphColor;
        private System.Windows.Forms.PictureBox imgGlyphImage;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.ContextMenuStrip cxmGlyphEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintPreview;
    }
}