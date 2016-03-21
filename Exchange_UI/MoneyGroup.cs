using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class MoneyGroup
    {
        public MoneyBoth[] allMoneyBoth;

        public MoneyGroup(int num)
        {
            allMoneyBoth = new MoneyBoth[num];
            for(int i = 0;i < allMoneyBoth.Length;i++)
            {
                allMoneyBoth[i] = new MoneyBoth();
            }
        }
    }
}
