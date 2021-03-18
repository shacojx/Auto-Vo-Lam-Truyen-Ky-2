using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using auto;

public class Player
{
    public class Item
    {
        public uint id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public uint cot
        {
            get;
            set;
        }

        public uint hang
        {
            get;
            set;
        }

        public uint type
        {
            get;
            set;
        }

        public uint soluong
        {
            get;
            set;
        }

        public string thanh
        {
            get;
            set;
        }

        public string phuluc
        {
            get;
            set;
        }

        public string MTdong1
        {
            get;
            set;
        }

        public string MTdong2
        {
            get;
            set;
        }

        public string MTdong3
        {
            get;
            set;
        }

        public string MTdong4
        {
            get;
            set;
        }

        public uint offset
        {
            get;
            set;
        }

        public int itemdong1
        {
            get;
            set;
        }

        public int itemdong2
        {
            get;
            set;
        }

        public int itemdong3
        {
            get;
            set;
        }

        public int itemdong4
        {
            get;
            set;
        }

        public int itemdong5
        {
            get;
            set;
        }

        public int itemdong6
        {
            get;
            set;
        }

        public int itemdong1_O
        {
            get;
            set;
        }

        public int itemdong2_O
        {
            get;
            set;
        }

        public int itemdong3_O
        {
            get;
            set;
        }

        public int itemdong4_O
        {
            get;
            set;
        }

        public int itemdong5_O
        {
            get;
            set;
        }

        public int itemdong6_O
        {
            get;
            set;
        }
    }

    public class NPCinfo
    {
        public uint id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public uint status
        {
            get;
            set;
        }

        public uint type
        {
            get;
            set;
        }

        public uint address
        {
            get;
            set;
        }
    }

    public class TrongCayinfo
    {
        public bool HG
        {
            get;
            set;
        }

        public bool BNN
        {
            get;
            set;
        }

        public bool BNL
        {
            get;
            set;
        }
    }

    public string Msg = "";

    public List<Item> sellitemlist = new List<Item>();

    public List<NPCinfo> sellnpclist = new List<NPCinfo>();

    public List<NPCinfo> buffnpclist = new List<NPCinfo>();

    public TrongCayinfo TrongCaylist = new TrongCayinfo();

    public List<NPCinfo> TaoNhomlist = new List<NPCinfo>();

    public IntPtr HProcess
    {
        get;
        set;
    }

    public IntPtr hWnd
    {
        get;
        set;
    }

    public uint Address
    {
        get;
        set;
    }

    public bool isChat
    {
        get;
        set;
    }

    public bool isSell
    {
        get;
        set;
    }

    public bool isBuff
    {
        get;
        set;
    }

    public bool isBuffALL
    {
        get;
        set;
    }

    public bool isKsTui
    {
        get;
        set;
    }

    public bool isUseSkillCs
    {
        get;
        set;
    }

    public bool isBaoDanh
    {
        get;
        set;
    }

    public bool isTrongCay
    {
        get;
        set;
    }

    public bool isNhatAll
    {
        get;
        set;
    }

    public bool isTaoNhom
    {
        get;
        set;
    }

    public bool isTruongNhom
    {
        get;
        set;
    }

    public bool isTheoSau
    {
        get;
        set;
    }

    public string TheoSau_Name
    {
        get;
        set;
    }

    public bool isDBCTC
    {
        get;
        set;
    }

    public string NPCBB_Name
    {
        get;
        set;
    }

    public string CTP_Name
    {
        get;
        set;
    }

    public int TimeDelayChat
    {
        get;
        set;
    }

    public string _Name
    {
        get;
        set;
    }

    public Player(IntPtr hProcess, uint addr)
    {
        HProcess = hProcess;
        Address = addr;
        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Player"))
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Player");
        }
    }

    public void SaveData()
    {
        _Name = Name();
        string contents = JsonConvert.SerializeObject((object)this);
        string path = Directory.GetCurrentDirectory() + "\\Player\\" + Name() + ".json";
        File.WriteAllText(path, contents);
    }

    public int Hp()
    {
        return WinAPI.ReadProcessMemoryInt(HProcess, Address + 692);
    }

    public int Mp()
    {
        return WinAPI.ReadProcessMemoryInt(HProcess, Address + 744);
    }

    public string Name()
    {
        _Name = CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(HProcess, Address + 68, 44));
        return CFont.TCVN3ToUnicode(WinAPI.ReadProcessMemoryString(HProcess, Address + 68, 44));
    }
}
