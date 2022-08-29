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
using WarehouseСontexts.ContextSelector;

namespace ServerLoadMonitoring.Helpers {
	/// <summary>
	/// Interaction logic for JobsHeatmapControl.xaml
	/// </summary>
	public partial class JobsHeatmapControl:IMonitoringElUserControl {
		public JobsHeatmapControl() {
			InitializeComponent();
		}

		public ContextSelector contextSelector { get; set; }
		public int RefreshTime { get; set; }
		public bool IsAutoRefresh { get; set; }
		public void RefreshData()
		{
			

		}
	}
}
