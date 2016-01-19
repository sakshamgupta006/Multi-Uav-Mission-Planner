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
    public partial class Form1 : Form
    {
        static SerialPort port1= new SerialPort();
        static SerialPort port2=new SerialPort();
        static SerialPort port3=new SerialPort();
        static SerialPort main=new SerialPort();
        bool flag1=false;
        bool flag2=false;
        bool flag3=false;
        static Int32 count = 0;

        static byte[] buffer = new byte[1024];


        public Form1()
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

            System.Threading.Thread t1 = new System.Threading.Thread(delegate()
            {
                while (1 == 1)
                {
                    while (flag1)
                    {
                       // port1.Read(buffer, 0 , 38);
                         port1.Read(buffer, 0, 1000);
                         System.Threading.Thread.Sleep(100);
                         //string blah = Convert.ToString(buffer);
                         port1.DiscardInBuffer();


                        main.Write(buffer,0,1000);
                        System.Threading.Thread.Sleep(400);
                        count++;
                        Console.WriteLine("writing in port 1: "+count);
                        
                    }

                    while (flag2)
                    {
                       // port2.Read(buffer, 0, 38);
                        port2.Read(buffer, 0, 1000);
                        System.Threading.Thread.Sleep(100);
                        string blah = Convert.ToString(buffer);
                        port2.DiscardInBuffer();
                        main.Write(buffer, 0, 1000);
                        System.Threading.Thread.Sleep(100);

                       // main.Write(blah);
                        //main.Write(port2.ReadExisting());
                        count++;
                        Console.WriteLine("writing in port 2 "+ count);
                    }

                    while (flag3)
                    {
                        //port3.Read(buffer, 0, 38);
                        port3.Read(buffer, 0, 512);
                        System.Threading.Thread.Sleep(100);
                        string blah = Convert.ToString(buffer);
                        port3.DiscardInBuffer();
                        main.Write(buffer, 0, 512);
                        System.Threading.Thread.Sleep(100);


                        //main.Write(blah);
                       // main.Write(port3.ReadExisting());
                        count++;
                        Console.WriteLine("writing in port 3");
                    }
                }

            });

            t1.Start();

           

            
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
                    port1.Handshake = Handshake.XOnXOff;
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
                    main.Open();

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
