using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using auto;

public class AutoBuff : Form
{
    private Player player;

    private IContainer components = null;

    private ListView listView1;

    private ColumnHeader columnHeader1;

    private ListView listView2;

    private ColumnHeader columnHeader2;

    private CheckBox checkBox1;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem getALLToolStripMenuItem;

    private ContextMenuStrip contextMenuStrip2;

    private ToolStripMenuItem deleteToolStripMenuItem;

    private ToolStripMenuItem deleteALLToolStripMenuItem;

    public AutoBuff(Client client)
    {
        InitializeComponent();
        player = client.player;
        if (!client.IsChecked)
        {
            return;
        }
        if (player.buffnpclist != null)
        {
            foreach (Player.NPCinfo item in player.buffnpclist)
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
            player.buffnpclist = new List<Player.NPCinfo>();
        }
        checkBox1.Checked = player.isBuffALL;
    }

    private void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked)
        {
            player.isBuffALL = false;
        }
        else
        {
            player.isBuffALL = true;
        }
    }

    private void GetALLToolStripMenuItem_Click(object sender, EventArgs e)
    {
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
        Player.NPCinfo item = player.buffnpclist.FirstOrDefault((Player.NPCinfo x) => x.Name == listView2.SelectedItems[0].SubItems[0].Text);
        player.buffnpclist.Remove(item);
        listView2.Items.Clear();
        foreach (Player.NPCinfo item2 in player.buffnpclist)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                item2.Name
            });
            listView2.Items.Add(value);
        }
    }

    private void DeleteALLToolStripMenuItem_Click(object sender, EventArgs e)
    {
        listView2.Items.Clear();
        player.buffnpclist.Clear();
    }

    private void ListView1_DoubleClick(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            ListViewItem value = new ListViewItem(new string[1]
            {
                listView1.SelectedItems[0].SubItems[0].Text
            });
            if (!player.buffnpclist.Exists((Player.NPCinfo x) => x.Name == listView1.SelectedItems[0].SubItems[0].Text))
            {
                listView2.Items.Add(value);
                player.buffnpclist.Add(new Player.NPCinfo
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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(2, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(148, 201);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.ListView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Danh Sách Nhân Vật";
            this.columnHeader1.Width = 217;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getALLToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // getALLToolStripMenuItem
            // 
            this.getALLToolStripMenuItem.Name = "getALLToolStripMenuItem";
            this.getALLToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.getALLToolStripMenuItem.Text = "Get";
            this.getALLToolStripMenuItem.Click += new System.EventHandler(this.GetALLToolStripMenuItem_Click);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.ContextMenuStrip = this.contextMenuStrip2;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(156, 2);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(148, 201);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Danh Sách Buff";
            this.columnHeader2.Width = 142;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.deleteALLToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(128, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // deleteALLToolStripMenuItem
            // 
            this.deleteALLToolStripMenuItem.Name = "deleteALLToolStripMenuItem";
            this.deleteALLToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.deleteALLToolStripMenuItem.Text = "DeleteALL";
            this.deleteALLToolStripMenuItem.Click += new System.EventHandler(this.DeleteALLToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(2, 209);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(130, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Buff Theo Danh Sách";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // AutoBuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 231);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Name = "AutoBuff";
            this.Text = "AutoBuff";
         //   this.Load += new System.EventHandler(this.AutoBuff_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    
}
