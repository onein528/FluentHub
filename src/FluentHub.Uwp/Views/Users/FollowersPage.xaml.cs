﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Users
{
    public sealed partial class FollowersPage : Page
    {
        public FollowersPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FollowersViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public FollowersViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // e.g. https://github.com/onein528?tab=followers
            string url = e.Parameter as string;
            var uri = new Uri(url);
            string login;

            if (url == "fluenthub://followers")
            {
                login = App.Settings.SignedInUserName;
                ViewModel.DisplayTitle = true;
            }
            else
            {
                login = uri.Segments[1];
            }

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Followers";
            currentItem.Description = $"{login}'s followers";
            currentItem.Url = url;
            currentItem.DisplayUrl = $"{login} / Followers";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };

            var command = ViewModel.RefreshFollowersCommand;
            if (command.CanExecute(login))
                command.Execute(login);
        }
    }
}
