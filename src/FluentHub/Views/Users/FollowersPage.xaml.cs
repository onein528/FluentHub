﻿using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class FollowersPage : Page
    {
        public FollowersPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string login = e.Parameter as string;

            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Followers", $"{login}'s followers", $"https://github.com/{login}?tab=followers", "\uE737");
            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = $"Followers".GetLocalized();
            currentTab.Description = $"{login}'s followers";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uE737"
            };
            await ViewModel.GetFollowersList(login);

            base.OnNavigatedTo(e);
        }
    }
}