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
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new BarsViewerForm());
			//}
		}

		
	}
}
