using System;
using System.Threading;
using auto;

public class HookCall : WinAPI
{
    public static uint Msg;

    public static void Chat(IntPtr hWnd, string text)
    {
        text = CFont.UnicodeToTCVN3(text);
        char[] array = text.ToCharArray();
        foreach (char lParam in array)
        {
            WinAPI.SendMessage(hWnd, Msg, 1014, lParam);
        }
        Thread.Sleep(100);
        WinAPI.SendMessage(hWnd, Msg, 1015, 1);
    }

    public static void BanItem(IntPtr hWnd, uint iditem)
    {
        WinAPI.SendMessage(hWnd, Msg, 1018u, iditem);
    }

    public static void UseItem(IntPtr hWnd, int c, int h)
    {
        WinAPI.SendMessage(hWnd, Msg, 1001u, (uint)c);
        WinAPI.SendMessage(hWnd, Msg, 1016u, (uint)h);
    }

    public static void UseItem(IntPtr hWnd, uint c, uint h)
    {
        WinAPI.SendMessage(hWnd, Msg, 1001u, c);
        WinAPI.SendMessage(hWnd, Msg, 1016u, h);
    }

    public static void SelectLineMenu(IntPtr hWnd, uint line, uint menuID)
    {
        WinAPI.SendMessage(hWnd, Msg, 1003u, menuID);
        WinAPI.SendMessage(hWnd, Msg, 1017u, line);
    }

    public static void CloseMenu(IntPtr hWnd)
    {
        WinAPI.SendMessage(hWnd, Msg, 1019, 8);
    }

    public static void UseSkill(IntPtr hWnd, uint x, uint y, uint skillid, uint PlayerAddr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1001u, x);
        WinAPI.SendMessage(hWnd, Msg, 1002u, y);
        WinAPI.SendMessage(hWnd, Msg, 1004u, skillid);
        WinAPI.SendMessage(hWnd, Msg, 1007u, PlayerAddr);
    }

    public static void ShortMove(IntPtr hWnd, uint x, uint y, uint PlayerAddr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1001u, x);
        WinAPI.SendMessage(hWnd, Msg, 1002u, y);
        WinAPI.SendMessage(hWnd, Msg, 1008u, PlayerAddr);
    }

    public static void ClickNpc(IntPtr hWnd, uint npcId, uint PlayerAddr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1003u, npcId);
        WinAPI.SendMessage(hWnd, Msg, 1005u, PlayerAddr);
    }

    public static void DoAttack(IntPtr hWnd, uint npcId, uint skillid, uint PlayerAddr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1003u, npcId);
        WinAPI.SendMessage(hWnd, Msg, 1004u, skillid);
        WinAPI.SendMessage(hWnd, Msg, 1006u, PlayerAddr);
    }

    public static void taykx5(IntPtr hWnd, uint iditem, uint hanhdong)
    {
        WinAPI.SendMessage(hWnd, Msg, 1003u, iditem);
        WinAPI.SendMessage(hWnd, Msg, 1020u, hanhdong);
    }

    public static void taykx5xoa(IntPtr hWnd, uint addr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1021u, addr);
    }

    public static void NhatItem(IntPtr hWnd)
    {
        WinAPI.SendMessage(hWnd, Msg, 1021, 0);
    }

    public static void TaoNhom(IntPtr hWnd)
    {
        WinAPI.SendMessage(hWnd, Msg, 1022, 0);
    }

    public static void GiaiTanNhom(IntPtr hWnd)
    {
        WinAPI.SendMessage(hWnd, Msg, 1023, 0);
    }

    public static void MoiNhom(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1024u, npcid);
    }

    public static void XinGiaNhap(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1025u, npcid);
    }

    public static void RoiNhom(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1026u, npcid);
    }

    public static void NhanLoi(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1027u, npcid);
    }

    public static void DongY(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1028u, npcid);
    }

    public static void TrucXuat(IntPtr hWnd, uint npcid)
    {
        WinAPI.SendMessage(hWnd, Msg, 1029u, npcid);
    }

    public static void Uplvl(IntPtr hWnd)
    {
        WinAPI.SendMessage(hWnd, Msg, 1030, 0);
    }

    public static void PickNPutItem(IntPtr hWnd, uint locationPick, uint cotpick, uint hangpick, uint locationPut, uint cotput, uint hangput, uint ItemAddr)
    {
        WinAPI.SendMessage(hWnd, Msg, 1032u, locationPick);
        WinAPI.SendMessage(hWnd, Msg, 1033u, cotpick);
        WinAPI.SendMessage(hWnd, Msg, 1034u, hangpick);
        WinAPI.SendMessage(hWnd, Msg, 1035u, locationPut);
        WinAPI.SendMessage(hWnd, Msg, 1036u, cotput);
        WinAPI.SendMessage(hWnd, Msg, 1037u, hangput);
        WinAPI.SendMessage(hWnd, Msg, 1031u, ItemAddr);
    }
}
