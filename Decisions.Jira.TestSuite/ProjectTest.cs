using System.Net;
using Decisions.Jira;
using Decisions.Jira.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decisions.JiraTestSuite
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void Create()
        {
             JiraProjectModel userModel = new JiraProjectModel
            {
                Description = "New Project123",
                Name = "Test123",
                Key = "NP123", 
                ProjectTemplateKey = "com.pyxis.greenhopper.jira:gh-simplified-agility-kanban",
                LeadAccountId = "5ea1ce8c1f32260c13047996" ,
                AssigneeType= "UNASSIGNED", 
                ProjectTypeKey="business"

            };
            HttpStatusCode actualStatusCode = Project.Create(userModel).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
        [TestMethod]
        public void Edit()
        {
            JiraProjectModel userModel = new JiraProjectModel
            {
                ProjectIdOrKey = "DW",
                Description = "Test Desc",
                Name = "Dec_wfh_1",
                Key = "DW"
            };
            HttpStatusCode actualStatusCode = Project.Edit(userModel).Status;
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}
