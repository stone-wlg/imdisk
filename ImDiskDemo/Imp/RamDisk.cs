
using System;
using System.IO;

namespace ImDiskDemo.Imp
{
    internal class RamDisk
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
    }
}
