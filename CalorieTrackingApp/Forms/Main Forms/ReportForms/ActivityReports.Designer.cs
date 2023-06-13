namespace WndPL.Forms.Main_Forms.ReportForms
{
    partial class ActivityReports
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
            Guna.Charts.WinForms.ChartFont chartFont1 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont2 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont3 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont4 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid1 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick1 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont5 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid2 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick2 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont6 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid3 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel1 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont7 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick3 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont8 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.LPoint lPoint1 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint2 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint3 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint4 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint5 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint6 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint7 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.LPoint lPoint8 = new Guna.Charts.WinForms.LPoint();
            Guna.Charts.WinForms.ChartFont chartFont9 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont10 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont11 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.ChartFont chartFont12 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid4 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick4 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont13 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid5 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.Tick tick5 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont14 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Grid grid6 = new Guna.Charts.WinForms.Grid();
            Guna.Charts.WinForms.PointLabel pointLabel2 = new Guna.Charts.WinForms.PointLabel();
            Guna.Charts.WinForms.ChartFont chartFont15 = new Guna.Charts.WinForms.ChartFont();
            Guna.Charts.WinForms.Tick tick6 = new Guna.Charts.WinForms.Tick();
            Guna.Charts.WinForms.ChartFont chartFont16 = new Guna.Charts.WinForms.ChartFont();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            chartWeightHistory = new Guna.Charts.WinForms.GunaChart();
            lineDataSet = new Guna.Charts.WinForms.GunaLineDataset();
            activityChart = new Guna.Charts.WinForms.GunaChart();
            activityBarChart = new Guna.Charts.WinForms.GunaBarDataset();
            pnlTop = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblCalorie = new Label();
            lblMinute = new Label();
            tsMinuteCalorie = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            SuspendLayout();
            // 
            // chartWeightHistory
            // 
            chartWeightHistory.BackColor = Color.White;
            chartWeightHistory.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] { lineDataSet });
            chartWeightHistory.Legend.Display = false;
            chartFont1.FontName = "Arial";
            chartWeightHistory.Legend.LabelFont = chartFont1;
            chartWeightHistory.Location = new Point(210, 12);
            chartWeightHistory.Name = "chartWeightHistory";
            chartWeightHistory.Size = new Size(572, 232);
            chartWeightHistory.TabIndex = 0;
            chartFont2.FontName = "Arial";
            chartFont2.Size = 14;
            chartFont2.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            chartWeightHistory.Title.Font = chartFont2;
            chartWeightHistory.Title.ForeColor = Color.DarkViolet;
            chartWeightHistory.Title.Text = "Weight History";
            chartFont3.FontName = "Arial";
            chartWeightHistory.Tooltips.BodyFont = chartFont3;
            chartFont4.FontName = "Arial";
            chartFont4.Size = 9;
            chartFont4.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            chartWeightHistory.Tooltips.TitleFont = chartFont4;
            chartWeightHistory.XAxes.GridLines = grid1;
            chartFont5.FontName = "Arial";
            tick1.Font = chartFont5;
            chartWeightHistory.XAxes.Ticks = tick1;
            chartWeightHistory.YAxes.GridLines = grid2;
            chartFont6.FontName = "Arial";
            tick2.Font = chartFont6;
            chartWeightHistory.YAxes.Ticks = tick2;
            chartWeightHistory.ZAxes.GridLines = grid3;
            chartFont7.FontName = "Arial";
            pointLabel1.Font = chartFont7;
            chartWeightHistory.ZAxes.PointLabels = pointLabel1;
            chartFont8.FontName = "Arial";
            tick3.Font = chartFont8;
            chartWeightHistory.ZAxes.Ticks = tick3;
            // 
            // lineDataSet
            // 
            lineDataSet.BorderColor = Color.DarkViolet;
            lPoint1.Label = "0";
            lPoint1.Y = 70D;
            lPoint2.Label = "10";
            lPoint2.Y = 68D;
            lPoint3.Label = "15";
            lPoint3.Y = 67D;
            lPoint4.Label = "20";
            lPoint4.Y = 72D;
            lPoint5.Label = "25";
            lPoint5.Y = 70D;
            lPoint6.Label = "30";
            lPoint6.Y = 68D;
            lPoint7.Label = "35";
            lPoint7.Y = 70D;
            lPoint8.Label = "40";
            lPoint8.Y = 73D;
            lineDataSet.DataPoints.AddRange(new Guna.Charts.WinForms.LPoint[] { lPoint1, lPoint2, lPoint3, lPoint4, lPoint5, lPoint6, lPoint7, lPoint8 });
            lineDataSet.FillColor = Color.DarkViolet;
            lineDataSet.Label = "Weight";
            lineDataSet.TargetChart = chartWeightHistory;
            // 
            // activityChart
            // 
            activityChart.BackColor = Color.White;
            activityChart.Datasets.AddRange(new Guna.Charts.Interfaces.IGunaDataset[] { activityBarChart });
            activityChart.Legend.Display = false;
            chartFont9.FontName = "Arial";
            activityChart.Legend.LabelFont = chartFont9;
            activityChart.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            activityChart.Location = new Point(210, 270);
            activityChart.Name = "activityChart";
            activityChart.Size = new Size(572, 268);
            activityChart.TabIndex = 1;
            chartFont10.FontName = "Arial";
            chartFont10.Size = 14;
            chartFont10.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            activityChart.Title.Font = chartFont10;
            activityChart.Title.ForeColor = Color.Teal;
            activityChart.Title.Text = "Exercises (Minute)";
            chartFont11.FontName = "Arial";
            activityChart.Tooltips.BodyFont = chartFont11;
            chartFont12.FontName = "Arial";
            chartFont12.Size = 9;
            chartFont12.Style = Guna.Charts.WinForms.ChartFontStyle.Bold;
            activityChart.Tooltips.TitleFont = chartFont12;
            activityChart.XAxes.GridLines = grid4;
            chartFont13.FontName = "Arial";
            tick4.Font = chartFont13;
            activityChart.XAxes.Ticks = tick4;
            activityChart.YAxes.GridLines = grid5;
            chartFont14.FontName = "Arial";
            tick5.Font = chartFont14;
            activityChart.YAxes.Ticks = tick5;
            activityChart.ZAxes.GridLines = grid6;
            chartFont15.FontName = "Arial";
            pointLabel2.Font = chartFont15;
            activityChart.ZAxes.PointLabels = pointLabel2;
            chartFont16.FontName = "Arial";
            tick6.Font = chartFont16;
            activityChart.ZAxes.Ticks = tick6;
            // 
            // activityBarChart
            // 
            activityBarChart.BarPercentage = 0.6D;
            activityBarChart.Label = "Minute";
            activityBarChart.TargetChart = activityChart;
            // 
            // pnlTop
            // 
            pnlTop.BorderColor = Color.White;
            pnlTop.BorderRadius = 6;
            pnlTop.CustomizableEdges = customizableEdges1;
            pnlTop.FillColor = Color.Teal;
            pnlTop.FillColor2 = Color.MediumPurple;
            pnlTop.Location = new Point(145, 250);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlTop.Size = new Size(717, 12);
            pnlTop.TabIndex = 10;
            // 
            // lblCalorie
            // 
            lblCalorie.AutoSize = true;
            lblCalorie.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblCalorie.ForeColor = Color.MediumPurple;
            lblCalorie.Location = new Point(542, 544);
            lblCalorie.Name = "lblCalorie";
            lblCalorie.Size = new Size(76, 22);
            lblCalorie.TabIndex = 15;
            lblCalorie.Text = "Calorie";
            // 
            // lblMinute
            // 
            lblMinute.AutoSize = true;
            lblMinute.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblMinute.ForeColor = Color.Teal;
            lblMinute.Location = new Point(394, 544);
            lblMinute.Name = "lblMinute";
            lblMinute.Size = new Size(73, 22);
            lblMinute.TabIndex = 14;
            lblMinute.Text = "Minute";
            // 
            // tsMinuteCalorie
            // 
            tsMinuteCalorie.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            tsMinuteCalorie.CheckedState.FillColor = Color.MediumPurple;
            tsMinuteCalorie.CheckedState.InnerBorderColor = Color.White;
            tsMinuteCalorie.CheckedState.InnerColor = Color.White;
            tsMinuteCalorie.CustomizableEdges = customizableEdges3;
            tsMinuteCalorie.Location = new Point(473, 544);
            tsMinuteCalorie.Name = "tsMinuteCalorie";
            tsMinuteCalorie.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tsMinuteCalorie.Size = new Size(63, 24);
            tsMinuteCalorie.TabIndex = 13;
            tsMinuteCalorie.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            tsMinuteCalorie.UncheckedState.FillColor = Color.Teal;
            tsMinuteCalorie.UncheckedState.InnerBorderColor = Color.White;
            tsMinuteCalorie.UncheckedState.InnerColor = Color.White;
            tsMinuteCalorie.CheckedChanged += tsMinuteCalorie_CheckedChanged;
            // 
            // ActivityReports
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 580);
            Controls.Add(lblCalorie);
            Controls.Add(lblMinute);
            Controls.Add(tsMinuteCalorie);
            Controls.Add(pnlTop);
            Controls.Add(activityChart);
            Controls.Add(chartWeightHistory);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ActivityReports";
            Text = "ActivityReports";
            Load += ActivityReports_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.Charts.WinForms.GunaChart chartWeightHistory;
        private Guna.Charts.WinForms.GunaLineDataset lineDataSet;
        private Guna.Charts.WinForms.GunaChart activityChart;
        private Guna.Charts.WinForms.GunaBarDataset activityBarChart;
        private Label label1;
        private Label label2;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlTop;
        private Label lblCalorie;
        private Label lblMinute;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tsMinuteCalorie;
    }
}