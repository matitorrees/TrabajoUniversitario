using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data;

namespace Negocios
{

    public delegate List<Propietario> ObtenerPropietarios();
    public delegate int DameNumeroPropietario();
    public delegate Boolean GuardarPropietario(Propietario p);
    public delegate Boolean ModifPropietario(Propietario p, Propietario p1);
    public delegate void SacateItem(string m);
    public delegate Boolean GuardarInquilino(Inquilino p);
    public delegate Boolean EliminarDeAdminYBase(string m);
    public delegate Boolean ModifInquilino(Inquilino p, Inquilino p1);
    public delegate Boolean GuardarPropiedad(Propiedad p);
    public delegate List<Propiedad> ObtenerPropiedades();
    public delegate Boolean ModifPropiedad(Propiedad p, Propiedad p1);
    public delegate List<Inquilino> ObtenerInquilinos();


    public class ExceptionCustom : Exception
    {
        public ExceptionCustom(String msg) : base(msg)
        {

        }
    }

    public class Administradora
    {
        int nroPropietario = 0;
        int nroPropiedad = 0;
        int nroInquilino = 0;

        List<Propietario> listaPropietarios;
        List<Propiedad> listaPropiedades;
        List<Inquilino> listaInquilinos;

        public Administradora()
        {
            listaPropietarios = new List<Propietario>();
            listaPropiedades = new List<Propiedad>();
            listaInquilinos = new List<Inquilino>();
        }

        public int DameNumPropietario()
        {
            return nroPropietario;
        }

        public int DameNumPropiedad()
        {
            return nroPropiedad;
        }

        public int DameNumInquilino()
        {
            return nroInquilino;
        }

        public List<Propietario> ObtenerPropieta()
        {
            if (listaPropietarios.Count == 0)
                return null;
            return listaPropietarios;
        }

        public List<Propiedad> ObtenerPropieda()
        {
            if (listaPropiedades.Count == 0)
                return null;
            return listaPropiedades;
        }

        public List<Inquilino> ObtenerInquilino()
        {
            if (listaInquilinos.Count == 0)
                return null;
            return listaInquilinos;
        }

        public int DameNPropietario()
        {
            return nroPropietario;
        }

        public List<Propietario> ListaPropietarios
        {
            get
            {
                return listaPropietarios;
            }
            set
            {
                listaPropietarios = value;
            }
        }

        public List<Inquilino> ListaInquilinos
        {
            get
            {
                return listaInquilinos;
            }
            set
            {
                listaInquilinos = value;
            }
        }
        public List<Propiedad> ListaPropiedades
        {
            get
            {
                return listaPropiedades;
            }
            set
            {
                listaPropiedades = value;
            }
        }
        public Boolean AgregarInquilino(Inquilino p)
        {
            int i = 0;
            Dato d = new Dato();

            while (i < listaInquilinos.Count && listaInquilinos[i].Dni != p.Dni)
                i++;

            if (i == listaInquilinos.Count)
            {
                nroInquilino++;
                p.NroPropietario = nroInquilino;
                listaInquilinos.Add(p);
                List<string> a1 = new List<string>();
                a1.Add(p.NroPropietario.ToString());
                a1.Add(p.Nombre);
                a1.Add(p.Dni.ToString());
                a1.Add(p.Telefono.ToString());
                a1.Add(p.Celular.ToString());
                a1.Add(p.Mail);
                a1.Add(p.Comentarios);
                d.GuardarInquilino(a1);
                d.GuardarAdmin(nroPropietario.ToString(), nroPropiedad.ToString(), nroInquilino.ToString());
                return true;
            }
            else
                return false;
        }

        public Inquilino DameInquilino(String a)
        {
            int i = 0;

            while (i < listaInquilinos.Count && listaInquilinos[i].NroPropietario.ToString() != a)
                i++;

            return listaInquilinos[i];
        }

