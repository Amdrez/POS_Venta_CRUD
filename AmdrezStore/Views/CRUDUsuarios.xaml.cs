using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;

namespace AmdrezStore.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page
    {
        readonly CN_Usuarios objeto_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios objeto_CE_Usuarios = new CE_Usuarios();
        readonly CN_Privilegios objeto_CN_Privilegios = new CN_Privilegios();

        #region Inicial

        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }

        #endregion

        #region Regresar

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }

        #endregion

        #region CargarPrivilegios

        void CargarCB()
        {
            List<string> privilegios = objeto_CN_Privilegios.ListarPrivilegios();
            for (int i=0; i<privilegios.Count; i++)
            {
                cbPrivilegio.Items.Add(privilegios[i]);
            }
        }

        #endregion

        #region ValidarCamposVacios

        public bool CamposLlenos() 
        {
            if (tbNombres.Text == "" || tbApellidos.Text == "" || tbDUI.Text == "" || tbNIT.Text == "" || tbCorreo.Text == "" || tbTelefono.Text == "" || tbFecha.Text == "" || cbPrivilegio.Text == "" || tbUsuario.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        #endregion

        #region CRUD (Create, Read, Update, Delete)

        public int IdUsuario;
        public string Patron = "123456";

        #region CREATE

        private void Crear(object sender, RoutedEventArgs e)
        {
            if (CamposLlenos() == true && tbContrasenia.Text != "")
            {
                int privilegio = objeto_CN_Privilegios.IdPrivilegio(cbPrivilegio.Text);

                objeto_CE_Usuarios.Nombres = tbNombres.Text;
                objeto_CE_Usuarios.Apellidos = tbApellidos.Text;
                objeto_CE_Usuarios.DUI = int.Parse(tbDUI.Text);
                objeto_CE_Usuarios.NIT = int.Parse(tbNIT.Text);
                objeto_CE_Usuarios.Correo = tbCorreo.Text;
                objeto_CE_Usuarios.Telefono = int.Parse(tbTelefono.Text);
                objeto_CE_Usuarios.Fecha_Nac = DateTime.Parse(tbFecha.Text);
                objeto_CE_Usuarios.Privilegio = privilegio;
                objeto_CE_Usuarios.Img = data;
                objeto_CE_Usuarios.Usuario = tbUsuario.Text;
                objeto_CE_Usuarios.Contrasenia = tbContrasenia.Text;
                objeto_CE_Usuarios.Patron = Patron;

                objeto_CN_Usuarios.Insertar(objeto_CE_Usuarios);

                Content = new Usuarios();
            }
            else
            {
                MessageBox.Show("Los campos no pueden quedar vacios!");
            }
        }
        
        #endregion
        #region READ
        public void Consultar() 
        {
            var a = objeto_CN_Usuarios.Consulta(IdUsuario);
            tbNombres.Text = a.Nombres.ToString();
            tbApellidos.Text = a.Apellidos.ToString();
            tbDUI.Text = a.DUI.ToString();
            tbNIT.Text = a.NIT.ToString();
            tbCorreo.Text = a.Correo.ToString();
            tbTelefono.Text = a.Telefono.ToString();
            tbFecha.Text = a.Fecha_Nac.ToString();

            var b = objeto_CN_Privilegios.NombrePrivilegio(a.Privilegio);
            cbPrivilegio.Text = b.NombrePrivilegio;

            ImageSourceConverter imgs = new ImageSourceConverter();
            imagen.Source = (ImageSource)imgs.ConvertFrom(a.Img);
            tbUsuario.Text = a.Usuario.ToString();
        }
        #endregion
        #region UPDATE
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            if (CamposLlenos()==true)
            {
                int privilegio = objeto_CN_Privilegios.IdPrivilegio(cbPrivilegio.Text);

                objeto_CE_Usuarios.IdUsuario = IdUsuario;
                objeto_CE_Usuarios.Nombres = tbNombres.Text;
                objeto_CE_Usuarios.Apellidos = tbApellidos.Text;
                objeto_CE_Usuarios.DUI = int.Parse(tbDUI.Text);
                objeto_CE_Usuarios.NIT = int.Parse(tbNIT.Text);
                objeto_CE_Usuarios.Correo = tbCorreo.Text;
                objeto_CE_Usuarios.Telefono = int.Parse(tbTelefono.Text);
                objeto_CE_Usuarios.Fecha_Nac = DateTime.Parse(tbFecha.Text);
                objeto_CE_Usuarios.Privilegio = privilegio;
                objeto_CE_Usuarios.Usuario = tbUsuario.Text;

                objeto_CN_Usuarios.ActualizarDatos(objeto_CE_Usuarios);

                Content = new Usuarios();
            }
            else
            {
                MessageBox.Show("Los campos no pueden quedar vacios!");
            }

            if (tbContrasenia.Text != "")
            {
                objeto_CE_Usuarios.IdUsuario = IdUsuario;
                objeto_CE_Usuarios.Contrasenia = tbContrasenia.Text;
                objeto_CE_Usuarios.Patron = Patron;

                objeto_CN_Usuarios.ActualizarPass(objeto_CE_Usuarios);
                Content = new Usuarios();
            }

            if (imagensubida==true) 
            {
                objeto_CE_Usuarios.IdUsuario = IdUsuario;
                objeto_CE_Usuarios.Img = data;

                objeto_CN_Usuarios.ActualizarIMG(objeto_CE_Usuarios);
                Content = new Usuarios();
            }
        }
        #endregion
        #region DELETE
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            objeto_CE_Usuarios.IdUsuario = IdUsuario;

            objeto_CN_Usuarios.Eliminar(objeto_CE_Usuarios);

            Content = new Usuarios();
        }
        #endregion
        #endregion

        #region Imagen

        byte[] data;
        private bool imagensubida = false;
        private void Subir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                imagen.SetValue(Image.SourceProperty, imgs.ConvertFromString(ofd.FileName.ToString()));
            }
            imagensubida = true;
        }

        #endregion
    }
}
