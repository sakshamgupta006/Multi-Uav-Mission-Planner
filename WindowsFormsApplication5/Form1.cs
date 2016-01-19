using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication5
{
    public partial class MultiUAV : Form
    {
        static SerialPort port1 = new SerialPort();
        static SerialPort port2 = new SerialPort();
        static SerialPort port3 = new SerialPort();
        static SerialPort main = new SerialPort();
        static bool flag1 = false;
        static bool flag2 = false;
        static bool flag3 = false;
        static Int32 count = 0;

        static byte[] buffer = new byte[14400];
        static byte[] buffer2 = new byte[14400];


        public MultiUAV()
        {

            InitializeComponent();
            // panel1.Enabled = false;
            // panel2.Enabled = false;
            // panel3.Enabled = false;
            foreach (string p in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(p);
                comboBox3.Items.Add(p);
                comboBox5.Items.Add(p);
                comboBox7.Items.Add(p);

            }


            // port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);



            /*     System.Threading.Thread t1 = new System.Threading.Thread(delegate()
                 {
                     while (1 == 1)
                     {
                         while (flag1)
                         {
                             port1.Read(buffer, 0 , 38);

                             main.Write(buffer, 0 , 38);
                             count++;
                             Console.WriteLine("writing in port 1: "+count);
                        
                         }

                         while (flag2)
                         {
                             port2.Read(buffer, 0, 38);
                             main.Write(buffer, 0, 38);
                             count++;
                             Console.WriteLine("writing in port 2 "+ count);
                         }

                         while (flag3)
                         {
                             port3.Read(buffer, 0, 38);
                             main.Write(buffer, 0, 38);
                             Console.WriteLine("writing in port 3");
                         }
                     }

                 });

                 t1.Start();




                 System.Threading.Thread t2 = new System.Threading.Thread(delegate()
                 {
                     while (1 == 1)
                     {
                         while (flag1)
                         {
                        

                             port1.Write(main.ReadExisting());
                             count++;
                             Console.WriteLine("writing in PX4: " + count);

                         }

                         while (flag2)
                         {
                             port1.Write(main.ReadExisting());
                             count++;
                             Console.WriteLine("writing in apm" + count);
                         }

                         while (flag3)
                         {
                             main.Read(buffer, 0, 264);
                             port3.Write(buffer, 0, 264);
                             Console.WriteLine("writing in port 3");
                         }
                     }

                 });

                 t2.Start();

             

                 */



        }

        //public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        System.Threading.Thread t1 = new System.Threading.Thread(delegate()
        {
            while (1 == 1)
            {
                run_always();
            }
        });


        static void run_always()
        {

            if (flag1)
            {
                //main.Write(port1.ReadExisting());
                //int i=0;
                //string text;
                //text = port1.ReadExisting();
                Int32 x = port1.BytesToRead;
                byte[] val = new byte[x];
                for (int i = 0; i < x; i++)
                {
                    val[i] = Convert.ToByte(port1.ReadByte());
                }

                //val = Convert.ToByte(text);
                // byte[] val = new byte[p];
                /* foreach(char obj in text)
                 {
                     val[i] = Convert.ToByte(obj);
                     i++;
                 }  */
                main.Write(val, 0, x);
                //System.Threading.Thread.Sleep(1);
                //port1.DiscardInBuffer();
                //System.Threading.Thread.Sleep(200);
                // port1.DiscardInBuffer();

            }

            if (flag2)
            {
                Int32 x = port2.BytesToRead;
                byte[] val = new byte[x];
                for (int i = 0; i < x; i++)
                {
                    val[i] = Convert.ToByte(port2.ReadByte());
                }
                main.Write(val, 0, x);
                //main.Write(port2.ReadExisting());
                //System.Threading.Thread.Sleep(200);
                //port2.DiscardInBuffer();

            }

            if (flag3)
            {
                Int32 x = port3.BytesToRead;
                byte[] val = new byte[x];
                for (int i = 0; i < x; i++)
                {
                    val[i] = Convert.ToByte(port3.ReadByte());
                }
                main.Write(val, 0, x);
                //main.Write(port3.ReadExisting());
                //System.Threading.Thread.Sleep(200);
                //port3.DiscardInBuffer();

            }
        }

        //public static void DataReceivedHandler_main(object sender, SerialDataReceivedEventArgs e)
        System.Threading.Thread t2 = new System.Threading.Thread(delegate()
        {
            while (1 == 1)
            {
                if (main.BytesToRead > 0)
                { bakchod(); }

            }

        });

        static void bakchod()
        {
            Int32 x = main.BytesToRead;
            byte[] val = new byte[x];
            for (int i = 0; i < x; i++)
            {
                val[i] = Convert.ToByte(main.ReadByte());
            }

            if (flag1)
            {
                port1.Write(val, 0, x);

            }

            if (flag2)
            {
                port2.Write(val, 0, x);

            }

            if (flag3)
            {
                port3.Write(val, 0, x);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Connect")
            {
                try
                {

                    port1.PortName = comboBox1.Text;


                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

                    port1.Parity = Parity.None;
                    port1.DataBits = 8;
                    port1.StopBits = StopBits.One;
                    // port1.Handshake = Handshake.XOnXOff;
                    // port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    port1.Open();

                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;

                    button2.Text = "Disconnect";
                }

                catch (Exception E)
                {
                    MessageBox.Show("ERROR:COM port could not be opened");
                }


            }

            else if (button2.Text == "Disconnect")
            {
                port1.Close();
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;

                button2.Text = "Connect";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Connect")
            {
                try
                {

                    port2.PortName = comboBox3.Text;


                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

                    port2.Parity = Parity.None;
                    port2.DataBits = 8;
                    port2.StopBits = StopBits.One;
                    //port2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    port2.Open();

                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;

                    button3.Text = "Disconnect";
                }

                catch (Exception E)
                {
                    MessageBox.Show("ERROR:COM port could not be opened");
                }


            }

            else if (button3.Text == "Disconnect")
            {
                port2.Close();
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;

                button3.Text = "Connect";

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Connect")
            {
                panel1.Enabled = true;
                panel2.Enabled = true;
                panel3.Enabled = true;
                try
                {

                    port3.PortName = comboBox5.Text;


                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

                    port3.Parity = Parity.None;
                    port3.DataBits = 8;
                    port3.StopBits = StopBits.One;
                    //port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                    port3.Open();

                    comboBox5.Enabled = false;
                    comboBox6.Enabled = false;


                    button4.Text = "Disconnect";
                }

                catch (Exception E)
                {
                    MessageBox.Show("ERROR:COM port could not be opened");
                }


            }

            else if (button4.Text == "Disconnect")
            {
                main.Close();
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;


                button4.Text = "Connect";

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Connect")
            {

                try
                {

                    main.PortName = comboBox7.Text;


                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

                    main.Parity = Parity.None;
                    main.DataBits = 8;
                    main.StopBits = StopBits.One;
                    //main.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_main);
                    main.Open();
                    t1.Start();
                    t2.Start();
                    comboBox7.Enabled = false;
                    comboBox8.Enabled = false;

                    button1.Text = "Disconnect";
                }

                catch (Exception E)
                {
                    MessageBox.Show("ERROR:COM port could not be opened");
                }


            }

            else if (button1.Text == "Disconnect")
            {
                main.Close();
                comboBox7.Enabled = true;
                comboBox8.Enabled = true;
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;

                button1.Text = "Connect";

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (main.IsOpen && port1.IsOpen)
            {
                flag1 = true;
                flag2 = false;
                flag3 = false;
                panel1.BackColor = Color.Green;
                panel2.BackColor = Color.Red;
                panel3.BackColor = Color.Red;
            }
            else
            {
                panel1.BackColor = Color.Red;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (main.IsOpen && port2.IsOpen)
            {
                flag1 = false;
                flag2 = true;
                flag3 = false;
                panel3.BackColor = Color.Green;
                panel1.BackColor = Color.Red;
                panel2.BackColor = Color.Red;
            }
            else
            {
                panel3.BackColor = Color.Red;
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (main.IsOpen && port3.IsOpen)
            {
                flag1 = false;
                flag2 = false;
                flag3 = true;
                panel2.BackColor = Color.Green;
                panel1.BackColor = Color.Red;
                panel3.BackColor = Color.Red;
            }
            else
            {
                panel2.BackColor = Color.Red;
            }
        }
    }
}















//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.IO.Ports;

//namespace WindowsFormsApplication5
//{
//    public partial class Form1 : Form
//    {
//        static SerialPort port1 = new SerialPort();
//        static SerialPort port2 = new SerialPort();
//        static SerialPort port3 = new SerialPort();
//        static SerialPort main = new SerialPort();
//        static bool flag1 = false;
//        static bool flag2 = false;
//        static bool flag3 = false;
//        static Int32 count = 0;

//        static byte[] buffer = new byte[14400];
//        static byte[] buffer2 = new byte[14400];


//        public Form1()
//        {

//            InitializeComponent();
           
//            foreach (string p in SerialPort.GetPortNames())
//            {
//                comboBox1.Items.Add(p);
//                comboBox3.Items.Add(p);
//                comboBox5.Items.Add(p);
//                comboBox7.Items.Add(p);

//            }


            


//        }

//        public static void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
//        {
//            if (flag1)
//            {
                
//                Int32 x = port1.BytesToRead;
//                byte[] val = new byte[x];
//                for (int i = 0; i < x; i++)
//                {
//                    val[i] = Convert.ToByte(port1.ReadByte());
//                }

               
//                main.Write(val, 0, x);
                

//            }

           
//        }


//        public static void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
//        {
            

//            if (flag2)
//            {
//                Int32 x = port2.BytesToRead;
//                byte[] val = new byte[x];
//                for (int i = 0; i < x; i++)
//                {
//                    val[i] = Convert.ToByte(port2.ReadByte());
//                }
//                main.Write(val, 0, x);
                

//            }

            
//        }


//        public static void DataReceivedHandler3(object sender, SerialDataReceivedEventArgs e)
//        {
            

//            if (flag3)
//            {
//                Int32 x = port3.BytesToRead;
//                byte[] val = new byte[x];
//                for (int i = 0; i < x; i++)
//                {
//                    val[i] = Convert.ToByte(port3.ReadByte());
//                }
//                main.Write(val, 0, x);
                

//            }
//        }

//        public static void DataReceivedHandler_main(object sender, SerialDataReceivedEventArgs e)
//        {
//            Int32 x = main.BytesToRead;
//            byte[] val = new byte[x];
//            for (int i = 0; i < x; i++)
//            {
//                val[i] = Convert.ToByte(main.ReadByte());
//            }

//            if (flag1)
//            {
//                port1.Write(val, 0, x);

//            }

//            if (flag2)
//            {
//                port2.Write(val, 0, x);

//            }

//            if (flag3)
//            {
//                port3.Write(val, 0, x);

//            }

//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            if (button2.Text == "Connect")
//            {
//                try
//                {

//                    port1.PortName = comboBox1.Text;


//                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

//                    port1.Parity = Parity.None;
//                    port1.DataBits = 8;
//                    port1.StopBits = StopBits.One;
//                    //port1.Handshake = Handshake.XOnXOff; do not enable this bastard
//                    port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);
//                    port1.Open();

//                    comboBox1.Enabled = false;
//                    comboBox2.Enabled = false;

//                    button2.Text = "Disconnect";
//                }

//                catch (Exception E)
//                {
//                    MessageBox.Show("ERROR:COM port could not be opened");
//                }


//            }

//            else if (button2.Text == "Disconnect")
//            {
//                port1.Close();
//                comboBox1.Enabled = true;
//                comboBox2.Enabled = true;

//                button2.Text = "Connect";

//            }
//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            if (button3.Text == "Connect")
//            {
//                try
//                {

//                    port2.PortName = comboBox3.Text;


//                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

//                    port2.Parity = Parity.None;
//                    port2.DataBits = 8;
//                    port2.StopBits = StopBits.One;
//                    port2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);
//                    port2.Open();

//                    comboBox3.Enabled = false;
//                    comboBox4.Enabled = false;

//                    button3.Text = "Disconnect";
//                }

//                catch (Exception E)
//                {
//                    MessageBox.Show("ERROR:COM port could not be opened");
//                }


//            }

//            else if (button3.Text == "Disconnect")
//            {
//                port2.Close();
//                comboBox3.Enabled = true;
//                comboBox4.Enabled = true;

//                button3.Text = "Connect";

//            }

//        }

//        private void button4_Click(object sender, EventArgs e)
//        {
//            if (button4.Text == "Connect")
//            {
//                panel1.Enabled = true;
//                panel2.Enabled = true;
//                panel3.Enabled = true;
//                try
//                {

//                    port3.PortName = comboBox5.Text;


//                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

//                    port3.Parity = Parity.None;
//                    port3.DataBits = 8;
//                    port3.StopBits = StopBits.One;
//                    port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler3);

//                    port3.Open();

//                    comboBox5.Enabled = false;
//                    comboBox6.Enabled = false;


//                    button4.Text = "Disconnect";
//                }

//                catch (Exception E)
//                {
//                    MessageBox.Show("ERROR:COM port could not be opened");
//                }


//            }

//            else if (button4.Text == "Disconnect")
//            {
//                main.Close();
//                comboBox5.Enabled = true;
//                comboBox6.Enabled = true;


//                button4.Text = "Connect";

//            }

//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (button1.Text == "Connect")
//            {

//                try
//                {

//                    main.PortName = comboBox7.Text;


//                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

//                    main.Parity = Parity.None;
//                    main.DataBits = 8;
//                    main.StopBits = StopBits.One;
//                    main.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_main);
//                    main.Open();

//                    comboBox7.Enabled = false;
//                    comboBox8.Enabled = false;

//                    button1.Text = "Disconnect";
//                }

//                catch (Exception E)
//                {
//                    MessageBox.Show("ERROR:COM port could not be opened");
//                }


//            }

//            else if (button1.Text == "Disconnect")
//            {
//                main.Close();
//                comboBox7.Enabled = true;
//                comboBox8.Enabled = true;
//                panel1.Enabled = false;
//                panel2.Enabled = false;
//                panel3.Enabled = false;

//                button1.Text = "Connect";

//            }
//        }

//        private void pictureBox1_Click(object sender, EventArgs e)
//        {
//            if (main.IsOpen && port1.IsOpen)
//            {
//                flag1 = true;
//                flag2 = false;
//                flag3 = false;
//                panel1.BackColor = Color.Green;
//                panel2.BackColor = Color.Red;
//                panel3.BackColor = Color.Red;
//            }
//            else
//            {
//                panel1.BackColor = Color.Red;
//            }
//        }

//        private void pictureBox2_Click(object sender, EventArgs e)
//        {
//            if (main.IsOpen && port2.IsOpen)
//            {
//                flag1 = false;
//                flag2 = true;
//                flag3 = false;
//                panel3.BackColor = Color.Green;
//                panel1.BackColor = Color.Red;
//                panel2.BackColor = Color.Red;
//            }
//            else
//            {
//                panel3.BackColor = Color.Red;
//            }

//        }

//        private void pictureBox3_Click(object sender, EventArgs e)
//        {
//            if (main.IsOpen && port3.IsOpen)
//            {
//                flag1 = false;
//                flag2 = false;
//                flag3 = true;
//                panel2.BackColor = Color.Green;
//                panel1.BackColor = Color.Red;
//                panel3.BackColor = Color.Red;
//            }
//            else
//            {
//                panel2.BackColor = Color.Red;
//            }
//        }
//    }
//}



////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;
////using System.IO.Ports;

////namespace WindowsFormsApplication5
////{
////    public partial class Form1 : Form
////    {
////        static SerialPort port1;
////        static SerialPort port2;
////        static SerialPort port3;
////        static SerialPort main;

////        bool flag1=false;
////        bool flag2=false;
////        bool flag3=false;


////        public Form1()
////        {

////            InitializeComponent();
////            panel1.Enabled = false;
////            panel2.Enabled = false;
////            panel3.Enabled = false;
////            foreach (string p in SerialPort.GetPortNames())
////            {
////                comboBox1.Items.Add(p);
////                comboBox3.Items.Add(p);
////                comboBox5.Items.Add(p);
////                comboBox7.Items.Add(p);

////            }

////            port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

////            System.Threading.Thread t1 = new System.Threading.Thread(delegate()
////            {
////                while (flag1)
////                { 

////                }

////                while (flag2)
////                {
////                    main.WriteLine(port2.ReadLine());
////                }

////                while (flag3)
////                {
////                    main.WriteLine(port3.ReadLine());
////                }




















////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;
////using System.IO.Ports;

////namespace WindowsFormsApplication5
////{
////    public partial class Form1 : Form
////    {
////        static SerialPort port1 = new SerialPort();
////        static SerialPort port2 = new SerialPort();
////        static SerialPort port3 = new SerialPort();
////        static SerialPort main = new SerialPort();
////        static bool flag1 = false;
////        static bool flag2 = false;
////        static bool flag3 = false;
////        static Int32 count = 0;

////        static byte[] buffer = new byte[14400];
////        static byte[] buffer2 = new byte[14400];


////        public Form1()
////        {

////            InitializeComponent();
////            // panel1.Enabled = false;
////            // panel2.Enabled = false;
////            // panel3.Enabled = false;
////            foreach (string p in SerialPort.GetPortNames())
////            {
////                comboBox1.Items.Add(p);
////                comboBox3.Items.Add(p);
////                comboBox5.Items.Add(p);
////                comboBox7.Items.Add(p);

////            }


////            // port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);



////            /*     System.Threading.Thread t1 = new System.Threading.Thread(delegate()
////                 {
////                     while (1 == 1)
////                     {
////                         while (flag1)
////                         {
////                             port1.Read(buffer, 0 , 38);

////                             main.Write(buffer, 0 , 38);
////                             count++;
////                             Console.Line("writing in port 1: "+count);
                        
////                         }

////                         while (flag2)
////                         {
////                             port2.Read(buffer, 0, 38);
////                             main.Write(buffer, 0, 38);
////                             count++;
////                             Console.WriteLine("writing in port 2 "+ count);
////                         }

////                         while (flag3)
////                         {
////                             port3.Read(buffer, 0, 38);
////                             main.Write(buffer, 0, 38);
////                             Console.WriteLine("writing in port 3");
////                         }
////                     }

////                 });

////                 t1.Start();




////                 System.Threading.Thread t2 = new System.Threading.Thread(delegate()
////                 {
////                     while (1 == 1)
////                     {
////                         while (flag1)
////                         {
                        

////                             port1.Write(main.ReadExisting());
////                             count++;
////                             Console.WriteLine("writing in PX4: " + count);

////                         }

////                         while (flag2)
////                         {
////                             port1.Write(main.ReadExisting());
////                             count++;
////                             Console.WriteLine("writing in apm" + count);
////                         }

////                         while (flag3)
////                         {
////                             main.Read(buffer, 0, 264);
////                             port3.Write(buffer, 0, 264);
////                             Console.WriteLine("writing in port 3");
////                         }
////                     }

////                 });

////                 t2.Start();

             

////                 */



////        }

////        public static void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
////        {
////            if (flag1)
////            {
////                //main.Write(port1.ReadExisting());
////                //int i=0;
////                //string text;
////                //text = port1.ReadExisting();
////                Int32 x = port1.BytesToRead;
////                byte[] val = new byte[x];
////                for (int i = 0; i < x; i++)
////                {
////                    val[i] = Convert.ToByte(port1.ReadByte());
////                }

////                //val = Convert.ToByte(text);
////                // byte[] val = new byte[p];
////                /* foreach(char obj in text)
////                 {
////                     val[i] = Convert.ToByte(obj);
////                     i++;
////                 }  */
////                main.Write(val, 0, x);
////                //System.Threading.Thread.Sleep(1);
////                //port1.DiscardInBuffer();
////                //System.Threading.Thread.Sleep(200);
////                // port1.DiscardInBuffer();

////            }

////            /* if (flag2)
////             {
////                 Int32 x = port2.BytesToRead;
////                 byte[] val = new byte[x];
////                 for (int i = 0; i < x; i++)
////                 {
////                     val[i] = Convert.ToByte(port2.ReadByte());
////                 }
////                 main.Write(val, 0, x);
////                 //main.Write(port2.ReadExisting());
////                 //System.Threading.Thread.Sleep(200);
////                 //port2.DiscardInBuffer();

////             }

////             if (flag3)
////             {
////                 Int32 x = port3.BytesToRead;
////                 byte[] val = new byte[x];
////                 for (int i = 0; i < x; i++)
////                 {
////                     val[i] = Convert.ToByte(port3.ReadByte());
////                 }
////                 main.Write(val, 0, x);
////                 //main.Write(port3.ReadExisting());
////                 //System.Threading.Thread.Sleep(200);
////                 //port3.DiscardInBuffer();

////             } */
////        }


////        public static void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
////        {
////            /* if (flag1)
////             {
////                 //main.Write(port1.ReadExisting());
////                 //int i=0;
////                 //string text;
////                 //text = port1.ReadExisting();
////                 Int32 x = port1.BytesToRead;
////                 byte[] val = new byte[x];
////                 for (int i = 0; i < x; i++)
////                 {
////                     val[i] = Convert.ToByte(port1.ReadByte());
////                 }

////                 //val = Convert.ToByte(text);
////                 // byte[] val = new byte[p];
////                  foreach(char obj in text)
////                  {
////                      val[i] = Convert.ToByte(obj);
////                      i++;
////                  }  
////                 main.Write(val, 0, x);
////                 //System.Threading.Thread.Sleep(1);
////                 //port1.DiscardInBuffer();
////                 //System.Threading.Thread.Sleep(200);
////                 // port1.DiscardInBuffer();

////             } */

////            if (flag2)
////            {
////                Int32 x = port2.BytesToRead;
////                byte[] val = new byte[x];
////                for (int i = 0; i < x; i++)
////                {
////                    val[i] = Convert.ToByte(port2.ReadByte());
////                }
////                main.Write(val, 0, x);
////                //main.Write(port2.ReadExisting());
////                //System.Threading.Thread.Sleep(200);
////                //port2.DiscardInBuffer();

////            }

////            /* if (flag3)
////             {
////                 Int32 x = port3.BytesToRead;
////                 byte[] val = new byte[x];
////                 for (int i = 0; i < x; i++)
////                 {
////                     val[i] = Convert.ToByte(port3.ReadByte());
////                 }
////                 main.Write(val, 0, x);
////                 //main.Write(port3.ReadExisting());
////                 //System.Threading.Thread.Sleep(200);
////                 //port3.DiscardInBuffer();

////             } */
////        }


////        public static void DataReceivedHandler3(object sender, SerialDataReceivedEventArgs e)
////        {
////            /*if (flag1)
////            {
////                //main.Write(port1.ReadExisting());
////                //int i=0;
////                //string text;
////                //text = port1.ReadExisting();
////                Int32 x = port1.BytesToRead;
////                byte[] val = new byte[x];
////                for (int i = 0; i < x; i++)
////                {
////                    val[i] = Convert.ToByte(port1.ReadByte());
////                }

////                //val = Convert.ToByte(text);
////                // byte[] val = new byte[p];
////                 foreach(char obj in text)
////                 {
////                     val[i] = Convert.ToByte(obj);
////                     i++;
////                 }  
////                main.Write(val, 0, x);
////                //System.Threading.Thread.Sleep(1);
////                //port1.DiscardInBuffer();
////                //System.Threading.Thread.Sleep(200);
////                // port1.DiscardInBuffer();

////            }

////            if (flag2)
////            {
////                Int32 x = port2.BytesToRead;
////                byte[] val = new byte[x];
////                for (int i = 0; i < x; i++)
////                {
////                    val[i] = Convert.ToByte(port2.ReadByte());
////                }
////                main.Write(val, 0, x);
////                //main.Write(port2.ReadExisting());
////                //System.Threading.Thread.Sleep(200);
////                //port2.DiscardInBuffer();

////            } */

////            if (flag3)
////            {
////                Int32 x = port3.BytesToRead;
////                byte[] val = new byte[x];
////                for (int i = 0; i < x; i++)
////                {
////                    val[i] = Convert.ToByte(port3.ReadByte());
////                }
////                main.Write(val, 0, x);
////                //main.Write(port3.ReadExisting());
////                //System.Threading.Thread.Sleep(200);
////                //port3.DiscardInBuffer();

////            }
////        }

////        public static void DataReceivedHandler_main(object sender, SerialDataReceivedEventArgs e)
////        {
////            Int32 x = main.BytesToRead;
////            byte[] val = new byte[x];
////            for (int i = 0; i < x; i++)
////            {
////                val[i] = Convert.ToByte(main.ReadByte());
////            }


////            if (flag1)
////            {
////                port1.Write(val, 0, x);

////            }


////            if (flag2)
////            {
////                port2.Write(val, 0, x);

////            }

////            if (flag3)
////            {
////                port3.Write(val, 0, x);

////            }

////        }

////        private void button2_Click(object sender, EventArgs e)
////        {
////            if (button2.Text == "Connect")
////            {
////                try
////                {

////                    port1.PortName = comboBox1.Text;


////                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

////                    port1.Parity = Parity.None;
////                    port1.DataBits = 8;
////                    port1.StopBits = StopBits.One;
////                    //port1.Handshake = Handshake.XOnXOff;
////                    port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);
////                    port1.Open();

////                    comboBox1.Enabled = false;
////                    comboBox2.Enabled = false;

////                    button2.Text = "Disconnect";
////                }

////                catch (Exception E)
////                {
////                    MessageBox.Show("ERROR:COM port could not be opened");
////                }


////            }

////            else if (button2.Text == "Disconnect")
////            {
////                port1.Close();
////                comboBox1.Enabled = true;
////                comboBox2.Enabled = true;

////                button2.Text = "Connect";

////            }
////        }

////        private void button3_Click(object sender, EventArgs e)
////        {
////            if (button3.Text == "Connect")
////            {
////                try
////                {

////                    port2.PortName = comboBox3.Text;


////                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

////                    port2.Parity = Parity.None;
////                    port2.DataBits = 8;
////                    port2.StopBits = StopBits.One;
////                    port2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);
////                    port2.Open();

////                    comboBox3.Enabled = false;
////                    comboBox4.Enabled = false;

////                    button3.Text = "Disconnect";
////                }

////                catch (Exception E)
////                {
////                    MessageBox.Show("ERROR:COM port could not be opened");
////                }


////            }

////            else if (button3.Text == "Disconnect")
////            {
////                port2.Close();
////                comboBox3.Enabled = true;
////                comboBox4.Enabled = true;

////                button3.Text = "Connect";

////            }

////        }

////        private void button4_Click(object sender, EventArgs e)
////        {
////            if (button4.Text == "Connect")
////            {
////                panel1.Enabled = true;
////                panel2.Enabled = true;
////                panel3.Enabled = true;
////                try
////                {

////                    port3.PortName = comboBox5.Text;


////                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

////                    port3.Parity = Parity.None;
////                    port3.DataBits = 8;
////                    port3.StopBits = StopBits.One;
////                    port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler3);

////                    port3.Open();

////                    comboBox5.Enabled = false;
////                    comboBox6.Enabled = false;


////                    button4.Text = "Disconnect";
////                }

////                catch (Exception E)
////                {
////                    MessageBox.Show("ERROR:COM port could not be opened");
////                }


////            }

////            else if (button4.Text == "Disconnect")
////            {
////                main.Close();
////                comboBox5.Enabled = true;
////                comboBox6.Enabled = true;


////                button4.Text = "Connect";

////            }

////        }

////        private void button1_Click(object sender, EventArgs e)
////        {
////            if (button1.Text == "Connect")
////            {

////                try
////                {

////                    main.PortName = comboBox7.Text;


////                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

////                    main.Parity = Parity.None;
////                    main.DataBits = 8;
////                    main.StopBits = StopBits.One;
////                    main.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_main);
////                    main.Open();

////                    comboBox7.Enabled = false;
////                    comboBox8.Enabled = false;

////                    button1.Text = "Disconnect";
////                }

////                catch (Exception E)
////                {
////                    MessageBox.Show("ERROR:COM port could not be opened");
////                }


////            }

////            else if (button1.Text == "Disconnect")
////            {
////                main.Close();
////                comboBox7.Enabled = true;
////                comboBox8.Enabled = true;
////                panel1.Enabled = false;
////                panel2.Enabled = false;
////                panel3.Enabled = false;

////                button1.Text = "Connect";

////            }
////        }

////        private void pictureBox1_Click(object sender, EventArgs e)
////        {
////            if (main.IsOpen && port1.IsOpen)
////            {
////                flag1 = true;
////                flag2 = false;
////                flag3 = false;
////                panel1.BackColor = Color.Green;
////                panel2.BackColor = Color.Red;
////                panel3.BackColor = Color.Red;
////            }
////            else
////            {
////                panel1.BackColor = Color.Red;
////            }
////        }

////        private void pictureBox2_Click(object sender, EventArgs e)
////        {
////            if (main.IsOpen && port2.IsOpen)
////            {
////                flag1 = false;
////                flag2 = true;
////                flag3 = false;
////                panel3.BackColor = Color.Green;
////                panel1.BackColor = Color.Red;
////                panel2.BackColor = Color.Red;
////            }
////            else
////            {
////                panel3.BackColor = Color.Red;
////            }

////        }

////        private void pictureBox3_Click(object sender, EventArgs e)
////        {
////            if (main.IsOpen && port3.IsOpen)
////            {
////                flag1 = false;
////                flag2 = false;
////                flag3 = true;
////                panel2.BackColor = Color.Green;
////                panel1.BackColor = Color.Red;
////                panel3.BackColor = Color.Red;
////            }
////            else
////            {
////                panel2.BackColor = Color.Red;
////            }
////        }
////    }
////}





























//////using System;
//////using System.Collections.Generic;
//////using System.ComponentModel;
//////using System.Data;
//////using System.Drawing;
//////using System.Linq;
//////using System.Text;
//////using System.Threading.Tasks;
//////using System.Windows.Forms;
//////using System.IO.Ports;

//////namespace WindowsFormsApplication5
//////{
//////    public partial class Form1 : Form
//////    {
//////        static SerialPort port1 = new SerialPort();
//////        static SerialPort port2 = new SerialPort();
//////        static SerialPort port3 = new SerialPort();
//////        static SerialPort main = new SerialPort();
//////        static bool flag1 = false;
//////        static bool flag2 = false;
//////        static bool flag3 = false;
//////        static Int32 count = 0;

//////        static byte[] buffer = new byte[14400];
//////        static byte[] buffer2 = new byte[14400];


//////        public Form1()
//////        {

//////            InitializeComponent();
//////            // panel1.Enabled = false;
//////            // panel2.Enabled = false;
//////            // panel3.Enabled = false;
//////            foreach (string p in SerialPort.GetPortNames())
//////            {
//////                comboBox1.Items.Add(p);
//////                comboBox3.Items.Add(p);
//////                comboBox5.Items.Add(p);
//////                comboBox7.Items.Add(p);

//////            }


//////            // port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);



//////            /*     System.Threading.Thread t1 = new System.Threading.Thread(delegate()
//////                 {
//////                     while (1 == 1)
//////                     {
//////                         while (flag1)
//////                         {
//////                             port1.Read(buffer, 0 , 38);

//////                             main.Write(buffer, 0 , 38);
//////                             count++;
//////                             Console.WriteLine("writing in port 1: "+count);
                        
//////                         }

//////                         while (flag2)
//////                         {
//////                             port2.Read(buffer, 0, 38);
//////                             main.Write(buffer, 0, 38);
//////                             count++;
//////                             Console.WriteLine("writing in port 2 "+ count);
//////                         }

//////                         while (flag3)
//////                         {
//////                             port3.Read(buffer, 0, 38);
//////                             main.Write(buffer, 0, 38);
//////                             Console.WriteLine("writing in port 3");
//////                         }
//////                     }

//////                 });

//////                 t1.Start();




//////                 System.Threading.Thread t2 = new System.Threading.Thread(delegate()
//////                 {
//////                     while (1 == 1)
//////                     {
//////                         while (flag1)
//////                         {
                        

//////                             port1.Write(main.ReadExisting());
//////                             count++;
//////                             Console.WriteLine("writing in PX4: " + count);

//////                         }

//////                         while (flag2)
//////                         {
//////                             port1.Write(main.ReadExisting());
//////                             count++;
//////                             Console.WriteLine("writing in apm" + count);
//////                         }

//////                         while (flag3)
//////                         {
//////                             main.Read(buffer, 0, 264);
//////                             port3.Write(buffer, 0, 264);
//////                             Console.WriteLine("writing in port 3");
//////                         }
//////                     }

//////                 });

//////                 t2.Start();

             

//////                 */



//////        }

//////        public static void DataReceivedHandler1(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            if (flag1)
//////            {
//////                //main.Write(port1.ReadExisting());
//////                //int i=0;
//////                //string text;
//////                //text = port1.ReadExisting();
//////                Int32 x = port1.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port1.ReadByte());
//////                }

//////                //val = Convert.ToByte(text);
//////                // byte[] val = new byte[p];
//////                /* foreach(char obj in text)
//////                 {
//////                     val[i] = Convert.ToByte(obj);
//////                     i++;
//////                 }  */
//////                main.Write(val, 0, x);
//////                //System.Threading.Thread.Sleep(1);
//////                //port1.DiscardInBuffer();
//////                //System.Threading.Thread.Sleep(200);
//////                // port1.DiscardInBuffer();

//////            }

//////            /* if (flag2)
//////             {
//////                 Int32 x = port2.BytesToRead;
//////                 byte[] val = new byte[x];
//////                 for (int i = 0; i < x; i++)
//////                 {
//////                     val[i] = Convert.ToByte(port2.ReadByte());
//////                 }
//////                 main.Write(val, 0, x);
//////                 //main.Write(port2.ReadExisting());
//////                 //System.Threading.Thread.Sleep(200);
//////                 //port2.DiscardInBuffer();

//////             }

//////             if (flag3)
//////             {
//////                 Int32 x = port3.BytesToRead;
//////                 byte[] val = new byte[x];
//////                 for (int i = 0; i < x; i++)
//////                 {
//////                     val[i] = Convert.ToByte(port3.ReadByte());
//////                 }
//////                 main.Write(val, 0, x);
//////                 //main.Write(port3.ReadExisting());
//////                 //System.Threading.Thread.Sleep(200);
//////                 //port3.DiscardInBuffer();

//////             } */
//////        }


//////        public static void DataReceivedHandler2(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            /* if (flag1)
//////             {
//////                 //main.Write(port1.ReadExisting());
//////                 //int i=0;
//////                 //string text;
//////                 //text = port1.ReadExisting();
//////                 Int32 x = port1.BytesToRead;
//////                 byte[] val = new byte[x];
//////                 for (int i = 0; i < x; i++)
//////                 {
//////                     val[i] = Convert.ToByte(port1.ReadByte());
//////                 }

//////                 //val = Convert.ToByte(text);
//////                 // byte[] val = new byte[p];
//////                  foreach(char obj in text)
//////                  {
//////                      val[i] = Convert.ToByte(obj);
//////                      i++;
//////                  }  
//////                 main.Write(val, 0, x);
//////                 //System.Threading.Thread.Sleep(1);
//////                 //port1.DiscardInBuffer();
//////                 //System.Threading.Thread.Sleep(200);
//////                 // port1.DiscardInBuffer();

//////             } */

//////            if (flag2)
//////            {
//////                Int32 x = port2.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port2.ReadByte());
//////                }
//////                main.Write(val, 0, x);
//////                //main.Write(port2.ReadExisting());
//////                //System.Threading.Thread.Sleep(200);
//////                //port2.DiscardInBuffer();

//////            }

//////            /* if (flag3)
//////             {
//////                 Int32 x = port3.BytesToRead;
//////                 byte[] val = new byte[x];
//////                 for (int i = 0; i < x; i++)
//////                 {
//////                     val[i] = Convert.ToByte(port3.ReadByte());
//////                 }
//////                 main.Write(val, 0, x);
//////                 //main.Write(port3.ReadExisting());
//////                 //System.Threading.Thread.Sleep(200);
//////                 //port3.DiscardInBuffer();

//////             } */
//////        }


//////        public static void DataReceivedHandler3(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            /*if (flag1)
//////            {
//////                //main.Write(port1.ReadExisting());
//////                //int i=0;
//////                //string text;
//////                //text = port1.ReadExisting();
//////                Int32 x = port1.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port1.ReadByte());
//////                }

//////                //val = Convert.ToByte(text);
//////                // byte[] val = new byte[p];
//////                 foreach(char obj in text)
//////                 {
//////                     val[i] = Convert.ToByte(obj);
//////                     i++;
//////                 }  
//////                main.Write(val, 0, x);
//////                //System.Threading.Thread.Sleep(1);
//////                //port1.DiscardInBuffer();
//////                //System.Threading.Thread.Sleep(200);
//////                // port1.DiscardInBuffer();

//////            }

//////            if (flag2)
//////            {
//////                Int32 x = port2.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port2.ReadByte());
//////                }
//////                main.Write(val, 0, x);
//////                //main.Write(port2.ReadExisting());
//////                //System.Threading.Thread.Sleep(200);
//////                //port2.DiscardInBuffer();

//////            } */

//////            if (flag3)
//////            {
//////                Int32 x = port3.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port3.ReadByte());
//////                }
//////                main.Write(val, 0, x);
//////                //main.Write(port3.ReadExisting());
//////                //System.Threading.Thread.Sleep(200);
//////                //port3.DiscardInBuffer();

//////            }
//////        }

//////        public static void DataReceivedHandler_main(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            Int32 x = main.BytesToRead;
//////            byte[] val = new byte[x];
//////            for (int i = 0; i < x; i++)
//////            {
//////                val[i] = Convert.ToByte(main.ReadByte());
//////            }
            
          
//////            if (flag1)
//////            {
//////                port1.Write(val, 0, x);

//////            }


//////            if (flag2)
//////            {
//////                port2.Write(val, 0, x);

//////            }

//////            if (flag3)
//////            {
//////                port3.Write(val, 0, x);

//////            }

//////        }

//////        private void button2_Click(object sender, EventArgs e)
//////        {
//////            if (button2.Text == "Connect")
//////            {
//////                try
//////                {

//////                    port1.PortName = comboBox1.Text;


//////                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

//////                    port1.Parity = Parity.None;
//////                    port1.DataBits = 8;
//////                    port1.StopBits = StopBits.One;
//////                    //port1.Handshake = Handshake.XOnXOff;
//////                    port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler1);
//////                    port1.Open();

//////                    comboBox1.Enabled = false;
//////                    comboBox2.Enabled = false;

//////                    button2.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button2.Text == "Disconnect")
//////            {
//////                port1.Close();
//////                comboBox1.Enabled = true;
//////                comboBox2.Enabled = true;

//////                button2.Text = "Connect";

//////            }
//////        }

//////        private void button3_Click(object sender, EventArgs e)
//////        {
//////            if (button3.Text == "Connect")
//////            {
//////                try
//////                {

//////                    port2.PortName = comboBox3.Text;


//////                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

//////                    port2.Parity = Parity.None;
//////                    port2.DataBits = 8;
//////                    port2.StopBits = StopBits.One;
//////                    port2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler2);
//////                    port2.Open();

//////                    comboBox3.Enabled = false;
//////                    comboBox4.Enabled = false;

//////                    button3.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button3.Text == "Disconnect")
//////            {
//////                port2.Close();
//////                comboBox3.Enabled = true;
//////                comboBox4.Enabled = true;

//////                button3.Text = "Connect";

//////            }

//////        }

//////        private void button4_Click(object sender, EventArgs e)
//////        {
//////            if (button4.Text == "Connect")
//////            {
//////                panel1.Enabled = true;
//////                panel2.Enabled = true;
//////                panel3.Enabled = true;
//////                try
//////                {

//////                    port3.PortName = comboBox5.Text;


//////                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

//////                    port3.Parity = Parity.None;
//////                    port3.DataBits = 8;
//////                    port3.StopBits = StopBits.One;
//////                    port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler3);

//////                    port3.Open();

//////                    comboBox5.Enabled = false;
//////                    comboBox6.Enabled = false;


//////                    button4.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button4.Text == "Disconnect")
//////            {
//////                main.Close();
//////                comboBox5.Enabled = true;
//////                comboBox6.Enabled = true;


//////                button4.Text = "Connect";

//////            }

//////        }

//////        private void button1_Click(object sender, EventArgs e)
//////        {
//////            if (button1.Text == "Connect")
//////            {

//////                try
//////                {

//////                    main.PortName = comboBox7.Text;


//////                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

//////                    main.Parity = Parity.None;
//////                    main.DataBits = 8;
//////                    main.StopBits = StopBits.One;
//////                    main.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_main);
//////                    main.Open();

//////                    comboBox7.Enabled = false;
//////                    comboBox8.Enabled = false;

//////                    button1.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button1.Text == "Disconnect")
//////            {
//////                main.Close();
//////                comboBox7.Enabled = true;
//////                comboBox8.Enabled = true;
//////                panel1.Enabled = false;
//////                panel2.Enabled = false;
//////                panel3.Enabled = false;

//////                button1.Text = "Connect";

//////            }
//////        }

//////        private void pictureBox1_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port1.IsOpen)
//////            {
//////                flag1 = true;
//////                flag2 = false;
//////                flag3 = false;
//////                panel1.BackColor = Color.Green;
//////                panel2.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel1.BackColor = Color.Red;
//////            }
//////        }

//////        private void pictureBox2_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port2.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = true;
//////                flag3 = false;
//////                panel3.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel2.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel3.BackColor = Color.Red;
//////            }

//////        }

//////        private void pictureBox3_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port3.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = false;
//////                flag3 = true;
//////                panel2.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel2.BackColor = Color.Red;
//////            }
//////        }
//////    }
//////}

















/////////*using System;
////////using System.Collections.Generic;
////////using System.ComponentModel;
////////using System.Data;
////////using System.Drawing;
////////using System.Linq;
////////using System.Text;
////////using System.Threading.Tasks;
////////using System.Windows.Forms;
////////using System.IO.Ports;

////////namespace WindowsFormsApplication5
////////{
////////    public partial class Form1 : Form
////////    {
////////        static SerialPort port1= new SerialPort();
////////        static SerialPort port2=new SerialPort();
////////        static SerialPort port3=new SerialPort();
////////        static SerialPort main=new SerialPort();
////////        bool flag1=false;
////////        bool flag2=false;
////////        bool flag3=false;
////////        static Int32 count = 0;

////////        static byte[] buffer = new byte[1440000];


////////        public Form1()
////////        {
            
////////            InitializeComponent();
////////           // panel1.Enabled = false;
////////           // panel2.Enabled = false;
////////           // panel3.Enabled = false;
////////            foreach (string p in SerialPort.GetPortNames())
////////            {
////////                comboBox1.Items.Add(p);
////////                comboBox3.Items.Add(p);
////////                comboBox5.Items.Add(p);
////////                comboBox7.Items.Add(p);

////////            }

////////            System.Threading.Thread t1 = new System.Threading.Thread(delegate()
////////            {
////////                while (1 == 1)
////////                {
////////                    while (flag1)
////////                    {
////////                        port1.Read(buffer, count*8 , 8);

////////                        main.Write(buffer, count*8 , 8);
////////                        main.Read(buffer, count * 8, 8);
////////                        port1.Write(buffer, count * 8, 8);
////////                        count++;
////////                        Console.WriteLine("writing in port 1: "+count);
                        
////////                    }

////////                    while (flag2)
////////                    {
////////                        port2.Read(buffer, 0, 38);
////////                        main.Write(buffer, 0, 38);
////////                        count++;
////////                        Console.WriteLine("writing in port 2 "+ count);
////////                    }

////////                    while (flag3)
////////                    {
////////                        port3.Read(buffer, 0, 38);
////////                        main.Write(buffer, 0, 38);
////////                        Console.WriteLine("writing in port 3");
////////                    }
////////                }

////////            });

////////            t1.Start();

           

            
////////        }

////////        private void button2_Click(object sender, EventArgs e)
////////        {
////////            if (button2.Text == "Connect")
////////            {
////////                try
////////                {

////////                    port1.PortName = comboBox1.Text;


////////                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

////////                    port1.Parity = Parity.None;
////////                    port1.DataBits = 8;
////////                    port1.StopBits = StopBits.One;
////////                    port1.Handshake = Handshake.XOnXOff;
////////                    port1.Open();

////////                    comboBox1.Enabled = false;
////////                    comboBox2.Enabled = false;

////////                    button2.Text = "Disconnect";
////////                }

////////                catch (Exception E)
////////                {
////////                    MessageBox.Show("ERROR:COM port could not be opened");
////////                }


////////            }

////////            else if (button2.Text == "Disconnect")
////////            {
////////                port1.Close();
////////                comboBox1.Enabled = true;
////////                comboBox2.Enabled = true;

////////                button2.Text = "Connect";

////////            }
////////        }

////////        private void button3_Click(object sender, EventArgs e)
////////        {
////////            if (button3.Text == "Connect")
////////            {
////////                try
////////                {

////////                    port2.PortName = comboBox3.Text;


////////                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

////////                    port2.Parity = Parity.None;
////////                    port2.DataBits = 8;
////////                    port2.StopBits = StopBits.One;
////////                    port2.Open();

////////                    comboBox3.Enabled = false;
////////                    comboBox4.Enabled = false;

////////                    button3.Text = "Disconnect";
////////                }

////////                catch (Exception E)
////////                {
////////                    MessageBox.Show("ERROR:COM port could not be opened");
////////                }


////////            }

////////            else if (button3.Text == "Disconnect")
////////            {
////////                port2.Close();
////////                comboBox3.Enabled = true;
////////                comboBox4.Enabled = true;

////////                button3.Text = "Connect";

////////            }

////////        }

////////        private void button4_Click(object sender, EventArgs e)
////////        {
////////            if (button4.Text == "Connect")
////////            {
////////                panel1.Enabled = true;
////////                panel2.Enabled = true;
////////                panel3.Enabled = true;
////////                try
////////                {

////////                    port3.PortName = comboBox5.Text;


////////                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

////////                    port3.Parity = Parity.None;
////////                    port3.DataBits = 8;
////////                    port3.StopBits = StopBits.One;
////////                    port3.Open();

////////                    comboBox5.Enabled = false;
////////                    comboBox6.Enabled = false;
                    

////////                    button4.Text = "Disconnect";
////////                }

////////                catch (Exception E)
////////                {
////////                    MessageBox.Show("ERROR:COM port could not be opened");
////////                }


////////            }

////////            else if (button4.Text == "Disconnect")
////////            {
////////                main.Close();
////////                comboBox5.Enabled = true;
////////                comboBox6.Enabled = true;


////////                button4.Text = "Connect";

////////            }

//////        }

//////        private void button1_Click(object sender, EventArgs e)
//////        {
//////            if (button1.Text == "Connect")
//////            {
               
//////                try
//////                {

//////                    main.PortName = comboBox7.Text;


//////                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

//////                    main.Parity = Parity.None;
//////                    main.DataBits = 8;
//////                    main.StopBits = StopBits.One;
//////                    main.Open();

//////                    comboBox7.Enabled = false;
//////                    comboBox8.Enabled = false;

//////                    button1.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button1.Text == "Disconnect")
//////            {
//////                main.Close();
//////                comboBox7.Enabled = true;
//////                comboBox8.Enabled = true;
//////                panel1.Enabled = false;
//////                panel2.Enabled = false;
//////                panel3.Enabled = false;

//////                button1.Text = "Connect";

//////            }
//////        }

//////        private void pictureBox1_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port1.IsOpen)
//////            {
//////                flag1 = true;
//////                flag2 = false;
//////                flag3 = false;
//////                panel1.BackColor = Color.Green;
//////                panel2.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel1.BackColor = Color.Red;
//////            }
//////        }

//////        private void pictureBox2_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port2.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = true;
//////                flag3 = false;
//////                panel3.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel2.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel3.BackColor = Color.Red;
//////            }

//////        }

//////        private void pictureBox3_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port3.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = false;
//////                flag3 = true;
//////                panel2.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel2.BackColor = Color.Red;
//////            }
//////        }
//////    }
//////} */



//////using System;
//////using System.Collections.Generic;
//////using System.ComponentModel;
//////using System.Data;
//////using System.Drawing;
//////using System.Linq;
//////using System.Text;
//////using System.Threading.Tasks;
//////using System.Windows.Forms;
//////using System.IO.Ports;

//////namespace WindowsFormsApplication5
//////{
//////    public partial class Form1 : Form
//////    {
//////        static SerialPort port1 = new SerialPort();
//////        static SerialPort port2 = new SerialPort();
//////        static SerialPort port3 = new SerialPort();
//////        static SerialPort main = new SerialPort();
//////        static bool flag1 = false;
//////        static bool flag2 = false;
//////        static bool flag3 = false;
//////        static Int32 count = 0;

//////        static byte[] buffer = new byte[14400];
//////        static byte[] buffer2 = new byte[14400];


//////        public Form1()
//////        {

//////            InitializeComponent();
//////            // panel1.Enabled = false;
//////            // panel2.Enabled = false;
//////            // panel3.Enabled = false;
//////            foreach (string p in SerialPort.GetPortNames())
//////            {
//////                comboBox1.Items.Add(p);
//////                comboBox3.Items.Add(p);
//////                comboBox5.Items.Add(p);
//////                comboBox7.Items.Add(p);

//////            }


//////            // port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);



//////            /*     System.Threading.Thread t1 = new System.Threading.Thread(delegate()
//////                 {
//////                     while (1 == 1)
//////                     {
//////                         while (flag1)
//////                         {
//////                             port1.Read(buffer, 0 , 38);

//////                             main.Write(buffer, 0 , 38);
//////                             count++;
//////                             Console.WriteLine("writing in port 1: "+count);
                        
//////                         }

//////                         while (flag2)
//////                         {
//////                             port2.Read(buffer, 0, 38);
//////                             main.Write(buffer, 0, 38);
//////                             count++;
//////                             Console.WriteLine("writing in port 2 "+ count);
//////                         }

//////                         while (flag3)
//////                         {
//////                             port3.Read(buffer, 0, 38);
//////                             main.Write(buffer, 0, 38);
//////                             Console.WriteLine("writing in port 3");
//////                         }
//////                     }

//////                 });

//////                 t1.Start();




//////                 System.Threading.Thread t2 = new System.Threading.Thread(delegate()
//////                 {
//////                     while (1 == 1)
//////                     {
//////                         while (flag1)
//////                         {
                        

//////                             port1.Write(main.ReadExisting());
//////                             count++;
//////                             Console.WriteLine("writing in PX4: " + count);

//////                         }

//////                         while (flag2)
//////                         {
//////                             port1.Write(main.ReadExisting());
//////                             count++;
//////                             Console.WriteLine("writing in apm" + count);
//////                         }

//////                         while (flag3)
//////                         {
//////                             main.Read(buffer, 0, 264);
//////                             port3.Write(buffer, 0, 264);
//////                             Console.WriteLine("writing in port 3");
//////                         }
//////                     }

//////                 });

//////                 t2.Start();

             

//////                 */



//////        }

//////        public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            if (flag1)
//////            {
//////                //main.Write(port1.ReadExisting());
//////                //int i=0;
//////                //string text;
//////                //text = port1.ReadExisting();
//////                Int32 x = port1.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port1.ReadByte());
//////                }

//////                //val = Convert.ToByte(text);
//////                // byte[] val = new byte[p];
//////                /* foreach(char obj in text)
//////                 {
//////                     val[i] = Convert.ToByte(obj);
//////                     i++;
//////                 }  */
//////                main.Write(val, 0, x);
//////                //System.Threading.Thread.Sleep(1);
//////                //port1.DiscardInBuffer();
//////                //System.Threading.Thread.Sleep(200);
//////                // port1.DiscardInBuffer();

//////            }

//////            if (flag2)
//////            {
//////                Int32 x = port2.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port2.ReadByte());
//////                }
//////                main.Write(val, 0, x);
//////                //main.Write(port2.ReadExisting());
//////                //System.Threading.Thread.Sleep(200);
//////                //port2.DiscardInBuffer();

//////            }

//////            if (flag3)
//////            {
//////                Int32 x = port3.BytesToRead;
//////                byte[] val = new byte[x];
//////                for (int i = 0; i < x; i++)
//////                {
//////                    val[i] = Convert.ToByte(port3.ReadByte());
//////                }
//////                main.Write(val, 0, x);
//////                //main.Write(port3.ReadExisting());
//////                //System.Threading.Thread.Sleep(200);
//////                //port3.DiscardInBuffer();

//////            }
//////        }

//////        public static void DataReceivedHandler_main(object sender, SerialDataReceivedEventArgs e)
//////        {
//////            Int32 x = main.BytesToRead;
//////            byte[] val = new byte[x];
//////            for (int i = 0; i < x; i++)
//////            {
//////                val[i] = Convert.ToByte(main.ReadByte());
//////            }

//////            if (flag1)
//////            {
//////                port1.Write(val, 0, x);

//////            }

//////            if (flag2)
//////            {
//////                port2.Write(val, 0, x);

//////            }

//////            if (flag3)
//////            {
//////                port3.Write(val, 0, x);

//////            }

//////        }

//////        private void button2_Click(object sender, EventArgs e)
//////        {
//////            if (button2.Text == "Connect")
//////            {
//////                try
//////                {

//////                    port1.PortName = comboBox1.Text;


//////                    port1.BaudRate = Convert.ToInt32(comboBox2.Text);

//////                    port1.Parity = Parity.None;
//////                    port1.DataBits = 8;
//////                    port1.StopBits = StopBits.One;
//////                    port1.Handshake = Handshake.XOnXOff;
//////                    port1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
//////                    port1.Open();

//////                    comboBox1.Enabled = false;
//////                    comboBox2.Enabled = false;

//////                    button2.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button2.Text == "Disconnect")
//////            {
//////                port1.Close();
//////                comboBox1.Enabled = true;
//////                comboBox2.Enabled = true;

//////                button2.Text = "Connect";

//////            }
//////        }

//////        private void button3_Click(object sender, EventArgs e)
//////        {
//////            if (button3.Text == "Connect")
//////            {
//////                try
//////                {

//////                    port2.PortName = comboBox3.Text;


//////                    port2.BaudRate = Convert.ToInt32(comboBox4.Text);

//////                    port2.Parity = Parity.None;
//////                    port2.DataBits = 8;
//////                    port2.StopBits = StopBits.One;
//////                    port2.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
//////                    port2.Open();

//////                    comboBox3.Enabled = false;
//////                    comboBox4.Enabled = false;

//////                    button3.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button3.Text == "Disconnect")
//////            {
//////                port2.Close();
//////                comboBox3.Enabled = true;
//////                comboBox4.Enabled = true;

//////                button3.Text = "Connect";

//////            }

//////        }

//////        private void button4_Click(object sender, EventArgs e)
//////        {
//////            if (button4.Text == "Connect")
//////            {
//////                panel1.Enabled = true;
//////                panel2.Enabled = true;
//////                panel3.Enabled = true;
//////                try
//////                {

//////                    port3.PortName = comboBox5.Text;


//////                    port3.BaudRate = Convert.ToInt32(comboBox6.Text);

//////                    port3.Parity = Parity.None;
//////                    port3.DataBits = 8;
//////                    port3.StopBits = StopBits.One;
//////                    port3.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

//////                    port3.Open();

//////                    comboBox5.Enabled = false;
//////                    comboBox6.Enabled = false;


//////                    button4.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button4.Text == "Disconnect")
//////            {
//////                main.Close();
//////                comboBox5.Enabled = true;
//////                comboBox6.Enabled = true;


//////                button4.Text = "Connect";

//////            }

//////        }

//////        private void button1_Click(object sender, EventArgs e)
//////        {
//////            if (button1.Text == "Connect")
//////            {

//////                try
//////                {

//////                    main.PortName = comboBox7.Text;


//////                    main.BaudRate = Convert.ToInt32(comboBox8.Text);

//////                    main.Parity = Parity.None;
//////                    main.DataBits = 8;
//////                    main.StopBits = StopBits.One;
//////                    main.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler_main);
//////                    main.Open();

//////                    comboBox7.Enabled = false;
//////                    comboBox8.Enabled = false;

//////                    button1.Text = "Disconnect";
//////                }

//////                catch (Exception E)
//////                {
//////                    MessageBox.Show("ERROR:COM port could not be opened");
//////                }


//////            }

//////            else if (button1.Text == "Disconnect")
//////            {
//////                main.Close();
//////                comboBox7.Enabled = true;
//////                comboBox8.Enabled = true;
//////                panel1.Enabled = false;
//////                panel2.Enabled = false;
//////                panel3.Enabled = false;

//////                button1.Text = "Connect";

//////            }
//////        }

//////        private void pictureBox1_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port1.IsOpen)
//////            {
//////                flag1 = true;
//////                flag2 = false;
//////                flag3 = false;
//////               // main.DiscardInBuffer();//changed
//////               // main.DiscardOutBuffer();//changed
//////                panel1.BackColor = Color.Green;
//////                panel2.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel1.BackColor = Color.Red;
//////            }
//////        }

//////        private void pictureBox2_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port2.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = true;
//////                flag3 = false;
//////               // main.DiscardInBuffer();//changed
//////                //main.DiscardOutBuffer();//changed
//////                panel3.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel2.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel3.BackColor = Color.Red;
//////            }

//////        }

//////        private void pictureBox3_Click(object sender, EventArgs e)
//////        {
//////            if (main.IsOpen && port3.IsOpen)
//////            {
//////                flag1 = false;
//////                flag2 = false;
//////                flag3 = true;
//////                //main.DiscardInBuffer();//changed
//////                //main.DiscardOutBuffer();//changed
//////                panel2.BackColor = Color.Green;
//////                panel1.BackColor = Color.Red;
//////                panel3.BackColor = Color.Red;
//////            }
//////            else
//////            {
//////                panel2.BackColor = Color.Red;
//////            }
//////        }
//////    }
//////}

