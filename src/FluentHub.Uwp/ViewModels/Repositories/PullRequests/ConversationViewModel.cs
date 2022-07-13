﻿using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class ConversationViewModel : ObservableObject
    {
        public ConversationViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _eventBlocks = new();
            EventBlocks = new(_eventBlocks);

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(RefreshPullRequestPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private TimelineViewModel _eventBlockViewModel;
        public TimelineViewModel EventBlockViewModel { get => _eventBlockViewModel; private set => SetProperty(ref _eventBlockViewModel, value); }

        private readonly ObservableCollection<Timeline> _eventBlocks;
        public ReadOnlyObservableCollection<Timeline> EventBlocks { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task RefreshPullRequestPageAsync(PullRequest pull)
        {
            try
            {
                if (pull != null)
                    PullItem = pull;

                _eventBlocks.Clear();

                PullRequestQueries pullRequestQueries = new();

                var bodyComment = await pullRequestQueries.GetBodyAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                var bodyCommentBlock = new Timeline()
                {
                    ViewModel = new TimelineViewModel()
                    {
                        EventType = "IssueComment",
                        IssueComment = bodyComment,
                        CommentBlockViewModel = new()
                        {
                            IssueComment = bodyComment,
                        },
                    },
                };

                _eventBlocks.Add(bodyCommentBlock);

                PullRequestEventQueries queries = new();
                var pullEvents = await queries.GetAllAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                foreach (var eventItem in pullEvents)
                {
                    if (eventItem == null) continue;

                    var viewmodel = new TimelineViewModel()
                    {
                        // FluentHub.Octokit.Models.Events.*
                        EventType = eventItem.GetType().ToString().Split(".")[4],
                    };

                    switch (viewmodel.EventType)
                    {
                        case nameof(AddedToProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.AddedToProjectEvent = eventItem as AddedToProjectEvent;
                            viewmodel.Actor = viewmodel?.AddedToProjectEvent.Actor;
                            break;
                        case nameof(AssignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA38";
                            viewmodel.AssignedEvent = eventItem as AssignedEvent;
                            viewmodel.Actor = viewmodel?.AssignedEvent.Actor;
                            break;
                        case nameof(AutoMergeDisabledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BF";
                            viewmodel.AutoMergeDisabledEvent = eventItem as AutoMergeDisabledEvent;
                            viewmodel.Actor = viewmodel?.AutoMergeDisabledEvent.Actor;
                            break;
                        case nameof(AutoMergeEnabledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BF";
                            viewmodel.AutoMergeEnabledEvent = eventItem as AutoMergeEnabledEvent;
                            viewmodel.Actor = viewmodel?.AutoMergeEnabledEvent.Actor;
                            break;
                        case nameof(AutoRebaseEnabledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BF";
                            viewmodel.AutoRebaseEnabledEvent = eventItem as AutoRebaseEnabledEvent;
                            viewmodel.Actor = viewmodel?.AutoRebaseEnabledEvent.Actor;
                            break;
                        case nameof(AutoSquashEnabledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BF";
                            viewmodel.AutoSquashEnabledEvent = eventItem as AutoSquashEnabledEvent;
                            viewmodel.Actor = viewmodel?.AutoSquashEnabledEvent.Actor;
                            break;
                        case nameof(AutomaticBaseChangeFailedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.AutomaticBaseChangeFailedEvent = eventItem as AutomaticBaseChangeFailedEvent;
                            viewmodel.Actor = viewmodel?.AutomaticBaseChangeFailedEvent.Actor;
                            break;
                        case nameof(AutomaticBaseChangeSucceededEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.AutomaticBaseChangeSucceededEvent = eventItem as AutomaticBaseChangeSucceededEvent;
                            viewmodel.Actor = viewmodel?.AutomaticBaseChangeSucceededEvent.Actor;
                            break;
                        case nameof(BaseRefChangedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.BaseRefChangedEvent = eventItem as BaseRefChangedEvent;
                            viewmodel.Actor = viewmodel?.BaseRefChangedEvent.Actor;
                            break;
                        case nameof(BaseRefDeletedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.BaseRefDeletedEvent = eventItem as BaseRefDeletedEvent;
                            viewmodel.Actor = viewmodel?.BaseRefDeletedEvent.Actor;
                            break;
                        case nameof(BaseRefForcePushedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.BaseRefForcePushedEvent = eventItem as BaseRefForcePushedEvent;
                            viewmodel.Actor = viewmodel?.BaseRefForcePushedEvent.Actor;
                            break;
                        case nameof(ClosedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                            viewmodel.TimelineBadgeGlyph = "\uE9E6";
                            viewmodel.ClosedEvent = eventItem as ClosedEvent;
                            viewmodel.Actor = viewmodel?.ClosedEvent.Actor;
                            break;
                        case nameof(CommentDeletedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAB6";
                            viewmodel.CommentDeletedEvent = eventItem as CommentDeletedEvent;
                            viewmodel.Actor = viewmodel?.CommentDeletedEvent.Actor;
                            break;
                        case nameof(ConnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConnectedEvent = eventItem as ConnectedEvent;
                            viewmodel.Actor = viewmodel?.ConnectedEvent.Actor;
                            break;
                        case nameof(ConvertedNoteToIssueEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConvertedNoteToIssueEvent = eventItem as ConvertedNoteToIssueEvent;
                            viewmodel.Actor = viewmodel?.ConvertedNoteToIssueEvent.Actor;
                            break;
                        case nameof(CrossReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CrossReferencedEvent = eventItem as CrossReferencedEvent;
                            viewmodel.Actor = viewmodel?.CrossReferencedEvent.Actor;
                            break;
                        case nameof(DemilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA12";
                            viewmodel.DemilestonedEvent = eventItem as DemilestonedEvent;
                            viewmodel.Actor = viewmodel?.DemilestonedEvent.Actor;
                            break;
                        case nameof(DeployedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9C5";
                            viewmodel.DeployedEvent = eventItem as DeployedEvent;
                            viewmodel.Actor = viewmodel?.DeployedEvent.Actor;
                            break;
                        case nameof(DeploymentEnvironmentChangedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9C5";
                            viewmodel.DeploymentEnvironmentChangedEvent = eventItem as DeploymentEnvironmentChangedEvent;
                            viewmodel.Actor = viewmodel?.DeploymentEnvironmentChangedEvent.Actor;
                            break;
                        case nameof(DisconnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DisconnectedEvent = eventItem as DisconnectedEvent;
                            viewmodel.Actor = viewmodel?.DisconnectedEvent.Actor;
                            break;
                        case nameof(HeadRefDeletedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.HeadRefDeletedEvent = eventItem as HeadRefDeletedEvent;
                            viewmodel.Actor = viewmodel?.HeadRefDeletedEvent.Actor;
                            break;
                        case nameof(HeadRefForcePushedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.HeadRefForcePushedEvent = eventItem as HeadRefForcePushedEvent;
                            viewmodel.Actor = viewmodel?.HeadRefForcePushedEvent.Actor;
                            break;
                        case nameof(HeadRefRestoredEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9B7";
                            viewmodel.HeadRefRestoredEvent = eventItem as HeadRefRestoredEvent;
                            viewmodel.Actor = viewmodel?.HeadRefRestoredEvent.Actor;
                            break;
                        case nameof(IssueComment):
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem as IssueComment };
                            break;
                        case nameof(LabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAA5";
                            viewmodel.LabeledEvent = eventItem as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel?.LabeledEvent.Label.Name,
                                Color = viewmodel?.LabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel?.LabeledEvent.Actor;
                            break;
                        case nameof(LockedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#636E7B");
                            viewmodel.TimelineBadgeGlyph = "\uEA05";
                            viewmodel.LockedEvent = eventItem as LockedEvent;
                            viewmodel.Actor = viewmodel?.LockedEvent.Actor;
                            break;
                        case nameof(MarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE959";
                            viewmodel.MarkedAsDuplicateEvent = eventItem as MarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel?.MarkedAsDuplicateEvent.Actor;
                            break;
                        case nameof(MergedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BD";
                            viewmodel.MergedEvent = eventItem as MergedEvent;
                            viewmodel.Actor = viewmodel?.MergedEvent.Actor;
                            break;
                        case nameof(MilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA12";
                            viewmodel.MilestonedEvent = eventItem as MilestonedEvent;
                            viewmodel.Actor = viewmodel?.MilestonedEvent.Actor;
                            break;
                        case nameof(MovedColumnsInProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.MovedColumnsInProjectEvent = eventItem as MovedColumnsInProjectEvent;
                            viewmodel.Actor = viewmodel?.MovedColumnsInProjectEvent.Actor;
                            break;
                        case nameof(PinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA3E";
                            viewmodel.PinnedEvent = eventItem as PinnedEvent;
                            viewmodel.Actor = viewmodel?.PinnedEvent.Actor;
                            break;
                        case nameof(PullRequestCommit):
                            viewmodel.TimelineBadgeGlyph = "\uE9B9";
                            viewmodel.PullRequestCommit = eventItem as PullRequestCommit;
                            viewmodel.Actor = viewmodel?.PullRequestCommit.Actor;
                            break;
                        case nameof(PullRequestReview):
                            viewmodel.TimelineBadgeGlyph = "\uE98B";
                            viewmodel.PullRequestReview = eventItem as PullRequestReview;
                            viewmodel.Actor = viewmodel?.PullRequestReview.Actor;
                            break;
                        case nameof(ReadyForReviewEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE9BF";
                            viewmodel.ReadyForReviewEvent = eventItem as ReadyForReviewEvent;
                            viewmodel.Actor = viewmodel?.ReadyForReviewEvent.Actor;
                            break;
                        case nameof(ReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE968";
                            viewmodel.ReferencedEvent = eventItem as ReferencedEvent;
                            viewmodel.Actor = viewmodel?.ReferencedEvent.Actor;
                            break;
                        case nameof(RemovedFromProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.RemovedFromProjectEvent = eventItem as RemovedFromProjectEvent;
                            viewmodel.Actor = viewmodel?.RemovedFromProjectEvent.Actor;
                            break;
                        case nameof(RenamedTitleEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA34";
                            viewmodel.RenamedTitleEvent = eventItem as RenamedTitleEvent;
                            viewmodel.Actor = viewmodel?.RenamedTitleEvent.Actor;
                            break;
                        case nameof(ReopenedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uE9EC";
                            viewmodel.ReopenedEvent = eventItem as ReopenedEvent;
                            viewmodel.Actor = viewmodel?.ReopenedEvent.Actor;
                            break;
                        case nameof(ReviewDismissedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uE98D";
                            viewmodel.ReviewDismissedEvent = eventItem as ReviewDismissedEvent;
                            viewmodel.Actor = viewmodel?.ReviewDismissedEvent.Actor;
                            break;
                        case nameof(ReviewRequestRemovedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uE98D";
                            viewmodel.ReviewRequestRemovedEvent = eventItem as ReviewRequestRemovedEvent;
                            viewmodel.Actor = viewmodel?.ReviewRequestRemovedEvent.Actor;
                            break;
                        case nameof(ReviewRequestedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uE98B";
                            viewmodel.ReviewRequestedEvent = eventItem as ReviewRequestedEvent;
                            viewmodel.Actor = viewmodel.ReviewRequestedEvent.Actor;
                            break;
                        //case nameof(SubscribedEvent):
                        case nameof(UnassignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA38";
                            viewmodel.UnassignedEvent = eventItem as UnassignedEvent;
                            viewmodel.Actor = viewmodel?.UnassignedEvent.Actor;
                            break;
                        case nameof(UnlabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAA5";
                            viewmodel.UnlabeledEvent = eventItem as UnlabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel?.UnlabeledEvent.Label.Name,
                                Color = viewmodel?.UnlabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel?.UnlabeledEvent.Actor;
                            break;
                        case nameof(UnlockedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAC4";
                            viewmodel.UnlockedEvent = eventItem as UnlockedEvent;
                            viewmodel.Actor = viewmodel?.UnlockedEvent.Actor;
                            break;
                        case nameof(UnmarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA8C";
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem as UnmarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel?.UnmarkedAsDuplicateEvent.Actor;
                            break;
                        case nameof(UnpinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA3E";
                            viewmodel.UnpinnedEvent = eventItem as UnpinnedEvent;
                            viewmodel.Actor = viewmodel?.UnpinnedEvent.Actor;
                            break;
                        case nameof(UserBlockedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAD6";
                            viewmodel.UserBlockedEvent = eventItem as UserBlockedEvent;
                            viewmodel.Actor = viewmodel?.UserBlockedEvent.Actor;
                            break;
                    }
                    
                    var eventBlock = new Timeline()
                    {
                        ViewModel = viewmodel
                    };

                    _eventBlocks.Add(eventBlock);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(RefreshPullRequestPageAsync), ex);
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
