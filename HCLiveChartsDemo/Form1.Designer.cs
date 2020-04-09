namespace HCLiveChartsDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.hcCircleChart1 = new HCLiveCharts.HCCircleChart();
            this.SuspendLayout();
            // 
            // hcCircleChart1
            // 
            this.hcCircleChart1.CenterColor = System.Drawing.Color.White;
            this.hcCircleChart1.CenterRadius = 60;
            this.hcCircleChart1.Colors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("hcCircleChart1.Colors")));
            this.hcCircleChart1.Location = new System.Drawing.Point(84, 12);
            this.hcCircleChart1.MaxRadius = 130;
            this.hcCircleChart1.Name = "hcCircleChart1";
            this.hcCircleChart1.Radius = 120;
            this.hcCircleChart1.Size = new System.Drawing.Size(606, 411);
            this.hcCircleChart1.TabIndex = 0;
            this.hcCircleChart1.Values = ((System.Collections.Generic.Dictionary<string, int>)(resources.GetObject("hcCircleChart1.Values")));
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.hcCircleChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private HCLiveCharts.HCCircleChart hcCircleChart1;
    }
}

