using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnightTour
{
    class Board
    {
        Random random = new Random();
        Bitmap bmpKnight;

        Pen pen = new Pen(Brushes.Black, 4);
        Rectangle[,] rectArr;
        bool[,] board;
        int[,] accessibility;
        List<SortablePoint> availableSqrsList;

        int currentRow;
        int currentColumn;

        int[] horizontal;
        int[] vertical;

        int movesCounter;

        private readonly int sqrRowCount;
        private const int boardSqrLength = 70;

        public Board(int count)
        {
            sqrRowCount = count;

            rectArr = new Rectangle[sqrRowCount, sqrRowCount];
            board = new bool[sqrRowCount, sqrRowCount];
            horizontal = new int[sqrRowCount];
            vertical = new int[sqrRowCount];
            SetMovesArr();

            accessibility = new int[sqrRowCount, sqrRowCount];
            SetAccessibilityArr();
        }

        private void SetMovesArr()
        {
            horizontal[0] = 2;
            horizontal[1] = 1;
            horizontal[2] = -1;
            horizontal[3] = -2;
            horizontal[4] = -2;
            horizontal[5] = -1;
            horizontal[6] = 1;
            horizontal[7] = 2;

            vertical[0] = -1;
            vertical[1] = -2;
            vertical[2] = -2;
            vertical[3] = -1;
            vertical[4] = 1;
            vertical[5] = 2;
            vertical[6] = 2;
            vertical[7] = 1;
        }

        public void SetStartSqr(int startX, int startY)
        {
            int startRow = 0;
            int startColumn = 0;

            for (int y = 0; y < rectArr.GetLength(1); y++)
            {
                for (int x = 0; x < rectArr.GetLength(0); x++)
                {
                    if (rectArr[x, y].Contains(startX, startY))
                    {
                        startRow = x;
                        startColumn = y;

                        break;
                    }
                }
            }

            currentRow = startRow;
            currentColumn = startColumn;
        }

        private void SetAccessibilityArr()
        {
            for (int y = 0; y < accessibility.GetLength(1); y++)
            {
                for (int x = 0; x < accessibility.GetLength(0); x++)
                {
                    accessibility[x, y] = GetAvailableSqrsCount(x, y);
                }
            }
        }

        private int GetAvailableSqrsCount(int x, int y)
        {
            int availableSqrsCount = 0;

            for (int moveNumber = 0; moveNumber < horizontal.Length; moveNumber++)
            {
                int newRow = x + vertical[moveNumber];
                int newColumn = y + horizontal[moveNumber];

                if (newRow >= 0 && newRow < sqrRowCount &&
                   newColumn >= 0 && newColumn < sqrRowCount &&
                   board[newRow, newColumn] == false)
                {
                    availableSqrsCount++;
                }
            }

            return availableSqrsCount;
        }

        private SortablePoint GetLeastAccessibleSqr(int currentX, int currentY)
        {
            availableSqrsList = new List<SortablePoint>();
            availableSqrsList.Clear();

            for (int moveNumber = 0; moveNumber < horizontal.Length; moveNumber++)
            {
                int newRow = currentX + vertical[moveNumber];
                int newColumn = currentY + horizontal[moveNumber];

                if (newRow >= 0 && newRow < sqrRowCount &&
                    newColumn >= 0 && newColumn < sqrRowCount &&
                    board[newRow, newColumn] == false)
                {
                    availableSqrsList.Add(new SortablePoint(newRow, newColumn, accessibility));
                }
            }

            if (availableSqrsList.Count > 0)
            {
                SortablePoint leastAccessibleSqr = availableSqrsList[0];
                for (int i = 1; i < availableSqrsList.Count; i++)
                {
                    if (accessibility[availableSqrsList[i].X, availableSqrsList[i].Y] < accessibility[leastAccessibleSqr.X, leastAccessibleSqr.Y])
                    {
                        leastAccessibleSqr = availableSqrsList[i];
                    }
                }

                return leastAccessibleSqr;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }


        private SortablePoint GetRandomSqr(int currentX, int currentY)
        {
            availableSqrsList = new List<SortablePoint>();
            availableSqrsList.Clear();

            for (int moveNumber = 0; moveNumber < horizontal.Length; moveNumber++)
            {
                int newRow = currentX + vertical[moveNumber];
                int newColumn = currentY + horizontal[moveNumber];

                if (newRow >= 0 && newRow < sqrRowCount &&
                    newColumn >= 0 && newColumn < sqrRowCount &&
                    board[newRow, newColumn] == false)
                {
                    availableSqrsList.Add(new SortablePoint(newRow, newColumn, accessibility));
                }
            }

            if (availableSqrsList.Count > 0)
            {
                SortablePoint randomAccessibleSqr = availableSqrsList[random.Next(0, availableSqrsList.Count)];

                return randomAccessibleSqr;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void ResetBoard()
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    board[x, y] = false;
                }
            }
        }

        public void ShowEmptyBoard(Graphics graphics)
        {
            for (int y = 0; y < rectArr.GetLength(1); y++)
            {
                for (int x = 0; x < rectArr.GetLength(0); x++)
                {
                    rectArr[x, y] = new Rectangle(x * boardSqrLength + 5, y * boardSqrLength + 5, boardSqrLength, boardSqrLength);

                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            graphics.FillRectangle(Brushes.White, rectArr[x, y]);
                        }
                        else
                        {
                            graphics.FillRectangle(Brushes.Gray, rectArr[x, y]);
                        }
                    }
                    else
                    {
                        if (x % 2 == 0)
                        {
                            graphics.FillRectangle(Brushes.Gray, rectArr[x, y]);
                        }
                        else
                        {
                            graphics.FillRectangle(Brushes.White, rectArr[x, y]);
                        }
                    }
                    graphics.DrawRectangle(pen, rectArr[x, y]);
                }
            }
        }

        public System.Windows.Forms.DialogResult Run(Graphics graphics, System.Windows.Forms.Form form)
        {
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.DialogResult.No;

            bmpKnight = new Bitmap(Properties.Resources.KnightPic, new Size(rectArr[0, 0].Width, rectArr[0, 0].Height));

            board[currentRow, currentColumn] = true;
            int maxAvailableMovesCount = sqrRowCount * sqrRowCount;
            movesCounter = 1;
            SetAccessibilityArr();

            graphics.DrawImage(bmpKnight, rectArr[currentRow, currentColumn]);
            graphics.DrawString(movesCounter.ToString(), new Font("Times New Roman", 20f), Brushes.LightGreen,
                    new PointF((float)rectArr[currentRow, currentColumn].X + (float)rectArr[currentRow, currentColumn].Width / 2,
                    (float)rectArr[currentRow, currentColumn].Y + (float)rectArr[currentRow, currentColumn].Height / 2));
            form.Refresh();

            try
            {
                for (int i = 0; i < maxAvailableMovesCount; i++)
                {
                    for (int moveNumber = 0; moveNumber < horizontal.Length; moveNumber++)
                    {
                        int newRow = currentRow + vertical[moveNumber];
                        int newColumn = currentColumn + horizontal[moveNumber];

                        SortablePoint leastAccessibleSqr = GetLeastAccessibleSqr(currentRow, currentColumn);

                        currentRow = leastAccessibleSqr.X;
                        currentColumn = leastAccessibleSqr.Y;

                        board[currentRow, currentColumn] = true;
                        movesCounter++;
                        SetAccessibilityArr();

                        graphics.DrawImage(bmpKnight, rectArr[currentRow, currentColumn]);
                        graphics.DrawString(movesCounter.ToString(), new Font("Times New Roman", 20f), Brushes.LightGreen,
                            new PointF((float)rectArr[currentRow, currentColumn].X + (float)rectArr[currentRow, currentColumn].Width / 2,
                            (float)rectArr[currentRow, currentColumn].Y + (float)rectArr[currentRow, currentColumn].Height / 2));
                        System.Threading.Thread.Sleep(1000);
                        form.Refresh();
                    }
                }
            }
            catch
            {
                if (movesCounter == maxAvailableMovesCount)
                {
                    System.Threading.Thread.Sleep(1000);
                    dialogResult = System.Windows.Forms.MessageBox.Show("Move count: " + movesCounter +
                        "\n\n" + "Would you like to start again?", "Full",
                        System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    dialogResult = System.Windows.Forms.MessageBox.Show("Move count: " + movesCounter +
                        "\n\n" + "Would you like to start again?", "No more move is available",
                        System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return dialogResult;
        }

        public List<SortablePoint> GetFillerPointsListBruteForce(int startX, int startY)
        {
            List<SortablePoint> fillerPointsList = new List<SortablePoint>();
            int maxAvailableMovesCount = sqrRowCount * sqrRowCount;


            while (movesCounter != maxAvailableMovesCount)
            {
                fillerPointsList.Clear();
                SetStartSqr(startX, startY);
                ResetBoard();

                board[currentRow, currentColumn] = true;
                movesCounter = 1;

                try
                {
                    for (int i = 0; i < maxAvailableMovesCount; i++)
                    {
                        for (int moveNumber = 0; moveNumber < horizontal.Length; moveNumber++)
                        {
                            SortablePoint randomSqr = GetRandomSqr(currentRow, currentColumn);

                            currentRow = randomSqr.X;
                            currentColumn = randomSqr.Y;

                            board[currentRow, currentColumn] = true;
                            movesCounter++;
                        }
                    }
                }
                catch
                {


                }
            }

            return fillerPointsList;
        }

        public void ShowFilledBoard(Graphics graphics, System.Windows.Forms.Form form, int startX, int startY)
        {
            List<SortablePoint> fillerPointsList = GetFillerPointsListBruteForce(startY, startY);
            bmpKnight = new Bitmap(Properties.Resources.KnightPic, new Size(rectArr[0, 0].Width, rectArr[0, 0].Height));

            for (int i = 0; i < fillerPointsList.Count; i++)
            {
                graphics.DrawImage(bmpKnight, rectArr[fillerPointsList[i].X, fillerPointsList[i].Y]);
                graphics.DrawString(i.ToString(), new Font("Times New Roman", 20f), Brushes.LightGreen,
                           new PointF((float)rectArr[fillerPointsList[i].X, fillerPointsList[i].Y].X + (float)rectArr[fillerPointsList[i].X, fillerPointsList[i].Y].Width / 2,
                           (float)rectArr[fillerPointsList[i].X, fillerPointsList[i].Y].Y + (float)rectArr[fillerPointsList[i].X, fillerPointsList[i].Y].Height / 2));
            }

            System.Windows.Forms.MessageBox.Show("Test");
        }

        public void ShowAccessibilityArray(Graphics graphics)
        {
            for (int y = 0; y < accessibility.GetLength(1); y++)
            {
                for (int x = 0; x < accessibility.GetLength(0); x++)
                {
                    graphics.DrawString(accessibility[x, y].ToString(), new Font("Times New Roman", 30f), Brushes.Black,
                        new PointF((float)rectArr[x, y].X, (float)rectArr[x, y].Y));
                }
            }
        }


    }
}
