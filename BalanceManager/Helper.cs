using System;

namespace Balances
{
    // Token: 0x02000005 RID: 5
    public static class Helper
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002600 File Offset: 0x00000800
        public static int RandomGenerator(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
