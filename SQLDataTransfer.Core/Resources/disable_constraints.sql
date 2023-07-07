-- Desabilitar todas as foreign keys no banco de dados
DECLARE @sql NVARCHAR(MAX) = '';
DECLARE @count INT=0;

SELECT @count = @count + 1, @sql = @sql + N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) +
    ' NOCHECK CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.foreign_keys
WHERE is_disabled = 0;

EXEC sp_executesql @sql;
select @count AS QUANTITY