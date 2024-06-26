﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // 查詢條件表單資料物件
    public class TableQueryFormData
    {
        // 句子編號
        public string SenNum { get; set; }
        // 句子關鍵字
        public string Keyword { get; set; }
        // 是否有字首字根
        public bool? HasWordOrigin { get; set; }
        // 是否有同義字
        public bool? HasSynonym { get; set; }
        // 是否有音檔
        public bool? HasAudio { get; set; }
        // 控制布林值是否加入條件式或加入後是聯集還是交集查詢
        public string BoolConditions { get; set; }
        // 出現情境
        public string Context { get; set; }
        // 文法分類
        public string GrammarCategories { get; set; }
        // 出現大題
        public string Part { get; set; }
        // 目前頁碼
        public int? Page { get; set; }
        // 幾天前存入
        public int? AddedDate { get; set; }
        // 最近幾筆
        public int? CountDesc { get; set; }
        // 檢查次數
        public int? CheckedTimes { get; set; }
        // 每頁幾筆
        public int PageSize { get; set; }
        // 判斷如果啟用檢查次數條件式後又checktime+1的筆數，分頁按鈕送出查詢時要在skip()裡加回來的筆數
        public int? SkipOffset { get; set; }

    }
}
