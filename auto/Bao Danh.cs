using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using auto;

public class BaoDanh : Form
{
    private Player player;

    private IContainer components = null;

    private CheckBox checkBox1;

    private CheckBox checkBox2;

    private ComboBox comboBox1;

    private Label label1;

    private ComboBox comboBox2;

    public BaoDanh(Client client)
    {
        InitializeComponent();
        player = client.player;
        if (client.IsChecked)
        {
            checkBox1.Checked = player.isDBCTC;
            checkBox2.Checked = !checkBox1.Checked;
            string[] array = new string[4]
            {
                "Tiểu Phương",
                "Mộ Binh Quan phe Tống",
                "Tiểu Ngọc",
                "Mộ Binh Quan phe Liêu"
            };
            ComboBox.ObjectCollection items = comboBox1.Items;
            object[] items2 = array;
            items.AddRange(items2);
            comboBox1.SelectedIndex = 0;
            string[] array2 = new string[3]
            {
                "Thôn Trang",
                "Tài Nguyên",
                "Pháo Đài"
            };
            ComboBox.ObjectCollection items3 = comboBox2.Items;
            items2 = array2;
            items3.AddRange(items2);
            comboBox2.SelectedIndex = 0;
        }
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked)
        {
            player.isDBCTC = true;
            checkBox2.Checked = false;
            comboBox2.Enabled = false;
        }
    }

    private void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox2.Checked)
        {
            player.isDBCTC = false;
            checkBox1.Checked = false;
            comboBox2.Enabled = true;
        }
    }

    private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        player.NPCBB_Name = comboBox1.Text;
    }

    private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        player.CTP_Name = comboBox2.Text;
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
        comboBox1 = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        comboBox2 = new System.Windows.Forms.ComboBox();
        SuspendLayout();
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(11, 52);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(122, 17);
        checkBox1.TabIndex = 0;
        checkBox1.Text = "Chiến Trường Chính";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += new System.EventHandler(CheckBox1_CheckedChanged);
        checkBox2.AutoSize = true;
        checkBox2.Location = new System.Drawing.Point(11, 85);
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new System.Drawing.Size(112, 17);
        checkBox2.TabIndex = 1;
        checkBox2.Text = "Chiến Trường Phụ";
        checkBox2.UseVisualStyleBackColor = true;
        checkBox2.CheckedChanged += new System.EventHandler(CheckBox2_CheckedChanged);
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(41, 12);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(166, 21);
        comboBox1.TabIndex = 2;
        comboBox1.SelectedIndexChanged += new System.EventHandler(ComboBox1_SelectedIndexChanged);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(6, 15);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 13);
        label1.TabIndex = 3;
        label1.Text = "NPC";
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.Enabled = false;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(135, 83);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(123, 21);
        comboBox2.TabIndex = 4;
        comboBox2.SelectedIndexChanged += new System.EventHandler(ComboBox2_SelectedIndexChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(270, 116);
        base.Controls.Add(comboBox2);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox1);
        base.Controls.Add(checkBox2);
        base.Controls.Add(checkBox1);
        base.Name = "BaoDanh";
        Text = "BaoDanh";
        ResumeLayout(false);
        PerformLayout();
    }
}
