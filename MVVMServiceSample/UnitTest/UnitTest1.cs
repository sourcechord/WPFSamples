using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMServiceSample;
using MVVMServiceSample.Services;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var vm = new MainWindowViewModel(new DummyDialogService());
            vm.Name = "hoge";
            vm.ShowNameCommand.Execute(null);
        }
    }
}
