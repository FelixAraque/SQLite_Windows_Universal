using SQLite_Windows_Universal.Connection;
using SQLite_Windows_Universal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SQLite_Windows_Universal.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ICommand ComandoNuevo { get; set; }
        public ICommand ComandoGuardar { get; set; }
        public ICommand ComandoActualizar { get; set; }
        public ICommand ComandoEliminar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private Conexion conexion;
        private List<Empleado> listaEmpleados;
        private Empleado empleadoSeleccionado;
        private string nombre;
        private int nHoras;
        private bool teletrabajo;
        private int dieta;
        private string notas;
        private bool txtNombre;
        private bool txtEmpleado;
        private bool btnActualizar;
        private bool btnGuardar;
        private bool btnEliminar;
        private bool btnNuevo;
        private int lbEmpleados;

        public MainViewModel()
        {
            conexion = new Conexion();
            comprobarExistenciaBD();
            listaEmpleados = conexion.leerEmpleado();
            estadoPrincipalControles();
            añadirAccionesBtn();
        }

        private void añadirAccionesBtn()
        {
            ComandoNuevo = new Command(accionNuevo);
            ComandoGuardar = new Command(accionGuardar);
            ComandoActualizar = new Command(accionActualizar);
            ComandoEliminar = new Command(accionEliminar);
        }

        private void estadoPrincipalControles()
        {
            BtnActualizar = false;
            BtnEliminar = false;
            BtnNuevo = true;
            BtnActualizar = false;
            LbEmpleados = -1;
            TxtNombre = false;
            TxtEmpleado = false;
            Nombre = "";
            NHoras = 0;
            Teletrabajo = false;
            Dieta = 0;
            Notas = "";
        }

        private void accionNuevo(object obj)
        {
            TxtNombre = true;
            TxtEmpleado = true;
            BtnGuardar = true;
            BtnEliminar = false;
            BtnActualizar = false;
            Nombre = "";
            NHoras = 0;
            Teletrabajo = false;
            Dieta = 0;
            Notas = "";

        }

        private void accionActualizar(object obj)
        {
            try
            {
                conexion.modificarEmpleado(empleadoSeleccionado.Id, Nombre, NHoras, Teletrabajo, Dieta, Notas);
                showMessageContentDialog("El empleado " + Nombre + " ha sido modificado.");
                ListaEmpleados = conexion.leerEmpleado();
            }
            catch (Exception e)
            {
                showMessageContentDialog("No se ha podido actualizar.");
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                estadoPrincipalControles();
            }
        }

        private void accionEliminar(object obj)
        {
            try
            {
                if (EmpleadoSeleccionado != null)
                {
                    conexion.borrarEmpleado(EmpleadoSeleccionado.Id);
                    showMessageContentDialog("El empleado " + empleadoSeleccionado.Nombre + " ha sido eliminado.");
                    ListaEmpleados = conexion.leerEmpleado();

                }
            }
            catch (Exception e)
            {
                showMessageContentDialog("No se ha podido eliminar el empleado " + EmpleadoSeleccionado.Nombre);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                estadoPrincipalControles();
            }
        }

        private void accionGuardar(object obj)
        {
            try
            {
                if (Nombre != "" || NHoras != 0 || Dieta != 0 || Notas != "")
                {
                    conexion.añadirEmpleado(Nombre, NHoras, Teletrabajo, Dieta, Notas);
                    showMessageContentDialog("Se añadió el empleado " + Nombre);
                    BtnGuardar = false;
                }
                else
                {
                    showMessageContentDialog("Rellena todos los campos");
                }
            }
            catch (Exception e)
            {
                showMessageContentDialog("No se pudo añadir el empleado");
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                ListaEmpleados = conexion.leerEmpleado();
                estadoPrincipalControles();
            }
        }

        private async void showMessageContentDialog(string mensaje)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Atencion",
                Content = mensaje,
                CloseButtonText = "Ok"
            };

            await dialog.ShowAsync();
        }

        public bool TxtEmpleado
        {
            get { return txtEmpleado; }
            set
            {
                txtEmpleado = value;
                OnPropertyChanged("TxtEmpleado");
            }
        }

        public bool TxtNombre
        {
            get { return txtNombre; }
            set
            {
                txtNombre = value;
                OnPropertyChanged("TxtNombre");
            }
        }

        public int LbEmpleados
        {
            get { return lbEmpleados; }
            set
            {
                lbEmpleados = value;
                OnPropertyChanged("LbEmpleados");
            }
        }

        public bool BtnEliminar
        {
            get { return btnEliminar; }
            set
            {
                btnEliminar = value;
                OnPropertyChanged("BtnEliminar");
            }
        }

        public bool BtnNuevo
        {
            get { return btnNuevo; }
            set
            {
                btnNuevo = value;
                OnPropertyChanged("BtnNuevo");
            }
        }

        public bool BtnGuardar
        {
            get { return btnGuardar; }
            set
            {
                btnGuardar = value;
                OnPropertyChanged("BtnGuardar");
            }
        }

        public bool BtnActualizar
        {
            get { return btnActualizar; }
            set
            {
                btnActualizar = value;
                OnPropertyChanged("BtnActualizar");
            }
        }

        public List<Empleado> ListaEmpleados
        {
            get { return listaEmpleados; }
            set
            {
                listaEmpleados = value;
                OnPropertyChanged("ListaEmpleados");
            }
        }

        public Empleado EmpleadoSeleccionado
        {
            get { return empleadoSeleccionado; }
            set
            {
                empleadoSeleccionado = value;
                OnPropertyChanged("EmpleadoSeleccionado");

                if (empleadoSeleccionado != null)
                {
                    BtnEliminar = true;
                    BtnActualizar = true;
                    BtnGuardar = false;
                    BtnNuevo = true;
                    TxtEmpleado = true;
                    TxtNombre = true;
                    Nombre = empleadoSeleccionado.Nombre;
                    NHoras = empleadoSeleccionado.NHoras;
                    Teletrabajo = empleadoSeleccionado.Teletrabajo;
                    Dieta = empleadoSeleccionado.Dieta;
                    Notas = empleadoSeleccionado.Notas;

                }
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public int NHoras
        {
            get { return nHoras; }
            set
            {
                nHoras = value;
                OnPropertyChanged("NHoras");
            }
        }

        public bool Teletrabajo
        {
            get { return teletrabajo; }
            set
            {
                teletrabajo = value;
                OnPropertyChanged("Teletrabajo");
            }
        }

        public int Dieta
        {
            get { return dieta; }
            set
            {
                dieta = value;
                OnPropertyChanged("Dieta");
            }
        }

        public string Notas
        {
            get { return notas; }
            set
            {
                notas = value;
                OnPropertyChanged("Notas");
            }
        }

        private async void comprobarExistenciaBD()
        {
            bool dbExiste = await conexion.compruebaSiExisteBD("BBDD_SQLite.db");

            if (!dbExiste)
            {
                conexion.createDataBase();
                //conexion.añadirPrimerosEmpleados();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
