﻿using System;
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
using MRGSoftPlatform.UI;
using MRGSoftPlatform.Windows;

namespace MRGSoftPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, MRGPluginInterface.IAppPlugin
    {
        ///所有插件的集合  
        private MRGPluginInterface.IPlugin[] vPlugs;

        private List<IPlugin> regPluginList;

        public MainWindow()
        {
            InitializeComponent();
            regPluginList = new List<IPlugin>();
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
            list1.Items.Clear();//清除所有
            LoadPlugIn();//加载插件
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
            ListBoxItem item = new ListBoxItem();
            item.MouseDoubleClick += NewLoad;
            list1.Items.Add(vPlug.PlugName);*/
            try
            {
                this.regPluginList.Add(vPlug);//添加到已注册列表

                GridItem button = new GridItem("Image/fullbox_desk_clear.png", vPlug.PlugName);
                int pluNum = this.regPluginList.Count;
                int x = (pluNum - 1) % 6;
                int y = (pluNum - 1) / 6;
                AddToGrid(button, x, y);

                //----超出部分未处理

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void AddToGrid(GridItem item, int x, int y)
        {
            PluginGrid.Children.Add(item);
            item.SetValue(Grid.ColumnProperty, x);
            item.SetValue(Grid.RowProperty, y);
            item.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            item.VerticalAlignment = System.Windows.VerticalAlignment.Center;
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

        private void AddTools(object sender, RoutedEventArgs e)
        {
            Window window = new AddToolsWindow();
            window.Show();
        }
    }
}
