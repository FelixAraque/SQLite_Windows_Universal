using SQLite_Windows_Universal.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace SQLite_Windows_Universal.Connection
{
    class Conexion
    {
        SQLite.SQLiteConnection conn;
        string path;

        /// <summary>
        /// Comprueba si existe la bbdd en el espacio de nombres.
        /// </summary>
        /// <param name="nombreBD">Nombre de la BD</param>
        /// <returns>Retorna true o false, dependiendo si esta la bbdd o no</returns>

        public async Task<bool> compruebaSiExisteBD(string nombreBD)
        {
            bool bdExiste = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(nombreBD);
            }
            catch (Exception)
            {
                bdExiste = false;
            }

            return bdExiste;
        }

        public void createDataBase()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

            conn = new SQLite.SQLiteConnection(path);

            conn.CreateTable<Empleado>();
            conn.Close();
        }

        //public void añadirPrimerosEmpleados()
        //{
        //    var listaEmpleados = new List<Empleado>()
        //    {
        //        new Empleado()
        //        {
        //            Nombre = "Antonio",
        //            NHoras = 1,
        //            Teletrabajo = true,
        //            Dieta = 5,
        //            Notas = "Nota1"
        //        },

        //        new Empleado()
        //        {
        //            Nombre = "Juan",
        //            NHoras = 2,
        //            Teletrabajo = false,
        //            Dieta = 3,
        //            Notas = "Nota2"
        //        },
        //    };

        //    path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

        //    conn = new SQLite.SQLiteConnection(path);

        //    conn.InsertAll(listaEmpleados);
        //    conn.Close();
        //}

        public List<Empleado> leerEmpleado()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

            conn = new SQLite.SQLiteConnection(path);

            var query = conn.Table<Empleado>();
            listaEmpleados = query.ToList<Empleado>();

            conn.Close();

            return listaEmpleados;
        }

        public void añadirEmpleado(string nombre, int nHoras, bool teletrabajo, int dieta, string notas)
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

            conn = new SQLite.SQLiteConnection(path);

            Empleado empleado = new Empleado();
            empleado.Nombre = nombre;
            empleado.NHoras = nHoras;
            empleado.Teletrabajo = teletrabajo;
            empleado.Dieta = dieta;
            empleado.Notas = notas;

            conn.Insert(empleado);
            conn.Close();
        }

        public void modificarEmpleado(int id, string nombre, int nHoras, bool teletrabajo, int dieta, string notas)
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

            conn = new SQLite.SQLiteConnection(path);

            var empleado = conn.Table<Empleado>().Where(x => x.Id == id).FirstOrDefault();

            if (empleado != null)
            {
                empleado.Nombre = nombre;
                empleado.NHoras = nHoras;
                empleado.Teletrabajo = teletrabajo;
                empleado.Dieta = dieta;
                empleado.Notas = notas;

                conn.Update(empleado);
            }

            conn.Close();
        }

        public void borrarEmpleado(int id)
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "BBDD_SQLite.db");

            conn = new SQLite.SQLiteConnection(path);

            var empleado = conn.Table<Empleado>().Where(x => x.Id == id).FirstOrDefault();


            conn.Delete(empleado);

            conn.Close();
        }
    }
}
