﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                Mensaje = "El nombre del Producto no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción de la Producto no puede ser vacía";
            else if (obj.oMarca.IdMarca == 0)
                Mensaje = "Debe seleccionar una marca";
            else if (obj.oCategoria.IdCategoria == 0)
                Mensaje = "Debe seleccionar una categoría";
            else if (obj.Precio <= 0)
                Mensaje = "El precio no puede ser 0";
            else if (obj.Stock < 0)
                Mensaje = "Debe ingresar un stock igual o mayor a 0";


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
                Mensaje = "El nombre del Producto no puede ser vacío";
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
                Mensaje = "La descripción de la Producto no puede ser vacía";
            else if (obj.oMarca.IdMarca == 0)
                Mensaje = "Debe seleccionar una marca";
            else if (obj.oCategoria.IdCategoria == 0)
                Mensaje = "Debe seleccionar una categoría";
            else if (obj.Precio <= 0)
                Mensaje = "El precio no puede ser 0";
            else if (obj.Stock < 0)
                Mensaje = "Debe ingresar un stock igual o mayor a 0";

            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDato.Editar(obj, out Mensaje);
            else
                return false;
        }

        public bool GuardarDatosImg(Producto oproducto, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImg(oproducto, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
