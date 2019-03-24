using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarsTool {
	static class Progam {
		[STAThread]
		static void Main(string[] args) {
			//if (!args.Contains("-nogui")) {
			string path = args.Length >= 1 ? args[0] : "";
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BarsViewerForm(path));
			//}
		}

		
	}
}
