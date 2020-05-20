using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace File_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {InitializeComponent();
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(Customised));
                FileStream f = new FileStream("text.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Customised customised = xml.Deserialize(f) as Customised;
                if (customised.Theme == "white")
                {
                    BackColor = SystemColors.Window;
                    ForeColor = SystemColors.WindowText;
                    menuStrip1.BackColor = SystemColors.Window;
                    listView1.BackColor = SystemColors.Window;
                    listView1.ForeColor = SystemColors.WindowText;
                    textBox1.BackColor = SystemColors.Window;
                    textBox1.ForeColor = SystemColors.WindowText;
                    menuStrip1.ForeColor = SystemColors.WindowText;
                }
                if (customised.Theme == "grey")
                {
                    BackColor = SystemColors.WindowFrame;
                    ForeColor = SystemColors.Control;
                    menuStrip1.BackColor = SystemColors.ControlDark;
                    listView1.BackColor = SystemColors.ControlDark;
                    listView1.ForeColor = SystemColors.Control;
                    textBox1.BackColor = SystemColors.ControlDark;
                    textBox1.ForeColor = SystemColors.Control;
                    menuStrip1.ForeColor = SystemColors.Control;
                }
                if (customised.Theme == "black")
                {

                    BackColor = SystemColors.Desktop;
                    ForeColor = SystemColors.Control;
                    menuStrip1.BackColor = SystemColors.WindowFrame;
                    listView1.BackColor = SystemColors.WindowFrame;
                    listView1.ForeColor = SystemColors.Control;
                    textBox1.BackColor = SystemColors.WindowFrame;
                    textBox1.ForeColor = SystemColors.Control;
                    menuStrip1.ForeColor = SystemColors.Control;
                }
                listView1.BackColor = customised.ListViewColor;
                listView1.ForeColor = customised.TextColor;
                listView1.Font = customised.Font;
                if (customised.view == "Tile")
                    listView1.View = View.Tile;
                if (customised.view == "List")
                    listView1.View = View.List;
                if (customised.view == "Small")
                    listView1.View = View.SmallIcon;
                if (customised.view == "Large")
                    listView1.View = View.LargeIcon;
                f.Close();
            }
            catch 
            {
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
          // listBox1.DrawMode = DrawMode.Normal;
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach(DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            } 
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Path.GetExtension(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text)) == "")
                {

                    textBox1.Text = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
                    listView1.Items.Clear();
                    DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
                    DirectoryInfo[] dirs = dir.GetDirectories();
                    foreach (DirectoryInfo crrDirectory in dirs)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = crrDirectory.Name;
                        lvi.ImageIndex = 1;
                        listView1.Items.Add(lvi);
                    }
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo crrFile in files)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = crrFile.Name;
                        lvi.ImageIndex = 2;
                        listView1.Items.Add(lvi);
                    }
                    fileSystemWatcher1.Path = textBox1.Text;

                }
                else
                {
                    Process.Start(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text));
                }
            }
            catch { }
            
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text[textBox1.Text.Length - 2] != ':')
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == '\\')
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                    {
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                }
                else if (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                {
                    while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                    {
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                }

                listView1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo crrDirectory in dirs)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrDirectory.Name;
                    lvi.ImageIndex = 1;
                    listView1.Items.Add(lvi);
                }
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo crrFile in files)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrFile.Name;
                    lvi.ImageIndex = 2;
                    listView1.Items.Add(lvi);
                }
            }
            else
            {
                textBox1.Text = "";
                listView1.Items.Clear();
                DriveInfo[] drives = DriveInfo.GetDrives();

                foreach (DriveInfo crrDrive in drives)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrDrive.Name;
                    lvi.ImageIndex = 0;
                    listView1.Items.Add(lvi);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listView1.Items.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo crrDrive in drives)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDrive.Name;
                lvi.ImageIndex = 0;
                listView1.Items.Add(lvi);
            }
        }
        bool b4clicked = false;
        string b4path = "";
        bool b5clicked;
        string b5path = "";
     
        public static void Compress(string sourceFile, string compressedFile)
            {
                using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(compressedFile))
                    {
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream); 
                        }
                    }
                }
            }
       
        public static void Decompress(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            const string FORMAT = "The {0} has changed in {1}";
            string text = string.Format(FORMAT, e.ChangeType, e.Name);
            MessageBox.Show(text);
        }
      
        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            string text = e.OldName+ " has been renamed "+ e.Name;
            MessageBox.Show(text);
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            string text = e.Name + " has been deleted ";
            MessageBox.Show(text);
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            string text = e.Name + " has been created ";
            MessageBox.Show(text);
        }

        private void переместитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b4clicked == false)
            {
                b4path = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);

                b4clicked = true;
            }
            else
            {
                try
                {
                    FileInfo fileInf = new FileInfo(b4path);
                    if (fileInf.Exists)
                    {
                        fileInf.MoveTo(Path.Combine(textBox1.Text, fileInf.Name));
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(b4path);
                    if (dirInfo.Exists && Directory.Exists(Path.Combine(textBox1.Text, dirInfo.Name)) == false)
                    {
                        dirInfo.MoveTo(Path.Combine(textBox1.Text, dirInfo.Name));
                    }
                }
                catch { }
                b4clicked = false;
            }
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b5path = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
            b5clicked = true;
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b5clicked == true)
            {
                try
                {
                    FileInfo fileInf = new FileInfo(b5path);

                    if (fileInf.Exists)
                    {
                        fileInf.CopyTo(Path.Combine(textBox1.Text, fileInf.Name), true);
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo from = new DirectoryInfo(b5path);
                    DirectoryInfo to = new DirectoryInfo(Path.Combine(textBox1.Text, from.Name));
                    CopyAll(from, to);

                    void CopyAll(DirectoryInfo source, DirectoryInfo target)
                    {
                        if (source.FullName.ToLower() == target.FullName.ToLower())
                        {
                            return;
                        }

                        // Check if the target directory exists, if not, create it.
                        if (Directory.Exists(target.FullName) == false)
                        {
                            Directory.CreateDirectory(target.FullName);
                        }

                        // Copy each file into it's new directory.
                        foreach (FileInfo fi in source.GetFiles())
                        {
                            Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                            fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                        }

                        // Copy each subdirectory using recursion.
                        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                        {
                            DirectoryInfo nextTargetSubDir =
                                target.CreateSubdirectory(diSourceSubDir.Name);
                            CopyAll(diSourceSubDir, nextTargetSubDir);
                        }
                    }
                }
                catch { }
                b5clicked = false;
                listView1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo crrDirectory in dirs)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrDirectory.Name;
                    lvi.ImageIndex = 1;
                    listView1.Items.Add(lvi);
                }
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo crrFile in files)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrFile.Name;
                    lvi.ImageIndex = 2;
                    listView1.Items.Add(lvi);
                }
            }

        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInf = new FileInfo(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text));
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text));
            Form2 form2 = new Form2();
            form2.ShowDialog();
            string path = Path.Combine(textBox1.Text, form2.newName);

            if (fileInf.Exists)
            {
                fileInf.MoveTo(path);
            }
            if (dirInfo.Exists && Directory.Exists(path) == false)
            {
                dirInfo.MoveTo(path);
            }
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInf = new FileInfo(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text));
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }

            try
            {
                Directory.Delete(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text), true);
            }
            catch { }
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            }
        }
    

        private void архивироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Path.GetExtension(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text)) == "")
            {

                string startPath = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
                string zipPath = startPath + ".zip";
                ZipFile.CreateFromDirectory(startPath, zipPath);

            }
            else
            {
                try
                {
                    string sourceFile = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
                    string compressedFile = sourceFile.Remove(sourceFile.LastIndexOf('.'), sourceFile.Length - sourceFile.LastIndexOf('.')) + ".gz";
                    Compress(sourceFile, compressedFile);
                }
                catch { }
            }
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            }
        }

        private void разархивироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text[textBox1.Text.Length - 1] == '\\')
            {
                try
                {
                    string zipPath = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
                    string endPath = zipPath.Remove(zipPath.Length - 4, 4) + '\\';
                    ZipFile.ExtractToDirectory(zipPath, endPath);
                }
                catch { }
            }
            else
            {
                try
                {
                    string sourceFile = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
                    Form2 form2 = new Form2();
                    form2.ShowDialog();
                    string path = Path.Combine(textBox1.Text, form2.newName);
                    Decompress(sourceFile, path);
                }
                catch { }
            }
            listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo crrDirectory in dirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrDirectory.Name;
                lvi.ImageIndex = 1;
                listView1.Items.Add(lvi);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo crrFile in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = crrFile.Name;
                lvi.ImageIndex = 2;
                listView1.Items.Add(lvi);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            listView1.BackColor = colorDialog1.Color;
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            listView1.Font = fontDialog1.Font;

            listView1.ForeColor = fontDialog1.Color;
        }

        private void белаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.WindowText;
            menuStrip1.BackColor = SystemColors.Window;
            listView1.BackColor = SystemColors.Window;
            listView1.ForeColor = SystemColors.WindowText;
            textBox1.BackColor = SystemColors.Window;
            textBox1.ForeColor = SystemColors.WindowText;
            menuStrip1.ForeColor = SystemColors.WindowText;
        }

        private void сераяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = SystemColors.WindowFrame;
            ForeColor = SystemColors.Control;
            menuStrip1.BackColor = SystemColors.ControlDark;
            listView1.BackColor = SystemColors.ControlDark;
            listView1.ForeColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.ControlDark;
            textBox1.ForeColor = SystemColors.Control;
            menuStrip1.ForeColor = SystemColors.Control;
        }

        private void чёрнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = SystemColors.Desktop;
            ForeColor = SystemColors.Control;
            menuStrip1.BackColor = SystemColors.WindowFrame;
            listView1.BackColor = SystemColors.WindowFrame;
            listView1.ForeColor = SystemColors.Control;
            textBox1.BackColor = SystemColors.WindowFrame;
            textBox1.ForeColor = SystemColors.Control;
            menuStrip1.ForeColor = SystemColors.Control;
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(40, 40);
           
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }
        
        private void использоватьНастройкиВСледующиеРазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customised customised = new Customised();
            if (BackColor == SystemColors.Control)
                customised.Theme = "white";
            if (BackColor == SystemColors.WindowFrame)
                customised.Theme = "grey";
            if (BackColor == SystemColors.Desktop)
                customised.Theme = "black";            
            customised.ListViewColor = listView1.BackColor;
            customised.TextColor = listView1.ForeColor;
            if(listView1.View == View.Tile)
            customised.view = "Tile";
            if (listView1.View == View.List)
                customised.view = "List";
            if (listView1.View == View.SmallIcon)
                customised.view = "Small";
            if (listView1.View == View.LargeIcon)
                customised.view = "Large";
            customised.Font = listView1.Font;
            XmlSerializer xml = new XmlSerializer(customised.GetType());
            FileStream f = new FileStream("text.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            xml.Serialize(f, customised);
            f.Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream f = new FileStream("AboutProgram.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            Process.Start("AboutProgram.txt");
        }

        private void темаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            FileInfo[] files = dir.GetFiles();
            Regex regex = new Regex(textBox2.Text);
            Parallel.ForEach(files, (crrFile) =>
            {
                if (regex.IsMatch(crrFile.Name))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = crrFile.Name;
                    lvi.ImageIndex = 2;
                    listView2.Items.Add(lvi);
                }
            }
               );
            Console.ReadLine();
        }

        private void скачатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Form5 form5 = new Form5();
            form5.ShowDialog();
            
        }
    }
}