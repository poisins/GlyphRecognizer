using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace Salvis_Poiss_PROGII_Project
{
    // Nodrošina raksta drukāšanu.
    // labais peles klikšķis uz raksta veidotāja - Pievienošanas un labošanas logos.
    public class GlyphPrinter
    {
        Glyph glyph;

        public GlyphPrinter() { glyph = new Glyph(); }

        byte[,] data;
        int size;

        public  void Print(byte[,] GlyphData, int GlyphSize)
        {
            data = GlyphData;
            size = GlyphSize;

            PrintDocument pDoc = new PrintDocument();
            pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPage);

            PrintDialog pDialog = new PrintDialog();
            DialogResult pDialogResult = pDialog.ShowDialog();
            if (pDialogResult == DialogResult.OK)
            {
                pDoc.Print();
            }
        }

        public void PrintPreview(byte[,] GlyphData, int GlyphSize)
        {
            data = GlyphData;
            size = GlyphSize;

            PrintDocument pDoc = new PrintDocument();
            pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPage);

            PrintPreviewDialog pPreview = new PrintPreviewDialog();
            pPreview.Document = pDoc;

            // atslēdzam drukāšanas pogu - tā neļauj izvēlēties drukāšanas iespējas - printeri utml.
            ((ToolStripButton)((ToolStrip)pPreview.Controls[1]).Items[0]).Enabled = false;

            pPreview.ShowDialog();
        }

        void pDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            double factor = 2.54;
            int wPixel = Convert.ToInt32((15.0 / factor) * (double)100);

            Bitmap imageToProcess = glyph.CreateGlyphIcon(data, size, wPixel);
            Graphics toDraw = Graphics.FromImage(imageToProcess);

            toDraw.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            toDraw.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            toDraw.DrawImage(imageToProcess, new Rectangle(0, 0, wPixel, wPixel));

            e.Graphics.DrawImage(
                imageToProcess,
                new Rectangle((int)x, (int)y, imageToProcess.Width, imageToProcess.Height),
                0f, 0f, (float)wPixel, (float)wPixel, GraphicsUnit.Pixel);
        }
    }
}
