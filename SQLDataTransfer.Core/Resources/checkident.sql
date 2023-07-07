DECLARE @CHECKIDENT VARCHAR(MAX)
DECLARE @NM_CHAVE VARCHAR(MAX)

BEGIN TRY
	SELECT
		@NM_CHAVE = ISNULL(C.NAME,'')
	FROM
		SYS.OBJECTS             O
		INNER JOIN SYS.SCHEMAS  S ON O.SCHEMA_ID = S.SCHEMA_ID
		LEFT JOIN SYS.COLUMNS   C ON C.OBJECT_ID = O.OBJECT_ID AND C.IS_IDENTITY = 1
	WHERE
		O.IS_MS_SHIPPED = 0 
		AND O.TYPE = 'U' 
		AND S.NAME + '.' + O.NAME = @NOMETB
 
	IF (ISNULL(@NM_CHAVE,'') <> '')
	BEGIN
		SET @CHECKIDENT = 'DECLARE @ULTCHAVE AS INT; '
		SET @CHECKIDENT = @CHECKIDENT + 'SET @ULTCHAVE = ISNULL((SELECT MAX(' + @NM_CHAVE + ') FROM ' + @NOMETB + ' (NOLOCK)), 1); '
		SET @CHECKIDENT = @CHECKIDENT + 'DBCC CHECKIDENT('''+ @NOMETB +''', RESEED, @ULTCHAVE); '
 
		EXEC (@CHECKIDENT);
	END
END TRY
BEGIN CATCH
END CATCH 
