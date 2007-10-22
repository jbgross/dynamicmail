using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Edu.Psu.Ist.DynamicMail
{
    public class Logger
    {
        //singlton class instance
        private static Logger instance=null;
        private static readonly object padlock = new object();

        //string to hold the location of the folder to hold the various output and input files
        private String logLocation = "C:\\Windows\\Temp\\";
        public String LogLocation
        {
            get { return logLocation; }
            set { logLocation = value; }
        }

        //Filename of the log file
        private String logFileName = "DynamicMailLog.txt";
        public String LogFileName
        {
            get { return logFileName; }
            set { logFileName = value; }
        }
        
        //public constructor
        private Logger()
        {
            
        }

        //method to get the singelton instance of indexes
        public static Logger Instance
        {
            get
            {
                //lock to make the singleton class thread safe
                lock (padlock)
                {
                    //if there is no current instance create a new one
                    if (instance == null)
                    {
                        instance = new Logger();
                    }
                    //return the sigelton instance
                    return instance;
                }
            }

            private set
            {
                instance = value;
            }
        }

        public void logMessage(String message)
        {
            // create a writer and open the file
            StreamWriter tw = File.AppendText(LogLocation + LogFileName);

            
            // write a line of text to the file
            tw.WriteLine(DateTime.Now.Year+"-"+DateTime.Now.Month+"-"+DateTime.Now.Day+"-"+DateTime.Now.Hour+":"+DateTime.Now.Minute+":"+DateTime.Now.Second+" \t"+message);
            
            // close the stream
            tw.Close();
        }
    }
}
