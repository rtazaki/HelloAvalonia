using Avalonia;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using System;

namespace HelloAvalonia
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI()
                .With(new FontManagerOptions
                {
                    //DefaultFamilyName = "avares://HelloAvalonia/Assets/BIZUDPGothic-Regular.ttf#BIZ UDPGothic",
                    DefaultFamilyName = "avares://HelloAvalonia/Assets/HackGen-Regular.ttf#HackGen",
                    //DefaultFamilyName = "avares://HelloAvalonia/Assets/NotoSansJP-Regular.ttf#Noto Sans JP",
                    //DefaultFamilyName = "avares://HelloAvalonia/Assets/UDEVGothic-Regular.ttf#UDEV Gothic",
                    FontFallbacks = new[]
                    {
                        new FontFallback
                        {
                            //FontFamily = new FontFamily("avares://HelloAvalonia/Assets/BIZUDPGothic-Regular.ttf#BIZ UDPGothic")
                            FontFamily = new FontFamily("avares://HelloAvalonia/Assets/HackGen-Regular.ttf#HackGen")
                            //FontFamily = new FontFamily("avares://HelloAvalonia/Assets/NotoSansJP-Regular.ttf#Noto Sans JP")
                            //FontFamily = new FontFamily("avares://HelloAvalonia/Assets/UDEVGothic-Regular.ttf#UDEV Gothic")
                        }
                    }
                });
    }
}