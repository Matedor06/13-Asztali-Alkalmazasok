using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Solution.Domain.Database;

public static class DomainAssemblyReference
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyReference).Assembly;
}