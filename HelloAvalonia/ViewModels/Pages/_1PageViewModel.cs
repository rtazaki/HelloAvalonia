using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace HelloAvalonia.ViewModels
{
    public class _1PageViewModel : ViewModelBase
    {
        public ICommand FileOpenDialogCommand { get; }
        private string _fileString = string.Empty;
        public string FileString
        {
            get => _fileString;
            set => this.RaiseAndSetIfChanged(ref _fileString, value);
        }

        public _1PageViewModel()
        {
            FileOpenDialogCommand = ReactiveCommand.Create<Window>(async (window) =>
            {
                var files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "ファイルを開く",
                    FileTypeFilter = new List<FilePickerFileType>
                    {
                        new FilePickerFileType("オリジナル拡張子")
                        {
                            Patterns = new List<string>{ "*.cm*" },
                        },
                    },
                    AllowMultiple = false,
                }); ;

                if (files.Count >= 1)
                {
                    await using var stream = await files[0].OpenReadAsync();
                    using var streamReader = new StreamReader(stream);
                    FileString = await streamReader.ReadToEndAsync();
                }
            });
        }
    }
}
