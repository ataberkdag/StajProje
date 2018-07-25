namespace APIProje
{
    partial class AnaForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTekrar = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblVeri = new System.Windows.Forms.Label();
            this.lblDLL = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnSeçenekler = new System.Windows.Forms.Button();
            this.btnDurdur = new System.Windows.Forms.Button();
            this.btnAra = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.panel1.Controls.Add(this.lblTekrar);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.lblVeri);
            this.panel1.Controls.Add(this.lblDLL);
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Controls.Add(this.btnSeçenekler);
            this.panel1.Controls.Add(this.btnDurdur);
            this.panel1.Controls.Add(this.btnAra);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 459);
            this.panel1.TabIndex = 0;
            // 
            // lblTekrar
            // 
            this.lblTekrar.AutoSize = true;
            this.lblTekrar.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTekrar.ForeColor = System.Drawing.Color.White;
            this.lblTekrar.Location = new System.Drawing.Point(12, 166);
            this.lblTekrar.Name = "lblTekrar";
            this.lblTekrar.Size = new System.Drawing.Size(125, 21);
            this.lblTekrar.TabIndex = 9;
            this.lblTekrar.Text = "Tekrar Sayısı: 0";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 65);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 3);
            this.panel5.TabIndex = 8;
            // 
            // lblVeri
            // 
            this.lblVeri.AutoSize = true;
            this.lblVeri.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblVeri.ForeColor = System.Drawing.Color.White;
            this.lblVeri.Location = new System.Drawing.Point(12, 134);
            this.lblVeri.Name = "lblVeri";
            this.lblVeri.Size = new System.Drawing.Size(107, 21);
            this.lblVeri.TabIndex = 6;
            this.lblVeri.Text = "Veri Sayısı: 0";
            // 
            // lblDLL
            // 
            this.lblDLL.AutoSize = true;
            this.lblDLL.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDLL.ForeColor = System.Drawing.Color.White;
            this.lblDLL.Location = new System.Drawing.Point(12, 101);
            this.lblDLL.Name = "lblDLL";
            this.lblDLL.Size = new System.Drawing.Size(90, 21);
            this.lblDLL.TabIndex = 5;
            this.lblDLL.Text = "DLL Sayısı:";
            // 
            // cbType
            // 
            this.cbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Kelime",
            "Kullanıcı",
            "Tümü"});
            this.cbType.Location = new System.Drawing.Point(3, 423);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(194, 24);
            this.cbType.TabIndex = 4;
            this.cbType.Text = "Tümü";
            // 
            // btnSeçenekler
            // 
            this.btnSeçenekler.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSeçenekler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeçenekler.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSeçenekler.ForeColor = System.Drawing.Color.White;
            this.btnSeçenekler.Location = new System.Drawing.Point(3, 356);
            this.btnSeçenekler.Name = "btnSeçenekler";
            this.btnSeçenekler.Size = new System.Drawing.Size(194, 61);
            this.btnSeçenekler.TabIndex = 3;
            this.btnSeçenekler.Text = "Seçenekler";
            this.btnSeçenekler.UseVisualStyleBackColor = true;
            this.btnSeçenekler.Click += new System.EventHandler(this.btnSeçenekler_Click);
            // 
            // btnDurdur
            // 
            this.btnDurdur.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDurdur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDurdur.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDurdur.ForeColor = System.Drawing.Color.White;
            this.btnDurdur.Location = new System.Drawing.Point(3, 289);
            this.btnDurdur.Name = "btnDurdur";
            this.btnDurdur.Size = new System.Drawing.Size(194, 61);
            this.btnDurdur.TabIndex = 2;
            this.btnDurdur.Text = "Durdur";
            this.btnDurdur.UseVisualStyleBackColor = true;
            this.btnDurdur.Click += new System.EventHandler(this.btnDurdur_Click);
            // 
            // btnAra
            // 
            this.btnAra.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAra.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAra.ForeColor = System.Drawing.Color.White;
            this.btnAra.Location = new System.Drawing.Point(3, 222);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(194, 61);
            this.btnAra.TabIndex = 1;
            this.btnAra.Text = "Ara";
            this.btnAra.UseVisualStyleBackColor = true;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 65);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "Proje";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.mainLayout);
            this.panel3.Location = new System.Drawing.Point(206, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(526, 405);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mainProgressBar);
            this.panel4.Location = new System.Drawing.Point(206, 423);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(526, 24);
            this.panel4.TabIndex = 7;
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 405F));
            this.mainLayout.Size = new System.Drawing.Size(526, 405);
            this.mainLayout.TabIndex = 13;
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainProgressBar.Location = new System.Drawing.Point(0, 0);
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(526, 24);
            this.mainProgressBar.TabIndex = 17;
            // 
            // AnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 459);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AnaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proje";
            this.Load += new System.EventHandler(this.AnaForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Button btnSeçenekler;
        private System.Windows.Forms.Button btnDurdur;
        private System.Windows.Forms.Button btnAra;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblVeri;
        private System.Windows.Forms.Label lblDLL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTekrar;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.ProgressBar mainProgressBar;
    }
}