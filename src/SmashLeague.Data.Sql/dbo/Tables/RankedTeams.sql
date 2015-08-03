CREATE TABLE [dbo].[RankedTeams]
(
  [TeamId] INT NOT NULL,
  [RankId] INT NOT NULL, 

  CONSTRAINT [PK_dbo.RankedTeams] PRIMARY KEY CLUSTERED ([TeamId] ASC),
  CONSTRAINT [FK_RankedTeams_Teams] FOREIGN KEY ([TeamId]) REFERENCES Teams([TeamId]), 
  CONSTRAINT [FK_RankedTeams_Ranks] FOREIGN KEY ([RankId]) REFERENCES Ranks([RankId])
)
