using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTApplication.Models;

namespace UTApplication;

public class ApplicationEvaluator
{

    public ApplicationResult Evaluate(JobApplication form)
    {
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
