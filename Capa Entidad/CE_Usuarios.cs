namespace Capa_Entidad
{
    public class CE_Usuarios
    {
        private int _IdUsuario;
        private string _Nombres;
        private string _Apellidos;
        private int _DUI;
        private int _NIT;
        private string _Correo;
        private int _Telefono;
        private DateTime _Fecha_Nac;
        private int _Privilegio;
        private byte[] _img;
        private string _Usuario;
        private string _Contrasenia;
        private string _patron;

        public int IdUsuario { get => _IdUsuario; set => _IdUsuario = value; }
        public string Nombres { get => _Nombres; set => _Nombres = value; }
        public string Apellidos { get => _Apellidos; set => _Apellidos = value; }
        public int DUI { get => _DUI; set => _DUI = value; }
        public int NIT { get => _NIT; set => _NIT = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public int Telefono { get => _Telefono; set => _Telefono = value; }
        public DateTime Fecha_Nac { get => _Fecha_Nac; set => _Fecha_Nac = value; }
        public int Privilegio { get => _Privilegio; set => _Privilegio = value; }
        public byte[] Img { get => _img; set => _img = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Contrasenia { get => _Contrasenia; set => _Contrasenia = value; }
        public string Patron { get => _patron; set => _patron = value; }
    }
}
