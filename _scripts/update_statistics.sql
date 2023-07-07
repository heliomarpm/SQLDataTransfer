declare @cmd nvarchar(max) = N'UPDATE STATISTICS ' + @NOMETB + ' WITH FULLSCAN';
exec sp_executesql @cmd;

