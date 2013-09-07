using System;
using ImDiskDemo.Imp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImDiskDemo
{
    [TestClass]
    public class RamDriveTest
    {
        [TestMethod]
        public void GetDriveByVolumeLabel_Pickup()
        {
            var ramdisk = new RamDrive();
            var volumeLabel = "Pickup";

            var actual = ramdisk.GetDriveByVolumeLabel(volumeLabel);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CreateDrive()
        {
            char driveLetter = 'y';
            int MB = 100;
            string label = "Pickup";

            var actual = RamDrive.CreateDrive(driveLetter, MB, label);

            Assert.IsTrue(actual);
        }
    }
}
