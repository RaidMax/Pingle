using System.Globalization;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Pingle.Shared.Abstractions;
using Pingle.Shared.Abstractions.Connection;
using Pingle.UI.Win.Graphics;

namespace Pingle.UI.Win;

public partial class BaseWindow : Form
{
    private IQualityMonitor? _qualityMonitor;

    public BaseWindow()
    {
        InitializeComponent();

        IntervalSelectBox.Items.Add("10");
        IntervalSelectBox.Items.Add("100");
        IntervalSelectBox.Items.Add("500");
        IntervalSelectBox.Items.Add("1000");
        IntervalSelectBox.Items.Add("5000");
        IntervalSelectBox.Items.Add("10000");
        IntervalSelectBox.SelectedIndex = 0;
    }

    private async void StartPingButton_Click(object sender, EventArgs e)
    {
        if (StartPingButton.Text == "Stop")
        {
            ResetForm();
            return;
        }

        if (_qualityMonitor is not null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(IPAddressTextBox.Text))
        {
            return;
        }

        StartPingButton.Enabled = false;

        // todo: this should be done elsewhere
        IPAddress address;
        try
        {
            address = (await Dns.GetHostAddressesAsync(IPAddressTextBox.Text)).First();
        }
        catch
        {
            MessageBox.Show("Could not resolve hostname.", "Error!");
            StartPingButton.Enabled = true;
            return;
        }

        SetupQualityMonitor(address);

        StartPingButton.Text = "Stop";
        IntervalSelectBox.Enabled = false;
        IPAddressTextBox.Enabled = false;
        StartPingButton.Enabled = true;
    }

    private void ResetForm()
    {
        IntervalSelectBox.Enabled = true;
        IPAddressTextBox.Enabled = true;
        StartPingButton.Text = "Start";
        LatencyValue.Text = "-";
        JitterValueLabel.Text = "-";
        _qualityMonitor?.Stop();
        _qualityMonitor?.Dispose();
        _qualityMonitor = null;
    }

    private void SetupQualityMonitor(IPAddress address)
    {
        _qualityMonitor = Program.ServiceProvider.GetRequiredService<IQualityMonitorFactory>().Create(
            new ConnectionParameters
            {
                Endpoint = new IPEndPoint(address, 0)
            });

        var interval = int.Parse((string)IntervalSelectBox.SelectedItem);
        _qualityMonitor.Start();
        _qualityMonitor.SetPollingInterval(TimeSpan.FromMilliseconds(interval));

        _qualityMonitor.OnErrorEncountered += (_, errorEvent) =>
        {
            Invoke(() =>
            {
                if (IsDisposed || Disposing)
                {
                    return;
                }

                if (errorEvent.IsFatal)
                {
                    ResetForm();
                }

                MessageBox.Show(errorEvent.Message, "Error!", MessageBoxButtons.OK);
            });
        };

        _qualityMonitor.OnJitterUpdated += (_, jitterEvent) =>
        {
            Invoke(() =>
            {
                if (IsDisposed || Disposing)
                {
                    return;
                }

                JitterValueLabel.Text = Math.Round(jitterEvent.Jitter, 3).ToString(CultureInfo.InvariantCulture);
            });
        };

        _qualityMonitor.OnLatencyUpdated += (_, latencyEvent) =>
        {
            Invoke(() =>
            {
                if (IsDisposed || Disposing)
                {
                    return;
                }

                LatencyValue.Text = Math.Round(latencyEvent.Latency, 3).ToString(CultureInfo.InvariantCulture);

                QualityGraph.Image ??= new Bitmap(QualityGraph.Width, QualityGraph.Height);
                QualityGraph.Refresh();
            });
        };
    }

    private void IPAddressTextBox_TextChanged(object sender, EventArgs e)
    {
        StartPingButton.Enabled = !string.IsNullOrWhiteSpace(IPAddressTextBox.Text);
    }

    private void BaseWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        _qualityMonitor?.Stop();
        _qualityMonitor?.Dispose();
    }

    private void QualityGraph_Paint(object sender, PaintEventArgs e)
    {
        if (QualityGraph.Image is null || _qualityMonitor is null)
        {
            return;
        }

        var orderedSamples = _qualityMonitor.Samples.OrderBy(sample => sample.SampleTime).ToList();
        var durationSamples = orderedSamples.Select(sample => new DataItem
            { Point = sample.Duration.HasValue ? new PointF(0, (float)sample.Duration.Value) : null }).ToArray();
        var varianceSamples = orderedSamples.Select(sample => new DataItem
            { Point = sample.Variance.HasValue ? new PointF(0, (float)sample.Variance.Value) : null }).ToArray();

        var durationSet = new DataSet
        {
            Points = durationSamples,
            DisplayInfo = new DisplayInfo
            {
                Label = "Duration",
                Color = Color.MediumSlateBlue
            }
        };

        var varianceSet = new DataSet
        {
            Points = varianceSamples,
            DisplayInfo = new DisplayInfo
            {
                Label = "Variance",
                Color = Color.DarkCyan
            }
        };

        Graph.RenderGraphOnGraphics(e.Graphics, e.ClipRectangle, durationSet, varianceSet);
    }
}
