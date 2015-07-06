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
using System.Reflection;
using System.IO;
using MRGPluginInterface;

namespace MRGSoftPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, MRGPluginInterface.IAppPlugin
    {
        ///所有插件的集合  
        private MRGPluginInterface.IPlugin[] vPlugs;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            menu1.IsOpen = true;
        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ___Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ksl_Click(object sender, RoutedEventArgs e)
        {
            //list1.Items.Clear();//清除所有
            LoadPlugIn();//加载插件
            /*
            if (list1.Items.Count > 2)
            {
                return;
            }
            list1.Items.Add("撸一把!");
            list1.Items.Add("大家一起撸!");
            list1.Items.Add("一起撸!一起撸!");
            list1.Items.Add("撸下来!");
            list1.Items.Add("撸下来!撸下来!");
            list1.Items.Add("撸个痛快!");
            list1.Items.Add("看不清了!");
            list1.Items.Add("撸的太多了吗？");
            list1.Items.Add("撸大师！");
            list1.Items.Add("一起撸大师！");
             */
        }

        // 载入插件  
        private void LoadPlugIn()
        {
            //　搜索目录下的所有DLL  
            string path = System.Environment.CurrentDirectory + "\\plugins"; // +"//plugs";  
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");
            // 产生插件数组
            vPlugs = new IPlugin[pluginFiles.Length];
            // 检索所有DLL  
            for (int i = 0; i < pluginFiles.Length; i++)
            {
                Type ObjType = null;
                try
                {
                    //载入插件  
                    Assembly ass = null;
                    ass = Assembly.LoadFrom(pluginFiles[i]);
                    if (ass != null)
                    {
                        //检索插件的指定类
                        string moduleName = ass.ManifestModule.Name.Split(new char[] { '.' })[0];//去除后缀名
                        string typePath = moduleName + ".PlugManager";
                        ObjType = ass.GetType(typePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    if (ObjType != null)
                    {
                        vPlugs[i] = (IPlugin)Activator.CreateInstance(ObjType);
                        vPlugs[i].Host = this;//添加主程序实例引用
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (vPlugs.Length > 0)
            {
                MessageBox.Show(string.Format("载入插件成功，共获取{0}个插件", vPlugs.Length));
            }
            else
            {
                MessageBox.Show(string.Format("载入插件失败，请确认已把插件文件放置在{0}目录中", path));
            }
        }

        /// <summary>
        /// 注册一个插件
        /// </summary>
        public bool Register(IPlugin vPlug)
        {
            /*
            ToolStripButton mn = new ToolStripButton();
            mn.Text = vPlug.PlugName;
            mn.Click += new EventHandler(NewLoad);
            toolStrip1.Items.Add(mn);*/
            ListBoxItem item = new ListBoxItem();
            item.MouseDoubleClick += NewLoad;
            list1.Items.Add(vPlug.PlugName);
            return true;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        private void NewLoad(object sender, System.EventArgs e)
        {
            ListBoxItem btnName = (ListBoxItem)sender;
            string plugName = btnName.Name;
            for (int i = 0; i < vPlugs.Length; i++)
            {
                if (vPlugs[i] == null) return;
                if (vPlugs[i].PlugName == plugName)
                {
                    vPlugs[i].ShowPlugFrm();
                    break;
                }
            }
        }
    }
}
