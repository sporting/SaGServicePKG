﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.Json
{
    public class SaJson
    {
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
