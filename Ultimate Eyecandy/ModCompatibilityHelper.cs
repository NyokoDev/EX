using System;
using System.IO;

public class CompatibilityHelper
{
    private static string steamAppsFolder = GetSteamAppsFolder();

    private static string GetSteamAppsFolder()
    {
        string programFilesFolder = GetProgramFilesFolder();

        // Check if running on macOS
        if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
        {
            string homeFolder = GetHomeFolder();
            return PathCombine(homeFolder, "Library/Application Support/Steam/steamapps");
        }
        // Check if running on Windows
        else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            return PathCombine(programFilesFolder, "Steam", "steamapps");
        }
        else
        {
            throw new Exception("Unsupported operating system");
        }
    }

    private static string GetHomeFolder()
    {
        string homePath = Environment.GetEnvironmentVariable("HOME");
        if (!string.IsNullOrEmpty(homePath))
        {
            return homePath;
        }

        string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
        if (!string.IsNullOrEmpty(userProfile))
        {
            return userProfile;
        }

        throw new Exception("Unable to determine user home folder");
    }

    private static string GetProgramFilesFolder()
    {
        string programFilesPath = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)");
        if (!string.IsNullOrEmpty(programFilesPath))
        {
            return programFilesPath;
        }

        programFilesPath = Environment.GetEnvironmentVariable("PROGRAMFILES");
        if (!string.IsNullOrEmpty(programFilesPath))
        {
            return programFilesPath;
        }

        throw new Exception("Unable to determine Program Files folder");
    }

    private static string PathCombine(params string[] paths)
    {
        string combinedPath = paths[0];
        for (int i = 1; i < paths.Length; i++)
        {
            combinedPath = Path.Combine(combinedPath, paths[i]);
        }

        return combinedPath;
    }

    private static bool IsModInstalled(string steamId)
    {
        string modFolderPath = PathCombine(steamAppsFolder, "workshop", "content", "255710", steamId);

        return Directory.Exists(modFolderPath);
    }

    public static bool IsLutCreatorInstalled()
    {
        return IsModInstalled("2979442499");
    }

    public static bool IsEyecandyXInstalled()
    {
        return IsModInstalled("2980339529");
    }

    public static bool IsPlayItInstalled()
    {
        return IsModInstalled("2741726428");
    }

    public static bool IsRelightInstalled()
    {
        return IsModInstalled("1209581656");
    }

    public static bool IsRenderItInstalled()
    {
        return IsModInstalled("1794015399");
    }

    public static bool IsSpeedSliderV2Installed()
    {
        return IsModInstalled("406354745");
    }

    public static bool IsThemeMixer2Installed()
    {
        return IsModInstalled("1899640536");
    }

    public static bool IsThemeMixer2_5Installed()
    {
        return IsModInstalled("2954236385");
    }

    public static bool IsUltimateEyecandyInstalled()
    {
        return IsModInstalled("672248733");
    }

    public static bool IsUnifiedUIInstalled()
    {
        return IsModInstalled("2966990700");
    }
}
