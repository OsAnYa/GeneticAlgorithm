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
            Point p1 = new Point(optModel, 0, 0);
            Point p2 = new Point(optModel, 10, 0);
            Point p3 = new Point(optModel, -10, -10);
            Point p4 = new Point(optModel, 0, -10);
            Point p5 = new Point(optModel, 10, 10);

            Line l1 = new Line() { P1 = p1, P2 = p5 };

            Mesh mesh = new Mesh();
            mesh.Chromosomes.Add(new Chromosome(optModel, p1.X1, p1.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p2.X1, p2.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p3.X1, p3.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p4.X1, p4.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p5.X1, p5.X2));

            mesh.Lines.Add(l1);

            return mesh;
        }

        private static Mesh GetTriangMesh(OptimizationModel optModel, int pointCount)
        {
            Point p1 = new Point(optModel, 0, 0);
            Point p2 = new Point(optModel, 10, 0);
            Point p3 = new Point(optModel, -10, -10);
            Point p4 = new Point(optModel, 0, -10);
            Point p5 = new Point(optModel, 10, 5);

            Line l1 = new Line() { P1 = p1, P2 = p5 };

            Mesh mesh = new Mesh();
            mesh.Chromosomes.Add(new Chromosome(optModel, p1.X1, p1.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p2.X1, p2.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p3.X1, p3.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p4.X1, p4.X2));
            mesh.Chromosomes.Add(new Chromosome(optModel, p5.X1, p5.X2));

            mesh.Lines.Add(l1);

            return mesh;
        }

    }
}
