using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;


namespace OsAnYa.OPRL2.OptimizationUI
{
    public class OptRezult
    {
        public InitialLoadType I { get; set; }
        public MutationType M { get; set; }
        public SelectionType S { get; set; }
        public EndCondition E { get; set; }
        public double F1 { get; set; }
        public double F2 { get; set; }
        public double F { get; set; }
    }
}
