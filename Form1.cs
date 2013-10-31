using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FelicaLib;

namespace FelicaTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (Felica f = new Felica())
                {
                    f.Polling((int)SystemCode.Common);
                    
                    //学籍番号の読み出し
                    byte[] data = f.ReadWithoutEncryption(0x1A8B, 0);
                    byte[] studentId = new byte[7];
                    Array.Copy(data, 2, studentId, 0, 7);

                    //名前読み出し
                    byte[] name = f.ReadWithoutEncryption(0x1A8B, 1);


                    studentIDLabel.Text = byte2str(studentId);
                    nameLabel.Text = byte2str(name);
                }
            }
            catch (Exception ex)
            {
                textBox.Text = ex.Message;
            }
        }

        private string byte2str(byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