        public Boolean ModificarInquilino(Inquilino nuevaPropietario, Inquilino anteriorPropietario)
        {

            Dato d = new Dato();

            if (nuevaPropietario.ToString2().Equals(anteriorPropietario.ToString2()))
            {
                return true;
            }
            else
                if (nuevaPropietario.Dni.Equals(anteriorPropietario.Dni))
            {
                int i = 0;

                while (i < listaInquilinos.Count && listaInquilinos[i].Dni != nuevaPropietario.Dni)
                    i++;

                if (i < listaInquilinos.Count)
                {
                    listaInquilinos[i].Comentarios = nuevaPropietario.Comentarios;    //Aca cambio todo lo que pudo haber cambiado que no sea el dni claramente y luego guardo en base
                }
                List<string> a1 = new List<string>();
                a1.Add(nuevaPropietario.NroPropietario.ToString());
                a1.Add(nuevaPropietario.Nombre);
                a1.Add(nuevaPropietario.Dni.ToString());
                a1.Add(nuevaPropietario.Telefono.ToString());
                a1.Add(nuevaPropietario.Celular.ToString());
                a1.Add(nuevaPropietario.Mail);
                a1.Add(nuevaPropietario.Comentarios);
                d.ActualizarInquilino(a1);
                return true;
            }
            else
            {
                int j = 0;

                while (j < listaInquilinos.Count && listaInquilinos[j].Dni != nuevaPropietario.Dni)
                    j++;

                if (j < listaInquilinos.Count)
                {
                    return false; //Aca el dni ya existe
                }
                else
                {
                    listaInquilinos.Remove(anteriorPropietario); //y aca elnuevo dni no existe. Elimino la anterior y agrego la nueva
                    listaInquilinos.Add(nuevaPropietario);
                    d.RemoverPropietario(anteriorPropietario.Dni.ToString());
                    List<string> a1 = new List<string>();
                    a1.Add(nuevaPropietario.NroPropietario.ToString());
                    a1.Add(nuevaPropietario.Nombre);
                    a1.Add(nuevaPropietario.Dni.ToString());
                    a1.Add(nuevaPropietario.Telefono.ToString());
                    a1.Add(nuevaPropietario.Celular.ToString());
                    a1.Add(nuevaPropietario.Mail);
                    a1.Add(nuevaPropietario.Comentarios);
                    d.GuardarInquilino(a1);
                    return true;
                }
            }
        }

        public Boolean EliminarInquilino(string a)
        {
            Dato d = new Dato();
            int nroPropietario = int.Parse(a);
            int i = 0;

            while (i < listaInquilinos.Count && listaInquilinos[i].NroPropietario != nroPropietario)
                i++;

            if (i < listaInquilinos.Count)
            {
                d.RemoverInquilino(listaInquilinos[i].Dni.ToString());
                Inquilino p = listaInquilinos[i];
                listaInquilinos.Remove(p);

                return true;
            }
            else return false;
        }

        public Boolean AgregarPropietario(Propietario p)
        {
            int i = 0;
            Dato d = new Dato();

            while (i < listaPropietarios.Count && listaPropietarios[i].Dni != p.Dni)
                i++;

            if (i == listaPropietarios.Count)
            {
                nroPropietario++;
                p.NroPropietario = nroPropietario;
                listaPropietarios.Add(p);
                List<string> a1 = new List<string>();
                a1.Add(p.NroPropietario.ToString());
                a1.Add(p.Nombre);
                a1.Add(p.Dni.ToString());
                a1.Add(p.Telefono.ToString());
                a1.Add(p.Celular.ToString());
                a1.Add(p.Mail);
                a1.Add(p.Comentarios);
                d.GuardarPropietario(a1);
                d.GuardarAdmin(nroPropietario.ToString(), nroPropiedad.ToString(),nroInquilino.ToString());
                return true;
            }
            else
                return false;
        }


        public Boolean AgregarPropiedad(Propiedad p)
        {
            int i = 0;
            Dato d = new Dato();

            while (i < listaPropiedades.Count && listaPropiedades[i].ToString2() != p.ToString2())
                i++;

            if (i == listaPropiedades.Count)
            {
                nroPropiedad++;
                p.NroPropiedad = nroPropiedad;
                listaPropiedades.Add(p);
                List<string> a1 = new List<string>();
                a1.Add(p.NroPropiedad.ToString());
                a1.Add(p.Direccion);
                a1.Add(p.Zona);
                a1.Add(p.Provincia);
                a1.Add(p.Estado);
                a1.Add(p.Tipo);
                a1.Add(p.Ambientes.ToString());
                a1.Add(p.Precio.ToString());

                if (p.Propietario != null)
                    a1.Add(p.Propietario.NroPropietario.ToString());
                else
                    a1.Add("0");

                d.GuardarPropiedad(a1);
                d.GuardarAdmin(nroPropietario.ToString(), nroPropiedad.ToString(),nroInquilino.ToString());
                return true;
            }
            else
                return false;
        }


        public Propietario DamePropietario(String a)
        {
            int i = 0;

            while (i < listaPropietarios.Count && listaPropietarios[i].NroPropietario.ToString() != a)
                i++;

            return listaPropietarios[i];
        }

