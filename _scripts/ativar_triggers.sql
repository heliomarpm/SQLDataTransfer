SET NOCOUNT ON
DECLARE @LINHACOMANDO VARCHAR(MAX),
  @RETORNO  INT = 0 
DECLARE LINHA CURSOR
FOR 
 SELECT
  'ENABLE TRIGGER ' + S.NAME + '.' + O.NAME + ' ON ' + S.NAME + '.' + OBJECT_NAME(O.PARENT_OBJ)
 FROM
  SYSOBJECTS    O
  INNER JOIN SYS.SCHEMAS S ON O.UID = S.SCHEMA_ID 
 WHERE
  O.XTYPE = 'TR'
  AND  S.NAME + '.' + OBJECT_NAME(O.PARENT_OBJ) = @NOMETB
   
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

