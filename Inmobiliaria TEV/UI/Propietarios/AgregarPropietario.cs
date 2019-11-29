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
    public partial class AgregarPropietario : UserControl
    {
        public event GuardarPropietario PropietarioCreado, ActualizarControlPropietario;
        Propietario p;
        public AgregarPropietario()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e) //Cancelar
        {
            this.textBoxCelProf.Clear();
            this.textBoxDniProf.Clear();
            this.textBoxMailProf.Clear();
            this.textBoxNombreProf.Clear();
            this.textBoxTelProf.Clear();
            this.richTextBoxComProf.Clear();
            Hide();
        }


        private void BtnGuardar_Click(object sender, EventArgs e) //Guardar
        {
            int nroProp = 0;
            long tel = 0;
            long dni = 0;
            long cel = 0;
            string nom = "";
            string mail = "";

            try
            {
                if (textBoxTelProf.TextLength < 8)
                {
                    throw new ExceptionCustom("Telefono es invalido");
                }

                if (textBoxCelProf.TextLength < 8)
                {
                    throw new ExceptionCustom("Celular es invalido");
                }

                nom = textBoxNombreProf.Text;
                tel = long.Parse(textBoxTelProf.Text);
                dni = long.Parse(textBoxDniProf.Text);
                cel = long.Parse(textBoxCelProf.Text);
                mail = textBoxMailProf.Text;

                if (nom.Length < 2)
                {
                    throw new ExceptionCustom("Nombre debe tener mas de 3 letras");
                }
                if (mail.Length < 7 || !mail.Contains("@") || !mail.Contains(".com"))
                {
                    throw new ExceptionCustom("Email es invalido");
                }

                p = new Propietario(nroProp, textBoxNombreProf.Text, dni,tel, cel, mail, richTextBoxComProf.Text);

                if (PropietarioCreado(p))
                {
                    ActualizarControlPropietario(p);
                    this.textBoxCelProf.Clear();
                    this.textBoxDniProf.Clear();
                    this.textBoxMailProf.Clear();
                    this.textBoxNombreProf.Clear();
                    this.textBoxTelProf.Clear();
                    this.richTextBoxComProf.Clear();
                    Hide();
                }
                else
                    MessageBox.Show("El propietario con DNI " + textBoxDniProf.Text + " ya existe", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de datos:" + System.Environment.NewLine + "El campo " + ex.Message, "Error");
            }
        }
    }
}
