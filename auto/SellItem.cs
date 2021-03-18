using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using auto;

public class Sellitem :Form
{
    private Player player;

    private IntPtr Module;

    private IContainer components = null;

    private ListView listView1;

    private ColumnHeader sellitem_name;

    private ListView listView2;

    private ColumnHeader sellitem_name1;

    private ListView listView3;

    private ColumnHeader sellitem_NPC;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem getItemToolStripMenuItem;

    private ContextMenuStrip contextMenuStrip2;

    private ToolStripMenuItem deleteAllToolStripMenuItem;

    private ContextMenuStrip contextMenuStrip3;

    private ToolStripMenuItem getToolStripMenuItem;

    private ListView listView4;

    private ColumnHeader columnHeader1;

    private ContextMenuStrip contextMenuStrip4;

    private ToolStripMenuItem deleteAllToolStripMenuItem1;

    private ToolStripMenuItem deleteToolStripMenuItem;

    public Sellitem(Client client)
    {
        InitializeComponent();
        player = client.player;
        Module = client.Module;
        if (!client.IsChecked)
        {
            return;
        }
        if (player.sellitemlist != null)
        {
            foreach (Player.Item item in player.sellitemlist)
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
            player.sellitemlist = new List<Player.Item>();
        }
        if (player.sellnpclist != null)
        {
            foreach (Player.NPCinfo item2 in player.sellnpclist)
            {
                ListViewItem value2 = new ListViewItem(new string[1]
                {
                    item2.Name
                });
                listView4.Items.Add(value2);
            }
        }
        else
        {
            player.sellnpclist = new List<Player.NPCinfo>();
        }
    }

