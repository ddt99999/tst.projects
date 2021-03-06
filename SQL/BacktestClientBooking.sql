/****** Script for SelectTopNRows command from SSMS  ******/
SELECT  
      [CreatedDate] AS ForecastDate
      ,[Toxicity]
	  ,OriginalBook
      ,[NewBook]
      ,A.Client
      ,A.[Symbol]
	  , A.HedgeFraction
	  , B.DateSnap AS ActualDate,
	  B.NetPnL,
	  B.NetPosUsd,
	  B.AbsVolumeUsd
  FROM [SIGNALS].[dbo].[SuggestedClientRecommendation]  A 
  INNER JOIN [SIGNALS].[dbo].[AggregatedTradeRecord] B
  ON A.CreatedDate = DATEADD(day, DATEDIFF(day, 0, B.DateSnap), -1) AND A.Client = B.Client AND A.Symbol = B.Symbol 
	WHERE BatchName = 'Backtest_Configs\RulesV0.json'
	AND (HedgeFraction < 0)
--	AND Abs(NetPosUsd) < 10000000
--	B.FlowType = 'AH' OR B.FlowType = 
	--AND (CreatedDate >= '2017-10-24 00:00:00.000000' AND CreatedDate <= '2017-10-28 00:00:00.000000') 
	--AND NewBook = 'B' -- AND Toxicity >= 4
--	AND HedgeFraction < -0.6
 -- AND A.Client = 'DIVISALIVE/70160'
 -- WHERE A.CreatedDate >= '2018-02-02 00:00:00.000000' AND A.CreatedDate <= '2018-02-10 00:00:00.000000'
  Order BY ABS(NetPnL) DESC