using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OsAnYa.OPRL2.DomainModel;
using OsAnYa.OPRL2.Services;

namespace OsAnYa.OPRL2.Core
{
    public class GeneticAlgorithm
    {
        public Mesh InitialMesh { get; private set; }
        public void Run(AlgorithmSettings settings)
        {
            InitialMesh = MeshService.GetMesh(settings.OptModel, settings.InitialPointCount, settings.InitialLoadType);
        }
    }
}
