namespace KnightTour
{
    partial class ChessBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessBoard));
            this.SuspendLayout();
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 708);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChessBoard";
            this.Text = "Knight\'s Tour";
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.Shown += new System.EventHandler(this.ChessBoard_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChessBoard_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion


    }
}

