using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        //+++++++++++++++CATEGORIA++++++++++++++++
        #region CATEGORIA


        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            else
                resultado = new CN_Categoria().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        //+++++++++++++++MARCA++++++++++++++++
        #region MARCA

        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();

            oLista = new CN_Marca().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            else
                resultado = new CN_Marca().Editar(objeto, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        #endregion

        //+++++++++++++++PRODUCTO++++++++++++++++
        #region PRODUCTO

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImg)
        {
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool guardarImg = true;
            decimal precio;

            Producto oProducto = new Producto();

            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-ES"), out precio))
                oProducto.Precio = precio;
            else
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser 00.00" }, JsonRequestBehavior.AllowGet);

            if (oProducto.IdProducto == 0)
            {
                int idGenerado = new CN_Producto().Registrar(oProducto, out mensaje);
                if(idGenerado != 0)
                {
                    oProducto.IdProducto = idGenerado;
                }
                else
                {
                    operacionExitosa = false;
                }
            }
            else
                operacionExitosa = new CN_Producto().Editar(oProducto, out mensaje);

            if (operacionExitosa)
            {
                if(archivoImg != null)
                {
                    string rutaGuardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImg.FileName);
                    string nombreImg = string.Concat(oProducto.IdProducto.ToString(), extension);

                    try
                    {
                        archivoImg.SaveAs(Path.Combine(rutaGuardar, nombreImg));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarImg = false;
                        throw ex;
                    }

                    if (guardarImg)
                    {
                        oProducto.UrlImagen = rutaGuardar;
                        oProducto.NombreImagen = nombreImg;
                        bool rspta = new CN_Producto().GuardarDatosImg(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Ha habido un error con la imagen pero se guardo su producto";
                    }
                }
            }


            return Json(new { operacionExitosa = operacionExitosa, idGenerado = oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult imagenProducto(int id)
        {
            bool conversion;
            Producto oproducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvetirBase64(Path.Combine(oproducto.UrlImagen, oproducto.NombreImagen), out conversion);

            return Json(new
            {

                conversion = conversion,
                textoBase64 = textoBase64,
                extesion = Path.GetExtension(oproducto.NombreImagen)

            },
            JsonRequestBehavior.AllowGet
            );
        }



        #endregion


    }
}