using Prism.Mvvm;

namespace SDAT.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = Core.Resources.Strings.ApplicationName;
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
