using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;
using IWshRuntimeLibrary;
using System.Threading;

namespace AtlasInstall.Logic
{
    public class InstallProcess
    {

        private int count;
        private int operationsCount;
        private bool _desktop;
        private bool _mainmenu;
        private bool _alluser;

        public InstallProcess( )
        {
           
        }

        public InstallProcess(bool desktop, bool mainmenu, bool alluser)
        {
            _desktop = desktop;
            _mainmenu = mainmenu;
            _alluser = alluser;
        }

        public void Uninstall(MainWindow mainWindow)
        {
            count = 0;
            string approotDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise";
            operationsCount = GetAllFilesCount(approotDir);
            operationsCount++;
            operationsCount++;
            operationsCount++;

           

            //Delete All Files
            if(Directory.Exists(approotDir))
            foreach (string newPath in Directory.GetFiles(approotDir, "*.*",
                   SearchOption.AllDirectories))
            {
                Thread.Sleep(250);
                System.IO.File.Delete(newPath);
                count++;
                mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "File Deleted: " + newPath);
               // mainWindow.Details += "File Deleted: " + newPath;
            }

            //Delete Directories
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company"))
                if (Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company").Count() == 1)
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company", true);
            else
                Directory.Delete(approotDir, true);

            //mainWindow.Details += "Directories deleted";
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Directories deleted");

            //Delete Desktop Link
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //Delete Common Desktop Link
            string commondeskDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

            if (System.IO.File.Exists(deskDir + "\\" + "Atlas" + ".url"))
            System.IO.File.Delete(deskDir + "\\" + "Atlas" + ".url");

            if (System.IO.File.Exists(commondeskDir + "\\" + "Atlas" + ".url"))
                System.IO.File.Delete(commondeskDir + "\\" + "Atlas" + ".url");
            //            mainWindow.Details += "Desktop Shorcut Removed. ";
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Desktop Shorcut Removed. ");

            //Delete Start Menu Shortcut
            string programs_path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string programs_path_all = Path.Combine(Environment.GetEnvironmentVariable("ALLUSERSPROFILE"), @"Microsoft\Windows\Start Menu\Programs");

            if (Directory.Exists(programs_path + "\\WildWest Company"))
            {
                if (Directory.GetDirectories(programs_path + "\\WildWest Company").Count() == 1)
                    Directory.Delete(programs_path + "\\WildWest Company", true);
                else
                    Directory.Delete(programs_path + "\\WildWest Company\\Atlas Enterprise", true);

                
            }

            if (Directory.Exists(programs_path_all + "\\WildWest Company"))
            {
                if (Directory.GetDirectories(programs_path_all + "\\WildWest Company").Count() == 1)
                    Directory.Delete(programs_path_all + "\\WildWest Company", true);
                else
                    Directory.Delete(programs_path_all + "\\WildWest Company\\Atlas Enterprise", true);
            }

            if(_desktop)
            {
                string LogsPath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WildWest Company"), "Atlas Enterprise"), "Logs");
                if (Directory.Exists(LogsPath))
                    Directory.Delete(LogsPath, true);
            }
            if (_mainmenu)
            {
                string AppDataPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WildWest Company"), "Atlas Enterprise");
                if (Directory.Exists(AppDataPath))
                    Directory.Delete(AppDataPath, true);
            }


            DeleteUninstaller();
           // mainWindow.Details += "Application Unregistred from System.";
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Application Unregistred from System.");

            Thread.Sleep(1250);
        }
        public void Install(MainWindow mainWindow)
        {
            count = 0;
            //Environment.SpecialFolder.ProgramFiles

            //  if(!Directory.Exists(Directory.spe))
            //  {
            string approotDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise";
            Directory.CreateDirectory(approotDir);

            operationsCount = GetAllFilesCount(Environment.CurrentDirectory + "\\InstallFiles");
            operationsCount++;
            operationsCount++;
            operationsCount++;

            string mediaRootDir = Environment.CurrentDirectory + "\\InstallFiles\\Media";

            //Coping Media
            CopyFilesRecursively(mediaRootDir, approotDir + "\\Media", mainWindow);

            string scriptRootDir = Environment.CurrentDirectory + "\\InstallFiles\\Source\\Scripts";
            //Coping Scripts
            CopyFilesRecursively(scriptRootDir, approotDir, mainWindow);

            string packageRootDir = Environment.CurrentDirectory + "\\InstallFiles\\Source\\Packages";
            //Coping PAckages
            CopyFilesRecursively(packageRootDir, approotDir, mainWindow);
            string miscRootDir = Environment.CurrentDirectory + "\\InstallFiles\\Misc";
            //Coping Misc
            CopyFilesRecursively(miscRootDir, approotDir,mainWindow);

            //Desktop shortcut
            if(_desktop)
            {
                appShortcutToDesktop("Atlas");
               // mainWindow.Details += "Desktop Shorcut Created. ";
            }          
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Desktop Shorcut Created. ");

            //Main Menu Shortcuts
            if(_mainmenu)
            if (!_alluser)
                CreateStartMenuShortcut();
            else
                CreateShortcutForAllUsers();


            SetDefaultProgram();
          //  mainWindow.Details += "Set Default program for .f4v. ";
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Set Default program for .f4v. ");

            CreateUninstaller();
            //mainWindow.Details += "Application Registred on System";           
            count++;
            mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount), "Application Registred on System");
            //mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount));

