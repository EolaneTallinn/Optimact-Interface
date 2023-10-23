using SAP.Middleware.Connector;
using SapBapiService.Classes;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Xml.Linq;
using ReadCredentialNET462;
using System.Windows.Forms;

namespace SapBapiSerice_Main
{

    class ServiceMain
    {
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // This table defines the status of each file: 0 - Not generated, 1 - Generated in live environment, 2 - Generated in test environmnent
            // Becareful of the indexes: In the table it goes from 0 to 6, while the file are from 1 to 7
            int[] FileGenerationStatus = { 1, 1, 2, 2, 2, 0, 1 };

            // Other variable initialization
            int FileNumber = 0;
            SAP_Payload objPayload = null;
            System.Data.DataTable pload_fmt_output = null;

            for (int i = 0; i < FileGenerationStatus.Length; i++)
            {
                // Get file number from index
                FileNumber = i + 1;
                if (FileGenerationStatus[i]!=0)
                {
                    // Generate the payload which contains all the connection info along with the function details and parameters. RFC data is inside
                    objPayload = SAP_PayloadManager.GeneratePayloadToSap(FileNumber);

                    try
                    {
                        // Function is called
                        Console.WriteLine("Invoking a payload...");
                        objPayload.pload.Invoke(objPayload.rfcDestination);
                        Console.WriteLine("Payload has been succesfully invoked");

                        // Converting the result of the function into a table
                        pload_fmt_output = SAP_PayloadManager.ToDataTable(objPayload.sapTable, "table");

                        // Writing the table in the CSV. The save location and name depends on the generation status
                        pload_fmt_output.ToCSV(FileNumber, FileGenerationStatus[i]);

                        // RFC Connection closed
                        RfcSessionManager.EndContext(objPayload.rfcDestination);
                        RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
                    }
                    catch (Exception ex)
                    {
                        RfcSessionManager.EndContext(objPayload.rfcDestination);
                        RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
                        Thread.Sleep(50);
                        Console.Write(ex.Message);
                    }
                }

            }
            /*
            SAP_Payload objPayload = SAP_PayloadManager.GeneratePayloadToSap(7);

            try
            {
                Console.WriteLine("Invoking a payload...");
                objPayload.pload.Invoke(objPayload.rfcDestination);
                Console.WriteLine("Payload has been succesfully invoked");

                System.Data.DataTable pload_fmt_output = SAP_PayloadManager.ToDataTable(objPayload.sapTable, "table");

                pload_fmt_output.ToCSV(7);

                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
            }
            catch (Exception ex)
            {
                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
                Thread.Sleep(50);
                Console.Write(ex.Message);
            }
            

            SAP_Payload objPayload = SAP_PayloadManager.GeneratePayloadToSap(1);

            try
            {
                Console.WriteLine("Invoking a payload...");
                objPayload.pload.Invoke(objPayload.rfcDestination);
                Console.WriteLine("Payload has been succesfully invoked");

                System.Data.DataTable pload_fmt_output = SAP_PayloadManager.ToDataTable(objPayload.sapTable, "table");

                pload_fmt_output.ToCSV(1);

                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
            }
            catch (Exception ex)
            {
                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
                Thread.Sleep(50);
                Console.Write(ex.Message);
            }

            objPayload = SAP_PayloadManager.GeneratePayloadToSap(2);

            try
            {
                Console.WriteLine("Invoking a payload...");
                objPayload.pload.Invoke(objPayload.rfcDestination);
                Console.WriteLine("Payload has been succesfully invoked");

                System.Data.DataTable pload_fmt_output = SAP_PayloadManager.ToDataTable(objPayload.sapTable, "table");

                pload_fmt_output.ToCSV(2);

                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
            }
            catch (Exception ex)
            {
                RfcSessionManager.EndContext(objPayload.rfcDestination);
                RfcDestinationManager.UnregisterDestinationConfiguration(objPayload.connectionConfig);
                Thread.Sleep(50);
                Console.Write(ex.Message);
            }
            */

        }

    }

}