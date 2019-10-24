using SaGUtil.System;
using System.Runtime.CompilerServices;

namespace SaGBridge.Utils
{
    public sealed class MyLog
    {
        static string LogName = "SaGBridge";
        //Info 使用時機: 程式掌控中的提示訊息
        public static void Info(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Info(LogName, InformationConcate(sender, message, memberName, sourceFilePath, sourceLineNumber));
        }

        //Warn 使用時機: 程式掌控中的警告訊息
        public static void Warn(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Warn(LogName, InformationConcate(sender, message, memberName, sourceFilePath, sourceLineNumber));
        }

        //Error 使用時機: 程式掌控中的錯誤訊息
        public static void Error(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Error(LogName, InformationConcate(sender, message, memberName, sourceFilePath, sourceLineNumber));
        }
        //Debug 使用時機: 程式 Debug 使用
        public static void Debug(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Debug(LogName, InformationConcate(sender, message, memberName, sourceFilePath, sourceLineNumber));
        }

        //Fatal 使用時機: 程式未掌控的錯誤訊息 (Exception)
        public static void Fatal(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Fatal(LogName, InformationConcate(sender, message, memberName, sourceFilePath, sourceLineNumber));
        }

        //Trace 使用時機: 程式 Trace 使用
        public static void Trace(object sender, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            SaLogMan.Instance.Trace(LogName, InformationConcate(sender,message,memberName,sourceFilePath,sourceLineNumber));
        }

        private static string InformationConcate(object sender, string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            return $"{sender.GetType().Name}.{memberName}.{sourceLineNumber}: {message}";
        }
    }
}
