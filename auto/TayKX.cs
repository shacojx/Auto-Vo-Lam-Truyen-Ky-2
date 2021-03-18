using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using auto;

public class TayKX5 : Form
{
    private Player player;

    private List<Player.Item> listItem = new List<Player.Item>();

    private List<Player.Item> listItemOld = new List<Player.Item>();

    private Thread othread;

    private IntPtr Module;

    private bool isloop = true;

    private bool isTayTam = true;

    private bool isLuyenLo = true;

    private int speed = 0;

    private string d1 = "";

    private string d2 = "";

    private string d3 = "";

    private Player.Item oldKX = new Player.Item();

    private IContainer components = null;

    private ComboBox comboBox1;

    private Label label1;

    private Button button1;

    private CheckBox checkBox1;

    private CheckBox checkBox2;

    private NumericUpDown numericUpDown1;

    private Label label2;

    private Label label3;

    private NumericUpDown numericUpDown2;

    private Label label4;

    private NumericUpDown numericUpDown3;

    private Button button2;

    private TrackBar trackBar1;

    private ComboBox comboBox2;

    private ComboBox comboBox3;

    private ComboBox comboBox4;

    public TayKX5(Client client)
    {
        InitializeComponent();
        player = client.player;
        Module = client.Module;
        if (client.IsChecked)
        {
        }
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (isLuyenLo)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            isTayTam = true;
            isLuyenLo = false;
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            string[] array = new string[6]
            {
                "Không Sử Dụng",
                "Sức Mạnh",
                "Nội Lực",
                "Gân Côt",
                "Thân Pháp",
                "Linh Hoạt"
            };
            ComboBox.ObjectCollection items = comboBox2.Items;
            object[] items2 = array;
            items.AddRange(items2);
            string[] array2 = new string[7]
            {
                "Không Sử Dụng",
                "Sức Mạnh",
                "Nội Lực",
                "Gân Côt",
                "Thân Pháp",
                "Linh Hoạt",
                "Sinh lực tối đa"
            };
            ComboBox.ObjectCollection items3 = comboBox3.Items;
            items2 = array2;
            items3.AddRange(items2);
            string[] array3 = new string[6]
            {
                "Không Sử Dụng",
                "Nội kích",
                "Tăng Ngoại Công",
                "Tăng Nội Ngoại Công",
                "Điểm bạo kích",
                "Sinh lực tối đa"
            };
            ComboBox.ObjectCollection items4 = comboBox4.Items;
            items2 = array3;
            items4.AddRange(items2);
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
    }

