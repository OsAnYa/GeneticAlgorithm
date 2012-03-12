using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OsAnYa.OPRL2.Core;
using OsAnYa.OPRL2.DomainModel;

namespace OsAnYa.OPRL2.OptimizationUI
{
    public partial class Form1 : Form
    {
        Func<double, double, double> func = (double x1, double x2) =>
            {
                return 100 - x1 * x2;
            };
        private double GetCriterion(double f1, double f2)
        {
            return f1 / f2;
        }

        private PGA alg = null;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<OptRezult> optrez = new List<OptRezult>();
            List<OptRezult> finalRez = new List<OptRezult>();
            alg = new PGA();
            OptimizationModel optModel = new OptimizationModel(func);
            EnumConverter InitialLoadTypeCollection = new EnumConverter(typeof(InitialLoadType));
            EnumConverter EndConditionTypeCollecton = new EnumConverter(typeof(EndCondition));
            EnumConverter MutationTypeCollecton = new EnumConverter(typeof(MutationType));
            EnumConverter SelectionTypeCollection = new EnumConverter(typeof(SelectionType));
            foreach (InitialLoadType il in InitialLoadTypeCollection.GetStandardValues())
            {
                foreach (EndCondition ec in EndConditionTypeCollecton.GetStandardValues())
                {
                    foreach (MutationType mt in MutationTypeCollecton.GetStandardValues())
                    {
                        foreach (SelectionType st in SelectionTypeCollection.GetStandardValues())
                        {
                            double[] f1m = new double[(int)numericUpDown8.Value];
                            double[] f2m = new double[(int)numericUpDown8.Value];
                            double[] fm = new double[(int)numericUpDown8.Value];

                            for (int i = 0; i < (int)numericUpDown8.Value; i++)
                            {
                                AlgorithmSettings settings = new AlgorithmSettings()
                                {
                                    InitialLoadType = il,
                                    OptModel = optModel,
                                    InitialPointCount = (int)numericUpDown1.Value,
                                    SelectionType = st,
                                    EndCondition = ec,
                                    MaxGenerationCount = (int)numericUpDown2.Value,
                                    SurvivedCount = (int)numericUpDown3.Value,
                                    MutationChance = (double)numericUpDown4.Value,
                                    CrossingGenNumber = (int)numericUpDown5.Value,
                                    Tolerance = (double)numericUpDown6.Value,
                                    MutationChanceAfterCrossing = (double)numericUpDown7.Value,
                                    MutationType = mt
                                };
                                alg.Run(settings);
                                double f1 = alg.Best.F;
                                double f2 = alg.CallCount;
                                double f = GetCriterion(f1, f2);
                                f1m[i] = f1;
                                f2m[i] = f2;
                                fm[i] = f;
                                optrez.Add(new OptRezult()
                                {
                                    I = il,
                                    E = ec,
                                    S = st,
                                    M = mt,
                                    F1 = f1,
                                    F2 = f2,
                                    F = f
                                });
                            }
                            finalRez.Add(new OptRezult()
                            {
                                I = il,
                                E = ec,
                                S = st,
                                M = mt,
                                F1 = f1m.Average(),
                                F2 = f2m.Average(),
                                F = fm.Average()

                            });
                        }
                    }
                }
            }


            dataGridView1.DataSource = finalRez.OrderByDescending(i => i.F).ToList();
            dataGridView2.DataSource = optrez;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }




    }
}
