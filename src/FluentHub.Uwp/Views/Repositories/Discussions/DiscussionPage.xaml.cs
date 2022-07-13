﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Discussions
{
    public sealed partial class DiscussionPage : Page
    {
        public DiscussionPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<DiscussionViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public DiscussionViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var pathSegments = url.Split("/");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussion";
            currentItem.Description = "Discussion";
            currentItem.Url = $"{url}";
            currentItem.DisplayUrl = $"{pathSegments[3]} / {pathSegments[4]} / {pathSegments[6]}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };

            var command = ViewModel.LoadDiscussionPageCommand;
            if (command.CanExecute(url))
                command.Execute(url);
        }
    }
}
