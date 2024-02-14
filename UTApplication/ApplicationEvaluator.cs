using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTApplication.Models;
using UTApplication.Services;

namespace UTApplication;

public class ApplicationEvaluator
{
    private const int MinAge = 18;
    private const int autoAcceptYearsOfExperience = 15;
    private List<string> techStackList = new() { "C", "C#", "C++", "Java" };
    private  readonly IIdentityValidator identityValidator;
    public ApplicationEvaluator(IIdentityValidator identityValidator)
    {
        this.identityValidator = identityValidator;
    }

    public ApplicationResult Evaluate(JobApplication form)
    {
        if (form.Applicant is null)
            throw new ArgumentNullException();

        if(form.Applicant.Age < MinAge)
            return ApplicationResult.AutoRejected;

        identityValidator.ValidationMode = form.Applicant.Age > 50 ? ValidationMode.Detailed : ValidationMode.Quick;

        if (identityValidator.Country != "Azerbaijan")
            return ApplicationResult.TransferredToCTO;


        var validIdentity = identityValidator.IsValid(form.Applicant.IdentityNumber);
        if (!validIdentity)
            return ApplicationResult.TransferredToHR;

        var sr = GetTechStackSimilarityRate(form.TechStackList);

        if (sr < 25)
            return ApplicationResult.AutoRejected;

        if (sr > 75 && form.YearsOfExperience>=autoAcceptYearsOfExperience)
            return ApplicationResult.AutoAccepted;

        

        return ApplicationResult.AutoAccepted;

    }

    private int GetTechStackSimilarityRate(List<string> techStack)
    {
        var MatchedCount = techStack.Where(i => techStackList.Contains(i, StringComparer.OrdinalIgnoreCase))
                             .Count();       

        return (int)((double)MatchedCount / techStackList.Count)*100;
    }

    public enum ApplicationResult
    {
        AutoRejected,
        TransferredToHR,
        TransferredToLead,
        TransferredToCTO,
        AutoAccepted
    }
}
