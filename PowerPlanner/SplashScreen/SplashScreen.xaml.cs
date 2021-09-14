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
using System.Windows.Shapes;

namespace PowerPlanner.SplashScreen
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Opacity = 0;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BlurHelper.EnableBlur(this);
            await FadeHelper.FadeIn(this, 20, 0.05);
            await Task.Delay(2000);
            await FadeHelper.FadeOut(this, 20, 0.05);
            this.Close();
        }
    }
}
