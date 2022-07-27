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
            this.LatencyListBox = new System.Windows.Forms.ListBox();
            this.JitterLabel = new System.Windows.Forms.Label();
            this.JitterValueLabel = new System.Windows.Forms.Label();
            this.IntervalSelectBox = new System.Windows.Forms.ComboBox();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.LatencyLabel = new System.Windows.Forms.Label();
            this.LatencyValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartPingButton
            // 
            this.StartPingButton.Enabled = false;
            this.StartPingButton.Location = new System.Drawing.Point(312, 25);
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
            this.IPAddressLabel.Location = new System.Drawing.Point(12, 9);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(62, 15);
            this.IPAddressLabel.TabIndex = 3;
            this.IPAddressLabel.Text = "Hostname";
            // 
            // LatencyListBox
            // 
            this.LatencyListBox.Enabled = false;
            this.LatencyListBox.FormattingEnabled = true;
            this.LatencyListBox.ItemHeight = 15;
            this.LatencyListBox.Location = new System.Drawing.Point(12, 56);
            this.LatencyListBox.Name = "LatencyListBox";
            this.LatencyListBox.Size = new System.Drawing.Size(167, 379);
            this.LatencyListBox.TabIndex = 4;
            // 
            // JitterLabel
            // 
            this.JitterLabel.AutoSize = true;
            this.JitterLabel.Location = new System.Drawing.Point(274, 56);
            this.JitterLabel.Name = "JitterLabel";
            this.JitterLabel.Size = new System.Drawing.Size(32, 15);
            this.JitterLabel.TabIndex = 5;
            this.JitterLabel.Text = "Jitter";
            // 
            // JitterValueLabel
            // 
            this.JitterValueLabel.AutoSize = true;
            this.JitterValueLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JitterValueLabel.ForeColor = System.Drawing.Color.Coral;
            this.JitterValueLabel.Location = new System.Drawing.Point(274, 71);
            this.JitterValueLabel.Name = "JitterValueLabel";
            this.JitterValueLabel.Size = new System.Drawing.Size(20, 25);
            this.JitterValueLabel.TabIndex = 6;
            this.JitterValueLabel.Text = "-";
            // 
            // IntervalSelectBox
            // 
            this.IntervalSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IntervalSelectBox.FormattingEnabled = true;
            this.IntervalSelectBox.Location = new System.Drawing.Point(185, 26);
            this.IntervalSelectBox.Name = "IntervalSelectBox";
            this.IntervalSelectBox.Size = new System.Drawing.Size(121, 23);
            this.IntervalSelectBox.TabIndex = 7;
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Location = new System.Drawing.Point(185, 8);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(46, 15);
            this.IntervalLabel.TabIndex = 8;
            this.IntervalLabel.Text = "Interval";
            // 
            // LatencyLabel
            // 
            this.LatencyLabel.AutoSize = true;
            this.LatencyLabel.Location = new System.Drawing.Point(185, 56);
            this.LatencyLabel.Name = "LatencyLabel";
            this.LatencyLabel.Size = new System.Drawing.Size(48, 15);
            this.LatencyLabel.TabIndex = 9;
            this.LatencyLabel.Text = "Latency";
            // 
            // LatencyValue
            // 
            this.LatencyValue.AutoSize = true;
            this.LatencyValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LatencyValue.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.LatencyValue.Location = new System.Drawing.Point(185, 71);
            this.LatencyValue.Name = "LatencyValue";
            this.LatencyValue.Size = new System.Drawing.Size(20, 25);
            this.LatencyValue.TabIndex = 10;
            this.LatencyValue.Text = "-";
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LatencyValue);
            this.Controls.Add(this.LatencyLabel);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.IntervalSelectBox);
            this.Controls.Add(this.JitterValueLabel);
            this.Controls.Add(this.JitterLabel);
            this.Controls.Add(this.LatencyListBox);
            this.Controls.Add(this.IPAddressLabel);
            this.Controls.Add(this.IPAddressTextBox);
            this.Controls.Add(this.StartPingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseWindow";
            this.Text = "Pingle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private Button StartPingButton;
    private TextBox IPAddressTextBox;
    private Label IPAddressLabel;
    private ListBox LatencyListBox;
    private Label JitterLabel;
    private Label JitterValueLabel;
    private ComboBox IntervalSelectBox;
    private Label IntervalLabel;
    private Label LatencyLabel;
    private Label LatencyValue;
}
