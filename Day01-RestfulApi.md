# Day 1. Review Knowledge about RESTful API

## Lời nói đầu

Để bắt đầu project này, mình cũng như các bạn cùng nhau hệ thống lại kiến thức cơ bản về REST, RESTful, RESTful API để có những cái nhìn khách quan khi chúng ta gặp những vấn đề sau này, từ đó có thể chọn cách khắc phục như thế nào cho chuẩn và tối ưu nhé.

![rest-Image](./assets/images/rest-api.png)

### 1. REST, RESTful - Nó là cái gì????

REST API (Representational State of Resource API) là một kiểu kiến trúc cho các ứng dụng mạng. Nó dựa trên ý tưởng về các resource, được xác định bởi các URI (Uniform Resource Identifiers), và có thể được thao tác bằng một tập hợp các hoạt động cố định.
RESTful là ứng dụng tuân theo các tiêu chuẩn của REST.

**Các đặc điểm chính của REST:**

- **Everything in REST is a resource(Resource-Based)**: mọi thư trong REST được định nghĩa dưới đạng resource
- **Client-Server Architecture**: server và client được phân biệt rõ ràng(client gởi request tới server và server trả về response cho client)
- **Stateless**: Server không cần biết bất kỳ thông tin nào về client giữa các request
- **Cacheable**: Có thể catche lại các respone để cải thiện hiệu suất 

### 2. Communication between Client and Server
Trong kiến trúc REST, clients gởi request để nhận hoặc chỉnh sửa các resource và server sẽ trả về response. 
Vậy bạn đã biết được một request có cấu tạo như thế nào chưa? Sau đây là những hiểu biết nông cạn của tôi về request/response. 

#### Request 

![structRequest](./assets/images/request.png) 

Chuẩn chung cho request thường bao gồm:
1. Request Line
2. Header
3. Body message (nếu cần)

<br> 
<br> 

##### Request Line
Là dòng đầu tiên của một request HTTP. Chứa:
- **Phương thức (method) được sử dụng**: Gồm 4 phương thức chính:
  - **GET**: Lấy thông tin resource qua đường dẫn (Path)
    `GET không làm thay đổi trạng thái của server, do đó nó là idempotent (nghĩa là gọi nhiều lần sẽ không thay đổi kết quả) và safe (không thay đổi dữ liệu).`
  - **POST**: Tạo một resource mới
    `POST gửi dữ liệu đến server để tạo resource mới, và có thể làm thay đổi trạng thái của server. Nó không phải là idempotent, nghĩa là gọi nhiều lần có thể tạo ra nhiều resource khác nhau.`
  - **PUT**: Cập nhật hoàn toàn một resource đã tồn tại
    `PUT là idempotent, tức là nhiều lần gọi PUT với cùng dữ liệu sẽ không thay đổi kết quả.` 
  - **PATCH**: Cập nhật một phần resource hiện có.
    `PATCH cũng không phải là idempotent nếu không được cài đặt cẩn thận. Nó chỉ sửa đổi các trường cụ thể của tài nguyên, không cần cập nhật toàn bộ.`
  - **DELETE**: Xóa một tài nguyên khỏi server. 
    `DELETE là idempotent, nghĩa là gọi nhiều lần thì tài nguyên chỉ bị xóa một lần.`
  Ngoài ra, còn có HEAD, OPTION các bạn có thể tìm hiểu thêm. 

- **Path (URL) đường dẫn trỏ tới các resource**
- **Version (phiên bản HTTP sử dụng)** 
<br> 

##### Header
Header của một HTTP request chứa thông tin về yêu cầu mà client (như trình duyệt, ứng dụng, v.v.) gửi đến server. Các header này có thể bao gồm thông tin về định dạng dữ liệu, loại nội dung, xác thực, cache, và nhiều thông tin khác liên quan đến yêu cầu. 

Dưới đây là các thành phần phổ biến trong header của một HTTP request:
1. **General Headers**: Tiêu đề chung
  - **Host**: Chỉ định tên miền hoặc địa chỉ IP của server nhận yêu cầu. 
```bash
Host: www.example.com
``` 

  - **User-Agent**: Thông tin về ứng dụng, trình duyệt hoặc thiết bị gửi request. 
```bash
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36
``` 

  - **Accept**: Chỉ định loại dữ liệu mà client có thể nhận về từ server (thường là MIME types). 
```bash
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng
``` 
  - **Accept-Encoding**: Thông báo về cách thức nén dữ liệu mà client có thể hiểu (ví dụ: gzip, deflate). 
```bash
Accept-Encoding: gzip, deflate, br 
``` 

  - **Accept-Language**: Chỉ định ngôn ngữ ưa thích mà client mong muốn từ server. 
```bash
Accept-Language: en-US,en;q=0.9
``` 

  - **Referer**: URL của trang trước đó, chỉ ra nguồn gốc của yêu cầu. 
```bash
Referer: https://www.example.com/previous-page
``` 

  - **Connection**: Chỉ ra cách quản lý kết nối giữa client và server (ví dụ: "keep-alive" để giữ kết nối mở, "close" để đóng sau khi hoàn thành).
```bash
Connection: keep-alive
``` 
<br>

2. **Request Headers Specific to HTTP Methods**: Tiêu đề yêu cầu cụ thể theo phương thức HTTP
  - **Cache-Control**: Quản lý cách dữ liệu được lưu trong bộ nhớ cache. 
```bash
Cache-Control: no-cache
``` 

  - **Content-Type**: Chỉ định loại dữ liệu mà client gửi đi (MIME type), ví dụ: JSON, form data.
```bash
Content-Type: application/json
``` 

  - **Content-Length**: Độ dài của nội dung (body) yêu cầu, được sử dụng cho POST, PUT, PATCH. 
```bash
Content-Length: 348
``` 

  - **Origin**: Chỉ định nguồn gốc (domain) của yêu cầu, thường sử dụng trong các yêu cầu liên miền (CORS).
```bash
Origin: https://www.example.com
``` 
<br> 

3. **Authorization Headers**: Tiêu đề xác thực
  - **Authorization**: Thường được sử dụng để gửi token hoặc thông tin đăng nhập nhằm xác thực với server. 
```bash
Authorization: Bearer your-jwt-token
``` 
or (basic) 
```bash
Authorization: Basic dXNlcm5hbWU6cGFzc3dvcmQ
``` 
<br> 

4. **Custom Headers**: Tiêu đề tùy chỉnh
Sau cùng là phần
**Message Body**: (nội dung) của yêu cầu HTTP. Body có thể chứa dữ liệu được 
****
<br> 

##### Response 
![respone-example](./assets/images/respone.png)
Cấu trúc HTTP Response gần giống với HTTP request. Điểm khác biệt là thay vì Request-Line, HTTP có response có Status-Line. 
Tương tự như Request-Line, Status-Line cũng có ba phần như sau:
1. **HTTP-version**:phiên bản HTTP cao nhất mà server hỗ trợ.
2. **Status-Code**: mã kết quả trả về.
3. **Reason-Phrase**: mô tả về Status-Code.

Điều quan trọng mà các bạn cân phải chú ý là các response code và ý nghĩa của nó. Chúng ta áp dùng sau này rất nhiều.
