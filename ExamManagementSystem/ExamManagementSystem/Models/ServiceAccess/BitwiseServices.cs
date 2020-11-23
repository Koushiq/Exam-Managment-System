using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public static class BitwiseServices
    {
        public static List<int> GetEnabledIndexList(int integerValue)
        {
            List<int> enabledIndecesList = new List<int>();
            for (int i = 0; i < 32; i++)
            {
                if (integerValue.CheckBit(i)) enabledIndecesList.Add(i);
            }
            return enabledIndecesList;
        }
        public static int GetIntegerValue(List<int> enabledIndecesList)
        {
            int integerValue = 0;
            foreach (int index in enabledIndecesList)
            {
                integerValue.SetBit(index);
            }
            return integerValue;
        }
        public static void SetBit(ref this int integerValue, int bit)
        {
            integerValue = integerValue | (1 << bit);
        }
        public static void ClearBit(ref this int integerValue, int bit)
        {
            integerValue = integerValue & (~(1 << bit));
        }
        public static bool CheckBit(this int integerValue, int bit)
        {
            return ((integerValue & (1 << bit)) != 0);
        }
    }
}