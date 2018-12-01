using System;
using System.Windows.Forms;
using System.Drawing;

namespace GomokuGame
{
    class GomokuBoard
    {
    // ************** VARIABLE ******************************
        public Player CurrPlayer = Player.None;
        public Player[,] Board;
        public frmMain Parent;
        public int Width, Height;
        public Player End = Player.None;
        public uint Msg = 0;
        public int OffsetW = 1;
        public int OffsetH = 1;

        public EValueBoard EBoard;

        // Phong thu chat, tan cong nhanh.
        public int[] TScore = new int[5] { 0, 1, 9, 85,769 };
        public int[] KScore = new int[5] { 0, 2, 28, 256, 2308 };

        // Bitmap...
        //public Bitmap bmpOut = new Bitmap("..//..//bmpOut.bmp");
        public Bitmap bmpHuman = new Bitmap("..//..//bmpHuman.bmp");
        public Bitmap bmpMachine = new Bitmap("..//..//bmpMachine.bmp");
        public Bitmap bmpNone = new Bitmap("..//..//bmpNone.bmp");
    // ************** CONSTRUCTOR ***************************
        
        // Default Constructor.
        public GomokuBoard() { }
        // With Parent form.
        public GomokuBoard(frmMain parent)
        {
            Parent = parent;
            Width = Parent.Width / 20;
            Height = Parent.Height / 20;
            Board = new Player[Height + 2, Width + 2];
            EBoard = new EValueBoard(this);
            ResetBoard();
        }
        
    // ************** HANDLE EVENT **************************
        public void HandleMouseDown(MouseEventArgs e)
        {
            Random rand = new Random();
            int count = rand.Next(4);
            int r = (e.Y / 20) + OffsetH;
            int c = (e.X / 20) + OffsetW;
            Node node = new Node();


            if (Board[r, c] == Player.None)
            {
                // nguoi choi di co.
                if (CurrPlayer == Player.Human && End == Player.None)
                {
                    Parent.SendMessage(0);
                    Board[r, c] = CurrPlayer;
                    CurrPlayer = Player.Machine;

                    End = CheckEnd(r, c);
                    if (End != Player.None)
                    {
                        if (End == Player.Human) Parent.SendMessage(1);
                        if (End == Player.Machine) Parent.SendMessage(2);
                    }
                }

                // May di co.
                if (CurrPlayer == Player.Machine && End == Player.None)
                {

                    // Tim nuoc di chien thang.
                    EBoard.ResetBoard();
                    GetGenResult();

                    if (Win) // Tim thay.
                    {
                        node = WinMoves[1];
                    }
                    else
                    {
                        EBoard.ResetBoard();
                        EvalueGomokuBoard(Player.Machine);
                        node = EBoard.GetMaxNode();
                        if (!Lose)
                            for (int i = 0; i < count; i++)
                            {
                                EBoard.Board[node.Row, node.Column] = 0;
                                node = EBoard.GetMaxNode();
                            }
                    }
                    // May di quan.
                    r = node.Row; c = node.Column;
                    Board[r, c] = CurrPlayer;
                    CurrPlayer = Player.Human;

                    // Kiem tra tran dau ket thuc chua ?
                    End = CheckEnd(r, c);
                    if (End != Player.None)
                    {
                        if (End == Player.Human) Parent.SendMessage(1);
                        if (End == Player.Machine) Parent.SendMessage(2);
                    }
                }

            }

            DrawBoard();
        }

    // ************** ADDING FUNCTION ***********************
        
