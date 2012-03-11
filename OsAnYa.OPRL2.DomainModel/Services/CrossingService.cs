using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.Services
{
    public static class CrossingService
    {
        public static List<Chromosome> DoCrossing(ChromosomePair pair, int GenNumber, double mutationChance = 0.07)
        {
            List<Chromosome> rezult = null;

            if (pair.Chr1.EqualByGenes(pair.Chr2))
                rezult = Inversion(pair);
            else
                rezult = Crossing(pair, GenNumber);
            for (int i = 0; i < rezult.Count; i++)
            {
                rezult[i] = rezult[i].Mutate(mutationChance);
            }
            return rezult;
        }

        private static List<Chromosome> Inversion(ChromosomePair pair)
        {
            List<Chromosome> rezult = new List<Chromosome>(1);
            Chromosome chr = pair.Chr1.CloneByGenes();
            chr[4] = pair.Chr1[9];

            rezult.Add(chr);
            return rezult;
        }

        private static List<Chromosome> Crossing(ChromosomePair pair, int GenNumber)
        {
            List<Chromosome> rezult = new List<Chromosome>(1);
            Chromosome chr1 = pair.Chr1.CloneByGenes();
            Chromosome chr2 = pair.Chr2.CloneByGenes();
            for (int i = GenNumber; i < chr1.Length; i++)
            {
                chr1[i] = pair.Chr2[i];
                chr2[i] = pair.Chr1[i];
            }
            rezult.Add(chr1);
            rezult.Add(chr2);
            return rezult;
        }
    }
}
