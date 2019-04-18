using FarmaciaB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Device.Location;
using System.Globalization;
using FarmaciaB.Models;

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

                db.SUCURSAL_PRODUCTO.OrderBy(x => x.ID_SUCURSAL_PRODUCTO).ToList().ForEach(x =>
                {
                    db.SUCURSAL.Where(s => s.ID_SUCURSAL == x.ID_SUCURSAL).ToList().ForEach(y =>
                    {

                        db.PRODUCTO.Where(p => p.ID_PRODUCTO == x.ID_PRODUCTO && p.PRODUCTO1.Contains(data.producto)).ToList().ForEach(w =>
                        {
                            double lat = Convert.ToDouble(y.LATITUD, CultureInfo.CreateSpecificCulture("en-US"));
                            double lon = Convert.ToDouble(y.LONGITUD, CultureInfo.CreateSpecificCulture("en-US"));

                            List<double> callfun = db.Database.SqlQuery<double>("select dbo.DistanceFromLatLonInKm({0}, {1}, {2}, {3})", new object[] { lonC, latC, lon, lat }).ToList();
                            if (callfun.FirstOrDefault() < 2)
                            {

                                lista.Add(new ProductSearchModel() { sucursal = y.SUCURSAL1, idSucursal = y.ID_SUCURSAL, latitud = y.LATITUD, longitud = y.LONGITUD, direccion = y.DIRECCION, idSucursalProducto = x.ID_SUCURSAL_PRODUCTO, producto = w.PRODUCTO1, precio = Convert.ToDecimal(x.PRECIO), idFarmacia = Convert.ToInt32(Farmacia.ID_FARMACIA) });
                            }


                        });

                    });
                });

                return Ok(lista);

            }

        }
    }
}
