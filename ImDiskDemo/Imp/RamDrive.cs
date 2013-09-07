
using System;
using System.Diagnostics;
using System.IO;

namespace ImDiskDemo.Imp
{
    internal class RamDrive
    {
        //public DriveInfo GetPickupDrive()
        //{
            
        //}

        //public DriveInfo GetQueueDrive()
        //{

        //}

        internal DriveInfo GetDriveByVolumeLabel(string volumeLabel)
        {
            var result = default(DriveInfo);
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (String.Equals(volumeLabel, drive.VolumeLabel, StringComparison.OrdinalIgnoreCase))
                {
                    result = drive;
                    break;
                }
            }

            return result;
        }

        internal DriveInfo CreateDrive(string volumeLabel)
        {
            var drive = default(DriveInfo);

            Int64 diskSize = 500 * 1024 * 1024;
            string driveName = LTR.IO.ImDisk.ImDiskAPI.FindFreeDriveLetter().ToString();
            string mountPoint = driveName + ":";
            UInt32 deviceNumber = 0;

            try
            {
                LTR.IO.ImDisk.ImDiskAPI.CreateDevice(diskSize, mountPoint, ref deviceNumber);

                drive = new DriveInfo(driveName);
                drive.VolumeLabel = volumeLabel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return drive;
        }

        public static bool CreateDrive(char driveLetter, int MegaByte, string label = "", string fileSystem = "NTFS", bool quickFormat = true, bool enableCompression = false, int? clusterSize = null)
        {
            #region args check

            if (!Char.IsLetter(driveLetter) ||
                !IsFileSystemValid(fileSystem))
            {
                return false;
            }

            #endregion
            bool success = false;
            string drive = driveLetter + ":";
//imdisk -a -s 100m -m x: -p "/fs:ntfs /q /y"
            try
            {
                var di = new DriveInfo(drive);
                var psi = new ProcessStartInfo();
                psi.FileName = "imdisk.exe";
                psi.WorkingDirectory = Environment.SystemDirectory;
                psi.Arguments = " -a -s " + MegaByte + " m -m " + drive +
                                " -p \"/FS:" + fileSystem +
                                             " /Y " +
                                             " /V:" + label +
                                             (quickFormat ? " /Q" : "") +
                                             ((fileSystem == "NTFS" && enableCompression) ? " /C" : "") +
                                             (clusterSize.HasValue ? " /A:" + clusterSize.Value : "") + "\"";
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                var formatProcess = Process.Start(psi);
                var swStandardInput = formatProcess.StandardInput;
                swStandardInput.WriteLine();
                formatProcess.WaitForExit();
                success = true;
            }
            catch (Exception) { }
            return success;
        }

        public static bool IsFileSystemValid(string fileSystem)
        {
            #region args check

            if (fileSystem == null)
            {
                return false;
            }

            #endregion
            switch (fileSystem)
            {
                case "FAT":
                case "FAT32":
                case "EXFAT":
                case "NTFS":
                case "UDF":
                    return true;
                default:
                    return false;
            }
        }
    }
}
