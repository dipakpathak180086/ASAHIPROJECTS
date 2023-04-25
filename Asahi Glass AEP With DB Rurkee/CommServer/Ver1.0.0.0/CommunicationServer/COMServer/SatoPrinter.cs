using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SATOPrinterAPI;
using System.Windows.Forms;
using AISCOMServer;

class SatoPrinter
{
    //public int PrintJobs { get { return SatoDriver.GetSpoolerPrintJobsNumber(Program.PrinterName); } }     

    // SATOPrinterAPI.Driver SatoDriver = null;
    // SATOPrinterAPI.Printer printer = null;
    Printer tcpPrinter = null;
    public SatoPrinter()
    {
        // SatoDriver = new Driver();
        //   printer = new Printer();

        tcpPrinter = new Printer();


        //var USBPorts = printer.GetUSBList();
        //if (USBPorts.Count > 0)
        //{
        //    printer.Interface = Printer.InterfaceType.USB;
        //    printer.USBPortID = USBPorts[0].PortID;
        //}
        //else
        //{
        //FillTcpPTR(PrinterIp,PrinterPort);
        // throw new Exception("Printer not found.");
        // }

    }
    //public static string GetprinterStatus()
    //{
    //    string status = "Not Connected";
    //    SATOPrinterAPI.Printer.Status printerStatus = new Printer.Status();
    //    status = printerStatus.State;
    //    return status;
    //}
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
    public void PrintMachiningLabel(string TrolleyCard, string ModelNo,string LotNo,string Qty, string PRNFile)
    {
        string sbpl = PRNFile; ;
        try
        {
            sbpl = sbpl.Replace("{VARLEN}", TrolleyCard.Length.ToString());
            sbpl = sbpl.Replace("{VARMODELNO}", ModelNo);
            //sbpl = sbpl.Replace("{VARLOTNO}", LotNo);
            sbpl = sbpl.Replace("{VARLOTNO}", TrolleyCard);
            sbpl = sbpl.Replace("{VARQTY}", Qty);
            sbpl = sbpl.Replace("{VARBARCODE}", TrolleyCard);

            if (sbpl == "")
            {
                //  throw new InfoException("Printing data can not be empty.");
            }
            byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);

            /*
            * TCP Printing
            */
            FillTcpPTR(Program.MachiningPrinterIP, Program.MachiningPrinterPort);
            tcpPrinter.Send(sbplByte);

            /*
             * Driver Printing
             */
            //Driver satoDriver = new Driver();
            //satoDriver.SendRawData(Program.PrinterName, sbplByte);
        }
        catch (Exception ex)
        {
            throw new Exception("Printing Error : " + ex.Message);
        }
    }

    public void PrintTrolleyBoxLabel(string TrolleyNo, string ModelNo, string PRNFile)
    {
        string sbpl = PRNFile; ;
        try
        {
            sbpl = sbpl.Replace("{VAR1LEN}", TrolleyNo.Length.ToString());
            sbpl = sbpl.Replace("{VAR1}", TrolleyNo);
            sbpl = sbpl.Replace("{VARMODEL}", ModelNo);

            if (sbpl == "")
            {
                //  throw new InfoException("Printing data can not be empty.");
            }
            byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);

            /*
            * TCP Printing
            */
            FillTcpPTR(Program.FinalPackingPrinterIP, Program.FinalPackingPrinterPort);
            tcpPrinter.Send(sbplByte);

            /*
             * Driver Printing
             */
            //Driver satoDriver = new Driver();
            //satoDriver.SendRawData(Program.PrinterName, sbplByte);
        }
        catch (Exception ex)
        {
            throw new Exception("Printing Error : " + ex.Message);
        }
    }

    public void PrintFinalPackingLabel(string TrolleyCard, string ModelNo, string LotNo, string Qty, string PRNFile)
    {
        string sbpl = PRNFile; ;
        try
        {
            sbpl = sbpl.Replace("{VARLEN}", TrolleyCard.Length.ToString());
            sbpl = sbpl.Replace("{VARMODELNO}", ModelNo);
            //sbpl = sbpl.Replace("{VARLOTNO}", LotNo);
            sbpl = sbpl.Replace("{VARLOTNO}", TrolleyCard);
            sbpl = sbpl.Replace("{VARQTY}", Qty);
            sbpl = sbpl.Replace("{VARBARCODE}", TrolleyCard);

            if (sbpl == "")
            {
                //  throw new InfoException("Printing data can not be empty.");
            }
            byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);

            /*
            * TCP Printing
            */
            FillTcpPTR(Program.FinalPackingPrinterIP, Program.FinalPackingPrinterPort);
            tcpPrinter.Send(sbplByte);

            /*
             * Driver Printing
             */
            //Driver satoDriver = new Driver();
            //satoDriver.SendRawData(Program.PrinterName, sbplByte);
        }
        catch (Exception ex)
        {
            throw new Exception("Printing Error : " + ex.Message);
        }
    }

    private void FillTcpPTR(string PrinterIP,string PrinterPort)
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


