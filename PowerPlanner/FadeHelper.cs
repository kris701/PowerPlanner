using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PowerPlanner
{
    public static class FadeHelper
    {
        public static async Task FadeIn(UIElement element, int ms, double step = 0.10)
        {
            element.Opacity = 0;
            for (double i = 0; i < 1.01; i += step)
            {
                element.Opacity = i;
                await Task.Delay(ms);
            }
            element.Opacity = 1;
        }

        public static async Task FadeOut(UIElement element, int ms, double step = 0.10)
        {
            element.Opacity = 1;
            for (double i = 1; i >= 0; i -= step)
            {
                element.Opacity = i;
                await Task.Delay(ms);
            }
            element.Opacity = 0;
        }
    }
}
