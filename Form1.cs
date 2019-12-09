using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSAKeyGen
{
    public partial class KeyGen : Form
    {
        public KeyGen()
        {
            InitializeComponent();
        }

        private void KeyGen_Load(object sender, EventArgs e)
        {
            make();
        }

        private void buttonRand_Click(object sender, EventArgs e)
        {
            make(); 
        }

        private string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        private void make()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            String dir = GetTimeStamp();//使用时间戳做新的文件夹
            buttonRand.Text = "Rand " + dir;
            if (!Directory.Exists(dir))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(dir); //新建文件夹
            }
            using (StreamWriter streamWriter = new StreamWriter("./"+dir+"/PublicKey.xml"))
            {
                streamWriter.Write(rsa.ToXmlString(false));// 将公匙保存到运行目录下的PublicKey
            }
            using (StreamWriter streamWriter = new StreamWriter("./" + dir + "/PrivateKey.xml"))
            {
                streamWriter.Write(rsa.ToXmlString(true)); // 将公匙&私匙保存到运行目录下的PrivateKey
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/potatoDIY/keygen");
        }
    }
}
