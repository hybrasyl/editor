using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace HybrasylEditor.UI
{
    public class FixedSpawnEditor
    {
        private Map map;

        public FixedSpawnEditor(Map map)
        {
            this.map = map;
        }

        public void Show()
        {
            // Showing the "CollectionEditor" outside of a PropertyGrid
            // http://stackoverflow.com/questions/3816018/is-there-any-way-to-use-a-collectioneditor-outside-of-the-property-grid
            
            PropertyDescriptor pd = TypeDescriptor.GetProperties(map)["SpawnsFixed"];
            var editor = (UITypeEditor)pd.GetEditor(typeof(UITypeEditor));
            RuntimeServiceProvider serviceProvider = new RuntimeServiceProvider();
            editor.EditValue(serviceProvider, serviceProvider, map.SpawnsFixed);
        }

    }
}
