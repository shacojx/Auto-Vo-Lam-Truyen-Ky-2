using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using auto;

public class TrongCay : Form
{
    private Player player;

    private Player.TrongCayinfo trongcay = new Player.TrongCayinfo();

    private IContainer components = null;

    private CheckBox checkBox1;

    private CheckBox checkBox2;

    private CheckBox checkBox3;

    private Button button1;

    public TrongCay(Client client)
    {
        InitializeComponent();
        player = client.player;
        if (client.IsChecked)
        {
            checkBox1.Checked = player.TrongCaylist.HG;
            checkBox2.Checked = player.TrongCaylist.BNN;
            checkBox3.Checked = player.TrongCaylist.BNL;
        }
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked)
        {
            trongcay.HG = true;
        }
        else
        {
            trongcay.HG = false;
        }
    }

    private void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox2.Checked)
        {
            trongcay.BNN = true;
        }
        else
        {
            trongcay.BNN = false;
        }
    }

    private void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox3.Checked)
        {
            trongcay.BNL = true;
        }
        else
        {
            trongcay.BNL = false;
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        player.TrongCaylist = trongcay;
        Close();
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
        checkBox1 = new System.Windows.Forms.CheckBox();
        checkBox2 = new System.Windows.Forms.CheckBox();
        checkBox3 = new System.Windows.Forms.CheckBox();
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(6, 12);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(72, 17);
        checkBox1.TabIndex = 0;
        checkBox1.Text = "Hạt giống";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += new System.EventHandler(CheckBox1_CheckedChanged);
        checkBox2.AutoSize = true;
        checkBox2.Location = new System.Drawing.Point(6, 35);
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new System.Drawing.Size(107, 17);
        checkBox2.TabIndex = 1;
        checkBox2.Text = "Cây Bát Nhã nhỏ";
        checkBox2.UseVisualStyleBackColor = true;
        checkBox2.CheckedChanged += new System.EventHandler(CheckBox2_CheckedChanged);
        checkBox3.AutoSize = true;
        checkBox3.Location = new System.Drawing.Point(6, 58);
        checkBox3.Name = "checkBox3";
        checkBox3.Size = new System.Drawing.Size(103, 17);
        checkBox3.TabIndex = 2;
        checkBox3.Text = "Cây Bát Nhã lớn";
        checkBox3.UseVisualStyleBackColor = true;
        checkBox3.CheckedChanged += new System.EventHandler(CheckBox3_CheckedChanged);
        button1.Location = new System.Drawing.Point(60, 86);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 3;
        button1.Text = "Save";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(214, 121);
        base.Controls.Add(button1);
        base.Controls.Add(checkBox3);
        base.Controls.Add(checkBox2);
        base.Controls.Add(checkBox1);
        base.Name = "TrongCay";
        Text = "TrongCay";
        ResumeLayout(false);
        PerformLayout();
    }
}
