using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "Select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = (int)dr["IdUsuario"],
                                Nombres = (string)dr["Nombres"],
                                Apellidos = (string)dr["Apellidos"],
                                Correo = (string)dr["Correo"],
                                Clave = (string)dr["Clave"],
                                Reestablecer = (bool)dr["Reestablecer"],
                                Activo = (bool)dr["Activo"]
                            });

                        }
                    }
                }

                
                
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}
