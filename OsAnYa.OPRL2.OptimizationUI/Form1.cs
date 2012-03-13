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
            return f1 * (double)numericUpDown9.Value - f2 * (double)numericUpDown10.Value;
            //return Math.Pow(f1, (double)numericUpDown9.Value) / Math.Pow(f2, (double)numericUpDown10.Value);
        }

        private PGA alg = null;

        public Form1()
        {
            InitializeComponent();

        }
        private List<OptRezult> optrez = new List<OptRezult>();
        private List<OptRezult> finalRez = new List<OptRezult>();
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int progress = 0;
            optrez.Clear();
            finalRez.Clear();
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
                            double[] x1m = new double[(int)numericUpDown8.Value];
                            double[] x2m = new double[(int)numericUpDown8.Value];
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
                                double x1 = alg.Best.X1;
                                double x2 = alg.Best.X2;
                                double f1 = alg.Best.F;
                                double f2 = alg.CallCount;
                                double f = GetCriterion(f1, f2);
                                f1m[i] = f1;
                                f2m[i] = f2;
                                fm[i] = f;
                                x1m[i] = x1;
                                x2m[i] = x2;
                                optrez.Add(new OptRezult()
                                {
                                    I = il,
                                    E = ec,
                                    S = st,
                                    M = mt,
                                    F1 = f1,
                                    F2 = f2,
                                    X1 = x1,
                                    X2 = x2,
                                    F = f
                                });
                            }
                            progress++;
                            backgroundWorker1.ReportProgress(progress * 100 / 24);
                            finalRez.Add(new OptRezult()
                            {
                                I = il,
                                E = ec,
                                S = st,
                                M = mt,
                                X1 = x1m.Average(),
                                X2 = x2m.Average(),
                                F1 = f1m.Average(),
                                F2 = f2m.Average(),
                                F = fm.Average()

                            });
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = finalRez.OrderByDescending(i => i.F).ToList();
            dataGridView2.DataSource = optrez;
            progressBar1.Value = 0;
        }




    }
}
