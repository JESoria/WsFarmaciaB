using FarmaciaB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FarmaciaB.Controllers
{
    public class SearchByDrugstoreController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> SearchDrugstore(SearchModel data)
        {
            List<ProductSearchModel> lista = new List<ProductSearchModel>();

            using (FarmaciaBEntities db = new FarmaciaBEntities())
            {
                var Farmacia = db.FARMACIA.FirstOrDefault();

                db.SUCURSAL_PRODUCTO.OrderBy(x => x.ID_SUCURSAL_PRODUCTO).ToList().ForEach(x =>
                {
                    db.PRODUCTO.Where(y => y.ID_PRODUCTO == x.ID_PRODUCTO && y.PRODUCTO1.Contains(data.producto) && x.ID_SUCURSAL == data.idSucursal).ToList().ForEach(z =>
                    {
                        lista.Add(new ProductSearchModel
                        {
                            idSucursalProducto = x.ID_SUCURSAL_PRODUCTO,
                            idSucursal = x.ID_SUCURSAL,
                            producto = z.PRODUCTO1,
                            precio = Convert.ToDecimal(x.PRECIO),
                            sucursal = x.SUCURSAL.SUCURSAL1,
                            latitud = x.SUCURSAL.LATITUD,
                            longitud = x.SUCURSAL.LONGITUD,
                            direccion = x.SUCURSAL.DIRECCION,
                            idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA)
                        });
                    });
                });
                return Ok(lista.OrderBy(x => x.precio));
            }
        }
    }
}
