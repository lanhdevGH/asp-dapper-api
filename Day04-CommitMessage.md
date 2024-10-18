# How to write a commit message conventional 

## 1. Conventional Commits 
  **Commit message** là một phần quan trọng trong quá trình sử dụng hệ thống quản lý phiên bản như Git. Mỗi khi bạn thực hiện một thay đổi (commit) trong dự án, bạn cần cung cấp một thông điệp mô tả những gì bạn đã thay đổi trong phiên bản mã nguồn. 
  **Conventional Commits** là một tiêu chuẩn để viết commit message theo một quy tắc nhất quán, giúp cải thiện việc theo dõi và quản lý thay đổi trong hệ thống quản lý phiên bản như Git. 
  Thực ra là không có cách viết nào là đúng và cũng không có cách viết nào là sai cả, bởi nó cũng chỉ là 1 một dung text cho việc thể hiện thôi. Do vậy nếu bạn có commit "abc" thì cũng không có sao hiện tại, nhưng tương lại thì có đấy. Tuy nhiên, nếu trong một project mỗi người viết commit message một kiểu thì khi nhìn vào commit history nó cũng khá giống 1 mớ bùi nhùi, **tệ**. Chưa kể khi cần chúng ta cần tìm kiếm và xem lại commit trong đống commit của vài tháng trước đó thông qua các commit message thì có thể xem như bạn đang lục lại sọt rác của mình vậy.
  **Conventional Commits** được thiết kế để giúp bạn viết commit message một cách rõ ràng

## 2. Conventional Commits trong dự án
  Dưới đây là cấu trúc chung của một commit message mình sẽ đặt ra, và minh sẽ sử dụng nó cho xuyên suốt quá trình học của mình( cho đến khi gặp một **Conventional commit mới**):
  > &#91;TYPE&#93; &#91;Scope&#93; &#91;Description&#93;
  &#91;option body&#93;
  &#91;option footer&#93;

  1. **`<type>`**  
   Loại thay đổi (feat, fix, docs, chore, etc.) 
  2. **`[optional scope]`**  
   Phạm vi thay đổi, tùy chọn (auth, api, etc.)
  3. **`<description>`**  
   Mô tả ngắn gọn về commit (không quá 72 ký tự).
  4. **`[optional body]`**  
   Phần mô tả chi tiết, tùy chọn. Giải thích thêm về thay đổi nếu cần.
  5. **`[optional footer(s)]`**  
   Thông tin thêm ở cuối như `BREAKING CHANGE` hoặc đóng các issue (`Closes #123`). 

  - **Type theo conventional commit**
  - **feat**: Được dùng khi thêm một tính năng mới.
  - **fix**: Sửa lỗi trong mã nguồn.
  - **docs**: Chỉ thay đổi tài liệu (documentation).
  - **style**: Các thay đổi về định dạng mã, không ảnh hưởng đến logic (ví dụ: thêm khoảng trắng, chỉnh sửa dấu câu).
  - **refactor**: Thay đổi mã nhưng không sửa lỗi hay thêm tính năng mới (ví dụ: tối ưu code, cải thiện cấu trúc).
  - **test**: Thêm hoặc sửa các bài kiểm tra (test).
  - **chore**: Các thay đổi không liên quan đến source code hoặc test (ví dụ: thay đổi cấu hình build hoặc các công cụ).
  - **perf**: Cải thiện hiệu suất.
  - **ci**: Thay đổi liên quan đến hệ thống tích hợp liên tục (CI).