using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PowerPlanner
{
    public interface ICallback
    {
        public List<PowerPlan> PowerPlans { get; }
        public void ToggleItemInContextField(UserControl element);
        public void AddItemToContextField(UserControl element);
    }
}
