using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Capa_Entidad;
using Capa_Negocio;


namespace Presentacion1.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page
    {
        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
        readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionDB"].ConnectionString);

        void CargarCB()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select NombrePrivilegio from Privilegios", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbPrivilegio.Items.Add(dr["NombrePrivilegio"].ToString());
            }
            con.Close();
        }

        #region CRUD (Create, Read, Update, Delete)
        public int IdUsuario;
        #region CREATE
        private void Crear(object sender, RoutedEventArgs e)
        {
            if (tbNombres.Text==""||tbApellidos.Text==""||tbDUI.Text==""||tbNIT.Text==""||tbCorreo.Text==""||tbTelefono.Text==""||tbFecha.Text==""||cbPrivilegio.Text==""||tbUsuario.Text==""||tbContrasenia.Text=="")
            {
                MessageBox.Show("Ninguno de los campos puede quedar vacio!\nPor favor complete los faltantes.");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select IdPrivilegio from Privilegios where NombrePrivilegio='"+cbPrivilegio.Text+"'",con);
                object valor = cmd.ExecuteScalar();
                int privilegio  = (int)valor;
                string patron = "123456";
                if (imagensubida == true)
                {
                    SqlCommand com = new SqlCommand("insert into Usuarios (nombres, apellidos, DUI, NIT, fecha_nac, telefono, correo, privilegio, img, usuario, contrasenia) values(@nombres, @apellidos, @DUI, @NIT, @fecha_nac, @telefono, @correo, @privilegio, @img, @usuario, (EncryptByPassPhrase('" + patron + "','" + tbContrasenia.Text + "')))", con);
                    com.Parameters.Add("@nombres", SqlDbType.VarChar).Value = tbNombres.Text;
                    com.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = tbApellidos.Text;
                    com.Parameters.Add("@DUI", SqlDbType.Int).Value = int.Parse(tbDUI.Text);
                    com.Parameters.Add("@NIT", SqlDbType.Int).Value = int.Parse(tbNIT.Text);
                    com.Parameters.Add("@fecha_nac", SqlDbType.Date).Value = tbFecha.Text;
                    com.Parameters.Add("@telefono", SqlDbType.Int).Value = int.Parse(tbTelefono.Text);
                    com.Parameters.Add("@correo", SqlDbType.VarChar).Value = tbCorreo.Text;
                    com.Parameters.Add("@privilegio", SqlDbType.Int).Value = privilegio;
                    com.Parameters.Add("@usuario", SqlDbType.VarChar).Value = tbUsuario.Text;
                    com.Parameters.AddWithValue("@img", SqlDbType.VarBinary).Value = data;
                    com.ExecuteNonQuery();
                    Content = new Usuarios();
                }
                else
                {
                    MessageBox.Show("Debe agregar una foto de perfil para el usuario!");
                }
                con.Close();
            }
        }
        #endregion
        #region READ
        public void Consultar() 
        {
            con.Open();
            SqlCommand com = new SqlCommand("select * from Usuarios inner join Privilegios on Usuarios.Privilegio=Privilegios.IdPrivilegio where IdUsuario="+IdUsuario,con);
            SqlDataReader dr = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            dr.Read();
            this.tbNombres.Text = dr["Nombres"].ToString();
            this.tbApellidos.Text = dr["Apellidos"].ToString();
            this.tbDUI.Text = dr["DUI"].ToString();
            this.tbNIT.Text = dr["NIT"].ToString();
            this.tbFecha.Text = dr["Fecha_nac"].ToString();
            this.tbTelefono.Text = dr["Telefono"].ToString();
            this.tbCorreo.Text = dr["Correo"].ToString();
            this.cbPrivilegio.SelectedItem = dr["NombrePrivilegio"];
            this.tbUsuario.Text = dr["usuario"].ToString();
            dr.Close();

            //IMAGEN
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select img from usuarios where IdUsuario='"+IdUsuario+"'",con);
            da.Fill(ds);
            byte[] data = (byte[])ds.Tables[0].Rows[0][0];
            MemoryStream strm = new MemoryStream();
            strm.Write(data, 0, data.Length);
            strm.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            imagen.Source = bi;
            //IMAGEN
            con.Close();
        }
        #endregion
        #region UPDATE
        private void Actualizar(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand("Select IdPrivilegio from Privilegios where NombrePrivilegio='"+cbPrivilegio.Text+"'",con);
            object valor = com.ExecuteScalar();
            int privilegio = (int)valor;
            string patron = "123456";
            if (tbNombres.Text == "" || tbApellidos.Text == "" || tbDUI.Text == "" || tbNIT.Text == "" || tbCorreo.Text == "" || tbTelefono.Text == "" || tbFecha.Text == "" || cbPrivilegio.Text == "" || tbUsuario.Text == "")
            {
                MessageBox.Show("Ninguno de los campos puede quedar vacio!\nPor favor complete los faltantes.");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Update Usuarios set Nombres='" + tbNombres.Text + "', Apellidos='" + tbApellidos.Text + "', DUI=" + int.Parse(tbDUI.Text) + ", NIT=" + float.Parse(tbNIT.Text) + ", Fecha_nac='" +tbFecha.Text + "', Telefono=" + int.Parse(tbTelefono.Text) + ", Correo='" + tbCorreo.Text + "', Privilegio='" + privilegio + "', Usuario='" + tbUsuario.Text + "' where IdUsuario="+IdUsuario+"", con);
                cmd.ExecuteNonQuery();
                if (imagensubida == true)
                {
                    SqlCommand img = new SqlCommand("Update Usuarios set img=@img where IdUsuario=" + IdUsuario + "", con);
                    img.Parameters.AddWithValue("@img", SqlDbType.VarBinary).Value = data;
                    img.ExecuteNonQuery();
                }
            }
            if (tbContrasenia.Text!="")
            {
                SqlCommand cmd = new SqlCommand("Update Usuarios set Contrasenia=(EncryptByPassPhrase('"+patron+"','"+tbContrasenia.Text+"')) where IdUsuario="+IdUsuario+"",con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            Content = new Usuarios();
        }
        #endregion
        #region DELETE
        private void Eliminar(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Usuarios where IdUsuario="+IdUsuario+"",con);
            cmd.ExecuteNonQuery();
            con.Close();
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
