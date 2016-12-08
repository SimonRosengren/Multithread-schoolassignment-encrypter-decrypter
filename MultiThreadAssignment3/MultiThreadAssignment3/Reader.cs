using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadAssignment3
{
    class Reader
    {
        Random rnd;
        Buffer buffer;
        TextBox textbox;

        int nrOfStrings;

        public Reader(Buffer buffer, Random rnd, TextBox tb, int nrOfStrings)
        {
            this.textbox = tb;
            this.buffer = buffer;
            this.rnd = rnd;
            this.nrOfStrings = nrOfStrings;
        }
        public void Start()
        {
            ReadFromBuffer();
        }
        /// <summary>
        /// Checks so that writer is not finised and makes sure
        /// that the encrypted queue is empty before it finishes
        /// </summary>
        private void ReadFromBuffer()
        {
            for (int i = 0; i < nrOfStrings; i++)
            {
                string s = buffer.DequeueEncrypted();
                buffer.decryptedStrings.Add(s);
                textbox.BeginInvoke((Action)delegate() { textbox.Text += "" + s + "\n"; });

                 Thread.Sleep(rnd.Next(200, 1000)); //Random sleep time between access to buffer 
            }
            
        }

    }
}
