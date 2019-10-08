using SaGKernel.Lib;
using SaGKernel.MajorClass;
using SaGUtil.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            foreach (IMajorClass major in _majorClassify)
            {
                if (major.IsMe(css))
                {
                    return major;
                }
            }

            return (from IMajorClass major in _majorClassify
                    where major.MajorClass == MajorClassEnum.DefaultMC
                    select major).First();
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

                foreach (Type type in sgkModel.GetTypes())
                {
                    if (type.GetInterfaces().Contains(typeof(IMajorClass)))
                    {
                        IMajorClass mc = (IMajorClass)Activator.CreateInstance(type);

                        int existCnt = (from v in _majorClassify
                                        where v.ClassName == mc.ClassName
                                        select v).Count();

                        if (existCnt<=0)
                        {
                            _majorClassify.Add(mc);
                        }
                    }
                }
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
                    LogMan.Instance.Error("MajorClassify.LoadAssembly", ex.Message, assemblyFileName);
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
            var v = (from IMajorClass mc in _majorClassify
                     where mc.ClassName == className
                     select mc);
            if (v.Count() > 0)
            {
                return v.First();
            }

            return null;
        }

        //取得Default MajorClass
        public  IMajorClass GetDefaultMajorClass()
        {
            return (from IMajorClass major in _majorClassify
                    where major.MajorClass == MajorClassEnum.DefaultMC
                    select major).First();
        }

        private void AutoLoadAssemblyByConfig()
        {
            foreach (string fileName in Config.MajorModelConfig.MajorModelDll())
            {
                LoadAssembly(fileName);
            }
        }
    }
}
