using System;
using System.Configuration;
using System.Timers;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SRM_IPItoT4D
{
    class GNAFunctions
    {

        public void writeLicenseExpiredMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Software: SRM_IPItoT4D");
            Console.WriteLine("The license for this software has expired");
            Console.WriteLine("Please contact Julian Gray at:");
            Console.WriteLine("gna.geomatics@gmail.com");
            Console.WriteLine("+49 176 7299 7904");
            Console.WriteLine("");
            Console.ReadLine();
        }



        public Array generateIPIdetails(int iNoOfDataLoggers)
        {
           
            string[,] strIPIdetails = new string[101, 101]; 

            

            if (iNoOfDataLoggers > 100)
            {
                Console.WriteLine("");
                Console.WriteLine("The number of IPI arrays is "+ iNoOfDataLoggers);
                Console.WriteLine("This is greater than the limit the of 100 that software can handle");
                Console.WriteLine("Split the installation into block of 100 IPIs or less ");
                Console.WriteLine("");
                Console.WriteLine("Press any key to exit..");

                Console.ReadKey();
                Environment.Exit(0);
            }

            string strDatalogger, strIPIlist;

            for (int i=1; i<=iNoOfDataLoggers; i++)
            {

                strDatalogger = "configDatalogger_"+Convert.ToString(i);
                strIPIlist = "configIPIList_" + Convert.ToString(i);

                strDatalogger = Convert.ToString(ConfigurationManager.AppSettings[strDatalogger]);
                strIPIlist = Convert.ToString(ConfigurationManager.AppSettings[strIPIlist]);

                strIPIdetails[i,0] = strDatalogger;
                strIPIdetails[i, 1] = strIPIlist;
            }




            return strIPIdetails;
        }




        public string formatDataLine(string strDataLine, string strNullDataString)
        {

            //Strip leading and training blanks
            strDataLine = strDataLine.Trim();
            // Replace all missing data ",," with the null data string
            // This is not really safe but the temperature will also show zero
            string strOld = ",,";
            string strNew = "," + strNullDataString + ",";
            strDataLine = strDataLine.Replace(strOld, strNew);
            strOld = ",,";
            strNew = ",";
            strDataLine = strDataLine.Replace(strOld, strNew);

            //strip the trailing commas from the line
            string strSubString = strDataLine.Substring(strDataLine.Length - 1, 1);
            if (strSubString == ",")
            {
                strDataLine = strDataLine.Substring(0, strDataLine.Length - 1);
            }

            return strDataLine;

        }

    }
}
