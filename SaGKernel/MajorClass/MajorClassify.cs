using SaGKernel.Config;
using SaGKernel.Utils;
using SaGUtil.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SaGKernel.MajorClass
{
    /// <summary>
    /// 依據檢體編號特定規則，對檢體作分類
    /// 例如: N19-82345 , 82345第一碼8表示 皮膚(Skin)
    /// </summary>
    public class MajorClassify
    {
        public static MajorClassify GetInstance()
        {
            return SingletonProvider<MajorClassify>.Instance;
        }

        public MajorClassify()
        {
            _majorClassify = new HashSet<IMajorClass>();

            LoadAssembly();
            AutoLoadAssemblyByConfig();
        }

        private HashSet<IMajorClass> _majorClassify;
        public IMajorClass Classify(QRDataStruct css)
        {
            //判斷是哪一個檢體分類
            var v = _majorClassify.Where(el => el.IsMe(css));
            if (v.Count() > 0)
            {
                return v.First();
            }

            return _majorClassify.Where(el => el.MajorClass == MajorClassEnum.DefaultMC).First();
        }

        private  void LoadAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            LoadAssembly(asm);
        }

        public void LoadAssembly(Assembly assembly)
        {
            if (_majorClassify == null)
            {
                _majorClassify = new HashSet<IMajorClass>();
            }

            if ((assembly != null) )
            {
                //Load assembly
                //動態載入所有 IMajorClass instance
                Assembly sgkModel = assembly;

                Array.ForEach(sgkModel.GetTypes(), t => {
                    if (t.GetInterfaces().Contains(typeof(IMajorClass)))
                    {
                        IMajorClass mc = (IMajorClass)Activator.CreateInstance(t);

                        int existCnt = _majorClassify.Where(el => el.ClassName == mc.ClassName).Count();

                        if (existCnt <= 0)
                        {
                            _majorClassify.Add(mc);
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

        //所有 major class 
        public  HashSet<IMajorClass> AllMajorClass()
        {
            LoadAssembly();

            return _majorClassify;
        }

        //取得任一MajorClass
        public  IMajorClass GetMajorClassByName(string className)
        {
            var v = _majorClassify.Where(el => el.ClassName == className);

            return v.Count() > 0 ? v.First() : null;
        }

        //取得Default MajorClass
        public  IMajorClass GetDefaultMajorClass()
        {
            return _majorClassify.Where(el => el.MajorClass == MajorClassEnum.DefaultMC).First();
        }

        private void AutoLoadAssemblyByConfig()
        {
            Array.ForEach(CustomModelConfig.CustomModelDll(), el => LoadAssembly(el));
        }
    }
}
