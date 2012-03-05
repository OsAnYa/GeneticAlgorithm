using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsAnYa.OPRL2.DomainModel
{
    public class Point
    {
        public double X1 { get;private set; }
        public double X2 { get;private set; }
        public double Accuracy { get; private set; }

        public Point(OptimizationModel optModel,double x1,double x2)
        {
            Accuracy = optModel.Accuracy;
            //////////Доделать округление
            X1 = x1;
            X2 = x2;
        }
    }
}
