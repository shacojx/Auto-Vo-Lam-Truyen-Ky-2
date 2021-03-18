using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using auto;

public class LuyenMT : Form
{
    private Player player;

    private List<Player.Item> listMT = new List<Player.Item>();

    private Thread othread;

    private bool isloop = true;

    private IntPtr Module;

    private IContainer components;

    private ListView listView1;

    private Button button1;

    private ColumnHeader MTName;

    private ColumnHeader Thanh;

    private ColumnHeader PhuLuc;

    private ColumnHeader d1;

    private ColumnHeader d2;

    private ColumnHeader d3;

    private ColumnHeader d4;

    private Button button2;

    private Button button3;

    private NumericUpDown numericUpDown1;

    private Label label1;

    private Label label2;

    private NumericUpDown numericUpDown2;

    private Label label3;

    private NumericUpDown numericUpDown3;

    private Label label4;

    private NumericUpDown numericUpDown4;

    private Label label5;

    private NumericUpDown numericUpDown5;

    private Label label6;

    private NumericUpDown numericUpDown6;

    private Button button4;

    public LuyenMT(Client client)
    {
        InitializeComponent();
        player = client.player;
        Module = client.Module;
        bool isChecked = client.IsChecked;
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        getMT();
    }

    private void getMT()
    {
        int[] array = new int[4]
        {
            (int)numericUpDown3.Value,
            (int)numericUpDown4.Value,
            (int)numericUpDown5.Value,
            (int)numericUpDown6.Value
        };
        int num = array[0];
        int num2 = 0;
        for (int i = 0; i < 4; i++)
        {
            if (array[i] > num)
            {
                num = array[i];
                num2 = i;
            }
        }
        if (array[0] == 0 && array[1] == 0 && array[2] == 0 && array[3] == 0)
        {
            num2 = 4;
        }
        listView1.Items.Clear();
        listMT.Clear();
        List<Player.Item> list = new List<Player.Item>();
        switch (num2)
        {
            case 0:
                list.AddRange(from x in AutoClient.GetItemList(player.HProcess)
                              orderby x.MTdong1 descending
                              select x);
                break;
            case 1:
                list.AddRange(from x in AutoClient.GetItemList(player.HProcess)
                              orderby x.MTdong2 descending
                              select x);
                break;
            case 2:
                list.AddRange(from x in AutoClient.GetItemList(player.HProcess)
                              orderby x.MTdong3 descending
                              select x);
                break;
            case 3:
                list.AddRange(from x in AutoClient.GetItemList(player.HProcess)
                              orderby x.MTdong4 descending
                              select x);
                break;
            default:
                list.AddRange(from x in AutoClient.GetItemList(player.HProcess)
                              orderby x.phuluc descending
                              select x);
                break;
        }
        foreach (Player.Item item in list)
        {
            if (int.Parse(item.phuluc) != 0 && int.Parse(item.phuluc) < 5 && item.type == 2)
            {
                ListViewItem value = new ListViewItem(new string[7]
                {
                    item.Name,
                    item.thanh,
                    item.phuluc,
                    item.MTdong1,
                    item.MTdong2,
                    item.MTdong3,
                    item.MTdong4
                });
                listView1.Items.Add(value);
                listMT.Add(item);
            }
        }
    }

    private void BanMT()
    {
        List<Player.Item> list = new List<Player.Item>();
        do
        {
            foreach (Player.Item item in listMT)
            {
                if (int.Parse(item.thanh) >= (int)numericUpDown1.Value && int.Parse(item.phuluc) >= (int)numericUpDown2.Value && int.Parse(item.MTdong1) >= (int)numericUpDown3.Value && int.Parse(item.MTdong2) >= (int)numericUpDown4.Value && int.Parse(item.MTdong3) >= (int)numericUpDown5.Value && int.Parse(item.MTdong4) >= (int)numericUpDown6.Value)
                {
                    button4.Invoke((MethodInvoker)delegate
                    {
                        button4.Text = "Auto Luyện";
                    });
                    isloop = false;
                    do
                    {
                        HookCall.CloseMenu(player.hWnd);
                        Thread.Sleep(100);
                    }
                    while (AutoClient.BaseMenu(player.HProcess) != 0);
                    return;
                }
                list.Add(item);
            }
            foreach (Player.Item item2 in list)
            {
                HookCall.BanItem(player.hWnd, item2.id);
            }
            list.Clear();
            listMT.Clear();
            foreach (Player.Item item3 in AutoClient.GetItemList(player.HProcess))
            {
                if (int.Parse(item3.phuluc) != 0 && int.Parse(item3.phuluc) < 5 && item3.type == 2)
                {
                    listMT.Add(item3);
                }
            }
        }
        while (listMT.Count > 0);
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        if (listMT.Count == 0)
        {
            return;
        }
        foreach (Player.Item item in listMT)
        {
            HookCall.BanItem(player.hWnd, item.id);
        }
        listMT.Clear();
        listView1.Items.Clear();
    }

