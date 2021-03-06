using System;
using System.Drawing;
using System.Windows.Forms;

namespace GomokuGame
{
    public enum Player { Out = -1, None =0, Human =1, Machine =2 }
    public struct Node
    {
        public int Row;
        public int Column;
    }

    public partial class frmMain : Form
    {
    // ************ VARIABLE *******************************
        private GomokuBoard view;
        private frmOption Option;

    // ************ CONSTRUCTOR ****************************
        public frmMain()
        {
            InitializeComponent();
            Option = new frmOption();
        }

    // ************ HANDLE EVENT ***************************
        private void frmMain_Load(object sender, EventArgs e)
        {
            view = new GomokuBoard(this);
            
            NewGame();
            statusbar.Hide();

            vScroll.Width =20;
            hScroll.Height =20;

            vScroll.Maximum = vScroll.Minimum = vScroll.Value = 1;
            hScroll.Maximum = hScroll.Minimum = hScroll.Value = 1;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            view.DrawBoard();
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                view.HandleMouseDown(e);
            }
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LỚP INPG15_NHÓM 4 Edited"
                            + "\n ĐỖ VĂN NHU"
                            + "\n LÊ MINH HIẾU"
                            + "\n TRẦN QUANG CHIẾN"
                            + "\n NGUYỄN THẾ ANH"
                            , "GIOI THIEU"
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Information);
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Option.Show();

        }

        private void vScroll_Scroll(object sender, ScrollEventArgs e)
        {
            view.OffsetH = vScroll.Value;
            Invalidate();
        }

        private void hScroll_Scroll(object sender, ScrollEventArgs e)
        {
            view.OffsetW = hScroll.Value;
            Invalidate();
        }
    
    // *********** ADDING FUNCTION *************************
        public void SendMessage(uint Msg)
        {
            switch (Msg)
            {
                case 0:
                    statusbar.Hide();
                    break;
                case 1:
                    Message.Text = " BẠN CÓ MUỐN CHƠI TIẾP KHÔNG? NẾU CHƠI TIẾP XIN NHAN F2 ĐỂ CHƠI BÀN MỚI!";
                    statusbar.Show();
                    break;

                case 2:
                    Message.Text = "XIN LỖI BẠN ĐÃ THUA RỒI";
                    statusbar.Show();
                    break;

                //case 3:
                //    Message.Text = "CHÚ Ý BẠN CHUẨN BỊ THUA ?";
                //    statusbar.Show();
                //    break;

                case 3:
                    Message.Text = "W.A.R.N.I.N.G....!";
                    statusbar.Show();
                    break;
            }
        }

        public void NewGame()
        {
            // thiet lap lai ban co.
            view.ResetBoard();
            view.End = Player.None;
             // cho may danh truoc ?
            if (Option.chkFirstMove.Checked) view.CurrPlayer = Player.Human;
            else
                view.Board[Height / 40 + view.OffsetH, Width / 40 + view.OffsetW] = Player.Machine;
          
            view.DrawBoard();
            statusbar.Hide();
        }

    // ********** OVERRIDE METHOD **************************
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (view != null)
            {
                hScroll.Maximum = view.Width + 1 - Width / 20;
                vScroll.Maximum = view.Height + 1 - Height / 20;

                view.OffsetH = vScroll.Value;
                view.OffsetW = hScroll.Value;
            }
        }

        private void mnuGame_Click(object sender, EventArgs e)
        {

        }

        private void tHOÁTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    
        }
    }
