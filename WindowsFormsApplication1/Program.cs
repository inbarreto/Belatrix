using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Loggear log = new Loggear();

            JobLogger jobLog = new JobLogger();
            log._logWarning=false;
            log._logToFile=true;
            log._logToConsole=true;
            log._logError=true;
            log.mensaje="Prueba de logeo de Belatrix .Net!";
            jobLog.LogMessage(log);

        }
    }
}
