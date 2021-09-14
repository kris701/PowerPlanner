using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerPlanner
{
    /// <summary>
    /// Interaction logic for PowerPlotControl.xaml
    /// </summary>
    public partial class PowerPlotControl : UserControl
    {
        private ICallback _callback;
        private double _canvasHeight = 0;
        private double _canvasWidth = 0;

        public PowerPlotControl()
        {
            InitializeComponent();
        }

        public void Setup(ICallback callback)
        {
            _callback = callback;

            Task.Run(RunBackground);

            Visibility = Visibility.Hidden;
        }

        private async Task RunBackground()
        {
            while (true)
            {
                Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
                    {
                        UpdateChart();
                    });

#if DEBUG
                await Task.Delay(5000);
#else
                await Task.Delay(60000);
#endif
            }
        }

        private void UpdateChart()
        {
            if (_canvasWidth == 0)
            {
                if (PlotCanvas.ActualWidth != 0)
                {
                    _canvasWidth = PlotCanvas.ActualWidth;
                    _canvasHeight = PlotCanvas.ActualHeight;
                }
            }
            else
            {
                DiffLabel.UpdateLabel(_callback.PowerStatus.BatteryLifePercent);

                double xOffset = _canvasWidth / 60;
                Line lastLine = null;

                foreach (UIElement child in PlotCanvas.Children)
                {
                    if (child is Line line)
                    {
                        line.X1 = line.X1 - xOffset;
                        line.X2 = line.X2 - xOffset;
                        lastLine = line;
                    }
                }

                double newYPos = _canvasHeight - (_callback.PowerStatus.BatteryLifePercent * _canvasHeight);
                Brush brush = new SolidColorBrush(Colors.Gray);

                if (lastLine == null)
                {
                    PlotCanvas.Children.Add(new Line()
                    {
                        X1 = _canvasWidth,
                        X2 = _canvasWidth - xOffset,
                        Y1 = newYPos,
                        Y2 = newYPos,
                        StrokeThickness = 2,
                        Stroke = brush
                    });
                }
                else
                {
                    PlotCanvas.Children.Add(new Line()
                    {
                        X1 = _canvasWidth,
                        X2 = _canvasWidth - xOffset,
                        Y1 = newYPos,
                        Y2 = lastLine.Y1,
                        StrokeThickness = 2,
                        Stroke = brush
                    });
                }

                if (PlotCanvas.Children.Count > 60)
                    PlotCanvas.Children.RemoveAt(0);
            }
        }
    }
}
