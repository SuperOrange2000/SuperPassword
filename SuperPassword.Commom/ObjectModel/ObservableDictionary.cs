using System.Collections.Specialized;
using System.ComponentModel;

namespace SuperPassword.Commom.ObjectModel
{

    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged where TKey : notnull
    {
        public ObservableDictionary()
            : base()
        { }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public new TValue this[TKey key]
        {
            get => base[key];
            set { SetValue(key, value); }
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, FindPair(key)));
            OnPropertyChanged("Keys");
            OnPropertyChanged("Values");
            OnPropertyChanged("Count");
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Keys");
            OnPropertyChanged("Values");
            OnPropertyChanged("Count");
        }

        public new bool Remove(TKey key)
        {
            var pair = FindPair(key);
            if (base.Remove(key))
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, pair));
                OnPropertyChanged("Keys");
                OnPropertyChanged("Values");
                OnPropertyChanged("Count");
                return true;
            }
            return false;
        }

        public TValue GetValue(TKey key, TValue defaultValue)
        {
            return ContainsKey(key) ? base[key] : defaultValue;
        }

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region private方法
        private void SetValue(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                var pair = FindPair(key);
                base[key] = value;
                var newpair = FindPair(key);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newpair, pair));
                OnPropertyChanged("Values");
                OnPropertyChanged("Item[]");
            }
            else
            {
                Add(key, value);
            }
        }

        private KeyValuePair<TKey, TValue> FindPair(TKey key)
        {
            foreach (var item in this)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
            return default;
        }
        #endregion
    }
}
