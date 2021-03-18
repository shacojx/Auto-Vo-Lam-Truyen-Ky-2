//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using auto;
using Newtonsoft.Json;

public class Form1 : Form
{
    public List<Client> _listClient = new List<Client>();

    private Client _currentPlayer;

    private IContainer components = null;

    private TabControl tabControl1;

    private TabPage tabtinhnang;

    private TabPage tabhoatdong;

    private ListView listViewPlayers;

    private ColumnHeader lvEnable;

    private ColumnHeader lvName;

    private ColumnHeader lvHide;

    private ColumnHeader lvhp;

    private ColumnHeader lvmp;

    private System.Windows.Forms.Timer time_client;

    private GroupBox groupBox1;

    private NumericUpDown numericUpDown1;

    private TextBox textBox1;

    private CheckBox cbChat;

    private GroupBox groupBox2;

    private Button btbando;

    private CheckBox cbSell;

    private GroupBox groupBox3;

    private Button btLuyenMT;

    private Label label2;

    private Label label1;

    private Button button3;

    private Label label3;

    private Button btTayKimXa;

    private GroupBox groupBox4;

    private Button btBuff;

    private CheckBox cbbuff;

    private CheckBox cbKsTui;

    private CheckBox cbskillcs;

    private GroupBox groupBox5;

    private Button btTrongCay;

    private CheckBox cbTrongCay;

    private GroupBox groupBox6;

    private CheckBox cbBaoDanh;

    private CheckBox cbNhatAll;

    private Button btBaoDanh;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem showToolStripMenuItem;

    private ToolStripMenuItem hideToolStripMenuItem;

    private GroupBox groupBox7;

    private Button btTaoNhom;

    private CheckBox cbTaoNhom;

    private ComboBox cbbTheoSau;

    private CheckBox cbTheoSau;

    private Button btgettheosau;

    public Form1()
    {
        InitializeComponent();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
        base.Top = 0;
        base.Left = Screen.PrimaryScreen.WorkingArea.Width - base.Width;

    }

    private void Time_client_Tick(object sender, EventArgs e)
    {
        ProcessJx2[] listjx = WinAPI.GetListjx2();
        for (int i = 0; i < Math.Max(_listClient.Count, listjx.Length); i++)
        {
            if (listjx.Length > _listClient.Count)
            {
                Client client1 = new Client(listjx[i].hWnd, listjx[i].PId);
                if (!_listClient.Exists((Client x) => x.hWnd == client1.hWnd))
                {
                    string text = client1.player.Name();
                    ListViewItem value = new ListViewItem(new string[5]
                    {
                        "",
                        text.Equals("") ? "DangKetNoi" : text,
                        WinAPI.IsWindowVisible(client1.hWnd) ? "0" : "1",
                        client1.player.Hp().ToString(),
                        client1.player.Mp().ToString()
                    });
                    _listClient.Add(client1);
                    listViewPlayers.Items.Add(value);
                }
            }
            else if (WinAPI.IsWindow(_listClient[i].hWnd))
            {
                _listClient[i]._time = _listClient[i]._time + 1;
                _listClient[i].player.Address = AutoClient.AddressPlayer(_listClient[i].player.HProcess);
                string text2 = _listClient[i].player.Name();
                string text3 = _listClient[i].player.Hp().ToString();
                string text4 = _listClient[i].player.Mp().ToString();
                string text5 = WinAPI.IsWindowVisible(_listClient[i].hWnd) ? "0" : "1";
                if (!text2.Equals(listViewPlayers.Items[i].SubItems[1].Text))
                {
                    listViewPlayers.Items[i].SubItems[1].Text = (text2.Equals("") ? "DangKetNoi" : text2);
                }
                if (!listViewPlayers.Items[i].SubItems[2].Text.Equals(text5))
                {
                    listViewPlayers.Items[i].SubItems[2].Text = text5;
                }
                if (!listViewPlayers.Items[i].SubItems[3].Text.Equals(text3))
                {
                    listViewPlayers.Items[i].SubItems[3].Text = (text2.Equals("DangKetNoi") ? "0" : text3);
                }
                if (!listViewPlayers.Items[i].SubItems[4].Text.Equals(text4))
                {
                    listViewPlayers.Items[i].SubItems[4].Text = text4;
                }
                if (_listClient[i].player.Hp() != 0)
                {
                    DoRunGame(_listClient[i], _listClient[i].player);
                }
                if (text2 != "" && _listClient[i].IsChecked)
                {
                    _listClient[i].player.SaveData();
                }
            }
            else
            {
                if (_listClient[i].player.Name() != "" && _listClient[i].IsChecked)
                {
                    _listClient[i].player.SaveData();
                }
                _listClient[i].Exit = true;
                _listClient.RemoveAt(i);
                listViewPlayers.Items.RemoveAt(i);
            }
        }
    }

   

