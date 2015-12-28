using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Salvis_Poiss_PROGII_Project
{
    public class GlyphFromDB
    {
        int id;
        string name;
        byte [,] data;
        int size;
        Color color;
        string image;
        Bitmap picture;

        public GlyphFromDB(string id, string name, string data, string size, string color, string image)
        {
            this.id = int.Parse(id);
            this.name = name;
            this.size = int.Parse(size);
            this.data = GlyphDataFromString(data, this.size);
            this.color = colorParser(color);
            this.image = image;
            this.picture = loadInMemory(this.image);
        }

        // Ielādē attēlu operatīvajā atmiņā
        private Bitmap loadInMemory(string imageName)
        {
            FileStream stream = new FileStream("Images\\" + imageName, FileMode.Open, FileAccess.Read);
            Bitmap pic = (Bitmap)Bitmap.FromStream(stream);
            stream.Close();
            stream.Dispose();

            return pic;
        }

        // Datubāzē glabātās simbolu virknes konvertēšana krāsā
        private Color colorParser(string colorString)
        {
            // Iegūstam masīvu ar 3 string tipa objektiem
            var splitString = colorString.Split(',');

            // Konvertējam katru stringu uz int'u. Iegūstam masīvu ar 3 int tipa elementiem.
            var splitInts = splitString.Select(item => int.Parse(item)).ToArray();

            // Atgriežam krāsu, kas iegūta no 3 skatiļiem
            return System.Drawing.Color.FromArgb(splitInts[0], splitInts[1], splitInts[2]);
        }

        // Datu bāzē glabātās simbolu virknes konvertēšana raksta 2D masīvā
        private static byte[,] GlyphDataFromString(string glyphStrData, int glyphSize)
        {
            byte[,] glyphData = new byte[glyphSize, glyphSize];

            for (int i = 0, k = 0; i < glyphSize; i++)
            {
                for (int j = 0; j < glyphSize; j++, k++)
                {
                    glyphData[i, j] = (byte)((glyphStrData[k] == '0') ? 0 : 1);
                }
            }

            return glyphData;
        }

        public int ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public byte [,] Data
        {
            get { return data; }
        }

        public int Size
        {
            get { return size; }
        }

        public Color Color
        {
            get { return color; }
        }

        public string Image
        {
            get { return image; }
        }

        public Bitmap Picture
        {
            get { return picture; }
        }

        // Teksts, kādu rādīt ListView'ā, zem ikonas
        public override string ToString()
        {
            return name + " " + size + "x" + size;
        }
    }
}