        // Thiet lap lai ban co.
        public void ResetBoard()
        {
            int r, c;
            //Thiet lap lai gia tri bang.
            for (r = 0; r < Height + 2; r++)
                for (c = 0; c < Height + 2; c++)
                {
                    if (r == 0 || c == 0 || r == Height + 1 || c == Width + 1)
                        Board[r, c] = Player.Out;
                    else Board[r, c] = Player.None;
                }

            CurrPlayer = Player.Human;//NGUOI CHOI 

            DrawBoard();
        }
        // Ve ban co.
        public void DrawBoard()
        {
            Graphics g = Parent.CreateGraphics();// tu khoa hinh ve do hoa 
            Rectangle rect = new Rectangle();//hinh chu nhat 

            for (int r = OffsetH; r <= Height + 1; r++)//offsetH=1
                for (int c = OffsetW; c <= Width + 1; c++)//offsetW=1
               //in ra so dong va so cot 
                {
                    rect = new Rectangle((c - OffsetW) * 20, (r - OffsetH) * 20, 20, 20);
                     if (Board[r, c] == Player.None)// la o bang 
                        g.DrawImage(bmpNone, rect);
                    if (Board[r, c] == Player.Human)//  nut tron la quan ta  
                        g.DrawImage(bmpHuman, rect);
                    if (Board[r, c] == Player.Machine)// quan dich
                        g.DrawImage(bmpMachine, rect);
                    //if (Board[r, c] == Player.Out)//
                    //    g.DrawImage(bmpOut, rect);
                }
        }
        // Kiem tra van dau co ket thuc chua ?
        private Player CheckEnd(int rw, int cl)
        {
            bool Human, Machine;
            int r = 1, c = 1;
            int i;

            // Kiem tra tren hang...
            while (c <= Width - 4)
            {
                Human = true; Machine = true;

                for (i = 0; i < 5; i++)
                {
                    if (Board[rw, c + i] != Player.Human)
                        Human = false;
                    if (Board[rw, c + i] != Player.Machine)
                        Machine = false;
                }

                if (Human && (Board[rw, c - 1] != Player.Machine || Board[rw, c + 5] != Player.Machine)) return Player.Human;
                if (Machine && (Board[rw, c - 1] != Player.Human || Board[rw, c + 5] != Player.Human)) return Player.Machine;
                c++;
            }

            // Kiem tra tren cot...
            while (r <= Height - 4)
            {
                Human = true; Machine = true;
                for (i = 0; i < 5; i++)
                {
                    if (Board[r + i, cl] != Player.Human)
                        Human = false;
                    if (Board[r + i, cl] != Player.Machine)
                        Machine = false;
                }
                if (Human && (Board[r - 1, cl] != Player.Machine || Board[r + 5, cl] != Player.Machine)) return Player.Human;
                if (Machine && (Board[r - 1, cl] != Player.Human || Board[r + 5, cl] != Player.Human)) return Player.Machine;
                r++;
            }

            // Kiem tra tren duong cheo xuong.
            r = rw; c = cl;
            // Di chuyen den dau duong cheo xuong.
            while (r > 1 && c > 1) { r--; c--; }
            while (r <= Height - 4 && c <= Width - 4)
            {
                Human = true; Machine = true;
                for (i = 0; i < 5; i++)
                {
                    if (Board[r + i, c + i] != Player.Human)
                        Human = false;
                    if (Board[r + i, c + i] != Player.Machine)
                        Machine = false;
                }
                if (Human && (Board[r - 1, c - 1] != Player.Machine || Board[r + 5, c + 5] != Player.Machine)) return Player.Human;
                if (Machine && (Board[r - 1, c - 1] != Player.Human || Board[r + 5, c + 5] != Player.Human)) return Player.Machine;
                r++; c++;
            }

            // Kiem tra duong cheo len...
            r = rw; c = cl;
            // Di chuyen den dau duong cheo len...
            while (r < Height && c > 1) { r++; c--; }
            while (r >= 5 && c <= Width - 4)
            {
                Human = true; Machine = true;
                for (i = 0; i < 5; i++)
                {
                    if (Board[r - i, c + i] != Player.Human)
                        Human = false;
                    if (Board[r - i, c + i] != Player.Machine)
                        Machine = false;
                }
                if (Human && (Board[r + 1, c - 1] != Player.Machine || Board[r - 5, c + 5] != Player.Machine)) return Player.Human;
                if (Machine && (Board[r + 1, c - 1] != Player.Human || Board[r - 5, c + 5] != Player.Human)) return Player.Machine;
                r--; c++;
            }

            return Player.None;
        }
        // Luong gia cho ban co - kinh nghiem cua may.
        private void EvalueGomokuBoard(Player player)
        {
            int rw, cl, i;
            int cntHuman, cntMachine;

            // Luong gia cho hang.
            for (rw = 1; rw <= Height; rw++)
                for (cl = 1; cl <= Width-4; cl++)
                {
                    cntHuman = 0; cntMachine = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (Board[rw, cl + i] == Player.Human) cntHuman++;
                        if (Board[rw, cl + i] == Player.Machine) cntMachine++;
                    }
                    // Luong gia...
                    if (cntHuman * cntMachine == 0 && cntHuman != cntMachine)
                        for (i = 0; i < 5; i++)
                            if (Board[rw, cl + i] == Player.None)
                            {
                                if (cntMachine == 0)
                                {
                                    if (player == Player.Machine) EBoard.Board[rw, cl + i] += TScore[cntHuman];
                                    else EBoard.Board[rw, cl + i] += KScore[cntHuman];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw, cl - 1] == Player.Machine && Board[rw, cl + 5] == Player.Machine)
                                        EBoard.Board[rw, cl + i] = 0;
                                }
                                if (cntHuman == 0) 
                                {
                                    if (player == Player.Human) EBoard.Board[rw, cl + i] += TScore[cntMachine];
                                    else EBoard.Board[rw, cl + i] += KScore[cntMachine];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw, cl - 1] == Player.Human && Board[rw, cl + 5] == Player.Human)
                                        EBoard.Board[rw, cl + i] = 0;
                                }
                                if ((cntHuman == 4 || cntMachine == 4)
                                    && (Board[rw, cl + i - 1] == Player.None || Board[rw, cl + i + 1] == Player.None))
                                    EBoard.Board[rw, cl + i] *= 2;
                            }
                }

            // Luong gia cho cot.
            for (cl = 1; cl <= Width; cl++)
                for (rw = 1; rw <= Height-4; rw++)
                {
                    cntHuman = 0; cntMachine = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (Board[rw+i, cl] == Player.Human) cntHuman++;
                        if (Board[rw+i, cl] == Player.Machine) cntMachine++;
                    }
                    // Luong gia...
                    if (cntHuman * cntMachine == 0 && cntMachine != cntHuman)
                        for (i = 0; i < 5; i++)
                            if (Board[rw + i, cl] == Player.None)
                            {
                                if (cntMachine == 0)
                                {
                                    if (player == Player.Machine) EBoard.Board[rw + i, cl] += TScore[cntHuman];
                                    else EBoard.Board[rw + i, cl] += KScore[cntHuman];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw - 1, cl] == Player.Machine && Board[rw + 5, cl] == Player.Machine)
                                        EBoard.Board[rw + i, cl] = 0;
                                }
                                if (cntHuman == 0)
                                {
                                    if (player == Player.Human) EBoard.Board[rw + i, cl] += TScore[cntMachine];
                                    else EBoard.Board[rw + i, cl] += KScore[cntMachine];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw - 1, cl] == Player.Human && Board[rw + 5, cl] == Player.Human)
                                        EBoard.Board[rw+i, cl] = 0;
                                
                                }
                                if ((cntHuman == 4 || cntMachine == 4)
                                    && (Board[rw + i - 1, cl] == Player.None || Board[rw + i + 1, cl] == Player.None))
                                    EBoard.Board[rw + i, cl] *= 2;
                            }
                }

            
            // Luong gia cho duong cheo xuong.
            for (rw = 1; rw <= Height-4; rw++)
                for (cl = 1; cl <= Width-4; cl++)
                {
                    cntHuman = 0; cntMachine = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (Board[rw + i, cl + i] == Player.Human) cntHuman++;
                        if (Board[rw + i, cl + i] == Player.Machine) cntMachine++;
                    }
                    // Luong gia...
                    if (cntHuman * cntMachine == 0 && cntMachine != cntHuman)
                        for (i = 0; i < 5; i++)
                            if (Board[rw+i, cl + i] == Player.None)
                            {
                                if (cntMachine == 0)
                                {
                                    if (player == Player.Machine) EBoard.Board[rw+i, cl + i] += TScore[cntHuman];
                                    else EBoard.Board[rw+i, cl + i] += KScore[cntHuman];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw - 1, cl - 1] == Player.Machine && Board[rw + 5, cl + 5] == Player.Machine)
                                        EBoard.Board[rw + i, cl + i] = 0;
                                }
                                if (cntHuman == 0)
                                {
                                    if (player == Player.Human) EBoard.Board[rw + i, cl + i] += TScore[cntMachine];
                                    else EBoard.Board[rw + i, cl + i] += KScore[cntMachine];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw - 1, cl - 1] == Player.Human && Board[rw + 5, cl + 5] == Player.Human)
                                        EBoard.Board[rw + i, cl + i] = 0;
                                }
                                if ((cntHuman == 4 || cntMachine == 4)
                                    && (Board[rw + i - 1, cl + i - 1] == Player.None || Board[rw + i + 1, cl + i + 1] == Player.None))
                                    EBoard.Board[rw + i, cl + i] *= 2;
                            }
                }

            // Luong gia cho duong cheo len.
            for (rw = 5; rw <= Height-4; rw++)
                for (cl = 1; cl <= Width-4; cl++)
                {
                    cntMachine = 0; cntHuman = 0;
                    for (i = 0; i < 5; i++)
                    {
                        if (Board[rw - i, cl + i] == Player.Human) cntHuman++;
                        if (Board[rw - i, cl + i] == Player.Machine) cntMachine++;
                    }
                    // Luong gia...
                    if (cntHuman * cntMachine == 0 && cntHuman != cntMachine)
                        for (i = 0; i < 5; i++)
                            if (Board[rw-i, cl + i] == Player.None)
                            {
                                if (cntMachine == 0)
                                {
                                    if (player == Player.Machine) EBoard.Board[rw - i, cl + i] += TScore[cntHuman];
                                    else EBoard.Board[rw - i, cl + i] += KScore[cntHuman];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw + 1, cl - 1] == Player.Machine && Board[rw - 5, cl + 5] == Player.Machine)
                                        EBoard.Board[rw - i, cl + i] = 0;
                                }
                                if (cntHuman == 0)
                                {
                                    if (player == Player.Human) EBoard.Board[rw - i, cl + i] += TScore[cntMachine];
                                    else EBoard.Board[rw - i, cl + i] += KScore[cntMachine];
                                    // Truong hop bi chan 2 dau.
                                    if (Board[rw + 1, cl - 1] == Player.Human && Board[rw - 5, cl + 5] == Player.Human)
                                        EBoard.Board[rw - i, cl + i] = 0;
                                }
                                if ((cntHuman == 4 || cntMachine == 4)
                                    && (Board[rw - i + 1, cl + i - 1] == Player.None || Board[rw - i - 1, cl + i + 1] == Player.None))
                                    EBoard.Board[rw - i, cl + i] *= 2;
                            }
                }
        }
        // Ve gia tri luong gia ra bang !
        private void DrawEvalue()
        {            
            Graphics g = Parent.CreateGraphics();
            Rectangle rect = new Rectangle();

            for (int r = 1; r <= Height+1; r++)
                for (int c = 1; c <= Width+1; c++)
                {
                    rect = new Rectangle((c - 1) * 20, (r - 1) * 20, 20, 20);

                    Font fnt = new Font(FontFamily.GenericMonospace, 9);
                    g.DrawString(EBoard.Board[r, c].ToString(), fnt, Brushes.Black, rect);
                }
        }
        // Copy mot ban co khac.
        public void CopyTo(ref GomokuBoard Br)
        {
            int rw, cl;
            Br.Parent = this.Parent;
            Br.ResetBoard();
            Br.EBoard = new EValueBoard(Br);

            for (rw = 0; rw < Height + 2; rw++)
                for (cl = 0; cl < Height + 2; cl++)
                    Br.Board[rw, cl] = Board[rw, cl];
        }
        // Sinh nuoc di - do thong minh cua may.
        public int Depth = 0;
        static public int MaxDepth = 21;
        static public int MaxBreadth = 8;
        
        public Node[] WinMoves = new Node[MaxDepth + 1];
        public Node[] MyMoves = new Node[MaxBreadth + 1];
        public Node[] HisMoves = new Node[MaxBreadth + 1];
        public bool Win, Lose;
        // Ham de quy - Sinh nuoc di cho may.
        public void GenerateMoves()
        {
            if (Depth >= MaxDepth) return;
            Depth++;
            bool lose = false;
            Win = false;

            Node MyNode = new Node();   // Duong di quan ta.
            Node HisNode = new Node();  // Duong di doi thu.
            int count = 0;

            // Luong gia cho ma tran.
            EvalueGomokuBoard(Player.Machine);
            
            // Lay MaxBreadth nuoc di tot nhat.
            for (int i = 1; i <= MaxBreadth; i++)
            {
                MyNode = EBoard.GetMaxNode();
                MyMoves[i] = MyNode;
                EBoard.Board[MyNode.Row, MyNode.Column] = 0;
            }
            // Lay nuoc di ra khoi danh sach - Danh thu nuoc di.
            count = 0;
            while (count < MaxBreadth)
            {
                count++;
                MyNode = MyMoves[count];
                WinMoves.SetValue(MyNode, Depth);
                Board[MyNode.Row, MyNode.Column] = Player.Machine;

                // Tim cac nuoc di toi uu cua doi thu.
                EBoard.ResetBoard();
                EvalueGomokuBoard(Player.Human);
                for (int i = 1; i <= MaxBreadth; i++)
                {
                    HisNode = EBoard.GetMaxNode();
                    HisMoves[i] = HisNode;
                    EBoard.Board[HisNode.Row, HisNode.Column] = 0;
                }

                for (int i = 1; i <= MaxBreadth; i++)
                {
                    HisNode = HisMoves[i];
                    Board[HisNode.Row, HisNode.Column] = Player.Human;
                    // Kiem tra ket qua nuoc di.
                    if (CheckEnd(MyNode.Row, MyNode.Column) == Player.Machine)
                        Win = true;
                    if (CheckEnd(HisNode.Row, HisNode.Column) == Player.Human)
                        lose = true;

                    if (lose)
                    {
                        // Loai nuoc di thu.
                        Lose = true;
                        Board[HisNode.Row, HisNode.Column] = Player.None;
                        Board[MyNode.Row, MyNode.Column] = Player.None;
                        return;
                    }

                    if (Win)
                    {
                        // Loai nuoc di thu.
                        Board[HisNode.Row, HisNode.Column] = Player.None;
                        Board[MyNode.Row, MyNode.Column] = Player.None;
                        return;
                    }
                    else GenerateMoves(); // tim tiep.
                    // Loai nuoc di thu.
                    Board[HisNode.Row, HisNode.Column] = Player.None;
                }
                
                Board[MyNode.Row, MyNode.Column] = Player.None;
            }
            
        }
        // Goi Generator - Tim duong di cho may.
        public void GetGenResult()
        {
            Win = Lose = false;
            // Xoa mang duong di.
            WinMoves = new Node[MaxDepth + 1];
            for (int i = 0; i <= MaxDepth; i++)
                WinMoves[i] = new Node();
            
            // Xoa stack.
            for (int i = 0; i < MaxBreadth; i++)
                MyMoves[i] = new Node();

            Depth = 0;
            GenerateMoves();
            if (Win && !Lose) Parent.SendMessage(3);
        }
    }
}