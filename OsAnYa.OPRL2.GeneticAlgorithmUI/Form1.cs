﻿using System;
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
                return 1.5 * x1 * x1 * Math.Exp((x1 + x2) / 100) * x2;
            };


        private DrawingDesc desc = null;
        private PGA alg = null;

        public Form1()
        {
            InitializeComponent();
            desc = new DrawingDesc(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            desc.Clear();
            alg = new PGA();
            OptimizationModel optModel = new OptimizationModel(func);
            AlgorithmSettings settings = new AlgorithmSettings()
            {
                InitialLoadType = (InitialLoadType)comboBox1.SelectedItem,
                OptModel = optModel,
                InitialPointCount = (int)numericUpDown1.Value,
                SelectionType = (SelectionType)comboBox2.SelectedItem,
                EndCondition = (EndCondition)comboBox3.SelectedItem,
                MaxGenerationCount = (int)numericUpDown2.Value,
                SurvivedCount = (int)numericUpDown3.Value,
                MutationChance = (double)numericUpDown4.Value,
                CrossingGenNumber = (int)numericUpDown5.Value,
                Tolerance = (double)numericUpDown6.Value,
                MutationChanceAfterCrossing = (double)numericUpDown7.Value,
                MutationType = (MutationType)comboBox4.SelectedItem
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

            comboBox1.Items.Add(InitialLoadType.Box);
            comboBox1.Items.Add(InitialLoadType.TriangleMesh);
            comboBox1.SelectedItem = InitialLoadType.Box;

            comboBox2.Items.Add(SelectionType.Random);
            comboBox2.Items.Add(SelectionType.Roulette);
            comboBox2.SelectedItem = SelectionType.Random;

            comboBox3.Items.Add(EndCondition.MaxGenCount);
            comboBox3.Items.Add(EndCondition.Tolerance);
            comboBox3.SelectedItem = EndCondition.MaxGenCount;

            comboBox4.Items.Add(MutationType.AfterSelection);
            comboBox4.Items.Add(MutationType.BeforeSelection);
            comboBox4.Items.Add(MutationType.Both);
            comboBox4.SelectedItem = MutationType.Both;

        }

        private Pen[] pens = new Pen[]
        {
            Pens.Red,
            Pens.Gray,
            Pens.Green,
            Pens.Blue,
            Pens.Orange,
            Pens.Black,
            Pens.Yellow,
            Pens.Turquoise,
            Pens.Pink,
            Pens.Tomato,
            Pens.Azure,
            Pens.LightGreen
        };

        private void DrawRezult(PGA alg)
        {
            numericUpDown8.Maximum = alg.Generations.Count - 1;

            desc.Clear();
            desc.DrawMesh(alg.InitialMesh, Pens.Red);
            for (int i = 0; i < alg.Generations.Count; i++)
            {
                if (alg.Generations[i].AllFinishChromosome != null)
                    foreach (var item in alg.Generations[i].AllFinishChromosome)
                    {
                        SolidBrush b = new SolidBrush(pens[item.GId].Color);

                        desc.AddCircle((float)item.X1, (float)item.X2, 3, pens[item.GId], true);
                        desc.AddText(item.GId + "." + item.Id, (float)item.X1, (float)item.X2, SystemFonts.SmallCaptionFont, b);
                    }
            }
            desc.Update();
        }

        private void WriteRezult(PGA alg)
        {
            int i = 0;
            int[] clr = new int[100];
            foreach (var item in alg.Generations)
            {

                WriteLine(string.Format("Покаление {0}", item.Id));

                WriteLine("На входе");
                if (item.InitialChromosomes != null)
                    foreach (var ch in item.InitialChromosomes)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Первая мутация");
                if (item.FirstMutation != null)
                    foreach (var ch in item.FirstMutation)
                    {
                        WriteChomosome(ch.Key, "Родитель");
                        WriteChomosome(ch.Value, "Потомок");
                        WriteLine();
                    }

                WriteLine("Перед скрещиванием");
                if (item.AfterFirstMutation != null)
                    foreach (var ch in item.AfterFirstMutation)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Пары1");
                if (item.Pairs1 != null)
                    foreach (var ch in item.Pairs1)
                    {
                        WriteChomosome(ch.Chr1);
                        WriteChomosome(ch.Chr2);
                        WriteLine();
                    }

                WriteLine("Победители в парах1");
                if (item.Winners1 != null)
                    foreach (var ch in item.Winners1)
                    {
                        WriteChomosome(ch);
                    }


                WriteLine("Пары2");
                if (item.Pairs2 != null)
                    foreach (var ch in item.Pairs2)
                    {
                        WriteChomosome(ch.Chr1);
                        WriteChomosome(ch.Chr2);
                        WriteLine();
                    }

                WriteLine("Победители в парах2");
                if (item.Winners2 != null)
                    foreach (var ch in item.Winners2)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Пары для генетических операций");
                if (item.PairsForGeneticOperation != null)
                    foreach (var ch in item.PairsForGeneticOperation)
                    {
                        WriteChomosome(ch.Chr1);
                        WriteChomosome(ch.Chr2);
                        WriteLine();
                    }

                WriteLine("Результат генетических операций");
                if (item.GeneticOperationRezults != null)
                    foreach (var ch in item.GeneticOperationRezults)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Все хромосомы покаления");
                if (item.AllFinishChromosome != null)
                    foreach (var ch in item.AllFinishChromosome)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Лучшие");
                if (item.Output != null)
                    foreach (var ch in item.Output)
                    {
                        WriteChomosome(ch);
                    }

                WriteLine("Лучшая");
                WriteChomosome(item.BestChromosome);

                i++;
                clr[i] = richTextBox1.Text.Length;
            }
            WriteLine(string.Format("Количество вычислений целевой ф-и {0}", alg.CallCount));
            WriteLine("Лучшая хромосома");
            WriteChomosome(alg.Best);
            for (int j = 0; j < i; j++)
            {
                richTextBox1.Select(clr[j], clr[j + 1] - clr[j]);
                richTextBox1.SelectionColor = pens[j].Color;
            }
        }

        private void WriteChomosome(Chromosome chr, string comment = "")
        {
            richTextBox1.Text += chr.GId + "." + chr.Id + "\t" + chr.X1 + "\t" + chr.X2 + "\t" + chr + "\t" + Math.Round(chr.F, 4) + comment + "\n";
        }

        private void WriteLine(string str = "")
        {
            richTextBox1.Text += str + "\n";
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            desc.Clear();
            desc.DrawMesh(alg.InitialMesh, Pens.Red);


            foreach (var item in alg.Generations[(int)numericUpDown8.Value].AfterFirstMutation)
            {

                desc.AddCircle((float)item.X1, (float)item.X2, 3, Pens.Blue, true);
                desc.AddText(item.GId + "." + item.Id, (float)item.X1, (float)item.X2);
            }

            foreach (var item in alg.Generations[(int)numericUpDown8.Value].InitialChromosomes)
            {

                desc.AddCircle((float)item.X1, (float)item.X2, 3, Pens.Red, true);
                desc.AddText(item.GId + "." + item.Id, (float)item.X1, (float)item.X2);
            }

            foreach (var item in alg.Generations[(int)numericUpDown8.Value].GeneticOperationRezults)
            {

                desc.AddCircle((float)item.X1, (float)item.X2, 3, Pens.Green, true);
                desc.AddText(item.GId + "." + item.Id, (float)item.X1, (float)item.X2);
            }
            desc.Update();
        }
    }
}
