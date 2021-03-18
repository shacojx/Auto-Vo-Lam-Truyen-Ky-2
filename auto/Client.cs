using System;
using System.Collections.Generic;
using System.Linq;
using auto;

public class AutoClient : WinAPI
{
    public static uint AddressPlayer(IntPtr hProcess)
    {
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 8837316u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 2562480);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 4);
        return WinAPI.ReadProcessMemoryUint(hProcess, num + 184);
    }

    public static uint MenuID(IntPtr hProcess)
    {
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 8883004u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 76);
        return WinAPI.ReadProcessMemoryUint(hProcess, num + 304);
    }

    public static uint AddressItemLocation(IntPtr hProcess)
    {
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 10323344u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 2562480);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num);
        return WinAPI.ReadProcessMemoryUint(hProcess, num + 4);
    }

    public static List<Player.Item> GetItemList(IntPtr hProcess)
    {
        List<Player.Item> list = new List<Player.Item>();
        uint num = AddressItemLocation(hProcess);
        uint num2 = WinAPI.ReadProcessMemoryUint(hProcess, 8837316u);
        num2 = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 2562524);
        num2 = WinAPI.ReadProcessMemoryUint(hProcess, num2);
        int num3 = 0;
        for (int i = 0; i < 257; i++)
        {
            uint num4 = (uint)(i * 4);
            uint num5 = WinAPI.ReadProcessMemoryUint(hProcess, num2 + num4);
            byte[] value = WinAPI.ReadProcessMemoryArrBytes(hProcess, num5 + 130, 216);
            string text = CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(hProcess, num5 + 2420, 44));
            uint num6 = WinAPI.ReadProcessMemoryUint(hProcess, num5 + 4);
            uint num7 = WinAPI.ReadProcessMemoryUint(hProcess, num5 + 8);
            uint soluong = WinAPI.ReadProcessMemoryUint(hProcess, num5 + 48);
            if (num6 == 0 || !(text != ""))
            {
                continue;
            }
            int type;
            int cot;
            int hang;
            while (true)
            {
                byte[] value2 = WinAPI.ReadProcessMemoryArrBytes(hProcess, (uint)((int)num + (1152 + num3 * 20)), 20);
                int num8 = BitConverter.ToInt32(value2, 4);
                type = BitConverter.ToInt32(value2, 8);
                cot = BitConverter.ToInt32(value2, 12);
                hang = BitConverter.ToInt32(value2, 16);
                if (num8 == num7)
                {
                    break;
                }
                num3++;
            }
            num3 = 0;
            byte[] array = WinAPI.ReadProcessMemoryArrBytes(hProcess, num5 + 1240, 8);
            Player.Item item = new Player.Item();
            item.Name = text.TrimEnd().Replace("ThiƠt", "Thiết");
            item.id = num6;
            item.cot = (uint)cot;
            item.hang = (uint)hang;
            item.type = (uint)type;
            item.soluong = soluong;
            item.MTdong1 = array[0].ToString();
            item.MTdong2 = array[1].ToString();
            item.MTdong3 = array[2].ToString();
            item.MTdong4 = array[3].ToString();
            item.phuluc = array[4].ToString();
            item.thanh = array[5].ToString();
            item.offset = num4;
            item.itemdong1 = BitConverter.ToInt32(value, 6);
            item.itemdong2 = BitConverter.ToInt32(value, 42);
            item.itemdong3 = BitConverter.ToInt32(value, 78);
            item.itemdong4 = BitConverter.ToInt32(value, 114);
            item.itemdong5 = BitConverter.ToInt32(value, 150);
            item.itemdong6 = BitConverter.ToInt32(value, 186);
            item.itemdong1_O = BitConverter.ToInt32(value, 0);
            item.itemdong2_O = BitConverter.ToInt32(value, 36);
            item.itemdong3_O = BitConverter.ToInt32(value, 72);
            item.itemdong4_O = BitConverter.ToInt32(value, 108);
            item.itemdong5_O = BitConverter.ToInt32(value, 144);
            item.itemdong6_O = BitConverter.ToInt32(value, 180);
            list.Add(item);
        }
        return list.OrderBy((Player.Item z) => z.Name).ToList();
    }

    public static List<Player.NPCinfo> GetNPCList(IntPtr hProcess)
    {
        List<Player.NPCinfo> list = new List<Player.NPCinfo>();
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 10323344u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 2562484);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num);
        for (int i = 0; i < 257; i++)
        {
            uint num2 = WinAPI.ReadProcessMemoryUint(hProcess, (uint)((int)num + i * 4));
            string text = CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(hProcess, num2 + 68, 44));
            uint num3 = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 4);
            uint type = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 184);
            uint status = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 196);
            if (num3 != 0 && text != "")
            {
                Player.NPCinfo nPCinfo = new Player.NPCinfo();
                nPCinfo.Name = text;
                nPCinfo.id = num3;
                nPCinfo.type = type;
                nPCinfo.status = status;
                list.Add(nPCinfo);
            }
        }
        list.OrderBy((Player.NPCinfo x) => x.Name);
        return list;
    }

    public static void KsTui(Client client)
    {
        Player player = client.player;
        uint num = WinAPI.ReadProcessMemoryUint(player.HProcess, 10323344u);
        num = WinAPI.ReadProcessMemoryUint(player.HProcess, num + 2562484);
        num = WinAPI.ReadProcessMemoryUint(player.HProcess, num);
        for (int i = 0; i < 257; i++)
        {
            uint num2 = WinAPI.ReadProcessMemoryUint(player.HProcess, (uint)((int)num + i * 4));
            string text = CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(player.HProcess, num2 + 68, 44));
            uint npcId = WinAPI.ReadProcessMemoryUint(player.HProcess, num2 + 4);
            uint num3 = WinAPI.ReadProcessMemoryUint(player.HProcess, num2 + 196);
            if (text.Contains("Túi trái cây") && num3 != 0 && num3 != 5)
            {
                HookCall.ClickNpc(player.hWnd, npcId, player.Address);
            }
        }
    }

    public static List<Player.Item> GetItemListChest(IntPtr hProcess)
    {
        List<Player.Item> list = new List<Player.Item>();
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 268655096u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 28);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 40);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 748);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 8);
        uint num2 = WinAPI.ReadProcessMemoryUint(hProcess, 8837316u);
        num2 = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 2562524);
        num2 = WinAPI.ReadProcessMemoryUint(hProcess, num2);
        int num3 = 0;
        for (int i = 0; i < 257; i++)
        {
            uint num4 = WinAPI.ReadProcessMemoryUint(hProcess, (uint)((int)(num + 1152) + i * 4));
            uint num5 = WinAPI.ReadProcessMemoryUint(hProcess, (uint)((int)num2 + i * 4));
            string text = CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(hProcess, num5 + 2420, 44));
            uint num6 = WinAPI.ReadProcessMemoryUint(hProcess, num2 + 4);
            if (num6 != 0 && text != "")
            {
                byte[] value = WinAPI.ReadProcessMemoryArrBytes(hProcess, (uint)((int)num + (1152 + num3 * 20)), 20);
                int num7 = BitConverter.ToInt32(value, 4);
                int type = BitConverter.ToInt32(value, 8);
                int cot = BitConverter.ToInt32(value, 12);
                int hang = BitConverter.ToInt32(value, 16);
                num3++;
                Player.Item item = new Player.Item();
                item.Name = text;
                item.id = num6;
                item.cot = (uint)cot;
                item.hang = (uint)hang;
                item.type = (uint)type;
                list.Add(item);
            }
        }
        return list.OrderBy((Player.Item z) => z.Name).ToList();
    }

    public static uint BaseMenu(IntPtr hProcess)
    {
        return WinAPI.ReadProcessMemoryUint(hProcess, 8840676u);
    }

    public static int GetGold(IntPtr hProcess)
    {
        uint num = WinAPI.ReadProcessMemoryUint(hProcess, 5970012u);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 484);
        num = WinAPI.ReadProcessMemoryUint(hProcess, num + 44);
        return WinAPI.ReadProcessMemoryInt(hProcess, num + 560);
    }

    public static PlayerEnum.TeamStatus GroupStatus(IntPtr hProcess, uint address)
    {
        switch (WinAPI.ReadProcessMemoryArrBytes(hProcess, address + 12393, 1)[0])
        {
            case 33:
                return PlayerEnum.TeamStatus.CoTeam;
            case 41:
                return PlayerEnum.TeamStatus.TeamLeader;
            default:
                return PlayerEnum.TeamStatus.KhongCoTeam;
        }
    }
}
