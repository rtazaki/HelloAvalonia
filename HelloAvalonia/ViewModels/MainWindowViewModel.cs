using ReactiveUI;
using System.Collections.Generic;

namespace HelloAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _currentPage = PageNames[0].ViewModel;
        }

        /// <summary>
        /// TransitioningContentControlのContentにバインディングして画面遷移を実現する。
        /// </summary>
        private ViewModelBase? _currentPage;
        public ViewModelBase? CurrentPage
        {
            get { return _currentPage; }
            private set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }

        /// <summary>
        /// ComboBoxの表示アイテムと、各ViewModelを一元管理する。
        /// </summary>
        public class PageInfo
        {
            public string? Name { get; set; }
            public ViewModelBase? ViewModel { get; set; }
        }

        public List<PageInfo> PageNames => new List<PageInfo>
        {
            new PageInfo{ Name="111", ViewModel= new _1PageViewModel() },
            new PageInfo{ Name="222", ViewModel= new _2PageViewModel() },
            new PageInfo{ Name="333", ViewModel= new _3PageViewModel() },
        };

        /// <summary>
        /// ComboBoxのSelectedIndexにバインディングして表示したい画面を指定する。
        /// </summary>
        private int _pageIndex;
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                CurrentPage = PageNames[value].ViewModel;
            }
        }
    }
}