CREATE TABLE [dbo].[Ranks]
(
  [RankId] INT NOT NULL PRIMARY KEY IDENTITY, 
  [Position] INT NOT NULL, 
  [MatchMakingRating] INT NOT NULL , 
  CONSTRAINT [CK_Range_Position] CHECK (Position > 0 AND Position < 2147483647), 
  CONSTRAINT [CK_Range_MatchMakingRating] CHECK (MatchMakingRating > 0 AND MatchMakingRating < 2147483647) 
)