    private void loadothersetting(Player player)
    {
        try
        {
            string text = player.Name();
            if (!(player._Name == text))
            {
                return;
            }
            string path = Directory.GetCurrentDirectory() + "\\Player\\" + text + ".json";
            if (File.Exists(path))
            {
                Player player2 = (Player)JsonConvert.DeserializeObject(File.ReadAllText(path), typeof(Player));
                textBox1.Text = player2.Msg;
                if (player2.TimeDelayChat == 0)
                {
                    player2.TimeDelayChat = 2;
                }
                numericUpDown1.Value = player2.TimeDelayChat;
                cbChat.Checked = player2.isChat;
                cbSell.Checked = player2.isSell;
                _currentPlayer.player.isChat = player2.isChat;
                _currentPlayer.player.isSell = player2.isSell;
                _currentPlayer.player.Msg = player2.Msg;
                _currentPlayer.player.TimeDelayChat = player2.TimeDelayChat;
                _currentPlayer.player.sellitemlist = player2.sellitemlist;
                _currentPlayer.player.sellnpclist = player2.sellnpclist;
                _currentPlayer.player.buffnpclist = player2.buffnpclist;
                _currentPlayer.player.isBuff = player2.isBuff;
                cbbuff.Checked = player2.isBuff;
                _currentPlayer.player.isBuffALL = player2.isBuffALL;
                _currentPlayer.player.isKsTui = player2.isKsTui;
                cbKsTui.Checked = player2.isKsTui;
                _currentPlayer.player.isUseSkillCs = player2.isUseSkillCs;
                cbskillcs.Checked = player2.isUseSkillCs;
                _currentPlayer.player.TrongCaylist = player2.TrongCaylist;
                _currentPlayer.player.isTrongCay = player2.isTrongCay;
                cbTrongCay.Checked = player2.isTrongCay;
                _currentPlayer.player.isNhatAll = player2.isNhatAll;
                cbNhatAll.Checked = player2.isNhatAll;
                _currentPlayer.player.isBaoDanh = player2.isBaoDanh;
                cbBaoDanh.Checked = player2.isBaoDanh;
                _currentPlayer.player.NPCBB_Name = player2.NPCBB_Name;
                _currentPlayer.player.CTP_Name = player2.CTP_Name;
                _currentPlayer.player.TaoNhomlist = player2.TaoNhomlist;
                _currentPlayer.player.isTaoNhom = player2.isTaoNhom;
                cbTaoNhom.Checked = player2.isTaoNhom;
                _currentPlayer.player.isTruongNhom = player2.isTruongNhom;
                _currentPlayer.player.isTheoSau = player2.isTheoSau;
                cbTheoSau.Checked = player2.isTheoSau;
                _currentPlayer.player.TheoSau_Name = player2.TheoSau_Name;
                if (player2.TheoSau_Name != null)
                {
                    cbbTheoSau.Items.Add(player2.TheoSau_Name);
                    cbbTheoSau.SelectedIndex = 0;
                }
                if (player.isTrongCay)
                {
                    _currentPlayer.trongcay = new Thread(_currentPlayer.otrongcay);
                    _currentPlayer.trongcay.IsBackground = true;
                    _currentPlayer.trongcay.Start();
                }
                if (player.isNhatAll)
                {
                    _currentPlayer.nhatall = new Thread(_currentPlayer.onhatall);
                    _currentPlayer.nhatall.IsBackground = true;
                    _currentPlayer.nhatall.Start();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Xuất hiện lỗi ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void DoRunGame(Client client, Player player)
    {
        if (player.isChat && client.IsChecked && (ulong)client._time % (ulong)player.TimeDelayChat == 0)
        {
            HookCall.Chat(client.hWnd, player.Msg);
            Thread.Sleep(100);
        }
        if (client.IsChecked && player.isTheoSau && (ulong)client._time % 2uL == 0)
        {
            foreach (Player.NPCinfo nPC in AutoClient.GetNPCList(player.HProcess))
            {
                if (nPC.Name == player.TheoSau_Name)
                {
                    HookCall.ClickNpc(player.hWnd, nPC.id, player.Address);
                }
            }
        }
        if (!client.IsChecked || !player.isTaoNhom || (ulong)client._time % 5uL != 0)
        {
            return;
        }
        if (AutoClient.GroupStatus(player.HProcess, player.Address) != 0)
        {
            if (AutoClient.GroupStatus(player.HProcess, player.Address) == PlayerEnum.TeamStatus.TeamLeader != player.isTruongNhom)
            {
                HookCall.GiaiTanNhom(player.hWnd);
            }
            if (!player.isTruongNhom)
            {
                return;
            }
            foreach (Player.NPCinfo item3 in AutoClient.GetNPCList(player.HProcess))
            {
                if (player.TaoNhomlist.Exists((Player.NPCinfo x) => x.Name == item3.Name))
                {
                    HookCall.DongY(player.hWnd, item3.id);
                }
            }
        }
        else
        {
            if (AutoClient.GroupStatus(player.HProcess, player.Address) != 0)
            {
                return;
            }
            if (player.isTruongNhom)
            {
                HookCall.TaoNhom(player.hWnd);
                return;
            }
            foreach (Client item4 in _listClient)
            {
                if (item4.player.isTruongNhom)
                {
                    continue;
                }
                foreach (Player.NPCinfo item2 in AutoClient.GetNPCList(player.HProcess))
                {
                    if (player.TaoNhomlist.Exists((Player.NPCinfo x) => x.Name == item2.Name))
                    {
                        HookCall.XinGiaNhap(player.hWnd, item2.id);
                        return;
                    }
                }
            }
        }
    }

    private void ListViewPlayers_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (listViewPlayers.Items[e.Index].Name.Equals("DangKetNoi"))
        {
            e.NewValue = e.CurrentValue;
        }
        else
        {
            _currentPlayer = _listClient[e.Index];
        }
    }

    private void ListViewPlayers_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
        if (e.Item.Index < 0)
        {
            return;
        }
        _currentPlayer = _listClient[e.Item.Index];
        if (_currentPlayer.player.Hp() == 0)
        {
            listViewPlayers.Items[e.Item.Index].Checked = false;
        }
        if (!e.Item.Checked)
        {
            try
            {
                if (_currentPlayer.IsChecked)
                {
                    WinAPI.UnmapDll(_currentPlayer.hWnd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            try
            {
                loadothersetting(_currentPlayer.player);
                if (WinAPI.InjectDll(_currentPlayer.hWnd) > 0 && HookCall.Msg == 0)
                {
                    HookCall.Msg = WinAPI.GetMsg();
                }
            }
            catch (DllNotFoundException ex2)
            {
                DllNotFoundException ex3 = ex2;
                MessageBox.Show("Thiếu file Jx2Hook.dll " + ex3.Message, "Thiếu tệp tin", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                e.Item.Checked = false;
            }
        }
        _currentPlayer.IsChecked = e.Item.Checked;
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        time_client.Stop();
        for (int i = 0; i < _listClient.Count; i++)
        {
            if (_listClient[i].IsChecked)
            {
                _listClient[i].player.SaveData();
                WinAPI.UnmapDll(_listClient[i].hWnd);
            }
            _listClient[i].Exit = true;
            try
            {
                WinAPI.CloseHandle(_listClient[i].player.HProcess);
            }
            catch
            {
            }
        }
        Application.Exit();
        Application.ExitThread();
        Environment.Exit(Environment.ExitCode);
    }

    private void cbChat_CheckedChanged(object sender, EventArgs e)
    {
        if (cbChat.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isChat = true;
                _currentPlayer.player.TimeDelayChat = (int)numericUpDown1.Value;
                _currentPlayer.player.Msg = textBox1.Text;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isChat = false;
            _currentPlayer.player.TimeDelayChat = (int)numericUpDown1.Value;
            _currentPlayer.player.Msg = textBox1.Text;
        }
    }

    private void cbSell_CheckedChanged(object sender, EventArgs e)
    {
        if (cbSell.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isSell = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isSell = false;
        }
    }

    private void Cbbuff_CheckedChanged(object sender, EventArgs e)
    {
        if (cbbuff.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isBuff = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isBuff = false;
        }
    }

    private void cbKsTui_CheckedChanged(object sender, EventArgs e)
    {
        if (cbKsTui.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isKsTui = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isKsTui = false;
        }
    }

    private void Cbskillcs_CheckedChanged(object sender, EventArgs e)
    {
        if (cbskillcs.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isUseSkillCs = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isUseSkillCs = false;
        }
    }

    private void cbBaoDanh_CheckedChanged(object sender, EventArgs e)
    {
        if (cbBaoDanh.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isBaoDanh = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isBaoDanh = false;
        }
    }

    private void CbTrongCay_CheckedChanged(object sender, EventArgs e)
    {
        if (cbTrongCay.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isTrongCay = true;
                _currentPlayer.trongcay = new Thread(_currentPlayer.otrongcay);
                _currentPlayer.trongcay.Start();
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isTrongCay = false;
            _currentPlayer.trongcay.Abort();
        }
    }

    private void CbNhatAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbNhatAll.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isNhatAll = true;
                _currentPlayer.nhatall = new Thread(_currentPlayer.onhatall);
                _currentPlayer.nhatall.Start();
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isNhatAll = false;
            _currentPlayer.nhatall.Abort();
        }
    }

    private void CbTaoNhom_CheckedChanged(object sender, EventArgs e)
    {
        if (cbTaoNhom.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isTaoNhom = true;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isTaoNhom = false;
        }
    }

    private void CbTheoSau_CheckedChanged(object sender, EventArgs e)
    {
        if (cbTheoSau.Checked)
        {
            if (_currentPlayer != null && _currentPlayer.IsChecked)
            {
                _currentPlayer.player.isTheoSau = true;
                _currentPlayer.player.TheoSau_Name = cbbTheoSau.Text;
            }
        }
        else if (_currentPlayer != null && _currentPlayer.IsChecked)
        {
            _currentPlayer.player.isTheoSau = false;
        }
    }

    private void ListViewPlayers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        try
        {
            if (e.IsSelected)
            {
                if (e.ItemIndex >= 0)
                {
                    _currentPlayer = _listClient[e.ItemIndex];
                }
                if (_currentPlayer != null && e.ItemIndex >= 0)
                {
                    loadothersetting(_currentPlayer.player);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Xuất hiện lỗi ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void ListViewPlayers_DoubleClick(object sender, EventArgs e)
    {
    }

    private void ListViewPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
    {
    }

    private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listViewPlayers.SelectedItems.Count > 0)
        {
            int index = listViewPlayers.Items.IndexOf(listViewPlayers.SelectedItems[0]);
            if (!WinAPI.IsWindowVisible(_listClient[index].hWnd))
            {
                WinAPI.ShowWindow(_listClient[index].hWnd, WinAPI.ShowWindowCommands.Restore);
            }
            else
            {
                WinAPI.SetForegroundWindow(_listClient[index].hWnd);
            }
        }
    }

    private void HideToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listViewPlayers.SelectedItems.Count > 0)
        {
            int index = listViewPlayers.Items.IndexOf(listViewPlayers.SelectedItems[0]);
            if (WinAPI.IsWindowVisible(_listClient[index].hWnd))
            {
                WinAPI.ShowWindow(_listClient[index].hWnd, WinAPI.ShowWindowCommands.ShowMinNoActive);
                WinAPI.ShowWindow(_listClient[index].hWnd, WinAPI.ShowWindowCommands.Hide);
            }
        }
    }

    private void ListViewPlayers_MouseDown(object sender, MouseEventArgs e)
    {
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        Process[] processesByName = Process.GetProcessesByName("so2game");
        Process[] array = processesByName;
        int num = 0;
        Process process;
        while (true)
        {
            if (num < array.Length)
            {
                process = array[num];
                if (process.MainWindowTitle == "ThienMonTran.Com (0.1)" || process.MainWindowTitle.Contains("Vâ L©m 2"))
                {
                    break;
                }
                num++;
                continue;
            }
            return;
        }
        if (WinAPI.InjectDll(process.MainWindowHandle) > 0 && HookCall.Msg == 0)
        {
            HookCall.Msg = WinAPI.GetMsg();
        }
        HookCall.ShortMove(process.MainWindowHandle, 45826u, 91580u, AutoClient.AddressPlayer(process.Handle));
        Thread.Sleep(2000);
        WinAPI.UnmapDll(process.MainWindowHandle);
    }

    private void btSell_Click(object sender, EventArgs e)
    {
        Sellitem sellitem = new Sellitem(_currentPlayer);
        sellitem.Text = _currentPlayer.player.Name();
        sellitem.Show();
    }

    private void btLuyenMT_Click(object sender, EventArgs e)
    {
        LuyenMT luyenMT = new LuyenMT(_currentPlayer);
        luyenMT.Text = _currentPlayer.player.Name();
        luyenMT.Show();
    }

    private void Button3_Click(object sender, EventArgs e)
    {
        UseGmItem useGmItem = new UseGmItem(_currentPlayer);
        useGmItem.Text = _currentPlayer.player.Name();
        useGmItem.Show();
    }

    private void BtTayKimXa_Click(object sender, EventArgs e)
    {
        TayKX5 tayKX = new TayKX5(_currentPlayer);
        tayKX.Text = _currentPlayer.player.Name();
        tayKX.Show();
    }

    private void btbuff_Click(object sender, EventArgs e)
    {
        AutoBuff autoBuff = new AutoBuff(_currentPlayer);
        autoBuff.Text = _currentPlayer.player.Name();
        autoBuff.Show();
    }

    private void BtBaoDanh_Click(object sender, EventArgs e)
    {
        BaoDanh baoDanh = new BaoDanh(_currentPlayer);
        baoDanh.Text = _currentPlayer.player.Name();
        baoDanh.Show();
    }

    private void BtTrongCay_Click(object sender, EventArgs e)
    {
        TrongCay trongCay = new TrongCay(_currentPlayer);
        trongCay.Text = _currentPlayer.player.Name();
        trongCay.Show();
    }

    private void BtTaoNhom_Click(object sender, EventArgs e)
    {
        TaoNhom taoNhom = new TaoNhom(_currentPlayer);
        taoNhom.Text = _currentPlayer.player.Name();
        taoNhom.Show();
    }

    private void Btgettheosau_Click(object sender, EventArgs e)
    {
        cbbTheoSau.Items.Clear();
        foreach (Player.NPCinfo nPC in AutoClient.GetNPCList(_currentPlayer.player.HProcess))
        {
            cbbTheoSau.Items.Add(nPC.Name);
        }
        cbbTheoSau.SelectedIndex = 0;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabtinhnang = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btTaoNhom = new System.Windows.Forms.Button();
            this.cbTaoNhom = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btTrongCay = new System.Windows.Forms.Button();
            this.cbTrongCay = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btBuff = new System.Windows.Forms.Button();
            this.cbbuff = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btgettheosau = new System.Windows.Forms.Button();
            this.cbbTheoSau = new System.Windows.Forms.ComboBox();
            this.cbTheoSau = new System.Windows.Forms.CheckBox();
            this.cbNhatAll = new System.Windows.Forms.CheckBox();
            this.cbskillcs = new System.Windows.Forms.CheckBox();
            this.cbKsTui = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btTayKimXa = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btLuyenMT = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btbando = new System.Windows.Forms.Button();
            this.cbSell = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbChat = new System.Windows.Forms.CheckBox();
            this.tabhoatdong = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btBaoDanh = new System.Windows.Forms.Button();
            this.cbBaoDanh = new System.Windows.Forms.CheckBox();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.lvEnable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvhp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvmp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.time_client = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabtinhnang.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabhoatdong.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabtinhnang);
            this.tabControl1.Controls.Add(this.tabhoatdong);
            this.tabControl1.Location = new System.Drawing.Point(2, 211);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(302, 273);
            this.tabControl1.TabIndex = 3;
            // 
            // tabtinhnang
            // 
            this.tabtinhnang.Controls.Add(this.groupBox7);
            this.tabtinhnang.Controls.Add(this.groupBox5);
            this.tabtinhnang.Controls.Add(this.groupBox4);
            this.tabtinhnang.Controls.Add(this.groupBox3);
            this.tabtinhnang.Controls.Add(this.groupBox2);
            this.tabtinhnang.Controls.Add(this.groupBox1);
            this.tabtinhnang.Location = new System.Drawing.Point(4, 22);
            this.tabtinhnang.Name = "tabtinhnang";
            this.tabtinhnang.Padding = new System.Windows.Forms.Padding(3);
            this.tabtinhnang.Size = new System.Drawing.Size(294, 247);
            this.tabtinhnang.TabIndex = 0;
            this.tabtinhnang.Text = "Tính Năng";
            this.tabtinhnang.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btTaoNhom);
            this.groupBox7.Controls.Add(this.cbTaoNhom);
            this.groupBox7.Enabled = false;
            this.groupBox7.Location = new System.Drawing.Point(150, 50);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(138, 42);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tạo Nhóm";
            // 
            // btTaoNhom
            // 
            this.btTaoNhom.Location = new System.Drawing.Point(56, 12);
            this.btTaoNhom.Name = "btTaoNhom";
            this.btTaoNhom.Size = new System.Drawing.Size(75, 23);
            this.btTaoNhom.TabIndex = 1;
            this.btTaoNhom.Text = "Cài Đặt";
            this.btTaoNhom.UseVisualStyleBackColor = true;
            this.btTaoNhom.Click += new System.EventHandler(this.BtTaoNhom_Click);
            // 
            // cbTaoNhom
            // 
            this.cbTaoNhom.AutoSize = true;
            this.cbTaoNhom.Location = new System.Drawing.Point(3, 16);
            this.cbTaoNhom.Name = "cbTaoNhom";
            this.cbTaoNhom.Size = new System.Drawing.Size(56, 17);
            this.cbTaoNhom.TabIndex = 0;
            this.cbTaoNhom.Text = "Active";
            this.cbTaoNhom.UseVisualStyleBackColor = true;
            this.cbTaoNhom.CheckedChanged += new System.EventHandler(this.CbTaoNhom_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btTrongCay);
            this.groupBox5.Controls.Add(this.cbTrongCay);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(150, 91);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(138, 43);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Trồng Cây";
            // 
            // btTrongCay
            // 
            this.btTrongCay.Location = new System.Drawing.Point(56, 12);
            this.btTrongCay.Name = "btTrongCay";
            this.btTrongCay.Size = new System.Drawing.Size(75, 23);
            this.btTrongCay.TabIndex = 1;
            this.btTrongCay.Text = "Cài Đặt";
            this.btTrongCay.UseVisualStyleBackColor = true;
            this.btTrongCay.Click += new System.EventHandler(this.BtTrongCay_Click);
            // 
            // cbTrongCay
            // 
            this.cbTrongCay.AutoSize = true;
            this.cbTrongCay.Location = new System.Drawing.Point(3, 16);
            this.cbTrongCay.Name = "cbTrongCay";
            this.cbTrongCay.Size = new System.Drawing.Size(56, 17);
            this.cbTrongCay.TabIndex = 0;
            this.cbTrongCay.Text = "Active";
            this.cbTrongCay.UseVisualStyleBackColor = true;
            this.cbTrongCay.CheckedChanged += new System.EventHandler(this.CbTrongCay_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btBuff);
            this.groupBox4.Controls.Add(this.cbbuff);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(6, 91);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 43);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buff";
            // 
            // btBuff
            // 
            this.btBuff.Location = new System.Drawing.Point(56, 12);
            this.btBuff.Name = "btBuff";
            this.btBuff.Size = new System.Drawing.Size(75, 23);
            this.btBuff.TabIndex = 1;
            this.btBuff.Text = "Cài Đặt";
            this.btBuff.UseVisualStyleBackColor = true;
            this.btBuff.Click += new System.EventHandler(this.btbuff_Click);
            // 
            // cbbuff
            // 
            this.cbbuff.AutoSize = true;
            this.cbbuff.Location = new System.Drawing.Point(3, 16);
            this.cbbuff.Name = "cbbuff";
            this.cbbuff.Size = new System.Drawing.Size(56, 17);
            this.cbbuff.TabIndex = 0;
            this.cbbuff.Text = "Active";
            this.cbbuff.UseVisualStyleBackColor = true;
            this.cbbuff.CheckedChanged += new System.EventHandler(this.Cbbuff_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btgettheosau);
            this.groupBox3.Controls.Add(this.cbbTheoSau);
            this.groupBox3.Controls.Add(this.cbTheoSau);
            this.groupBox3.Controls.Add(this.cbNhatAll);
            this.groupBox3.Controls.Add(this.cbskillcs);
            this.groupBox3.Controls.Add(this.cbKsTui);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btTayKimXa);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.btLuyenMT);
            this.groupBox3.Location = new System.Drawing.Point(6, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 107);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Extra";
            // 
            // btgettheosau
            // 
            this.btgettheosau.Enabled = false;
            this.btgettheosau.Location = new System.Drawing.Point(239, 13);
            this.btgettheosau.Name = "btgettheosau";
            this.btgettheosau.Size = new System.Drawing.Size(40, 23);
            this.btgettheosau.TabIndex = 13;
            this.btgettheosau.Text = "Get";
            this.btgettheosau.UseVisualStyleBackColor = true;
            this.btgettheosau.Click += new System.EventHandler(this.Btgettheosau_Click);
            // 
            // cbbTheoSau
            // 
            this.cbbTheoSau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTheoSau.Enabled = false;
            this.cbbTheoSau.FormattingEnabled = true;
            this.cbbTheoSau.Location = new System.Drawing.Point(124, 14);
            this.cbbTheoSau.Name = "cbbTheoSau";
            this.cbbTheoSau.Size = new System.Drawing.Size(111, 21);
            this.cbbTheoSau.TabIndex = 12;
            // 
            // cbTheoSau
            // 
            this.cbTheoSau.AutoSize = true;
            this.cbTheoSau.Enabled = false;
            this.cbTheoSau.Location = new System.Drawing.Point(56, 16);
            this.cbTheoSau.Name = "cbTheoSau";
            this.cbTheoSau.Size = new System.Drawing.Size(73, 17);
            this.cbTheoSau.TabIndex = 11;
            this.cbTheoSau.Text = "Theo Sau";
            this.cbTheoSau.UseVisualStyleBackColor = true;
            this.cbTheoSau.CheckedChanged += new System.EventHandler(this.CbTheoSau_CheckedChanged);
            // 
            // cbNhatAll
            // 
            this.cbNhatAll.AutoSize = true;
            this.cbNhatAll.Enabled = false;
            this.cbNhatAll.Location = new System.Drawing.Point(93, 42);
            this.cbNhatAll.Name = "cbNhatAll";
            this.cbNhatAll.Size = new System.Drawing.Size(84, 17);
            this.cbNhatAll.TabIndex = 10;
            this.cbNhatAll.Text = "Nhặt Tất Cả";
            this.cbNhatAll.UseVisualStyleBackColor = true;
            this.cbNhatAll.CheckedChanged += new System.EventHandler(this.CbNhatAll_CheckedChanged);
            // 
            // cbskillcs
            // 
            this.cbskillcs.AutoSize = true;
            this.cbskillcs.Enabled = false;
            this.cbskillcs.Location = new System.Drawing.Point(1, 42);
            this.cbskillcs.Name = "cbskillcs";
            this.cbskillcs.Size = new System.Drawing.Size(84, 17);
            this.cbskillcs.TabIndex = 8;
            this.cbskillcs.Text = "Use Skill CS";
            this.cbskillcs.UseVisualStyleBackColor = true;
            this.cbskillcs.CheckedChanged += new System.EventHandler(this.Cbskillcs_CheckedChanged);
            // 
            // cbKsTui
            // 
            this.cbKsTui.AutoSize = true;
            this.cbKsTui.Enabled = false;
            this.cbKsTui.Location = new System.Drawing.Point(1, 16);
            this.cbKsTui.Name = "cbKsTui";
            this.cbKsTui.Size = new System.Drawing.Size(56, 17);
            this.cbKsTui.TabIndex = 7;
            this.cbKsTui.Text = "Ks Túi";
            this.cbKsTui.UseVisualStyleBackColor = true;
            this.cbKsTui.CheckedChanged += new System.EventHandler(this.cbKsTui_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tẩy Kim Xà";
            // 
            // btTayKimXa
            // 
            this.btTayKimXa.Location = new System.Drawing.Point(200, 80);
            this.btTayKimXa.Name = "btTayKimXa";
            this.btTayKimXa.Size = new System.Drawing.Size(75, 23);
            this.btTayKimXa.TabIndex = 5;
            this.btTayKimXa.Text = "Cài Đặt";
            this.btTayKimXa.UseVisualStyleBackColor = true;
            this.btTayKimXa.Click += new System.EventHandler(this.BtTayKimXa_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dùng GM Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Luyện Mật Tịch";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(104, 80);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cài Đặt";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // btLuyenMT
            // 
            this.btLuyenMT.Location = new System.Drawing.Point(12, 78);
            this.btLuyenMT.Name = "btLuyenMT";
            this.btLuyenMT.Size = new System.Drawing.Size(75, 23);
            this.btLuyenMT.TabIndex = 1;
            this.btLuyenMT.Text = "Cài Đặt";
            this.btLuyenMT.UseVisualStyleBackColor = true;
            this.btLuyenMT.Click += new System.EventHandler(this.btLuyenMT_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btbando);
            this.groupBox2.Controls.Add(this.cbSell);
            this.groupBox2.Location = new System.Drawing.Point(6, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 42);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bán Đồ";
            // 
            // btbando
            // 
            this.btbando.Location = new System.Drawing.Point(56, 12);
            this.btbando.Name = "btbando";
            this.btbando.Size = new System.Drawing.Size(75, 23);
            this.btbando.TabIndex = 1;
            this.btbando.Text = "Cài Đặt";
            this.btbando.UseVisualStyleBackColor = true;
            this.btbando.Click += new System.EventHandler(this.btSell_Click);
            // 
            // cbSell
            // 
            this.cbSell.AutoSize = true;
            this.cbSell.Location = new System.Drawing.Point(3, 16);
            this.cbSell.Name = "cbSell";
            this.cbSell.Size = new System.Drawing.Size(56, 17);
            this.cbSell.TabIndex = 0;
            this.cbSell.Text = "Active";
            this.cbSell.UseVisualStyleBackColor = true;
            this.cbSell.CheckedChanged += new System.EventHandler(this.cbSell_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.cbChat);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chat";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(243, 16);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 20);
            this.textBox1.TabIndex = 1;
            // 
            // cbChat
            // 
            this.cbChat.AutoSize = true;
            this.cbChat.Location = new System.Drawing.Point(6, 19);
            this.cbChat.Name = "cbChat";
            this.cbChat.Size = new System.Drawing.Size(15, 14);
            this.cbChat.TabIndex = 0;
            this.cbChat.UseVisualStyleBackColor = true;
            this.cbChat.CheckedChanged += new System.EventHandler(this.cbChat_CheckedChanged);
            // 
            // tabhoatdong
            // 
            this.tabhoatdong.Controls.Add(this.groupBox6);
            this.tabhoatdong.Location = new System.Drawing.Point(4, 22);
            this.tabhoatdong.Name = "tabhoatdong";
            this.tabhoatdong.Padding = new System.Windows.Forms.Padding(3);
            this.tabhoatdong.Size = new System.Drawing.Size(294, 247);
            this.tabhoatdong.TabIndex = 1;
            this.tabhoatdong.Text = "Hoạt Động";
            this.tabhoatdong.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btBaoDanh);
            this.groupBox6.Controls.Add(this.cbBaoDanh);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(138, 42);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Báo Danh";
            // 
            // btBaoDanh
            // 
            this.btBaoDanh.Location = new System.Drawing.Point(57, 12);
            this.btBaoDanh.Name = "btBaoDanh";
            this.btBaoDanh.Size = new System.Drawing.Size(75, 23);
            this.btBaoDanh.TabIndex = 2;
            this.btBaoDanh.Text = "Cài Đặt";
            this.btBaoDanh.UseVisualStyleBackColor = true;
            this.btBaoDanh.Click += new System.EventHandler(this.BtBaoDanh_Click);
            // 
            // cbBaoDanh
            // 
            this.cbBaoDanh.AutoSize = true;
            this.cbBaoDanh.Location = new System.Drawing.Point(3, 16);
            this.cbBaoDanh.Name = "cbBaoDanh";
            this.cbBaoDanh.Size = new System.Drawing.Size(56, 17);
            this.cbBaoDanh.TabIndex = 0;
            this.cbBaoDanh.Text = "Active";
            this.cbBaoDanh.UseVisualStyleBackColor = true;
            this.cbBaoDanh.CheckedChanged += new System.EventHandler(this.cbBaoDanh_CheckedChanged);
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.CheckBoxes = true;
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvEnable,
            this.lvName,
            this.lvHide,
            this.lvhp,
            this.lvmp});
            this.listViewPlayers.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewPlayers.FullRowSelect = true;
            this.listViewPlayers.GridLines = true;
            this.listViewPlayers.Location = new System.Drawing.Point(2, 3);
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(298, 202);
            this.listViewPlayers.TabIndex = 2;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            this.listViewPlayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewPlayers_ItemCheck);
            this.listViewPlayers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewPlayers_ItemChecked);
            this.listViewPlayers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewPlayers_ItemSelectionChanged);
            this.listViewPlayers.DoubleClick += new System.EventHandler(this.ListViewPlayers_DoubleClick);
            this.listViewPlayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewPlayers_MouseDoubleClick);
            this.listViewPlayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewPlayers_MouseDown);
            // 
            // lvEnable
            // 
            this.lvEnable.Text = "";
            this.lvEnable.Width = 18;
            // 
            // lvName
            // 
            this.lvName.Text = "Name";
            this.lvName.Width = 121;
            // 
            // lvHide
            // 
            this.lvHide.Text = "Ẩn";
            this.lvHide.Width = 33;
            // 
            // lvhp
            // 
            this.lvhp.Text = "HP";
            // 
            // lvmp
            // 
            this.lvmp.Text = "Mana";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.HideToolStripMenuItem_Click);
            // 
            // time_client
            // 
            this.time_client.Enabled = true;
            this.time_client.Interval = 1500;
            this.time_client.Tick += new System.EventHandler(this.Time_client_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 483);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listViewPlayers);
            this.Name = "Form1";
            this.Text = "auto Auto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabtinhnang.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabhoatdong.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    
}
