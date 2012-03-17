using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.Services
{
    public static class SelectionService
    {
        public static ChromosomePair GetPair(List<Chromosome> chromosomes, SelectionType selectionType)
        {

            ChromosomePair pair = null;
            switch (selectionType)
            {
                case SelectionType.Random:
                    {
                        pair = GetPairRandom(chromosomes); break;
                    }
                case SelectionType.Roulette:
                    {
                        pair = GetPairRoulette(chromosomes); break;
                    }
                default:
                    {
                        throw new ArgumentException("unknonw selection type", "selectionType");
                    }
            }

            return pair;
        }

        private static ChromosomePair GetPairRoulette(List<Chromosome> chromosomes)
        {
            Chromosome ch1 = chromosomes[0];
            Chromosome ch2 = chromosomes[1];
            double[] range = new double[chromosomes.Count + 1];
            double min = chromosomes.Min(ch => ch.F);
            for (int i = 0; i < chromosomes.Count; i++)
            {
                range[i + 1] = range[i] + chromosomes[i].F - min;
            }
            double A = range[chromosomes.Count];
            double r1 = Randomizer.Rnd.NextDouble() * A;
            double r2 = Randomizer.Rnd.NextDouble() * A;
            for (int i = 0; i < chromosomes.Count; i++)
            {
                if ((range[i] <= r1) && (r1 <= range[i + 1]))
                    ch1 = chromosomes[i];
                if ((range[i] <= r2) && (r2 <= range[i + 1]))
                    ch2 = chromosomes[i];
            }
            ChromosomePair pair = new ChromosomePair() { Chr1 = ch1, Chr2 = ch2 };

            return pair;
        }

        private static ChromosomePair GetPairRandom(List<Chromosome> chromosomes)
        {
            ChromosomePair pair = new ChromosomePair();
            if (chromosomes.Count < 2)
                throw new ArgumentException("chromosomes count must be >= 2");
            else if (chromosomes.Count == 2)
            {
                pair.Chr1 = chromosomes[0];
                pair.Chr2 = chromosomes[1];
            }
            else
            {
                int i1 = Randomizer.Rnd.Next(chromosomes.Count - 1);
                int i2 = Randomizer.Rnd.Next(chromosomes.Count - 1);
                pair.Chr1 = chromosomes[i1];
                pair.Chr2 = chromosomes[i2];
            }

            return pair;
        }
    }

}
