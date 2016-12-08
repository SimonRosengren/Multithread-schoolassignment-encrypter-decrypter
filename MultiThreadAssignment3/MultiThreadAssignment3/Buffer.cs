using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadAssignment3
{
    class Buffer
    {
        int queueSize = 4;  //The size of the queue

        public List<string> decryptedStrings;

        Queue<string> encrypted;
        Queue<string> decrypted;

        public Semaphore encryptionReaderSemaphore { get; set; }
        public Semaphore writerSemaphore { get; set; }
        public Semaphore readerSemaphore { get; set; }
        public Semaphore encryptionWriterSemaphore { get; set; }


        Mutex myMutex;

        public Buffer()
        {
            encrypted = new Queue<string>();
            decrypted = new Queue<string>();

            decryptedStrings = new List<string>();

            encryptionReaderSemaphore = new Semaphore(0, (int) queueSize);   
            writerSemaphore = new Semaphore((int)queueSize, (int)queueSize);
            readerSemaphore = new Semaphore(0, (int)queueSize);
            encryptionWriterSemaphore = new Semaphore((int)queueSize, (int)queueSize);

            myMutex = new Mutex();
        }
        /// <summary>
        /// Works similiar to the enqueueDecrypted 
        /// but is working with the second queue, the one which is
        /// for encrypted strings
        /// </summary>
        /// <param name="s"></param>
        public void EnqueueEncrypted(string s)
        {

            encryptionWriterSemaphore.WaitOne();
            myMutex.WaitOne(200);

            encrypted.Enqueue(s);   

            myMutex.ReleaseMutex();
            encryptionReaderSemaphore.Release();
            Thread.Sleep(200);

        }
        /// <summary>
        /// The final deqeuue for the encrypted strings
        /// </summary>
        /// <returns></returns>
        public string DequeueEncrypted()
        {
            encryptionReaderSemaphore.WaitOne();
            myMutex.WaitOne(200);
            string r = encrypted.Dequeue();

            myMutex.ReleaseMutex();
            encryptionWriterSemaphore.Release();

            return r;
        }
        /// <summary>
        /// Adds strings to the not yet altered queue of strings.
        /// Waits for places to be free in the queue by the semaphore,
        /// and releases semaphores/spots for the encrypter so that it knows
        /// there are string to encrypt.
        /// </summary>
        public void EnqueueDecrypted(string s)
        {
            writerSemaphore.WaitOne(); 
            myMutex.WaitOne(200);

            decrypted.Enqueue(s);   

            myMutex.ReleaseMutex();
            readerSemaphore.Release(); 
        }
        /// <summary>
        ///Waits for the semaphore to show that there are string to dequeue from the queue
        ///Releases for writer that one of the enqueued strings now have been dequeued.
        /// </summary>
        /// <returns>Strängen vi har dequeueat</returns>
        public string DequeueDecrypted()
        {
            readerSemaphore.WaitOne(); 
            myMutex.WaitOne(200);

            string r = decrypted.Dequeue();

            myMutex.ReleaseMutex();
            writerSemaphore.Release();
            return r;
        }
    }
}

