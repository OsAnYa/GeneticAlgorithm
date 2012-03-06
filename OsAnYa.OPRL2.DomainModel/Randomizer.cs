using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public static class Randomizer
    {
        private static Random random = null;
        public static Random Rnd
        {
            get
            {
                if (random == null)
                    random = new Random();
                return random;
            }
        }
    }
}
