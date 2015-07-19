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
using System.Windows.Shapes;

namespace MRGSoftPlatform.Windows
{
    /// <summary>
    /// AddTools.xaml 的交互逻辑
    /// </summary>
    public partial class AddToolsWindow : Window
    {
        public AddToolsWindow()
        {
            InitializeComponent();
            this.ToolPath.AllowDrop = true;
            this.ToolPath.DragEnter += ToolPath_DragEnter;
            this.ToolIcon.AllowDrop = true;
        }

        void ToolPath_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("a");
        }

        /// <summary>
        /// 工具文件被放下
        /// </summary>
        private void OnToolPathDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        /// <summary>
        /// 图标文件被放下
        /// </summary>
        private void OnToolIconDrop(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        /// 工具文件被拖入
        /// </summary>
        private void OnToolPathDragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("enter");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }


    }
}
