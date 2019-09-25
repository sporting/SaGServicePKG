using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGModel
{
    // Slide 列印需要的資訊 for server side 
    public class PrintSlideM
    {
        //例如 default 對應到 LayoutSample template
        public string Template { get; set; } //Slide 樣版名稱Key (樣版的長相，讀取資料庫設定檔 Template Mapping)
        //例如 default 對應到 Normal(White) 卡匣
        public string Printer { get; set; } //印表機名稱Key (依據檢體類別，決定從哪一個Printer印出)
        //例如 default 對應到 C:\Cassette\WS1
        public string WorkStation { get; set; } //工作站的位置，決定 slide csv file 輸出的目錄 (決定 slide 列印機器監測目錄)
        //列印的 Data
        public OrderSlideM Data { get; set; }
    }
}
