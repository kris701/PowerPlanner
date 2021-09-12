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
    public partial class MainWindow : Window, ICallback
    {
        public List<PowerPlan> PowerPlans { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            myNotifyIcon.Icon = new System.Drawing.Icon("powericon.ico");

            PositionWindowInCorner();

            PowerPlans = PowerManager.GetAllPlans();

            PlanSwitcherControl.Setup(this);

            SetupContextMenu();

            Visibility = Visibility.Hidden;
        }

        public void ToggleItemInContextField(UserControl element)
        {
            if (element.Visibility == Visibility.Visible)
            {
                element.Visibility = Visibility.Collapsed;
                this.Height = this.Height - element.Height;
                this.Top = this.Top + element.Height;
            }
            else
            {
                element.Visibility = Visibility.Visible;
                this.Height = this.Height + element.Height;
                this.Top = this.Top - element.Height;
            }
        }

        public void AddItemToContextField(UserControl element)
        {
            ContextField.Children.Add(element);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void myNotifyIcon_PopupOpened(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Visible;
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


        private void SetupContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Header = "Exit";
            item.Click += ExitButton_Click;
            contextMenu.Items.Add(item);
            myNotifyIcon.ContextMenu = contextMenu;
            myNotifyIcon.ContextMenu.IsOpen = false;
        }

        private void myNotifyIcon_TrayRightMouseDown(object sender, RoutedEventArgs e)
        {
            myNotifyIcon.ContextMenu.IsOpen = true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PowerGridButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleItemInContextField(PowerPlotControl);
        }
    }
}
