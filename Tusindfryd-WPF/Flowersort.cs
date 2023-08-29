using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tusindfryd_WPF
{
    internal class Flowersort
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public int ProductionTime { get; set; }
        public int HalfLife { get; set; }
        public int Size { get; set; }

        public Flowersort(string name, string picturePath, int productionTime, int halfLife, int size)
        {
            Name = name;
            PicturePath = picturePath;
            ProductionTime = productionTime;
            HalfLife = halfLife;
            Size = size;
        }
    }
}
