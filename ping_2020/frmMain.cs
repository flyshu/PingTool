using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ping_2020.AuthorityService;
using ping_2020.messageService;

namespace ping_2020 {
	public partial class frmMain : Form {


		private delegate void ChangeTextCallback(string value, int id, string color = "black");
		private ChangeTextCallback ChangeTextInvoke;
		Int32 hostCount = 1; //定级主机数量
		GroupBox[] groupBoxs = new GroupBox[1]; //包裹txt的外框
		TextBox[] TxtPings = new TextBox[1];//用于输入ping的文本框
		Thread[] thread = new Thread[1];//多线程
		Int32[] ProcessStatus = new Int32[1]; //线程状态
		Hashtable Hosts = new Hashtable();
		//
		public string token_id;

		public frmMain() {
			InitializeComponent();
			this.Text = "服务器实时网络状态监控程序 Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			//
			Hosts.Add("baidu.com", "baidu.com");
			Hosts.Add("大共享服务器", "172.16.32.1");
			Hosts.Add("考勤服务器", "172.16.32.2");
			Hosts.Add("云服务器1", "172.16.32.9");
			Hosts.Add("云服务器2", "172.16.32.10");
			Hosts.Add("设计部服务器", "172.16.32.35");
			Hosts.Add("新档案服务器", "172.16.32.18");
			Hosts.Add("致远OA服务器", "172.16.33.140");
			Hosts.Add("新OA数据服务器", "172.16.32.22");
			Hosts.Add("总管理处共享", "172.16.33.67");
			Hosts.Add("总体行销服务器", "172.16.33.43");
			Hosts.Add("RedMine服务器", "172.16.32.33");
			Hosts.Add("财务用友服务器", "172.16.33.59");
			hostCount = Hosts.Count;
			groupBoxs = new GroupBox[hostCount];
			TxtPings = new TextBox[hostCount];
			thread = new Thread[hostCount];
			ProcessStatus = new Int32[hostCount];
			//
			AuthorityService.authorityService ws = new AuthorityService.authorityService();
			AuthorityService.UserToken token = ws.authenticate("service-admin", "123456");
			Console.WriteLine(token.id);
			token_id = token.id;


		}

