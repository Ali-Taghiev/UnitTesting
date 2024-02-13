using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTApplication.Models;

namespace UTApplication;

public class ApplicationEvaluator
{
    private const int MinAge = 18;

    public ApplicationResult Evaluate(JobApplication form)
    {
        if(form.Applicant.Age < MinAge)
            return ApplicationResult.AutoRejected;

        return ApplicationResult.AutoAccepted;

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
