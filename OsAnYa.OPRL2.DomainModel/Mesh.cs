using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class Mesh
    {
        public List<Chromosome> Chromosomes { get; set; }
        public List<Line> Lines { get; set; }
        public Mesh()
        {
            Chromosomes = new List<Chromosome>();
            Lines = new List<Line>();
        }
    }
}
