-- Desabilitar as triggers de uma tabela específica
--DECLARE @NOMETB NVARCHAR(128) = 'NomeDaTabela'; -- Substitua pelo nome da tabela desejada

DECLARE @sql NVARCHAR(MAX) = '';
DECLARE @rcount INT = 0; 

SELECT @rcount = @rcount + 1, @sql = @sql + N'ENABLE TRIGGER ' + QUOTENAME(name) + ' ON ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_id)) + ';' + CHAR(13)
FROM sys.triggers
WHERE parent_id = OBJECT_ID(@NOMETB) AND is_disabled = 1;

EXEC sp_executesql @sql;

SELECT @rcount AS QUANTITY;