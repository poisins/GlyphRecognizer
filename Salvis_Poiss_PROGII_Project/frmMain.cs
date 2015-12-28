using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.IO;
using System.Media;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using AForge.Math.Geometry;

namespace Salvis_Poiss_PROGII_Project
{
    public partial class frmMain : Form
    {
        // Programmā izmantotie mainīgie
        public FilterInfoCollection usbCams;
        public AsyncVideoSource cam = null; // Asinhrons video avots. Nodrošina vairāku kadru apstrādi paralēli,
                                            // lai nerastos FPS (kadri/sekundē) kritumi

        int glyphSize = 0;
        float confidence;

        List<GlyphFromDB> glyphsLoadedList;
        Glyph glyph;

        string dbFileString = Application.StartupPath + "\\glyphDB.mdb";
        OleDbConnection connection = new OleDbConnection();

        UnmanagedImage image;
        Bitmap frame;

        BlobCounter blobCounter;
        SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
        DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
        BackwardQuadrilateralTransformation backwardQuadrilateralTransformation = new BackwardQuadrilateralTransformation();

        public frmMain()
        {
            InitializeComponent();

            glyph = new Glyph();

            // izveido melno plankumu skaitītāju
            blobCounter = new BlobCounter();
            blobCounter.MinHeight = 32;
            blobCounter.MinWidth = 32;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;
        }

        // atjauno kameru sarakstu
        void listCameras()
        {
            lstCameras.Items.Clear();
            try
            {
                // Iegūst sarakstu ar pieejamajām USB kamerām
                usbCams = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                foreach (FilterInfo camera in usbCams)
                {
                    lstCameras.Items.Add(camera.Name);
                }
            }
            catch { }
            finally
            {
                // ja kameras nav atrastas, kaut ko ievieto sarakstā
                if (lstCameras.Items.Count == 0)
                {
                    lstCameras.Items.Add("No camera found!");
                }
                lstCameras.SelectedIndex = 0;
            }
        } // listCameras() beigas

