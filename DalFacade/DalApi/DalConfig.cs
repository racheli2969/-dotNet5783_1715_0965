namespace DalApi;

using System.Diagnostics;
using System.Xml.Linq;

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;

    static DalConfig()
    {
        //string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\\..\\xml\dal-config.xml");
        //string sFilePath = Path.GetFullPath(sFile);
        //XElement dalConfig = XElement.Load(sFilePath)
            XElement dalConfig = XElement.Load(@"C:\Users\brach\source\repos\-dotNet5783_1715_0965\xml\dal-config.xml")
           // C: \Users\brach\source\repos\-dotNet5783_1715_0965\DalFacade\DalApi\DalConfig.cs
           ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
