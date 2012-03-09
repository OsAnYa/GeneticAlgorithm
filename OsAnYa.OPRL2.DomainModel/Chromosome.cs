using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class Chromosome
    {
        private static int identity = 0;
        private bool[] m_Genes;
        private OptimizationModel m_OptModel = null;
        public int Id { get; private set; }
        public int GId { get; set; }

        public double X1
        {
            get
            {
                double rezult = 0;

                int k = 0;
                for (int i = 4; i >= 1; i--)
                {
                    if (m_Genes[i])
                        rezult += Math.Pow(2, k);
                    k++;
                }

                if (m_Genes[5] && m_Genes[6])
                    rezult += 0.75;
                else if (m_Genes[6])
                    rezult += 0.25;
                else if (m_Genes[5])
                    rezult += 0.5;


                if (m_Genes[0])
                    rezult = -rezult;

                return rezult;
            }
        }

        public Chromosome CloneByGenes()
        {
            Chromosome rezult = new Chromosome(m_OptModel, m_Genes);
            return rezult;
        }

        public double X2
        {
            get
            {
                double rezult = 0;

                int k = 0;
                for (int i = 11; i >= 8; i--)
                {
                    if (m_Genes[i])
                        rezult += Math.Pow(2, k);
                    k++;
                }

                if (m_Genes[12] && m_Genes[13])
                    rezult += 0.75;
                else if (m_Genes[13])
                    rezult += 0.25;
                else if (m_Genes[12])
                    rezult += 0.5;
                

                if (m_Genes[7])
                    rezult = -rezult;

                return rezult;
            }
        }

        public Chromosome(OptimizationModel optModel, double x1, double x2)
        {
            m_OptModel = optModel;
            m_Genes = new bool[14];
            for (int i = 0; i < m_Genes.Length; i++)
            {
                m_Genes[i] = false;
            }


            if (x1 < 0)
                m_Genes[0] = true;
            if (x2 < 0)
                m_Genes[7] = true;

            int fx1 = (int)Math.Floor(Math.Abs(x1));
            int fx2 = (int)Math.Floor(Math.Abs(x2));
            if (fx1 > 10)
                throw new ArgumentOutOfRangeException("x1", x1, "x1 must be [-10;10]");

            if (fx1 >= 8)
                m_Genes[1] = true;
            if ((fx1 >= 4) && (fx1 <= 7))
                m_Genes[2] = true;
            if ((fx1 == 2) || (fx1 == 3) || (fx1 == 6) || (fx1 == 7) || (fx1 == 10))
                m_Genes[3] = true;
            if (fx1 % 2 == 1)
                m_Genes[4] = true;

            if (fx2 >= 8)
                m_Genes[8] = true;
            if ((fx2 >= 4) && (fx2 <= 7))
                m_Genes[9] = true;
            if ((fx2 == 2) || (fx2 == 3) || (fx2 == 6) || (fx2 == 7) || (fx2 == 10))
                m_Genes[10] = true;
            if (fx2 % 2 == 1)
                m_Genes[11] = true;

            double dec1 = Math.Abs(x1) - fx1;
            double dec2 = Math.Abs(x2) - fx2;

            if ((dec1 > 0.15) && (dec1 < 0.35))
                m_Genes[6] = true;
            if ((dec1 > 0.35) && (dec1 < 0.6))
                m_Genes[5] = true;
            if (dec1 > 0.6)
            {
                m_Genes[6] = true;
                m_Genes[5] = true;
            }

            if ((dec2 > 0.15) && (dec2 < 0.35))
                m_Genes[13] = true;
            if ((dec2 > 0.35) && (dec2 < 0.6))
                m_Genes[12] = true;
            if (dec2 > 0.6)
            {
                m_Genes[13] = true;
                m_Genes[12] = true;
            }
            Id = identity;
            identity++;
        }

        public bool IsValid()
        {
            return (X1 <= 10) && (X1 >= -10) && (X2 <= 10) && (X2 >= -10);
        }

        public bool EqualByGenes(Chromosome chromosome)
        {
            bool rezult = true;
            if (chromosome.Length != Length)
                rezult = false;
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    rezult = rezult && (m_Genes[i] == chromosome[i]);
                }
            }

            return rezult;
        }

        public Chromosome(OptimizationModel optModel, bool[] genes)
        {
            m_OptModel = optModel;
            m_Genes = new bool[14];
            for (int i = 0; i < 14; i++)
            {
                m_Genes[i] = genes[i];
            }
            Id = identity;
            identity++;
        }

        public Chromosome Mutate(double chance)
        {
            Chromosome rezult = new Chromosome(m_OptModel, m_Genes);

            for (int i = 0; i < rezult.Length; i++)
            {
                double p = Randomizer.Rnd.NextDouble();
                if (p < chance)
                    rezult[i] = !rezult[i];
            }

            return rezult;
        }

        public int Length
        {
            get
            {
                return m_Genes.Length;
            }
        }

        public bool this[int n]
        {
            get
            {
                return m_Genes[n];
            }
            set
            {
                m_Genes[n] = value;
            }
        }

        public double F
        {
            get
            {
                return m_OptModel.Criterion(X1, X2);
            }
        }

        public override string ToString()
        {
            string rezult = string.Empty;
            for (int i = 0; i < m_Genes.Length; i++)
            {
                if (m_Genes[i])
                    rezult += "1";
                else
                    rezult += "0";
            }
            return rezult;
        }
    }
}
