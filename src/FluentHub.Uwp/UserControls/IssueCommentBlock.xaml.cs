using FluentHub.Uwp.Extensions;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class IssueCommentBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(IssueCommentBlockViewModel),
                typeof(IssueCommentBlock),
                new PropertyMetadata(null)
                );

        public IssueCommentBlockViewModel ViewModel
        {
            get => (IssueCommentBlockViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                ViewModel?.SetMarkdownCommentToWebViewAsync(CommentWebView);
            }
        }
        #endregion

        public IssueCommentBlock()
            => InitializeComponent();

        private async void OnCommentWebViewNavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
            => await((WebView2)sender).HandleResize();

        private async void OnWebViewSizeChanged(object sender, SizeChangedEventArgs e)
            => await ((WebView2)sender).HandleResize();

        private async void OnCommentWebViewLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
