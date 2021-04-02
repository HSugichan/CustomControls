using CustomControls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CustomControls
{
    /// <summary>
    /// Abstruct class to change language rerion.
    /// Override "Culture" property on each assembly.
    /// </summary>
    public class ResourcesService : INotifyPropertyChanged
    {
        #region シングルトン対策
        /// <summary>
        /// ResourceServiceインスタンスプロパティ
        /// </summary>
        public static ResourcesService Current { get; } = new ResourcesService();
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ResourcesService() { }
        #endregion

        #region INotifyPropertyChanged対策
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notify a property change. 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// Resourceプロパティ
        /// </summary>
        internal Resources _Resources { get; } = new Resources();
        /// <summary>
        /// Culture information of UI inheriting this class
        /// </summary>
        public virtual CultureInfo Culture
        {
            get => Resources.Culture;
            protected set => Resources.Culture = value;
        }

        /// <summary>
        /// リソースのカルチャーを変更
        /// </summary>
        /// <param name="culture"></param>
        public void ChangeCulture(CultureInfo culture)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Culture = culture;
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#endif
                //無視
            }
            RaisePropertyChanged();
        }
        /// <summary>
        /// リソースのカルチャーを変更
        /// </summary>
        /// <param name="name">カルチャー名</param>
        public void ChangeCulture(string name = "")
            => ChangeCulture(CultureInfo.GetCultureInfo(name));
        /// <summary>
        /// リソースのカルチャーを変更
        /// </summary>
        /// <param name="region"></param>
        public void ChangeCulture(LanguageRegion region) => ChangeCulture(region.GetRegionName());
        /// <summary>
        /// Available language list
        /// </summary>
        public static  LanguageRegion[] LanguageRegions => EnumUtil<LanguageRegion>.Array();
    }

    /// <summary>
    /// 言語の列挙型
    /// </summary>
    public enum LanguageRegion : int
    {
        /// <summary>
        /// 英語、デフォルト値
        /// </summary>
        English = 0,
        /// <summary>
        /// 日本語
        /// </summary>
        Japanese = 1,
    }
    /// <summary>
    /// LanguageRegion  に対する拡張メソッドの定義
    /// </summary>
    public static class LangRegionrExt
    {
        /// <summary>
        /// RegionCultureの取得
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string GetRegionName(this LanguageRegion region)
        {
            switch (region)
            {
                case LanguageRegion.English:
                    return "";
                case LanguageRegion.Japanese:
                    return "ja-JP";

                default:
                    return "";
            }
        }
    }
}