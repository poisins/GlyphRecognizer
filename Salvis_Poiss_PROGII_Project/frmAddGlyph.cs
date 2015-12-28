using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Printing;

namespace Salvis_Poiss_PROGII_Project
{
    public partial class frmAddGlyph : Form
    {
        string fileName = "";
        string imagePath = "";

        int glyphSize = 0;
        Glyph glyph;
        GlyphPrinter glyphPrinter;

        string dbFileString = Application.StartupPath + "\\glyphDB.mdb";
        OleDbConnection connection = new OleDbConnection();

        bool error = false;

        public frmAddGlyph()
        {
            InitializeComponent();
            glyph = new Glyph();
            glyphPrinter = new GlyphPrinter();
            glyphEditor.ContextMenuStrip = cxmGlyphEditor;
        }

        private void frmEditGlyph_Load(object sender, EventArgs e)
        {
            clearForm();
        }

        void clearForm()
        {
            error = false;
            txtName.Text = "";
            imagePath = "";
            fileName = "";
            pnlGlyphColor.BackColor = Color.Red;
            imgGlyphImage.Image = null;
            fillSizes();
        }

        void fillSizes()
        {
            cmbSize.Items.Clear();
            for (int i = 5; i < 10; i++)
            {
                cmbSize.Items.Add((object)(i+"x"+i));
            }
            cmbSize.SelectedIndex = 0;
        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            // Atver krāsu izvēlni
            DialogResult colorResult = colorDialog.ShowDialog();

            // Ja lietotājs ir nospiedis OK
            if (colorResult == DialogResult.OK)
            {
                pnlGlyphColor.BackColor = colorDialog.Color;
            }
        }

        private void imgGlyphImage_Click(object sender, EventArgs e)
        {
            string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.InitialDirectory = desktopFolder;
            imageDialog.Filter = "Pictures|*.jpg;*.bmp;*.png;*.jpeg"; // Atļaujam tikai JPG, BMP un PNG attēlus
            imageDialog.Title = "Izvēlies attēlu rakstam";

            DialogResult imageDialogResult = imageDialog.ShowDialog();

            if (imageDialogResult == DialogResult.OK)
            {
                imagePath = imageDialog.FileName;
                fileName = imageDialog.SafeFileName;
                imgGlyphImage.Image = Image.FromFile(imagePath);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sizeString = cmbSize.SelectedItem.ToString();
            glyphSize = int.Parse(sizeString[0].ToString());
            glyphEditor.GlyphData = new byte[glyphSize, glyphSize];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!glyph.CheckIfEveryRowColumnHasValue(glyphEditor.GlyphData))
            {
                MessageBox.Show("Rakstam jāsatur vismaz viena balta šūna katrā rindā un kolonnā.", "Kļūda");
                error = true;
                return;
            }

            if (glyph.CheckIfRotationInvariant(glyphEditor.GlyphData))
            {
                MessageBox.Show("Raksts neatbalsta rotāciju (izskatās tāds pats, kad ir pagriezts).", "Kļūda");
                error = true;
                return;
            }

            if (!notInDB(glyph.GlyphDataToString(glyphEditor.GlyphData)))
            {
                MessageBox.Show("Šāds raksts jau pastāv.", "Kļūda");
                error = true;
                return;
            }

            if (imagePath=="")
            {
                MessageBox.Show("Nav izvēlēts attēls raksta aizvietošanai.", "Kļūda");
                error = true;
                return;
            }

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Nav norādīts raksta vārds.", "Kļūda");
                error = true;
                return;
            }

            string imgName = copyImage();
            if (error) return;

            saveInDB(imgName);
            if (error) return;

            clearForm();
        }

        bool notInDB(string glyphDataString)
        {
            bool notFound = true;

            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM glyphs WHERE glyph_data='"+ glyphDataString +"'", connection);
            if (connection.State == ConnectionState.Open)
            {
                try
                {
                    object countRead = cmd.ExecuteScalar();
                    int count = int.Parse(countRead.ToString());
                    if (count == 0)
                    {
                        notFound = true;
                    }
                    else
                    {
                        notFound = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda: " + ex);
                    notFound = false;
                }
                finally
                {
                    connection.Close();
                }
            }

            return notFound;
        }

        string copyImage()
        {
            try
            {
                string imageName = fileName;
                string uploadFolderPath = Application.StartupPath + "\\Images";

                // Ja mape neeksistē, izveido to
                bool exists = Directory.Exists(uploadFolderPath);
                if (!exists)
                    Directory.CreateDirectory(uploadFolderPath);

                // Ja šāds fails jau eksistē, pieliek priekšā 0 tā nosaukumam
                exists = File.Exists(uploadFolderPath + "\\" + imageName);
                while(exists){
                    imageName = "0" + imageName;
                    exists = File.Exists(uploadFolderPath + "\\" + imageName);
                }

                Image bitmap = Image.FromFile(imagePath);
                bitmap.Save(uploadFolderPath + "\\" + imageName);
                bitmap.Dispose();

                error = false;
                return imageName;
            }
            catch { error = true; return ""; }
        }

        void saveInDB(string imageName)
        {
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO glyphs(glyph_name, glyph_data, glyph_size, glyph_color, glyph_image)" +
                                                "VALUES (@glyph_name, @glyph_data, @glyph_size, @glyph_color, @glyph_image)", connection);
            if (connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@glyph_name", OleDbType.VarChar).Value = txtName.Text.Trim();
                cmd.Parameters.Add("@glyph_data", OleDbType.VarChar).Value = glyph.GlyphDataToString(glyphEditor.GlyphData);
                cmd.Parameters.Add("@glyph_size", OleDbType.Integer).Value = glyphSize;
                cmd.Parameters.Add("@glyph_color", OleDbType.VarChar).Value = glyph.ColorToRGB(pnlGlyphColor.BackColor);
                cmd.Parameters.Add("@glyph_image", OleDbType.VarChar).Value = imageName;
                try
                {
                    cmd.ExecuteNonQuery();
                    error = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda: " + ex);
                    error = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();

            if (!error)
            {
                btnClose.PerformClick();
            }
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            glyphPrinter.Print(glyphEditor.GlyphData, glyphSize);
        }

        private void mnuPrintPreview_Click(object sender, EventArgs e)
        {
            glyphPrinter.PrintPreview(glyphEditor.GlyphData, glyphSize);
        }
    }
}
