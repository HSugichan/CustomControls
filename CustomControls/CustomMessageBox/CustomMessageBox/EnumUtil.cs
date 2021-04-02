using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControls
{
    /// <summary>
    /// Utility class for Enum.
    /// </summary>
    /// <typeparam name="T">Enum</typeparam>
    public class EnumUtil<T> where T :struct, Enum//値型に制約.これがないと参照型（クラス）にもつかえる,Null許容対策でstruct
    {
        /// <summary>
        /// Show a statf whether the "n" is defined in "T."
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsDefined(int n) => Enum.IsDefined(typeof(T), n);
        /// <summary>
        /// Array all elements in "T."
        /// </summary>
        /// <returns></returns>
        public static T[] Array()
        {
            var list = new List<T>();
            foreach (var eObj in Enumerate())
                list.Add(eObj);
            return list.Distinct().ToArray();
        }
        /// <summary>
        /// 文字列の変換
        /// 数値文字列（"1"）でも可
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static bool TryParse(string name, out T enumObj) 
            => Enum.TryParse(name, out enumObj) && Enum.IsDefined(typeof(T), enumObj);
        /// <summary>
        /// Converts the string representation of a enum to equivalent or default value.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns></returns>
        public static T Parse(string name)
        {
            TryParse(name, out var enumValue);
            return enumValue;
        }


        /// <summary>
        /// 整数値の変換
        /// </summary>
        /// <param name="num"></param>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static bool TryParse(int num, out T enumObj) => TryParse(num.ToString(), out enumObj);
        /// <summary>
        /// Converts the number of a enum to equivalent or default value.
        /// </summary>
        /// <param name="num">integer</param>
        /// <returns></returns>
        public static T Parse(int num) => Parse(num.ToString());
        /// <summary>
        /// Get name array.
        /// </summary>
        /// <returns></returns>
        public static string[] GetNames() => Enum.GetNames(typeof(T));

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private EnumUtil() { }

        /// <summary>
        /// Foreach用のGetEnumeratorを持つヘルパクラス
        /// </summary>
        public class EnumerateEnum
        {
            /// <summary>
            /// Foreach用
            /// </summary>
            /// <returns></returns>
            public IEnumerator<T> GetEnumerator()
            {
                foreach (T e in Enum.GetValues(typeof(T)))
                    yield return e;
            }
        }

        /// <summary>
        /// enum定義をforeachに渡すためのヘルパクラスを返す
        /// </summary>
        /// <returns></returns>
        public static EnumerateEnum Enumerate() => new EnumerateEnum();
    }
}