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
    /// Interaction logic for PlanSwitcherControl.xaml
    /// </summary>
    public partial class PlanSwitcherControl : UserControl
    {
        private ICallback _callback;
        private int _previousIndex = 0;


        public PlanSwitcherControl()
        {
            InitializeComponent();
        }

        public void Setup(ICallback callback)
        {
            _callback = callback;

            SetupSlider();
        }

        private void SetupSlider()
        {
            ValueSlider.Maximum = _callback.PowerPlans.Count - 1;

            Guid currentPlan = PowerManager.GetActiveGuid();
            for (int i = 0; i < _callback.PowerPlans.Count; i++)
            {
                if (_callback.PowerPlans[i].Guid == currentPlan)
                {
                    ValueSlider.Value = i;
                    break;
                }
            }
        }

        private void EditPowerPlansButton_Click(object sender, RoutedEventArgs e)
        {
            PowerManager.OpenControlPanel();
        }

        private void ValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newValue = (int)ValueSlider.Value;
            if (_previousIndex != newValue && newValue != -1)
            {
                _previousIndex = newValue;
                PowerManager.SetActive(_callback.PowerPlans[newValue]);
                ActivePowerPlanLabel.Content = _callback.PowerPlans[newValue].Name;
            }
        }
    }
}
