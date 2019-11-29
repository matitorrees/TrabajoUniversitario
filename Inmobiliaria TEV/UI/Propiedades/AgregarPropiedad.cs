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
    public partial class AgregarPropiedad : UserControl
    {
        public event ObtenerPropietarios ObtenerProp;
        public event GuardarPropiedad PropiedadCreada, ActualizarControlPropiedad;

        List<Propietario> lista;
        Propiedad p;

        public AgregarPropiedad()
        {
            InitializeComponent();
        }

        private void AgregarPropiedad_Load(object sender, EventArgs e)
        {

            label3.Hide();
            textBoxAmbientes.Hide();

            lista = ObtenerProp();   

            foreach (Propietario p in lista)
            {
                listBox1.Items.Add(p.Nombre + " - " + p.Dni);
            }
        }

        private void buttonSeleccionar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                MessageBox.Show("Seleccionó el propietario: "+listBox1.SelectedItem.ToString());
                textBoxPropSeleccionado.Text= listBox1.SelectedItem.ToString();

            }
            else
                MessageBox.Show("No seleccionó ningun propietario");
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxTipo.SelectedItem.ToString()!="Terreno")
            {
                label3.Show();
                textBoxAmbientes.Show();
            }
            else
            {
                label3.Hide();
                textBoxAmbientes.Hide();
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            textBoxDireccion.Clear();
            textBoxZona.Clear();
            textBoxProvincia.Clear();
            textBoxPrecio.Clear();
            textBoxAmbientes.Clear();
            textBoxPropSeleccionado.Clear();
            Hide();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            int nroProp = 0;
            String direc = "";
            String zona = "";
            String prov = "";
            String tipo = "";
            String estado = "";
            int ambien = 0;
            double prec = 0;

            int i = 0;

            try
            {
                if (textBoxDireccion.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner la dirección");
                }

                if (textBoxZona.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner la zona");
                }

                if (textBoxProvincia.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner la provincia");
                }

                if(comboBoxTipo.SelectedItem==null)
                {
                    throw new ExceptionCustom("Olvido seleccionar el tipo de propiedad");
                }

                if (textBoxPrecio.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner el precio");
                }

                if(radioButtonAlquiler.Checked == false && radioButtonVenta.Checked == false)
                {
                    throw new ExceptionCustom("Olvido poner si estará a la venta o en alquiler");
                }


                direc = textBoxDireccion.Text;
                zona = textBoxZona.Text;
                prov = textBoxProvincia.Text;
                tipo = comboBoxTipo.SelectedItem.ToString();
                prec = double.Parse(textBoxPrecio.Text);

                if (radioButtonVenta.Checked)
                {
                    estado = "Venta";
                }
                else
                    estado = "Alquiler";

                
                if(tipo!="Terreno" && textBoxAmbientes.Text== "")
                {
                    throw new ExceptionCustom("Olvido poner la cantidad de ambientes");
                }

                if(tipo!="Terreno")
                    ambien = int.Parse(textBoxAmbientes.Text);
                
                

                p = new Propiedad(nroProp, direc, zona, prov, estado, tipo, ambien, prec);

                if (textBoxPropSeleccionado.Text != "")
                {
                    String prop = textBoxPropSeleccionado.Text;

                    while (i < lista.Count && lista[i].ToString3() != prop)
                        i++;

                    if (i < lista.Count)
                    {
                        p.Propietario = lista[i];
                    }
                }

                if (PropiedadCreada(p))
                {
                    ActualizarControlPropiedad(p);
                    if(textBoxPropSeleccionado.Text != "") //Acá le agrego al propietario la propiedad a su lista de propiedades (Si lo seleccionó).
                        p.Propietario.Propiedades.Add(p);
                    
                    textBoxDireccion.Clear();
                    textBoxZona.Clear();
                    textBoxProvincia.Clear();
                    textBoxPrecio.Clear();
                    textBoxAmbientes.Clear();
                    textBoxPropSeleccionado.Clear();
                    Hide();
                }
                else
                {
                    MessageBox.Show("La propiedad ya existe", null, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (textBoxPropSeleccionado.Text == "")
                        MessageBox.Show("No asignó ningún propietario a la propiedad. Si desea hacerlo luego puede hacerlo desde la parte de propietarios o la parte de propiedades");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de datos:" + System.Environment.NewLine + ex.Message, "Error");
            }
        }
    }
}
