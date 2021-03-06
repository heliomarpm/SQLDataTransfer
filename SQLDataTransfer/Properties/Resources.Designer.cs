//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLDataTransfer.Properties {
    using System;
    
    
    /// <summary>
    ///   Uma classe de recurso de tipo de alta segurança, para pesquisar cadeias de caracteres localizadas etc.
    /// </summary>
    // Essa classe foi gerada automaticamente pela classe StronglyTypedResourceBuilder
    // através de uma ferramenta como ResGen ou Visual Studio.
    // Para adicionar ou remover um associado, edite o arquivo .ResX e execute ResGen novamente
    // com a opção /str, ou recrie o projeto do VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Retorna a instância de ResourceManager armazenada em cache usada por essa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SQLDataTransfer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Substitui a propriedade CurrentUICulture do thread atual para todas as
        ///   pesquisas de recursos que usam essa classe de recurso de tipo de alta segurança.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a DECLARE @CHECKIDENT VARCHAR(MAX)
        ///DECLARE @NM_CHAVE VARCHAR(MAX)
        ///
        ///BEGIN TRY
        ///	SELECT
        ///		@NM_CHAVE = ISNULL(C.NAME,&apos;&apos;)
        ///	FROM
        ///		SYS.OBJECTS             O
        ///		INNER JOIN SYS.SCHEMAS  S ON O.SCHEMA_ID = S.SCHEMA_ID
        ///		LEFT JOIN SYS.COLUMNS   C ON C.OBJECT_ID = O.OBJECT_ID AND C.IS_IDENTITY = 1
        ///	WHERE
        ///		O.IS_MS_SHIPPED = 0 
        ///		AND O.TYPE = &apos;U&apos; 
        ///		AND S.NAME + &apos;.&apos; + O.NAME = @NOMETB
        /// 
        ///	IF (ISNULL(@NM_CHAVE,&apos;&apos;) &lt;&gt; &apos;&apos;)
        ///	BEGIN
        ///		SET @CHECKIDENT = &apos;DECLARE @ULTCHAVE AS INT; &apos;
        ///		SET @CHECKIDENT = @CHECKIDEN [o restante da cadeia de caracteres foi truncado]&quot;;.
        /// </summary>
        internal static string checkident {
            get {
                return ResourceManager.GetString("checkident", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a SET NOCOUNT ON;
        ///
        ///CREATE TABLE #TB_TABLES (NM_TABLE VARCHAR(50), NM_COLUNA VARCHAR(50))
        ///CREATE TABLE #TB_TABLES_R (NM_TABLE VARCHAR(50), NM_COLUNA VARCHAR(50), RCOUNT INT)
        ///
        ///INSERT INTO #TB_TABLES
        ///SELECT S.NAME + &apos;.[&apos; + O.NAME + &apos;]&apos; AS NM_TABLE, &apos;[&apos; + C.NAME + &apos;]&apos; AS NM_COLUNA
        ///FROM
        ///    SYS.OBJECTS O
        ///    INNER JOIN SYS.SCHEMAS S ON S.SCHEMA_ID = O.SCHEMA_ID
        ///    INNER JOIN SYS.COLUMNS C ON C.OBJECT_ID = O.OBJECT_ID
        ///WHERE
        ///    C.IS_IDENTITY = 1
        ///    AND O.IS_MS_SHIPPED = 0
        ///    AND O.TYPE = &apos;U&apos;
        ///    A [o restante da cadeia de caracteres foi truncado]&quot;;.
        /// </summary>
        internal static string checkident_all_tables {
            get {
                return ResourceManager.GetString("checkident_all_tables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a EXEC (&apos;SET IDENTITY_INSERT &apos; +  @NOMETB + &apos; ON;&apos;)
        ///.
        /// </summary>
        internal static string disable_set_identity {
            get {
                return ResourceManager.GetString("disable_set_identity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a EXEC (&apos;SET IDENTITY_INSERT &apos; +  @NOMETB + &apos; OFF;&apos;)
        ///.
        /// </summary>
        internal static string enable_set_identity {
            get {
                return ResourceManager.GetString("enable_set_identity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a SELECT
        ///    LOWER(S.NAME) + &apos;.&apos; + UPPER(O.NAME) AS NM_TABLE, ISNULL(MAX(C.NAME),&apos;&apos;) AS NM_COLUNA
        ///FROM
        ///    SYS.OBJECTS             O
        ///    INNER JOIN SYS.SCHEMAS  S ON O.schema_id = S.SCHEMA_ID
        ///    LEFT JOIN SYS.COLUMNS   C ON C.OBJECT_ID = O.OBJECT_ID AND C.IS_IDENTITY = 1
        ///WHERE
        ///    O.IS_MS_SHIPPED = 0 AND O.TYPE = &apos;U&apos; AND O.NAME NOT LIKE &apos;SYS%&apos;
        ///GROUP BY S.NAME, O.NAME
        ///ORDER BY S.NAME, O.NAME
        ///.
        /// </summary>
        internal static string get_tables {
            get {
                return ResourceManager.GetString("get_tables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a SET NOCOUNT ON
        ///BEGIN TRY
        /// EXEC(&apos;TRUNCATE TABLE &apos; + @NOMETB)
        ///END TRY
        ///BEGIN CATCH
        ///    SET ROWCOUNT 2000
        ///    EXEC (&apos;WHILE ((SELECT COUNT(1) FROM &apos; + @NOMETB + &apos; (NOLOCK)) &gt; 0) DELETE FROM &apos; + @NOMETB)
        ///    SET ROWCOUNT 0
        ///END CATCH
        ///SET NOCOUNT OFF
        ///.
        /// </summary>
        internal static string truncate {
            get {
                return ResourceManager.GetString("truncate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Consulta uma cadeia de caracteres localizada semelhante a declare @cmd nvarchar(max) = N&apos;UPDATE STATISTICS &apos; + @NOMETB + &apos; WITH FULLSCAN&apos;;
        ///exec sp_executesql @cmd;
        ///.
        /// </summary>
        internal static string update_statistics {
            get {
                return ResourceManager.GetString("update_statistics", resourceCulture);
            }
        }
    }
}
