using NLog;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaGUtil.System
{
    public sealed class SaLogMan
    {
        private SaLogMan()
        {
        }
        private static readonly Lazy<SaLogMan> lazy = new Lazy<SaLogMan>(() => new SaLogMan());
        public static SaLogMan Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void Info(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Info(message, args);
        }

        public void Warn(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Warn(message, args);
        }

        public void Error(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Error(message, args);
        }
        public void Debug(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Debug(message, args);
        }
        public void Fatal(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Fatal(message, args);
        }
        public void Trace(string logName, string message, params object[] args)
        {
            LogManager.GetLogger(logName).Trace(message, args);
        }
    }
}
