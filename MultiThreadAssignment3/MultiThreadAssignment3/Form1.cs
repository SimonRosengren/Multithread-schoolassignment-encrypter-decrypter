using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace MultiThreadAssignment3
{
    public partial class Form1 : Form
    {
        Random rnd; //For test purposes

        string fileToOpen = "text.txt";

        Reader reader;
        Writer writer;
        Encrypter encrypter;
        Buffer buffer;

        Thread readerThread;
        Thread writerThread;
        Thread encrypterThread;

        List<string> stringsFromFile;

        public Form1()
        {
            InitializeComponent();
            rnd = new Random();

            encryptButton.Enabled = false;
            decryptButton.Enabled = false;


            stringsFromFile = new List<string>();       
        }
        /// <summary>
        /// Starts the encryption process by
        /// creating and starting each thread. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encryptButton_Click(object sender, EventArgs e)
        {
            readerThread = new Thread(reader.Start);
            encrypterThread = new Thread(encrypter.Start);
            writerThread = new Thread(writer.Start);


            readerThread.Start();
            encrypterThread.Start();
            writerThread.Start();
            decryptButton.Enabled = true;
        }
        /// <summary>
        /// Opens a file dialog box and
        /// that lets you choose a txt-file
        /// one chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadTextButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open text file";
            ofd.Filter = "TXT files|*.txt";
            ofd.InitialDirectory = @"C:\";
            ofd.ShowDialog();
            fileToOpen = ofd.FileName.ToString();
            StreamReader file = new StreamReader(fileToOpen);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                stringsFromFile.Add(line);
            }
            file.Close();

            buffer = new Buffer();
            encrypter = new Encrypter(buffer, rnd, stringsFromFile.Count);
            reader = new Reader(buffer, rnd, encryptedTextBox, stringsFromFile.Count);
            writer = new Writer(buffer, rnd, plainTextBox, encryptedTextBox, stringsFromFile);

            encryptButton.Enabled = true;
            loadTextButton.Enabled = false;
        }
        /// <summary>
        /// Gets the text from where we put it after we had encrypted it
        /// instead of from a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void decryptButton_Click(object sender, EventArgs e)
        {
            stringsFromFile.Clear();
            for (int i = 0; i < buffer.decryptedStrings.Count; i++)
            {
                stringsFromFile.Add(buffer.decryptedStrings[i]);
            }

            readerThread = new Thread(reader.Start);
            encrypterThread = new Thread(encrypter.Start);
            writerThread = new Thread(writer.Start);


            readerThread.Start();
            encrypterThread.Start();
            writerThread.Start();
        }
    }
}
