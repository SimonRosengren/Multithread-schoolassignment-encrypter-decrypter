using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadAssignment3
{
    class Encrypter
    {
        Buffer buffer;
        Random rnd; //For test purposes

        int nrOfStrings;

        public Encrypter(Buffer buffer, Random rnd, int nrOfStrings)
        {
            this.buffer = buffer;
            this.rnd = rnd;
            this.nrOfStrings = nrOfStrings;
        }
        public void Start()
        {
            StartEncrypting();
        }
        private void StartEncrypting()
        {
            for (int i = 0; i < nrOfStrings; i++)
            {
                string lineToEncrypt;
                lineToEncrypt = buffer.DequeueDecrypted();

                char[] flippedString = lineToEncrypt.ToCharArray();
                Array.Reverse(flippedString);

                buffer.EnqueueEncrypted(new string(flippedString));

                Thread.Sleep(rnd.Next(200, 1000)); //Random sleep time between access to buffer 
            }
        }
    }
}
