namespace WebApiDapper.ExceptionFilters.ExceptionModels
{
    public class ExceptionModel
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string StackTrace { get; set; }
    }
}
