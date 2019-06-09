using MVVMShowWindowSample.Common;
using MVVMShowWindowSample.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShowWindowSample
{
    class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// ダイアログ表示をしてTODO要素の編集を行うためのサービス
        /// </summary>
        private IShowWindowService<ToDoItemViewModel> _showWindowService;

        public MainWindowViewModel(IShowWindowService<ToDoItemViewModel> showWindowService)
        {
            this._showWindowService = showWindowService;
            this.ToDoList = new ObservableCollection<ToDoItemViewModel>();
        }


        private ObservableCollection<ToDoItemViewModel> toDoList;
        public ObservableCollection<ToDoItemViewModel> ToDoList
        {
            get { return toDoList; }
            set { SetProperty(ref toDoList, value); }
        }

        private ToDoItemViewModel selectedItem;
        public ToDoItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set{
                SetProperty(ref selectedItem, value);
                // リストボックスの選択状態が変わったので、各種コマンドの実行可否を更新する。
                this.EditItemCommand.RaiseCanExecuteChanged();
                this.RemoveItemCommand.RaiseCanExecuteChanged();
            }
        }


        #region 各種コマンドの実装
        private RelayCommand editItemCommand;
        public RelayCommand EditItemCommand
        {
            get { return editItemCommand = editItemCommand ?? new RelayCommand(EditItem, () => this.SelectedItem != null); }
        }
        private void EditItem()
        {
            var editVM = this.SelectedItem.Clone();
            var result = this._showWindowService.ShowDialog(editVM);
            if (result == true)
            {
                // 編集ダイアログでOKが押されていたら、その結果をToDoListに反映する
                this.SelectedItem.Title = editVM.Title;
                this.SelectedItem.Detail = editVM.Detail;
            }
        }

        private RelayCommand addItemCommand;
        public RelayCommand AddItemCommand
        {
            get { return addItemCommand = addItemCommand ?? new RelayCommand(AddItem); }
        }
        private void AddItem()
        {
            // 追加要素のVMを作成する
            var newItemViewModel = new ToDoItemViewModel();
            var ret = this._showWindowService.ShowDialog(newItemViewModel);
            if (ret == true)
            {
                // ダイアログでOKが押された場合は、ToDoListに要素を追加する。
                this.ToDoList.Add(newItemViewModel);
            }
        }

        private RelayCommand removeItemCommand;
        public RelayCommand RemoveItemCommand
        {
            get { return removeItemCommand = removeItemCommand ?? new RelayCommand(RemoveItem, () => this.SelectedItem != null); }
        }
        private void RemoveItem()
        {
            this.ToDoList.Remove(this.SelectedItem);
        }
        #endregion
    }
}
