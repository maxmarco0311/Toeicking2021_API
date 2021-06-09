using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Toeicking2021.Models
{
    // 泛型類別：定義類別時傳入一個型別未定的參數<T>，該型別參數可用在類別的屬性(型別)或方法的參數(型別)或回傳值(型別)
    // 基本概念是可讓類別裡供運算的資料(如屬性、方法參數或回傳值)型別是動態的，在實體化(編譯)時才決定要用何種型別
    // 不用因為資料型別不同的關係而寫多個邏輯相同的類別

    // 此類別是裝"整包"回傳的JSON資料，主要目的是加一些自訂屬性，可讓回傳的內容多一點提示訊息
    public class ServiceResponse<T>
    {
        // Data整包JSON資料的一個屬性，其屬性值是主要回應的資料內容
        // 這裡的T是指這個屬性值所對應的C#型別，看要回傳的是物件、物件集合或純值都可以(通常是用Dto物件型別)
        // 這裡名稱是大寫，變成JSON時會自動改小寫
        public T Data { get; set; }
        // Success整包JSON資料的一個屬性，其屬性值預設是true(表正常回應)，但如果是異常狀況時要給false去回應
        public bool Success { get; set; } = true;
        // Message整包JSON資料的一個屬性，其屬性值預設是null(表正常回應)，但如果是異常狀況時要給客製的錯誤訊息字串去回應
        public string Message { get; set; } = null;
    }
}
