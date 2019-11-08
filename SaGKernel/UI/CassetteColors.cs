using SaGUtil.Lib;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SaGKernel.UI
{
    /// <summary>
    /// 包埋盒顏色對應的集合 (Fixed 卡匣名稱跟顏色)
    /// </summary>
    public class CassetteColors : List<CassetteColor>
    {
        public static CassetteColors GetInstance()
        {
            return SingletonProvider<CassetteColors>.Instance;
        }

        public CassetteColors()
        {
            //this.Add(new CassetteColor() { Name = "醫師", CassetteName = "Normal(White)", AssignColor = Color.White });
            //this.Add(new CassetteColor() { Name = "小件", CassetteName = "Normal(Pink)", AssignColor = Color.FromArgb(218, 155, 148) });
            //this.Add(new CassetteColor() { Name = "皮膚", CassetteName = "Normal(Blue)", AssignColor = Color.FromArgb(86, 197, 206) });
            //this.Add(new CassetteColor() { Name = "Liver", CassetteName = "Normal(Green)", AssignColor = Color.FromArgb(20, 201, 4) });
            //this.Add(new CassetteColor() { Name = "醫檢師/醫助", CassetteName = "Normal(Purple)", AssignColor = Color.FromArgb(147, 134, 234) });
            //this.Add(new CassetteColor() { Name = "特定醫院/EDTA脫鈣", CassetteName = "Normal(Sakura)", AssignColor = Color.FromArgb(185, 50, 212) });
            //this.Add(new CassetteColor() { Name = "脫鈣", CassetteName = "Normal(Yellow)", AssignColor = Color.Yellow });
            this.Add(new CassetteColor() {  Name = "Normal(White)", AssignColor = Color.White });
            this.Add(new CassetteColor() { Name = "Normal(Pink)", AssignColor = Color.FromArgb(218, 155, 148) });
            this.Add(new CassetteColor() { Name = "Normal(Blue)", AssignColor = Color.FromArgb(86, 197, 206) });
            this.Add(new CassetteColor() { Name = "Normal(Green)", AssignColor = Color.FromArgb(20, 201, 4) });
            this.Add(new CassetteColor() { Name = "Normal(Purple)", AssignColor = Color.FromArgb(147, 134, 234) });
            this.Add(new CassetteColor() { Name = "Normal(Sakura)", AssignColor = Color.FromArgb(185, 50, 212) });
            this.Add(new CassetteColor() { Name = "Normal(Yellow)", AssignColor = Color.Yellow });
        }

        public CassetteColors(CassetteColor[] colors)
        {
            this.AddRange(colors);
        }

        public Color GetColor(string name)
        {
            var v = this.Find((x) => (x.Name == name));
            if (v == null)
            {
                return Color.White;
            }
            else
            {
                return v.AssignColor;
            }
        }

        public string[] GetNames()
        {
            return this.Select(x => x.Name).ToArray();
        }

        //public string GetCassetteTxtMagazineName(string name)
        //{
        //    var v = this.Find((x) => (x.Name == name));
        //    if (v == null)
        //    {
        //        return GetCassetteTxtMagazineName(GetDefaultName());
        //    }
        //    else
        //    {
        //        return v.CassetteName;
        //    }
        //}

        public string GetDefaultName()
        {
            return this.First().Name;
        }
    }
}