        void getGlyphSizes()
        {
            try
            {
                glyphsLoadedList = new List<GlyphFromDB>();

                cmbGlyphSize.Items.Clear();
                OleDbDataReader reader;
                connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT glyph_size FROM glyphs GROUP BY glyph_size ORDER BY glyph_size;", connection);
                if (connection.State == ConnectionState.Open)
                {
                    try
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            object readSize = reader["glyph_size"];
                            cmbGlyphSize.Items.Add((object)(readSize.ToString() + "x" + readSize.ToString()));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kļūda: " + ex);
                    }
                    finally
                    {
                        connection.Close();

                        // paņemam noklusējuma sarakstu. Ja saraksts tukšs, neatlasa neko;
                        try
                        {
                            cmbGlyphSize.SelectedIndex = 0;
                        }
                        catch
                        {
                            cmbGlyphSize.SelectedItem = null;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("DB fails nav atrasts!", "Kļūda!");
                btnAddGlyph.Enabled = false;
                btnEditGlyphs.Enabled = false;
            }
        } // getGlyphSizes() beigas

        private void frmMain_Load(object sender, EventArgs e)
        {
            listCameras();
            getGlyphSizes();
            optVisualNone.Select();
        } // frmMain_Load beigas

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            listCameras();
        } // tnRefreshList_Click beigas

        void cam_NewFrame(object sender, NewFrameEventArgs e)
        {
            // Ja nav norādīts raksta izmērs, neveic turpmākās darbības. Tikai ataino kameras kadrus
            if (glyphSize == 0)
            {
                imgCamera.Image = (Bitmap)e.Frame.Clone();
                return;
            }

            if (optVisualNone.Checked) // ja nav nepieciešama vizualizācija, nav vērts turpināt
            {
                imgCamera.Image = (Bitmap)e.Frame.Clone();
                return;
            }

            // 0. - nolasa kameras kadru
            image = UnmanagedImage.FromManagedImage((Bitmap)e.Frame.Clone());
            frame = (Bitmap)e.Frame.Clone();

            // 1. - padara attēlu melnbaltu - samazina apstrādājamās informācijas apjomu
            UnmanagedImage grayImage = null;

            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                grayImage = image;
            }
            else
            {
                grayImage = UnmanagedImage.Create(image.Width, image.Height,
                    PixelFormat.Format8bppIndexed);
                Grayscale.CommonAlgorithms.BT709.Apply(image, grayImage);
            }

            // 2. - nosaka attēla objektu malas/ kontūras
            //DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
            UnmanagedImage edgesImage = edgeDetector.Apply(grayImage);

            // 3. - atsijā nenozīmīgas spilgtuma izmaiņas
            Threshold thresholdFilter = new Threshold(40);
            thresholdFilter.ApplyInPlace(edgesImage);

            // 4. - atrod atsevišķus melnos plankumus
            blobCounter.ProcessImage(edgesImage);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // 5. - pārbauda katru melno plankumu
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                List<IntPoint> corners = null;

                // vai tas izskatās pēc kvadrāta?
                if (shapeChecker.IsQuadrilateral(edgePoints, out corners))
                {
                    // iegūst labās un kreisās malas galējos punktus
                    List<IntPoint> leftEdgePoints, rightEdgePoints;
                    blobCounter.GetBlobsLeftAndRightEdges(blobs[i],
                        out leftEdgePoints, out rightEdgePoints);

                    // aprēķina vidējo atšķirību pikseļu vērtībām, starp formas iekšpusi un ārpusi
                    float diff = CalculateAverageEdgesBrightnessDifference(
                        leftEdgePoints, rightEdgePoints, grayImage);

                    // pārbauda vidējo atšķirību, cik ļoti ārpuse ir gaišāka par iekšpusi
                    if (diff > 20)
                    {
                        // 6. - pārveido atrato kvadrātu par raksta attēlu
                        QuadrilateralTransformation quadrilateralTransformation =
                            new QuadrilateralTransformation(corners, 150, 150);
                        UnmanagedImage glyphImage = quadrilateralTransformation.Apply(grayImage);

                        // iegūst attēlu melnā un baltā krāsā (tikai)
                        OtsuThreshold otsuThresholdFilter = new OtsuThreshold();
                        otsuThresholdFilter.ApplyInPlace(glyphImage);

                        // cenšas atpazīt rakstu
                        byte[,] glyphValues = Recognize(glyphImage,
                                new Rectangle(0, 0, glyphImage.Width, glyphImage.Height), out confidence);

                        if (confidence > 0.6)
                        {
                            if (glyph.CheckIfGlyphHasBorder(glyphValues) &&
                                glyph.CheckIfEveryRowColumnHasValue(glyphValues))
                            {

                                if (glyphsLoadedList.Count != 0)
                                {

                                    try
                                    {
                                        int glyphIndex = -1;
                                        int rotation = -1;
                                        for (int j = 0; i < glyphsLoadedList.Count; j++)
                                        {
                                            // Ja atpazīst rakstu, saglabā tā indeksu ielādēto raskstu sarakstā
                                            rotation = glyph.CheckForMatching(glyphValues, glyphsLoadedList[j].Data);
                                            if (rotation != -1)
                                            {
                                                glyphIndex = j;
                                                break;
                                            }
                                        }

                                        if (rotation != -1)
                                        {
                                            if (optVisualFrame.Checked || optVisualName.Checked)
                                            {
                                                Graphics g = Graphics.FromImage(frame);
                                                Pen pen = new Pen(glyphsLoadedList[glyphIndex].Color, 3);
                                                g.DrawPolygon(pen, ToPointsArray(corners));

                                                if (optVisualName.Checked)
                                                {
                                                    // aprēķina raksta centra punktu
                                                    IntPoint minXY, maxXY;
                                                    PointsCloud.GetBoundingRectangle(corners, out minXY, out maxXY);
                                                    IntPoint center = (minXY + maxXY) / 2;

                                                    string glyphTitle = glyphsLoadedList[glyphIndex].Name;

                                                    System.Drawing.Font defaultFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);

                                                    // raksta vārda izmērs
                                                    SizeF nameSize = g.MeasureString(glyphTitle, defaultFont);

                                                    Brush brush = new SolidBrush(pen.Color);
                                                    System.Drawing.Point centerPoint = new System.Drawing.Point(center.X - (int)nameSize.Width / 2, center.Y - (int)nameSize.Height / 2);

                                                    // drukā nosaukumu
                                                    g.DrawString(glyphTitle, defaultFont, brush, centerPoint);

                                                    brush.Dispose();
                                                } // if Name.Checked

                                                pen.Dispose();
                                            }// if Frame.checked or Name.checked

                                            if (optVisualImage.Checked)
                                            {
                                                // <--! aizvieto atrasto raksta kvadrātu ar attēlu (norādīts datubāzē) !-->

                                                // Iespēja Nr.1 - katru reizi ielādēt no atmiņas (rodas problēma ar attēla dzēšanu - raksta labošasnas/ dzēšanas brīdī)
                                                //1. backwardQuadrilateralTransformation.SourceImage = (Bitmap)Bitmap.FromFile("Images\\" + glyphsLoadedList[glyphIndex].Image);

                                                // Iespēja Nr.2
                                                // Lai nerastos problēmas ar attēlu dzēšanu (labošasnas/ dzēšanas brīdī) attēli tiek straumēti no diska
                                                //2. FileStream stream = new FileStream("Images\\" + glyphsLoadedList[glyphIndex].Image, FileMode.Open, FileAccess.Read);
                                                //2. backwardQuadrilateralTransformation.SourceImage = (Bitmap)Bitmap.FromStream(stream);

                                                // Iespēja Nr.3 - attēls, raksta objekta (glyphFromDB) izveides brīdī tiek ielādēti operatīvajā atmiņā (izmantojot strumēšanu)
                                                // Uz atmiņas patēriņa rēķina, pieaug attēlu parādīšanas ātrums (RAM's ir daudz ātrāks par HDD)
                                                // un samazinās noslodze cietajam diskam. Atrisinās arī problēma ar dzēšanu (attēls tiek nolasīts tikai 1 reizi)
                                                backwardQuadrilateralTransformation.SourceImage = glyphsLoadedList[glyphIndex].Picture;
                                                backwardQuadrilateralTransformation.DestinationQuadrilateral = corners;
                                                backwardQuadrilateralTransformation.ApplyInPlace(frame);

                                                //2. stream.Close();
                                                //2. stream.Dispose(); // Pārtrauc strumēšanu un atbrīvo RAM
                                            } // if Image.Checked
                                        }// if rotation !=-1
                                    }
                                    catch { }
                                } // if glyph count !=0;
                            } // if borders
                        } // if confidence > 60
                    } // if diff>20
                }
            }

