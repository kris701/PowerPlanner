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
    /// Interaction logic for PowerIconControl.xaml
    /// </summary>
    public partial class PowerIconControl : UserControl
    {
        private ICallback _callback;
        private int PowerCanvasHeight = 0;

        public PowerIconControl()
        {
            InitializeComponent();

            PowerCanvasHeight = (int)PowerHeightCanvas.Height;
        }

        public void Setup(ICallback callback)
        {
            _callback = callback;
            
            Task.Run(RunBackground);
        }

        private async Task RunBackground()
        {
            while (true)
            {
                Application.Current.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
                    {
                        Update();
                    });

#if DEBUG
                await Task.Delay(5000);
#else
                await Task.Delay(60000);
#endif
            }
        }

        public void Update()
        {
            PowerLabel.Content = $"{Math.Round(_callback.PowerStatus.BatteryLifePercent * 100,0)}%";
            PowerHeightCanvas.Height = _callback.PowerStatus.BatteryLifePercent * PowerCanvasHeight;
        }
    }
}
