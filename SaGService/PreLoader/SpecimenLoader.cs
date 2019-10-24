using SaGKernel.Config;
using SaGKernel.Specimen;
using SaGLogic;
using SaGModel;
using SaGUtil.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaGService.PreLoader
{
    public class SpecimenLoader
    {
        public SpecimenCollection Specimens { get; set; }

        public static SpecimenLoader GetInstance()
        {
            return SingletonProvider<SpecimenLoader>.Instance;
        }

        //只會撈一次放在記憶體中
        public SpecimenLoader()
        {
            SysSpecimenStain ssStain = new SysSpecimenStain();
            SysSpecimenStainM[] ssms= ssStain.GetValidAll();
            Specimens = new SpecimenCollection();
            Array.ForEach(ssms, ssm => {
                Specimens.Add(ssm.Specimen, ssm.Seq.ToString(), ssm.Stain);
            });
            //Specimens =  SpeicmenConfig.GetSpecimenData();
        }


    }
}