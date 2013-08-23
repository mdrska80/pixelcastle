using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Castles.Conf
{
    public class CastlesConfigurationReader
    {
        #region Fields
        /// <summary>
        /// Název konfiguračního souboru
        /// </summary>
        private static string _configurationFile = null;

        /// <summary>
        /// Zámek objektu pro multithread přístup
        /// </summary>        
        private static readonly object ConfigurationFileLock = new object();        
                /// <summary>
        /// Načtený konfigurační objekt
        /// </summary>
        private static CastlesConfiguration _configuration = null;

        /// <summary>
        /// Zámek pro thread-safe přístup
        /// </summary>
        private readonly static Object Locker = new Object();
        #endregion Fields

        #region Properties
        /// <summary>
        /// Název souboru s konfigurací včetně plné cesty
        /// </summary>
        public static String ConfigurationFile
        {
            get 
            {
                if (_configurationFile != null) return _configurationFile;
                lock( ConfigurationFileLock) 
                {
                    if (_configurationFile == null) {
                        _configurationFile = FindConfigurationFileName();
                    }
                    return _configurationFile;
                }
            }
        }

        public static string ConfigurationFilePath
        {
            get { 
                string s = ConfigurationFile;
                if (string.IsNullOrEmpty(s)) return s;
                string rv = Path.GetDirectoryName(s);
                return rv;
            }
        }

        private static string _logFilesDirectory = null;
        public static string LogFilesDirectory
        {
            get
            { 
                // lock se nevyplati, jen pri startu se to nastavi mozna vic nez jednou, ale k chybe nedojde
                // nepouzivam primo static string  _LogFilesDirectory = _LogFilesDirectory =Path.Combine(ConfigurationFilePath, "LogFiles");
                // protoze se to pak velmi spatne ladi v pripade problemu
                if (_logFilesDirectory == null) {
                    _logFilesDirectory =Path.Combine(ConfigurationFilePath, "LogFiles");
                }
                return _logFilesDirectory;
            }
        }


        #endregion Properties



        #region CCtor
        /// <summary>
        /// Defaultní konstruktor, načte konfiguraci ze souboru
        /// </summary>
        private CastlesConfigurationReader()
        {
            // thread-safe lock
            lock (Locker)
            {                
                FileStream stream = null;
                try
                {
                    // načtení ze streamu
                    try
                    {
                        stream = new FileStream(ConfigurationFile, FileMode.Open, FileAccess.Read);
                    }
                    catch (FileNotFoundException fnfex)
                    {
                        Console.WriteLine("Configuration file '" + ConfigurationFile + "' doesn't exists. " + fnfex.Message);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error reading configuration file. [" + ConfigurationFile + "] " + ex.Message);
                        throw;
                    }

                    // deserializace
                    try
                    {
                        var configFileSerializer = new XmlSerializer(typeof(CastlesConfiguration));
                        _configuration = (CastlesConfiguration)configFileSerializer.Deserialize(stream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Configuration file '" + ConfigurationFile + "' is not valid xml file. " + ex.Message);
                        throw;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (stream != null) stream.Close();
                }
            }
        }
        #endregion Cctor

        #region Methods
        /// <summary>
        /// Vrátí konfigurační objekt
        /// Při vícenásobném volání vrací stále stejnou instanci, objekt není vytvářen znovu, pokud již byl vytvořen dříve
        /// </summary>
        /// <returns>Objekt konfigurace</returns>
        public static CastlesConfiguration GetConfiguration()
        {
            // pokud je vyvořena, dá se vrátit bez zámku.
            if (_configuration != null) return _configuration;
            lock (Locker)
            {
                // má smysl znovu testovat, protože mohla neexistovat při prvním testu ale zámek jsem získal až jako druhý
                if (_configuration == null)
                {
                    new CastlesConfigurationReader();
                }
                return _configuration;
            }
        }

        /// <summary>
        /// Vrátí cestu ke konfiguračnímu souboru
        /// </summary>
        /// <returns>Cesta ke konfiguračnímu souboru</returns>
        public static String GetConfigurationFile()
        {
            return ConfigurationFile;
        }        
        
        /// <summary>
        /// Vrátí lokální id aplikace podle nastavení v app.config
        /// </summary>
        /// <returns>Lokální id aplikace</returns>
        public static String GetLocalAppId()
        {
			return "0";//LocalAppId.GetLocalAppId().ToString();
        }

        #region Private
        /// <summary>
        /// Vrati cestu k konfiguracnimu souboru. Soubor je bud v adresari aplikace nebo v nejakem nadrizenem.
        /// Jmeno je pevne dane, <see cref="ConfigFileName"/>
        /// </summary>
        /// <returns></returns>
        private static string FindConfigurationFileName()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var rv = FindConfigurationFileName(directory);
            return rv;
        }

        /// <summary>
        /// Jmeno souboru s konfiguraci
        /// </summary>
        private const string ConfigFileName = @"CastlesConfiguration.xml";

        /// <summary>
        /// Najde konfigurační soubor
        /// </summary>
        /// <param name="directory">Název složky, kde se začíná hledat</param>
        /// <returns>Název konfiguračního souboru</returns>
        private static string FindConfigurationFileName(string directory)
        {
            if (string.IsNullOrEmpty(directory )) return null;
            var fn =Path.Combine(directory, ConfigFileName );
            if (File.Exists(fn))
            {
                return fn;
            }
            if (directory.Length > 3) // Pokud cestas konci \ tak by mi GetDirectoryName vratilo totez a mel bych nekonecnou smycku
            {
                directory = directory.TrimEnd('\\');
                directory = directory.TrimEnd('/');
            }
            var upperDir = Path.GetDirectoryName(directory);
            return FindConfigurationFileName(upperDir);
        }
        #endregion Private

        #endregion Methods
    }
}
