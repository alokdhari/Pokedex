using System.Net;

namespace Pokedex.Api.Services.Models
{
    public class Result<TEntity> where TEntity : class
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public bool Successful { get; set; }

        public string Message { get; set; }

        public TEntity Data { get; set; }
    }
}
