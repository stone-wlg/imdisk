using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImDiskDemo
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void LoadAllDrivers()
        {
            var drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                Console.WriteLine(drive.Name);
            }
        }
    }
}
