USE [RestAPIDapper1]
GO

INSERT INTO [Attributes] (Code, Name, BackendType, IsActive, SortOrder)
VALUES 
	('name', N'Tên', 'string', 1, 1),
	('Description', N'Mô tả', 'string', 1, 2),
	('Content', N'Nội dung', 'string', 1, 3),
	('SeoAlias', N'Seo Alias', 'string', 1, 4),
	('SeoAlias', N'Seo Alias', 'string', 1, 5),
	('SeoTitle', N'Seo Title', 'string', 1, 6),
	('SeoKeyword', N'Seo Keyword', 'string', 1, 7),
	('SeoDescription', N'Seo Description', 'string', 1, 8)
GO

INSERT INTO [Categories] (Name, ParentId, SortOrder, IsActive)
VALUES
	('Màn hình', null, 1, 1),
	('Phụ tùng', null, 1, 1),
	('Bán lẻ', null, 1, 1)
go