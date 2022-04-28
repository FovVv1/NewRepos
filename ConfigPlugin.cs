using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElMessage;

namespace ServerLoadMonitoring {
	public static class ConfigPlugin {

		public static ElConnectionServer connectionElServer;
		public static string BasePath;
		private static ulong _SecurityKey;

        public static string ELAppData { get; set; }
		public static ulong SecurityKey {
            get { return _SecurityKey; }
		}

		public static MainControlViewModel GlobalModel { get; set; }

		public static string TargetContext { get; set; }
		public static int ProcessId { get; set; }

		public static void SetSecurityKey(ulong key) {
			_SecurityKey = key;
			GlobalModel.ShowPage();
		}

		public static bool GetPermissions(int permission) {
			return (((ulong)1 << (permission - 1)) & ConfigPlugin.SecurityKey) > 0 ? true : false;
		}

		public static long SupplyIdForOrder { get; set; }
	}
}
