using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ElControls.ELHotKey;
using ElMessage;

namespace ServerLoadMonitoring
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl,IDisposable,IElHotKey
    {
		public MainControl(ElPluginClientSettings setting) {
			ConfigPlugin.connectionElServer = setting.ConnectionElServer;
			ConfigPlugin.BasePath = setting.PluginFolderPath;
			ConfigPlugin.TargetContext = null;
			ConfigPlugin.GlobalModel = new MainControlViewModel();
			////Если передан целевой параметр на запуск то запускаем с указаного контекста
			//if (setting.Parameters.ContainsKey("TargetContext")) {
			//	ConfigPlugin.TargetContext = (string)setting.Parameters["TargetContext"];
			//	ConfigPlugin.SetSecurityKey((ulong)setting.Parameters["SecurityKey"]);
			//}
			//Регестрируем идентификатор процесса
			ConfigPlugin.ProcessId = (int)setting.Parameters["PluginId"];
            if (setting.Parameters.ContainsKey("EL_APPDATA"))
                ConfigPlugin.ELAppData = (string)setting.Parameters["EL_APPDATA"];
			InitializeComponent();
			Base.DataContext = ConfigPlugin.GlobalModel;

		}

        #region DISPOSE

        private bool _disposed = false;

        // реализация интерфейса IDisposable.
        public void Dispose()
        {
            Dispose(true);
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

                foreach (var viewModel in navigationView.Items)
                {
                    if (!(viewModel is MainControlModel tmpss)) continue;
                    if (tmpss.Content is IDisposable dis) dis.Dispose();
                }
            }
            // освобождаем неуправляемые объекты
            _disposed = true;
        }

        // Деструктор
        ~MainControl()
        {
            Dispose(false);
        }
        #endregion
        public void OnElHotKey(KeyEventArgs e)
        {
            //Вызов пощи для расширения
            if (e.Key == Key.F1)
            {
                ConfigPlugin.GlobalModel.CommandStartHelper.Execute(null);
                return;
            }


            if (ConfigPlugin.GlobalModel.IncludedContentList[ConfigPlugin.GlobalModel.SelectedIndex].Content is IElHotKey vm) vm.OnElHotKey(e);

        }

        public void OnElScanData(List<KeyEventArgs> listKey)
        {
            if (ConfigPlugin.GlobalModel.IncludedContentList[ConfigPlugin.GlobalModel.SelectedIndex].Content is IElHotKey vm) vm.OnElScanData(listKey);
        }

	}
}
