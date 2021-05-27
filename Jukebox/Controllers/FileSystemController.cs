using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using StandardStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace Jukebox.Controllers
{
    public class FileSystemController : WebApiController
    {
        [Route(HttpVerbs.Get, "/files")]
        public async Task<string> GetMusicPaths()
        {
            var files = await FileSystem.Current.LocalStorage.GetFilesAsync();
            var query = (from file in files
                        select file.Name).ToList();
            return JsonConvert.SerializeObject(query, Formatting.Indented);
        }
    }
}
