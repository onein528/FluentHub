﻿using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProfilePageViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        private string login;

        public ProfilePageViewModel ViewModel { get; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            login = e.Parameter as string;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{login}'s profile";
            currentItem.Description = $"{login}'s profile";
            currentItem.Url = $"https://github.com/{login}";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE77B"
            };

            var command = ViewModel.LoadUserCommand;
            if (command.CanExecute(login))
                command.Execute(login);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UserNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Overview" => UserNavViewContent.Navigate(typeof(OverviewPage), login, args.RecommendedNavigationTransitionInfo),
                "Repositories" => UserNavViewContent.Navigate(typeof(RepositoriesPage), login, args.RecommendedNavigationTransitionInfo),
                "Stars" => UserNavViewContent.Navigate(typeof(StarredReposPage), login, args.RecommendedNavigationTransitionInfo),
                "Followers" => UserNavViewContent.Navigate(typeof(FollowersPage), login, args.RecommendedNavigationTransitionInfo),
                "Following" => UserNavViewContent.Navigate(typeof(FollowingPage), login, args.RecommendedNavigationTransitionInfo),
                _ => UserNavViewContent.Navigate(typeof(OverviewPage), login, args.RecommendedNavigationTransitionInfo)
            };
        }

        private void UpdateVisibility()
        {
            if (!string.IsNullOrEmpty(CompanyLinkTextBlock.Text))
            {
                CompanyBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(UserBioTextBlock.Text))
            {
                UserBioTextBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LocationTextBlock.Text))
            {
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LinkButton.Content as string))
            {
                var uri = new UriBuilder(LinkButton.Content as string).Uri;
                LinkButton.NavigateUri = uri;
                LinkBlock.Visibility = Visibility.Visible;
            }
        }

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowers.IsSelected = true;
        }

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowing.IsSelected = true;
        }
    }
}