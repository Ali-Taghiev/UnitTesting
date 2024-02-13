using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTApplication.Services;
public class IdentityValidator : IIdentityValidator
{

    public bool IsValid(string IdentityNum)
    {
        return true;
    }
}

