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
        public SelectionType SelectionType { get; set; }
        public EndCondition EndCondition { get; set; }
        public int MaxGenerationCount { get; set; }
        public int SurvivedCount { get; set; }
        public double MutationChance { get; set; }
        public int CrossingGenNumber { get; set; }
        public double Tolerance { get; set; }
    }
}
