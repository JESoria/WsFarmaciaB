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
        public async Task<IHttpActionResult> ProductDetail(ProductSearchModel data)
        {
            List<DetailModel> detalle = new List<DetailModel>();

            using (FarmaciaBEntities db = new FarmaciaBEntities())
            {
                db.SUCURSAL_PRODUCTO.Where(x => x.ID_SUCURSAL_PRODUCTO == data.idSucursalProducto).ToList().ForEach(x => {
                    db.PRODUCTO.Where(y => y.ID_PRODUCTO == x.ID_PRODUCTO).ToList().ForEach(y => {
                        db.PRESENTACION.Where(z => z.ID_PRESENTACION == y.ID_PRESENTACION).ToList().ForEach(z => {
                            db.CATEGORIA.Where(c => c.ID_CATEGORIA == y.ID_CATEGORIA).ToList().ForEach(c => {
                                db.LABORATORIO.Where(l => l.ID_LABORATORIO == y.ID_LABORATORIO).ToList().ForEach(l =>
                                {
                                    detalle.Add(new DetailModel()
                                    {
                                        producto = y.PRODUCTO1,
                                        presentacion = z.PRESENTACION1,
                                        fechaVencimiento = x.FECHA_VENCIMIENTO,
                                        laboratorio = l.LABORATORIO1,
                                        principiosActivos = y.DESCRIPCION,
                                        categoria = c.CATEGORIA1,
                                        precio = Convert.ToDouble(x.PRECIO),
                                        existencia = x.EXISTENCIA
                                    });
                                });
                            });
                        });
                    });
                });

                return Ok(detalle);
            }
        }
    }
}
