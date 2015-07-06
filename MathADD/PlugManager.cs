using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRGPluginInterface;

namespace MathADD
{
    class PlugManager : IPlugin
    {
        private string _plugName = "加法";
        public string PlugName
        {
            get
            {
                return this._plugName;
            }
            set
            {
                this._plugName = value;
            }
        }
        private IAppPlugin _appPlugin = null;
        public IAppPlugin Host
        {
            get
            {
                return this._appPlugin;
            }
            set
            {
                _appPlugin = value;
                _appPlugin.Register(this);
            }
        }

        public void ShowPlugFrm()
        {
            Form1 frm = new Form1();
            frm.ShowDialog();  
        }
    }
}
