﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Utilities
{
    public class ChangeWordListOrder
    {
        // 1. 點按MyWordList中某個字彙後，該字彙就會移到MyWordList的第一個
        // 2. 加入MyWordList的字彙，該字彙就會移到MyWordList的第一個
        public static string MoveToTop(string originalWordList, string item)
        {
            List<string> temp = originalWordList.Split(',').ToList();
            if (temp.Count > 0 && temp.Contains(item))
            {
                temp.Remove(item);
                temp.Insert(0, item);
            }
            string newWordList = string.Join(',', temp);
            return newWordList;
        }

    }

}
