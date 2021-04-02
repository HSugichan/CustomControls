using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CustomControls
{
    /// <summary>
    /// Notify property changed.
    /// https://qiita.com/nossey/items/7c415799bc6fda45f94e
    /// </summary>
    public class PropertyChangedEventListener : IDisposable
    {
        INotifyPropertyChanged Source;
        readonly PropertyChangedEventHandler Handler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">object with property to notify changed.</param>
        /// <param name="handler">property changed event</param>
        public PropertyChangedEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            Source = source;
            Handler = handler;
            Source.PropertyChanged += Handler;
        }

        /// <summary>
        /// Reset event handler.
        /// </summary>
        public void Dispose()
        {
            if (Source != null && Handler != null)
                Source.PropertyChanged -= Handler;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>        
        /// INotifyPropertyChangedは必ずこのイベントをPublicでもっていなければならない
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Implement a setter to notify property changed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">property</param>
        /// <param name="value">set value</param>
        /// <param name="caller">property name (omittable)</param>
        public void SetProperty<T>(ref T target, T value, [CallerMemberName] string caller = "")
        {
            if (target?.Equals(value)??false)
                return;

            target = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }

}
