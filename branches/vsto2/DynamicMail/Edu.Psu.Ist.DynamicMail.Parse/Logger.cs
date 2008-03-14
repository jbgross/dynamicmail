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
        private static String localAccountAddress;

        public static String LocalAccountAddress
        {
            get { return Logger.localAccountAddress; }
            set { Logger.localAccountAddress = value; }
        }

        private String fileName = "c:\\" + localAccountAddress + "log.txt";
        private StreamWriter writer;
        
        /// <summary>
        /// constructor
        /// </summary>
        private Logger()
        {
            // create a writer and open the file
            this.writer = File.AppendText(this.fileName);
            this.writer.AutoFlush = true;
            this.logMessage("Logger started.");
        }

        /// <summary>
        /// method to get the singelton instance of indexes
        /// </summary>
        public static Logger Instance
        {
              //if there is no current instance create a new one
            get 
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                //return the sigelton instance
                return instance;
            }
        }

        /// <summary>
        /// Appends Time/Date and String message to the StreamWriter
        /// </summary>
        /// <param name="message"></param>
        public void logMessage(String message)
        {
            lock (this)
            {
                // write a line of text to the file
                tw.WriteLine(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-"
                    + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second
                    + " " + message);

                // close the stream
                tw.Close();
            }
        }

        /// <summary>
        /// Close down the writer when application exits
        /// </summary>
        public void Close()
        {
            if (this.writer != null)
            {
                this.logMessage("Logger stopped.");
                this.writer.Close();
                this.writer.Dispose();
                this.writer = null;
            }
        }

        /// <summary>
        /// At the end, close if necessary
        /// </summary>
        private void Finalize()
        {
            this.Close();
        }
    }
}
