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

namespace UI.Propiedades
{
    public partial class ControlPrincipalPropiedades : UserControl
    {
        public event ObtenerPropiedades ObtenerPropied;

        public ControlPrincipalPropiedades()
        {
            InitializeComponent();
        }

        public ListView DameListView()
        {
            return listView1;
        }

        private void button1_Click(object sender, EventArgs e) //Buscar
        {
            String p="nada";
            try 
            {
                p = comboBoxFiltro.SelectedItem.ToString();
            }
            catch (Exception ex)
            { }
            
            MessageBox.Show(p);
        }

        private void ControlPrincipalPropiedades_Load(object sender, EventArgs e)
        {
            try
            {
                List<Propiedad> l = ObtenerPropied();
                foreach (Propiedad p in l)
                {
                    ActualizarListaPropiedades(p);
                }
            }
            catch(Exception ex)
            { }
        }

        public Boolean ActualizarListaPropiedades(Propiedad p)
        {
            ListViewItem a = new ListViewItem(p.NroPropiedad.ToString());
            a.SubItems.Add(p.Estado);
            a.SubItems.Add(p.Precio.ToString());

            listView1.Items.Add(a);

            return false;
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
