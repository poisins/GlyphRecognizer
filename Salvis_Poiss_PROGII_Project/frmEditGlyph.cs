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
    public partial class frmEditGlyph : Form
    {
        string fileName = "";
        string oldFileName = "";
        string imagePath = "";

        List<GlyphFromDB> glyphsLoadedList = new List<GlyphFromDB>();
        ImageList glyphsIconList = new ImageList();

        int glyphID = 0;
        int glyphSize = 0;
        Glyph glyph;
        GlyphPrinter glyphPrinter;

        bool error = false;

        string dbFileString = Application.StartupPath + "\\glyphDB.mdb";
        OleDbConnection connection = new OleDbConnection();

        public frmEditGlyph()
        {
            InitializeComponent();

            glyph = new Glyph();
            glyphPrinter = new GlyphPrinter();

            glyphsIconList.ImageSize = new Size(32, 32);
            lstGlyphCollection.LargeImageList = glyphsIconList;
            glyphEditor.ContextMenuStrip = cxmGlyphEditor;
        }

        private void frmEditGlyph_Load(object sender, EventArgs e)
        {
            clearFields();
            getGlyphList();
            createIcons();
        }

        void createIcons()
        {
            foreach (GlyphFromDB glyphDB in glyphsLoadedList)
            {
                glyphsIconList.Images.Add(glyphDB.ID.ToString(), glyph.CreateGlyphIcon(glyphDB.Data, glyphDB.Size, 32));
                ListViewItem lvi = lstGlyphCollection.Items.Add(glyphDB.ToString(), glyphDB.ID.ToString());
                lvi.Tag = glyphDB;
            }
        }

        void clearFields()
        {
            glyphsIconList.Images.Clear();
            glyphsLoadedList.Clear();
            lstGlyphCollection.Clear();

            glyphID = 0;
            glyphSize = 0;
            fileName = "";
            oldFileName = "";

            txtName.Text = "";
            txtSize.Text = "";
            pnlGlyphColor.BackColor = Color.Red;
            imgGlyphImage.Image = null;
            glyphEditor.GlyphData = null;

            txtName.Enabled = false;
            txtSize.Enabled = false;
            pnlGlyphColor.Enabled = false;
            imgGlyphImage.Enabled = false;
            btnSave.Enabled = false;
            btnSaveAndClose.Enabled = false;
            btnDelete.Enabled = false;
            glyphEditor.Enabled = false;
        }

        void getGlyphList()
        {
            OleDbDataReader reader;
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM glyphs ORDER BY glyph_size, glyph_name", connection);
            if (connection.State == ConnectionState.Open)
            {
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        object readID = reader["ID"];
                        object readName = reader["glyph_name"];
                        object readData = reader["glyph_data"];
                        object readSize = reader["glyph_size"];
                        object readColor = reader["glyph_color"];
                        object readImage = reader["glyph_image"];

                        glyphsLoadedList.Add(new GlyphFromDB(readID.ToString(), readName.ToString(), readData.ToString(),
                            readSize.ToString(), readColor.ToString(), readImage.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda: " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void lstGlyphCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstGlyphCollection.SelectedItems != null)
                {
                    ListViewItem lvi = lstGlyphCollection.SelectedItems[0];
                    glyphID = ((GlyphFromDB)lvi.Tag).ID;
                    txtName.Text = ((GlyphFromDB)lvi.Tag).Name;
                    glyphSize = ((GlyphFromDB)lvi.Tag).Size;
                    txtSize.Text = glyphSize + "x" + glyphSize;
                    pnlGlyphColor.BackColor = ((GlyphFromDB)lvi.Tag).Color;

                    FileStream stream = new FileStream("Images\\" + ((GlyphFromDB)lvi.Tag).Image, FileMode.Open, FileAccess.Read);
                    //imgGlyphImage.Image = Bitmap.FromFile("Images\\" + ((GlyphFromDB)lvi.Tag).Image);
                    imgGlyphImage.Image = Bitmap.FromStream(stream);
                    stream.Close();

                    fileName = ((GlyphFromDB)lvi.Tag).Image;
                    oldFileName = ((GlyphFromDB)lvi.Tag).Image;
                    glyphEditor.GlyphData = ((GlyphFromDB)lvi.Tag).Data;

                    txtName.Enabled = true;
                    txtSize.Enabled = true;
                    pnlGlyphColor.Enabled = true;
                    imgGlyphImage.Enabled = true;
                    btnSave.Enabled = true;
                    btnSaveAndClose.Enabled = true;
                    btnDelete.Enabled = true;
                    glyphEditor.Enabled = true;
                }
                else
                {
                    txtName.Enabled = false;
                    txtSize.Enabled = false;
                    pnlGlyphColor.Enabled = false;
                    imgGlyphImage.Enabled = false;
                    btnSave.Enabled = false;
                    btnSaveAndClose.Enabled = false;
                    btnDelete.Enabled = false;
                    glyphEditor.Enabled = false;
                }
            }
            catch { }
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

            if (!notInDB(glyph.GlyphDataToString(glyphEditor.GlyphData), glyphID))
            {
                MessageBox.Show("Šāds raksts jau pastāv.", "Kļūda");
                error = true;
                return;
            }

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Nav norādīts raksta vārds.", "Kļūda");
                error = true;
                return;
            }

            string imgName = fileName;
            if (oldFileName != fileName)
            {
                deleteFile(oldFileName);
                if (error) return;

                imgName = copyImage();
            }
            if (error) return;

            saveInDB(imgName);
            if (error) return;

            clearFields();
            getGlyphList();
            createIcons();
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
                while (exists)
                {
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
            OleDbCommand cmd = new OleDbCommand("UPDATE glyphs" +
                                                " SET glyph_name=@glyph_name, glyph_data=@glyph_data, glyph_color=@glyph_color, glyph_image=@glyph_image"+
                                                " WHERE ID="+glyphID, connection);
            if (connection.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@glyph_name", OleDbType.VarChar).Value = txtName.Text.Trim();
                cmd.Parameters.Add("@glyph_data", OleDbType.VarChar).Value = glyph.GlyphDataToString(glyphEditor.GlyphData);
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

        bool notInDB(string glyphDataString, int glyphID)
        {
            bool notFound = true;

            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM glyphs WHERE glyph_data='" + glyphDataString + "' AND ID <> " + glyphID + ";", connection);
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

        private void pnlGlyphColor_Click(object sender, EventArgs e)
        {
            // Atver krāsu izvēlni
            ColorDialog colorDialog = new ColorDialog();
            DialogResult colorResult = colorDialog.ShowDialog();

            // Ja lietotājs ir nospiedis OK
            if (colorResult == DialogResult.OK)
            {
                pnlGlyphColor.BackColor = colorDialog.Color;
            }
        }

        void deleteFile(string image)
        {
            try
            {
                imgGlyphImage.Image.Dispose();
                imgGlyphImage.Image = null;
                File.Delete(Application.StartupPath + "\\Images\\" + image);
                error = false;
            }
            catch
            {
                error = true;
            }
        }

        void deleteFromDB(int glyphID, string image)
        {
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            OleDbCommand cmd = new OleDbCommand("DELETE FROM glyphs WHERE ID=" + glyphID, connection);
            if (connection.State == ConnectionState.Open)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    deleteFile(image);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kļūda: " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vai tiešām vēlaties dzēst rakstu?", "Raksta dzēšana", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                deleteFromDB(glyphID, oldFileName);
                clearFields();
                getGlyphList();
                createIcons();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                btnSave.PerformClick();
            }

            if (!error)
            {
                btnClose.PerformClick();
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
