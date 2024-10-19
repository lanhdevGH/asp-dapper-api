# Các Kiểu Trả Về Trong ASP.NET Core - IActionResult

## 1. Phản hồi thành công (Success Responses)
Các phản hồi này được trả về khi yêu cầu từ phía client được xử lý thành công. Thông thường, các mã trạng thái sẽ thuộc dải từ 200 đến 299.

- **`OkResult / Ok(Object)`**: Mã trạng thái HTTP 200 (OK). 
  - Trả về khi yêu cầu thành công và có thể kèm theo dữ liệu.
- **`CreatedResult / CreatedAtAction / CreatedAtRoute`**: Mã trạng thái HTTP 201 (Created).
  - Trả về khi một tài nguyên mới được tạo thành công.
- **`NoContentResult`**: Mã trạng thái HTTP 204 (No Content).
  - Trả về khi yêu cầu thành công nhưng không có dữ liệu nào để trả về.

### Ví dụ: 
- `Ok()` → HTTP 200
- `CreatedAtAction()` → HTTP 201
- `NoContent()` → HTTP 204

## 2. Phản hồi lỗi từ phía client (Client Error Responses)
Các phản hồi này được trả về khi có lỗi từ phía client. Thông thường, các mã trạng thái sẽ thuộc dải từ 400 đến 499.

- **`BadRequestResult / BadRequest(Object)`**: Mã trạng thái HTTP 400 (Bad Request).
  - Trả về khi yêu cầu không hợp lệ (có thể do dữ liệu không đúng).
- **`UnauthorizedResult`**: Mã trạng thái HTTP 401 (Unauthorized).
  - Trả về khi người dùng chưa xác thực.
- **`ForbidResult`**: Mã trạng thái HTTP 403 (Forbidden).
  - Trả về khi người dùng đã xác thực nhưng không có quyền truy cập tài nguyên.
- **`NotFoundResult / NotFound(Object)`**: Mã trạng thái HTTP 404 (Not Found).
  - Trả về khi không tìm thấy tài nguyên.
- **`ConflictResult`**: Mã trạng thái HTTP 409 (Conflict).
  - Trả về khi có xung đột trong quá trình xử lý yêu cầu.

### Ví dụ:
- `BadRequest()` → HTTP 400
- `Unauthorized()` → HTTP 401
- `Forbid()` → HTTP 403
- `NotFound()` → HTTP 404

## 3. Phản hồi chuyển hướng (Redirection Responses)
Các phản hồi này được sử dụng để chuyển hướng client tới một URL hoặc action khác. Thông thường, mã trạng thái sẽ thuộc dải từ 300 đến 399.

- **`RedirectResult / RedirectToAction / RedirectToRoute`**: Mã trạng thái HTTP 302 (Found).
  - Chuyển hướng tới một URL hoặc action khác.
- **`LocalRedirectResult`**: Chuyển hướng tới một URL nội bộ.
- **`RedirectPermanentResult`**: Chuyển hướng vĩnh viễn với mã trạng thái HTTP 301 (Moved Permanently).

### Ví dụ:
- `Redirect("https://www.example.com")` → HTTP 302
- `RedirectPermanent()` → HTTP 301

## 4. Phản hồi lỗi từ phía server (Server Error Responses)
Các phản hồi này được trả về khi có lỗi từ phía server. Thông thường, mã trạng thái sẽ thuộc dải từ 500 đến 599.

- **`StatusCodeResult`**: Trả về mã trạng thái HTTP tùy chỉnh (500 trở lên).
  - Ví dụ: có thể trả về HTTP 500 (Internal Server Error) khi có lỗi hệ thống.
  
### Ví dụ:
- `StatusCode(500)` → HTTP 500
  
## 5. Phản hồi dữ liệu (Data Responses)
Các kiểu trả về này dùng để gửi dữ liệu (JSON, file, hoặc nội dung khác) cho client.

- **`JsonResult`**: Trả về dữ liệu JSON.
- **`FileResult`**: Trả về một file, như PDF, hình ảnh, hoặc tài liệu.
- **`ContentResult`**: Trả về nội dung đơn giản (text, HTML, hoặc nội dung tùy chỉnh).

### Ví dụ:
- `Json(data)` → Trả về dữ liệu JSON
- `File(fileBytes, "application/pdf")` → Trả về file PDF
- `Content("Hello, world!")` → Trả về text/plain

## Tóm tắt
Các kiểu trả về của `IActionResult` có thể được phân thành các nhóm chính như sau:
1. **Phản hồi thành công** (Success Responses)
2. **Phản hồi lỗi từ phía client** (Client Error Responses)
3. **Phản hồi chuyển hướng** (Redirection Responses)
4. **Phản hồi lỗi từ phía server** (Server Error Responses)
5. **Phản hồi dữ liệu** (Data Responses)
