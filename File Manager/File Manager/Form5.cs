using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public string newName;
        public string path;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = textBox1.Text;
            newName = textBox2.Text;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;
            int bytesProcessed = 0;
            try
            {

                // Create a request for the specified remote file name

                WebRequest request = WebRequest.Create(path);
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object 
                    response = request.GetResponse();
                    if (response != null)
                    {
                        if (token.IsCancellationRequested)
                        {
                            MessageBox.Show("Операция прервана");
                            return;
                        }
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(newName);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        Task task1 = new Task(() =>
                        {
                            do
                            {
                                if (token.IsCancellationRequested)
                                {
                                    MessageBox.Show("Операция прервана");
                                    return;
                                }
                                // Read data (up to 1k) from the stream
                                bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                                // Write the data to the local file
                                localStream.Write(buffer, 0, bytesRead);

                                // Increment total bytes processed
                                bytesProcessed += bytesRead;
                            } while (bytesRead > 0);
                        }); task1.Start();
                        if (decline == true)
                            cancelTokenSource.Cancel();
                    }
                }
                
            }
            catch (Exception ew)
            {
                MessageBox.Show(ew.Message);

            }
            finally
            {
                // Close the response and streams objects here 
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
                Close();
            }
            
        }
        public bool decline = false;
        private void button2_Click(object sender, EventArgs e)
        {
            decline = true;
        }
    }
}