    private void Button3_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < 59; i++)
        {
            HookCall.UseItem(player.hWnd, 0, 0);
            Thread.Sleep(20);
            HookCall.SelectLineMenu(player.hWnd, 8u, AutoClient.MenuID(player.HProcess));
            Thread.Sleep(20);
            HookCall.SelectLineMenu(player.hWnd, 3u, AutoClient.MenuID(player.HProcess));
            Thread.Sleep(20);
        }
        for (int j = 0; j < 6; j++)
        {
            for (int k = 0; k < 12; k++)
            {
                if (j != 0 || k != 0)
                {
                    HookCall.UseItem(player.hWnd, j, k);
                    Thread.Sleep(20);
                    HookCall.UseItem(player.hWnd, 0, 0);
                    Thread.Sleep(20);
                    HookCall.SelectLineMenu(player.hWnd, 8u, AutoClient.MenuID(player.HProcess));
                    Thread.Sleep(20);
                    HookCall.SelectLineMenu(player.hWnd, 3u, AutoClient.MenuID(player.HProcess));
                    Thread.Sleep(20);
                }
            }
        }
        for (int l = 0; l < 20; l++)
        {
            HookCall.CloseMenu(player.hWnd);
        }
        getMT();
    }

    private void Button4_Click(object sender, EventArgs e)
    {
        if (button4.Text == "Auto Luyện")
        {
            button4.Text = "Stop";
            othread = new Thread(ThreadLuyenMT);
            othread.Start();
            return;
        }
        button4.Text = "Auto Luyện";
        othread.Abort();
        do
        {
            HookCall.CloseMenu(player.hWnd);
            Thread.Sleep(100);
        }
        while (AutoClient.BaseMenu(player.HProcess) != 0);
    }

    private void ThreadLuyenMT()
    {
        isloop = true;
        while (isloop)
        {
            for (int i = 0; i < 59; i++)
            {
                HookCall.UseItem(player.hWnd, 0, 0);
                Thread.Sleep(20);
                HookCall.SelectLineMenu(player.hWnd, 8, AutoClient.MenuID(player.HProcess));//chọn dòng - 8
                Thread.Sleep(20);
                HookCall.SelectLineMenu(player.hWnd, 0, AutoClient.MenuID(player.HProcess));// chọn dòng thứ 2 - 0
                Thread.Sleep(20);
            }
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 12; k++)
                {
                    if (j != 0 || k != 0)
                    {
                        HookCall.UseItem(player.hWnd, j, k);
                        Thread.Sleep(20);
                        HookCall.UseItem(player.hWnd, 0, 0);
                        Thread.Sleep(20);
                        HookCall.SelectLineMenu(player.hWnd, 8u, AutoClient.MenuID(player.HProcess));
                        Thread.Sleep(20);
                        HookCall.SelectLineMenu(player.hWnd, 3u, AutoClient.MenuID(player.HProcess));
                        Thread.Sleep(20);
                    }
                }
            }
            listView1.Invoke((MethodInvoker)delegate
            {
                listView1.Items.Clear();
                getMT();
            });
            BanMT();
        }
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
        listView1 = new System.Windows.Forms.ListView();
        MTName = new System.Windows.Forms.ColumnHeader();
        Thanh = new System.Windows.Forms.ColumnHeader();
        PhuLuc = new System.Windows.Forms.ColumnHeader();
        d1 = new System.Windows.Forms.ColumnHeader();
        d2 = new System.Windows.Forms.ColumnHeader();
        d3 = new System.Windows.Forms.ColumnHeader();
        d4 = new System.Windows.Forms.ColumnHeader();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        numericUpDown2 = new System.Windows.Forms.NumericUpDown();
        label3 = new System.Windows.Forms.Label();
        numericUpDown3 = new System.Windows.Forms.NumericUpDown();
        label4 = new System.Windows.Forms.Label();
        numericUpDown4 = new System.Windows.Forms.NumericUpDown();
        label5 = new System.Windows.Forms.Label();
        numericUpDown5 = new System.Windows.Forms.NumericUpDown();
        label6 = new System.Windows.Forms.Label();
        numericUpDown6 = new System.Windows.Forms.NumericUpDown();
        button4 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
        SuspendLayout();
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[7]
        {
            MTName,
            Thanh,
            PhuLuc,
            d1,
            d2,
            d3,
            d4
        });
        listView1.FullRowSelect = true;
        listView1.GridLines = true;
        listView1.Location = new System.Drawing.Point(3, 2);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(512, 582);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        MTName.Text = "Name";
        MTName.Width = 169;
        Thanh.Text = "Thành";
        Thanh.Width = 44;
        PhuLuc.Text = "Phụ Lục";
        PhuLuc.Width = 55;
        d1.Text = "Dòng 1";
        d2.Text = "Dòng 2";
        d3.Text = "Dòng 3";
        d4.Text = "Dòng 4";
        button1.Location = new System.Drawing.Point(521, 12);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 1;
        button1.Text = "Get";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        button2.Location = new System.Drawing.Point(521, 41);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 2;
        button2.Text = "Bán Tất";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        button3.Location = new System.Drawing.Point(521, 92);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 3;
        button3.Text = "Luyện";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(Button3_Click);
        numericUpDown1.Location = new System.Drawing.Point(47, 590);
        numericUpDown1.Maximum = new decimal(new int[4]
        {
            10,
            0,
            0,
            0
        });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new System.Drawing.Size(38, 20);
        numericUpDown1.TabIndex = 4;
        numericUpDown1.Value = new decimal(new int[4]
        {
            10,
            0,
            0,
            0
        });
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(0, 592);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(41, 13);
        label1.TabIndex = 5;
        label1.Text = "Thành:";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(86, 592);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(50, 13);
        label2.TabIndex = 7;
        label2.Text = "Phụ Lục:";
        numericUpDown2.Location = new System.Drawing.Point(142, 590);
        numericUpDown2.Maximum = new decimal(new int[4]
        {
            4,
            0,
            0,
            0
        });
        numericUpDown2.Name = "numericUpDown2";
        numericUpDown2.Size = new System.Drawing.Size(34, 20);
        numericUpDown2.TabIndex = 6;
        numericUpDown2.Value = new decimal(new int[4]
        {
            4,
            0,
            0,
            0
        });
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(181, 592);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(24, 13);
        label3.TabIndex = 9;
        label3.Text = "D1:";
        numericUpDown3.Location = new System.Drawing.Point(211, 590);
        numericUpDown3.Name = "numericUpDown3";
        numericUpDown3.Size = new System.Drawing.Size(46, 20);
        numericUpDown3.TabIndex = 8;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(264, 592);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(24, 13);
        label4.TabIndex = 11;
        label4.Text = "D2:";
        numericUpDown4.Location = new System.Drawing.Point(294, 590);
        numericUpDown4.Name = "numericUpDown4";
        numericUpDown4.Size = new System.Drawing.Size(46, 20);
        numericUpDown4.TabIndex = 10;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(428, 592);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(24, 13);
        label5.TabIndex = 15;
        label5.Text = "D4:";
        numericUpDown5.Location = new System.Drawing.Point(375, 590);
        numericUpDown5.Name = "numericUpDown5";
        numericUpDown5.Size = new System.Drawing.Size(46, 20);
        numericUpDown5.TabIndex = 14;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(345, 592);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(24, 13);
        label6.TabIndex = 13;
        label6.Text = "D3:";
        numericUpDown6.Location = new System.Drawing.Point(458, 590);
        numericUpDown6.Name = "numericUpDown6";
        numericUpDown6.Size = new System.Drawing.Size(46, 20);
        numericUpDown6.TabIndex = 12;
        button4.Location = new System.Drawing.Point(521, 587);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 16;
        button4.Text = "Auto Luyện";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(Button4_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(602, 620);
        base.Controls.Add(button4);
        base.Controls.Add(label5);
        base.Controls.Add(numericUpDown5);
        base.Controls.Add(label6);
        base.Controls.Add(numericUpDown6);
        base.Controls.Add(label4);
        base.Controls.Add(numericUpDown4);
        base.Controls.Add(label3);
        base.Controls.Add(numericUpDown3);
        base.Controls.Add(label2);
        base.Controls.Add(numericUpDown2);
        base.Controls.Add(label1);
        base.Controls.Add(numericUpDown1);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(listView1);
        base.Name = "LuyenMT";
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
