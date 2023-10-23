using System;
using System.Data;
using SAP.Middleware.Connector;

namespace SapBapiService.Classes
{
    public class SAP_Payload
    {
        public IRfcFunction pload { get; set; }
        public IRfcTable sapTable { get; set; }
        public RfcDestination rfcDestination { get; set; }
        public SAP_ConfigurationManager connectionConfig { get; set; }
        public SAP_Payload(IRfcFunction payload, IRfcTable table, RfcDestination rfcDestination, SAP_ConfigurationManager connectionConfig)
        {
            pload = payload;
            sapTable = table;
            this.rfcDestination = rfcDestination;
            this.connectionConfig = connectionConfig;
        }
    }
    public static class SAP_PayloadManager
    {
        public static SAP_Payload GeneratePayloadToSap(int SelectedFileNum)
        {
            SAP_ConfigurationManager connectionConfig = null;
            RfcDestination rfcDestination = null;

            try
            {
                switch (SelectedFileNum)
                {
                    case 1:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_MATERIAL_1");

                            GeneratedPayload.SetValue("I_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_MATERIAL");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                    case 2:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_SLDPRD_2A");

                            GeneratedPayload.SetValue("I_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_SLSPRD");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                    case 3:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_PURPRD_3A");

                            GeneratedPayload.SetValue("I_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_PURPRD");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                    case 4:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_BOM4");

                            GeneratedPayload.SetValue("P_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_BOM");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                    case 5:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_SCHAGR_FRCST_5A");

                            GeneratedPayload.SetValue("P_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_FRCST");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                    case 6:
                        {
                            return null;
                        }
                    case 7:
                        {
                            connectionConfig = new SAP_ConfigurationManager();
                            connectionConfig.SelectedFileNum = SelectedFileNum;

                            RfcDestinationManager.RegisterDestinationConfiguration(connectionConfig);

                            rfcDestination = RfcDestinationManager.GetDestination("PEO");

                            RfcRepository repo = rfcDestination.Repository;
                            IRfcFunction GeneratedPayload = repo.CreateFunction("ZOPTI_STOCK_7");

                            GeneratedPayload.SetValue("I_WERKS", "P200");

                            IRfcTable dataTable = GeneratedPayload.GetTable("ET_STOCK");

                            return new SAP_Payload(GeneratedPayload, dataTable, rfcDestination, connectionConfig);
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static DataTable ToDataTable(this IRfcTable sapTable, string name)
        {
            DataTable adoTable = new DataTable(name);

            for (int liElement = 0; liElement < sapTable.ElementCount; liElement++)
            {
                RfcElementMetadata metadata = sapTable.GetElementMetadata(liElement);
                adoTable.Columns.Add(metadata.Name, GetDataType(metadata.DataType));
            }

            foreach (IRfcStructure row in sapTable)
            {
                DataRow ldr = adoTable.NewRow();
                for (int liElement = 0; liElement < sapTable.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = sapTable.GetElementMetadata(liElement);

                    switch (metadata.DataType)
                    {
                        case RfcDataType.DATE:
                            ldr[metadata.Name] = row.GetString(metadata.Name).Substring(0, 4) + row.GetString(metadata.Name).Substring(5, 2) + row.GetString(metadata.Name).Substring(8, 2);
                            break;
                        case RfcDataType.BCD:
                            ldr[metadata.Name] = row.GetDecimal(metadata.Name);
                            break;
                        case RfcDataType.CHAR:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.STRING:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                        case RfcDataType.INT2:
                            ldr[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.INT4:
                            ldr[metadata.Name] = row.GetInt(metadata.Name);
                            break;
                        case RfcDataType.FLOAT:
                            ldr[metadata.Name] = row.GetDouble(metadata.Name);
                            break;
                        default:
                            ldr[metadata.Name] = row.GetString(metadata.Name);
                            break;
                    }
                }
                adoTable.Rows.Add(ldr);
            }
            return adoTable;
        }

        private static Type GetDataType(RfcDataType rfcDataType)
        {
            switch (rfcDataType)
            {
                case RfcDataType.DATE:
                    return typeof(string);
                case RfcDataType.CHAR:
                    return typeof(string);
                case RfcDataType.STRING:
                    return typeof(string);
                case RfcDataType.BCD:
                    return typeof(decimal);
                case RfcDataType.INT2:
                    return typeof(int);
                case RfcDataType.INT4:
                    return typeof(int);
                case RfcDataType.FLOAT:
                    return typeof(double);
                default:
                    return typeof(string);
            }
        }
    }
}
