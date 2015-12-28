namespace Salvis_Poiss_PROGII_Project
{
    partial class frmEditGlyph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditGlyph));
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblImage = new System.Windows.Forms.Label();
            this.imgGlyphImage = new System.Windows.Forms.PictureBox();
            this.pnlGlyphColor = new System.Windows.Forms.Panel();
            this.lblColor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpGlyph = new System.Windows.Forms.GroupBox();
            this.glyphEditor = new Salvis_Poiss_PROGII_Project.GlyphEditorControl();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpGlyphList = new System.Windows.Forms.GroupBox();
            this.lstGlyphCollection = new System.Windows.Forms.ListView();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cxmGlyphEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imgGlyphImage)).BeginInit();
            this.grpGlyph.SuspendLayout();
            this.grpGlyphList.SuspendLayout();
            this.cxmGlyphEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(337, 228);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(186, 23);
            this.btnSaveAndClose.TabIndex = 24;
            this.btnSaveAndClose.Text = "Saglabāt un aizvērt";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(337, 293);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(186, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Aizvērt logu";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(337, 199);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Saglabāt";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(462, 87);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(33, 13);
            this.lblImage.TabIndex = 21;
            this.lblImage.Text = "Attēls";
            // 
            // imgGlyphImage
            // 
            this.imgGlyphImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgGlyphImage.Location = new System.Drawing.Point(433, 103);
            this.imgGlyphImage.Name = "imgGlyphImage";
            this.imgGlyphImage.Size = new System.Drawing.Size(90, 90);
            this.imgGlyphImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgGlyphImage.TabIndex = 20;
            this.imgGlyphImage.TabStop = false;
            this.imgGlyphImage.Click += new System.EventHandler(this.imgGlyphImage_Click);
            // 
            // pnlGlyphColor
            // 
            this.pnlGlyphColor.BackColor = System.Drawing.Color.Red;
            this.pnlGlyphColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGlyphColor.Location = new System.Drawing.Point(337, 103);
            this.pnlGlyphColor.Name = "pnlGlyphColor";
            this.pnlGlyphColor.Size = new System.Drawing.Size(90, 90);
            this.pnlGlyphColor.TabIndex = 19;
            this.pnlGlyphColor.Click += new System.EventHandler(this.pnlGlyphColor_Click);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(365, 87);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 18;
            this.lblColor.Text = "Krāsa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Izmērs:";
            // 
            // grpGlyph
            // 
            this.grpGlyph.Controls.Add(this.glyphEditor);
            this.grpGlyph.Location = new System.Drawing.Point(529, 7);
            this.grpGlyph.Name = "grpGlyph";
            this.grpGlyph.Size = new System.Drawing.Size(303, 315);
            this.grpGlyph.TabIndex = 15;
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
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(337, 23);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 20);
            this.txtName.TabIndex = 14;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(334, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 13);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Nosaukums:";
            // 
            // grpGlyphList
            // 
            this.grpGlyphList.Controls.Add(this.lstGlyphCollection);
            this.grpGlyphList.Location = new System.Drawing.Point(4, 7);
            this.grpGlyphList.Name = "grpGlyphList";
            this.grpGlyphList.Size = new System.Drawing.Size(324, 315);
            this.grpGlyphList.TabIndex = 25;
            this.grpGlyphList.TabStop = false;
            this.grpGlyphList.Text = "Rakstu datubāze";
            // 
            // lstGlyphCollection
            // 
            this.lstGlyphCollection.Location = new System.Drawing.Point(6, 19);
            this.lstGlyphCollection.MultiSelect = false;
            this.lstGlyphCollection.Name = "lstGlyphCollection";
            this.lstGlyphCollection.Size = new System.Drawing.Size(312, 290);
            this.lstGlyphCollection.TabIndex = 0;
            this.lstGlyphCollection.UseCompatibleStateImageBehavior = false;
            this.lstGlyphCollection.SelectedIndexChanged += new System.EventHandler(this.lstGlyphCollection_SelectedIndexChanged);
            // 
            // txtSize
            // 
            this.txtSize.BackColor = System.Drawing.SystemColors.Window;
            this.txtSize.Location = new System.Drawing.Point(380, 49);
            this.txtSize.MaxLength = 20;
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(143, 20);
            this.txtSize.TabIndex = 26;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(337, 257);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(186, 23);
            this.btnDelete.TabIndex = 27;
            this.btnDelete.Text = "Dzēst";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            // frmEditGlyph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 328);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.grpGlyphList);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.imgGlyphImage);
            this.Controls.Add(this.pnlGlyphColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpGlyph);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditGlyph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rakstu labošana";
            this.Load += new System.EventHandler(this.frmEditGlyph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgGlyphImage)).EndInit();
            this.grpGlyph.ResumeLayout(false);
            this.grpGlyphList.ResumeLayout(false);
            this.cxmGlyphEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.PictureBox imgGlyphImage;
        private System.Windows.Forms.Panel pnlGlyphColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpGlyph;
        private GlyphEditorControl glyphEditor;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grpGlyphList;
        private System.Windows.Forms.ListView lstGlyphCollection;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ContextMenuStrip cxmGlyphEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintPreview;
    }
}