    private void GetItemToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView1.Items.Clear();
        foreach (Player.Item item in AutoClient.GetItemList(player.HProcess))
        {
            if (item.type == 2 || item.type == 37)
            {
                ListViewItem value = new ListViewItem(new string[1]
                {
                    item.Name
                });
                listView1.Items.Add(value);
            }
        }
    }

    private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView2.Items.Clear();
        player.sellitemlist.Clear();
    }

    private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listView2.SelectedItems.Count <= 0)
        {
            return;
        }
        Player.Item item = player.sellitemlist.FirstOrDefault((Player.Item x) => x.Name == listView2.SelectedItems[0].SubItems[0].Text);
        player.sellitemlist.Remove(item);
        listView2.Items.Clear();
        foreach (Player.Item item2 in player.sellitemlist)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                item2.Name
            });
            listView2.Items.Add(value);
        }
    }

    private void GetToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView3.Items.Clear();
        foreach (Player.NPCinfo nPC in AutoClient.GetNPCList(player.HProcess))
        {
            if (nPC.status == 6)
            {
                ListViewItem value = new ListViewItem(new string[1]
                {
                    nPC.Name
                });
                listView3.Items.Add(value);
            }
        }
    }

    private void DeleteAllToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        listView4.Items.Clear();
        player.sellnpclist.Clear();
    }

    private void ListView1_DoubleClick(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                listView1.SelectedItems[0].SubItems[0].Text
            });
            if (!player.sellitemlist.Exists((Player.Item x) => x.Name == listView1.SelectedItems[0].SubItems[0].Text))
            {
                listView2.Items.Add(value);
                player.sellitemlist.Add(new Player.Item
                {
                    Name = listView1.SelectedItems[0].SubItems[0].Text
                });
            }
        }
    }

    private void ListView3_DoubleClick(object sender, EventArgs e)
    {
        if (listView3.SelectedItems.Count > 0)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                listView3.SelectedItems[0].SubItems[0].Text
            });
            if (!player.sellnpclist.Exists((Player.NPCinfo x) => x.Name == listView3.SelectedItems[0].SubItems[0].Text))
            {
                listView4.Items.Add(value);
                player.sellnpclist.Add(new Player.NPCinfo
                {
                    Name = listView3.SelectedItems[0].SubItems[0].Text
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
        listView1 = new System.Windows.Forms.ListView();
        sellitem_name = new System.Windows.Forms.ColumnHeader();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        getItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        listView2 = new System.Windows.Forms.ListView();
        sellitem_name1 = new System.Windows.Forms.ColumnHeader();
        contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
        deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        listView3 = new System.Windows.Forms.ListView();
        sellitem_NPC = new System.Windows.Forms.ColumnHeader();
        contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(components);
        getToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        listView4 = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(components);
        deleteAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        contextMenuStrip1.SuspendLayout();
        contextMenuStrip2.SuspendLayout();
        contextMenuStrip3.SuspendLayout();
        contextMenuStrip4.SuspendLayout();
        SuspendLayout();
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            sellitem_name
        });
        listView1.ContextMenuStrip = contextMenuStrip1;
        listView1.Location = new System.Drawing.Point(2, 8);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(205, 147);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        listView1.DoubleClick += new System.EventHandler(ListView1_DoubleClick);
        sellitem_name.Text = "Item Name";
        sellitem_name.Width = 201;
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
        {
            getItemToolStripMenuItem
        });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(120, 26);
        getItemToolStripMenuItem.Name = "getItemToolStripMenuItem";
        getItemToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
        getItemToolStripMenuItem.Text = "Get Item";
        getItemToolStripMenuItem.Click += new System.EventHandler(GetItemToolStripMenuItem_Click);
        listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            sellitem_name1
        });
        listView2.ContextMenuStrip = contextMenuStrip2;
        listView2.Location = new System.Drawing.Point(213, 8);
        listView2.Name = "listView2";
        listView2.Size = new System.Drawing.Size(205, 147);
        listView2.TabIndex = 1;
        listView2.UseCompatibleStateImageBehavior = false;
        listView2.View = System.Windows.Forms.View.Details;
        sellitem_name1.Text = "Sell Item Name ";
        sellitem_name1.Width = 201;
        contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
        {
            deleteToolStripMenuItem,
            deleteAllToolStripMenuItem
        });
        contextMenuStrip2.Name = "contextMenuStrip2";
        contextMenuStrip2.Size = new System.Drawing.Size(181, 70);
        deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
        deleteAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        deleteAllToolStripMenuItem.Text = "DeleteAll";
        deleteAllToolStripMenuItem.Click += new System.EventHandler(DeleteAllToolStripMenuItem_Click);
        listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            sellitem_NPC
        });
        listView3.ContextMenuStrip = contextMenuStrip3;
        listView3.Location = new System.Drawing.Point(2, 161);
        listView3.Name = "listView3";
        listView3.Size = new System.Drawing.Size(205, 147);
        listView3.TabIndex = 3;
        listView3.UseCompatibleStateImageBehavior = false;
        listView3.View = System.Windows.Forms.View.Details;
        listView3.DoubleClick += new System.EventHandler(ListView3_DoubleClick);
        sellitem_NPC.Text = "NPC Name";
        sellitem_NPC.Width = 201;
        contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
        {
            getToolStripMenuItem
        });
        contextMenuStrip3.Name = "contextMenuStrip3";
        contextMenuStrip3.Size = new System.Drawing.Size(93, 26);
        getToolStripMenuItem.Name = "getToolStripMenuItem";
        getToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
        getToolStripMenuItem.Text = "Get";
        getToolStripMenuItem.Click += new System.EventHandler(GetToolStripMenuItem_Click);
        listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1]
        {
            columnHeader1
        });
        listView4.ContextMenuStrip = contextMenuStrip4;
        listView4.Location = new System.Drawing.Point(213, 161);
        listView4.Name = "listView4";
        listView4.Size = new System.Drawing.Size(205, 147);
        listView4.TabIndex = 7;
        listView4.UseCompatibleStateImageBehavior = false;
        listView4.View = System.Windows.Forms.View.Details;
        columnHeader1.Text = "Sell NPC Name";
        columnHeader1.Width = 201;
        contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
        {
            deleteAllToolStripMenuItem1
        });
        contextMenuStrip4.Name = "contextMenuStrip4";
        contextMenuStrip4.Size = new System.Drawing.Size(122, 26);
        deleteAllToolStripMenuItem1.Name = "deleteAllToolStripMenuItem1";
        deleteAllToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
        deleteAllToolStripMenuItem1.Text = "DeleteAll";
        deleteAllToolStripMenuItem1.Click += new System.EventHandler(DeleteAllToolStripMenuItem1_Click);
        deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        deleteToolStripMenuItem.Text = "Delete";
        deleteToolStripMenuItem.Click += new System.EventHandler(DeleteToolStripMenuItem_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(426, 315);
        base.Controls.Add(listView4);
        base.Controls.Add(listView3);
        base.Controls.Add(listView2);
        base.Controls.Add(listView1);
        base.Name = "Sellitem";
        Text = "Sellitem";
        contextMenuStrip1.ResumeLayout(false);
        contextMenuStrip2.ResumeLayout(false);
        contextMenuStrip3.ResumeLayout(false);
        contextMenuStrip4.ResumeLayout(false);
        ResumeLayout(false);
    }
}
