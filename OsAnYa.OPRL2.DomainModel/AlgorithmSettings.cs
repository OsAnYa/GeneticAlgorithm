using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class AlgorithmSettings
    {
        public InitialLoadType InitialLoadType { get; set; }
        public OptimizationModel OptModel { get; set; }
        public int InitialPointCount { get; set; }
    }
}
