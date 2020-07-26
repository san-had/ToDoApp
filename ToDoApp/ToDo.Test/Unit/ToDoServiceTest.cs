using Ninject;
using NUnit.Framework;
using ToDo.Extensibility;

namespace ToDo.Test.Unit
{
    public class ToDoServiceTest : TestBase
    {
        [TestCase(2, 10, 5, TestName = "Full page test")]
        [TestCase(3, 12, 5, TestName = "Page with rest items test")]
        [TestCase(1, 3, 5, TestName = "No full page at all test")]
        [TestCase(0, 0, 5, TestName = "Empty result test")]
        public void PageCountTest(int expectedCount, int recordCount, int pageSize)
        {
            var toDoService = kernel.Get<IToDoService>();

            int actualCount = toDoService.GetPageCount(recordCount, pageSize);

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}