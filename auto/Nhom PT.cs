using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using auto;

public class TaoNhom : Form
{
    private Player player;

    private IContainer components = null;

    private ListView listView2;

    private ColumnHeader danhsachnhom;

    private ListView listView1;

    private ColumnHeader npc_name;

    private ContextMenuStrip contextMenuStrip1;

    private ContextMenuStrip contextMenuStrip2;

    private ToolStripMenuItem getToolStripMenuItem;

    private ToolStripMenuItem deleteToolStripMenuItem;

    private ToolStripMenuItem deleteAllToolStripMenuItem;

    private CheckBox checkBox1;

    public TaoNhom(Client client)
    {
        InitializeComponent();
        player = client.player;
        if (!client.IsChecked)
        {
            return;
        }
        if (player.TaoNhomlist != null)
        {
            foreach (Player.NPCinfo item in player.TaoNhomlist)
            {
                ListViewItem value = new ListViewItem(new string[1]
                {
                    item.Name
                });
                listView2.Items.Add(value);
            }
        }
        else
        {
            player.TaoNhomlist = new List<Player.NPCinfo>();
        }
        checkBox1.Checked = player.isTruongNhom;
    }

    private void GetToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView1.Items.Clear();
        foreach (Player.NPCinfo nPC in AutoClient.GetNPCList(player.HProcess))
        {
            if (nPC.status == 5)
            {
                ListViewItem value = new ListViewItem(new string[1]
                {
                    nPC.Name
                });
                listView1.Items.Add(value);
            }
        }
    }

    private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listView2.SelectedItems.Count <= 0)
        {
            return;
        }
        Player.NPCinfo item = player.TaoNhomlist.FirstOrDefault((Player.NPCinfo x) => x.Name == listView2.SelectedItems[0].SubItems[0].Text);
        player.TaoNhomlist.Remove(item);
        listView2.Items.Clear();
        foreach (Player.NPCinfo item2 in player.TaoNhomlist)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                item2.Name
            });
            listView2.Items.Add(value);
        }
    }

    private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView2.Items.Clear();
        player.TaoNhomlist.Clear();
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        player.isTruongNhom = checkBox1.Checked;
    }

    private void ListView1_DoubleClick(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems.Count < 2)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                listView1.SelectedItems[0].SubItems[0].Text
            });
            if (!player.TaoNhomlist.Exists((Player.NPCinfo x) => x.Name == listView1.SelectedItems[0].SubItems[0].Text))
            {
                listView2.Items.Add(value);
                player.TaoNhomlist.Add(new Player.NPCinfo
                {
                    Name = listView1.SelectedItems[0].SubItems[0].Text
                });
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
        components = new System.ComponentModel.Container();
        listView2 = new System.Windows.Forms.ListView();
        danhsachnhom = new System.Windows.Forms.ColumnHeader();
        listView1 = new System.Windows.Forms.ListView();
        npc_name = new System.Windows.Forms.ColumnHeader();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
        getToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        checkBox1 = new System.Windows.Forms.CheckBox();
        contextMenuStrip1.SuspendLayout();
        contextMenuStrip2.SuspendLayout();
        SuspendLayout();
        listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            danhsachnhom
        });
        listView2.ContextMenuStrip = contextMenuStrip2;
        listView2.GridLines = true;
        listView2.Location = new System.Drawing.Point(218, 7);
        listView2.Name = "listView2";
        listView2.Size = new System.Drawing.Size(205, 201);
        listView2.TabIndex = 3;
        listView2.UseCompatibleStateImageBehavior = false;
        listView2.View = System.Windows.Forms.View.Details;
        danhsachnhom.Text = "Danh Sách PT";
        danhsachnhom.Width = 201;
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            npc_name
        });
        listView1.ContextMenuStrip = contextMenuStrip1;
        listView1.GridLines = true;
        listView1.Location = new System.Drawing.Point(7, 7);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(205, 201);
        listView1.TabIndex = 2;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        listView1.DoubleClick += new System.EventHandler(ListView1_DoubleClick);
        npc_name.Text = "Name";
        npc_name.Width = 201;
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
        {
            getToolStripMenuItem
        });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
        contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
        {
            deleteToolStripMenuItem,
            deleteAllToolStripMenuItem
        });
        contextMenuStrip2.Name = "contextMenuStrip2";
        contextMenuStrip2.Size = new System.Drawing.Size(122, 48);
        getToolStripMenuItem.Name = "getToolStripMenuItem";
        getToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        getToolStripMenuItem.Text = "Get";
        getToolStripMenuItem.Click += new System.EventHandler(GetToolStripMenuItem_Click);
        deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        deleteToolStripMenuItem.Text = "Delete";
        deleteToolStripMenuItem.Click += new System.EventHandler(DeleteToolStripMenuItem_Click);
        deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
        deleteAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        deleteAllToolStripMenuItem.Text = "DeleteAll";
        deleteAllToolStripMenuItem.Click += new System.EventHandler(DeleteAllToolStripMenuItem_Click);
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(7, 214);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(91, 17);
        checkBox1.TabIndex = 6;
        checkBox1.Text = "Trưởng Nhóm";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += new System.EventHandler(CheckBox1_CheckedChanged);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(431, 243);
        base.Controls.Add(checkBox1);
        base.Controls.Add(listView2);
        base.Controls.Add(listView1);
        base.Name = "TaoNhom";
        Text = "TaoNhom";
        contextMenuStrip1.ResumeLayout(false);
        contextMenuStrip2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