            imgCamera.Image = frame;
        } // cam_NewFrame beigas

        private void lstCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                cam.SignalToStop();
                cam.WaitForStop();
            }
            catch { }

            try
            {
                VideoCaptureDevice videoSource = new VideoCaptureDevice(usbCams[lstCameras.SelectedIndex].MonikerString);
                cam = new AsyncVideoSource(videoSource);
                
                // jauna kadra notikumu apstrāde
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
            }
            catch
            {
                //ja kamera nav atrasta, ievieto statisku attēlu
                imgCamera.Image = System.Drawing.Image.FromFile(@"img\no_camera.jpg");
            }
        } // lstCameras_SelectedIndexChanged beigas

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        } // frmMain_FormClosed beigas

        private void btnAddGlyph_Click(object sender, EventArgs e)
        {
            frmAddGlyph addForm = new frmAddGlyph();
            addForm.ShowDialog(this);
            getGlyphSizes();
        }

        const int stepSize = 3;

        // Aprēķina vidējo atšķirību starp pikseļiem ārpusē un iekšpusē
        private float CalculateAverageEdgesBrightnessDifference(
            List<IntPoint> leftEdgePoints,
            List<IntPoint> rightEdgePoints,
            UnmanagedImage image)
        {
            // izveido sarakstu ar punktiem, kas atrodas nedaudz pa labi/kreisi no malām
            List<IntPoint> leftEdgePoints1 = new List<IntPoint>();
            List<IntPoint> leftEdgePoints2 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints1 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints2 = new List<IntPoint>();

            int tx1, tx2, ty;
            int widthM1 = image.Width - 1;

            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                tx1 = leftEdgePoints[k].X - stepSize;
                tx2 = leftEdgePoints[k].X + stepSize;
                ty = leftEdgePoints[k].Y;

                leftEdgePoints1.Add(new IntPoint(
                    (tx1 < 0) ? 0 : tx1, ty));
                leftEdgePoints2.Add(new IntPoint(
                    (tx2 > widthM1) ? widthM1 : tx2, ty));

                tx1 = rightEdgePoints[k].X - stepSize;
                tx2 = rightEdgePoints[k].X + stepSize;
                ty = rightEdgePoints[k].Y;

                rightEdgePoints1.Add(new IntPoint(
                    (tx1 < 0) ? 0 : tx1, ty));
                rightEdgePoints2.Add(new IntPoint(
                    (tx2 > widthM1) ? widthM1 : tx2, ty));
            }

            // apkopo pikseļu vērtības norādītajos punktos
            byte[] leftValues1 = image.Collect8bppPixelValues(leftEdgePoints1);
            byte[] leftValues2 = image.Collect8bppPixelValues(leftEdgePoints2);
            byte[] rightValues1 = image.Collect8bppPixelValues(rightEdgePoints1);
            byte[] rightValues2 = image.Collect8bppPixelValues(rightEdgePoints2);

            // aprēķina vidējo atšķirību pikseļu vērtībām, starp formas iekšpusi un ārpusi
            float diff = 0;
            int pixelCount = 0;

            for (int k = 0; k < leftEdgePoints.Count; k++)
            {
                if (rightEdgePoints[k].X - leftEdgePoints[k].X > stepSize * 2)
                {
                    diff += (leftValues1[k] - leftValues2[k]);
                    diff += (rightValues2[k] - rightValues1[k]);
                    pixelCount += 2;
                }
            }

            return diff / pixelCount;
        } // CalculateAverageEdgesBrightnessDifference beigas

        // Pārveido AForge.IntPoint uz Drawing.Point
        // Nepieciešams priekš līniju zīmēšanas
        private System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            int count = points.Count;
            System.Drawing.Point[] pointsArray = new System.Drawing.Point[count];

            for (int i = 0; i < count; i++)
            {
                pointsArray[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return pointsArray;
        } // ToPointsArray beigas

        private void cmbGlyphSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            glyphsLoadedList.Clear();
            OleDbDataReader reader;
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbFileString;
            connection.Open();
            char size = cmbGlyphSize.SelectedItem.ToString()[0];
            glyphSize = int.Parse(size + "");
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM glyphs WHERE glyph_size=" + size, connection);
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
        }  // cmbGlyphSize_SelectedIndexChanged beigas

        // Atpazīst rakstu melnbaltā attēlā, atgriež bitu masīvu un pārliecību par raksta nolasīšanas korektumu
        byte[,] Recognize(UnmanagedImage image, Rectangle rect, out float confidence)
        {
            int glyphStartX = rect.Left;
            int glyphStartY = rect.Top;

            int glyphWidth = rect.Width;
            int glyphHeight = rect.Height;

            // raksta šūnas/rūtiņas izmērs
            int cellWidth = glyphWidth / glyphSize;
            int cellHeight = glyphHeight / glyphSize;

            // pieļauj nelielu atstarpi katrai šūnai, kas netiks pārbaudīta
            int cellOffsetX = (int)(cellWidth * 0.2);
            int cellOffsetY = (int)(cellHeight * 0.2);

            // šūnas skenējamais izmērs
            int cellScanX = (int)(cellWidth * 0.6);
            int cellScanY = (int)(cellHeight * 0.6);
            int cellScanArea = cellScanX * cellScanY;

            // krāsas summārā intensitāte ketrā raksta šūnā
            int[,] cellIntensity = new int[glyphSize, glyphSize];

            unsafe
            {
                int stride = image.Stride;

                byte* srcBase = (byte*)image.ImageData.ToPointer() +
                    (glyphStartY + cellOffsetY) * stride + glyphStartX + cellOffsetX;
                byte* srcLine;
                byte* src;

                // visām raksta rindām
                for (int gi = 0; gi < glyphSize; gi++)
                {
                    srcLine = srcBase + cellHeight * gi * stride;

                    // visām rindas līnijām
                    for (int y = 0; y < cellScanY; y++)
                    {

                        // visām raksta kolonnām
                        for (int gj = 0; gj < glyphSize; gj++)
                        {
                            src = srcLine + cellWidth * gj;

                            // katram kolonnas pikselim
                            for (int x = 0; x < cellScanX; x++, src++)
                            {
                                cellIntensity[gi, gj] += *src;
                            }
                        }

                        srcLine += stride;
                    }
                }
            }

            // aprēķina ktras raksta šūnas vērtību, un nosaka pārliecības (confidence) līmeni uz mazāko šūnu vērtību
            byte[,] glyphValues = new byte[glyphSize, glyphSize];
            confidence = 1f;

            for (int gi = 0; gi < glyphSize; gi++)
            {
                for (int gj = 0; gj < glyphSize; gj++)
                {
                    float fullness = (float)(cellIntensity[gi, gj] / 255) / cellScanArea;
                    float conf = (float)System.Math.Abs(fullness - 0.5) + 0.5f;

                    glyphValues[gi, gj] = (byte)((fullness > 0.5f) ? 1 : 0);

                    if (conf < confidence)
                        confidence = conf;
                }
            }

            return glyphValues;
        } // Recognize beigas

        private void btnEditGlyphs_Click(object sender, EventArgs e)
        {
            frmEditGlyph editForm = new frmEditGlyph();
            editForm.ShowDialog();
            getGlyphSizes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Apstādina kameru
            try
            {
                cam.SignalToStop();
                cam.WaitForStop();
                //cam.Stop();
            }
            catch { }
        } // frmMain_FormClosing beigas

    } // klases beigas
} // namespace beigas
