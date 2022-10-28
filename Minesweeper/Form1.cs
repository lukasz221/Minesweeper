using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        List<Tile> tileList;
        public bool isGameOn = false;
        new Game game;
        public Form1()
        {
            InitializeComponent();
            tileList = new List<Tile>();
            game = new Game();
            comboBoxDifLevel.SelectedIndex = 1;
            buttonRestart.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            DifMethod();
            IsGame(true);
            buttonRestart.Enabled = true;

        }

        private void DifMethod()
        {
            if (comboBoxDifLevel.Text == "Easy")
            {
                this.Size = new Size(282, 335);
                CreatePalens(6, 6);
                game.StartGame(tileList, "easy");
            }
            else if (comboBoxDifLevel.Text == "Normal")
            {
                this.Size = new Size(445, 495);
                CreatePalens(10, 10); 
                game.StartGame(tileList, "normal");
            }
            else
            {
                this.Size = new Size(855, 580);
                CreatePalens(20, 12);
                game.StartGame(tileList, "hard");
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (isGameOn)
            {
                game = null;
                game = new Game();
                foreach (Tile tile in tileList)
                {
                    tile.Panel.Dispose();
                }
                tileList.Clear();
                DifMethod();
                isGameOn =false;
            }
            IsGame(true);
        }

        private void CreatePalens(int x, int y)
        {
            int id = 0;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    id++;
                    Panel p = new Panel();
                    Label l = new Label();
                    this.Controls.Add(p);
                    p.Controls.Add(l);

                    l.Tag = id;
                    l.Text = " ";
                    l.Location = new Point(10, 10);
                    l.AutoSize = false;
                    l.Size = new Size(80, 80);
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Dock = DockStyle.Fill;
                    l.Font = new Font("Bold", 20);

                    p.Name = $"Panel id: {id}* w: {j}* h: {i}*";
                    p.Tag = id;
                    p.BackColor = Color.Gray;
                    p.Size = new Size(40, 40);
                    p.Location = new Point(j * 41 + 10, i * 41 + 40);
                    Tile tile = new Tile(id, j, i, p, l);
                    tileList.Add(tile);
                    p.Enabled = false;
                    p.MouseClick += new MouseEventHandler(this.button_click);
                }
            }
        }

        private void IsGame(bool b)
        {
            if (b)
            {
                isGameOn = true;
                buttonStart.Enabled = false;
            }
            else
            {
                isGameOn = false;
                buttonStart.Enabled = true;
            }
        }

        void button_click(object sender, MouseEventArgs e)
        {
            CheckTile(sender, e); 
        }

        private void CheckTile(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel;
            if (e.Button == MouseButtons.Left)
            {
                foreach (var item in tileList.Where(t => t.Id.ToString() == p.Tag.ToString()))
                {
                    game.CheckNumberOfMinesAround(item);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                foreach (var item in tileList.Where(t => t.Id.ToString() == p.Tag.ToString()))
                {
                    game.SetFlag(item);
                }
            }
        }
    }
}
