using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FluentFiles.ViewModels;
using Windows.Storage;

namespace FluentFiles
{
    public sealed partial class MainPage : Page
    {
        private IStorageFolder CurrentFolder { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void navigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //contentFrame.Navigate(typeof(SampleSettingsPage));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                switch ((string)selectedItem.Tag)
                {
                    case "Desktop":
                        var desktopFolder = StorageFolder.GetFolderFromPathAsync(@"C:\Users\remi_\Desktop").AsTask().Result;
                        NavigateToFolder(desktopFolder);
                        break;
                    case "Documents":
                        NavigateToFolder(KnownFolders.DocumentsLibrary);
                        break;
                    case "Downloads":
                        var downloadsFolder = StorageFolder.GetFolderFromPathAsync(@"C:\Users\remi_\Downloads").AsTask().Result;
                        NavigateToFolder(downloadsFolder);
                        break;
                    case "Music":
                        NavigateToFolder(KnownFolders.MusicLibrary);
                        break;
                    case "Pictures":
                        NavigateToFolder(KnownFolders.PicturesLibrary);
                        break;
                    case "Videos":
                        NavigateToFolder(KnownFolders.VideosLibrary);
                        break;
                    case "C:":
                        var cFolder = StorageFolder.GetFolderFromPathAsync(@"C:").AsTask().Result;
                        NavigateToFolder(cFolder);
                        break;
                }
            }
        }

        //private void DoubleTapped_EventHandler(object sender, RoutedEventArgs e)
        //{
        //    var element = sender as FrameworkElement;
        //    if (element == null)
        //        return;

        //    var storageItem = (element.DataContext as StorageItemViewModel)?.StorageItem;
        //    if (storageItem == null)
        //        return;

        //    OpenStorageItem(storageItem);
        //}

        private void navigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (contentFrame.CanGoBack)
            {
                contentFrame.GoBack();
            }
        }

        private void NavigateToFolder(IStorageFolder folder)
        {
            contentFrame.Navigate(typeof(DirectoryPage), folder);
        }

        private void OpenStorageItem(IStorageItem storageItem)
        {
            if (storageItem.IsOfType(StorageItemTypes.Folder))
            {
                NavigateToFolder(storageItem as IStorageFolder);
            }
        }
    }
}
