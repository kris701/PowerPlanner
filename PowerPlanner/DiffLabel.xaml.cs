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
    /// Interaction logic for DiffLabel.xaml
    /// </summary>
    public partial class DiffLabel : UserControl
    {
        private int lastPrc = 0;
        private int currentPrc = 0;

        public DiffLabel()
        {
            InitializeComponent();
        }

        public void UpdateLabel(float value)
        {
            SetValueLabel(value);
        }

        private void SetValueLabel(float value)
        {
            lastPrc = currentPrc;
            currentPrc = (int)(value * 100);
            if (lastPrc == 0)
                lastPrc = currentPrc;

            if (currentPrc == lastPrc)
                ValueLabel.Foreground = new SolidColorBrush(Colors.White);
            if (currentPrc < lastPrc)
                ValueLabel.Foreground = new SolidColorBrush(Colors.Red);
            if (currentPrc > lastPrc)
                ValueLabel.Foreground = new SolidColorBrush(Colors.Green);

            ValueLabel.Content = currentPrc - lastPrc;
        }
    }
}
