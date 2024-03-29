SET NOCOUNT ON 
CREATE TABLE #TB_TABLES (NM_TABLE VARCHAR(50), NM_COLUNA VARCHAR(50))
CREATE TABLE #TB_TABLES_R (NM_TABLE VARCHAR(50), NM_COLUNA VARCHAR(50), RCOUNT INT)
INSERT INTO #TB_TABLES
SELECT S.NAME + '.[' + O.NAME + ']' AS NM_TABLE, '[' + C.NAME + ']' AS NM_COLUNA
FROM
 SYS.OBJECTS O
 INNER JOIN SYS.SCHEMAS S ON S.SCHEMA_ID = O.SCHEMA_ID
 INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = O.OBJECT_ID
WHERE
 C.IS_IDENTITY = 1
 AND O.IS_MS_SHIPPED = 0
 AND O.TYPE = 'U'
 AND O.NAME NOT LIKE 'SYS%'
DECLARE @NM_TABLE   VARCHAR(50),
        @NM_COLUNA  VARCHAR(50),
        @RCOUNT     INT,
        @SQL_TOP1   VARCHAR(200)
                
DECLARE LINHA CURSOR
FOR 
    SELECT NM_TABLE, NM_COLUNA FROM #TB_TABLES ORDER BY NM_TABLE
OPEN LINHA 
FETCH NEXT FROM LINHA INTO @NM_TABLE, @NM_COLUNA
WHILE (@@FETCH_STATUS = 0) 
BEGIN
    SET @SQL_TOP1 = 'SELECT TOP 1 ''@NM_TABLE'', ''@NM_COLUNA'', @NM_COLUNA FROM @NM_TABLE WITH(NOLOCK) ORDER BY @NM_COLUNA DESC'
    SET @SQL_TOP1 = REPLACE(@SQL_TOP1, '@NM_TABLE', @NM_TABLE)
    SET @SQL_TOP1 = REPLACE(@SQL_TOP1, '@NM_COLUNA', @NM_COLUNA)
   
    INSERT INTO #TB_TABLES_R
 EXEC(@SQL_TOP1)
 
 IF (@@ROWCOUNT=0)
        INSERT INTO #TB_TABLES_R VALUES(@NM_TABLE, @NM_COLUNA, 1)
 
    FETCH NEXT FROM LINHA INTO @NM_TABLE, @NM_COLUNA
END  
CLOSE LINHA 
DEALLOCATE LINHA
 
DECLARE @COMANDO NVARCHAR(500)        
DECLARE LINHA CURSOR
FOR 
    SELECT NM_TABLE, NM_COLUNA, RCOUNT FROM #TB_TABLES_R ORDER BY NM_TABLE
OPEN LINHA 
FETCH NEXT FROM LINHA INTO @NM_TABLE, @NM_COLUNA, @RCOUNT
WHILE (@@FETCH_STATUS = 0) 
BEGIN
 SET @COMANDO ='DBCC CHECKIDENT('''+ @NM_TABLE + ''', RESEED, ' + CAST(@RCOUNT AS VARCHAR(20)) + ')'
 --PRINT @COMANDO
 EXEC (@COMANDO)
    FETCH NEXT FROM LINHA INTO @NM_TABLE, @NM_COLUNA, @RCOUNT
END  
CLOSE LINHA 
DEALLOCATE LINHA              
DROP TABLE #TB_TABLES
DROP TABLE #TB_TABLES_R

