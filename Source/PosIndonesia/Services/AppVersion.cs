using System.Reflection;

namespace PosIndonesia.Services;
public class AppVersion
{
    public string Version
    {
        get => GetVersionFromAssembly();
    }

    static public string GetVersionFromAssembly()
    {
        string strVersion = default!;
        var versionAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        if (versionAttribute != null)
        {
            var version = versionAttribute.InformationalVersion;
            var plusIndex = version.IndexOf('+');
            if (plusIndex >= 0 && plusIndex + 9 < version.Length)
            {
                strVersion = version[..(plusIndex + 9)];
            }
            else
            {
                strVersion = version;
            }
        }

        return strVersion;
    }
}
