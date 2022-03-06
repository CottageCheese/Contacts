
/****** Object:  Table [dbo].[Contact]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Contacts_Delete_Contact_SP]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contacts_Delete_Contact_SP]
(
	@Id INT
)
AS
DELETE FROM
	Contact
WHERE
	Id= @Id

GO
/****** Object:  StoredProcedure [dbo].[Contacts_Get_Contact_By_Id]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Contacts_Get_Contact_By_Id] @id int
AS
SELECT [Id]
      ,[Firstname]
      ,[Lastname]
      ,[email]
  FROM [dbo].[Contact]
  WHERE
	Id = @id

GO
/****** Object:  StoredProcedure [dbo].[Contacts_Get_Contact_By_ID_SP]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Contacts_Get_Contact_By_ID_SP] @id int
AS
SELECT [Id]
      ,[Firstname]
      ,[Lastname]
      ,[email]
  FROM [dbo].[Contact]
  WHERE
	Id = @id

GO
/****** Object:  StoredProcedure [dbo].[Contacts_Get_Contacts_SP]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Contacts_Get_Contacts_SP]    Script Date: 05/03/2022 11:14:08 ******/
CREATE  PROCEDURE [dbo].[Contacts_Get_Contacts_SP]
AS

SELECT [Id]
      ,[Firstname]
      ,[Lastname]
      ,[email]
  FROM [dbo].[Contact]

GO
/****** Object:  StoredProcedure [dbo].[Contacts_Insert_Contact_SP]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contacts_Insert_Contact_SP]
(
	@Firstname varchar(max),
	@Lastname varchar(max),
	@Email varchar(max),
	@Id INT OUTPUT
)
AS
INSERT INTO
Contact(Firstname, Lastname, email)
VALUES
(@Firstname, @Lastname, @Email)


SELECT 
	@Id = [Contact].Id
FROM
	[Contact]
WHERE
	[Contact].Id = SCOPE_IDENTITY();

GO
/****** Object:  StoredProcedure [dbo].[Contacts_Update_Contact_SP]    Script Date: 06/03/2022 14:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contacts_Update_Contact_SP]
(
	@Firstname varchar(max),
	@Lastname varchar(max),
	@Email varchar(max),
	@Id INT OUTPUT
)
AS

UPDATE
	Contact
SET
	Firstname = @Firstname,
	Lastname = @Lastname,
	Email = @Email
WHERE
	Id = @Id
GO
