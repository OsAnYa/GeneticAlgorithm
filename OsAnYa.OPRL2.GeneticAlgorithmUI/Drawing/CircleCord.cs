using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    class CircleCord
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Rpx { get; set; }
        public bool Fill { get; set; }

        public System.Drawing.Pen pen;

        public CircleCord(float x, float y, float rpx, System.Drawing.Pen p, bool fill = false)
        {
            X = x;
            Y = y;
            Rpx = rpx;
            Fill = fill;

            pen = p;
        }
    }
}
