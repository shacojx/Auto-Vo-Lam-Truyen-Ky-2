using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using auto;

public class Client
{
    public int Pid;

    public uint _time;

    public Thread thread;

    public Thread trongcay;

    public Thread nhatall;

    public IntPtr hWnd
    {
        get;
        set;
    }

    public IntPtr Module
    {
        get;
        set;
    }

    public Player player
    {
        get;
        set;
    }

    public bool IsChecked
    {
        get;
        set;
    }

    public bool Exit
    {
        get;
        set;
    }

    public Client(IntPtr _hWnd, int pid)
    {
        hWnd = _hWnd;
        Pid = pid;
        IntPtr intPtr = WinAPI.OpenProcess(ProcessAccessFlags.VirtualMemoryRead, false, pid);
        if (intPtr == IntPtr.Zero)
        {
            MessageBox.Show("Khong the doc duoc thong tin, chay lai auto voi quyen Admin", "Khong Auto duoc", MessageBoxButtons.OK);
            return;
        }
        player = new Player(intPtr, AutoClient.AddressPlayer(intPtr));
        player.hWnd = _hWnd;
        thread = new Thread(ThreadRunAutoClien);
        thread.IsBackground = true;
        thread.Start();
    }

    public void ThreadRunAutoClien()
    {
        while (!Exit && WinAPI.IsWindow(hWnd))
        {
            player.Address = AutoClient.AddressPlayer(player.HProcess);
            if (player.Name() != "" && player.Hp() != 0)
            {
                if (player.isSell && IsChecked && player.sellitemlist.Count > 0)
                {
                    foreach (Player.Item item in AutoClient.GetItemList(player.HProcess))
                    {
                        if (item.type != 2)
                        {
                            continue;
                        }
                        foreach (Player.Item item2 in player.sellitemlist)
                        {
                            if (item2.Name == item.Name)
                            {
                                HookCall.BanItem(hWnd, item.id);
                                break;
                            }
                        }
                    }
                }
                if (player.isBuff && IsChecked && (ulong)_time % 15uL == 0)
                {
                }
                if (IsChecked)
                {
                    HookCall.Uplvl(player.hWnd);
                    Thread.Sleep(10);
                }
                if (IsChecked && player.isKsTui)
                {
                    AutoClient.KsTui(this);
                    Thread.Sleep(10);
                }
                if (IsChecked && player.isBaoDanh)
                {
                    foreach (Player.NPCinfo nPC in AutoClient.GetNPCList(player.HProcess))
                    {
                        if (nPC.Name == player.NPCBB_Name && player.isDBCTC)
                        {
                            HookCall.ClickNpc(player.hWnd, nPC.id, player.Address);
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                        }
                        if (nPC.Name == player.NPCBB_Name && !player.isDBCTC)
                        {
                            HookCall.ClickNpc(player.hWnd, nPC.id, player.Address);
                            Thread.Sleep(20);
                            if (player.CTP_Name == "Thôn Trang")
                            {
                                HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                                Thread.Sleep(20);
                                HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                            }
                            else if (player.CTP_Name == "Tài Nguyên")
                            {
                                HookCall.SelectLineMenu(player.hWnd, 1u, AutoClient.MenuID(player.HProcess));
                                Thread.Sleep(20);
                                HookCall.SelectLineMenu(player.hWnd, 1u, AutoClient.MenuID(player.HProcess));
                            }
                            else if (player.CTP_Name == "Pháo Đài")
                            {
                                HookCall.SelectLineMenu(player.hWnd, 2u, AutoClient.MenuID(player.HProcess));
                                Thread.Sleep(20);
                                HookCall.SelectLineMenu(player.hWnd, 2u, AutoClient.MenuID(player.HProcess));
                            }
                        }
                    }
                }
            }
            Thread.Sleep(100);
        }
    }

    public void otrongcay()
    {
        while (player.isTrongCay)
        {
            List<Player.NPCinfo> nPCList = AutoClient.GetNPCList(player.HProcess);
            bool hG = player.TrongCaylist.HG;
            bool bNN = player.TrongCaylist.BNN;
            bool bNL = player.TrongCaylist.BNL;
            if (nPCList.Exists((Player.NPCinfo x) => x.Name.Contains(player.Name() + " trồng ")))
            {
                foreach (Player.Item item in AutoClient.GetItemList(player.HProcess))
                {
                    if (item.Name == "Hạt giống" && hG)
                    {
                        HookCall.UseItem(player.hWnd, item.cot, item.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                    if (item.Name == "Cây Bát Nhã nhỏ" && bNN)
                    {
                        HookCall.UseItem(player.hWnd, item.cot, item.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                    if (item.Name == "Cây Bát Nhã lớn" && bNL)
                    {
                        HookCall.UseItem(player.hWnd, item.cot, item.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                }
                foreach (Player.NPCinfo item2 in nPCList)
                {
                    if (item2.Name.Contains(player.Name() + " trồng "))
                    {
                        HookCall.ClickNpc(player.hWnd, item2.id, player.Address);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                }
            }
            else
            {
                foreach (Player.Item item3 in AutoClient.GetItemList(player.HProcess))
                {
                    if (item3.Name == "Hạt giống" && hG)
                    {
                        HookCall.UseItem(player.hWnd, item3.cot, item3.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                    if (item3.Name == "Cây Bát Nhã nhỏ" && bNN)
                    {
                        HookCall.UseItem(player.hWnd, item3.cot, item3.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                    if (item3.Name == "Cây Bát Nhã lớn" && bNL)
                    {
                        HookCall.UseItem(player.hWnd, item3.cot, item3.hang);
                        Thread.Sleep(50);
                        HookCall.SelectLineMenu(player.hWnd, 0u, AutoClient.MenuID(player.HProcess));
                    }
                }
            }
            Thread.Sleep(50);
            CloseMenu();
            Thread.Sleep(5000);
        }
    }

    public void onhatall()
    {
        while (player.isNhatAll)
        {
            HookCall.NhatItem(player.hWnd);
            Thread.Sleep(100);
        }
    }

    public void CloseMenu()
    {
        do
        {
            HookCall.CloseMenu(player.hWnd);
            Thread.Sleep(100);
            WinAPI.PostMessage(player.hWnd, 256u, 13u, 0u);
        }
        while (AutoClient.BaseMenu(player.HProcess) != 0);
    }
}
