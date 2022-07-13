﻿using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.ButtonBlocks;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class CommitsViewModel : ObservableObject
    {
        public CommitsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(RefreshPullRequestPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private readonly ObservableCollection<CommitButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<CommitButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task RefreshPullRequestPageAsync(PullRequest pull)
        {
            try
            {
                if (pull != null)
                    PullItem = pull;

                CommitQueries queries = new();
                var items = await queries.GetAllAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                _items.Clear();
                foreach (var item in items)
                {
                    CommitButtonBlockViewModel viewModel = new()
                    {
                        CommitItem = item,
                    };

                    _items.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof("RefreshPullRequestPageAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
