using AIS_GLASS_COMMON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS_GLASS_PC_APP
{
    public class CustomFunction
    {
        
        #region PUBLIC CUSTOM FUNCTION

        #region Get Gross weight from weighing scale

        string _strData = string.Empty;

        bool _IsSerialConnected = false;


        public string GetGrossWeight()
        {
            try
            {
                _strData = string.Empty;
                if (!TCPConnnect())
                {
                    MessageBox.Show("Error in Tcp Connect !");
                    return null;
                }
                DisposeWeightVariables();
                return _strData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region " TCP CONNECTION"
        bool TCPConnnect()
        {

            try
            {
                //_IComm = new TcpCommunication(GlobalVariable.IP, GlobalVariable.PORT);
                //_IComm.Logger = new BcilLib.Logger.TraceLogger(BcilLib.Logger.LoggerVerbosity.Normal);

                //_IComm.Delimiter = "\r\n";                                    // change the delimiter since it defaults to "\r\n".
                //_IComm.CurrentEncoding = System.Text.ASCIIEncoding.ASCII;   // this is unnecessary since it defaults to ASCII
                //_IComm.ReceivedDelimitedString += new EventHandler<ReceivedDelimitedStringEventArgs>(_IComm_ReceivedDelimitedString); // Since our pretend protocol uses \r as the delimiter let's make life easy and subscribe to the ReceivedDelimitedString event. We could also subscribe to ReceivedString and ReceivedBytes.
                //_IComm.IncludeDelimiterInRawResponse = false;               // this is unnecessary since it defaults to false
                //_IComm.ReadBufferEnabled = false;                           // this is unnecessary since it defaults to false and it since we are using the ReceivedDelimitedString event there would be no need to use a buffer.
                //_IComm.DefaultSendDelayInterval = 0;                        // this is unnecessary since it defaults to 0               
                //// Start connection monitor.
                //_IComm.ConnectionMonitorTimeout = 4000;
                //_IComm.ConnectionMonitorTestRequest = "HELLO\r\n";            // a pretend message to send when no data has been received for a while (note that serial connections can not detect physical disconnections so we count on this).
                //_IComm.ConnectionEstablished += new EventHandler<EventArgs>(_IComm_ConnectionEstablished);
                //_IComm.ConnectionLost += new EventHandler<EventArgs>(_IComm_ConnectionLost);
                //_IComm.StartConnectionMonitor();                            // if we were not using connection monitoring we could call _comm.Open().
                //_IsSerialConnected = true;
                //Thread.Sleep(1000);
                return _IsSerialConnected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                //throw ex;
            }
        }

        void _IComm_ConnectionEstablished(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);

            //_IComm.Send("Test\\");
            //string _sDat = "233.34";
            //decimal _
            // this.InvokeIfRequired(() => { lblStatus.Text = "Status: Connected"; });
        }

        void _IComm_ConnectionLost(object sender, EventArgs e)
        {
            //this.InvokeIfRequired(() => { lblStatus.Text = "Status: Not Connected"; });
        }

        //void _IComm_ReceivedDelimitedString(object sender, ReceivedDelimitedStringEventArgs e)
        //{
        //    try
        //    {
        //        _strData = e.RawResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        // throw ex;
        //    }
        //}

        private void SerialDataToTextBox(object s, EventArgs e) // method for display Serial Port Data to text Box
        {
            //_strWeighmentGrossWt = serialPort1.ReadLine();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //if (_isActive == false)
            //{
            //    this.Invoke(new EventHandler(SerialDataToTextBox));
            //}
        }
        #endregion

        public void DisposeWeightVariables()
        {
            //try
            //{
            //    if (_IComm != null)
            //    {
            //        _IComm.StopConnectionMonitor();
            //        Thread.Sleep(1000);
            //        _IComm.Dispose();
            //        Thread.Sleep(1000);
            //    }
            //    _IComm = null;
            //    _IsSerialConnected = false;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        #endregion


       

        /// <summary>
        /// NUMERIC DECIMAL VALIDATION
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public void NumericDecimalValidation(object Sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) || e.KeyChar.ToString() == "." || char.IsControl(e.KeyChar))
            {
                //if (((TextBox)Sender).Text.IndexOf(".") != -1 )
                //    e.Handled = true;
                //else
                //if (e.KeyChar.ToString() == ".")
                //    e.Handled = false;
                //else
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        public void VehicleValidation(object Sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) || e.KeyChar.ToString() == "." || char.IsControl(e.KeyChar))
            {
                if (((TextBox)Sender).Text.IndexOf(".") != -1 && e.KeyChar.ToString() == ".")
                    e.Handled = true;
                else
                    if (e.KeyChar.ToString() == ".")
                    e.Handled = false;
                else
                    e.Handled = false;
            }
            else
                e.Handled = true;
        }

        public void NumericValidation(object Sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        
        public void FillListView(DataTable dt, ListView lv)
        {
            lv.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                lv.Items.Add(item);
            }
            // lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void FillListView(DataTable dt, ListView lv, int iIcon)
        {
            lv.Items.Clear();
            for (int x = 1; x < dt.Rows.Count; x++)
            {
                lv.Items.Add(new ListViewItem("" + dt.Rows[x].ItemArray.GetValue(0).ToString(), iIcon));
                //ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    lv.Items[x].SubItems.Add("" + dt.Rows[x].ItemArray.GetValue(i).ToString());
                    //item.SubItems.Add(row[i].ToString());
                }
                //lv.Items.Add(item, iIcon);
            }
            // lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void FillListView(ListView sListView, string sTable, int iStart, int iEnd, int iIcon, DataSet dsSource, int iNumField)
        {
            try
            {
                int totalRow = 1;
                totalRow = dsSource.Tables[sTable].Rows.Count;
                sListView.Items.Clear();
                iStart = iStart - 1;
                for (int x = iStart; x <= iEnd - 1; x++)
                {
                    sListView.Items.Add(new ListViewItem("" + dsSource.Tables[sTable].Rows[x].ItemArray.GetValue(0).ToString(), iIcon));
                    if (iNumField != 0)
                    {
                        for (int a = 1; a < iNumField; a++)
                        {
                            int n = iStart;
                            sListView.Items[x - n].SubItems.Add("" + dsSource.Tables[sTable].Rows[x].ItemArray.GetValue(a).ToString());
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void setSelectedItem(Label sLabel, ListView sListView)
        {
            try { sLabel.Text = "Selected Record: " + (sListView.FocusedItem.Index + 1); }
            catch (Exception) { }
        }

        public void setSearchFilter(Label sLabel, string sSelect)
        {

        }

        public string GetVersion()
        {
            string sVer = "Version :-" + Application.ProductVersion;
            return sVer;
        }
        public Hashtable FileToHashTable(string FileName, char SplitSeperator, int[] PrimaryIndex)
        {
            Hashtable _ht = new Hashtable();
            string _Value = "";
            string _Key = "";
            try
            {
                if (File.Exists(FileName))
                {
                    StreamReader _sr = new StreamReader(FileName);
                    while (!_sr.EndOfStream)
                    {
                        _Key = "";
                        _Value = "";
                        string _data = _sr.ReadLine();
                        if (_data == "")
                            break;
                        string[] _sArr = _data.Split(SplitSeperator);
                        for (int i = 0; i < PrimaryIndex.Length; i++)
                        {
                            if (_Key == "")
                                _Key = _sArr[PrimaryIndex[i]].ToString();
                            else
                                _Key = _Key + _sArr[PrimaryIndex[i]].ToString();
                        }

                        _Value = _data;
                        if (!_ht.ContainsKey(_Key))
                            _ht.Add(_Key, _Value);
                    }
                    _sr.Close();
                    _sr = null;
                }
                return _ht;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePcFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }

        public void WriteData(string FileName, string Data)
        {
            StreamWriter _sw = new StreamWriter(FileName, true);
            try
            {
                _sw.WriteLine(Data);
                _sw.Close();
                _sw = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
