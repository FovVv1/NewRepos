using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ElMessage;
using Newtonsoft.Json;
using ServerLoadMonitoring.Helpers;
using Telerik.Windows.Controls;

namespace ServerLoadMonitoring {
	public class MainControlViewModel:ViewModelBase {
		private int _selectedIndex;
        /// <summary>Индекс выбраной страницы </summary>
		public int SelectedIndex {
			get => _selectedIndex;
            set {
				_selectedIndex = value;
				OnPropertyChanged("SelectedIndex");
		}       
		}

        /// <summary>Коллекция контролов содержимого страниц </summary>
		private ObservableCollection<MainControlModel> _includedContentList;
		public ObservableCollection<MainControlModel> IncludedContentList {
			get => _includedContentList;
            set {
				_includedContentList = value;
				OnPropertyChanged("IncludedContentList");
			}
		} 
		
		/// <summary>Коллекция контролов содержимого страниц </summary>
		private ObservableCollection<IMonitoringElUserControl> _ControlsList;
		public ObservableCollection<IMonitoringElUserControl> ControlsList {
			get => _ControlsList;
            set {
				_ControlsList = value;
				OnPropertyChanged("ControlsList");
			}
		}

		public void ShowPage() {
			//IncludedContentList.FirstOrDefault(m => m.Title == "Дашборд").Visibility = Visibility.Visible;
			//IncludedContentList.FirstOrDefault(m => m.Title == "Данные отчета").Visibility = Visibility.Visible;
			//IncludedContentList.FirstOrDefault(m => m.Title == "Настройки отчета").Visibility = Visibility.Visible;

		}

		public MainControlViewModel() {
			IncludedContentList = new ObservableCollection<MainControlModel>();
            //Команда вызова помошника для расширения
			CommandStartHelper = new DelegateCommand(OnStartHelper);

			ControlsList = new ObservableCollection<IMonitoringElUserControl>() {new ServerLoadMonitoring(), new ServerLoadMonitoringAdditional()};

			//IncludedContentList.Add(new MainControlModel(typeof(Dashboard.Dashboard)) { Image = "&#xe600;", Title = "Дашборд" });
			//IncludedContentList.Add(new MainControlModel(typeof(ServerLoadMonitoringData.ServerLoadMonitoringAdditional)) { Image = "&#xe651;", Title = "Журнал заданий" });
			//IncludedContentList.Add(new MainControlModel(typeof(ServerLoadMonitoringSettings.ServerLoadMonitoringSettings)) { Image = "&#xe10f;", Title = "Настройки отчета" });
		}


 #region COMMANDS
		
		public ICommand CommandStartHelper { get; set; }
		private void OnStartHelper(object obj) {

            //Формируем набр параметров для удаленного запуска расширения
            var param = new Dictionary<string, object>
            {
                  {"PluginName", "Helper"}
                , {"NotLimitCopies", ""}
                , {"PageIndex", (int) 10}
            };

            ////Формируем список пользователей для назначения
			var targetListUserId = new List<int>();
			targetListUserId.Add(ConfigPlugin.connectionElServer.CurrentUser.UserID);
			param.Add("TargetListUserId", JsonConvert.SerializeObject(targetListUserId));
			////**********************************************

            //Регистрация запуска расширения для списка последних использованых
			ConfigPlugin.connectionElServer.SendMessage(new ElMessageClient("BasicFunctions", "ShowPlugin", null), JsonConvert.SerializeObject(param));

		}

 #endregion
	}
}
