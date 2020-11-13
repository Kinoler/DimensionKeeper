using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMod.Helpers
{
    public class CycledCounter
    {
        private int current;
        private int countValues;

        public CycledCounter()
        {
            current = -1;
            countValues = 0;
        }

        public int Current => current;
        public int Max => countValues;

        public void AddNew()
        {
            if (countValues == 0)
            {
                countValues = 0;
                current = 0;
            }

            countValues++;
        }

        public int Next()
        {
            current++;
            if (current >= countValues)
            {
                current = 0;
            }

            if (countValues == 0)
            {
                countValues = 0;
                current = -1;
            }

            return current;
        }
    }
}
