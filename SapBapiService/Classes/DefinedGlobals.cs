using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SapBapiService.Classes
{
    /// <summary>
    /// Class <c>DefinedGlobals<c> should include everything that should be used globaly, 
    /// never instantiate this class by an object - only use parts of it to reduce CPU/RAM usage.
    /// </summary>
    static class DefinedGlobals
    {
        //@@@ Regex section @@@
        //  When using said Regexes, you should create an array in specified class, example portrayed below.
        //
        //      Regex[] GlobalsRX = new Regex[]
        //      {
        //          DefinedGlobals.METHODNAME()
        //      };

        /// <summary>
        /// Method <c>excessWhiteSpaceRX</c> This Regex is used to determine =>5 whitespaces before any amount of digits.
        /// </summary>
        /// <returns>Regex Object</returns>
        /*
        //public static Regex excessWhiteSpaceRX = new Regex(@"^(     +)\d+$");
        //{
        //    return new Regex(@"^(     +)\d+$");
        //}

        public static Regex GetPartFromAnyFloat()
        {
            return new Regex(@"^([+-]?)(\d+)?([.,])?(\d*)?([+-]?)$");
        }

        public static Regex GetFloatWithWhole()
        {
            return new Regex(@"^([+-]?)(\d+)([.,]\d*)?([+-]?)$");
        }

        public static Regex GetFloatWithoutWhole()
        {
            return new Regex(@"^([+-]?)([.,]\d+)([+-]?)$");
        }

        public static Regex GetFloatWithThreeDecimalsRX = new Regex(@"(\d+\[.,]\d{1,3})\d*");
        //{
        //    return new Regex(@"(\d+\[.,]\d{1,3})\d*");
        //}

        public static Regex ZerosInFront = new Regex(@"(^0+)(?=\d)");
        //{
        //    return new Regex(@"(^0+)(?=\d)");
        //}

        public static Regex SpacesInFront()
        {
            return new Regex(@"^\s+");
        }
        */

        public static Regex notNull = new Regex(@".+");
        public static Regex wholeUnsignedNumber = new Regex(@"^(?:0*)(\d+)$"); //replaced by $1
        public static Regex wholeNumber = new Regex(@"^([+-]?)(?:0*)(\d+)(?:[.,]\d+)?([+-]?)$"); //replaced by $1$3$2
        public static Regex floatingNumber = new Regex(@"^([+-]?)(?:0*)(\d+)([.,]\d{1,3})?(?:\d*)([+-]?)$"); //replaced by $1$4$2$3
        public static Regex date = new Regex(@"^(\d{4})(\d{2})(\d{2})$"); //replaced by $3/$2/$1
        public static Regex trimExtended = new Regex(@"^\s+|\s+$"); //replaced by nothing

        //This list consists of collumn names which contain text data.
        public static List<string> textCaptions = new List<string>
        {
            "COL_A",
            "NAME1",
            "MAKTX",
            "PRDHA",
            "VTEXT",
            "MTART",
            "LIFNR",
            "NAME1_S",
            "BISMT",
            "WAERS",
            "MFRPN",
            "ACTION",
            "MMSTA",
            "DISPO",
            "MAABC",
            "MEINS",
            "STRGR",
            "BEZEI1",
            "BEZEI2",
            "ATWTB",
            "VENDOR_STOCK",
            "MRPGR",
            "FILE1",
            "YEARMONTH",
            "FILETYP",
            "F7",
            "F8",
            "SLS_PRODN",
            "SO_TYPE",
            "SLS_PRODN_ORDER",
            "ORDER_LINE",
            "ORDER_SUBLINE",
            "COMMENTS",
            "ORDER_REASON",
            "F34",
            "F37",
            "YEARMTH",
            "SUP_NAME",
            "TRNS_ORD_TYP_MBT",
            "PUR_PROD_ORD_TYP",
            "ORDER_COMMENTS",
            "FILE90",
            "SLOC",
            "S",
            "MATL_GROUP",
            "BUN",
            "CRCY",
            "PRODUCT_HIERARCHY",
            "NETTABLE"
        };

        //This list consists of collumn names which contain datetime data.
        public static List<string> dateCaptions = new List<string>
        {
            "CRDATE",
            "MSTDE",
            "IMPORT_DATUM",
            "DATE1",
            "ORDER_DATE",
            "REQ_DATE",
            "ST1_CONF_DT",
            "ACT_CONF_DT",
            "SHPD_CONS_DT",
            "ORDER_DATE1",
            "PUR_PROD_ORD_DT",
            "REQ_DT",
            "CONF_DT",
            "SHIP_PROD_DT",
            "VALID_SDT",
            "VALID_EDT"
        };

        //This list consists of collumn names which contain IDs (numbers processed as text but 0 are removed in front).
        public static List<string> idCaptions = new List<string>
        {
            "WERK",
            "MATNR",
            "LMATN",
            "NFMAT",
            "WRH_ID",
            "MATERIAL",
            "ARTICLE_ID",
            "SUP_ID",
            "SUP_WHS_ID",
            "SUP_PRD_ID",
            "PUR_PROD_ORD_SLN_NO",
            "PUR_PROD_ORD_NUM",
            "PUR_PROD_ORD_LN_NUM",
            "BOM_ID",
            "WAREHOUSE_ID"
        };

        //This list consists of collumn names which contain whole numbers.
        public static List<string> wholeNumberCaptions = new List<string>
        {
            "LT_PIR",
            "PEINH",
            "STOCKED",
            "PEINH_BH",
            "SPEME",
            "KPEIN",
            "BSTRTF",
            "TOTAL_CONSIG_STK",
            "NO_OF_LINES",
            "CONF_QTY",
            "SHIP_PROD_QTY",
            "OPEN_QTY_DELIVER",
            "SPECIALSTOCK_NUMBER",
            "UNRESTICTED",
            "TRANSIT_TRANSF",
            "IN_QUALITY_INSP",
            "RESTRICTED_USE",
            "BLOCKED",
            "RETURNS"
        };

        //This list consists of collumn names which contain floating numbers.
        public static List<string> floatingNumberCaptions = new List<string>
        {
            "COGS",
            "MINBM",
            "NORBM",
            "NETPR",
            "LBKUM",
            "NTGEW",
            "KBETR",
            "EISBE",
            "ORDER_QTY",
            "ST1_CONF_QTY",
            "ACT_CONF_QTY",
            "SHPD_CONS_QTY",
            "OPEN_QTY_TO_DLVR",
            "ORDER_LINE_VALUE",
            "COGS_LINE_ORDR_VAL",
            "PUR_LINE_ORD_VAL",
            "VALUE_UNRESTRICTED",
            "VAL_IN_TRANS_TFR",
            "VALUE_IN_QUALINSP",
            "VALUE_RESTRICTED",
            "VALUE_BLOCKED_STOCK",
            "VALUE_RETS_BLOCKED"
        };

        public static string ReturnHeaderNames(int fileNr)
        {
            switch (fileNr)
            {
                case 1:
                    return null;
                case 2:
                    return @"File: 2.2;YearMonth (YYYYMM);Date;File Type: D/M;Warehouse ID;Article ID;F7;F8;Sales Area;Sales Region;Customer Group;Customer Line;Sales/Production;Sales Order Type;Sales/Production Order number;Sales/Production Order line number;Sales/Production Order subline number;Sales/Production Order Date;Order Quantity;Requested Date;First Confirmed Quantity;First Confirmed Date;Actual Confirmed Quantity;Actual Confirmed Date;Shipped/Consumed Quantity;Shipped/Consumed Date;Open Quantity To Deliver;Sales Line/Production Line Order Value;COGS Line Order Value;Order Comments;Number of Lines;Sales/Production Order Date1;Order Reason;F34;FreeNumber2;FreeNumber3;F37";
                case 3:
                    return @"File: 3.2;YearMonth (YYYYMM);Date;File Type: D/M;Warehouse ID;Article ID;Supplier ID/Supplying Warehouse ID/Production Unit ID;Supplier Name;SupWhsID;SupPrdID/Routing ID;Supplier Group;BOM ID;Transaction Order Type: MBT (Make/Buy/Transfer);Purchase/Production Order Type;Purchase/Production Order number;Purchase/Production Order line number;Purchase/Production Order subline number;Purchase/Production Order Date;Order Quantity;Requested Date;Confirmed Quantity;Confirmed Date;Shipped/Produced Quantity;Shipped/Produced Date;Open Quantity To Deliver;empty;Purchase Line Order Value;Order Comments;empty_1;FreeText1;FreeText2;FreeText3;FreeNumber1;FreeNumber2;FreeNumber3";
                case 4:
                    return @"File40;Date;BOMID;WarehouseID;ArticleID;QuantityPer;BOMLineNumber;ComponentWarehouseID;ComponentID;QuantityComponent;LeadTime;ScrapPercentage;ScrapQuantity;BOMPath;ValidityStartDate;ValidityEndDate;ProcurementType;Empty1;Empty2;AlternativeBOM;Empty3;Empty4";
                case 7:
                    return @"File90;empty;Importdatum;WarehouseID;ArticleID;SLoc;S;Matl Group;Special stock number;BUn;Unrestricted;Crcy;Value Unrestricted;Transit/Transf.;Val. in Trans./Tfr;In Quality Insp.;Value in QualInsp.;Restricted-Use;Value Restricted;Blocked;Value BlockedStock;Returns;Value Rets Blocked;Product hierarchy;Nettable";
                default:
                    return null;
            }
        }

    }
}
