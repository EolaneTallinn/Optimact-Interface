using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using static System.Net.WebRequestMethods;
using System.Diagnostics;

namespace SapBapiService.Classes
{
    public static class DataWorker
    {
        public static void ToCSV(this DataTable dtDataTable, int filenum, int GenerationStatus)
        {
            //Regex[] GlobalRegex = new Regex[]
            //{
            //    DefinedGlobals.ExcessWhiteSpaceRX(),
            //    DefinedGlobals.GetFloatWithThreeDecimalsRX(),
            //    DefinedGlobals.ZerosInFront(),
            //    DefinedGlobals.SpacesInFront(),
            //    DefinedGlobals.GetPartFromAnyFloat()
            //};
            //List<string> dateAndTimeCaptions = DefinedGlobals.dateAndTimeCaptions;
            //List<int> dateAndTimePositions = new List<int>();

            DateTime currentTime = DateTime.Now;
            string strFilePath = null;
            string day = currentTime.Day.ToString("00");

            string[] FileName = { "Item", "Sales" , "Purchase", "BOM", "Special Forecast", "Sales D6", "Special_MB52" };

            string SavePath = @"\\tninf19\Data-RESULTS\SFTP\" + FileName[filenum-1] + ".csv";
            string TestSaveDir = @"T:\SAP KU\Tickets\OPTIMACT BAPI developments\ComparingFiles\AUTOGEN\" + day;
            string TestSavePath = TestSaveDir + @"\BAPI-" + filenum + ".csv";
            if (GenerationStatus == 1)
            {
                strFilePath = SavePath;
            }
            else
            {
                if (!Directory.Exists(TestSaveDir))
                {
                    Directory.CreateDirectory(TestSaveDir);
                }
                strFilePath = TestSavePath;
            }

            /*
            switch (filenum)
            {
                case 1:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
                case 2:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-2.csv";
                    }
                    break;
                case 3:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
                case 4:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
                case 5:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
                case 6:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
                case 7:
                    if (GenerationStatus == 1)
                    {
                        strFilePath = SavePath + @"\Item.csv";
                    }
                    else
                    {
                        strFilePath = TestSavePath + @"\BAPI-1.csv";
                    }
                    break;
            }
            */
            StreamWriter currentDocument = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);

            //for (int i = 0; i < dtDataTable.Columns.Count; i++)
            //{
            //    if (i < dtDataTable.Columns.Count - 1)
            //    {
            //        if (dateAndTimeCaptions.Contains(dtDataTable.Columns[i].Caption))
            //        {
            //            dateAndTimePositions.Add(i);
            //        }
            //    }
            //}

            if (DefinedGlobals.ReturnHeaderNames(filenum) != null)
            {
                currentDocument.Write(DefinedGlobals.ReturnHeaderNames(filenum));
                currentDocument.Write(currentDocument.NewLine);
            }

            int colIndex = 0;
            string initialValue = null;
            string processedValue = null;
            //string plainEntry = null;
            //string extraEntry = null;

