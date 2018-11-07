using System.Collections.Generic;
using System.Windows.Input;
using Windows.Storage;
using FluentFiles.Models;
using FluentFiles.Common;
using System;

namespace FluentFiles.ViewModels
{
    public interface IFileExplorerViewModel : IViewModel
    {
        bool CanNavigateBack { get; }
        IList<KnownDirectory> KnownDirectories { get; }
        KnownDirectory CurrentKnownDirectory { get; }
        IDirectoryViewModel CurrentDirectory { get; }

        ICommand NavigateBackCommand { get; }
        ICommand NavigateToFolderCommand { get; }
    }

    public class FileExplorerViewModel : ViewModel, IFileExplorerViewModel
    {
        public static readonly IList<KnownDirectory> PresetKnownDirectories = new List<KnownDirectory>()
        {
            KnownDirectory.Desktop,
            KnownDirectory.Downloads,
            KnownDirectory.Documents,
            KnownDirectory.Music,
            KnownDirectory.Pictures,
            KnownDirectory.Videos
        };

        private Stack<IDirectoryViewModel> PreviousDirectories { get; } = new Stack<IDirectoryViewModel>(); 

        public bool CanNavigateBack => PreviousDirectories.Count > 0;
        public IList<KnownDirectory> KnownDirectories => PresetKnownDirectories;

        private KnownDirectory currentKnownDirectory;
        public KnownDirectory CurrentKnownDirectory
        {
            get { return currentKnownDirectory; }
            set
            {
                currentKnownDirectory = value;
                RaisePropertyChanged();
            }
        }

        private IDirectoryViewModel currentDirectory;
        public IDirectoryViewModel CurrentDirectory
        {
            get { return currentDirectory; }
            private set
            {
                if (currentDirectory != null)
                {
                    currentDirectory.OnStorageItemOpen -= OpenStorageItem;
                }
                currentDirectory = value;
                currentDirectory.OnStorageItemOpen += OpenStorageItem;
                RaisePropertyChanged();
            }
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateToFolderCommand { get; }
        public ICommand NavigateToKnownDirectoryCommand { get; }

        public FileExplorerViewModel()
        {
            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateToFolderCommand = new RelayCommand<IStorageFolder>(NavigateToFolder);
            NavigateToKnownDirectoryCommand = new RelayCommand<KnownDirectory>(NavigateToKnownDirectory);

            NavigateToKnownDirectory(KnownDirectory.Desktop);
        }

        public void NavigateBack()
        {
            if (!CanNavigateBack)
                return;

            var previousDirectory = PreviousDirectories.Pop();
            CurrentDirectory = previousDirectory;
            CurrentKnownDirectory = CurrentDirectory.RootKnownDirectory;

            RaisePropertyChanged(nameof(CanNavigateBack));
            RaisePropertyChanged(nameof(CurrentKnownDirectory));
        }

        public void NavigateToFolder(IStorageFolder folder)
        {
            if (CurrentDirectory != null && folder == CurrentDirectory.Folder)
                return;

            if (CurrentDirectory != null)
            {
                PreviousDirectories.Push(CurrentDirectory);
            }

            CurrentDirectory = new DirectoryViewModel(CurrentKnownDirectory, folder);

            RaisePropertyChanged(nameof(CanNavigateBack));
        }

        public void NavigateToKnownDirectory(KnownDirectory knownDirectory)
        {
            CurrentKnownDirectory = knownDirectory;
            NavigateToFolder(knownDirectory.Folder);
        }

        private void OpenStorageItem(IStorageItem storageItem)
        {
            if (storageItem.IsOfType(StorageItemTypes.Folder))
            {
                NavigateToFolder(storageItem as IStorageFolder);
            }
            else if (storageItem.IsOfType(StorageItemTypes.File))
            {
                Windows.System.Launcher.LaunchFileAsync(storageItem as IStorageFile).AsTask().Wait();
            }
        }
    }
}
