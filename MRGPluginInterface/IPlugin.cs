using System;
using System.Collections.Generic;
using System.Text;

namespace MRGPluginInterface
{
    public interface IPlugin
    {
        /// <summary>  
        /// 插件名  
        /// </summary>
        string PlugName
        { get; set; }

        ///主程接口  
        IAppPlugin Host
        { get; set; }

        /// <summary>
        /// 显示窗口
        /// </summary>
        void ShowPlugFrm();
    }

    /// <summary>
    /// 主程序的接口
    /// </summary>
    public interface IAppPlugin
    {
        /// <summary>
        /// 动态加载插件
        /// </summary>
        bool Register(IPlugin vPlug);
    }
}
