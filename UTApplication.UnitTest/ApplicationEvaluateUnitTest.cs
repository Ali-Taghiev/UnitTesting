using NUnit.Framework;
using UTApplication.Models;
using static UTApplication.ApplicationEvaluator;
using Moq;
using UTApplication.Services;
using FluentAssertions;
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

            //Assert.AreEqual(Result, ApplicationResult.AutoRejected);
            Result.Should().Be(ApplicationResult.AutoRejected);


        }
        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i=>i.IsValid(It.IsAny<string>())).Returns(true);
            moqValidator.Setup(i => i.Country).Returns("Azerbaijan");

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

            //Assert.AreEqual(ApplicationResult.AutoRejected, Result);
            Result.Should().Be(ApplicationResult.AutoRejected);


        }
        [Test]
        public void Application_WithTechStackRateOver75_TransferredToAutoAccepted()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            moqValidator.Setup(i => i.Country).Returns("Azerbaijan");
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

            //Assert.AreEqual(ApplicationResult.AutoAccepted,Result);
            Result.Should().Be(ApplicationResult.AutoAccepted);



        }
        [Test]
        public void Application_WithInValidIndentityNumber_TransferredToHR()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            moqValidator.Setup(i => i.Country).Returns("Azerbaijan");
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

            //Assert.AreEqual(ApplicationResult.TransferredToHR, Result);
            Result.Should().Be(ApplicationResult.TransferredToHR);


        }

        [Test]
        public void Application_WithOfficeLocation_TransferredToCTO()  //UnitOfWork_Condition_ExpectedResult
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.Country).Returns("Turkey");
            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,

                }


            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            //Assert.AreEqual(ApplicationResult.TransferredToCTO, Result);
            Result.Should().Be(ApplicationResult.TransferredToCTO);


        }

        [Test]

        public void Application_WithOver50_ValidationModeToDetailed()
        {

            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();

            moqValidator.SetupProperty(i => i.ValidationMode); //Persist Data

            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 51,

                }


            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            //Assert.AreEqual(ValidationMode.Detailed,moqValidator.Object.ValidationMode);
            moqValidator.Object.ValidationMode.Should().Be(ValidationMode.Detailed);

        }
        [Test]
        public void Application_WithNullApplicant_ThrowArgumentNullException()
        {

            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication();
            //Action

            Action ResultAction = () =>evaluater.Evaluate(form);
            //Assert
            ResultAction.Should().Throw<ArgumentNullException>();
          
        }
        [Test]
        public void Application_WithDefaultValue_IsValidCalled()
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.Country).Returns("Azerbaijan");

            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                    IdentityNumber="555"
                },
                



            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            moqValidator.Verify(i => i.IsValid("555"),"IsValid Method should be called with 555"); 
            //moqValidator.Verify(i => i.IsValid(It.IsAny<string>()));

        }
        [Test]
        public void Application_WithYoungAge_IsValidNeverCalled()
        {
            //Arrange
            var moqValidator = new Mock<IIdentityValidator>();
            moqValidator.Setup(i => i.Country).Returns("Azerbaijan");

            var evaluater = new ApplicationEvaluator(moqValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 11
                },




            };
            //Action

            var Result = evaluater.Evaluate(form);
            //Assert

            moqValidator.Verify(i => i.IsValid(It.IsAny<string>()),Times.Never); //OR //Times.Exactly(0)

        }

    }
}