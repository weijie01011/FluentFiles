using Windows.UI.Xaml.Controls;
using FluentFiles.Models;
using FluentFiles.ViewModels;

namespace FluentFiles
{
    public sealed partial class FileExplorer : Page
    {
        private IViewModel viewModel;
        public IViewModel ViewModel
        {
            get { return viewModel; }
            set { DataContext = viewModel = value; }
        }

        public FileExplorer()
        {
            this.InitializeComponent();
            ViewModel = new FileExplorerViewModel();
        }

        private void navigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            (ViewModel as FileExplorerViewModel).NavigateBackCommand.Execute(null);
        }

        private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var knownDirectory = (KnownDirectory)args.InvokedItem;
            (ViewModel as FileExplorerViewModel).NavigateToKnownDirectoryCommand.Execute(knownDirectory);
        }
    }
}
