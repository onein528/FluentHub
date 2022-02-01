﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;


namespace FluentHub.Views.RepoPages
{
    public sealed partial class OverviewPage : Page
    {
        private long RepoId { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void RepoPageNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Code":
                    RepoPageNavViewFrame.Navigate(typeof(CodePage), RepoId.ToString());
                    break;
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssueListPage), RepoId.ToString());
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestListPage), RepoId.ToString());
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings), RepoId.ToString());
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var repo = await App.Client.Repository.Get(RepoId);

            RepoOwnerName.Text = repo.Owner.Login;

            RepoName.Text = repo.Name;

            RepoOwnerAvatar.Source = new BitmapImage(new Uri(repo.Owner.AvatarUrl));

            WatchersCountBadge.Value = repo.SubscribersCount;

            ForksCountBadge.Value = repo.ForksCount;

            StargazersCountBadge.Value = repo.StargazersCount;

            if (repo.OpenIssuesCount != 0)
            {
                IssuesCountBadge.Value = repo.OpenIssuesCount;
                IssuesCountBadge.Visibility = Visibility.Visible;
            }

            var pulls = await App.Client.PullRequest.GetAllForRepository(RepoId);

            if (pulls.Count() != 0)
            {
                PullsCountBadge.Value = pulls.Count();
                PullsCountBadge.Visibility = Visibility.Visible;
            }
        }
    }
}
