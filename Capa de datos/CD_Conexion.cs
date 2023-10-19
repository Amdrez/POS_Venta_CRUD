using System.Data.SqlClient;
using System.Data;

namespace Capa_de_datos
{
    public class CD_Conexion
    {
        private readonly SqlConnection con = new SqlConnection("Data Source = DESKTOP-U5HT1PC; initial catalog = AmdrezsStore; integrated security = true;");

        public SqlConnection AbrirConexion()
        {
            if (con.State==ConnectionState.Closed)
                con.Open();
            return con;
        }
        public SqlConnection CerrarConexion() 
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return con;
        }
    }
}
