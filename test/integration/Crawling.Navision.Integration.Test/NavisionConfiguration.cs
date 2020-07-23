using System.Collections.Generic;
using CluedIn.Crawling.Navision.Core;

namespace CluedIn.Crawling.Navision.Integration.Test
{
  public static class NavisionConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { NavisionConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
