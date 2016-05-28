using BlueMirrorIndexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.IO;
namespace BlueMirrorIndexer.Test
{
    
    
    /// <summary>
    ///This is a test class for FrmMainTest and is intended
    ///to contain all FrmMainTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FrmMainTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for importFrom1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlueMirrorIndexer.exe")]
        [DeploymentItem("database.octopus")]
        public void importFrom1Test() {
            FrmMain_Accessor target = new FrmMain_Accessor(); 
            string octopusDatabasePath = "database.octopus";
            target.importFrom1(octopusDatabasePath);
            Assert.AreEqual(FrmMain_Accessor.Database.GetDiscs()[0].Name, "KINGSTON 1");
            Assert.AreEqual((FrmMain_Accessor.Database.GetDiscs()[1] as IFolder).Folders[1].Files[0].Name, "OpenlifeR16.4rc2.exe");
            Assert.AreEqual(((FrmMain_Accessor.Database.GetDiscs()[1] as IFolder).Folders[1].Folders[0] as FolderInDatabase).Name, "Test");
        }

        /// <summary>
        ///A test for IsEmptyDatabase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlueMirrorIndexer.exe")]
        public void IsEmptyDatabaseTest() {
            FrmMain_Accessor target = new FrmMain_Accessor(); // TODO: Initialize to an appropriate value
            target.createNewVolumeDatabase();
            bool actual = target.IsEmptyDatabase();
            Assert.AreEqual(true, actual);
            FrmMain_Accessor.Database.AddDisc(new DiscInDatabase());
            actual = target.IsEmptyDatabase();
            Assert.AreEqual(false, actual);
        }
    }
}
