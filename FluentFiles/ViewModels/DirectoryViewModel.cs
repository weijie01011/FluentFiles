using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using FluentFiles.Models;

namespace FluentFiles.ViewModels
{
    public interface IDirectoryViewModel : IViewModel
    {
        event Action<IStorageItem> OnStorageItemOpen;

        KnownDirectory RootKnownDirectory { get; }
        IStorageFolder Folder { get; }
        IEnumerable<IStorageItemViewModel> Items { get; }
        string Path { get; }
    }

    public class DirectoryViewModel : ViewModel, IDirectoryViewModel
    {
        public event Action<IStorageItem> OnStorageItemOpen;

        public KnownDirectory RootKnownDirectory { get; }
        public IStorageFolder Folder { get; }

        private IEnumerable<IStorageItemViewModel> items;
        public IEnumerable<IStorageItemViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged();
            }
        }

        public string Path { get; private set; }

        public DirectoryViewModel(KnownDirectory rootKnownDirectory, IStorageFolder folder)
        {
            RootKnownDirectory = rootKnownDirectory;
            Folder = folder;
            //Path = string.IsNullOrWhiteSpace(Folder.Path) ? Folder.Name : Folder.Path.Replace("\\", " \\ ");
            Path = RootKnownDirectory.DisplayName;
            GetFolderItems(Folder);   
        }

        public async void GetFolderItems(IStorageFolder folder)
        {
            var storageItems = await folder.GetItemsAsync();
            Items = storageItems.Select(storageItem =>
            {
                var storageItemViewModel = new StorageItemViewModel(storageItem);
                storageItemViewModel.OnOpen += () => OnStorageItemOpen?.Invoke(storageItem);
                return storageItemViewModel;
            });
        }
    }
}
