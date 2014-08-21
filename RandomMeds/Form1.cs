﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomMeds
{
    public partial class Form1 : Form
    {
        private int raasOK = 0;
        private int raasNOT = 0;
        private int blockOK = 0;
        private int blockNOT = 0;
        private int teller;
        private bool isGo = false;
        private List<String> strList = new List<string>();
        private List<String> exclList = new List<string>();
        private List<int> numberList = Enumerable.Range(1, 80).ToList();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createLabels();
            createCheckBoxes();
            createNames();

        }
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            //var list = new List<int> {1,2,3,4,5};
            //var intVar = 4;
            //var exists = list.Contains(intVar);
            CheckBox cb = (CheckBox)sender;
            //MessageBox.Show("De tag is: " +cb.Tag);
            if (cb.Checked == true && !isGo)
            {
                exclList.Add(cb.Tag.ToString());
                //string[] words = cb.Tag.ToString().Split(':');
                //MessageBox.Show(words[1]);
            }
            else if(!isGo)
            {
                exclList.Remove(cb.Tag.ToString());
            }
        }
        private void createNames()
        {
            int left = 90;
            int top = 20;
            for (int i = 0; i < 8; i++)
            {
                Label label = new Label();
                label.Left = left;
                label.Top = top;
                if ((i % 2) == 1)
                {
                    label.Text = "RAAS";
                    left += 120;
                    Console.WriteLine("jaja");
                }
                else
                {
                    label.Text = "Betablokker";
                    left += 80;
                }

                this.Controls.Add(label);
                label.BringToFront();

            }
        }
        private void createCheckBoxes()
        {

            int left = 100;
            int top = 45;
            for (int k = 0; k < 4; k++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        CheckBox ii = new CheckBox();
                        ii.AutoSize = false;
                        ii.Size = new System.Drawing.Size(17, 18);
                        ii.Name = "cbx" + i;
                        ii.Text = i.ToString();
                        ii.Location = new Point(left, top);
                        ii.Tag = ((20 * k) + i) + ":" + j; // Assuming you have your files in an array or similar
                        ii.CheckedChanged += new System.EventHandler(this.Check_CheckedChanged);
                        top += 25;
                        this.Controls.Add(ii);
                        ii.BringToFront();
                    }
                    top = 45;
                    if (j == 2)
                    {
                        left += 50;
                    }
                    else
                    {
                        left += 20;
                    }
                } left += 90;
            }
        }
        private void createLabels()
        {
            int top = 50;
            int left = 30;
            int nr = 1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Label label = new Label();
                    label.Left = left;
                    label.Top = top;
                    if (nr < 10) { label.Text = "STOP00" + nr; }
                    else
                    {
                        label.Text = "STOP0" + nr;
                    }
                    this.Controls.Add(label);
                    top += label.Height + 2;
                    nr++;
                }
                left += 200;
                top = 50;
            }
        }
        private void uncheckAll()
        {
            raasOK = 0;
            raasNOT = 0;
            blockNOT = 0;
            blockOK = 0;
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }
            }
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            isGo = true;
            uncheckAll();
            int nr = 0;
            Random rnd = new Random();
             int tempROK = 0;
                int tempRNOT = 0;
                int tempBNOT = 0;
                int tempBOK = 0;
            for (int i = 1; i <= 80; i++)
            {
               
                    
                    int rand = rnd.Next(1, 5);
                    foreach (Control c in this.Controls)
                    {
                        if (c is CheckBox)
                        {
                            CheckBox cb = (CheckBox)c;
                            if (exclList.Contains(cb.Tag.ToString()))
                            {
                                //rand = int.Parse(cb.Tag.ToString().Split(':')[1]);
                                //exclList.Remove(cb.Tag.ToString());
                            }
                            //string[] words = cb.Tag.ToString().Split(':');
                            if (cb.Tag.Equals(nr + ":" + rand))
                            {
                                strList.Add(cb.Tag.ToString());
                                switch (rand)
                                {
                                    case 1:
                                        tempBOK += 1;
                                        break;
                                    case 2:
                                        tempBNOT += 1;
                                        break;
                                    case 3:
                                        tempROK += 1;
                                        break;
                                    case 4:
                                        tempRNOT += 1;
                                        break;
                                    default:
                                        Console.WriteLine("Default case");
                                        break;
                                }
                            }
                        }
                    }

                }
                if ((tempBNOT != tempBOK) || (tempRNOT != tempROK))
                {
                    strList.Clear();
                    
                    tempRNOT = 0;
                    tempROK = 0;
                    tempBOK = 0;
                    tempBNOT = 0;
                    nr -= 4;
                }
                else
                {
                    blockOK += tempBOK;
                    blockNOT += tempBNOT;
                    raasOK += tempROK;
                    raasNOT += tempRNOT;
                    lblBok.Text = "Betablokker OK: " + blockOK;
                    lblBnot.Text = "Betablokker NOT: " + blockNOT;
                    lblRok.Text = "Raas OK: " + raasOK;
                    lblRnot.Text = "Raas NOT: " + raasNOT;
                    foreach (Control c in this.Controls)
                    {
                        if (c is CheckBox)
                        {
                            CheckBox cb = (CheckBox)c;
                            if (strList.Contains(cb.Tag.ToString()))
                            {
                                cb.Checked = true;
                            }
                            //teller = i;
                        }
                    }
                    strList.Clear();
                    //MessageBox.Show("Nummer is: " + nr + " en index is: " + teller);
                }
            isGo = false;
            }
            
        }
    }

