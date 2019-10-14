﻿using SaGKernel.Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGKernel.UI
{
    public class CassetteColors:List<CassetteColor>
    {
        public static CassetteColors GetInstance()
        {
            return SingletonProvider<CassetteColors>.Instance;
        }

        public CassetteColors() {
            this.Add(new CassetteColor() { Name = "Normal(White)", AssignColor = Color.White });
            this.Add(new CassetteColor() { Name = "Normal(Pink)", AssignColor = Color.FromArgb(218, 155, 148) });
            this.Add(new CassetteColor() { Name = "Normal(Blue)", AssignColor = Color.FromArgb(86, 197, 206) });
            this.Add(new CassetteColor() { Name = "Normal(Green)", AssignColor = Color.FromArgb(20, 201, 4) });
            this.Add(new CassetteColor() { Name = "Normal(Purple)", AssignColor = Color.FromArgb(147, 134, 234) });
            this.Add(new CassetteColor() { Name = "Normal(Sakura)", AssignColor = Color.FromArgb(185, 50, 212) });
            this.Add(new CassetteColor() { Name = "Normal(Yellow)", AssignColor = Color.Yellow });
        }

        public Color GetColor(string name)
        {
            var v= this.Find((x) => (x.Name == name));
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

        public string GetDefaultName() {
            return this.First().Name;
        }
    }
}
