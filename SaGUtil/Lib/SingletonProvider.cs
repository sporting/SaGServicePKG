﻿using System;

namespace SaGUtil.Lib
{
    ///網路上抄來的，還不錯用

    /// <summary>
    /// 用于从另一个类创建或获取单例的静态助手。
    /// </summary>
    /// <typeparam name="T">要创建或获取单例的类型。</typeparam>
    public static class SingletonProvider<T> where T : class, new()
    {
        #region Fields

        /// <summary>
        /// 获取给定类型的单例。
        /// </summary>
        private static readonly Lazy<T> _lazy = new Lazy<T>(() => new T());

        #endregion

        #region Properties

        /// <summary>
        /// 获取给定类型的单例。
        /// </summary>
        public static T Instance => _lazy.Value;

        #endregion
    }
}
