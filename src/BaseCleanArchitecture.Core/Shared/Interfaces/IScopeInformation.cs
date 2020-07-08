using System.Collections.Generic;

namespace BaseCleanArchitecture.Core.Shared.Interfaces
{
    public interface IScopeInformation
    {
        Dictionary<string, string> HotScopeInfo { get; }
    }
}