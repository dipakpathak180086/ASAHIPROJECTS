using AIS_GLASS_COMMON;
using AIS_GLASS_PL;
using SATOPrinterAPI;
using System;
using System.Data;
using System.Linq;
using System.Text;


namespace AIS_GLASS_PC_APP
{
    internal class SatoPrinter
    {
        private Printer tcpPrinter = null;
        public SatoPrinter()
        {
           tcpPrinter = new Printer();
        }
        public string QueryStatus()
        {
            byte[] qry = SATOPrinterAPI.Utils.StringToByteArray("");
            byte[] result = tcpPrinter.Query(qry);
            string status = SATOPrinterAPI.Utils.ByteArrayToString(result);
            return status;
        }

        public string GetprinterStatus()
        {
            string status = "Not Connected";

            try
            {
                tcpPrinter.Connect();
                SATOPrinterAPI.Printer.Status printerStatus = tcpPrinter.GetPrinterStatus();
                status = printerStatus.Code;
                status = GetStatusMessages(status);
                if (status.StartsWith("ONLINE"))
                {
                    return "OK_" + status;
                }
                else
                {
                    return "NOT_OK_" + status;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                tcpPrinter.Disconnect();
            }
            return status;
        }


        public void PrintPartBarcode(PL_BarcodePrinting _PlObj)
        {
            //bool IsPrinted = false;
            string sbpl = "";
            try
            {
                if (GlobalVariable.ReadPartPrn())
                {
                    sbpl = GlobalVariable.mPrn;
                    sbpl = sbpl.Replace("{BARCODE}", _PlObj.Barcode);
                    sbpl = sbpl.Replace("{CUST_PART}", _PlObj.CustomerPartNo);
                    sbpl = sbpl.Replace("{SR_NO}", _PlObj.SrNo);
                    sbpl = sbpl.Replace("{SHORT_NAME}", _PlObj.AISPartNo); 
                    sbpl = sbpl.Replace("{LenBarcodeLen}", "DN" + _PlObj.Barcode.Length.ToString().PadLeft(4, '0'));
                    if (GlobalVariable.mPrinterName.Contains("."))
                    {
                        SatoPrinter satoPrinter = new SatoPrinter();
                        byte[] sbplByte = null;

                        sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl, "UTF8");

                        satoPrinter.FillTcpPTR(GlobalVariable.mPrinterName, "9100");
                        satoPrinter.tcpPrinter.Send(sbplByte);

                    }
                    else
                    {
                        PrintBarcode.PrintCommand(sbpl, GlobalVariable.mPrinterName);
                    }
                }
                else
                {
                    throw new NoNullAllowedException("Prn File " + GlobalVariable.mPalletPrnFileName + " not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void PrintMachine(PL_MachineMaster _PlObj)
        //{
        //    //bool IsPrinted = false;
        //    string sbpl = "";
        //    try
        //    {
        //        if (GlobalVariable.ReadMachinePrn())
        //        {
        //            string LenBarcodeLen = _PlObj.MachineCode.Length.ToString();
        //            sbpl = GlobalVariable.mPrn;
        //            sbpl = sbpl.Replace("{MachineCode}", _PlObj.MachineCode);
        //            sbpl = sbpl.Replace("{MachineName}", _PlObj.MachineName);
        //            sbpl = sbpl.Replace("{MachineType}", _PlObj.MachineType);
        //            sbpl = sbpl.Replace("{LenBarcodeLen}", LenBarcodeLen);

        //            if (GlobalVariable.mPrinterName.Contains("."))
        //            {
        //                SatoPrinter satoPrinter = new SatoPrinter();
        //                byte[] sbplByte = null;

        //                sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl, "UTF8");

        //                satoPrinter.FillTcpPTR(GlobalVariable.mPrinterName, "9100");
        //                satoPrinter.tcpPrinter.Send(sbplByte);

        //            }
        //            {
        //                PrintBarcode.PrintCommand(sbpl, GlobalVariable.mPrinterName);
        //            }
        //        }
        //        else
        //        {
        //            throw new NoNullAllowedException("Prn File " + GlobalVariable.mPalletPrnFileName + " not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        private void FillTcpPTR(string PrinterIP, string PrinterPort)
        {
            try
            {
                tcpPrinter.Interface = Printer.InterfaceType.TCPIP;
                tcpPrinter.TCPIPAddress = PrinterIP;
                tcpPrinter.TCPIPPort = PrinterPort;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPrinterStatusBeforePrinting()
        {
            try
            {
                //MessageBox.Show("before status");
                Printer.Status st = tcpPrinter.GetPrinterStatus();
                string status = GetStatusMessages(st.Code);
                if (status.StartsWith("ONLINE"))
                {
                    return "OK_" + status;
                }
                else
                {
                    return "NOT_OK_" + status;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                throw ex;
            }
        }
        private static string GetStatusMessages(string data)
        {
            switch (Convert.ToChar(data))//HexToInt(data)))
            {
                case '0':
                    return ("OFFLINE_STATE" + " : " + "STATUS_NO_ERROR");

                case '1':
                    return ("OFFLINE_STATE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case '2':
                    return ("OFFLINE_STATE" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case '3':
                    return ("OFFLINE_STATE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case '4':
                    return ("OFFLINE_STATE" + " : " + "STATUS_PRINTER_PAUSE");

                case 'A':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_NO_ERROR");

                case 'B':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'C':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'D':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'E':
                    return ("ONLINE_STATE" + " : " + "STATUS_WAIT_TO_RECEIVE" + " : " + "STATUS_PRINTER_PAUSE");

                case 'G':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING");

                case 'H':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'I':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'J':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'K':
                    return ("ONLINE_STATE" + " : " + "STATUS_PRINTING" + " : " + "STATUS_PRINTER_PAUSE");

                case 'M':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY");

                case 'N':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'O':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'P':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'Q':
                    return ("ONLINE_STATE" + " : " + "STATUS_STANDBY" + " : " + "STATUS_PRINTER_PAUSE");

                case 'S':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING");

                case 'T':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END");

                case 'U':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_BUFFER_NEAR_FULL");

                case 'V':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_RIBBON_LABEL_NEAR_END_BUFFER_NEAR_FULL");

                case 'W':
                    return ("ONLINE_STATE" + " : " + "STATUS_ANALYZING" + " : " + "STATUS_PRINTER_PAUSE");

                case 'b':
                    return ("ERROR_DETECTION" + " : " + "STATUS_HEAD_OPEN");

                case 'c':
                    return ("ERROR_DETECTION" + " : " + "STATUS_PAPER_END");

                case 'd':
                    return ("ERROR_DETECTION" + " : " + "STATUS_RIBBON_END");

                case 'e':
                    return ("ERROR_DETECTION" + " : " + "STATUS_MEDIA_ERROR");

                case 'f':
                    return ("ERROR_DETECTION" + " : " + "STATUS_SENSOR_ERROR");

                case 'g':
                    return ("ERROR_DETECTION" + " : " + "STATUS_HEAD_ERROR");

                case 'h':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CUTTER_OPEN_ERROR");

                case 'i':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CARD_ERROR");

                case 'j':
                    return ("ERROR_DETECTION" + " : " + "STATUS_CUTTER_ERROR");

                case 'k':
                    return ("ERROR_DETECTION" + " : " + "STATUS_OTHER_ERRORS");

                case 'o':
                    return ("ERROR_DETECTION" + " : " + "STATUS_OTHER_IC_TAG_ERROR");

                case 'q':
                    return ("ERROR_DETECTION" + " : " + "STATUS_BATTER_ERROR");
            }
            return "UNEXPECTED_VALUE";
        }
    }
}
