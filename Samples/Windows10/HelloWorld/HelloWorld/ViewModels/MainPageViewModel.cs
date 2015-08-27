using System.Collections.Generic;
using Windows.Foundation.Metadata;
using Windows.UI.ApplicationSettings;
using HelloWorld.Services;
using Prism.Windows.Mvvm;
using Prism.Windows.Interfaces;
using Prism.Commands;

namespace HelloWorld.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IDataRepository _dataRepository;
        
        public MainPageViewModel(IDataRepository dataRepository, INavigationService navService)
        {
            _dataRepository = dataRepository;

            NavigateCommand = new DelegateCommand(() => navService.Navigate("UserInput", null));
        }

        public DelegateCommand NavigateCommand { get; set; }

        public List<string> DisplayItems
        {
            get { return _dataRepository.GetFeatures(); }
        }

        private bool _isNavigationDisabled;

        public bool IsNavigationDisabled
        {
            get { return _isNavigationDisabled; }
            set { SetProperty(ref _isNavigationDisabled, value); }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            e.Cancel = _isNavigationDisabled;

            base.OnNavigatingFrom(e, viewModelState, suspending);
        }
    }
}
