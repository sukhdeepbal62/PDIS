SET IDENTITY_INSERT [dbo].[Departments] ON 
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (1, N'Business Development')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (2, N'Sales & Marketing')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (3, N'Development')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (4, N'Test Team')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (5, N'Architecture')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (6, N'Operations')
GO
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName]) VALUES (7, N'Customer Support')
GO
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (1, N'Hardware Technician', 5600)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (2, N'Help Desk Analyst', 6000)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (3, N'Network Administrator', 7500)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (4, N'Business Analyst', 8000)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (5, N'IT Project Manager', 12000)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (6, N'Software Developer', 8500)
GO
INSERT [dbo].[Posts] ([PostID], [PostName], [Salary]) VALUES (7, N'Senior Software Developer', 10000)
GO
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [ExtName], [UserName], [BasicSalary], [JoiningDate], [DepartmentID], [PostID]) VALUES (1, N'Hillary Kit', N'.png', N'P001@pdis.com', 7500, CAST(N'1999-06-16T23:36:00.0000000' AS DateTime2), 5, 3)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Salaries] ON 
GO
INSERT [dbo].[Salaries] ([SalaryID], [SalaryMonth], [SalaryYear], [TotalSalary], [EmployeeID]) VALUES (1, N'January', 2021, 10837.5, 1)
GO
INSERT [dbo].[Salaries] ([SalaryID], [SalaryMonth], [SalaryYear], [TotalSalary], [EmployeeID]) VALUES (2, N'February', 2021, 10837.5, 1)
GO
INSERT [dbo].[Salaries] ([SalaryID], [SalaryMonth], [SalaryYear], [TotalSalary], [EmployeeID]) VALUES (3, N'March', 2021, 10837.5, 1)
GO
SET IDENTITY_INSERT [dbo].[Salaries] OFF
GO
SET IDENTITY_INSERT [dbo].[SalaryDetails] ON 
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (1, N'Basic Salary', 7500, 1)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (2, N'Dearness Allowance', 750, 1)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (3, N'House Rent Allowance', 825, 1)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (4, N'Entertainment Allowance', 262.5, 1)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (5, N'Provident Fund', 1500, 1)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (6, N'Basic Salary', 7500, 2)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (7, N'Dearness Allowance', 750, 2)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (8, N'House Rent Allowance', 825, 2)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (9, N'Entertainment Allowance', 262.5, 2)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (10, N'Provident Fund', 1500, 2)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (11, N'Basic Salary', 7500, 3)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (12, N'Dearness Allowance', 750, 3)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (13, N'House Rent Allowance', 825, 3)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (14, N'Entertainment Allowance', 262.5, 3)
GO
INSERT [dbo].[SalaryDetails] ([DetailsID], [AllowanceName], [Amount], [SalaryID]) VALUES (15, N'Provident Fund', 1500, 3)
GO
SET IDENTITY_INSERT [dbo].[SalaryDetails] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b0eebf44-9385-493a-b8d8-61d90d2949c6', N'hr', N'hr', N'4596acbf-5c01-4e6c-af6d-94a503b8169f')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'38fa1b41-e62f-424f-8232-4227c736ec49', N'P001@pdis.com', N'P001@PDIS.COM', N'P001@pdis.com', N'P001@PDIS.COM', 1, N'AQAAAAEAACcQAAAAEH5pVaNUddODWPzzJ8FlhJ/9wVuUCpl8HQChGkw1koEmRGJDa2UPJu3VNsWntYY04g==', N'C7D6FXKADPC6EK6KHS7KGVFHABY5UP3I', N'175bf5b0-8343-4b1e-a86a-cdb18f25ecbd', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd3b9befb-d7a1-4875-ac5e-5f8362d41fcd', N'hra@pdis.com', N'HRA@PDIS.COM', N'hra@pdis.com', N'HRA@PDIS.COM', 1, N'AQAAAAEAACcQAAAAEICsaRuHYxWCSiADpAfIF9u0ZFfSAuOBPerT7oDOUTrAydh4IaFpdrxdN2n8+KTC9g==', N'PQAQ7GLKPIIDDU5XS27EBRRWSU2GWMDX', N'e4ddc128-6e06-4a8e-bd64-5abcd69c8c21', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd3b9befb-d7a1-4875-ac5e-5f8362d41fcd', N'b0eebf44-9385-493a-b8d8-61d90d2949c6')
GO
SET IDENTITY_INSERT [dbo].[Allowances] ON 
GO
INSERT [dbo].[Allowances] ([AllowanceID], [ShortName], [AllowanceName], [Percentage]) VALUES (1, N'DA', N'Dearness Allowance', 10)
GO
INSERT [dbo].[Allowances] ([AllowanceID], [ShortName], [AllowanceName], [Percentage]) VALUES (2, N'HRA', N'House Rent Allowance', 11)
GO
INSERT [dbo].[Allowances] ([AllowanceID], [ShortName], [AllowanceName], [Percentage]) VALUES (3, N'EA', N'Entertainment Allowance', 3.5)
GO
INSERT [dbo].[Allowances] ([AllowanceID], [ShortName], [AllowanceName], [Percentage]) VALUES (4, N'PF', N'Provident Fund', 20)
GO
SET IDENTITY_INSERT [dbo].[Allowances] OFF
GO
