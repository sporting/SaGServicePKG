using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    // Cassette 列印需要的資訊 for server side 
    // 儲存在資料庫端才須使用
    public class PrintCassetteM
    {
        //例如 default 對應到 Normal template
        public string Template { get; set; } //Cassette 樣版名稱Key (樣版的長相，讀取資料庫設定檔 Template Mapping)
        //例如 default 對應到 Normal(White) 卡匣
        public string Magazine { get; set; } //卡匣名稱Key (依據檢體類別，決定從哪一個卡匣印出)
        //例如 default 對應到 C:\Cassette\WS1
        public string WorkStation { get; set; } //工作站的位置，決定 cassette text file 輸出的目錄 (決定cassette 列印機器監測目錄)
        //列印的 Data
        public OrderCassetteM Data { get; set; }
    }
}
