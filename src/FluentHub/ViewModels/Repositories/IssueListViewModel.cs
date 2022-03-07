﻿using FluentHub.Models.Items;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories
{
    public class IssueListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IssueListItem> Items { get; private set; }

        public IssueListViewModel()
        {
            Items = new();
        }

        public async void GetRepoIssues(long repoId)
        {
            IsActive = true;

            RepositoryIssueRequest request = new RepositoryIssueRequest();
            request.State = ItemStateFilter.All;

            ApiOptions options = new() { PageSize = 30, PageCount = 1, StartPage = 1 };

            var issues = await App.Client.Issue.GetAllForRepository(repoId, request, options);

            foreach (var item in issues)
            {
                if (item.PullRequest == null)
                {
                    IssueListItem listItem = new IssueListItem();
                    listItem.RepoId = repoId;
                    listItem.IssueIndex = item.Number;
                    Items.Add(listItem);
                }
            }

            Items = new ObservableCollection<IssueListItem>(Items.OrderByDescending(x => x.IssueIndex));

            IsActive = false;
        }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
