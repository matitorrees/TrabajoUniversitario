using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;

namespace UI.Propietarios
{
    public partial class ControlPrincipalPropietarios : UserControl
    {
        public event ObtenerPropietarios ObtenerProp;
        public ControlPrincipalPropietarios()
        {
            InitializeComponent();
        }

        public ListView DameListView()
        {
            return listView1;
        }

        private void ControlPrincipalPropietarios_Load(object sender, EventArgs e)
        {
            try
            {
                List<Propietario> l = ObtenerProp();
                foreach (Propietario p in l)
                    ActualizarListaPropietarios(p);
            }catch(Exception ex)
            { }
        }

        public Boolean ActualizarListaPropietarios(Propietario p)
        {
            ListViewItem a = new ListViewItem(p.NroPropietario.ToString());
            a.SubItems.Add(p.Nombre);
            a.SubItems.Add(p.Dni.ToString());

            listView1.Items.Add(a);

            return false;
        }

        public void SacateItem(ListViewItem a)
        {
            a.Remove();
        }
        public void SacateItem2(string a)
        {
            foreach (ListViewItem b in listView1.Items)
            {
                String nroProp = b.ToString();
                nroProp = nroProp.Trim('{', '}');
                nroProp = nroProp.Substring(15);
                if (nroProp.Equals(a))
                    b.Remove();
            }
        }
    }
}
