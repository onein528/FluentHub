﻿using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Users
{
    public class StarredReposViewModel : ObservableObject
    {
        public StarredReposViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(LoadStarredRepositoriesAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private bool _displayTitle;
        public bool DisplayTitle { get => _displayTitle; set => SetProperty(ref _displayTitle, value); }

        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }
        #endregion

        private async Task LoadStarredRepositoriesAsync(string login, CancellationToken token)
        {
            try
            {
                StarredRepoQueries queries = new();
                var items = await queries.GetAllAsync(login);
                if (items == null) return;

                _repositories.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = true,
                        DisplayStarButton = true,
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadStarredRepositoriesAsync()", ex);
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