    private void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (isTayTam)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            isTayTam = false;
            isLuyenLo = true;
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            string[] array = new string[6]
            {
                "Không Sử Dụng",
                "Phòng thủ bạo kích",
                "Giảm thương bạo kích",
                "Tăng nội phòng",
                "Tăng ngoại phòng",
                "Sinh lực tối đa"
            };
            ComboBox.ObjectCollection items = comboBox2.Items;
            object[] items2 = array;
            items.AddRange(items2);
            string[] array2 = new string[6]
            {
                "Không Sử Dụng",
                "Sức Mạnh",
                "Nội Lực",
                "Gân Côt",
                "Thân Pháp",
                "Linh Hoạt"
            };
            ComboBox.ObjectCollection items3 = comboBox3.Items;
            items2 = array2;
            items3.AddRange(items2);
            string[] array3 = new string[4]
            {
                "Không Sử Dụng",
                "Tốc độ di chuyển",
                "Tốc độ xuất chiêu",
                "Xác xuất giảm nửa"
            };
            ComboBox.ObjectCollection items4 = comboBox4.Items;
            items2 = array3;
            items4.AddRange(items2);
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        comboBox1.Items.Clear();
        foreach (Player.Item item in from x in AutoClient.GetItemList(player.HProcess)
                                     orderby x.id descending
                                     select x)
        {
            if (item.type == 8 && !item.Name.Contains("Luyện Lô") && !item.Name.Contains("Tẩy Tâm"))
            {
                comboBox1.Items.Add(item.Name);
                oldKX.Name = item.Name;
                oldKX.id = item.id;
            }
            listItemOld.Add(item);
        }
        comboBox1.SelectedIndex = 0;
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        if (button2.Text == "Start")
        {
            button2.Text = "Stop";
            othread = new Thread((ThreadStart)delegate
            {
                ThreadTayKX();
            });
            othread.Start();
        }
        else
        {
            button2.Text = "Start";
            othread.Abort();
            isloop = false;
        }
    }

    private void ThreadTayKX()
    {
        isloop = true;
        string name = "";
        uint num = 0u;
        int num2 = 0;
        while (isloop)
        {
            listItem.Clear();
            comboBox1.Invoke((MethodInvoker)delegate
            {
                name = comboBox1.Text;
            });
            num = ((!isLuyenLo) ? ttoffsetKX(name) : lloffsetKX(name));
            if (AutoClient.BaseMenu(player.HProcess) != 0)
            {
                do
                {
                    HookCall.CloseMenu(player.hWnd);
                    Thread.Sleep(100);
                    HookCall.CloseMenu(player.hWnd);
                }
                while (AutoClient.BaseMenu(player.HProcess) != 0);
            }
            HookCall.taykx5(player.hWnd, num, 78u);
            Thread.Sleep(700 - speed);
            listItem.AddRange(AutoClient.GetItemList(player.HProcess));
            if (num2 == 50)
            {
                num2 = 0;
                foreach (Player.Item item2 in listItem)
                {
                    if (item2.Name == "Thẻ Trải Nghiệm")
                    {
                        HookCall.UseItem(player.hWnd, item2.cot, item2.hang);
                        Thread.Sleep(20);
                        HookCall.SelectLineMenu(player.hWnd, 12u, AutoClient.MenuID(player.HProcess));
                        do
                        {
                            HookCall.CloseMenu(player.hWnd);
                            Thread.Sleep(10);
                            HookCall.CloseMenu(player.hWnd);
                        }
                        while (AutoClient.BaseMenu(player.HProcess) != 0);
                    }
                }
            }
            int kxcap = GetCapKX(name);
            if (!listItem.Exists((Player.Item x) => x.Name.Contains("Tẩy Tâm Thạch cấp " + kxcap) && x.type == 2) && isTayTam)
            {
                foreach (Player.Item item3 in listItem)
                {
                    if (item3.Name == "Thẻ Trải Nghiệm")
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            HookCall.UseItem(player.hWnd, item3.cot, item3.hang);
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 13u, AutoClient.MenuID(player.HProcess));
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 9u, AutoClient.MenuID(player.HProcess));
                            Thread.Sleep(20);
                        }
                        Thread.Sleep(100);
                        do
                        {
                            HookCall.CloseMenu(player.hWnd);
                            Thread.Sleep(100);
                            HookCall.CloseMenu(player.hWnd);
                        }
                        while (AutoClient.BaseMenu(player.HProcess) != 0);
                    }
                }
            }
            if (!listItem.Exists((Player.Item x) => x.Name.Contains("Luyện Lô Thiết cấp " + kxcap) && x.type == 2) && isLuyenLo)
            {
                foreach (Player.Item item4 in listItem)
                {
                    if (item4.Name == "Thẻ Trải Nghiệm")
                    {
                        for (int j = 0; j < 11; j++)
                        {
                            HookCall.UseItem(player.hWnd, item4.cot, item4.hang);
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 13u, AutoClient.MenuID(player.HProcess));
                            Thread.Sleep(20);
                            HookCall.SelectLineMenu(player.hWnd, 9u, AutoClient.MenuID(player.HProcess));
                            Thread.Sleep(20);
                        }
                        Thread.Sleep(100);
                        do
                        {
                            HookCall.CloseMenu(player.hWnd);
                            Thread.Sleep(100);
                            HookCall.CloseMenu(player.hWnd);
                        }
                        while (AutoClient.BaseMenu(player.HProcess) != 0);
                    }
                }
            }
            if (isTayTam && !listItem.Exists((Player.Item x) => x.Name.Contains("Tẩy Tâm Thạch cấp " + kxcap) && x.type == 8))
            {
                foreach (Player.Item item5 in listItem)
                {
                    if (item5.Name.Contains("Tẩy Tâm Thạch cấp " + kxcap) && item5.type == 2)
                    {
                        HookCall.PickNPutItem(player.hWnd, item5.type, item5.cot, item5.hang, 8u, 1u, 0u, AutoClient.AddressItemLocation(player.HProcess));
                        break;
                    }
                }
            }
            if (isLuyenLo && !listItem.Exists((Player.Item x) => x.Name.Contains("Luyện Lô Thiết cấp " + kxcap) && x.type == 8))
            {
                foreach (Player.Item item6 in listItem)
                {
                    if (item6.Name.Contains("Luyện Lô Thiết cấp " + kxcap) && item6.type == 2)
                    {
                        HookCall.PickNPutItem(player.hWnd, item6.type, item6.cot, item6.hang, 8u, 1u, 0u, AutoClient.AddressItemLocation(player.HProcess));
                        break;
                    }
                }
            }
            foreach (Player.Item item in listItem.Where((Player.Item x) => x.Name == name))
            {
                if (item.Name == name && item.type != 8 && isTayTam && !listItemOld.Exists((Player.Item x) => x.id == item.id) && item.type != 3)
                {
                    if ((item.itemdong1 == (int)numericUpDown1.Value || (int)numericUpDown1.Value == 0) && (item.itemdong2 == (int)numericUpDown2.Value || (int)numericUpDown2.Value == 0) && (item.itemdong3 == (int)numericUpDown3.Value || (int)numericUpDown3.Value == 0) && (GetOpKX(d1) == 0 || item.itemdong1_O == GetOpKX(d1)) && (GetOpKX(d2) == 0 || item.itemdong2_O == GetOpKX(d2)) && (GetOpKX(d3) == 0 || item.itemdong3_O == GetOpKX(d3)))
                    {
                        HookCall.taykx5(player.hWnd, num, 79u);
                        button2.Invoke((MethodInvoker)delegate
                        {
                            button2.Text = "Start";
                        });
                        isloop = false;
                        return;
                    }
                }
                else if (item.Name == name && item.type != 8 && isLuyenLo && !listItemOld.Exists((Player.Item x) => x.id == item.id) && item.type != 3 && (item.itemdong4 == (int)numericUpDown1.Value || (int)numericUpDown1.Value == 0) && (item.itemdong5 == (int)numericUpDown2.Value || (int)numericUpDown2.Value == 0) && (item.itemdong6 == (int)numericUpDown3.Value || (int)numericUpDown3.Value == 0) && (GetOpKX(d1) == 0 || item.itemdong4_O == GetOpKX(d1)) && (GetOpKX(d2) == 0 || item.itemdong5_O == GetOpKX(d2)) && (GetOpKX(d3) == 0 || item.itemdong6_O == GetOpKX(d3)))
                {
                    HookCall.taykx5(player.hWnd, num, 79u);
                    button2.Invoke((MethodInvoker)delegate
                    {
                        button2.Text = "Start";
                    });
                    isloop = false;
                    return;
                }
            }
            int num3 = 0;
            while (true)
            {
            IL_0afe:
                HookCall.taykx5(player.hWnd, num, 80u);
                Thread.Sleep(500 - speed);
                foreach (Player.Item item7 in AutoClient.GetItemList(player.HProcess))
                {
                    if (item7.Name == name && item7.type != 1 && item7.type != 2 && item7.type != 3 && item7.type != 7 && item7.type != 8)
                    {
                        num3++;
                        goto IL_0afe;
                    }
                    if (num3 > 300)
                    {
                        button2.Invoke((MethodInvoker)delegate
                        {
                            button2.Text = "Start";
                        });
                        MessageBox.Show("LAGGGGGGGGGGGGGG!!!");
                        return;
                    }
                }
                break;
            }
            num2++;
        }
    }

    private void TrackBar1_Scroll(object sender, EventArgs e)
    {
        speed = trackBar1.Value * 100;
    }

    public uint ttoffsetKX(string name)
    {
        switch (name)
        {
            case "áo khoác Hiệp Cốt":
                return 151u;
            case "áo khoác Qủy Phù":
                return 152u;
            case "áo khoác Đằng Vân":
                return 153u;
            case "Huy chương Hiệp Cốt":
                return 154u;
            case "Huy chương Qủy Phù":
                return 155u;
            case "Huy chương Đằng Vân":
                return 156u;
            case "Hiệp Cốt Trường Ngoa":
                return 157u;
            case "Qủy Phù Chiến Ngoa":
                return 158u;
            case "Đằng Vân Đạo Ngoa":
                return 159u;
            case "Hoan Lăng Phi Phong":
                return 201u;
            case "Đằng Giao Phi Phong":
                return 203u;
            case "Khởi Phượng Phi Phon":
                return 204u;
            case "Hoan Lăng Chương":
                return 205u;
            case "Đằng Giao Chương":
                return 206u;
            case "Khởi Phượng Chương":
                return 207u;
            case "Hoan Lăng Ngoa":
                return 208u;
            case "Đằng Giao Ngoa":
                return 209u;
            case "Khởi Phượng Ngoa":
                return 210u;
            default:
                return 0u;
        }
    }

    public uint lloffsetKX(string name)
    {
        switch (name)
        {
            case "áo khoác Hiệp Cốt":
                return 160u;
            case "áo khoác Qủy Phù":
                return 161u;
            case "áo khoác Đằng Vân":
                return 162u;
            case "Huy chương Hiệp Cốt":
                return 163u;
            case "Huy chương Qủy Phù":
                return 164u;
            case "Huy chương Đằng Vân":
                return 165u;
            case "Hiệp Cốt Trường Ngoa":
                return 166u;
            case "Qủy Phù Chiến Ngoa":
                return 167u;
            case "Đằng Vân Đạo Ngoa":
                return 168u;
            case "Hoan Lăng Phi Phong":
                return 211u;
            case "Đằng Giao Phi Phong":
                return 212u;
            case "Khởi Phượng Phi Phon":
                return 213u;
            case "Hoan Lăng Chương":
                return 214u;
            case "Đằng Giao Chương":
                return 215u;
            case "Khởi Phượng Chương":
                return 216u;
            case "Hoan Lăng Ngoa":
                return 217u;
            case "Đằng Giao Ngoa":
                return 218u;
            case "Khởi Phượng Ngoa":
                return 219u;
            default:
                return 0u;
        }
    }

    public int GetCapKX(string name)
    {
        switch (name)
        {
            case "áo khoác Hiệp Cốt":
                return 5;
            case "áo khoác Qủy Phù":
                return 5;
            case "áo khoác Đằng Vân":
                return 5;
            case "Huy chương Hiệp Cốt":
                return 5;
            case "Huy chương Qủy Phù":
                return 5;
            case "Huy chương Đằng Vân":
                return 5;
            case "Hiệp Cốt Trường Ngoa":
                return 5;
            case "Qủy Phù Chiến Ngoa":
                return 5;
            case "Đằng Vân Đạo Ngoa":
                return 5;
            case "Hoan Lăng Phi Phong":
                return 6;
            case "Đằng Giao Phi Phong":
                return 6;
            case "Khởi Phượng Phi Phon":
                return 6;
            case "Hoan Lăng Chương":
                return 6;
            case "Đằng Giao Chương":
                return 6;
            case "Khởi Phượng Chương":
                return 6;
            case "Hoan Lăng Ngoa":
                return 6;
            case "Đằng Giao Ngoa":
                return 6;
            case "Khởi Phượng Ngoa":
                return 6;
            default:
                return 0;
        }
    }

    public int GetOpKX(string name)
    {
        switch (name)
        {
            case "Sinh lực tối đa":
                return 35;
            case "Tốc độ xuất chiêu":
                return 46;
            case "Tốc độ di chuyển":
                return 67;
            case "Tăng Ngoại Công":
                return 91;
            case "Nội kích":
                return 92;
            case "Sức Mạnh":
                return 144;
            case "Nội Lực":
                return 145;
            case "Gân Côt":
                return 146;
            case "Thân Pháp":
                return 147;
            case "Linh Hoạt":
                return 148;
            case "Xác xuất giảm nửa":
                return 181;
            case "Khởi Phượng Phi Phon":
                return 6;
            case "Tăng ngoại phòng":
                return 342;
            case "Tăng nội phòng":
                return 500;
            case "Điểm bạo kích":
                return 531;
            case "Phòng thủ bạo kích":
                return 532;
            case "Giảm thương bạo kích":
                return 535;
            case "Tăng Nội Ngoại Công":
                return 6029403;
            default:
                return 0;
        }
    }

    private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        d1 = comboBox2.Text;
    }

    private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        d2 = comboBox3.Text;
    }

    private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
    {
        d3 = comboBox4.Text;
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
  //      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(auto.TayKX5));
        comboBox1 = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        button1 = new System.Windows.Forms.Button();
        checkBox1 = new System.Windows.Forms.CheckBox();
        checkBox2 = new System.Windows.Forms.CheckBox();
        numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        numericUpDown2 = new System.Windows.Forms.NumericUpDown();
        label4 = new System.Windows.Forms.Label();
        numericUpDown3 = new System.Windows.Forms.NumericUpDown();
        button2 = new System.Windows.Forms.Button();
        trackBar1 = new System.Windows.Forms.TrackBar();
        comboBox2 = new System.Windows.Forms.ComboBox();
        comboBox3 = new System.Windows.Forms.ComboBox();
        comboBox4 = new System.Windows.Forms.ComboBox();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
        SuspendLayout();
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(83, 12);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(172, 21);
        comboBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 15);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(65, 13);
        label1.TabIndex = 1;
        label1.Text = "Tên Kim Xà:";
        button1.Location = new System.Drawing.Point(261, 10);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(59, 23);
        button1.TabIndex = 2;
        button1.Text = "Get";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(15, 118);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(68, 17);
        checkBox1.TabIndex = 3;
        checkBox1.Text = "Tẩy Tâm";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += new System.EventHandler(CheckBox1_CheckedChanged);
        checkBox2.AutoSize = true;
        checkBox2.Location = new System.Drawing.Point(111, 118);
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new System.Drawing.Size(70, 17);
        checkBox2.TabIndex = 4;
        checkBox2.Text = "Luyện Lô";
        checkBox2.UseVisualStyleBackColor = true;
        checkBox2.CheckedChanged += new System.EventHandler(CheckBox2_CheckedChanged);
        numericUpDown1.Location = new System.Drawing.Point(83, 39);
        numericUpDown1.Maximum = new decimal(new int[4]
        {
            100000,
            0,
            0,
            0
        });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new System.Drawing.Size(120, 20);
        numericUpDown1.TabIndex = 5;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(12, 41);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(45, 13);
        label2.TabIndex = 6;
        label2.Text = "Dòng 1:";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(12, 67);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(45, 13);
        label3.TabIndex = 8;
        label3.Text = "Dòng 2:";
        numericUpDown2.Location = new System.Drawing.Point(83, 65);
        numericUpDown2.Maximum = new decimal(new int[4]
        {
            100000,
            0,
            0,
            0
        });
        numericUpDown2.Name = "numericUpDown2";
        numericUpDown2.Size = new System.Drawing.Size(120, 20);
        numericUpDown2.TabIndex = 7;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(12, 93);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(45, 13);
        label4.TabIndex = 10;
        label4.Text = "Dòng 3:";
        numericUpDown3.Location = new System.Drawing.Point(83, 91);
        numericUpDown3.Maximum = new decimal(new int[4]
        {
            100000,
            0,
            0,
            0
        });
        numericUpDown3.Name = "numericUpDown3";
        numericUpDown3.Size = new System.Drawing.Size(120, 20);
        numericUpDown3.TabIndex = 9;
        button2.Location = new System.Drawing.Point(245, 114);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 11;
        button2.Text = "Start";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        trackBar1.Location = new System.Drawing.Point(15, 141);
        trackBar1.Maximum = 5;
        trackBar1.Name = "trackBar1";
        trackBar1.Size = new System.Drawing.Size(328, 45);
        trackBar1.TabIndex = 12;
        trackBar1.Value = 3;
        trackBar1.Scroll += new System.EventHandler(TrackBar1_Scroll);
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(209, 38);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(134, 21);
        comboBox2.TabIndex = 13;
        comboBox2.SelectedIndexChanged += new System.EventHandler(ComboBox2_SelectedIndexChanged);
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(209, 64);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(134, 21);
        comboBox3.TabIndex = 14;
        comboBox3.SelectedIndexChanged += new System.EventHandler(ComboBox3_SelectedIndexChanged);
        comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox4.FormattingEnabled = true;
        comboBox4.Location = new System.Drawing.Point(209, 90);
        comboBox4.Name = "comboBox4";
        comboBox4.Size = new System.Drawing.Size(134, 21);
        comboBox4.TabIndex = 15;
        comboBox4.SelectedIndexChanged += new System.EventHandler(ComboBox4_SelectedIndexChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(355, 167);
        base.Controls.Add(comboBox4);
        base.Controls.Add(comboBox3);
        base.Controls.Add(comboBox2);
        base.Controls.Add(trackBar1);
        base.Controls.Add(button2);
        base.Controls.Add(label4);
        base.Controls.Add(numericUpDown3);
        base.Controls.Add(label3);
        base.Controls.Add(numericUpDown2);
        base.Controls.Add(label2);
        base.Controls.Add(numericUpDown1);
        base.Controls.Add(checkBox2);
        base.Controls.Add(checkBox1);
        base.Controls.Add(button1);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox1);
      
        base.Name = "TayKX5";
        Text = "TayKX5";
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
        ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
