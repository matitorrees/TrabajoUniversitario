using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class Dato
    {
        static string ip;
        static string puerto;
        static MySqlConnection con;


        //public static string IP
        //{
        //    get { return ip; }
        //    set { ip = value; }
        //}
        //public static string Puerto
        //{
        //    get { return puerto; }
        //    set { puerto = value; }
        //}
        //public static bool TryConnect(string ipp, string pp)
        //{

        //    try {
        //        if (ipp == null)
        //        {
        //            var streamReader = new StreamReader("ipport");

        //            string line;
        //            line = streamReader.ReadLine();
        //            puerto = line;
        //            line = streamReader.ReadLine();
        //            ip = line;
        //            streamReader.Close();
        //        }else
        //        {
        //            puerto = pp;

        //            ip = ipp;
        //        }


        //        con = new MySqlConnection("server = " + ip + ";port=" + puerto + "; user id = club; persistsecurityinfo = True; database = club;password=club1234");
        //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM actividades", con);
        //        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        adp.Fill(ds);

        //        return true;
        //    }
        //    catch(Exception e)
        //    {
        //        return false;
        //    }

        //}
        public Dato()
        {
            string ip = "localhost - root";
            string puerto = "3308";
            //con = new MySqlConnection("server = " + ip + "   ;port=" + puerto + "; user id = club; persistsecurityinfo = True; database = inmobiliaria;password=club1234");
            //con = new MySqlConnection("server = " + ip + "   ;port=" + puerto + "; user id = club; persistsecurityinfo = True; database = inmobiliaria;password=club1234");
            con = new MySqlConnection("server = 190.176.204.54 ; port=" + puerto + "; user id = MatI; persistsecurityinfo = True; database = inmobiliaria;password=41449347oO$");
        }

        public bool ActualizarInquilino(List<string> a)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO inquilino (idinquilino, nombre, dni, telefono, celular, mail, comentarios) VALUES(\"" + a[0] + "\", \"" + a[1] + "\", \"" + a[2] + "\", \"" + a[3] + "\", \"" + a[4] + "\", \"" + a[5] + "\", \"" + a[6] + "\") ON DUPLICATE KEY UPDATE dni=\"" + a[2] + "\", idinquilino=\"" + a[0] + "\", nombre=\"" + a[1] + "\", telefono=\"" + a[3] + "\", celular=\"" + a[4] + "\", mail=\"" + a[5] + "\", comentarios=\"" + a[6] + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }


        public bool GuardarInquilino(List<string> a)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO inquilino (idinquilino, nombre, dni, telefono, celular, mail, comentarios) VALUES(\"" + a[0] + "\", \"" + a[1] + "\", \"" + a[2] + "\", \"" + a[3] + "\", \"" + a[4] + "\", \"" + a[5] + "\", \"" + a[6] + "\")", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }

        public bool RemoverInquilino(string a)
        {
            MySqlCommand cmd = new MySqlCommand("delete from inquilino where dni=\"" + a + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();

            return true;
        }



        public bool GuardarPropietario(List<string> a)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO propietario (idpropietario, nombre, dni, telefono, celular, mail, comentarios) VALUES(\"" + a[0] + "\", \"" + a[1] + "\", \"" + a[2] + "\", \"" + a[3] + "\", \"" + a[4] + "\", \"" + a[5] + "\", \"" + a[6] + "\")", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }

        
        public bool ActualizarPropietario(List<string> a)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO propietario (idpropietario, nombre, dni, telefono, celular, mail, comentarios) VALUES(\"" + a[0] + "\", \"" + a[1] + "\", \"" + a[2] + "\", \"" + a[3] + "\", \"" + a[4] + "\", \"" + a[5] + "\", \"" + a[6] + "\") ON DUPLICATE KEY UPDATE dni=\"" + a[2] + "\", idpropietario=\"" + a[0] + "\", nombre=\"" + a[1] + "\", telefono=\"" + a[3] + "\", celular=\"" + a[4] + "\", mail=\"" + a[5] + "\", comentarios=\"" + a[6] + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }

        public bool RemoverPropietario(string a)
        {
            MySqlCommand cmd = new MySqlCommand("delete from propietario where dni=\"" + a + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();

            return true;
        }

        public DataSet TraerInquilinos()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM inquilino", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();

            return ds;
        }

        public DataSet TraerPropietarios()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM propietario", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();

            return ds;
        }


        public bool GuardarPropiedad(List<string> a)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO propiedad (idpropiedad, dirección, zona, provincia, estado, tipo, ambientes, precio, nroPropietario) VALUES(\"" + a[0] + "\", \"" + a[1] + "\", \"" + a[2] + "\", \"" + a[3] + "\", \"" + a[4] + "\", \"" + a[5] + "\", \"" + a[6] + "\", \"" + a[7] + "\", \"" + a[8] + "\")", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }


        public DataSet TraerPropiedades()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM propiedad", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();

            return ds;
        }

        public bool RemoverPropiedad(string a)
        {
            MySqlCommand cmd = new MySqlCommand("delete from propiedad where idpropiedad=\"" + a + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();

            return true;
        }



        public bool GuardarAdmin(string a, string b, string c)
        {
            string s = "1";
            MySqlCommand cmd = new MySqlCommand("INSERT INTO administradora (idadministradora, nroPropietario, nroPropiedad, nroInquilino) VALUES(\"" + s + "\", \"" + a + "\", \"" + b + "\", \"" + c + "\") ON DUPLICATE KEY UPDATE idadministradora=\"" + s + "\", nroPropietario=\"" + a + "\", nroPropiedad=\"" + b + "\", nroInquilino=\"" + c + "\"", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            //con.Close();
            return true;
        }
       

        public DataSet ObtenerAdmin()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM administradora", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            Console.WriteLine("ACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            return ds;
        }
    }
}
