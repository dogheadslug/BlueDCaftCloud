/**********************************************
 * FamilyTong updater 
 * 1. check the version and file size of current familytong (need more instructions)
 * 2. download new version as dat file from the server (not needed)
 * 3. after download, remove the outdated exe (automaticlly achieved)
 * 4. change the downloaded file's postfix (not needed)
 * 5. open new file and end update process (achieved)
 * 
 * 
 * *******************************************/

 //things need to figure out:
 //how to check file size
 //multithreading

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

namespace BluedcraftCloudUpdate{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window{

        //private static System.Timers.Timer aTimer;
        //private static int SomeInt = 20;


        

        public MainWindow(){
            InitializeComponent();
            //check the file version, if do not match, download the file from server

            //UpdateProgress: change the length of progressbar pBar1
            //UpdateProgress();
            //the length of progress bar (0 - 100)
            pBar1.Value = 75;


            System.Net.WebClient myWebClient = new System.Net.WebClient();
            //download file:
            //https://msdn.microsoft.com/en-us/library/ez801hhe(v=vs.110).aspx
            //the FOLDER path needed for DownloadFile
            string DLURI = "https://people.ucsc.edu/~zhon4/";
            //the file name of downloaded item for DownloadFile
            string FileName = "index.html";
            //the combined FILE path needed for DownloadFile
            string myFileSrc = DLURI + FileName;

            myWebClient.OpenRead(myFileSrc);
            Int64 DL_FileSize = Convert.ToInt64(myWebClient.ResponseHeaders["Content-Length"]);
            Int64 LC_FileSize = Convert.ToInt64(new System.IO.FileInfo(FileName).Length);


            //if the file size do not match
            if (DL_FileSize != LC_FileSize)
            {
                //FileName = System.IO.Path.ChangeExtension(FileName, ".dat");
                myWebClient.DownloadFile(myFileSrc, FileName);
                pBar1.Value = 60;

                
                //after download is finished, launch the familyTong
                System.Diagnostics.Process.Start(FileName);
            }

            //if file size match then close the window directly
            else this.Close();
            

            //close window
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

        //https://stackoverflow.com/questions/12126889/how-to-use-winforms-progress-bar
        //how to use progress bar
        private void UpdateProgress(){
            //aTimer = new System.Timers.Timer(2000);
            //pBar1.Value = Someint;

        }


    }


}

//https://stackoverflow.com/questions/29904427/how-to-read-dat-file-in-c-sharp
//rename dat file to gz file, decompress the file then read it
