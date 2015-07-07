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

namespace MRGSoftPlatform.UI
{
    /// <summary>
    /// GirdItem.xaml 的交互逻辑
    /// </summary>
    public partial class GridItem : UserControl
    {
        public GridItem(string PluginImage, string PluginName)
        {
            InitializeComponent();

            this.PluginImg.Source = new BitmapImage(new Uri(PluginImage,UriKind.Relative));
            this.PluginName.Content = PluginName;
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("123456789");
        }
    }
}
