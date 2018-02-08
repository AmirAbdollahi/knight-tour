using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightTour
{
    public partial class ChessBoard : DoubleBufferedForm
    {
        Board board;
        
        Graphics graphics;
        Bitmap bmpMain;

        public ChessBoard()
        {
            InitializeComponent();
        }

        private void ChessBoard_Load(object sender, EventArgs e)
        {
            bmpMain = new Bitmap(this.Width, this.Height);
            graphics = Graphics.FromImage(bmpMain);
            board = new Board(8);
        }

        private void ChessBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmpMain, 0, 0);
        }

        private void ChessBoard_Shown(object sender, EventArgs e)
        {
            board.ShowEmptyBoard(graphics);
            this.Refresh();
        }

        private void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            board.SetStartSqr(e.X, e.Y);
            this.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseDown);
            DialogResult dialogResult = board.Run(graphics, this);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                board.ResetBoard();
                board.ShowEmptyBoard(graphics);
                this.Refresh();
                this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseDown);
            }
        }
    }
}
