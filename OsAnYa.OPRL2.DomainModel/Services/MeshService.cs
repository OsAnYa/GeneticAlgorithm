using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.Services
{
    public static class MeshService
    {
        public static Mesh GetMesh(OptimizationModel optModel, int pointCount, InitialLoadType type)
        {
            Mesh mesh = null;
            switch (type)
            {
                case InitialLoadType.Box:
                    mesh = GetBoxMesh(optModel, pointCount); break;
                case InitialLoadType.TriangleMesh:
                    mesh = GetTriangMesh(optModel, pointCount); break;
                default:
                    break;
            }

            return mesh;
        }

        private static Mesh GetBoxMesh(OptimizationModel optModel, int pointCount)
        {
            Mesh mesh = new Mesh();
            for (double y = -10; y <= 10; y += 10)
                for (double x = -10; x <= 10; x += 10)
                    mesh.nodes.Add(new Point(optModel, x, y));

            for (double y = -5; y <= 5; y += 5)
                for (double x = -5; x <= 5; x += 5)
                    mesh.nodes.Add(new Point(optModel, x, y));

            for (int i = 0; i <= 8; i++)
                mesh.Lines.Add(new Line() { P1 = mesh.nodes[4], P2 = mesh.nodes[i] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[0], P2 = mesh.nodes[2] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[0], P2 = mesh.nodes[6] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[8], P2 = mesh.nodes[2] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[8], P2 = mesh.nodes[6] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[9], P2 = mesh.nodes[11] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[9], P2 = mesh.nodes[15] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[17], P2 = mesh.nodes[11] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[17], P2 = mesh.nodes[15] });


            for (int i = 0; i < pointCount; i++)
                mesh.Chromosomes.Add(new Chromosome(optModel, mesh.nodes[i].X1, mesh.nodes[i].X2));

            return mesh;
        }

        private static Mesh GetTriangMesh(OptimizationModel optModel, int pointCount)
        {
            Mesh mesh = new Mesh();
            for (double y = -10; y <= 10; y += 10)
                for (double x = -10; x <= 9.8; x += 6.6)
                    mesh.nodes.Add(new Point(optModel, x, y));

            for (double y = -5; y <= 5; y += 10)
                for (double x = -6.7; x <= 6.5; x += 6.6)
                    mesh.nodes.Add(new Point(optModel, x, y));



            mesh.Lines.Add(new Line() { P1 = mesh.nodes[0], P2 = mesh.nodes[3] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[0], P2 = mesh.nodes[10] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[0], P2 = mesh.nodes[8] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[11], P2 = mesh.nodes[3] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[11], P2 = mesh.nodes[8] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[11], P2 = mesh.nodes[1] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[9], P2 = mesh.nodes[4] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[9], P2 = mesh.nodes[1] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[9], P2 = mesh.nodes[3] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[10], P2 = mesh.nodes[2] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[10], P2 = mesh.nodes[7] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[10], P2 = mesh.nodes[0] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[2], P2 = mesh.nodes[7] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[2], P2 = mesh.nodes[8] });
            mesh.Lines.Add(new Line() { P1 = mesh.nodes[4], P2 = mesh.nodes[1] });

            for (int i = 0; i < pointCount; i++)
                mesh.Chromosomes.Add(new Chromosome(optModel, mesh.nodes[i].X1, mesh.nodes[i].X2));


            return mesh;
        }


    }
}
