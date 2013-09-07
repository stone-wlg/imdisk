
using LTR.IO.ImDisk;
using System;
using System.Runtime.InteropServices;
namespace ImDiskDemo.Imp
{
    public class ImDiskWrapper
    {
        #region CreateDevice

        /// <summary>
        /// create a virtual drive (unformatted)
        /// </summary>
        /// <param name="driveLetter">drive letter. Example : 'A', 'B', 'C', 'D', ..., 'Z'.</param>
        /// <param name="sizeInMegaBytes">drive size in mega bytes</param>
        /// <param name="deviceNumber">device number to create, UInt32.MaxValue for auto</param>
        /// <param name="backWithVirtualMemory">back the virtual drive with virtual memory (might slow the drive but enhance reliability)</param>
        /// <param name="skipImDiskNet">access directly the C++ ImDisk library without the ImDiskNet wrapper</param>
        /// <returns>true if success, false if failure</returns>
        public static bool CreateDevice(char driveLetter, int sizeInMegaBytes, ref uint deviceNumber, bool backWithVirtualMemory = false, bool skipImDiskNet = false)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }
            if (sizeInMegaBytes < 1)
            {
                throw new ArgumentException("Must be positive.", "sizeInMegaBytes");
            }

            #endregion
            long sizeInBytes = sizeInMegaBytes * 1024 * 1024;
            string mountPoint = GetMountPoint(driveLetter);
            ImDiskFlags mode = ImDiskFlags.DeviceTypeHD;
            if (backWithVirtualMemory)
            {
                mode = ImDiskFlags.DeviceTypeHD | ImDiskFlags.TypeVM;
            }
            if (!Load())
            {
                return false;
            }
            try
            {
                if (skipImDiskNet)
                {
                    CreateDevice_Cpl(sizeInBytes,      // Size of virtual disk. If this parameter is zero, current size of disk image file will automatically be used as virtual disk size.
                                     0,                // Number of tracks per cylinder for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // Number of sectors per track for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // Number of bytes per sector for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // A skip offset if virtual disk data does not begin immediately at start of disk image file. Frequently used with image formats like Nero NRG which start with a file header not used by ImDisk or Windows filesystem drivers.
                                     mode,             // Flags specifying properties for virtual disk. See comments for each flag value.
                                     null,             // Name of disk image file to use or create. If disk image file already exists the DiskSize parameter can be zero in which case current disk image file size will be used as virtual disk size. If Filename paramter is Nothing/null disk will be created in virtual memory and not backed by a physical disk image file.
                                     false,            // Specifies whether Filename parameter specifies a path in Windows native path format, the path format used by drivers in Windows NT kernels, for example \Device\Harddisk0\Partition1\imagefile.img. If this parameter is False path in FIlename parameter will be interpreted as an ordinary user application path.
                                     mountPoint,       // Mount point in the form of a drive letter and colon to create for newly created virtual disk. If this parameter is Nothing/null the virtual disk will be created without a drive letter.
                                     ref deviceNumber, // In: Device number for device to create. Device number must not be in use by an existing virtual disk. For automatic allocation of device number, pass UInt32.MaxValue. Out: Device number for created device.
                                     IntPtr.Zero);     // Optional handle to control that can display status messages during operation.
                }
                else
                {
                    ImDiskAPI.CreateDevice(sizeInBytes,      // Size of virtual disk. If this parameter is zero, current size of disk image file will automatically be used as virtual disk size.
                                           0,                // Number of tracks per cylinder for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // Number of sectors per track for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // Number of bytes per sector for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // A skip offset if virtual disk data does not begin immediately at start of disk image file. Frequently used with image formats like Nero NRG which start with a file header not used by ImDisk or Windows filesystem drivers.
                                           mode,             // Flags specifying properties for virtual disk. See comments for each flag value.
                                           null,             // Name of disk image file to use or create. If disk image file already exists the DiskSize parameter can be zero in which case current disk image file size will be used as virtual disk size. If Filename paramter is Nothing/null disk will be created in virtual memory and not backed by a physical disk image file.
                                           false,            // Specifies whether Filename parameter specifies a path in Windows native path format, the path format used by drivers in Windows NT kernels, for example \Device\Harddisk0\Partition1\imagefile.img. If this parameter is False path in FIlename parameter will be interpreted as an ordinary user application path.
                                           mountPoint,       // Mount point in the form of a drive letter and colon to create for newly created virtual disk. If this parameter is Nothing/null the virtual disk will be created without a drive letter.
                                           ref deviceNumber, // In: Device number for device to create. Device number must not be in use by an existing virtual disk. For automatic allocation of device number, pass UInt32.MaxValue. Out: Device number for created device.
                                           IntPtr.Zero);     // Optional handle to control that can display status messages during operation.
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        /// <summary>
        /// create a virtual drive (unformatted)
        /// </summary>
        /// <param name="driveLetter">drive letter. Example : 'A', 'B', 'C', 'D', ..., 'Z'.</param>
        /// <param name="sizeInMegaBytes">drive size in mega bytes</param>
        /// <param name="backWithVirtualMemory">back the virtual drive with virtual memory (might slow the drive but enhance reliability)</param>
        /// <param name="skipImDiskNet">access directly the C++ ImDisk library without the ImDiskNet wrapper</param>
        /// <returns>true if success, false if failure</returns>
        public static bool CreateDevice(char driveLetter, int sizeInMegaBytes, bool backWithVirtualMemory = false, bool skipImDiskNet = false)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }
            if (sizeInMegaBytes < 1)
            {
                throw new ArgumentException("Must be positive.", "sizeInMegaBytes");
            }

            #endregion
            long sizeInBytes = sizeInMegaBytes * 1024 * 1024;
            string mountPoint = GetMountPoint(driveLetter);
            ImDiskFlags mode = ImDiskFlags.DeviceTypeHD;
            if (backWithVirtualMemory)
            {
                mode = ImDiskFlags.DeviceTypeHD | ImDiskFlags.TypeVM;
            }
            if (!Load())
            {
                return false;
            }
            try
            {
                if (skipImDiskNet)
                {
                    CreateDevice_Cpl(sizeInBytes,      // Size of virtual disk. If this parameter is zero, current size of disk image file will automatically be used as virtual disk size.
                                     0,                // Number of tracks per cylinder for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // Number of sectors per track for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // Number of bytes per sector for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                     0,                // A skip offset if virtual disk data does not begin immediately at start of disk image file. Frequently used with image formats like Nero NRG which start with a file header not used by ImDisk or Windows filesystem drivers.
                                     mode,             // Flags specifying properties for virtual disk. See comments for each flag value.
                                     null,             // Name of disk image file to use or create. If disk image file already exists the DiskSize parameter can be zero in which case current disk image file size will be used as virtual disk size. If Filename paramter is Nothing/null disk will be created in virtual memory and not backed by a physical disk image file.
                                     false,            // Specifies whether Filename parameter specifies a path in Windows native path format, the path format used by drivers in Windows NT kernels, for example \Device\Harddisk0\Partition1\imagefile.img. If this parameter is False path in FIlename parameter will be interpreted as an ordinary user application path.
                                     mountPoint,       // Mount point in the form of a drive letter and colon to create for newly created virtual disk. If this parameter is Nothing/null the virtual disk will be created without a drive letter.
                                     IntPtr.Zero);     // Optional handle to control that can display status messages during operation.
                }
                else
                {
                    ImDiskAPI.CreateDevice(sizeInBytes,      // Size of virtual disk. If this parameter is zero, current size of disk image file will automatically be used as virtual disk size.
                                           0,                // Number of tracks per cylinder for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // Number of sectors per track for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // Number of bytes per sector for virtual disk geometry. This parameter can be zero in which case most reasonable value will be automatically used by the driver.
                                           0,                // A skip offset if virtual disk data does not begin immediately at start of disk image file. Frequently used with image formats like Nero NRG which start with a file header not used by ImDisk or Windows filesystem drivers.
                                           mode,             // Flags specifying properties for virtual disk. See comments for each flag value.
                                           null,             // Name of disk image file to use or create. If disk image file already exists the DiskSize parameter can be zero in which case current disk image file size will be used as virtual disk size. If Filename paramter is Nothing/null disk will be created in virtual memory and not backed by a physical disk image file.
                                           false,            // Specifies whether Filename parameter specifies a path in Windows native path format, the path format used by drivers in Windows NT kernels, for example \Device\Harddisk0\Partition1\imagefile.img. If this parameter is False path in FIlename parameter will be interpreted as an ordinary user application path.
                                           mountPoint,       // Mount point in the form of a drive letter and colon to create for newly created virtual disk. If this parameter is Nothing/null the virtual disk will be created without a drive letter.
                                           IntPtr.Zero);     // Optional handle to control that can display status messages during operation.
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #endregion

        #region CreateDevice_Cpl

        #region interop

        [DllImport("imdisk.cpl", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern bool ImDiskCreateDevice(IntPtr hWndStatusText,
                                                      ref DISK_GEOMETRY DiskGeometry,
                                                      ref long ImageOffset,
                                                      uint Flags,
                                                      [In, MarshalAs(UnmanagedType.LPWStr)] string Filename,
                                                      [MarshalAs(UnmanagedType.Bool)] bool NativePath,
                                                      [In, MarshalAs(UnmanagedType.LPWStr)] string MountPoint);

        [DllImport("imdisk.cpl", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern bool ImDiskCreateDeviceEx(IntPtr hWndStatusText,
                                                        ref uint DeviceNumber,
                                                        ref DISK_GEOMETRY DiskGeometry,
                                                        ref long ImageOffset,
                                                        uint Flags,
                                                        [In, MarshalAs(UnmanagedType.LPWStr)] string Filename,
                                                        [MarshalAs(UnmanagedType.Bool)] bool NativePath,
                                                        [In, MarshalAs(UnmanagedType.LPWStr)] string MountPoint);

        [StructLayout(LayoutKind.Sequential)]
        internal struct DISK_GEOMETRY
        {
            internal long Cylinders;
            internal MEDIA_TYPE MediaType;
            internal uint TracksPerCylinder;
            internal uint SectorsPerTrack;
            internal uint BytesPerSector;

            internal enum MEDIA_TYPE
            {
                Unknown,
                F5_1Pt2_512,
                F3_1Pt44_512,
                F3_2Pt88_512,
                F3_20Pt8_512,
                F3_720_512,
                F5_360_512,
                F5_320_512,
                F5_320_1024,
                F5_180_512,
                F5_160_512,
                RemovableMedia,
                FixedMedia,
                F3_120M_512,
                F3_640_512,
                F5_640_512,
                F5_720_512,
                F3_1Pt2_512,
                F3_1Pt23_1024,
                F5_1Pt23_1024,
                F3_128Mb_512,
                F3_230Mb_512,
                F8_256_128,
                F3_200Mb_512,
                F3_240M_512,
                F3_32M_512
            }
        }

        #endregion

        private static void CreateDevice_Cpl(long diskSize, uint tracksPerCylinder, uint sectorsPerTrack, uint bytesPerSector, long imageOffset,
                                             ImDiskFlags flags, string filename, bool nativePath, string mountPoint, IntPtr statusControl)
        {
            DISK_GEOMETRY diskGeometry = new DISK_GEOMETRY
            {
                Cylinders = diskSize,
                TracksPerCylinder = tracksPerCylinder,
                SectorsPerTrack = sectorsPerTrack,
                BytesPerSector = bytesPerSector
            };
            ImDiskCreateDevice(statusControl, ref diskGeometry, ref imageOffset, (uint)flags, filename, nativePath, mountPoint);
        }

        private static void CreateDevice_Cpl(long diskSize, uint tracksPerCylinder, uint sectorsPerTrack, uint bytesPerSector, long imageOffset,
                                             ImDiskFlags flags, string filename, bool nativePath, string mountPoint, ref uint deviceNumber, IntPtr statusControl)
        {
            DISK_GEOMETRY diskGeometry = new DISK_GEOMETRY
            {
                Cylinders = diskSize,
                TracksPerCylinder = tracksPerCylinder,
                SectorsPerTrack = sectorsPerTrack,
                BytesPerSector = bytesPerSector
            };
            ImDiskCreateDeviceEx(statusControl, ref deviceNumber, ref diskGeometry, ref imageOffset, (uint)flags, filename, nativePath, mountPoint);
        }

        #endregion

        #region CreateMountPoint

        /// <summary>
        /// create a mount point
        /// </summary>
        /// <param name="driveLetter">drive letter. Example : 'A', 'B', 'C', 'D', ..., 'Z'.</param>
        /// <returns>true if success, false if failure</returns>
        [Obsolete("Seems non functionnal, use DriveManager.FormatDrive() instead")]
        public static bool CreateMountPoint(char driveLetter)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }

            #endregion
            string mountPoint = GetMountPoint(driveLetter);
            string nativePath = @"\Device\ImDisk0";
            if (!Load())
            {
                return false;
            }
            try
            {
                ImDiskAPI.CreateMountPoint(mountPoint,  // Path to empty directory on an NTFS volume, or a drive letter followed by a colon.
                                           nativePath); // Target path in native format, for example \Device\ImDisk0
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #endregion

        #region GetMountPoint

        /// <summary>
        /// get "D:" from 'D' for example
        /// </summary>
        /// <param name="driveLetter">drive letter</param>
        /// <returns>drive name</returns>
        private static string GetMountPoint(char driveLetter)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }

            #endregion
            return driveLetter + ":";
        }

        #endregion

        #region Load

        public static bool Loaded { get; set; }

        /// <summary>
        /// load the ImDisk driver in Windows Kernel
        /// </summary>
        /// <returns>true if loaded, false otherwise</returns>
        public static bool Load()
        {
            if (Loaded == false)
            {
                try
                {
                    ImDiskAPI.LoadDriver();
                    Loaded = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("ImDiskInst.exe was not installed correctly, please reinstall it first.");
                }
            }
            return Loaded;
        }

        #endregion

        #region RemoveDevice

        /// <summary>
        /// remove a device
        /// </summary>
        /// <param name="driveLetter">drive letter. Example : 'A', 'B', 'C', 'D', ..., 'Z'.</param>
        /// <param name="deviceNumber">number of the device to remove</param>
        /// <returns>true if success, false if failure</returns>
        public static bool RemoveDevice(char driveLetter, uint deviceNumber = 0)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }

            #endregion
            string mountPoint = GetMountPoint(driveLetter);
            if (!Load())
            {
                return false;
            }
            try
            {
                ImDiskAPI.RemoveDevice(mountPoint); // Mount point in the form of a drive letter and colon to create for newly created virtual disk. If this parameter is Nothing/null the virtual disk will be created without a drive letter.
                ImDiskAPI.ForceRemoveDevice(deviceNumber);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #endregion

        #region RemoveMountPoint

        /// <summary>
        /// remove a mount point
        /// </summary>
        /// <param name="driveLetter">drive letter. Example : 'A', 'B', 'C', 'D', ..., 'Z'.</param>
        /// <returns>true if success, false if failure</returns>
        public static bool RemoveMountPoint(char driveLetter)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                throw new ArgumentException("Must be a letter from A to Z.", "driveLetter");
            }

            #endregion
            string mountPoint = GetMountPoint(driveLetter);
            if (!Load())
            {
                return false;
            }
            try
            {
                ImDiskAPI.RemoveMountPoint(mountPoint);  // Path to empty directory on an NTFS volume, or a drive letter followed by a colon.
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #endregion
    }
}
