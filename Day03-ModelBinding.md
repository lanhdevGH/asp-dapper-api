# Tìm hiểu về ModelBinding trong ASP.net core 

  ## 1. Khái niệm 

  - Model Binding trong ASP.NET Core là một tính năng mạnh mẽ giúp chuyển đổi dữ liệu từ các yêu cầu HTTP (request) thành các đối tượng trong mã nguồn (models). Điều này giúp lập trình viên dễ dàng xử lý dữ liệu đầu vào từ form, query string, route data, hoặc các định dạng JSON/XML mà không cần phải viết mã thủ công để phân tích dữ liệu. 

  ## 2. Cách thức hoạt động 
  Khi một yêu cầu HTTP được gửi đến, ASP.NET Core sẽ thực hiện các bước sau:
    1. **Nhận dữ liệu từ yêu cầu HTTP**: Dữ liệu có thể đến từ nhiều nguồn khác nhau như: 
      - Query string (chuỗi truy vấn trong URL) 
      - Form data (dữ liệu từ form gửi lên)
      - Route data (dữ liệu được trích xuất từ route của ứng dụng)
      - Headers (HTTP headers) 
      - Body của yêu cầu (ví dụ JSON, XML)
    2. **Khớp dữ liệu với thuộc tính của model**: ASP.NET Core sẽ cố gắng khớp tên các biến trong yêu cầu HTTP với tên thuộc tính của model trong controller. Nếu chỉ định rõ nguồn data, việc khớp data sẽ tiến hành nhanh hơn.
    3. **Chuyển đổi dữ liệu**: Nếu dữ liệu được khớp, ASP.NET sẽ tự động chuyển đổi các chuỗi dữ liệu từ yêu cầu sang kiểu dữ liệu phù hợp với thuộc tính của model 
    4. **Cung cấp đối tượng đã khớp vào controller action** 

  ## 3. Các thuộc tính trong Model binding
  Bạn có thể sử dụng các thuộc tính để chỉ định rõ nguồn dữ liệu hoặc kiểm soát quá trình binding: 
  - **[FromQuery]**: Lấy dữ liệu từ query string trong URL.
  - **[FromForm]**: Lấy dữ liệu từ form (thường dùng trong HTTP POST).
  - **[FromRoute]**: Lấy dữ liệu từ route data trong URL.
  - **[FromBody]**: Lấy dữ liệu từ body của request (dùng với JSON/XML).
  - **[FromHeader]**: Lấy dữ liệu từ HTTP headers.
  - **[Bind]**: Chỉ định rõ các thuộc tính cụ thể của model mà bạn muốn bind.