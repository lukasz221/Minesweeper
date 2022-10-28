using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class Tile
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MinesArund { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsMine { get; set; }
        public bool IsFlag { get; set; }
        public Panel Panel { get; set; }
        public Label Label { get; set; }


        public Tile(int id, int x, int y, Panel panel, Label label)
        {
            Id = id;
            X = x;
            Y = y;
            Panel = panel;  
            Label = label;
            MinesArund = 0;
            IsEmpty = true;
            IsFlag = false;
            label.Visible = false;
            label.Enabled = false;
        }
        
        public void ShowMine()
        {
            Label.Visible = true;
            Label.Enabled = true;
            if (IsMine)
            {
                Label.Text = "X";
            }
        }

        public void SetNumberOnTile(int mines)
        {
            if (mines == 0)
            {
                Label.Visible = true;
                MinesArund = mines;
                Label.Text = " ";
            }
            else
            {
                Label.Visible = true;
                Label.Enabled = true;
                MinesArund = mines;
                Label.Text = mines.ToString();
                if (mines == 1) 
                {
                    Label.ForeColor = Color.Blue;
                }
                else if (mines == 2)
                {
                    Label.ForeColor = Color.Green;
                }
                else if (mines == 3)
                {
                    Label.ForeColor = Color.Red;
                }
                else if (mines == 4)
                {
                    Label.ForeColor = Color.DarkRed;
                }
                else if (mines >= 5)
                {
                    Label.ForeColor = Color.Maroon;
                }
            }
            Panel.BackColor = Color.LightGray;
        }

        public void SetFlag()
        {
            if (IsFlag = false)
            {
                IsFlag = false;
                Label.Visible = false;
                Label.Text = " ";
                Panel.BackColor = Color.Gray;
            }
            else
            {
                IsFlag = true;
                Label.Visible = true;
                Label.Text = "Ṱ";
                Panel.BackColor = Color.LightGray;
            }
        }
    }
}
