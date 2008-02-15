using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Edu.Psu.Ist.DynamicMail.Psu
{
    public class Logger
    {
        /// <summary>
        /// singlton class instance
        /// </summary>
        private static Logger instance=null;
        private static readonly object padlock = new object();

        /// <summary>
        /// string to hold the location of the folder to hold the various output and input files
        /// </summary>
        private String logLocation = "C:\\Windows\\Temp\\";
        public String LogLocation
        {
            get { return logLocation; }
            set { logLocation = value; }
        }

        /// <summary>
        /// Filename of the log file
        /// </summary>
        private String logFileName = "DynamicMailLog.txt";
        public String LogFileName
        {
            get { return logFileName; }
            set { logFileName = value; }
        }
        
        //
        /// <summary>
        /// public constructor
        /// </summary>
        private Logger()
        {
            
        }

        /// <summary>
        /// method to get the singelton instance of indexes
        /// </summary>
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

        /// <summary>
        /// Appends Time/Date and String message to the StreamWriter
        /// </summary>
        /// <param name="message"></param>
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
