using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.Tests
{
    [TestClass]
    public class ChromosomeTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            double x1 = 0;
            double x2 = 0;
            Chromosome chr = new Chromosome(null, x1, x2);
            Assert.AreEqual(x1, chr.X1);
            Assert.AreEqual(x2, chr.X2);
            Assert.AreEqual("00000000000000", chr.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            for (int i = -10; i <= 10; i++)
            {
                for (int j = -10; j <= 10; j++)
                {
                    Chromosome chr = new Chromosome(null, i, j);
                    Assert.AreEqual(i, (int)chr.X1);
                    Assert.AreEqual(j, (int)chr.X2);
                }
            }
        }
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void TestMethod3()
        //{
        //    Chromosome chr = new Chromosome(null, 11, -11);
        //}
        [TestMethod]
        public void TestMethod4()
        {
            double x1 = 2.75;
            double x2 = -3.5;
            Chromosome chr = new Chromosome(null, x1, x2);
            Assert.AreEqual(x1, chr.X1);
            Assert.AreEqual(x2, chr.X2);
            Assert.AreEqual("00010111001110", chr.ToString());
        }

        [TestMethod]
        public void TestMethod5()
        {
            for (double i = -10; i <= 10; i += 0.25)
            {
                for (double j = -10; j <= 10; j += 0.25)
                {
                    Chromosome chr = new Chromosome(null, i, j);
                    Assert.AreEqual(i, chr.X1);
                    Assert.AreEqual(j, chr.X2);
                }
            }
        }
    }
}
