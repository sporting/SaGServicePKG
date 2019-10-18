using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SaGSampleModel
{
    public class SpecimenCollection : IEnumerable<SpecimenItem>
    {
        private List<SpecimenItem> _List = new List<SpecimenItem>();

        public SpecimenCollection() { }

        public SpecimenCollection(List<SpecimenItem> list) : this()
        {
            _List = list;
        }

        public SpecimenItem this[int index]
        {
            get { return _List[index]; }
            set { _List.Insert(index, value); }
        }

        public IEnumerator<SpecimenItem> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(string kind, string seq, string staining)
        {
            _List.Add(new SpecimenItem(kind, seq, staining));
        }

        public void Clear()
        {
            _List.Clear();
        }

      

        public string[] DistinctSpecimens()
        {
            return (from SpecimenItem item in _List
                    select item.SpecimenKind).Distinct().ToArray();
        }

        public string[] GetStainingMethods(string kind)
        {
            return (from SpecimenItem item in _List
                    where item.SpecimenKind==kind
                    orderby item.Sequence
                    select item.StainingMethod).ToArray();
        }

        //csv 檔案格式如下
        // 檢體別, 序號, 染色劑(染色方法)
        public static SpecimenCollection ParseCSVSpecimenData(string csvFileFullName)
        {
            if (File.Exists(csvFileFullName))
            {
                string[] lines = File.ReadAllLines(csvFileFullName);
                List<SpecimenItem> data = new List<SpecimenItem>();

                for (int i = 1; i < lines.Count(); i++)
                {
                    string line = lines[i];
                    if (line.Trim() == string.Empty)
                    {
                        break;
                    }

                    string[] d = line.Split(',');
                    data.Add(new SpecimenItem()
                    {
                        SpecimenKind = d[0],
                        Sequence = d[1],
                        StainingMethod = d[2]
                    });
                }

                return new SpecimenCollection(data);
            }
            return new SpecimenCollection();
        }
    }

    public class SpecimenItem
    {
        //檢體別
        public string SpecimenKind { get; set; }
        //流水號
        public string Sequence { get; set; }
        //染色法
        public string StainingMethod { get; set; }

        public SpecimenItem()
        {

        }

        public SpecimenItem(string kind, string seq, string staining) : this()
        {
            SpecimenKind = kind;
            Sequence = seq;
            StainingMethod = staining;
        }
    }
}
