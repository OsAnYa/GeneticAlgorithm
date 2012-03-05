using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class OptimizationModel
    {
        public int CallCount { get; private set; }
        public void ResetCallCount()
        {
            CallCount = 0;
        }
        private Func<double, double, double> m_Call;
        public double X1Min { get; private set; }
        public double X1Max { get; private set; }
        public double X2Min { get; private set; }
        public double X2Max { get; private set; }
        public double Accuracy { get; private set; }
        public OptimizationModel(Func<double, double, double> criterion)
        {
            m_Call = criterion;
            X1Min = -10;
            X1Max = 10;
            X2Min = -10;
            X2Max = 10;
            Accuracy = 0.25;
        }
        public double Criterion(double x1, double x2)
        {
            if ((x1 < -10) && (x1 > 10))
                throw new ArgumentOutOfRangeException("x1", x1, string.Format("available range [{0};{1}]", X1Min, X1Max));
            if ((x1 < -10) && (x1 > 10))
                throw new ArgumentOutOfRangeException("x2", x2, string.Format("available range [{0};{1}]", X2Min, X2Max));
            CallCount++;
            return m_Call(x1, x2);
        }
    }
}
