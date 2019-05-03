using FarmaciaB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Globalization;

namespace FarmaciaB.Controllers
{
    public class SearchController : ApiController
    {
        [HttpPost]
        public IHttpActionResult NearbyDrugstore(SearchModel data)
        {
            double lonC = data.longitud;
            double latC = data.latitud;

            using (FarmaciaBEntities db = new FarmaciaBEntities())
            {
                List<ProductSearchModel> lista = new List<ProductSearchModel>();
                var Farmacia = db.FARMACIA.FirstOrDefault();

                db.SUCURSAL.ToList().ForEach(x =>
                {
                    double lat = Convert.ToDouble(x.LATITUD, CultureInfo.CreateSpecificCulture("en-US"));
                    double lon = Convert.ToDouble(x.LONGITUD, CultureInfo.CreateSpecificCulture("en-US"));

                    List<double> callfun = db.Database.SqlQuery<double>("select dbo.DistanceFromLatLonInKm({0}, {1}, {2}, {3})", new object[] { lonC, latC, lon, lat }).ToList();
                    if (callfun.FirstOrDefault() < 2)
                    {
                        x.SUCURSAL_PRODUCTO.Where(y => y.PRODUCTO.PRODUCTO1.ToLower().Contains(data.producto.ToLower())).ToList().ForEach(y =>
                        {
                            lista.Add(new ProductSearchModel()
                            {
                                sucursal = x.SUCURSAL1,
                                idSucursal = x.ID_SUCURSAL,
                                latitud = x.LATITUD,
                                longitud = x.LONGITUD,
                                direccion = x.DIRECCION,
                                idSucursalProducto = y.ID_SUCURSAL_PRODUCTO,
                                producto = y.PRODUCTO.PRODUCTO1,
                                precio = Convert.ToDecimal(y.PRECIO),
                                idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA)
                            });
                        });
                    }
                });

                return Ok(lista);

            }

        }
    }
}
