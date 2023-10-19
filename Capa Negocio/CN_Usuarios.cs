using System.Data;
using Capa_de_datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Usuarios
    {
        private readonly CD_Usuarios objDatos = new CD_Usuarios();

        //CRUD Usuarios
        #region Insertar

        public void Insertar(CE_Usuarios Usuarios)
        {
            objDatos.CD_Insertar(Usuarios);
        }

        #endregion

        #region Consultar

        public CE_Usuarios Consulta(int IdUsuario)
        {
            return objDatos.CD_Consulta(IdUsuario);
        }

        #endregion

        #region Eliminar

        public void Eliminar(CE_Usuarios Usuarios)
        {
            objDatos.CD_Eliminar(Usuarios);
        }

        #endregion

        #region Actualizar

        #region Actualizar Datos

        public void ActualizarDatos(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarDatos(Usuarios);
        }

        #endregion

        #region Actualizar Pass

        public void ActualizarPass(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarPass(Usuarios);
        }

        #endregion

        #region Actualizar Imagen

        public void ActualizarIMG(CE_Usuarios Usuarios)
        {
            objDatos.CD_ActualizarIMG(Usuarios);
        }

        #endregion

        #endregion

        //Vista Usuarios
        #region Cargar Usuarios

        public DataTable CargarUsuarios()
        {
            return objDatos.CagarUsuarios();
        }

        #endregion
    }
}
