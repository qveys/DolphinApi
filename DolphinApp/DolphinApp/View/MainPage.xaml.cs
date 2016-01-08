using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DolphinApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        #region AffichageTailleEcran

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Window.Current.SizeChanged += OnWindowSizeChanged;
            UpdateContent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateContent();
        }

        void UpdateContent()
        {
            Rect bounds = Window.Current.Bounds;
            ReportViewWidth.Text = bounds.Width.ToString();
            ReportViewHeight.Text = bounds.Height.ToString();
        }

        #endregion

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
    }
}
