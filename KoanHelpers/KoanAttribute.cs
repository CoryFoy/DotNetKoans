using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetKoans
{
    public class KoanAttribute : Xunit.FactAttribute
    {
        public int Position { get; set; }

        public KoanAttribute(int position) { this.Position = position; }
    }
}
