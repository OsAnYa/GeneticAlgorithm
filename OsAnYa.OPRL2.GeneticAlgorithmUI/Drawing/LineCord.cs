using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    class LineCord
    {
        public float X1;
        public float X2;
        public float Y1;
        public float Y2;

        public System.Drawing.Pen pen;

        public LineCord(float x1, float y1, float x2, float y2, System.Drawing.Pen p)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;

            pen = p;
        }
    }
}
