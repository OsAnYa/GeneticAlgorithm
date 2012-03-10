using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OsAnYa.OPRL2.DomainModel;
using OsAnYa.OPRL2.Services;

namespace OsAnYa.OPRL2.Core
{
    public class PGA
    {
        public Mesh InitialMesh { get; private set; }
        public List<Generation> Generations { get; private set; }
        public Chromosome Best { get; private set; }
        public int CallCount { get; private set; }
        public void Run(AlgorithmSettings settings)
        {
            settings.OptModel.ResetCallCount();
            InitialMesh = MeshService.GetMesh(settings.OptModel, settings.InitialPointCount, settings.InitialLoadType);
            Generations = new List<Generation>();
            Generations.Add(new Generation(InitialMesh.Chromosomes, settings));
            int i = 0;
            while (!Generations[i].IsFinal)
            {
                Generations.Add(new Generation(Generations[i]));
                i++;
            }
            CallCount = settings.OptModel.CallCount;
            Best = Generations[i].BestChromosome;
        }
    }
}
