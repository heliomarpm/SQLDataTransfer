SET NOCOUNT ON
BEGIN TRY
 	EXEC('TRUNCATE TABLE ' + @NOMETB)
END TRY
BEGIN CATCH
    BEGIN TRY
		SET ROWCOUNT 2000
		EXEC ('WHILE ((SELECT COUNT(1) FROM ' + @NOMETB + ' (NOLOCK)) > 0) DELETE FROM ' + @NOMETB)
		SET ROWCOUNT 0
	END TRY
	BEGIN CATCH
		DECLARE @ErrMessage NVARCHAR(MAX);
		DECLARE @ErrSeverity INT;
		DECLARE @ErrState INT;

		SELECT
			@ErrMessage = 'ErroLinha: ' + CAST(ERROR_LINE() AS VARCHAR) + ' - ' + ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE();

		RAISERROR (@ErrMessage, @ErrSeverity, @ErrState)
	END CATCH
END CATCH
SET NOCOUNT OFF
