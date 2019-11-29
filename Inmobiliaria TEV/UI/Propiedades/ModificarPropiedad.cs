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
    public partial class ModificarPropiedad : UserControl
    {
        Propiedad p;
        public event ModifPropiedad ModifiPropied;
        public event SacateItem Sacatelo;
        public event GuardarPropiedad ActualizarControlPropiedad;

        public ModificarPropiedad()
        {
            InitializeComponent();
        }

        public void CargarTextBox(Propiedad p1)
        {
            p = p1;
            this.textBoxDireccion.Text = p.Direccion;
            this.textBoxZona.Text = p.Zona;
            this.textBoxProvincia.Text = p.Provincia;

            if (p.Estado == "Venta")
                radioButtonVenta.Checked = true;
            else
                radioButtonAlquiler.Checked = true;

            comboBoxTipo.Text = p.Tipo;
            textBoxAmbientes.Text = p.Ambientes.ToString();
            textBoxPrecio.Text = p.Precio.ToString();

            if (p.Propietario != null)
                textBoxPropSeleccionado.Text = p.Propietario.ToString3();

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

                if (comboBoxTipo.SelectedItem == null)
                {
                    throw new ExceptionCustom("Olvido seleccionar el tipo de propiedad");
                }

                if (textBoxPrecio.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner el precio");
                }

                if (radioButtonAlquiler.Checked == false && radioButtonVenta.Checked == false)
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


                if (tipo != "Terreno" && textBoxAmbientes.Text == "")
                {
                    throw new ExceptionCustom("Olvido poner la cantidad de ambientes");
                }

                if (tipo != "Terreno")
                    ambien = int.Parse(textBoxAmbientes.Text);



                Propiedad propiedadNueva = new Propiedad(p.NroPropiedad, direc, zona, prov, estado, tipo, ambien, prec);

                if(p.Propietario!=null)
                    propiedadNueva.Propietario = p.Propietario;


                if (ModifiPropied(propiedadNueva,p))
                {
                    Sacatelo(p.NroPropiedad.ToString());
                    ActualizarControlPropiedad(propiedadNueva);

                    if (textBoxPropSeleccionado.Text != "")             //Acá le agrego al propietario la propiedad a su lista de propiedades y le elimino la vieja (Si tenia).
                    {
                        p.Propietario.Propiedades.Add(propiedadNueva);
                        p.Propietario.Propiedades.Remove(p);
                    }
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de datos:" + System.Environment.NewLine + ex.Message, "Error");
            }
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipo.SelectedItem.ToString() != "Terreno")
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
    }
}
