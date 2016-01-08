using DolphinApp.ViewModel;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DolphinApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }

        #region ChangeOrientation
        //Forcing Phone 4"
        int x = 320; int y = 569;

        //Forcing Phone 5"
        //int x = 360; int y = 640;
        private void ChangeOrientation(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.Orientation.Equals(ApplicationViewOrientation.Landscape))
                view.TryResizeView(new Size { Width = x, Height = y });
            else
                view.TryResizeView(new Size { Width = y, Height = x });
        }
        #endregion ChangeOrientation

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ((MenuViewModel)DataContext).OnNavigateTo(e);
        }
    }
}
