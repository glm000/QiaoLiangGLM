namespace JiYiTunnelSystem.ReceiveData
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Btn_Start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.COMList = new System.Windows.Forms.ComboBox();
            this.IPList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TargetList = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.OrderTxt = new System.Windows.Forms.TextBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btn_SendFile = new System.Windows.Forms.Button();
            this.txt_FoldPath = new System.Windows.Forms.TextBox();
            this.Btn_OpenFile = new System.Windows.Forms.Button();
            this.txt_Receive = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Start
            // 
            this.Btn_Start.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Start.Location = new System.Drawing.Point(128, 197);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Btn_Start.Size = new System.Drawing.Size(110, 39);
            this.Btn_Start.TabIndex = 0;
            this.Btn_Start.Text = "开启";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.COMList);
            this.groupBox1.Controls.Add(this.IPList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Btn_Start);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 266);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "本地设置";
            // 
            // COMList
            // 
            this.COMList.DropDownWidth = 240;
            this.COMList.Font = new System.Drawing.Font("宋体", 15F);
            this.COMList.FormattingEnabled = true;
            this.COMList.Items.AddRange(new object[] {
            "85"});
            this.COMList.Location = new System.Drawing.Point(106, 108);
            this.COMList.Name = "COMList";
            this.COMList.Size = new System.Drawing.Size(240, 33);
            this.COMList.TabIndex = 1;
            // 
            // IPList
            // 
            this.IPList.DropDownWidth = 240;
            this.IPList.Font = new System.Drawing.Font("宋体", 15F);
            this.IPList.FormattingEnabled = true;
            this.IPList.Items.AddRange(new object[] {
            "172.27.45.44",
            "127.0.0.1"});
            this.IPList.Location = new System.Drawing.Point(106, 36);
            this.IPList.Name = "IPList";
            this.IPList.Size = new System.Drawing.Size(240, 33);
            this.IPList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TargetList);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.OrderTxt);
            this.groupBox2.Controls.Add(this.Btn_Send);
            this.groupBox2.Location = new System.Drawing.Point(12, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 363);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送设置";
            // 
            // TargetList
            // 
            this.TargetList.DropDownWidth = 240;
            this.TargetList.Font = new System.Drawing.Font("宋体", 15F);
            this.TargetList.FormattingEnabled = true;
            this.TargetList.Location = new System.Drawing.Point(106, 35);
            this.TargetList.Name = "TargetList";
            this.TargetList.Size = new System.Drawing.Size(240, 33);
            this.TargetList.TabIndex = 1;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 14F);
            this.radioButton2.Location = new System.Drawing.Point(249, 250);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(67, 28);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "HEX";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "目标:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 14F);
            this.radioButton1.Location = new System.Drawing.Point(42, 250);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(91, 28);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "ASCII";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // OrderTxt
            // 
            this.OrderTxt.Font = new System.Drawing.Font("宋体", 14F);
            this.OrderTxt.Location = new System.Drawing.Point(17, 96);
            this.OrderTxt.Multiline = true;
            this.OrderTxt.Name = "OrderTxt";
            this.OrderTxt.Size = new System.Drawing.Size(329, 125);
            this.OrderTxt.TabIndex = 5;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Font = new System.Drawing.Font("宋体", 15F);
            this.Btn_Send.Location = new System.Drawing.Point(128, 301);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(110, 39);
            this.Btn_Send.TabIndex = 4;
            this.Btn_Send.Text = "发送";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Btn_SendFile);
            this.groupBox3.Controls.Add(this.txt_FoldPath);
            this.groupBox3.Controls.Add(this.Btn_OpenFile);
            this.groupBox3.Controls.Add(this.txt_Receive);
            this.groupBox3.Location = new System.Drawing.Point(377, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(738, 647);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "接收数据";
            // 
            // Btn_SendFile
            // 
            this.Btn_SendFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_SendFile.Location = new System.Drawing.Point(611, 585);
            this.Btn_SendFile.Name = "Btn_SendFile";
            this.Btn_SendFile.Size = new System.Drawing.Size(118, 39);
            this.Btn_SendFile.TabIndex = 10;
            this.Btn_SendFile.Text = "发送文件";
            this.Btn_SendFile.UseVisualStyleBackColor = true;
            this.Btn_SendFile.Click += new System.EventHandler(this.Btn_SendFile_Click);
            // 
            // txt_FoldPath
            // 
            this.txt_FoldPath.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_FoldPath.Location = new System.Drawing.Point(6, 588);
            this.txt_FoldPath.Multiline = true;
            this.txt_FoldPath.Name = "txt_FoldPath";
            this.txt_FoldPath.ReadOnly = true;
            this.txt_FoldPath.Size = new System.Drawing.Size(547, 33);
            this.txt_FoldPath.TabIndex = 9;
            // 
            // Btn_OpenFile
            // 
            this.Btn_OpenFile.BackColor = System.Drawing.Color.Transparent;
            this.Btn_OpenFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_OpenFile.BackgroundImage")));
            this.Btn_OpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_OpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_OpenFile.Location = new System.Drawing.Point(559, 585);
            this.Btn_OpenFile.Name = "Btn_OpenFile";
            this.Btn_OpenFile.Size = new System.Drawing.Size(46, 39);
            this.Btn_OpenFile.TabIndex = 8;
            this.Btn_OpenFile.UseVisualStyleBackColor = false;
            this.Btn_OpenFile.Click += new System.EventHandler(this.Btn_OpenFile_Click);
            // 
            // txt_Receive
            // 
            this.txt_Receive.Location = new System.Drawing.Point(6, 15);
            this.txt_Receive.Multiline = true;
            this.txt_Receive.Name = "txt_Receive";
            this.txt_Receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Receive.Size = new System.Drawing.Size(723, 547);
            this.txt_Receive.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 662);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "数据接收";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox COMList;
        private System.Windows.Forms.ComboBox IPList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_Receive;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox OrderTxt;
        private System.Windows.Forms.ComboBox TargetList;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button Btn_OpenFile;
        private System.Windows.Forms.TextBox txt_FoldPath;
        private System.Windows.Forms.Button Btn_SendFile;
    }
}

