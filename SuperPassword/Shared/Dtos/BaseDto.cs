using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SuperPassword.Shared.DTOs
{
    public class BaseDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现通知更新
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Encrypt(string data)
        {
            if (data == null)
                return data;

            var encodedString = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(encodedString);
        }

        public string Decrypt(string data)
        {
            if (data == null)
                return data;

            var decodedBytes = Convert.FromBase64String(data);
            var decodedString = Encoding.UTF8.GetString(decodedBytes);
            return decodedString;
        }
    }
}
