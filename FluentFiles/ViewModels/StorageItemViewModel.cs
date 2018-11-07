using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace FluentFiles.ViewModels
{
    public interface IStorageItemViewModel : IViewModel
    {
        event Action OnOpen;
        string Name { get; }
        void Open();
    }

    public class StorageItemViewModel : ViewModel, IStorageItemViewModel
    {
        private static class Icons
        {
            public const string Folder = "\uE8B7";
            public const string File = "\uE8A5";
            public const string Document = "\uF000";
            public const string Image = "\uEB9F";
            public const string Music = "\uE93C"; //EC4F
            public const string Video = "\uE8B2"; //E714
        }

        public event Action OnOpen;

        public IStorageItem StorageItem { get; private set; }

        public string Name => StorageItem.Name;

        public string Icon => StorageItem.IsOfType(StorageItemTypes.Folder) ? Icons.Folder : Icons.Document;

        public StorageItemViewModel(IStorageItem storageItem)
        {
            StorageItem = storageItem;
        }

        public void Open()
        {
            OnOpen?.Invoke();
        }
    }
}
