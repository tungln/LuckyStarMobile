using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyStar.Models
{
    public class PercentItem
    {
        public string Name { get; set; }

        public double Percent { get; set; }

        public PercentItem(string name, double percent)
        {
            Name = name;
            Percent = percent;
        }
    }
}
