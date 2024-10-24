using System.Text.Json;

namespace WebApiDapper.ExceptionFilters.ExceptionModels
{
    public class ExceptionModel
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
