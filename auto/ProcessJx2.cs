using System;
using System.ComponentModel;
using auto;

public class ProcessJx2 : IComparable
{
    public IntPtr hWnd;

    public int PId;

    public long StartTime;

    public int CompareTo(object obj)
    {
        return DateTime.FromFileTime(StartTime).CompareTo(DateTime.FromFileTime(((ProcessJx2)obj).StartTime));
    }

    public void Kill()
    {
        if (!WinAPI.TerminateProcess(WinAPI.OpenProcess(ProcessAccessFlags.Terminate, true, PId), -1))
        {
            throw new Win32Exception();
        }
    }
}
