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
            ChromosomePair pair = new ChromosomePair();

            return pair;
        }

        private static ChromosomePair GetPairRandom(List<Chromosome> chromosomes)
        {
            ChromosomePair pair = new ChromosomePair();

            return pair;
        }
    }

}
