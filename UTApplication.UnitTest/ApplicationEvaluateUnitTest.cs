using NUnit.Framework;
using UTApplication.Models;
using static UTApplication.ApplicationEvaluator;
using Moq;
using UTApplication.Services;
namespace UTApplication.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {



        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var evaluater = new ApplicationEvaluator(null);
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
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i=>i.IsValid(It.IsAny<string>())).Returns(true);    
            
            var evaluater = new ApplicationEvaluator(moqValidator.Object);
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
        public void Application_WithTechStackRateOver75_TransferredToAutoAccepted()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            var evaluater = new ApplicationEvaluator(moqValidator.Object);
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
        [Test]
        public void Application_WithInValidIndentityNumber_TransferredToHR()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                }
                

            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            Assert.AreEqual(Result, ApplicationResult.TransferredToHR);


        }

    }
}