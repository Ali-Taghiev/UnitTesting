using NUnit.Framework;
using UTApplication.Models;
using static UTApplication.ApplicationEvaluator;
namespace UTApplication.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {



        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var evaluater = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };
            //Action

             var Result = evaluater.Evaluate(form);
            //Assert

            Assert.AreEqual(Result, ApplicationResult.AutoRejected);


        }
        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var evaluater = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList = new List<string>() {""}
                
                
                
            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            Assert.AreEqual(Result, ApplicationResult.AutoRejected);


        }
        [Test]
        public void Application_WithTechStackRateOver75_TransferredToAutoRejected()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var evaluater = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList = new List<string>() { "C", "C#", "C++", "Java" },
                YearsOfExperience = 16


            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            Assert.AreEqual(Result, ApplicationResult.AutoAccepted);


        }

    }
}