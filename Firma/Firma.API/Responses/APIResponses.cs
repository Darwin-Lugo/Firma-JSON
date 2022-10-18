#region References
using Firma.Core.CustomEntities;
using System.Net; 
#endregion

namespace Firma.API.Responses
{
    public class APIResponses<T>
    {
        public APIResponses(T data)
        {
            Data = data;
        }

        public string Version { get; set; } = null!;
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public MetaData? Meta { get; set;}
        public Error? Error { get; set; }
    }
}
