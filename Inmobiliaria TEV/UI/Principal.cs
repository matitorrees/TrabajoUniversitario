using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Negocios;

namespace UI
{
    public partial class Principal : Form
    {
        protected Administradora admin;
        public event EliminarDeAdminYBase EliminarPropietario;
        public event EliminarDeAdminYBase EliminarInquilino;
        public Principal()
        {
            admin = new Administradora();
            
            InitializeComponent();
            
            botonRestaurar.Visible = false;
            this.panelPropietario.BringToFront();
            this.controlPrincipalPropietarios1.BringToFront();
            

            this.agregarPropietario1.PropietarioCreado += new GuardarPropietario(admin.AgregarPropietario);
            this.agregarPropietario1.ActualizarControlPropietario += new GuardarPropietario(this.controlPrincipalPropietarios1.ActualizarListaPropietarios);
            this.modificarPropietario1.ModPropietario += new ModifPropietario(admin.ModificarPropietario);
            this.modificarPropietario1.ActualizarControlPropietario += new GuardarPropietario(this.controlPrincipalPropietarios1.ActualizarListaPropietarios);
            this.modificarPropietario1.Sacatelo += new SacateItem(this.controlPrincipalPropietarios1.SacateItem2);
            EliminarPropietario += new EliminarDeAdminYBase(admin.EliminarPropietario);
            EliminarInquilino += new EliminarDeAdminYBase(admin.EliminarInquilino);

            this.agregarPropiedad1.ObtenerProp += new ObtenerPropietarios(admin.ObtenerPropieta);
            this.agregarPropiedad1.PropiedadCreada += new GuardarPropiedad(admin.AgregarPropiedad);
            this.agregarPropiedad1.ActualizarControlPropiedad += new GuardarPropiedad(this.controlPrincipalPropiedades1.ActualizarListaPropiedades);
            this.modificarPropiedad1.ModifiPropied += new ModifPropiedad(admin.ModificarPropiedad);
            this.modificarPropiedad1.Sacatelo += new SacateItem(this.controlPrincipalPropiedades1.SacateItem2);
            this.modificarPropiedad1.ActualizarControlPropiedad += new GuardarPropiedad(this.controlPrincipalPropiedades1.ActualizarListaPropiedades);

            this.agregarInquilino1.InquilinoCreado += new GuardarInquilino(admin.AgregarInquilino);
            this.agregarInquilino1.ActualizarControlInquilino += new GuardarInquilino(this.controlPrincipalInquilino1.ActualizarListaInquilinos);
            this.modificarInquilino1.ActualizarControlInquilino += new GuardarInquilino(this.controlPrincipalInquilino1.ActualizarListaInquilinos);
            this.modificarInquilino1.ModInquilino += new ModifInquilino(admin.ModificarInquilino);
            this.modificarInquilino1.Sacatelo += new SacateItem(this.controlPrincipalInquilino1.SacateItem2);///// sacate item dos?

            this.agregarAlquiler1.ObtenerPropied += new ObtenerPropiedades(admin.ObtenerPropieda);
            this.agregarAlquiler1.ObtenerInqui += new ObtenerInquilinos(admin.ObtenerInquilino);


            //Datos
            admin.Inicializar();
            this.controlPrincipalPropietarios1.ObtenerProp += new ObtenerPropietarios(admin.ObtenerPropieta);
            this.controlPrincipalPropiedades1.ObtenerPropied += new ObtenerPropiedades(admin.ObtenerPropieda);
            this.controlPrincipalInquilino1.ObtenerInq += new ObtenerInquilinos(admin.ObtenerInquilino);

        }

        private void Button1_Click(object sender, EventArgs e) //AgregarPropietario
        {
            this.agregarPropietario1.Show();
            this.agregarPropietario1.BringToFront();
        }