            Thread.Sleep(1250);

            //  }
        }

        private void SetDefaultProgram()
        {
            //First you find out file type of some extension, say.jpg:
            var imgKey = Registry.ClassesRoot.OpenSubKey(".f4v");
            var imgType = imgKey.GetValue("");
            //Then you find out the path to your executable and build the "command string":

            String myExecutable = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Windows Media Player\\wmplayer.exe";
            String command = "\"" + myExecutable + "\"" + " \"%1\"";
           // And register your executable to open files of that type:

            String keyName = imgType + @"\shell\Open\command";
            using (var key = Registry.ClassesRoot.CreateSubKey(keyName))
            {
                key.SetValue("", command);
            }
        }

        private  void CopyFilesRecursively(string SourcePath, string DestinationPath, MainWindow mainWindow)
        {
            DirectoryInfo source = new DirectoryInfo(SourcePath);
            DirectoryInfo target = new DirectoryInfo(DestinationPath);
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                // mainWindow.Details = "Directory Created: " + target.FullName+"\\"+dir.Name;
                string message = "Directory Created: " + target.FullName + "\\" + dir.Name;
                mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount),message);
                CopyFilesRecursively(dir.FullName, target.CreateSubdirectory(dir.Name).FullName, mainWindow);
            }
               
            foreach (FileInfo file in source.GetFiles())
            {
                Thread.Sleep(250);
                file.CopyTo(Path.Combine(target.FullName, file.Name));
                count++;
                string message = "File Created: " + file.FullName;
                mainWindow.backgroundWorker.ReportProgress((int)((count * 100) / operationsCount),message);
               // mainWindow.Details += "File Created: " + file.FullName; 

                
            }
               
        }

        public int  GetAllFilesCount(string SourcePath)
        {
            return Directory.Exists(SourcePath)? Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories).Count(): 0;
        }
        private void CopyFiles(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
                System.IO.File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }
        private void CreateUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = "A7666A2F-E9F2-4CA0-8721-43E5E2B4D39C";// UninstallGuid.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);


                        if (key == null)
                        {
                            throw new Exception(String.Format("Unable to create uninstaller '{0}\\{1}'", @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", guidText));
                        }

                        Assembly asm = GetType().Assembly;
                        Version v = asm.GetName().Version;
                        string exe = "\"" + asm.CodeBase.Substring(8).Replace("/", "\\\\") + "\"";
                        string app = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\Atlas.exe";
                        app = "\"" + app.Replace("/", "\\\\") + "\"";



                        key.SetValue("DisplayName", "Atlas");
                        key.SetValue("ApplicationVersion", v.ToString());
                        key.SetValue("Publisher", "WildWest Company");
                        key.SetValue("DisplayIcon", exe);
                        key.SetValue("DisplayVersion", v.ToString(2));
                        key.SetValue("URLInfoAbout", "http://www.wildwest.com");
                        key.SetValue("Contact", "support@wildwest.com");
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", exe.Replace("Atlas.exe", "uninstall.exe") + " /uninstallprompt");
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }
            }
        }
        public bool ExistUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    return false;
                }
                try
                {
                 

                    try
                    {
                        string guidText = "A7666A2F-E9F2-4CA0-8721-43E5E2B4D39C";// UninstallGuid.ToString("B");
                        return parent.OpenSubKey(guidText, true) != null;


                       
                    }
                    finally
                    {
                       
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }

            }
        }

        private void DeleteUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(
                         @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = "A7666A2F-E9F2-4CA0-8721-43E5E2B4D39C";// UninstallGuid.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);


                        if (key == null)
                        {
                            throw new Exception(String.Format("Unable to create uninstaller '{0}\\{1}'", @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", guidText));
                        }

                        parent.DeleteSubKey(guidText);
                    }
                    finally
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                        ex);
                }
            }
        }

        private void appShortcutToDesktop(string linkName)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if(_alluser)
                deskDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
               
                string app = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\Atlas.exe";
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }

        public  void CreateStartMenuShortcut()
        {
            string programs_path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string shortcutFolder = Path.Combine(programs_path, @"WildWest Company\Atlas Enterprise");
            if (!Directory.Exists(shortcutFolder))
            {
                Directory.CreateDirectory(shortcutFolder);
            }

            string app = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\Atlas.exe";
            string icon = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\atlasicono.ico";
            WshShellClass shellClass = new WshShellClass();
            //Create First Shortcut for Application Settings
            string settingsLink = Path.Combine(shortcutFolder, "Atlas.lnk");
            IWshShortcut shortcut = (IWshShortcut)shellClass.CreateShortcut(settingsLink);
            shortcut.TargetPath = app;
            shortcut.IconLocation = icon;
           // shortcut.Arguments = "arg1 arg2";
            shortcut.Description = "Business Software Suite";
            shortcut.Save();


            string uninstall = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\uninstall.exe";
            string uicon = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\uicon.ico";

            //Create Second Shortcut for Uninstall Setup
            string uninstallLink = Path.Combine(shortcutFolder, "Uninstall.lnk");
            shortcut = (IWshShortcut)shellClass.CreateShortcut(uninstallLink);
            shortcut.TargetPath = uninstall;
            shortcut.IconLocation = uicon;
            //shortcut.Arguments = "u";
            shortcut.Save();
        }
        public void CreateShortcutForAllUsers()
        {
            //StringBuilder allUserProfile = new StringBuilder(260);
            //SHGetSpecialFolderPath(IntPtr.Zero, allUserProfile, CSIDL_COMMON_STARTMENU, false);
            ////The above API call returns: C:\ProgramData\Microsoft\Windows\Start Menu
            //string programs_path = Path.Combine(allUserProfile.ToString(), "Programs");

            ////You can even use below one line code to avoid api call
            string programs_path= Path.Combine(Environment.GetEnvironmentVariable("ALLUSERSPROFILE"), @"Microsoft\Windows\Start Menu\Programs");
            string shortcutFolder = Path.Combine(programs_path, @"WildWest Company\Atlas Enterprise");
            if (!Directory.Exists(shortcutFolder))
            {
                Directory.CreateDirectory(shortcutFolder);
            }

            string app = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\Atlas.exe";
            string icon = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\WildWest Company\\Atlas Enterprise\\atlasicono.ico";

            WshShellClass shellClass = new WshShellClass();
            //Create Shortcut for Application Settings
            string settingsLink = Path.Combine(shortcutFolder, "Atlas.lnk");
            IWshShortcut shortcut = (IWshShortcut)shellClass.CreateShortcut(settingsLink);
            shortcut.TargetPath = app;
            shortcut.IconLocation = icon;
            //shortcut.Arguments = "arg1 arg2";
            shortcut.Description = "Business Software Suite";
            shortcut.Save();
        }


    }
}
