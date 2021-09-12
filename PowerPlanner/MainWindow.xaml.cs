using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PowerPlan> powerPlans;
        private int _previousIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            myNotifyIcon.Icon = new System.Drawing.Icon("powericon.ico");

            PositionWindowInCorner();

            SetupSlider();
        }

        private void ValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newValue = (int)ValueSlider.Value;
            if (_previousIndex != newValue && newValue != -1)
            {
                _previousIndex = newValue;
                PowerManager.SetActive(powerPlans[newValue]);
                ActivePowerPlanLabel.Content = powerPlans[newValue].Name;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void EditPowerPlansButton_Click(object sender, RoutedEventArgs e)
        {
            PowerManager.OpenControlPanel();
        }

        private void myNotifyIcon_PopupOpened(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            Activate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BlurHelper.EnableBlur(this);
        }

        private void PositionWindowInCorner()
        {
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 10;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 45;
        }

        private void SetupSlider()
        {
            powerPlans = PowerManager.GetAllPlans();

            ValueSlider.Maximum = powerPlans.Count - 1;

            Guid currentPlan = PowerManager.GetActiveGuid();
            for (int i = 0; i < powerPlans.Count; i++)
            {
                if (powerPlans[i].Guid == currentPlan)
                {
                    ValueSlider.Value = i;
                    break;
                }
            }
        }
    }
}
