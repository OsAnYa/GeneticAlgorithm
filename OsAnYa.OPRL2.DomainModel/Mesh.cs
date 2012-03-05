using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class Mesh
    {
        public List<Point> Points { get; set; }
        public List<Line> Lines { get; set; }
        public Mesh()
        {
            Points = new List<Point>();
            Lines = new List<Line>();
        }
    }
}
