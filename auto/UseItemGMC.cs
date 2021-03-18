using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using auto;

public class UseGmItem : Form
{
    private Player player;

    private IntPtr Module;

    private List<Player.Item> listItem = new List<Player.Item>();

    public static string[] NameDong = new string[21]
    {
        "Không Sử Dụng",
        "Dòng 1",
        "Dòng 2",
        "Dòng 3",
        "Dòng 4",
        "Dòng 5",
        "Dòng 6",
        "Dòng 7",
        "Dòng 8",
        "Dòng 9",
        "Dòng 10",
        "Dòng 11",
        "Dòng 12",
        "Dòng 13",
        "Dòng 14",
        "Dòng 15",
        "Dòng 16",
        "Dòng 17",
        "Dòng 18",
        "Dòng 19",
        "Dòng 20"
    };

    private IContainer components = null;

    private ComboBox comboBox1;

    private Label label1;

    private Label label2;

    private ComboBox comboBox2;

    private Button button1;

    private Label label3;

    private ComboBox comboBox3;

    private Label label4;

    private ComboBox comboBox4;

    private Label label5;

    private ComboBox comboBox5;

    private Label label6;

    private ComboBox comboBox6;

    private Label label7;

    private ComboBox comboBox7;

    private NumericUpDown numericUpDown1;

    private Label label8;

    private Button button2;

