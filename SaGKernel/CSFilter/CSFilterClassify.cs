using PPMSXLib.Format;
using LCPMS15Lib.Format;
using SaGUtil.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SaGKernel.Specimen;
using SaGKernel.Config;
using System.IO;
using SaGKernel.Utils;

namespace SaGKernel.CSFilter
{
    public class CSFilterClassify
    {
        SlideEnvironment _Slenv;
        SpecimenCollection _Sc;

        private HashSet<ICassetteToSlideFilter> _CsFilters;
        public static CSFilterClassify GetInstance()
        {
            return SingletonProvider<CSFilterClassify>.Instance;
        }

        public CSFilterClassify()
        {
            //_Sc = SpeicmenConfig.GetSpecimenData();

            LoadAssembly();
            AutoLoadAssemblyByConfig();
        }
        public void SetSpecimenCollection(SpecimenCollection sc)
        {
            _Sc = sc;
        }

        private void LoadAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            LoadAssembly(asm);
        }

 
        public void LoadAssembly(Assembly assembly)
        {
            if (_CsFilters == null)
            {
                _CsFilters = new HashSet<ICassetteToSlideFilter>();
            }

            if ((assembly != null))
            {
                //Load assembly
                //動態載入所有 ICassetteToSlideFilter instance
                Assembly sgkCSFilter = assembly;

                Array.ForEach(sgkCSFilter.GetTypes(), t => {
                    if (t.GetInterfaces().Contains(typeof(ICassetteToSlideFilter)))
                    {
                        ICassetteToSlideFilter csf = (ICassetteToSlideFilter)Activator.CreateInstance(t);
                        if (_Sc != null)
                        {
                            PropertyInfo info = t.GetProperty("SPCollection");
                            if (info != null)
                            {
                                info.SetValue(csf, _Sc, null);
                            }
                        }

                        int existCnt = (from v in _CsFilters
                                        where v.CSFilterName == csf.CSFilterName
                                        select v).Count();

                        if (existCnt <= 0)
                        {
                            _CsFilters.Add(csf);
                        }
                    }
                });
            }
        }

        public void LoadAssembly(string assemblyFileName)
        {
            if (File.Exists(assemblyFileName))
            {
                FileInfo f = new FileInfo(assemblyFileName);
                try
                {
                    Assembly asm = Assembly.LoadFile(f.FullName);
                    LoadAssembly(asm);
                }
                catch (Exception ex)
                {
                    MyLog.Fatal(this, $"{assemblyFileName}: {ex.Message}");
                }
            }
        }

        private void AutoLoadAssemblyByConfig()
        {
            Array.ForEach(CustomModelConfig.CustomModelDll(), dll => LoadAssembly(dll));
        }

        public void SetSlideEnvironment(SlideEnvironment slenv)
        {
            _Slenv = slenv;
        }

        public SlideFormat[] MatchRule2GetSlides(CassetteFormat css)
        {
            var matches = _CsFilters.OrderByDescending(csf => csf.Priority);
            var v = matches.Where(csf => csf.MatchRule(css));
            
            if (v.Count() > 0)
            {
                return v.First().GenerateSlides(css, _Slenv);
            }
           
            return new SlideFormat[] { };   
        }
    }
}
