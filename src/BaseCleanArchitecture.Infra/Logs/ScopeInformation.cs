using System;
using System.Collections.Generic;
using System.Reflection;
using BaseCleanArchitecture.Core.Shared.Interfaces;

namespace BaseCleanArchitecture.Infra.Logs
{
    public class ScopeInformation : IScopeInformation
    {
        public Dictionary<string, string> HotScopeInfo => new Dictionary<string, string>
        {
            {"MachineName", Environment.MachineName },
            {"EntryPoint", Assembly.GetEntryAssembly().GetName().Name }
        };
    }
}