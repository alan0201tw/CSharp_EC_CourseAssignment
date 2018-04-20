using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_Simulation
{
    class Utility
    {
        public static Int64 LongRandom(Int64 min, Int64 max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            Int64 longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}