            foreach (DataRow dr in dtDataTable.Rows)
            {
                colIndex = 0;

                foreach (DataColumn dc in dtDataTable.Columns)//for (int colPos = 0; colPos < dtDataTable.Columns.Count; colPos++)
                {
                    //Triming
                    initialValue = DefinedGlobals.trimExtended.Replace(dr[dc.ColumnName].ToString(), "");

                    //Removing Zeros in the front of the string
                    //initialEntry = GlobalRegex[2].Replace(initialEntry, "");

                    //Removing Spaces in the front of the string (it was needed for some fields in File 2 because the trim was not enough)
                    //initialEntry = GlobalRegex[3].Replace(initialEntry, "");

                    //Check if the value is not null
                    if (DefinedGlobals.notNull.IsMatch(initialValue))
                    {

                        processedValue = FormatCheck(initialValue, dc.ColumnName);
                        currentDocument.Write(processedValue);
                        /*
                        if (initialEntry.Contains(","))
                        {
                            extraEntry = initialEntry; //string.Format("\"{0}\"", initialEntry);
                            extraEntry = FormatCheck(extraEntry, GlobalRegex, colPos, dateAndTimePositions, filenum);
                            currentDocument.Write(extraEntry);
                        }
                        else
                        {
                            plainEntry = initialEntry;
                            plainEntry = FormatCheck(plainEntry, GlobalRegex, colPos, dateAndTimePositions, filenum);
                            currentDocument.Write(plainEntry);
                        }
                        */

                    }

                    colIndex++;
                    if (colIndex < dtDataTable.Columns.Count)
                    {
                        currentDocument.Write(";");
                    }
                }
                currentDocument.Write(currentDocument.NewLine);
            }
            currentDocument.Close();
        }
        /// <summary>
        /// Method <c>RemoveWhitespace</c> is being used to remove whitespaces from the string completely.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>A string with no whitespaces.</returns>
        public static string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Method <c>FormatCheck</c> is being used to format the incoming strings, to be
        /// compliable with the standards that the optimact provided - for the data to be used in their system.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="GlobalRegex"></param>
        /// <param name="colPos"></param>
        /// <param name="dateConvertPosition"></param>
        /// <returns>A string that is compliable with the optimact standards.</returns>
        public static string FormatCheck(string str, string colName)
        {
            //This 'if' checks that if there are way too much spaces in the string, and they are being followed by
            //any amount of digits - it will remove all of the whitespaces.
            //if (GlobalRegex[0].IsMatch(str))
            //{
            //    str = RemoveWhitespace(str);
            //}

            //If there is a semi-column, we change it to space to avoid breaking the csv file.
            if (str.Contains(";")) { str = str.Replace(';', ','); }

            /*
            //We check if string could be parsed as a float - view descriptions inside.
            if (GlobalRegex[4].IsMatch(str))
            {
                if (colPos != 0)
                {
                    str = GlobalRegex[4].Replace(str, "$1$5$2$3$4");

                    if (str.Contains(".") && float.TryParse(str, out float _str) == true)
                    {
                        str = _str.ToString();
                    }

                    switch (filenum)
                    {
                        case 7:
                            if (new[] { 12, 14, 16, 18, 20, 22 }.Contains(colPos))
                            {
                                str = GlobalRegex[1].Replace(str, "$1");
                            }
                            else if (new[] { 10, 13, 15, 17, 19, 21 }.Contains(colPos))
                            {
                                if (str.Contains("."))
                                {
                                    str = str.Substring(0, str.IndexOf("."));
                                }
                                else if (str.Contains(","))
                                {
                                    str = str.Substring(0, str.IndexOf(","));
                                }
                            }

                            break;
                    }

                    str = str.Replace('.', ',');
                }

            }
            */
            /*
            if (str.Contains(".") && float.TryParse(str, out float _str) == true)
            {
                if (colPos == 0)
                {
                    str = _str.ToString();
                } 
                else
                {
                    str = _str.ToString();

                    switch (filenum)
                    {
                        case 7:
                            if (new[] { 12, 14, 16, 18, 20, 22 }.Contains(colPos))
                            {
                                str = GlobalRegex[1].Replace(str, "$1");
                            }
                            else if (new[] { 10, 13, 15, 17, 19, 21 }.Contains(colPos))
                            {
                                if (str.Contains(".") || str.Contains(","))
                                {
                                    str = str.Substring(0, str.IndexOf("."));
                                }
                            }

                            break;
                    }

                }
                str = str.Replace('.', ',');
            }
            */

            if (DefinedGlobals.textCaptions.Contains(colName))
            {
                
                //No processing for text captions at the moment.

            }

            //This is BAPI-Query specific - previously in the collumns itteration phase, if there were any points at which there were date collumns
            //we append the number of that collumn position to the list 'dateConvertPosition', and convert the string to 'DateTime' object that is
            //comprehensible by optimact if the current colPos corresponds to any entry in that list.

            if (DefinedGlobals.dateCaptions.Contains(colName))
            {
                if (DefinedGlobals.wholeNumber.IsMatch(str))
                {
                    str = DefinedGlobals.wholeNumber.Replace(str, "$2$1");

                    if (DefinedGlobals.date.IsMatch(str))
                    {
                        str = DefinedGlobals.date.Replace(str, "$3/$2/$1");
                    }
                    else
                    {
                        Console.WriteLine("Could not parse date: " + str + " from column " + colName);
                        str = "";
                    }
                }
                else
                {
                    Console.WriteLine("Could not parse date: " + str + " from column " + colName);
                    str = "";
                }

                /*
                //Removing zeros in the front to ignore empty dates
                str = DefinedGlobals.ZerosInFront.Replace(str, "");

                if (str.Length == 8)
                {
                    string x1 = str.Substring(0, 4);
                    string x2 = str.Substring(4, 2);
                    string x3 = str.Substring(6, 2);

                    str = x3 + "/" + x2 + "/" + x1;
                }
                else
                {
                    Console.WriteLine("Could not parse date: " + str);
                    str = "";
                }
                */
            }


            if (DefinedGlobals.idCaptions.Contains(colName))
            {
                if (DefinedGlobals.wholeUnsignedNumber.IsMatch(str))
                {
                    str = DefinedGlobals.wholeUnsignedNumber.Replace(str, "$1");
                }
            }


            if (DefinedGlobals.wholeNumberCaptions.Contains(colName))
            {
                if (DefinedGlobals.wholeNumber.IsMatch(str))
                {
                    str = DefinedGlobals.wholeNumber.Replace(str, "$1$3$2");
                }
                else
                {
                    Console.WriteLine("Could not parse whole number: " + str + " from column " + colName);
                    str = "";
                }
            }


            if (DefinedGlobals.floatingNumberCaptions.Contains(colName))
            {
                if (DefinedGlobals.floatingNumber.IsMatch(str))
                {
                    str = DefinedGlobals.floatingNumber.Replace(str, "$1$4$2$3");
                    str = str.Replace('.', ',');
                }
                else
                {
                    Console.WriteLine("Could not parse floating number: " + str + " from column " + colName);
                    str = "";
                }
            }

            //We basically remove excess zeroes from the product codes.
            //if (str.Length > 11)
            //{
            //    if (str.Substring(0, 11).Count(x => x == '0') == 11)
            //    {
            //        str = str.Remove(0, 11);
            //    }
            //    else if (str.Substring(0, 9).Count(x => x == '0') == 9)
            //    {
            //        str = str.Remove(0, 9);
            //    }
            //}

            return str;
        }

        //public static void FloatIntegerDecimalPoints(string currentString, int[] colPos)
        //{
        //
        //}
    }
}