        public Propiedad DamePropiedad(String a)
        {
            int i = 0;

            while (i < listaPropiedades.Count && listaPropiedades[i].NroPropiedad.ToString() != a)
                i++;

            return listaPropiedades[i];
        }

        public Boolean ModificarPropietario(Propietario nuevaPropietario, Propietario anteriorPropietario)
        {

            Dato d = new Dato();

            if (nuevaPropietario.ToString2().Equals(anteriorPropietario.ToString2()))
            {
                return true;
            }
            else
                if (nuevaPropietario.Dni.Equals(anteriorPropietario.Dni))
            {
                int i = 0;

                while (i < listaPropietarios.Count && listaPropietarios[i].Dni != nuevaPropietario.Dni)
                    i++;

                if (i < listaPropietarios.Count)
                {
                    listaPropietarios[i].Nombre = nuevaPropietario.Nombre;
                    listaPropietarios[i].Telefono = nuevaPropietario.Telefono;
                    listaPropietarios[i].Celular = nuevaPropietario.Celular;
                    listaPropietarios[i].Mail = nuevaPropietario.Mail;
                    listaPropietarios[i].Comentarios = nuevaPropietario.Comentarios;    //Aca cambio todo lo que pudo haber cambiado que no sea el dni claramente y luego guardo en base
                }
                List<string> a1 = new List<string>();
                a1.Add(nuevaPropietario.NroPropietario.ToString());
                a1.Add(nuevaPropietario.Nombre);
                a1.Add(nuevaPropietario.Dni.ToString());
                a1.Add(nuevaPropietario.Telefono.ToString());
                a1.Add(nuevaPropietario.Celular.ToString());
                a1.Add(nuevaPropietario.Mail);
                a1.Add(nuevaPropietario.Comentarios);
                d.ActualizarPropietario(a1);
                return true;
            }
            else
            {
                int j = 0;

                while (j < listaPropietarios.Count && listaPropietarios[j].Dni != nuevaPropietario.Dni)
                    j++;

                if (j < listaPropietarios.Count)
                {
                    return false; //Aca el dni ya existe
                }
                else
                {
                    listaPropietarios.Remove(anteriorPropietario); //y aca elnuevo dni no existe. Elimino la anterior y agrego la nueva
                    listaPropietarios.Add(nuevaPropietario);
                    d.RemoverPropietario(anteriorPropietario.Dni.ToString());
                    List<string> a1 = new List<string>();
                    a1.Add(nuevaPropietario.NroPropietario.ToString());
                    a1.Add(nuevaPropietario.Nombre);
                    a1.Add(nuevaPropietario.Dni.ToString());
                    a1.Add(nuevaPropietario.Telefono.ToString());
                    a1.Add(nuevaPropietario.Celular.ToString());
                    a1.Add(nuevaPropietario.Mail);
                    a1.Add(nuevaPropietario.Comentarios);
                    d.GuardarPropietario(a1);
                    return true;
                }
            }
        }


        public Boolean ModificarPropiedad(Propiedad nuevaPropiedad, Propiedad anteriorPropiedad)
        {

            Dato d = new Dato();

            if (nuevaPropiedad.ToString2().Equals(anteriorPropiedad.ToString2()))
            {
                return true;
            }
            else
            {
                int i = 0;

                while (i < listaPropiedades.Count && listaPropiedades[i].ToString2() != nuevaPropiedad.ToString2())
                    i++;

                if (i == listaPropiedades.Count)
                {
                    listaPropiedades.Remove(anteriorPropiedad);
                    listaPropiedades.Add(nuevaPropiedad);
                    d.RemoverPropiedad(anteriorPropiedad.NroPropiedad.ToString());

                    List<string> a1 = new List<string>();
                    a1.Add(nuevaPropiedad.NroPropiedad.ToString());
                    a1.Add(nuevaPropiedad.Direccion);
                    a1.Add(nuevaPropiedad.Zona);
                    a1.Add(nuevaPropiedad.Provincia);
                    a1.Add(nuevaPropiedad.Estado);
                    a1.Add(nuevaPropiedad.Tipo);
                    a1.Add(nuevaPropiedad.Ambientes.ToString());
                    a1.Add(nuevaPropiedad.Precio.ToString());

                    if (nuevaPropiedad.Propietario != null)
                        a1.Add(nuevaPropiedad.Propietario.NroPropietario.ToString());
                    else
                        a1.Add("0");

                    d.GuardarPropiedad(a1);
                    return true;
                }
                else
                    return false;
            }
            
        }


