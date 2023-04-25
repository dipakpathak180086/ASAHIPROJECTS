using System;
using System.Printing;
using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Sequential)]
public struct DOCINFO
{
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pDocName;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pOutputFile;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pDataType;
}

internal class PrintBarcode
{
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    public static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern long StartPagePrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern long WritePrinter(IntPtr hPrinter, string data, int buf, ref int pcWritten);
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern long EndPagePrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern long EndDocPrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern long ClosePrinter(IntPtr hPrinter);


    public static string PrintCommand(string printData, string PrinterName)
    {
    PrintAgain:
        System.Windows.Forms.Application.DoEvents();
        if (GetNumberOfPrintJobs(PrinterName) < 10)
        {
            System.IntPtr lhPrinter = new System.IntPtr();
            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            int iprinter = 0;
            for (int i = 0; i <= System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1; i++)
            {
                if (System.Drawing.Printing.PrinterSettings.InstalledPrinters[i].ToString() == PrinterName)
                {
                    iprinter = 1;
                    break;
                }
            }
            if (iprinter == 1)
            {

                Console.WriteLine(PrinterName);
                PrintBarcode.OpenPrinter(PrinterName, ref lhPrinter, 0);
                if (lhPrinter == IntPtr.Zero)
                {
                    return "Printer Not found";
                }
                //PrintDirect.OpenPrinter("LPT:", ref lhPrinter, 0); 
                di.pDocName = "Test";
                //di.pDataType = "RAW";

                PrintBarcode.StartDocPrinter(lhPrinter, 1, ref di);
                PrintBarcode.StartPagePrinter(lhPrinter);
                PrintBarcode.WritePrinter(lhPrinter, printData, printData.Length, ref pcWritten);
                PrintBarcode.EndPagePrinter(lhPrinter);
                PrintBarcode.EndDocPrinter(lhPrinter);
                PrintBarcode.ClosePrinter(lhPrinter);
                return "OK";
            }
            else
            {
                return "Printer Not found!!";
            }
        }
        else
        { goto PrintAgain; }
    }
    public static int GetNumberOfPrintJobs(string sPrinterName)
    {
        LocalPrintServer server = new LocalPrintServer();
        PrintQueueCollection queueCollection = server.GetPrintQueues();
        PrintQueue printQueue = null;

        foreach (PrintQueue pq in queueCollection)
        {
            if (pq.FullName == sPrinterName)
            {
                printQueue = pq;
            }
        }

        int numberOfJobs = 0;
        if (printQueue != null)
        {
            numberOfJobs = printQueue.NumberOfJobs;
        }

        return numberOfJobs;
    }

}

