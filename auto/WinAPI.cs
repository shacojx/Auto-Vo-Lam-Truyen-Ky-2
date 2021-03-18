using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using auto;

public class WinAPI
{
    public enum ShowWindowCommands
    {
        Hide = 0,
        Normal = 1,
        ShowMinimized = 2,
        Maximize = 3,
        ShowMaximized = 3,
        ShowNoActivate = 4,
        Show = 5,
        Minimize = 6,
        ShowMinNoActive = 7,
        ShowNa = 8,
        Restore = 9,
        ShowDefault = 10,
        ForceMinimize = 11
    }

    public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

    private enum GetWindowType : uint
    {
        GwHwndfirst,
        GwHwndlast,
        GwHwndnext,
        GwHwndprev,
        GwOwner,
        GwChild,
        GwEnabledpopup
    }

    [DllImport("jx2hook.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
    public static extern uint GetMsg();

    [DllImport("jx2hook.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
    public static extern int InjectDll(IntPtr hwnd);

    [DllImport("jx2hook.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
    public static extern int UnmapDll(IntPtr hwnd);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);

    public static IntPtr ReadProcessMemoryIntPtr(IntPtr hProcess, uint lpBaseAddress)
    {
        byte[] array = new byte[4];
        ReadProcessMemory(hProcess, lpBaseAddress, array, array.Length, IntPtr.Zero);
        return (IntPtr)(long)BitConverter.ToUInt32(array, 0);
    }

    public static string ReadProcessMemoryString(IntPtr hProcess, uint lpBaseAddress, int size)
    {
        byte[] array = new byte[size];
        ReadProcessMemory(hProcess, lpBaseAddress, array, size, IntPtr.Zero);
        return Encoding.Default.GetString(array, 0, size).Split(default(char))[0];
    }

    public static uint ReadProcessMemoryUint(IntPtr hProcess, uint lpBaseAddress)
    {
        byte[] array = new byte[4];
        ReadProcessMemory(hProcess, lpBaseAddress, array, array.Length, IntPtr.Zero);
        return BitConverter.ToUInt32(array, 0);
    }

    public static int ReadProcessMemoryInt(IntPtr hProcess, uint lpBaseAddress)
    {
        byte[] array = new byte[4];
        ReadProcessMemory(hProcess, lpBaseAddress, array, array.Length, IntPtr.Zero);
        return BitConverter.ToInt32(array, 0);
    }

    public static byte[] ReadProcessMemoryArrBytes(IntPtr hProcess, uint lpBaseAddress, int size)
    {
        byte[] array = new byte[size];
        ReadProcessMemory(hProcess, lpBaseAddress, array, size, IntPtr.Zero);
        return array;
    }

    public static byte[] ReadProcessMemoryArrBytes(IntPtr hProcess, IntPtr lpBaseAddress, int size)
    {
        byte[] array = new byte[size];
        ReadProcessMemory(hProcess, (uint)(int)lpBaseAddress, array, size, IntPtr.Zero);
        return array;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, uint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, StringBuilder lParam);

    public static IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam)
    {
        return SendMessage(hWnd, msg, (IntPtr)(long)wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, uint lParam);

    public static bool PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam)
    {
        return PostMessage(hWnd, msg, (IntPtr)(long)wParam, lParam);
    }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("kernel32.dll", SetLastError = true)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
    {
        return OpenProcess(flags, false, proc.Id);
    }

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("kernel32.dll")]
    public static extern uint SetThreadExecutionState(uint esFlags);

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool TerminateProcess(IntPtr hProcess, int uExitCode);

    [DllImport("kernel32.dll")]
    public static extern uint GetLastError();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int InternalGetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetProcessTimes(IntPtr hProcess, out long lpCreationTime, out long lpExitTime, out long lpKernelTime, out long lpUserTime);

    public static ProcessJx2[] GetListjx2()
    {
        List<ProcessJx2> _list = new List<ProcessJx2>();
        EnumWindows(delegate (IntPtr hwnd, IntPtr param)
        {
            if (GetWindow(hwnd, GetWindowType.GwOwner) == IntPtr.Zero)
            {
                StringBuilder stringBuilder = new StringBuilder(100);
                InternalGetWindowText(hwnd, stringBuilder, 100);
                if (stringBuilder.ToString().Contains("ThienMonTran.Com (0.1)") || stringBuilder.ToString().Contains("Vâ L©m 2"))
                {
                    uint lpdwProcessId;
                    GetWindowThreadProcessId(hwnd, out lpdwProcessId);
                    ProcessJx2 processJx = new ProcessJx2
                    {
                        hWnd = hwnd,
                        PId = (int)lpdwProcessId
                    };
                    IntPtr intPtr = OpenProcess(ProcessAccessFlags.QueryInformation, false, (int)lpdwProcessId);
                    long lpCreationTime = 0L;
                    long lpExitTime;
                    long lpKernelTime;
                    long lpUserTime;
                    if (intPtr != IntPtr.Zero && GetProcessTimes(intPtr, out lpCreationTime, out lpExitTime, out lpKernelTime, out lpUserTime))
                    {
                        processJx.StartTime = lpCreationTime;
                        CloseHandle(intPtr);
                        _list.Add(processJx);
                    }
                }
            }
            return true;
        }, IntPtr.Zero);
        _list.Sort();
        return _list.ToArray();
    }

    public static IntPtr getmodule(int pid)
    {
        Process[] processesByName = Process.GetProcessesByName("so2game");
        Process[] array = processesByName;
        foreach (Process process in array)
        {
            if (process.Id != pid)
            {
                continue;
            }
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.ToLower().Contains("engine.dll"))
                {
                    return module.BaseAddress;
                }
            }
        }
        return IntPtr.Zero;
    }

    public static IntPtr getmodule(IntPtr hWnd, string module)
    {
        Process[] processesByName = Process.GetProcessesByName("so2game");
        Process[] array = processesByName;
        foreach (Process process in array)
        {
            if (!(process.MainWindowHandle == hWnd))
            {
                continue;
            }
            foreach (ProcessModule module2 in process.Modules)
            {
                if (module2.ModuleName.ToLower().Contains(module.ToLower()))
                {
                    return module2.BaseAddress;
                }
            }
        }
        return IntPtr.Zero;
    }

    public static uint getmodule()
    {
        Process[] processesByName = Process.GetProcessesByName("so2game");
        Process[] array = processesByName;
        foreach (Process process in array)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.ToLower().Contains("engine.dll"))
                {
                    return (uint)(int)module.BaseAddress;
                }
            }
        }
        return 0u;
    }
}
