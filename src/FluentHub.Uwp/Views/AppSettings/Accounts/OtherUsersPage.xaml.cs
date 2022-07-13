﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.AppSettings.Accounts;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.AppSettings.Accounts
{
    public sealed partial class OtherUsersPage : Page
    {
        public OtherUsersPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<OtherUsersViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public OtherUsersViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = url.Split("/").ToList();
            pathSegments.RemoveRange(0, 3);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Other users";
            currentItem.Description = "Other users";
            currentItem.Url = "fluenthub://settings/account/otherusers";
            currentItem.DisplayUrl = "Settings / Account / Other users";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Accounts.png"))
            };

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag as string;

            // Remove from list
            ViewModel.RemoveAccount(tag);
        }

        private async void OnAddAccountClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.AccountSwitching();
            await dialog.ShowAsync();
        }
    }
}
