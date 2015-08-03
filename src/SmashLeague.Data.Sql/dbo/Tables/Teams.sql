CREATE TABLE [dbo].[Teams]
(
  [TeamId] INT  NOT NULL
  CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([TeamId] ASC), 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Teams_Name] ON [dbo].[Teams] ([Name])
