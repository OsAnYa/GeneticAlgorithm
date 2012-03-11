using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.Services
{
    public class ChromosomeComparer:IEqualityComparer<Chromosome>
    {
        public bool Equals(Chromosome x, Chromosome y)
        {
            return x.EqualByGenes(y);
        }

        public int GetHashCode(Chromosome obj)
        {
            return Convert.ToInt32(obj.ToString(), 2);
        }
    }
}
