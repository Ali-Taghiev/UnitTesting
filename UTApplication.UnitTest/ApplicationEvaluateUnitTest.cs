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

    }
}