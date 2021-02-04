using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SummonersWar
{
    public partial class hWndList : Form
    {
        public hWndList()
        {
            InitializeComponent();
            ListHwndDetail();
        }

        public void ListHwndDetail()
        {
            WindowsAPI fw = new WindowsAPI();
            fw.GetEnums();

            List<string> HwndName = fw.ToGetHwndNameList();
            List<string> DexCode = fw.ToGetDexCodeList();
            List<IntPtr> HwndList = fw.ToGetHwndList();

            ConsoleBox.Items.Clear();
            for (int i = 0; i < HwndName.Count; i++)
                ConsoleBox.Items.Add(HwndList[i] + " , " + HwndName[i] + " , " + DexCode[i]);
        }
        
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
