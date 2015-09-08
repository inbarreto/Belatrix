using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;

namespace TestUnitario
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Loggear log = new Loggear();

            JobLogger jobLog = new JobLogger();
            log._logWarning = false;
            log._logToFile = true;
            log._logToConsole = false;
            log._logError = true;
            log.mensaje = "Prueba de logeo de Belatrix .Net!";
            jobLog.LogMessage(log);
        }


        [TestMethod]
        public void TestMethod2()
        {
            Loggear log = new Loggear();

            JobLogger jobLog = new JobLogger();
            log._logWarning = true;
            log._logToFile = false;
            log._logToConsole = true;
            log._logError = true;
            log.LogToDatabase = true;
            log.mensaje = "Prueba de logeo de Belatrix .Net!";
            jobLog.LogMessage(log);
        }


        [TestMethod]
        public void TestMethod3()
        {
            Loggear log = new Loggear();

            JobLogger jobLog = new JobLogger();
            log._logWarning = false;
            log._logToFile = false;
            log._logToConsole = true;
            log._logError = true;
            log.LogToDatabase = false;
            log.mensaje = "Prueba de logeo de Belatrix .Net!";
            jobLog.LogMessage(log);
        }
    }
}
