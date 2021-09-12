using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlanner
{
    public class PowerPlan
    {
        public readonly string Name;
        public Guid Guid;

        public PowerPlan(string name, Guid guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}
