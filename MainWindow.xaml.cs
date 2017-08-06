/**********************************************
 * FamilyTong updater 
 * 1. check the version and file size of current familytong (need more instructions)
 * 2. download new version as dat file from the server (not needed)
 * 3. after download, remove the outdated exe (automaticlly achieved)
 * 4. change the downloaded file's postfix (not needed)
 * 5. open new file and terminate update process (achieved)
 * 
 * This piece of code is based on Chad's template (which includes basic user interface)
 * *******************************************/

//next step: start the downloaded file after update complete

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using System.ComponentModel;

namespace BluedcraftCloudUpdate{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window{
        
        //private static System.Timers.Timer aTimer;
        //private static int SomeInt = 20;

        public MainWindow(){
            InitializeComponent();

            //download file:
            //https://msdn.microsoft.com/en-us/library/ez801hhe(v=vs.110).aspx
            //the FOLDER path needed for DownloadFile
            //www.bluedcraft.com/download/bluedcraftcloud/update.dat 
            string DLURI = "https://www.bluedcraft.com/download/bluedcraftcloud/";
            //string DLURI = "https://people.ucsc.edu/~zhon4/";
            //the file name of downloaded item for DownloadFile
            string FileName = "update.dat";
            //string FileName = "index.html";
            //the combined FILE path needed for DownloadFile
            string myFileSrc = DLURI + FileName;

            //string Cent = Convert.ToString(pBar1.Value);

            startDownload(myFileSrc, FileName);

            /**************************
             * this part checks if the file is fully downloaded
             * ??? not sure if it's really needed
             * 
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            myWebClient.OpenRead(myFileSrc);
            //size of download file
            Int64 DL_FileSize = Convert.ToInt64(myWebClient.ResponseHeaders["Content-Length"]);
            //size of local file
            Int64 LC_FileSize = Convert.ToInt64(new System.IO.FileInfo(FileName).Length);
            //while the file size do not match
            //use the while loop so it checks if the file has been successfully downloaded
            while (DL_FileSize != LC_FileSize) {
                startDownload(myFileSrc, FileName);
                //check if the file has been downloaded completely
                LC_FileSize = Convert.ToInt64(new System.IO.FileInfo(FileName).Length);
            }
            ******************************/

            //after download is finished, launch the familyTong
            //System.Diagnostics.Process.Start(FileName);
            //close window(not here!)
            //this.Close();
        }


        //basic interface functions. title bar at the top
        //drag the window around
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            try{
                this.DragMove();
            }
            catch { }
        }

        //close the window
        private void Window_close(object sender, RoutedEventArgs e){
            this.Close();
        }

        //minimize the window
        private void Window_minimize(object sender, RoutedEventArgs e){
            this.WindowState = WindowState.Minimized;
            //i have no idea how i figured it out
        }

        //uwp progress bar
        //https://social.msdn.microsoft.com/Forums/windows/en-US/d07047a7-bd9c-4f9e-b4a0-41f63164b769/c-httpclient-download-file-progress?forum=winforms
        //System.Net.WebClient m_WebClient;
        private void startDownload(string URL, string fileName){
            WebClient m_WebClient = new WebClient();
            m_WebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            m_WebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
            Uri uri = new Uri(URL);
            m_WebClient.DownloadFileAsync(uri, fileName);
        }

        private void DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e){
            //Update the progress bar value with the percentage 0-100
            pBar1.Value = (int)e.ProgressPercentage;

            //download percentage display
            tB1.Text = Convert.ToString(pBar1.Value) + "%";

            //ProgressPercentage is avaliable so do not need to calculate percentage
        }

        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e){
            // System.Diagnostics.Process.Start(FileName);

            //close the window after download is complete, instantly 
            //maybe we should just let user to do it mannualy?
            //this.Close();

            //another window comes out : update complete, launch the new version now?
            tB1.Text = "Finished!";
        }

        //https://stackoverflow.com/questions/12126889/how-to-use-winforms-progress-bar
        //how to use progress bar

    }
}
