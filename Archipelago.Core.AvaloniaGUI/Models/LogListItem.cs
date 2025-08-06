using Archipelago.Core.AvaloniaGUI.Logging;
using Archipelago.Core.AvaloniaGUI.Utils;
using Archipelago.Core.AvaloniaGUI.ViewModels;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Color = Avalonia.Media.Color;

namespace Archipelago.Core.AvaloniaGUI.Models
{
    public class LogListItem : ViewModelBase
    {
        private ObservableCollection<TextSpan> _textSpans = new ObservableCollection<TextSpan>();

        public ObservableCollection<TextSpan> TextSpans
        {
            get => _textSpans;
            set => this.RaiseAndSetIfChanged(ref _textSpans, value);
        }

        public LogListItem(string text)
        {
            TextSpans = new ObservableCollection<TextSpan>()
            {
                new TextSpan(){Text = text},
            };
        }
        public LogListItem(string text, Color color)
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                TextSpans = new ObservableCollection<TextSpan>()
                {
                    new TextSpan(){Text = text, TextColor = new SolidColorBrush(color)},
                };
            });

        }
        public LogListItem(IEnumerable<TextSpan> spans)
        {
            TextSpans = spans.ToObservableCollection();
        }
        public LogListItem(APMessageModel message)
        {
            TextSpans = new ObservableCollection<TextSpan>();
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                foreach (var part in message.Parts)
                {
                    var span = new TextSpan();
                    span.Text = part.Text;
                    span.TextColor = new SolidColorBrush(Color.FromRgb((byte)part.Color.R, (byte)part.Color.G, (byte)part.Color.B));
                    TextSpans.Add(span);
                }
            });
        }
    }
}
