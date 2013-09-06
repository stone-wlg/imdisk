using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LTR.IO;
using System.IO;

namespace ImDiskDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateDevice()
        {
            Int64 diskSize = 500 * 1024 * 1024;
            string mountPoint = "X:";
            UInt32 deviceNumber = 0;

            LTR.IO.ImDisk.ImDiskAPI.CreateDevice(diskSize, mountPoint, ref deviceNumber);
            //LTR.IO.ImDisk.ImDiskAPI.CreateMountPoint(mountPoint, deviceNumber);
            
            var diskData = LTR.IO.ImDisk.ImDiskAPI.QueryDevice(deviceNumber);

            Assert.IsTrue(deviceNumber >= 0);
        }

        [TestMethod]
        public void CreateMountPoint()
        {
            string directory = @"D:\h";
            string target = @"\Device\ImDisk4";

            try
            {
                LTR.IO.ImDisk.ImDiskAPI.CreateMountPoint(directory, target);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void LoadDriver()
        {
            //LTR.IO.ImDisk.ImDiskAPI.;
        }

        [TestMethod]
        public void RemoveMountPoint()
        {
            string directory = @"D:\h";

            LTR.IO.ImDisk.ImDiskAPI.RemoveMountPoint(directory);
        }

        [TestMethod]
        public void RemoveMountPoint_H()
        {
            string mountPoint = @"G";

            LTR.IO.ImDisk.ImDiskAPI.RemoveDevice(mountPoint);
        }

        [TestMethod]
        public void GetDevice()
       { 
            var deviceNumbers = LTR.IO.ImDisk.ImDiskAPI.GetDeviceList();
            foreach (uint deviceNumber in deviceNumbers)
            {
                var diskData = LTR.IO.ImDisk.ImDiskAPI.QueryDevice(deviceNumber);
            }
        }

        [TestMethod]
        public void ImDiskFindFreeDriveLetter()
        {
            char driveLetter = LTR.IO.ImDisk.DLL.ImDiskFindFreeDriveLetter();

            Assert.AreNotEqual<char>('C', driveLetter);
        }

        [TestMethod]
        public void RemoveDevice()
        {
            var deviceNumbers = LTR.IO.ImDisk.ImDiskAPI.GetDeviceList();

            foreach (uint deviceNumber in deviceNumbers)
            {
                //var diskData = LTR.IO.ImDisk.ImDiskAPI.QueryDevice(deviceNumber);
                //IntPtr handler = new IntPtr();

                try
                {
                    LTR.IO.ImDisk.ImDiskAPI.ForceRemoveDevice(deviceNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
