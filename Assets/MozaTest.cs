using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;

public class MozaTest : MonoBehaviour 
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll")]
    private static extern uint GetLastError();

    void Start()
    {
        // Try to load both DLLs
        string pluginsPath = Path.Combine(Application.dataPath, "Plugins");
        string cDllPath = Path.Combine(pluginsPath, "MOZA_API_C.dll");
        string csharpDllPath = Path.Combine(pluginsPath, "MOZA_API_CSharp.dll");

        Debug.Log($"Loading DLLs from: {pluginsPath}");

        // Try loading C DLL first (since it might be a dependency)
        IntPtr cHandle = LoadLibrary(cDllPath);
        if (cHandle == IntPtr.Zero)
        {
            uint error = GetLastError();
            Debug.LogError($"Failed to load MOZA_API_C.dll. Error code: {error}");
        }
        else
        {
            Debug.Log("Successfully loaded MOZA_API_C.dll");
        }

        // Then try loading C# DLL
        IntPtr csharpHandle = LoadLibrary(csharpDllPath);
        if (csharpHandle == IntPtr.Zero)
        {
            uint error = GetLastError();
            Debug.LogError($"Failed to load MOZA_API_CSharp.dll. Error code: {error}");
        }
        else
        {
            Debug.Log("Successfully loaded MOZA_API_CSharp.dll");
        }

        // Clean up
        if (cHandle != IntPtr.Zero)
            FreeLibrary(cHandle);
        if (csharpHandle != IntPtr.Zero)
            FreeLibrary(csharpHandle);

        // Additional debug info
        Debug.Log($"Process Architecture: {(Environment.Is64BitProcess ? "64-bit" : "32-bit")}");
        Debug.Log($"C DLL exists: {File.Exists(cDllPath)}");
        Debug.Log($"C# DLL exists: {File.Exists(csharpDllPath)}");
        
        // Try to get DLL version info
        if (File.Exists(csharpDllPath))
        {
            try
            {
                var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(csharpDllPath);
                Debug.Log($"C# DLL Version: {versionInfo.FileVersion}");
                Debug.Log($"C# DLL Description: {versionInfo.FileDescription}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error getting version info: {e.Message}");
            }
        }
    }
}