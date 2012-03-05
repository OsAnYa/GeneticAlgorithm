using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    class TextCord
    {
        public string str;
        public float X;
        public float Y;
        public System.Drawing.Font font;
        public System.Drawing.Brush brush;
        public TextCord(string st, float x, float y, System.Drawing.Font f, System.Drawing.Brush b)
        {
            X = x;
            Y = y;
            font = f;
            brush = b;
            str = st;
        }
    }
}
