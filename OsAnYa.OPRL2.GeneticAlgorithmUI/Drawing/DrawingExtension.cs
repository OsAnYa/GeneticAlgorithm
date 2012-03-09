using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;
using System.Drawing;

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    public static class DrawingExtension
    {
        public static void DrawMesh(this DrawingDesc desc, Mesh mesh, Pen pen)
        {
            foreach (var item in mesh.Lines)
            {
                desc.AddLine((float)item.P1.X1, (float)item.P1.X2, (float)item.P2.X1, (float)item.P2.X2, pen);
            }
        }

        public static void DrawGeneration(this DrawingDesc desc, Generation generation, Pen pen)
        {

        }
    }
}
