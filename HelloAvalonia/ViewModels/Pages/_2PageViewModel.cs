using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using System.Windows.Input;

namespace HelloAvalonia.ViewModels
{
    public class _2PageViewModel : ViewModelBase
    {

        private readonly Models.Settings _settings = Models.Settings.Instance;
        public ICommand OpenFolderDialogCommand { get; }
        private string _folderString = string.Empty;
        public string FolderString
        {
            get => _folderString;
            set => this.RaiseAndSetIfChanged(ref _folderString, value);
        }

        public _2PageViewModel()
        {
            OpenFolderDialogCommand = ReactiveCommand.Create<Window>(async (window) =>
            {
                FolderString = string.Empty;
                var folders = await window.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
                {
                    Title = "フォルダを開く",
                });

                if (folders.Count >= 1)
                {
                    var files = folders[0].GetItemsAsync();
                    await foreach (var file in files)
                    {
                        if (file.Name.EndsWith(_settings.Read().Script.Ext))
                        {
                            FolderString += $"{file.TryGetLocalPath()}\n";
                        }
                    }
                }
            });
        }
    }
}
