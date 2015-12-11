using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace RSI_test
{
    public partial class RSIServerForm : Form
    {
        private static Thread _RSIThread;
        int receiveport = 49152;
        UdpClient UDPServer;

        /*
        string testxml = @"
        <Rob Type=""KUKA"">
        <RIst X=""1350.0"" Y=""0.0"" Z=""1569.0"" A=""180.0"" B=""0.0"" C=""180.0""/>
        <RSol X=""1350.0"" Y=""0.0"" Z=""1569.0"" A=""180.0"" B=""0.0"" C=""180.0""/>
        <Delay D=""5""/>
        <Tech C11=""0.0"" C12=""0.0"" C13=""0.0"" C14=""0.0"" C15=""0.0"" C16=""0.0""  C17=""0.0"" C18=""0.0"" C19=""0.0"" C110=""0.0""/>
        <DiL>0</DiL>
        <Digout o1=""0"" o2=""0"" o3=""0""/>
        <Source1>38.0</Source1>
        <IPOC>366168</IPOC></Rob>
        ";
        
        string testxml = @"
        <Rob Type=""KUKA"">
        <RIst X=""1350.0"" Y=""0.0"" Z=""1569.0"" A=""180.0"" B=""0.0"" C=""180.0""/>
        <RSol X=""1350.0"" Y=""0.0"" Z=""1569.0"" A=""180.0"" B=""0.0"" C=""180.0""/>
        <MACur A1=""1.0"" A2=""0.0"" A3=""9.0"" A4=""10.0"" A5=""0.0"" A6=""0.0""/>
        <Delay D=""0""/>
        <POSMON X=""1.0"" Y=""0.0"" Z=""19.0"" A=""1.0"" B=""0.0"" C=""1.0""/>
        <DoL>0</DoL>
        <DiL>0</DiL>
        <PCSTAT>0</PCSTAT>
        <IPOC>366168</IPOC>
        </Rob>
        ";
         
        string testxml = @"
        <Rob Type=""KUKA"">
        <RIst X=""1350.000"" Y=""0.000"" Z=""1569.000"" A=""-180.000"" B=""0.000"" C=""180.000""/>
        <RSol X=""1350.000"" Y=""0.000"" Z=""1569.000"" A=""180.000"" B=""0.000"" C=""180.000""/>
        <MACur A1=""-1.924"" A2=""0.734"" A3=""2.369"" A4=""0.032"" A5=""0.239"" A6=""-0.066""/>
        <Delay D=""10""/>
        <POSMON X=""0.000"" Y=""0.000"" Z=""0.000"" A=""0.000"" B=""0.000"" C=""0.000""/>
        <DOUTS>0</DOUTS>
        <DINS>0</DINS>
        <IPOC>6222796</IPOC>
        </Rob>";
        

        string testxml = @"<Sen Type=""ImFree"">
        <EStr></EStr>
        <RKorr X=""0.0000"" Y=""0.0000"" Z=""0.0000"" A=""0.0000"" B=""0.0000"" C=""0.0000""/>
        <DOUT>0</DOUT>
        <HALT>0</HALT>
        <STOPCORR>0</STOPCORR>
        <IPOC>366152</IPOC>
        </Sen>";
        */

        KUKA KR2500 = new KUKA();

        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;

        public RSIServerForm()
        {
            InitializeComponent();

            _RSIThread = new Thread(ProcessUDPString);
            _RSIThread.IsBackground = true;

            this.DoubleBuffered = true;
        }

        private void ProcessUDPString()
        {
            IPEndPoint KRCEP = null;

            this.Invoke((MethodInvoker)delegate { DebugTextBox.AppendText("Thread Started\n"); });


            UDPServer = new UdpClient(receiveport);
            UDPServer.Client.ReceiveTimeout = 1000;


            while (!_shouldStop)
            {
                try
                {
                    //receive is blocking
                    byte[] data = UDPServer.Receive(ref KRCEP);

                    if (data == null || data.Length == 0)
                    {
                        //this.Invoke((MethodInvoker)delegate { DebugTextBox.AppendText("\nReceive thread timed out\n"); });
                    }
                    else
                    {
                        KR2500.processXML(data);


                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(KR2500.CreateXMLOut());

                        UDPServer.Send(msg, msg.Length, KRCEP);
                        //this.Invoke((MethodInvoker)delegate { DebugTextBox.AppendText(XMLText); });

                        /*
                        this.Invoke((MethodInvoker)delegate
                        {
                            DebugTextBox.Clear();
                            DebugTextBox.AppendText(KR2500.Print());
                            DebugTextBox.AppendText(KR2500.LatePackets.ToString() + "\n");
                            DebugTextBox.AppendText(KR2500.IPOC.ToString() + "\n");
                        });*/
                    }
                }
                catch
                {
                    //this.Invoke((MethodInvoker)delegate { DebugTextBox.AppendText("\nReceive thread timed out\n"); });
                }
            }

            UDPServer.Close();
            this.Invoke((MethodInvoker)delegate { DebugTextBox.AppendText("Thread Closed\n"); });
        }

        private void StartUDPButton_Click(object sender, EventArgs e)
        {
            if (_RSIThread.IsAlive)
            {
                _shouldStop = true;
                StartUDPButton.Text = "Start";
                UITimer.Stop();
            }
            else
            {
                _shouldStop = false;
                _RSIThread = new Thread(ProcessUDPString);
                _RSIThread.IsBackground = true;
                _RSIThread.Start();
                StartUDPButton.Text = "Stop";
                UITimer.Start();
            }
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {

            DebugTextBox.Clear();
            DebugTextBox.AppendText(KR2500.Print());
            DebugTextBox.AppendText(KR2500.LatePackets.ToString() + "\n");
            DebugTextBox.AppendText(KR2500.IPOC.ToString() + "\n");
            DebugTextBox.AppendText("Delta T = " + KR2500.DeltaT.ToString() + "ms\n");

            //DebugTextBox.AppendText(KR2500.CreateXMLOut());
            //DebugTextBox.AppendText(KR2500.Print6DOF(KR2500.POSAct, false) + " " + KR2500.Print6DOF(KR2500.POSTar, false) + "\r\n");
        }

        private void StopCorrButton_Click(object sender, EventArgs e)
        {
            if (StopCorrButton.Checked)
                KR2500.STOPCORR = 1;
            else
                KR2500.STOPCORR = 0;
        }

        private void HaltButton_Click(object sender, EventArgs e)
        {
            if (HaltButton.Checked)
                KR2500.HALT = 1;
            else
                KR2500.HALT = 0;
        }

        private void CorrectButton_Click(object sender, EventArgs e)
        {
            KR2500.POSCorr.X = 0.001;
        }
    }

    public struct POS6DOF
    {
        public double X;
        public double Y;
        public double Z;
        public double A;
        public double B;
        public double C;
    }

    public struct Joints
    {
        public double A1;
        public double A2;
        public double A3;
        public double A4;
        public double A5;
        public double A6;
    }

    public class KUKA
    {
        public XmlDocument xmlDoc;

        public string SensorName;

        //actual position
        public POS6DOF POSAct;
        //target position
        public POS6DOF POSTar;
        //POSMON
        public POS6DOF POSMON;
        //correction
        public POS6DOF POSCorr;

        //Axis Current
        public Joints MotorCurrent;

        //digital IO status
        public ushort DINs;
        public ushort DOUTs;

        //digital O command
        public ushort DOUT;

        public UInt32 LatePackets;

        public long IPOC;
        public long IPOCOld;
        public int DeltaT;

        //path stop (stop 2)
        public int HALT;

        //stop correction
        public int STOPCORR;

        public string ErrorString;

        public bool SingleStepCorr;

        //Correction limit single step
        public double PosL, RotL;

        public KUKA()
        {
            POSAct = new POS6DOF();
            POSTar = new POS6DOF();
            POSMON = new POS6DOF();
            POSCorr = new POS6DOF();

            MotorCurrent = new Joints();

            SensorName = "ImFree";
            ErrorString = string.Empty;

            SingleStepCorr = true;

            xmlDoc = new XmlDocument();

            PosL = 0.01;
            RotL = 0.01;
        }

        public void processXML(byte[] udpdata)
        {
            string XMLText = Encoding.ASCII.GetString(udpdata);

            xmlDoc.LoadXml(XMLText);
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/Rob");
            XmlNode node = nodes[0];

            this.POSAct.X = double.Parse(node.SelectSingleNode("RIst").Attributes["X"].Value);
            this.POSAct.Y = double.Parse(node.SelectSingleNode("RIst").Attributes["Y"].Value);
            this.POSAct.Z = double.Parse(node.SelectSingleNode("RIst").Attributes["Z"].Value);
            this.POSAct.A = double.Parse(node.SelectSingleNode("RIst").Attributes["A"].Value);
            this.POSAct.B = double.Parse(node.SelectSingleNode("RIst").Attributes["B"].Value);
            this.POSAct.C = double.Parse(node.SelectSingleNode("RIst").Attributes["C"].Value);

            this.POSTar.X = double.Parse(node.SelectSingleNode("RSol").Attributes["X"].Value);
            this.POSTar.Y = double.Parse(node.SelectSingleNode("RSol").Attributes["Y"].Value);
            this.POSTar.Z = double.Parse(node.SelectSingleNode("RSol").Attributes["Z"].Value);
            this.POSTar.A = double.Parse(node.SelectSingleNode("RSol").Attributes["A"].Value);
            this.POSTar.B = double.Parse(node.SelectSingleNode("RSol").Attributes["B"].Value);
            this.POSTar.C = double.Parse(node.SelectSingleNode("RSol").Attributes["C"].Value);

            this.MotorCurrent.A1 = double.Parse(node.SelectSingleNode("MACur").Attributes["A1"].Value);
            this.MotorCurrent.A2 = double.Parse(node.SelectSingleNode("MACur").Attributes["A2"].Value);
            this.MotorCurrent.A3 = double.Parse(node.SelectSingleNode("MACur").Attributes["A3"].Value);
            this.MotorCurrent.A4 = double.Parse(node.SelectSingleNode("MACur").Attributes["A4"].Value);
            this.MotorCurrent.A5 = double.Parse(node.SelectSingleNode("MACur").Attributes["A5"].Value);
            this.MotorCurrent.A6 = double.Parse(node.SelectSingleNode("MACur").Attributes["A6"].Value);

            this.LatePackets = UInt32.Parse(node.SelectSingleNode("Delay").Attributes["D"].Value);

            this.POSMON.X = double.Parse(node.SelectSingleNode("POSMON").Attributes["X"].Value);
            this.POSMON.Y = double.Parse(node.SelectSingleNode("POSMON").Attributes["Y"].Value);
            this.POSMON.Z = double.Parse(node.SelectSingleNode("POSMON").Attributes["Z"].Value);
            this.POSMON.A = double.Parse(node.SelectSingleNode("POSMON").Attributes["A"].Value);
            this.POSMON.B = double.Parse(node.SelectSingleNode("POSMON").Attributes["B"].Value);
            this.POSMON.C = double.Parse(node.SelectSingleNode("POSMON").Attributes["C"].Value);

            this.IPOCOld = this.IPOC;
            this.IPOC = long.Parse(node.SelectSingleNode("IPOC").InnerText);
            this.DeltaT = (int)(this.IPOC - this.IPOCOld);
        }

        public string CreateXMLOut()
        {
            checkCorrLimits();

            XDocument doc = new XDocument(new XElement("Sen",
                                           new XAttribute("Type", this.SensorName),
                                           new XElement("EStr", this.ErrorString),
                                           new XElement("RKorr",
                                            new XAttribute("X", string.Format("{0:0.000}", this.POSCorr.X)),
                                            new XAttribute("Y", string.Format("{0:0.000}", this.POSCorr.Y)),
                                            new XAttribute("Z", string.Format("{0:0.000}", this.POSCorr.Z)),
                                            new XAttribute("A", string.Format("{0:0.000}", this.POSCorr.A)),
                                            new XAttribute("B", string.Format("{0:0.000}", this.POSCorr.B)),
                                            new XAttribute("C", string.Format("{0:0.000}", this.POSCorr.C))),
                                           new XElement("DOUT", this.DOUT),
                                           new XElement("HALT", this.HALT),
                                           new XElement("STOPCORR", this.STOPCORR),
                                           new XElement("IPOC", this.IPOC)));

            if (this.SingleStepCorr)
                resetCorr();

            return doc.ToString();
        }

        void checkCorrLimits()
        {
            this.POSCorr.X = Clamp(this.POSCorr.X, -PosL, PosL);
            this.POSCorr.Y = Clamp(this.POSCorr.Y, -PosL, PosL);
            this.POSCorr.Z = Clamp(this.POSCorr.Z, -PosL, PosL);
            this.POSCorr.A = Clamp(this.POSCorr.A, -RotL, RotL);
            this.POSCorr.B = Clamp(this.POSCorr.B, -RotL, RotL);
            this.POSCorr.C = Clamp(this.POSCorr.C, -RotL, RotL);
        }

        void resetCorr()
        {
            this.POSCorr.X = 0;
            this.POSCorr.Y = 0;
            this.POSCorr.Z = 0;
            this.POSCorr.A = 0;
            this.POSCorr.B = 0;
            this.POSCorr.C = 0;
        }

        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public string Print()
        {
            string s;

            s = "Actual Position\r\n" + Print6DOF(POSAct) + "Target Position\r\n" + Print6DOF(POSTar) + 
                "Total Correction\r\n" + Print6DOF(POSMON) + "Motor Currents\r\n" + PrintJoints(MotorCurrent);
            return s;
        }

        public string Print6DOF(POS6DOF R, bool newline = true)
        {
            string s;

            if (newline)
                s = string.Format(" X = {0:0.000}\r\n Y = {1:0.000}\r\n Z = {2:0.000}\r\n A = {3:0.000}\r\n B = {4:0.000}\r\n C = {5:0.000}\r\n",
                    R.X, R.Y, R.Z, R.A, R.B, R.C);
            else
                s = string.Format("{0:0.000} {1:0.000} {2:0.000} {3:0.000} {4:0.000} {5:0.000}",
                R.X, R.Y, R.Z, R.A, R.B, R.C);

            return s;
        }

        public string PrintJoints(Joints R, bool newline = true)
        {
            string s;

            if (newline)
                s = string.Format(" A1 = {0:0.000}\r\n A2 = {1:0.000}\r\n A3 = {2:0.000}\r\n A4 = {3:0.000}\r\n A5 = {4:0.000}\r\n A6 = {5:0.000}\r\n",
                    R.A1, R.A2, R.A3, R.A4, R.A5, R.A6);
            else
                s = string.Format("{0:0.000} {1:0.000} {2:0.000} {3:0.000} {4:0.000} {5:0.000}",
                R.A1, R.A2, R.A3, R.A4, R.A5, R.A6);

            return s;
        }
    }
}
