using MVVMServiceSample.Common;
using MVVMServiceSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMServiceSample
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private RelayCommand showNameCommand;
        public RelayCommand ShowNameCommand
        {
            get { return showNameCommand = showNameCommand ?? new RelayCommand(ShowName); }
        }

        private void ShowName()
        {
            // サービス経由で、ダイアログの表示を行う。
            this._dialogService.ShowMessage($"こんにちは、{this.Name}さん");
        }
    }
}
