namespace GomokuGame
{
    partial class frmOption
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
            this.btnOk = new System.Windows.Forms.Button();
            this.chkFirstMove = new System.Windows.Forms.CheckBox();
            this.cmbKinhNghiem = new System.Windows.Forms.ComboBox();
            this.lblKinhNghiem = new System.Windows.Forms.Label();
            this.scrTrinhDo = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.grbMoves = new System.Windows.Forms.GroupBox();
            this.rbtClassic = new System.Windows.Forms.RadioButton();
            this.rbtNut = new System.Windows.Forms.RadioButton();
            this.rbtWeb = new System.Windows.Forms.RadioButton();
            this.grbMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(86, 250);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkFirstMove
            // 
            this.chkFirstMove.AutoSize = true;
            this.chkFirstMove.Location = new System.Drawing.Point(12, 12);
            this.chkFirstMove.Name = "chkFirstMove";
            this.chkFirstMove.Size = new System.Drawing.Size(149, 17);
            this.chkFirstMove.TabIndex = 1;
            this.chkFirstMove.Text = "Muon danh truoc khong ?";
            this.chkFirstMove.UseVisualStyleBackColor = true;
            // 
            // cmbKinhNghiem
            // 
            this.cmbKinhNghiem.FormattingEnabled = true;
            this.cmbKinhNghiem.Items.AddRange(new object[] {
            "Chuyen gia tan cong",
            "Chuyen gia phong thu",
            "Phong thu chat, tan cong nhanh"});
            this.cmbKinhNghiem.Location = new System.Drawing.Point(86, 46);
            this.cmbKinhNghiem.Name = "cmbKinhNghiem";
            this.cmbKinhNghiem.Size = new System.Drawing.Size(124, 21);
            this.cmbKinhNghiem.TabIndex = 2;
            this.cmbKinhNghiem.Text = "Phong thu chat, tan cong nhanh";
            // 
            // lblKinhNghiem
            // 
            this.lblKinhNghiem.AutoSize = true;
            this.lblKinhNghiem.Location = new System.Drawing.Point(12, 49);
            this.lblKinhNghiem.Name = "lblKinhNghiem";
            this.lblKinhNghiem.Size = new System.Drawing.Size(68, 13);
            this.lblKinhNghiem.TabIndex = 3;
            this.lblKinhNghiem.Text = "Kinh nghiem:";
            // 
            // scrTrinhDo
            // 
            this.scrTrinhDo.LargeChange = 3;
            this.scrTrinhDo.Location = new System.Drawing.Point(86, 85);
            this.scrTrinhDo.Maximum = 21;
            this.scrTrinhDo.Name = "scrTrinhDo";
            this.scrTrinhDo.Size = new System.Drawing.Size(124, 17);
            this.scrTrinhDo.SmallChange = 3;
            this.scrTrinhDo.TabIndex = 4;
            this.scrTrinhDo.Value = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Trinh do :";
            // 
            // grbMoves
            // 
            this.grbMoves.Controls.Add(this.rbtWeb);
            this.grbMoves.Controls.Add(this.rbtNut);
            this.grbMoves.Controls.Add(this.rbtClassic);
            this.grbMoves.Location = new System.Drawing.Point(18, 126);
            this.grbMoves.Name = "grbMoves";
            this.grbMoves.Size = new System.Drawing.Size(200, 100);
            this.grbMoves.TabIndex = 6;
            this.grbMoves.TabStop = false;
            this.grbMoves.Text = "Chon Quan Co";
            // 
            // rbtClassic
            // 
            this.rbtClassic.AutoSize = true;
            this.rbtClassic.Checked = true;
            this.rbtClassic.Location = new System.Drawing.Point(29, 19);
            this.rbtClassic.Name = "rbtClassic";
            this.rbtClassic.Size = new System.Drawing.Size(92, 17);
            this.rbtClassic.TabIndex = 0;
            this.rbtClassic.TabStop = true;
            this.rbtClassic.Text = "Kieu kinh dien";
            this.rbtClassic.UseVisualStyleBackColor = true;
            // 
            // rbtNut
            // 
            this.rbtNut.AutoSize = true;
            this.rbtNut.Location = new System.Drawing.Point(29, 42);
            this.rbtNut.Name = "rbtNut";
            this.rbtNut.Size = new System.Drawing.Size(64, 17);
            this.rbtNut.TabIndex = 1;
            this.rbtNut.Text = "Kieu nut";
            this.rbtNut.UseVisualStyleBackColor = true;
            // 
            // rbtWeb
            // 
            this.rbtWeb.AutoSize = true;
            this.rbtWeb.Location = new System.Drawing.Point(29, 65);
            this.rbtWeb.Name = "rbtWeb";
            this.rbtWeb.Size = new System.Drawing.Size(102, 17);
            this.rbtWeb.TabIndex = 2;
            this.rbtWeb.Text = "Kieu mang nhen";
            this.rbtWeb.UseVisualStyleBackColor = true;
            // 
            // frmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 285);
            this.ControlBox = false;
            this.Controls.Add(this.grbMoves);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scrTrinhDo);
            this.Controls.Add(this.lblKinhNghiem);
            this.Controls.Add(this.cmbKinhNghiem);
            this.Controls.Add(this.chkFirstMove);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmOption";
            this.ShowInTaskbar = false;
            this.Text = "Option";
            this.grbMoves.ResumeLayout(false);
            this.grbMoves.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblKinhNghiem;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkFirstMove;
        public System.Windows.Forms.ComboBox cmbKinhNghiem;
        public System.Windows.Forms.HScrollBar scrTrinhDo;
        public System.Windows.Forms.GroupBox grbMoves;
        public System.Windows.Forms.RadioButton rbtWeb;
        public System.Windows.Forms.RadioButton rbtNut;
        public System.Windows.Forms.RadioButton rbtClassic;
    }
}