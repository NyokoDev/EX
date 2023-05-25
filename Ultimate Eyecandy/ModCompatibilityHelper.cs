using ColossalFramework.Plugins;
using ColossalFramework;

public static class ModCompatibilityHelper
{
    public static bool IsPlayItEnabled(string PlayIt)
    {
        foreach (var mod in Singleton<PluginManager>.instance.GetPluginsInfo())
        {
            if (mod.publishedFileID.AsUInt64 == 2741726428uL && mod.isEnabled)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsRenderItEnabled()
    {
        foreach (var mod in Singleton<PluginManager>.instance.GetPluginsInfo())
        {
            if (mod.publishedFileID.AsUInt64 == 1794015399uL && mod.isEnabled)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsThemeMixer25Enabled()
    {
        foreach (var mod in Singleton<PluginManager>.instance.GetPluginsInfo())
        {
            if (mod.publishedFileID.AsUInt64 == 2954236385uL && mod.isEnabled)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsRelightEnabled()
    {
        foreach (var mod in Singleton<PluginManager>.instance.GetPluginsInfo())
        {
            if (mod.publishedFileID.AsUInt64 == 1209581656uL && mod.isEnabled)
            {
                return true;
            }
        }
        return false;
    }
}
