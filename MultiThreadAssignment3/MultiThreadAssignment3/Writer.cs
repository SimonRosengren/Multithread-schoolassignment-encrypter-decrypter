using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MultiThreadAssignment3
{
    class Writer
    {
        Buffer buffer;
        Random rnd;

        TextBox PlainTextBox;
        TextBox encrytedTextBox;

        List<string> stringsFromFile;

        public Writer(Buffer buffer, Random rnd, TextBox tb, TextBox encryptedTextBox, List<string> stringsFromFile)
        {
            this.buffer = buffer;
            this.rnd = rnd;
            this.PlainTextBox = tb;

            this.stringsFromFile = stringsFromFile;

            this.encrytedTextBox = encryptedTextBox;
        }
        public void Start()
        {
            ReadFromStringsFromFile();
        }
        private void ReadFromStringsFromFile()
        {
            for (int i = 0; i < stringsFromFile.Count; i++)
            {
                string line = stringsFromFile[i];
                PlainTextBox.BeginInvoke((Action)delegate() { PlainTextBox.Text += "" + line + "\n"; });
                buffer.EnqueueDecrypted(line);

                 Thread.Sleep(rnd.Next(200, 1000)); //Random sleep time between access to buffer 
            }
            
        }
        private void ReadFromBuffer()
        {
            
        }
    }
}
