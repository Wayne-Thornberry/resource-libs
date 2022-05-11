using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerLauncher.Program
{
    public interface ITestObj
    {
        string Name { get; }
    }

    public class TestData : ITestObj
    {
        public string Name { get; set; }
    }

    public class TestFile : DataFile<TestData>, ITestObj
    {
        public string Name => DataObject.Name;
    }
}
