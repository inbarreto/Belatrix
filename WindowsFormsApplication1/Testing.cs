using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace WindowsFormsApplication1
{


    public class Loggear
    {
        public bool _logToFile {get;set;}
        public bool _logToConsole{get;set;}
        public bool _logMessage{get;set;}
        public bool _logWarning{get;set;}
        public bool _logError{get;set;}
        public bool LogToDatabase{get;set;}
        public bool _initialized{get;set;}
        public string mensaje{get;set;}

        public Loggear(){}
        public Loggear(bool inicializado,bool grabarEnConsola,bool logMensaje,bool logWarning,bool logError,bool logIntoDB){
            this._initialized =inicializado;
            this._logToConsole= grabarEnConsola;
            this._logMessage=logMensaje;
            this._logWarning = logWarning;
            this._logError =logError;
            this.LogToDatabase=logIntoDB;
        }            
    }

public enum Mensajes{
    Mensajes,
    Warning,
    Error
}

    public interface IGuardaBaseDeDato{

         void GuardarLogDB(Loggear log);
    }

    public interface IGuardaEnArchivo{

         void GuardaLogEnArchivo(Loggear log);
    }

      public interface IMuestraPorPantalla{

        void MuestrPorPantalla(Loggear log);
    }


    public class GuardaEnBaseDeDatos : IGuardaBaseDeDato{

        public void GuardarLogDB(Loggear log)
        {
            int t;
            if (log._logMessage)
                t=1;
            else if(log._logError)
                t=2;
            else 
                t=3;
             System.Data.SqlClient.SqlConnection connection = new
        System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
        connection.Open();
             System.Data.SqlClient.SqlCommand command = new
        System.Data.SqlClient.SqlCommand("Insert into Log Values('" + log.mensaje + "', " +
        t.ToString() + ")");
        command.ExecuteNonQuery();

        }
    }

    public class GuardaEnArchivo : IGuardaEnArchivo
    {
        public void GuardaLogEnArchivo(Loggear log)
        {
            string l = string.Empty ;
        if
        (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
        {
            l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
        }
        if (log._logError)
        {
        l +=  Mensajes.Mensajes + DateTime.Now.ToShortDateString() + log._logMessage;
        }
        if (log._logWarning)
        {
        l +=  Mensajes.Warning + DateTime.Now.ToShortDateString() + log._logWarning;
        }
        if (log._logMessage)
        {
        l += Mensajes.Error + DateTime.Now.ToShortDateString() + log._logMessage;
        }

        System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings[
        "LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);

        }
    }

    public class MuestraPorConsola : IMuestraPorPantalla
    {
        public  void MuestrPorPantalla(Loggear log)
        {
         if (log._logError)
        {
        Console.ForegroundColor = ConsoleColor.Red;
        }
        if (log._logWarning)
        {
        Console.ForegroundColor = ConsoleColor.Yellow;
        }
        if (log._logMessage)
        {
        Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(DateTime.Now.ToShortDateString() + log.mensaje);
     
        }       
    }

public class JobLogger
{

public void LogMessage(Loggear log)
{        
    if (log==null)
    {
        if (!log._logToConsole&& !log._logToFile&& !log.LogToDatabase)
                 ThrowException("Configuración Invalida");
        
        if (!log._logError && !log._logMessage&& !log._logWarning) 
                ThrowException("Error or Warning or Message must be specified");
        
        MuestraPorConsola consola = new MuestraPorConsola();        
        GuardaEnArchivo archivo = new GuardaEnArchivo();
        GuardaEnBaseDeDatos DB = new GuardaEnBaseDeDatos();
        if(log._logToConsole)
            consola.MuestrPorPantalla(log);
        if(log._logToFile)
            archivo.GuardaLogEnArchivo(log);
        if(log.LogToDatabase)
            DB.GuardarLogDB(log);

    }                     
}
    public Exception ThrowException(string mensaje)
    {
        throw new Exception(mensaje);
    }
}
}