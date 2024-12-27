SELECT SUSER_NAME() AS UserName;

SELECT 
    spr.name AS PrincipalName,
    spr.type_desc AS PrincipalType,
    sp.permission_name AS PermissionName,
    sp.state_desc AS PermissionState
FROM 
    sys.server_permissions sp
JOIN 
    sys.server_principals spr ON sp.grantee_principal_id = spr.principal_id
WHERE 
    spr.name = 'sa';


-- Liệt kê tất cả người dùng trong cơ sở dữ liệu:
SELECT name AS UserName, type_desc AS UserType
FROM sys.database_principals
WHERE type IN ('S', 'E', 'X', 'G', 'R') -- SQL User, External User, Windows Group...
AND name NOT LIKE '##%';


--Liệt kê tất cả vai trò server:
SELECT name AS RoleName, type_desc AS RoleType
FROM sys.server_principals
WHERE type IN ('R'); -- Chỉ liệt kê các vai trò (Roles)

--Cấp quyền cho người dùng hoặc vai trò:
GRANT EXECUTE ON OBJECT::[Create_Permission] TO sa;
GRANT CREATE TO sa;


--Thêm người dùng vào vai trò:
ALTER ROLE db_datawriter ADD MEMBER <TênNgườiDùng>;