		private void SendOAMessage(string content) {
			List<string> strList = new List<string>();
			strList.Add("B01388");
			strList.Add("B01329");
			List<string> urlList = new List<string>();
			urlList.Add("https://www.baidu.com");
			string[] astr = strList.ToArray();
			string[] url = urlList.ToArray();
			try {
				messageService.messageService msg = new messageService.messageService();
				msg.sendMessageByLoginNameAsync(token_id, astr, content, url);

			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}


		private void SetValue(string value, int id, string color = "black") {
			if (color == "red") {
				this.TxtPings[id].BackColor = System.Drawing.Color.Red;
			} else {
				this.TxtPings[id].BackColor = System.Drawing.Color.Black;
			}
			this.TxtPings[id].Text += value.ToString() + Environment.NewLine;
			this.TxtPings[id].SelectionStart = this.TxtPings[id].Text.Length;
			this.TxtPings[id].ScrollToCaret();
		}

		private Int32 ReturnMs(string str) {
			int pos1, pos2;
			string str1 = "时间", str2 = "ms", result = "";
			if (str.Length == 0)
				return -1;
			pos1 = str.IndexOf(str1);
			if (pos1 < 0)
				return -1;
			pos2 = str.IndexOf(str2, pos1);
			if (pos2 == 0) {
				return -1;
			}

			result = str.Substring(pos1 + 3, pos2 - pos1 - 3);
			return Convert.ToInt32(result);
		}
		private void Ping(object obj) {
			//Process[] P =new Process[hostCount];
			string[] strs = (string[])obj;
			int id = Convert.ToInt32(strs[0]);
			string url = strs[1];
			string name = strs[2];
			Process P;
			string readline = "";
			Int32 ms;
			Int32 errNum = 0, normalNum = 0;
			P = new Process();
			P.StartInfo.FileName = "ping";
			P.StartInfo.Arguments = url + " -t";
			P.StartInfo.UseShellExecute = false;
			P.StartInfo.RedirectStandardInput = true;
			P.StartInfo.RedirectStandardOutput = true;
			P.StartInfo.RedirectStandardError = true;
			P.StartInfo.CreateNoWindow = true;
			P.Start();
			while (!P.HasExited) {
				if (ProcessStatus[id] == 0) {
					TxtPings[id].Invoke(ChangeTextInvoke, Environment.NewLine + "等待关闭进程" + ".", id, "");
					P.Kill();
					break;
				} else if (!P.StandardOutput.EndOfStream) {
					readline = P.StandardOutput.ReadLine();
					ms = ReturnMs(readline);
					if (ms == -1) {
						errNum += 1;
					} else if (ms > 100) {
						errNum += 1;
					} else {
						normalNum += 1;
					}
					TxtPings[id].Invoke(ChangeTextInvoke, readline, id, "");
				}
				if (errNum > 5) {
					errNum = 0;
					normalNum = 0;
					SendOAMessage(name + url + "网络超时超过5次");
					TxtPings[id].Invoke(ChangeTextInvoke, "超时超过5次", id, "red");
				}
				if (normalNum > 20 && errNum==0) {
					normalNum = 0;
					//SendOAMessage(name + url + "恢复正常");
					TxtPings[id].Invoke(ChangeTextInvoke, "正常状态", id, "black");
				}
			}

			TxtPings[id].Invoke(ChangeTextInvoke, Environment.NewLine + "关闭成功", id, "");
			thread[id].Abort();
		}

		private void menuMethodsStart_Click(object sender, EventArgs e) {
			Int32 i = 0;
			string[] para = new string[2];
			foreach (DictionaryEntry host in Hosts) {
				para = new string[3];
				thread[i] = new Thread(new ParameterizedThreadStart(Ping));
				ChangeTextInvoke = new ChangeTextCallback(SetValue);
				ProcessStatus[i] = 1;
				thread[i].IsBackground = true;
				para[0] = i.ToString();
				para[1] = host.Value.ToString();
				para[2] = host.Key.ToString();
				thread[i].Start(para);
				i++;
			}
		}
		private void menuMethodsStop_Click(object sender, EventArgs e) {
			for (Int32 i = 0; i < hostCount; i++) {
				TxtPings[i].AppendText(Environment.NewLine + "正在关闭thread");
				//while (thread[i].IsAlive) {
				ProcessStatus[i] = 0;
				//Application.DoEvents();
				//}
			}
		}
		private void InitGroupAndText() {
			Int32 x, y, i, width, height, txtWidth, txtHeight;
			Int32 num_per_line = 5, lines = 4;
			x = y = i = 0;
			foreach (DictionaryEntry host in Hosts) {
				width = (this.ClientRectangle.Width - 20) / num_per_line;
				height = (this.ClientRectangle.Height - 20) / lines;
				groupBoxs[i] = new GroupBox
				{
					Location = new Point(x * width + 20, height * y + 25),
					Size = new System.Drawing.Size(width, height),
					Name = "groups-" + i,
					Text = host.Key.ToString() + " - " + host.Value.ToString(),
					Tag = i
				};
				txtWidth = groupBoxs[i].Width - 10;
				txtHeight = groupBoxs[i].Height - 20;
				TxtPings[i] = new TextBox
				{
					Location = new Point(5, 15), //New Point(Form1.Label1.Text, Form1.Label2.Text)
					Size = new System.Drawing.Size(width, height),
					Name = "txt_ping-" + i, //TextBox1.Text
					Tag = i,
					Multiline = true,
					Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left,
					WordWrap = false,
					ScrollBars = ScrollBars.Vertical,
					ContextMenuStrip = contextMenuStripText,
					ForeColor = Color.White,
					BackColor = Color.Black,
					ReadOnly = true
				};
				groupBoxs[i].Controls.Add(TxtPings[i]);
				x += 1;
				if (i % num_per_line == 0) {
					y += 1;
					x = 0;
				}
				this.Controls.Add(groupBoxs[i]);
				i += 1;
				//Console.WriteLine(groupBoxs[i].Location.ToString());
			}
		}



		//载入config文件
		private string LoadConfig() {
			return "";
		}

		private void frmMain_Load(object sender, EventArgs e) {
			InitGroupAndText();
		}

		private void frmMain_Resize(object sender, EventArgs e) {
			Int32 display_rows = 5;
			Int32 lines = 3;
			Int32 i = 0, j = 0, line = 0;
			Int32 width = (this.ClientRectangle.Width) / display_rows;
			Int32 height = (this.ClientRectangle.Height) / lines;

			//lines =(hostCount / display_rows);
			//this.AutoScroll = true;

			foreach (GroupBox group in groupBoxs) {
				group.Width = width;
				group.Height = height;
				if (group.HasChildren) {
					group.Controls[0].Width = width - 5;
					group.Controls[0].Height = height - 5;
				}
				if (i % display_rows == 0) {
					if (Convert.ToDouble(i / display_rows) > 0) {
						j = 0;
						line += 1;
					}
				}
				group.Left = width * j;
				group.Top = height * (line) + 25;
				i++;
				j++;
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
			DialogResult dr = MessageBox.Show("确定要退出吗？", "", MessageBoxButtons.OKCancel);
			if (dr == DialogResult.OK) {
				//KillAall();
			} else {
				e.Cancel = true;
			}
		}

		private void ToolStripMenuItemStart_Click(object sender, EventArgs e) {

		}

		private void toolStripMenuItemStop_Click(object sender, EventArgs e) {
			Int32 id = Convert.ToInt32(contextMenuStripText.SourceControl.Tag);
			ProcessStatus[id] = 0;
		}

		private void toolStripMenuItemColor_Click(object sender, EventArgs e) {
			Int32 id = Convert.ToInt32(contextMenuStripText.SourceControl.Tag);
			TxtPings[id].BackColor = System.Drawing.Color.Black;
		}
	}
}
