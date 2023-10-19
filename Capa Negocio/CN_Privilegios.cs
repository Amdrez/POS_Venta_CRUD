using Capa_de_datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Privilegios
    {
        CD_Privilegios CD_Privilegios = new CD_Privilegios();

        #region IdPrivilegio

        public int IdPrivilegio(string NombrePrivilegio) 
        {
            return CD_Privilegios.IdPrivilegio(NombrePrivilegio);
        }

        #endregion

        #region NombrePrivilegio

        public CE_Privilegios NombrePrivilegio(int IdPrivilegio) 
        {
            return CD_Privilegios.NombrePrivilegio(IdPrivilegio);
        }

        #endregion

        #region Listar Privilegioa

        public List<string> ListarPrivilegios()
        {
            return CD_Privilegios.ObtenerPrivilegios();
        }

        #endregion
    }
}
