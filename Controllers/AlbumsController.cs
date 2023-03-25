using SmartMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartMvcApp.Controllers
{
    public class AlbumsController : ApiController
    {
        AlbumInfoRepository repository = new AlbumInfoRepository();

        // GET api/values
        public IEnumerable<Album> Get()
        {
            return repository.GetData();
        }
    }
}