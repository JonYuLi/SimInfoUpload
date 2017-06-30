using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimInfoUpload
{
    public partial class Form1 : Form
    {
        private string fileName;
        private List<SimInfo> simInfoList;

        public Form1()
        {
            InitializeComponent();
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            this.fileName = openFileDialog.FileName;
            fileNameTx.Text = this.fileName;
            if (this.fileName == "")
            {
                infoTx.Text = "未选择文件";
                return;
            }
            ExeclUtility execlHelper = new ExeclUtility(this.fileName);
            var simInfoList = execlHelper.GetDataFromExcelByConn();
            this.simInfoList = simInfoList;
            statusTx.Text = execlHelper.execlCheckStatus;
            infoTx.Text = String.Format("共可以上传{0}条", this.simInfoList.Count);
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread t = new Thread(ThreadUpload);
            t.Start();
        }

        private void ThreadUpload()
        {
            if (this.simInfoList == null || this.simInfoList.Count == 0)
            {
                infoTx.Text = "没有可以上传的数据";
                return;
            }
            uploadBtn.Enabled = false;
            try
            {
                DisplayValue();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                uploadBtn.Enabled = true;
            }
            
        }

        public Task<int> GetValueAsync(SimInfo simInfo)
        {
            return Task.Run(() =>
            {
                HttpUtility httpHelper = new HttpUtility();
                if (httpHelper.getNumFormServer(simInfo.num) != null)
                {
                    return 1;
                }
                if (httpHelper.getSimInfoByCCID(simInfo.ccid) != null)
                {
                    return 2;
                }
                return 0;
            });
        }

        public async void DisplayValue()
        {
            try
            {
                statusTx.Text = "";
                int success = 0; int fail = 0;
                for (int i = 0; i < this.simInfoList.Count; i++)
                {
                    int result = await GetValueAsync(simInfoList[i]);
                    switch (result)
                    {
                        case 0:
                            HttpUtility httpPost = new HttpUtility();
                            if (httpPost.HttpPost(simInfoList[i]))
                            {
                                success++;
                            }
                            else
                            {
                                fail++;
                                statusTx.AppendText("提交到服务器时出错 number: " + simInfoList[i].num);
                            }
                            break;
                        case 1:
                            fail++;
                            statusTx.AppendText("己存在的number " + simInfoList[i].num + "\r\n");
                            break;
                        case 2:
                            fail++;
                            statusTx.AppendText("己存在的ccid " + simInfoList[i].ccid + "\r\n");
                            break;
                    }
                    SetStatus(success, fail, simInfoList.Count);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                uploadBtn.Enabled = true;
            }
        }

        public void SetStatus(int success, int fail, int total)
        {
            infoTx.Text = String.Format("成功 {0}， 失败{1}， 共 {2}", success, fail, total);
        }
    }
}
