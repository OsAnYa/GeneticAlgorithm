using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.Services;

namespace OsAnYa.OPRL2.DomainModel
{
    public class Generation
    {
        public bool IsSolved { get; set; }
        public double BestF { get; private set; }


        public int Id { get; private set; }
        public AlgorithmSettings Settings { get; set; }
        public Generation Previous { get; private set; }


        public List<Chromosome> InitialChromosomes { get; private set; }
        public List<Chromosome> Output { get; private set; }
        public List<ChromosomePair> Pairs1 { get; private set; }
        public List<ChromosomePair> Pairs2 { get; private set; }
        public List<Chromosome> AllFinishChromosome { get; private set; }
        public List<Chromosome> AfterFirstMutation { get; private set; }
        public List<Chromosome> Winners1 { get; private set; }
        public List<Chromosome> Winners2 { get; private set; }
        public List<ChromosomePair> PairsForGeneticOperation { get; private set; }
        public Dictionary<Chromosome, Chromosome> FirstMutation { get; private set; }
        public List<Chromosome> GeneticOperationRezults { get; private set; }

        public Chromosome BestChromosome { get; set; }

        public bool IsFinal
        {
            get
            {
                Solve();
                bool rezult = false;

                switch (Settings.EndCondition)
                {
                    case EndCondition.MaxGenCount:
                        {
                            if (Id >= Settings.MaxGenerationCount)
                                rezult = true;
                            break;
                        }
                    default:
                        {
                            rezult = true;
                            break;
                        }
                }

                return rezult;
            }
        }

        public Generation(IEnumerable<Chromosome> chromosomes, AlgorithmSettings settings)
        {
            Id = 0;
            InitialChromosomes = chromosomes.ToList();
            Settings = settings;
            Previous = null;

        }
        public Generation(Generation previous)
        {
            if (!previous.IsSolved)
                throw new ArgumentException("previous generation must be solved");
            Id = previous.Id + 1;

            Previous = previous;
            Settings = previous.Settings;
            InitialChromosomes = Previous.Output;
        }

        public void Solve()
        {

            FirstMutation = new Dictionary<Chromosome, Chromosome>();

            foreach (var item in InitialChromosomes)
            {
                Chromosome chr = item.Mutate(Settings.MutationChance);
                chr.GId = Id;
                if (!item.EqualByGenes(chr))
                    if (chr.IsValid())
                        FirstMutation.Add(item, chr);
            }

            AfterFirstMutation = new List<Chromosome>();
            AfterFirstMutation.AddRange(InitialChromosomes);
            AfterFirstMutation.AddRange(FirstMutation.Values.AsEnumerable());

            Pairs1 = new List<ChromosomePair>();
            for (int i = 0; i < AfterFirstMutation.Count / 2; i++)
            {
                Pairs1.Add(SelectionService.GetPair(AfterFirstMutation, Settings.SelectionType));
            }

            Winners1 = Pairs1.Select(p => (p.Chr1.F > p.Chr2.F) ? p.Chr1 : p.Chr2).ToList();

            Pairs2 = new List<ChromosomePair>();
            for (int i = 0; i < AfterFirstMutation.Count / 2; i++)
            {
                Pairs2.Add(SelectionService.GetPair(AfterFirstMutation, Settings.SelectionType));
            }

            Winners2 = Pairs2.Select(p => (p.Chr1.F > p.Chr2.F) ? p.Chr1 : p.Chr2).ToList();

            PairsForGeneticOperation = new List<ChromosomePair>();
            for (int i = 0; i < Winners1.Count; i++)
            {
                PairsForGeneticOperation.Add(new ChromosomePair() { Chr1 = Winners1[i], Chr2 = Winners2[i] });
            }

            GeneticOperationRezults = new List<Chromosome>();
            foreach (var item in PairsForGeneticOperation)
            {
                List<Chromosome> pairRezult = CrossingService.DoCrossing(item, Settings.CrossingGenNumber, Settings.MutationChance);
                for (int i = 0; i < pairRezult.Count; i++)
                {
                    pairRezult[i].GId = Id;
                }
                GeneticOperationRezults.AddRange(pairRezult.Where(p => p.IsValid()));
            }

            AllFinishChromosome = new List<Chromosome>();
            AllFinishChromosome.AddRange(AfterFirstMutation);
            AllFinishChromosome.AddRange(GeneticOperationRezults);

            Output = AllFinishChromosome.OrderByDescending(ch => ch.F).Take(Settings.SurvivedCount).ToList();
            BestChromosome = Output[0];

            IsSolved = true;
        }
    }
}
