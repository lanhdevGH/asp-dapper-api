# Global Error Handling 
Trong context của ASP.net core Web Api hay bất kỳ framework web app nào, global exception handling liên quan đến (involve) việc catching exception xảy ra (occur) trong quá trình xử lý request và trả về response error thích hợp (appropriate) đến client. Thay vì cho phép exception truyền (propagate) đến các cấp cao hơn, và show lỗi cho client thì global exception handling có thể chuyển đổi chúng thành chuẩn để trả về cho client.

# Lợi ích của việc sử dụng Global Error Handling

 1. **Nhất quán (Consistent) các error response**
 2. **Cải thiện trải nghiệm người dùng**: Thay vì hiển thị một message error khó hiểu (cryptic) hay các stack traces, global có thể return về một message dễ đọc, dễ hiểu hơn.
 3.  **Khả năng phục hồi ứng dụng** 
 4. **Bảo mật**
 5. **Logging và Monitoring**

# Example

Đầu tiên, ta đã xây dựng class ```ExceptionModel``` để chứa thông tin lỗi trả về của hệ thống.
```bash
public class ExceptionModel {
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string StackTrace { get; set; }
}
```
Tiếp theo, hãy tạo môt **static class** ```ExceptionMiddlewareExtensions``` 
Bây giờ, modify lại phương thức ```ConfigureExceptionHandler``` là phương thức mở rộng của interface ```IApplicationBuilder``` 
```bash
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async (context) =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ExceptionModels.ExceptionModel
                {
                    Message = "Internal Server Error.",
                    StatusCode = context.Response.StatusCode,
                    StackTrace = ""
                }.ToString());
            });
        });
    }
}
``` 
Sau đó, đăng ký chúng trong ```program``` file.
```bash
app.ConfigureExceptionHandler();
```
# Handling Errors Globally With the Custom Middleware 
Tạo thư mục `CustomExceptionMiddleware` và tạo class ExceptionMiddleware.cs trong đó. 
```bash
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        }.ToString());
    }
}
``` 
Sử dụng middleware vừa tạo trong phương thức mở rộng `ConfigureExceptionHandler` mới tạo ở ví dụ trước.
```bash
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        //app.UseExceptionHandler(appError =>
        //{
        //    appError.Run(async (context) =>
        //    {
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        context.Response.ContentType = "application/json";
        //        await context.Response.WriteAsync(new ExceptionModels.ExceptionModel
        //        {
        //            Message = "Internal Server Error.",
        //            StatusCode = context.Response.StatusCode,
        //        }.ToString());
        //    });
        //});
    }
}
``` 
Chạy kiểm tra kết quả.
