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

namespace UI.Pagos
{
    public partial class ControlCuotas : UserControl
    {
        Inquilino socioLocal;

        public ControlCuotas()
        {
            InitializeComponent();
        }
        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = (dataGridView1.ClientRectangle.Height - dataGridView1.ColumnHeadersHeight) / dataGridView1.Rows.Count;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void CargarControl(Inquilino s)
        {
            //List<Factura> listafacturas = s.ListaDeCuotas;
            //int i = 0;
            //int j = 0;

            //for (j = 0; j < 12; j++)
            //    dataGridView1.Rows.Add();

            //foreach (Factura f in listafacturas)
            //{
            //    dataGridView1.Rows[i].Cells[0].Value = i + 1;
            //    if (f.Estado)
            //        dataGridView1.Rows[i].Cells[1].Value = "Pagada";
            //    else
            //        dataGridView1.Rows[i].Cells[1].Value = "Impago";
            //    dataGridView1.Rows[i].Cells[2].Value = f.FechaEmision;
            //    dataGridView1.Rows[i].Cells[3].Value = f.FechaVencimiento;
            //    dataGridView1.Rows[i].Cells[4].Value = "$" + f.CalcularTotal().ToString();
            //    i++;
            //}
            //dataGridView1.ClearSelection();
            //socioLocal = s;
        }

        private void Button2_Click(object sender, EventArgs e) //cancelar
        {
            Hide();
        }

        private void Button1_Click(object sender, EventArgs e) //Pagar
        {
            foreach (DataGridViewCell c in dataGridView1.SelectedCells)
            {
                if (c.ColumnIndex == 5)
                {
                    //MessageBox.Show("Cuota: "+c.RowIndex.ToString());
                   // socioLocal.Pagar(c.RowIndex + 1);
                }
            }

            Hide();
        }

        
    }
}
