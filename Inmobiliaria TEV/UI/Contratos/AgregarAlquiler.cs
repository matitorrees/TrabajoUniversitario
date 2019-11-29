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

namespace UI.Contratos
{
    public partial class AgregarAlquiler : UserControl
    {

        public event ObtenerPropiedades ObtenerPropied;
        public event ObtenerInquilinos ObtenerInqui;

        List<Propiedad> l;
        List<Inquilino> l2;

        public AgregarAlquiler()
        {
            InitializeComponent();
        }

        private void AgregarAlquiler_Load(object sender, EventArgs e)
        {
            
            l = ObtenerPropied();

            foreach(Propiedad p in l)
            {
                if(p.Estado =="Alquiler")
                {
                    listBox1.Items.Add(p.ToString3());
                }
            }

            l2 = ObtenerInqui();

            foreach (Inquilino i in l2)
            {

                listBox2.Items.Add(i.ToString3());

            }

        }

        private void button1_Click(object sender, EventArgs e) //SeleccionarInquilino
        {
            if (listBox2.SelectedItem != null)
            {
                MessageBox.Show("Seleccionó el inquilino: " + listBox2.SelectedItem.ToString());
                textBoxSeleccionadoInquilino.Text = listBox2.SelectedItem.ToString();

            }
            else
                MessageBox.Show("No seleccionó ningun inquilino");
        }

        private void buttonSeleccionarPropiedad_Click(object sender, EventArgs e) //Seleccionar propiedad
        {
            int i = 0;

            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show("Seleccionó la propiedad: " + listBox1.SelectedItem.ToString());
                textBoxPropSeleccionadoPropiedad.Text = listBox1.SelectedItem.ToString();

                if (textBoxPropSeleccionadoPropiedad.Text != "")
                {
                    String prop = textBoxPropSeleccionadoPropiedad.Text;

                    while (i < l.Count && l[i].ToString3() != prop)
                        i++;

                    if (i < l.Count)
                    {
                        textBoxPrecioCuota.Text = l[i].Precio.ToString();
                    }
                }

            }
            else
                MessageBox.Show("No seleccionó ninguna propiedad");
        }


        private void buttonGuardar_Click(object sender, EventArgs e) //Boton Guardar
        {
            int nroContrato = 0;
            int i, j;
            try
            {
                if (textBoxPropSeleccionadoPropiedad.Text == "")
                {
                    throw new ExceptionCustom("Olvido seleccionar la propiedad");
                }

                if (textBoxSeleccionadoInquilino.Text == "")
                {
                    throw new ExceptionCustom("Olvido seleccionar el inquilino");
                }

                if (comboBox1.SelectedItem == null)
                {
                    throw new ExceptionCustom("Olvido seleccionar el periodo del contrato");
                }

                ///////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////
                /// VALIDAR DESPUES DE QUE SI LA PROPIEDAD NO TIENE ASIGNADO UN PROPIETARIO NO PUEDE HACERSE UN CONTRATO CON LA MISMA///
                /// 
                DateTime emision = DateTime.Today;
                //Tambien hay que hccer herencia para los dos tiposde contrato, cambiarlo despues.
                Contrato contrato = new Contrato(nroContrato,emision);

                if (textBoxPropSeleccionadoPropiedad.Text != "")
                {
                    i = 0;
                    String prop = textBoxPropSeleccionadoPropiedad.Text;

                    while (i < l.Count && l[i].ToString3() != prop)
                        i++;

                    if (i < l.Count)
                    {
                        contrato.Propiedad = l[i];
                    }
                }

                if (textBoxSeleccionadoInquilino.Text != "")
                {
                    j = 0;
                    String prop = textBoxSeleccionadoInquilino.Text;

                    while (j < l2.Count && l2[j].ToString3() != prop)
                        j++;

                    if (j < l2.Count)
                    {
                        contrato.Inquilino = l2[j];
                    }
                }

                ///Aca un if con un bool delegado para saber si crea el contrato en la administradora, una vez que lo hace le generamos las cuotas y hacemos la actualizacion del controlprincipal de contratos 
                ///(falta hacer ese control)
                ///
                //List<Cuota> listaCuotas = new List<Cuota>();
                //DateTime aux;
                
                //aux = emision;
                //DateTime fpago = new DateTime(0001, 1, 01);

                //for (int j = 0; j < 12; j++)
                //{
                //    Factura f;

                //    DateTime vencimiento = aux.AddDays(30);
                //    if (s.Tipo.Equals("CLUB"))
                //        f = new FacturaSocio(s, emision, vencimiento, j + 1, false, fpago);
                //    else
                //        f = new FacturaActividad(s, emision, vencimiento, j + 1, false, fpago);

                //    listaCuotas.Add(f);
                //    aux = vencimiento;
                //}
                //s.ListaDeCuotas = listaCuotas;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de datos:" + System.Environment.NewLine + ex.Message, "Error");
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e) //Boton Cancelar
        {
            textBoxPrecioCuota.Clear();
            textBoxPropSeleccionadoPropiedad.Clear();
            textBoxSeleccionadoInquilino.Clear();
            comboBox1.Text = "";
            Hide();
        }
    }
}
