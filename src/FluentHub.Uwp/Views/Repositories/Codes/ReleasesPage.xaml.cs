﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.Repositories.Codes;
using FluentHub.Uwp.ViewModels.Repositories.Codes.Layouts;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.Codes
{
    public sealed partial class ReleasesPage : Page
    {
        public ReleasesPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ReleasesViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        public ReleasesViewModel ViewModel { get; }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.ContextViewModel = ViewModel.ContextViewModel = e.Parameter as RepoContextViewModel;
            DataContext = ViewModel;

            #region tabitem
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Releases · {ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = $"https://github.com/{ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}/releases";

            currentItem.DisplayUrl = $"{ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}/releases";

            currentItem.DisplayUrl = $"{ViewModel.ContextViewModel.Repository.Owner.Login} / {ViewModel.ContextViewModel.Repository.Name} / Releases";

            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
            #endregion

            var command = ViewModel.LoadReleasesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
