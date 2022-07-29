namespace Pingle.UI.Win;

partial class BaseWindow
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseWindow));
            this.StartPingButton = new System.Windows.Forms.Button();
            this.IPAddressTextBox = new System.Windows.Forms.TextBox();
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.JitterLabel = new System.Windows.Forms.Label();
            this.JitterValueLabel = new System.Windows.Forms.Label();
            this.IntervalSelectBox = new System.Windows.Forms.ComboBox();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.LatencyLabel = new System.Windows.Forms.Label();
            this.LatencyValue = new System.Windows.Forms.Label();
            this.QualityGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.QualityGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // StartPingButton
            // 
            this.StartPingButton.Enabled = false;
            this.StartPingButton.Location = new System.Drawing.Point(314, 27);
            this.StartPingButton.Name = "StartPingButton";
            this.StartPingButton.Size = new System.Drawing.Size(75, 23);
            this.StartPingButton.TabIndex = 1;
            this.StartPingButton.Text = "Start";
            this.StartPingButton.UseVisualStyleBackColor = true;
            this.StartPingButton.Click += new System.EventHandler(this.StartPingButton_Click);
            // 
            // IPAddressTextBox
            // 
            this.IPAddressTextBox.Location = new System.Drawing.Point(12, 27);
            this.IPAddressTextBox.Name = "IPAddressTextBox";
            this.IPAddressTextBox.Size = new System.Drawing.Size(167, 23);
            this.IPAddressTextBox.TabIndex = 2;
            this.IPAddressTextBox.TextChanged += new System.EventHandler(this.IPAddressTextBox_TextChanged);
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(12, 8);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(62, 15);
            this.IPAddressLabel.TabIndex = 3;
            this.IPAddressLabel.Text = "Hostname";
            // 
            // JitterLabel
            // 
            this.JitterLabel.AutoSize = true;
            this.JitterLabel.Location = new System.Drawing.Point(92, 68);
            this.JitterLabel.Name = "JitterLabel";
            this.JitterLabel.Size = new System.Drawing.Size(32, 15);
            this.JitterLabel.TabIndex = 5;
            this.JitterLabel.Text = "Jitter";
            // 
            // JitterValueLabel
            // 
            this.JitterValueLabel.AutoSize = true;
            this.JitterValueLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JitterValueLabel.ForeColor = System.Drawing.Color.DarkCyan;
            this.JitterValueLabel.Location = new System.Drawing.Point(92, 83);
            this.JitterValueLabel.Name = "JitterValueLabel";
            this.JitterValueLabel.Size = new System.Drawing.Size(20, 25);
            this.JitterValueLabel.TabIndex = 6;
            this.JitterValueLabel.Text = "-";
            // 
            // IntervalSelectBox
            // 
            this.IntervalSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IntervalSelectBox.FormattingEnabled = true;
            this.IntervalSelectBox.Location = new System.Drawing.Point(187, 27);
            this.IntervalSelectBox.Name = "IntervalSelectBox";
            this.IntervalSelectBox.Size = new System.Drawing.Size(121, 23);
            this.IntervalSelectBox.TabIndex = 7;
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Location = new System.Drawing.Point(187, 8);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(46, 15);
            this.IntervalLabel.TabIndex = 8;
            this.IntervalLabel.Text = "Interval";
            // 
            // LatencyLabel
            // 
            this.LatencyLabel.AutoSize = true;
            this.LatencyLabel.Location = new System.Drawing.Point(12, 68);
            this.LatencyLabel.Name = "LatencyLabel";
            this.LatencyLabel.Size = new System.Drawing.Size(48, 15);
            this.LatencyLabel.TabIndex = 9;
            this.LatencyLabel.Text = "Latency";
            // 
            // LatencyValue
            // 
            this.LatencyValue.AutoSize = true;
            this.LatencyValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LatencyValue.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.LatencyValue.Location = new System.Drawing.Point(12, 83);
            this.LatencyValue.Name = "LatencyValue";
            this.LatencyValue.Size = new System.Drawing.Size(20, 25);
            this.LatencyValue.TabIndex = 10;
            this.LatencyValue.Text = "-";
            // 
            // QualityGraph
            // 
            this.QualityGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QualityGraph.Location = new System.Drawing.Point(12, 111);
            this.QualityGraph.Name = "QualityGraph";
            this.QualityGraph.Size = new System.Drawing.Size(459, 153);
            this.QualityGraph.TabIndex = 11;
            this.QualityGraph.TabStop = false;
            this.QualityGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.QualityGraph_Paint);
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 276);
            this.Controls.Add(this.QualityGraph);
            this.Controls.Add(this.LatencyValue);
            this.Controls.Add(this.LatencyLabel);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.IntervalSelectBox);
            this.Controls.Add(this.JitterValueLabel);
            this.Controls.Add(this.JitterLabel);
            this.Controls.Add(this.IPAddressLabel);
            this.Controls.Add(this.IPAddressTextBox);
            this.Controls.Add(this.StartPingButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(499, 315);
            this.Name = "BaseWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pingle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.QualityGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private Button StartPingButton;
    private TextBox IPAddressTextBox;
    private Label IPAddressLabel;
    private Label JitterLabel;
    private Label JitterValueLabel;
    private ComboBox IntervalSelectBox;
    private Label IntervalLabel;
    private Label LatencyLabel;
    private Label LatencyValue;
    private PictureBox QualityGraph;
}
