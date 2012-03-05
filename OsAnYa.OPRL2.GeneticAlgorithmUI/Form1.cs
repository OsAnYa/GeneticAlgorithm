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

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    public partial class Form1 : Form
    {
        Func<double, double, double> func = (double x1, double x2) =>
            {
                return x1 * x2;
            };


        private DrawingDesc desc = null;
        private GeneticAlgorithm alg = null;

        public Form1()
        {
            InitializeComponent();
            desc = new DrawingDesc(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alg = new GeneticAlgorithm();
            OptimizationModel optModel = new OptimizationModel(func);
            AlgorithmSettings settings = new AlgorithmSettings()
            {
                InitialLoadType = InitialLoadType.Box,
                OptModel = optModel,
                InitialPointCount = (int)numericUpDown1.Value
            };
            alg.Run(settings);
            DrawRezult(alg);
            WriteRezult(alg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            desc.Scale = 10;
            desc.Asix = true;
            desc.OneStroke = true;
        }

        private void DrawRezult(GeneticAlgorithm alg)
        {
            desc.DrawMesh(alg.InitialMesh, Pens.Red);
            desc.Update();
        }

        private void WriteRezult(GeneticAlgorithm alg)
        {

        }
    }
}
