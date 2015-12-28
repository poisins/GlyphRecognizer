using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Salvis_Poiss_PROGII_Project
{
    public class Glyph
    {
        public Glyph() { }

        // Pārbaudam, vai rakstam ir malas.
        // Malas ir 1 šūnas platuma melns rāmis rāmis
        // 5x5 izmēra attēlam, derīgā raksta vieta ir tikai 3x3 rūtiņas. Apkārt ir rāmis.
        public bool CheckIfGlyphHasBorder(byte[,] rawGlyphData)
        {
            int size = rawGlyphData.GetLength(0);

            if (size != rawGlyphData.GetLength(1))
            {
                throw new ArgumentException("Nederīgs raksta masīvs. Jābūt kvadrātam!");
            }

            int sizeM1 = size - 1;

            for (int i = 0; i <= sizeM1; i++)
            {
                if (rawGlyphData[0, i] == 1)
                    return false;
                if (rawGlyphData[sizeM1, i] == 1)
                    return false;

                if (rawGlyphData[i, 0] == 1)
                    return false;
                if (rawGlyphData[i, sizeM1] == 1)
                    return false;
            }

            return true;
        } // CheckIfGlyphHasBorder beigas

        // Raksta katrā rindā un kolonnā jābūt vismaz 1 baltai šūnai
        public bool CheckIfEveryRowColumnHasValue(byte[,] rawGlyphData)
        {
            int size = rawGlyphData.GetLength(0);

            if (size != rawGlyphData.GetLength(1))
            {
                throw new ArgumentException("Nederīgs raksta masīvs. Jābūt kvadrātam!");
            }

            int sizeM1 = size - 1;

            byte[] rows = new byte[sizeM1];
            byte[] cols = new byte[sizeM1];

            for (int i = 1; i < sizeM1; i++)
            {
                for (int j = 1; j < sizeM1; j++)
                {
                    byte value = rawGlyphData[i, j];

                    rows[i] |= value;
                    cols[j] |= value;
                }
            }

            for (int i = 1; i < sizeM1; i++)
            {
                if ((rows[i] == 0) || (cols[i] == 0))
                    return false;
            }

            return true;
        } // CheckIfEveryRowColumnHasValue beigas

        // Rakstam jāatbalsta rotācija (nedrīkst izskatīties tāds pats, pat ja pagriezts, piem., pa 180 grādiem)
        public bool CheckIfRotationInvariant(byte[,] rawGlyphData)
        {
            int size = rawGlyphData.GetLength(0);

            if (size != rawGlyphData.GetLength(1))
            {
                throw new ArgumentException("Nederīgs raksta masīvs. Jābūt kvadrātam!");
            }

            int sizeM1 = size - 1;

            bool match2 = true;
            bool match3 = true;
            bool match4 = true;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    byte value = rawGlyphData[i, j];

                    // 180 grādi
                    match2 &= (value == rawGlyphData[sizeM1 - i, sizeM1 - j]);
                    // 90 grādi
                    match3 &= (value == rawGlyphData[sizeM1 - j, i]);
                    // 270 grādi
                    match4 &= (value == rawGlyphData[j, sizeM1 - i]);
                }
            }

            return (match2 || match3 || match4);
        } // CheckIfRotationInvariant beigas

        // Pārveido raksta datu masīvu par simbolu virkni.
        // Simbolu virkne tiek saglabāta datubāzē
        public string GlyphDataToString(byte[,] glyphData)
        {
            StringBuilder sb = new StringBuilder();
            int glyphSize = glyphData.GetLength(0);

            for (int i = 0; i < glyphSize; i++)
            {
                for (int j = 0; j < glyphSize; j++)
                {
                    sb.Append(glyphData[i, j]);
                }
            }

            return sb.ToString();
        } // GlyphDataToString beigas

        // Balstoties uz padoto masīvu, tiek izveidots raksta attēls
        // Tiek izmantots kā ikonas, tā arī izdrukai paredzētā raksta zīmēšanai
        public Bitmap CreateGlyphIcon(byte [,] glyphData, int glyphSize, int iconWidth)
        {
            Bitmap bitmap = new Bitmap(iconWidth, iconWidth, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int cellSize = iconWidth / glyphSize;

            for (int i = 0; i < iconWidth; i++)
            {
                int yCell = i / cellSize;

                for (int j = 0; j < iconWidth; j++)
                {
                    int xCell = j / cellSize;

                    if ((yCell >= glyphSize) || (xCell >= glyphSize))
                    {
                        // ievieto caurspīdīgu pikseli, ja tas atrodas ārpus raksta
                        bitmap.SetPixel(j, i, Color.Transparent);
                    }
                    else
                    {
                        // ievieto melnu vai baltu pikseli, balstoties uz raksta šūnas vērtību
                        bitmap.SetPixel(j, i,
                            (glyphData[yCell, xCell] == 0) ? Color.Black : Color.White);
                    }
                }
            }

            return bitmap;
        } // CreateGlyphIcon beigas

        // Konvertējam atlasīto krāsu simbolu virknē.
        // Izmantots krāsas vērtības uzglabāšanai datubāzē
        public string ColorToRGB(Color color)
        {
            return color.R + "," + color.G + "," + color.B;
        } // ColorToRGB beigas

        // Salīdzina rakstus. Atgriež rotāciju grādos 
        public int CheckForMatching(byte[,] rawGlyphData, byte[,] databaseGlyphData)
        {
            int size = rawGlyphData.GetLength(0);
            int sizeM1 = size - 1;

            bool match1 = true;
            bool match2 = true;
            bool match3 = true;
            bool match4 = true;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    byte value = rawGlyphData[i, j];

                    // nav rotācijas
                    match1 &= (value == databaseGlyphData[i, j]);
                    // 180 grādi
                    match2 &= (value == databaseGlyphData[sizeM1 - i, sizeM1 - j]);
                    // 90 grādi
                    match3 &= (value == databaseGlyphData[sizeM1 - j, i]);
                    // 270 grādi
                    match4 &= (value == databaseGlyphData[j, sizeM1 - i]);
                }
            }

            if (match1)
                return 0;
            else if (match2)
                return 180;
            else if (match3)
                return 90;
            else if (match4)
                return 270;

            return -1;
        }// CheckForMatching beigas

    }
}
