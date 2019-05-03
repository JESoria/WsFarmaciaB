using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FarmaciaB.Models;

namespace FarmaciaB.Controllers
{
    public class DetailController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ProductDetail(ProductSearchModel data)
        {
            DetailModel detalle = null;

            using (FarmaciaBEntities db = new FarmaciaBEntities())
            {
                db.SUCURSAL_PRODUCTO.Where(x => x.ID_SUCURSAL_PRODUCTO == data.idSucursalProducto).ToList().ForEach(x =>
                {

                    detalle = new DetailModel()
                    {
                        producto = x.PRODUCTO.PRODUCTO1,
                        presentacion = x.PRODUCTO.PRESENTACION.PRESENTACION1,
                        fechaVencimiento = x.FECHA_VENCIMIENTO.ToShortDateString(),
                        laboratorio = x.PRODUCTO.LABORATORIO.LABORATORIO1.Trim(),
                        principiosActivos = x.PRODUCTO.DESCRIPCION.Trim().Replace("\t", " "),
                        categoria = x.PRODUCTO.CATEGORIA.CATEGORIA1,
                        precio = Convert.ToDouble(x.PRECIO),
                        existencia = x.EXISTENCIA
                    };

                });

                return Ok(detalle);
            }
        }
    }
}