        private void Button6_Click(object sender, EventArgs e) //ModificarPropietario
        {
            ListView l = this.controlPrincipalPropietarios1.DameListView();
            ListViewItem b = null;
            this.controlPrincipalPropietarios1.BringToFront();

            foreach (ListViewItem a in l.SelectedItems)
            {
                String nroPropietario = a.ToString();
                nroPropietario = nroPropietario.Trim('{', '}');
                nroPropietario = nroPropietario.Substring(15);
                b = a;
                this.modificarPropietario1.CargarTextBox(admin.DamePropietario(nroPropietario));
                this.modificarPropietario1.Show();
                this.modificarPropietario1.BringToFront();
            }

            if (b == null)
                MessageBox.Show("Selecciona el Propietario que desea modificar y luego oprima Modificar en la barra lateral", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button7_Click(object sender, EventArgs e) //EliminarPropietario
        {
            ListView l = this.controlPrincipalPropietarios1.DameListView();
            ListViewItem b = null;
            this.controlPrincipalPropietarios1.BringToFront();


            foreach (ListViewItem a in l.SelectedItems)
            {
                String nroPropietario = a.ToString();
                nroPropietario = nroPropietario.Trim('{', '}');
                nroPropietario = nroPropietario.Substring(15);   
                b = a;
                if (EliminarPropietario(nroPropietario)) //Si tiene propiedades que no se pueda borrar
                {
                    this.controlPrincipalPropietarios1.SacateItem(a);
                    MessageBox.Show("El Propietario numero " + nroPropietario + " se eliminó", null, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("El Propietario numero " + nroPropietario + " no se eliminó debido a que posee propiedades dadas de alta. Debe eliminar las propiedades del mismo para poder eliminarlo", null, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }

            if (b == null)
                MessageBox.Show("Selecciona el Propietario que desea eliminar y luego oprima Eliminar en la barra lateral", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void button5_Click(object sender, EventArgs e) //EliminarPropiedad
        {
            //HAY QUE COPIAR EL ELIMINAR PROPIETARIO. EN ESTO HAY QUE VALIDAR QUE LA PROPIEDAD NO TENGA NINGUN CONTRATO VIGENTE.
        }

        private void button9_Click(object sender, EventArgs e) //ModificarPropiedad
        {
            ListView l = this.controlPrincipalPropiedades1.DameListView();
            ListViewItem b = null;
            this.controlPrincipalPropiedades1.BringToFront();

            foreach (ListViewItem a in l.SelectedItems)
            {
                String nroPropiedad = a.ToString();
                nroPropiedad = nroPropiedad.Trim('{', '}');
                nroPropiedad = nroPropiedad.Substring(15);
                b = a;
                this.modificarPropiedad1.CargarTextBox(admin.DamePropiedad(nroPropiedad));
                this.modificarPropiedad1.Show();
                this.modificarPropiedad1.BringToFront();
            }

            if (b == null)
                MessageBox.Show("Selecciona el Propietario que desea modificar y luego oprima Modificar en la barra lateral", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button10_Click(object sender, EventArgs e) //AgregarPropiedad
        {
            this.agregarPropiedad1.Show();
            this.agregarPropiedad1.BringToFront();
        }


        private void button15_Click(object sender, EventArgs e) //Asignar propiedatario a propiedad
        {
          //(HAY QUE COPIAR EL CODIGO Y LA PARTE DE LA INTERFAZ DE AGREGAR PROPIEDAD DONDE SE PUEDE ASIGNAR UN PROPIETARIO)
        }



        private void button17_Click(object sender, EventArgs e)// Agregar inquilino
        {
            this.agregarInquilino1.Show();
            this.agregarInquilino1.BringToFront();
        }

        private void button16_Click(object sender, EventArgs e)// Modificar inquilino
        {
            ListView l = this.controlPrincipalInquilino1.DameListView();
            ListViewItem b = null;
            this.controlPrincipalInquilino1.BringToFront();

            foreach (ListViewItem a in l.SelectedItems)
            {
                String nroPropietario = a.ToString();
                nroPropietario = nroPropietario.Trim('{', '}');
                nroPropietario = nroPropietario.Substring(15);
                b = a;
                this.modificarInquilino1.CargarTextBox(admin.DameInquilino(nroPropietario));
                this.modificarInquilino1.Show();
                this.modificarInquilino1.BringToFront();
            }

            if (b == null)
                MessageBox.Show("Selecciona el inquilino que desea modificar y luego oprima Modificar en la barra lateral", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button14_Click(object sender, EventArgs e)// Eliminar inquilino
        {


            ListView l = this.controlPrincipalInquilino1.DameListView();
            ListViewItem b = null;
            this.controlPrincipalInquilino1.BringToFront();


            foreach (ListViewItem a in l.SelectedItems)
            {
                String nroPropietario = a.ToString();
                nroPropietario = nroPropietario.Trim('{', '}');
                nroPropietario = nroPropietario.Substring(15);
                b = a;
                if (EliminarInquilino(nroPropietario))// Si tiene propiedades que no se pueda borrar
                {
                    this.controlPrincipalInquilino1.SacateItem(a);
                    MessageBox.Show("El inquilino numero " + nroPropietario + " se eliminó", null, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("El inquilino numero " + nroPropietario + " no se eliminó debido a que posee propiedades dadas de alta. Debe eliminar las propiedades del mismo para poder eliminarlo", null, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }

            if (b == null)
                MessageBox.Show("Selecciona el inquilino que desea eliminar y luego oprima Eliminar en la barra lateral", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void button21_Click(object sender, EventArgs e)
        {
            this.controlCuotas1.Show();
            this.controlCuotas1.BringToFront();

        }

        private void buttonAgregarAlquiler_Click(object sender, EventArgs e) //AgregarAlquiler
        {
            this.agregarAlquiler1.Show();
            this.agregarAlquiler1.BringToFront();
        }





        /////// BOTONES PARA CONTROLAR LOS PANELES

        private void button8_Click(object sender, EventArgs e) //PROPIETARIOS
        {
            this.controlPrincipalPropietarios1.Show();
            this.controlPrincipalPropietarios1.BringToFront();
            this.panelPropietario.Show();
            this.panelPropietario.BringToFront();
        }


        private void button4_Click(object sender, EventArgs e) //PROPIEDADES
        {
            this.controlPrincipalPropiedades1.Show();
            this.controlPrincipalPropiedades1.BringToFront();
            this.panelPropiedad.Show();
            this.panelPropiedad.BringToFront();
        }


        private void button3_Click(object sender, EventArgs e) // INQUILINOS
        {
            this.controlPrincipalInquilino1.Show();
            this.controlPrincipalInquilino1.BringToFront();
            this.panelInquilinos.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e) //CONTRATOS
        {
            this.panelContratos.Show();
            this.panelContratos.BringToFront();
        }

        

        private void button18_Click_1(object sender, EventArgs e) //PAGOS
        {
            this.panelPagos.Show();
            this.panelPagos.BringToFront();
            this.controlPrincipalInquilino1.Show();
            this.controlPrincipalInquilino1.BringToFront();

        }


        private void BotonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BotonMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            botonMaximizar.Visible = false;
            botonRestaurar.Visible = true;
        }

        private void BotonRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            botonMaximizar.Visible = true;
            botonRestaurar.Visible = false;
        }

        private void BotonMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
 
    }
}
