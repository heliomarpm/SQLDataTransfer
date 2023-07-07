SET NOCOUNT ON 
DECLARE @LINHACOMANDO VARCHAR(MAX),
  @RETORNO  INT = 0
DECLARE LINHA CURSOR
FOR 
 SELECT
  'ALTER TABLE ' + TABLE_SCHEMA + '.' + TABLE_NAME + ' CHECK CONSTRAINT ' + CONSTRAINT_NAME + ';' AS COMANDO
 FROM
  INFORMATION_SCHEMA.TABLE_CONSTRAINTS
 WHERE
  CONSTRAINT_TYPE = 'FOREIGN KEY'
OPEN LINHA 
FETCH NEXT FROM LINHA INTO @LINHACOMANDO  
WHILE (@@FETCH_STATUS = 0) 
BEGIN 
 EXEC (@LINHACOMANDO)
 
 SET @RETORNO += 1
 FETCH NEXT FROM LINHA INTO @LINHACOMANDO 
END  
CLOSE LINHA 
DEALLOCATE LINHA 
SELECT @RETORNO AS RETORNO;
SET NOCOUNT OFF