        public Boolean EliminarPropietario(string a)
        {
            Dato d = new Dato();
            int nroPropietario = int.Parse(a);
            int i = 0;

            while (i < listaPropietarios.Count && listaPropietarios[i].NroPropietario != nroPropietario)
                i++;

            if (i < listaPropietarios.Count)
            {
                if (ListaPropietarios[i].Propiedades.Count > 0)
                {
                    return false;
                }
                else
                {
                    d.RemoverPropietario(listaPropietarios[i].Dni.ToString());
                    Propietario p = listaPropietarios[i];
                    listaPropietarios.Remove(p);
                    return true;
                }
            }
            return false;
        }

        public void Inicializar()
        {
            try
            {
                Dato d = new Dato();
                DataSet ds1 = d.TraerPropietarios();

                List<Propietario> l = new List<Propietario>();

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    Propietario p = new Propietario(int.Parse(ds1.Tables[0].Rows[i][0].ToString()), ds1.Tables[0].Rows[i][1].ToString(), long.Parse(ds1.Tables[0].Rows[i][2].ToString()), long.Parse(ds1.Tables[0].Rows[i][3].ToString()), long.Parse(ds1.Tables[0].Rows[i][4].ToString()), ds1.Tables[0].Rows[i][5].ToString(), ds1.Tables[0].Rows[i][6].ToString());
                    l.Add(p);
                }

                ListaPropietarios = l;

                DataSet ds3 = d.TraerPropiedades();

                List<Propiedad> l1 = new List<Propiedad>();

                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    Propiedad pr = new Propiedad(int.Parse(ds3.Tables[0].Rows[i][0].ToString()), ds3.Tables[0].Rows[i][1].ToString(), ds3.Tables[0].Rows[i][2].ToString(), ds3.Tables[0].Rows[i][3].ToString(), ds3.Tables[0].Rows[i][4].ToString(), ds3.Tables[0].Rows[i][5].ToString(), int.Parse(ds3.Tables[0].Rows[i][6].ToString()), double.Parse(ds3.Tables[0].Rows[i][7].ToString()));

                    if (ds3.Tables[0].Rows[i][8].ToString() != "0")
                    {
                        
                        DamePropietario(ds3.Tables[0].Rows[i][8].ToString()).Propiedades.Add(pr);
                        pr.Propietario = DamePropietario(ds3.Tables[0].Rows[i][8].ToString());

                    }

                    l1.Add(pr);
                }

                ListaPropiedades = l1;

                
                DataSet ds4 = d.TraerInquilinos();

                List<Inquilino> l3 = new List<Inquilino>();

                for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
                {
                    Inquilino p = new Inquilino(int.Parse(ds4.Tables[0].Rows[i][0].ToString()), ds4.Tables[0].Rows[i][1].ToString(), long.Parse(ds4.Tables[0].Rows[i][2].ToString()), long.Parse(ds4.Tables[0].Rows[i][3].ToString()), long.Parse(ds4.Tables[0].Rows[i][4].ToString()), ds4.Tables[0].Rows[i][5].ToString(), ds4.Tables[0].Rows[i][6].ToString());
                    l3.Add(p);
                }

                ListaInquilinos = l3;

