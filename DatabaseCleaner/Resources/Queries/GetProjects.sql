SELECT
	COUNT(*) AS  duplicates ,[TITLE],[Period],[CompanyName],[PROJECT],[STARTDATE],[ENDDATE],[HOURS],[TYPE],[SOFTWARE],[DESC],[TECHNEW],[TECH],[PROB],[OPLO],[METH],[ZELF],[PRIN],[Opmerkingen],[Vragen Senter],[Afgewezen]
FROM ((WBSO_P  
	INNER JOIN [Periode] ON [WBSO_P].[Periode] = [Periode].[PeriodID])
	INNER JOIN [Customers] ON [WBSO_P].[CustomerID] = [Customers].[CustomerID])
GROUP BY 
	 [TITLE],[Period],[CompanyName],[STARTDATE],[ENDDATE],[PROJECT],[HOURS],[TYPE],[SOFTWARE],[DESC],[TECHNEW],[TECH],[PROB],[OPLO],[METH],[ZELF],[PRIN],[Opmerkingen],[Vragen Senter],[Afgewezen]
ORDER BY
	[CompanyName],[TITLE]