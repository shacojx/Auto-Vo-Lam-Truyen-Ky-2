using System;

[Flags]
public enum ProcessAccessFlags : uint
{
    Terminate = 0x1u,
    CreateThread = 0x2u,
    VirtualMemoryOperation = 0x8u,
    VirtualMemoryRead = 0x10u,
    VirtualMemoryWrite = 0x20u,
    DuplicateHandle = 0x40u,
    CreateProcess = 0x80u,
    SetQuota = 0x100u,
    SetInformation = 0x200u,
    QueryInformation = 0x400u,
    QueryLimitedInformation = 0x1000u,
    Synchronize = 0x100000u,
    All = 0x1F0FFFu
}