                DataSet ds2 = d.ObtenerAdmin();
                nroPropietario = int.Parse(ds2.Tables[0].Rows[0][1].ToString());
                Console.WriteLine("ACAAAAAA1 "+nroPropietario.ToString());
                nroPropiedad = int.Parse(ds2.Tables[0].Rows[0][2].ToString());
                Console.WriteLine("ACAAAAAA2 "+nroPropiedad.ToString());
                nroInquilino = int.Parse(ds2.Tables[0].Rows[0][3].ToString());
                Console.WriteLine("ACAAAAAA3 "+nroInquilino.ToString());

            }
            catch(Exception ex)
            { }
        }

    }


    public abstract class Persona
    {
        private int nroPropietario;
        private String nombre;
        private long telefono;
        private long dni;
        private long celular;
        private String comentarios;
        private String mail, tipo;
        public Persona(int n, String nom, long d, long tel, long c, String m, String co)
        {
            nroPropietario = n;
            nombre = nom;
            telefono = tel;
            dni = d;
            celular = c;
            comentarios = co;
            mail = m;
        }

        public int NroPropietario
        {
            get
            {
                return nroPropietario;
            }
            set
            {
                nroPropietario = value;
            }
        }


        public String Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public long Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }


        public long Celular
        {
            get
            {
                return celular;
            }
            set
            {
                celular = value;
            }
        }


        public long Dni
        {
            get
            {
                return dni;
            }
            set
            {
                dni = value;
            }
        }

        public String Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
            }
        }

        public String Comentarios
        {
            get
            {
                return comentarios;
            }
            set
            {
                comentarios = value;
            }
        }

    }


    public class Propietario : Persona
    {
        List<Propiedad> listaPropiedades;
        public Propietario(int n, String nom, long d, long tel, long c, String m, String co) : base(n, nom, d, tel, c, m, co)
        {
            listaPropiedades = new List<Propiedad>();
        }

        public List<Propiedad> Propiedades
        {
            get
            {
                return listaPropiedades;
            }

            set
            {
                listaPropiedades = value;
            }
        }

        public String ToString2()
        {
            return Nombre + Dni.ToString() + Telefono.ToString() + Celular.ToString() + Mail + Comentarios;
        }

        public String ToString3()
        {
            String t= Nombre + " - " + Dni;
            return t;
        }

    }

    public class Inquilino : Persona
    {
        List<Contrato> lisContratos;

        public Inquilino(int n, String nom, long d, long tel, long c, String m, String co) : base(n, nom, d, tel, c, m, co)
        {
            lisContratos = new List<Contrato>();
        }

        public String ToString2()
        {
            return Nombre + Dni.ToString() + Telefono.ToString() + Celular.ToString() + Mail + Comentarios;
        }

        public String ToString3()
        {
            String t= Nombre + " - " + Dni;
            return t;
        }
    }

    public class Comprador : Persona
    {
        public Comprador(int n, String nom, long d, long tel, long c, String m, String co) : base(n, nom, d, tel, c, m, co)
        {

        }
    }

    public class Propiedad
    {
        int nroPropiedad;
        String zona;
        String direccion;
        String provincia;
        String estado;
        String tipo;
        int ambientes;
        double precio;
        Propietario propietario;
        List<Contrato> listaContratos;

        public Propiedad(int n, String d, String z, String p, String e,String t, int a, double pre)
        {
           nroPropiedad=n;
           zona=z;
           direccion=d;
           provincia=p;
           estado = e;
           tipo=t;
           ambientes=a;
           precio = pre;
           propietario = null;
           listaContratos = new List<Contrato>();
        }

        public int NroPropiedad
        {
            get
            {
                return nroPropiedad;
            }
            set
            {
                nroPropiedad = value;
            }
        }

        public String Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }

        public String Zona
        {
            get
            {
                return zona;
            }
            set
            {
                zona = value;
            }
        }

        public String Provincia
        {
            get
            {
                return provincia;
            }
            set
            {
                provincia = value;
            }
        }

        public String Estado
        {
            get
            {
                return estado;
            }
            set
            {
                estado = value;
            }
        }

        public String Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public int Ambientes
        {
            get
            {
                return ambientes;
            }
            set
            {
                ambientes = value;
            }
        }

        public double Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
            }
        }

        public Propietario Propietario
        {
            get
            {
                return propietario;
            }
            set
            {
                propietario = value;
            }
        }

        public String ToString2()
        {
            return Direccion + Zona + Provincia + Estado + Tipo + Ambientes + Precio;        
        }

        public String ToString3()
        {
            String p = Direccion+" - "+Zona+" - "+Provincia+" - "+Tipo+" - "+Ambientes+" Ambientes - $"+Precio;
            return p;
        }
    }

    public class Contrato
    {
        int nroContrato;
        DateTime fechaInicio;
        Propiedad propiedad;
        Inquilino inquilino;
        List<Cuota> listaCuotas;
        public Contrato(int n, DateTime f)
        {
            nroContrato = n;
            fechaInicio = f;
            propiedad = null;
            inquilino = null;
            listaCuotas = new List<Cuota>();
        }


        public Inquilino Inquilino
        {
            get
            {
                return inquilino;
            }
            set
            {
                inquilino = value;
            }
        }

        public Propiedad Propiedad
        {
            get
            {
                return propiedad;
            }
            set
            {
                propiedad = value;
            }
        }

    }

    public class Cuota
    {
        int nroCuota;
        DateTime fechaEmision;
        DateTime fechaVencimiento;
        DateTime fechaPago;
        Boolean estado;

        public Cuota(int n, DateTime fe, DateTime fv, DateTime fp, Boolean e)
        {
            nroCuota = n;
            fechaEmision = fe;
            fechaVencimiento = fv;
            fechaPago = fp;
            estado = e;
        }
    }
}
