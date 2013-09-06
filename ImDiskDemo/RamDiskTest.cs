using System;
using ImDiskDemo.Imp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImDiskDemo
{
    [TestClass]
    public class RamDiskTest
    {
        [TestMethod]
        public void GetDriveByVolumeLabel_Pickup()
        {
            var ramdisk = new RamDisk();
            var volumeLabel = "Pickup";

            var actual = ramdisk.GetDriveByVolumeLabel(volumeLabel);

            Assert.IsNotNull(actual);
        }
    }
}