    public UseGmItem(Client client)
    {
        InitializeComponent();
        player = client.player;
        Module = client.Module;
        if (client.IsChecked)
        {
            ComboBox.ObjectCollection items = comboBox2.Items;
            object[] nameDong = NameDong;
            items.AddRange(nameDong);
            ComboBox.ObjectCollection items2 = comboBox3.Items;
            nameDong = NameDong;
            items2.AddRange(nameDong);
            ComboBox.ObjectCollection items3 = comboBox4.Items;
            nameDong = NameDong;
            items3.AddRange(nameDong);
            ComboBox.ObjectCollection items4 = comboBox5.Items;
            nameDong = NameDong;
            items4.AddRange(nameDong);
            ComboBox.ObjectCollection items5 = comboBox6.Items;
            nameDong = NameDong;
            items5.AddRange(nameDong);
            ComboBox.ObjectCollection items6 = comboBox7.Items;
            nameDong = NameDong;
            items6.AddRange(nameDong);
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        comboBox1.Items.Clear();
        listItem.Clear();
        foreach (Player.Item item in from x in AutoClient.GetItemList(player.HProcess)
                                     orderby x.id descending
                                     select x)
        {
            if (item.type == 2)
            {
                comboBox1.Items.Add(item.Name);
                listItem.Add(item);
            }
        }
        comboBox1.SelectedIndex = 0;
    }

    private void UseGmItem_Load(object sender, EventArgs e)
    {
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < (int)numericUpDown1.Value; i++)
        {
            Player.Item item = listItem.FirstOrDefault((Player.Item x) => x.Name == comboBox1.Text);
            HookCall.UseItem(player.hWnd, item.cot, item.hang);
            Thread.Sleep(20);
            if (comboBox2.Text != "Không Sử Dụng")
            {
                string s = comboBox2.Text.Split(' ')[1];
                int line = int.Parse(s) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
            if (comboBox3.Text != "Không Sử Dụng")
            {
                string s2 = comboBox3.Text.Split(' ')[1];
                int line2 = int.Parse(s2) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line2, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
            if (comboBox4.Text != "Không Sử Dụng")
            {
                string s3 = comboBox4.Text.Split(' ')[1];
                int line3 = int.Parse(s3) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line3, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
            if (comboBox5.Text != "Không Sử Dụng")
            {
                string s4 = comboBox5.Text.Split(' ')[1];
                int line4 = int.Parse(s4) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line4, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
            if (comboBox6.Text != "Không Sử Dụng")
            {
                string s5 = comboBox6.Text.Split(' ')[1];
                int line5 = int.Parse(s5) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line5, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
            if (comboBox7.Text != "Không Sử Dụng")
            {
                string s6 = comboBox7.Text.Split(' ')[1];
                int line6 = int.Parse(s6) - 1;
                HookCall.SelectLineMenu(player.hWnd, (uint)line6, AutoClient.MenuID(player.HProcess));
                Thread.Sleep(20);
            }
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
       // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(auto.UseGmItem));
        comboBox1 = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        comboBox2 = new System.Windows.Forms.ComboBox();
        button1 = new System.Windows.Forms.Button();
        label3 = new System.Windows.Forms.Label();
        comboBox3 = new System.Windows.Forms.ComboBox();
        label4 = new System.Windows.Forms.Label();
        comboBox4 = new System.Windows.Forms.ComboBox();
        label5 = new System.Windows.Forms.Label();
        comboBox5 = new System.Windows.Forms.ComboBox();
        label6 = new System.Windows.Forms.Label();
        comboBox6 = new System.Windows.Forms.ComboBox();
        label7 = new System.Windows.Forms.Label();
        comboBox7 = new System.Windows.Forms.ComboBox();
        numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        label8 = new System.Windows.Forms.Label();
        button2 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        SuspendLayout();
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(78, 9);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(149, 21);
        comboBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 39);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(34, 13);
        label1.TabIndex = 1;
        label1.Text = "Lần 1";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(12, 12);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(27, 13);
        label2.TabIndex = 3;
        label2.Text = "Item";
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(78, 36);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(149, 21);
        comboBox2.TabIndex = 2;
        button1.Location = new System.Drawing.Point(245, 7);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 4;
        button1.Text = "Get";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(12, 66);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(34, 13);
        label3.TabIndex = 6;
        label3.Text = "Lần 2";
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(78, 63);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(149, 21);
        comboBox3.TabIndex = 5;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(12, 93);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(34, 13);
        label4.TabIndex = 8;
        label4.Text = "Lần 3";
        comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox4.FormattingEnabled = true;
        comboBox4.Location = new System.Drawing.Point(78, 90);
        comboBox4.Name = "comboBox4";
        comboBox4.Size = new System.Drawing.Size(149, 21);
        comboBox4.TabIndex = 7;
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(12, 120);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(34, 13);
        label5.TabIndex = 10;
        label5.Text = "Lần 4";
        comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox5.FormattingEnabled = true;
        comboBox5.Location = new System.Drawing.Point(78, 117);
        comboBox5.Name = "comboBox5";
        comboBox5.Size = new System.Drawing.Size(149, 21);
        comboBox5.TabIndex = 9;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(12, 147);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(34, 13);
        label6.TabIndex = 12;
        label6.Text = "Lần 5";
        comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox6.FormattingEnabled = true;
        comboBox6.Location = new System.Drawing.Point(78, 144);
        comboBox6.Name = "comboBox6";
        comboBox6.Size = new System.Drawing.Size(149, 21);
        comboBox6.TabIndex = 11;
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(12, 174);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(34, 13);
        label7.TabIndex = 14;
        label7.Text = "Lần 6";
        comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox7.FormattingEnabled = true;
        comboBox7.Location = new System.Drawing.Point(78, 171);
        comboBox7.Name = "comboBox7";
        comboBox7.Size = new System.Drawing.Size(149, 21);
        comboBox7.TabIndex = 13;
        numericUpDown1.Location = new System.Drawing.Point(242, 59);
        numericUpDown1.Minimum = new decimal(new int[4]
        {
            1,
            0,
            0,
            0
        });
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new System.Drawing.Size(75, 20);
        numericUpDown1.TabIndex = 15;
        numericUpDown1.Value = new decimal(new int[4]
        {
            1,
            0,
            0,
            0
        });
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(247, 36);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(70, 13);
        label8.TabIndex = 16;
        label8.Text = "Lần Sử Dụng";
        button2.Location = new System.Drawing.Point(242, 93);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 17;
        button2.Text = "Start";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(329, 201);
        base.Controls.Add(button2);
        base.Controls.Add(label8);
        base.Controls.Add(numericUpDown1);
        base.Controls.Add(label7);
        base.Controls.Add(comboBox7);
        base.Controls.Add(label6);
        base.Controls.Add(comboBox6);
        base.Controls.Add(label5);
        base.Controls.Add(comboBox5);
        base.Controls.Add(label4);
        base.Controls.Add(comboBox4);
        base.Controls.Add(label3);
        base.Controls.Add(comboBox3);
        base.Controls.Add(button1);
        base.Controls.Add(label2);
        base.Controls.Add(comboBox2);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox1);
      //  base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        base.Name = "UseGmItem";
        Text = "UseGmItem";
        base.Load += new System.EventHandler(UseGmItem_Load);
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
