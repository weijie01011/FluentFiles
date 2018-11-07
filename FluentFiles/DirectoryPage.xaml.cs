using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FluentFiles.ViewModels;

namespace FluentFiles
{
    public sealed partial class DirectoryPage : Page
    {
        private IViewModel viewModel;
        public IViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                DataContext = viewModel = value;
            }
        }

        public DirectoryPage()
        {
            this.InitializeComponent();
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e == null)
        //        return;

        //    if (e.Parameter is IStorageFolder)
        //    {
        //        ViewModel = new DirectoryViewModel(e.Parameter as IStorageFolder);
        //    }

        //    base.OnNavigatedTo(e);
        //}
    }
}
