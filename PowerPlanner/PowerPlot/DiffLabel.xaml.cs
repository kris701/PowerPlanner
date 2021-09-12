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
        private List<int> LatestDiffs = new List<int>();

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
            LatestDiffs.Add(currentPrc - lastPrc);
            if (LatestDiffs.Count > 10)
                LatestDiffs.RemoveAt(0);

            long total = 0;
            foreach (int i in LatestDiffs)
                total += i;
            double totalDiff = (double)total / 10;

            if (totalDiff == 0)
                ValueLabel.Foreground = new SolidColorBrush(Colors.White);
            if (totalDiff < 0)
                ValueLabel.Foreground = new SolidColorBrush(Colors.Red);
            if (totalDiff > 0)
                ValueLabel.Foreground = new SolidColorBrush(Colors.Green);

            ValueLabel.Content = Math.Round(totalDiff,2);
        }
    }
}
