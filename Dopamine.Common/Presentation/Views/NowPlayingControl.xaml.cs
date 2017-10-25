﻿using Dopamine.Common.Presentation.Views.Base;
using Dopamine.Common.Prism;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace Dopamine.Common.Presentation.Views
{
    public partial class NowPlayingControl : PlaylistViewBase
    {
        public NowPlayingControl()
        {
            InitializeComponent();

            this.ViewInExplorerCommand = new DelegateCommand(() => this.ViewInExplorer(this.ListBoxTracks));
            this.JumpToPlayingTrackCommand = new DelegateCommand(() => this.ScrollToPlayingTrackAsync(this.ListBoxTracks));

            // PubSub Events
            this.eventAggregator.GetEvent<ScrollToPlayingTrack>().Subscribe(async (_) => await this.ScrollToPlayingTrackAsync(this.ListBoxTracks));
        }
      
        private async void ListBoxTracks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await this.ActionHandler(sender, e.OriginalSource as DependencyObject, false);
        }

        private void ListBoxTracks_KeyUp(object sender, KeyEventArgs e)
        {
            this.KeyUpHandlerAsync(sender, e);
        }

        private void ListBoxTracks_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ActionHandler(sender, e.OriginalSource as DependencyObject, false);
            }
        }
    }
}
