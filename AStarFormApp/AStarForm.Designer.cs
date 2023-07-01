namespace AStarFormApp
{
    partial class AStarForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.MapPictureBox = new System.Windows.Forms.PictureBox();
            this.N4_Path_PictureBox = new System.Windows.Forms.PictureBox();
            this.DotDot_N4_Path_PictureBox = new System.Windows.Forms.PictureBox();
            this.N8_Path_PictureBox = new System.Windows.Forms.PictureBox();
            this.DotDot_N8_Path_PictureBox = new System.Windows.Forms.PictureBox();
            this.Trip_PictureBox = new System.Windows.Forms.PictureBox();
            this.DotDot_Trip_PictureBox = new System.Windows.Forms.PictureBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.ArrayPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.N4_Path_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_N4_Path_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.N8_Path_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_N8_Path_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trip_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_Trip_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrayPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MapPictureBox
            // 
            this.MapPictureBox.BackColor = System.Drawing.Color.White;
            this.MapPictureBox.Location = new System.Drawing.Point(12, 12);
            this.MapPictureBox.Name = "MapPictureBox";
            this.MapPictureBox.Size = new System.Drawing.Size(100, 100);
            this.MapPictureBox.TabIndex = 0;
            this.MapPictureBox.TabStop = false;
            // 
            // N4_Path_PictureBox
            // 
            this.N4_Path_PictureBox.BackColor = System.Drawing.Color.White;
            this.N4_Path_PictureBox.Location = new System.Drawing.Point(12, 118);
            this.N4_Path_PictureBox.Name = "N4_Path_PictureBox";
            this.N4_Path_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.N4_Path_PictureBox.TabIndex = 2;
            this.N4_Path_PictureBox.TabStop = false;
            // 
            // DotDot_N4_Path_PictureBox
            // 
            this.DotDot_N4_Path_PictureBox.BackColor = System.Drawing.Color.White;
            this.DotDot_N4_Path_PictureBox.Location = new System.Drawing.Point(118, 118);
            this.DotDot_N4_Path_PictureBox.Name = "DotDot_N4_Path_PictureBox";
            this.DotDot_N4_Path_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.DotDot_N4_Path_PictureBox.TabIndex = 3;
            this.DotDot_N4_Path_PictureBox.TabStop = false;
            // 
            // N8_Path_PictureBox
            // 
            this.N8_Path_PictureBox.BackColor = System.Drawing.Color.White;
            this.N8_Path_PictureBox.Location = new System.Drawing.Point(12, 224);
            this.N8_Path_PictureBox.Name = "N8_Path_PictureBox";
            this.N8_Path_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.N8_Path_PictureBox.TabIndex = 6;
            this.N8_Path_PictureBox.TabStop = false;
            // 
            // DotDot_N8_Path_PictureBox
            // 
            this.DotDot_N8_Path_PictureBox.BackColor = System.Drawing.Color.White;
            this.DotDot_N8_Path_PictureBox.Location = new System.Drawing.Point(118, 224);
            this.DotDot_N8_Path_PictureBox.Name = "DotDot_N8_Path_PictureBox";
            this.DotDot_N8_Path_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.DotDot_N8_Path_PictureBox.TabIndex = 7;
            this.DotDot_N8_Path_PictureBox.TabStop = false;
            // 
            // Trip_PictureBox
            // 
            this.Trip_PictureBox.BackColor = System.Drawing.Color.White;
            this.Trip_PictureBox.Location = new System.Drawing.Point(12, 330);
            this.Trip_PictureBox.Name = "Trip_PictureBox";
            this.Trip_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.Trip_PictureBox.TabIndex = 8;
            this.Trip_PictureBox.TabStop = false;
            // 
            // DotDot_Trip_PictureBox
            // 
            this.DotDot_Trip_PictureBox.BackColor = System.Drawing.Color.White;
            this.DotDot_Trip_PictureBox.Location = new System.Drawing.Point(118, 330);
            this.DotDot_Trip_PictureBox.Name = "DotDot_Trip_PictureBox";
            this.DotDot_Trip_PictureBox.Size = new System.Drawing.Size(100, 100);
            this.DotDot_Trip_PictureBox.TabIndex = 9;
            this.DotDot_Trip_PictureBox.TabStop = false;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(118, 12);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 11;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // ArrayPictureBox
            // 
            this.ArrayPictureBox.BackColor = System.Drawing.Color.White;
            this.ArrayPictureBox.Location = new System.Drawing.Point(224, 12);
            this.ArrayPictureBox.Name = "ArrayPictureBox";
            this.ArrayPictureBox.Size = new System.Drawing.Size(300, 300);
            this.ArrayPictureBox.TabIndex = 12;
            this.ArrayPictureBox.TabStop = false;
            // 
            // AStarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(535, 441);
            this.Controls.Add(this.ArrayPictureBox);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.DotDot_Trip_PictureBox);
            this.Controls.Add(this.Trip_PictureBox);
            this.Controls.Add(this.DotDot_N8_Path_PictureBox);
            this.Controls.Add(this.N8_Path_PictureBox);
            this.Controls.Add(this.DotDot_N4_Path_PictureBox);
            this.Controls.Add(this.N4_Path_PictureBox);
            this.Controls.Add(this.MapPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AStarForm";
            this.Text = "AStar";
            this.Load += new System.EventHandler(this.AStarForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.N4_Path_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_N4_Path_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.N8_Path_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_N8_Path_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Trip_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DotDot_Trip_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrayPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MapPictureBox;
        private System.Windows.Forms.PictureBox N4_Path_PictureBox;
        private System.Windows.Forms.PictureBox DotDot_N4_Path_PictureBox;
        private System.Windows.Forms.PictureBox N8_Path_PictureBox;
        private System.Windows.Forms.PictureBox DotDot_N8_Path_PictureBox;
        private System.Windows.Forms.PictureBox Trip_PictureBox;
        private System.Windows.Forms.PictureBox DotDot_Trip_PictureBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.PictureBox ArrayPictureBox;
    }
}

