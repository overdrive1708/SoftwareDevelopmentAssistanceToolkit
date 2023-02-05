using Prism.Mvvm;

namespace MainApplication.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = MainApplication.Core.Resources.Text.ApplicationName;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
