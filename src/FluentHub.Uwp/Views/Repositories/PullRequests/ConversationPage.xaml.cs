﻿using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Repositories.PullRequests
{
    public sealed partial class ConversationPage : Page
    {
        public ConversationPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ConversationViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public ConversationViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Octokit.Models.PullRequest param = e.Parameter as Octokit.Models.PullRequest;

            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{param.Title} · #{param.Number}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = $"https://github.com/{param.OwnerLogin}/{param.Name}/pull/{param.Number}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };

            var command = ViewModel.RefreshPullRequestPageCommand;
            if (command.CanExecute(param))
                command.Execute(param);
        }
    }
}
