using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();


        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                Mensaje = "El Nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                Mensaje = "El Apellido del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje = "El Correo del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Contraseña cuenta";
                string mensaje = "<h3>Su cuenta fue creada correctamente</h3></br>Para acceder a su cuenta su cuenta:</br>Nombre de usuario: !correo! </br>Contraseña: !clave!</br> Ante cualquier error contactenos!";
                mensaje = mensaje.Replace("!clave!", clave);
                mensaje = mensaje.Replace("!correo!", obj.Correo);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje);
                if (respuesta)
                {
                    obj.Clave = CN_Recursos.EncriptarContraseña(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);

                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
                return 0;
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                Mensaje = "El Nombre del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                Mensaje = "El Apellido del usuario no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje = "El Correo del usuario no puede ser vacío";

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.Editar(obj, out Mensaje);
            else
                return false;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

    }
}
