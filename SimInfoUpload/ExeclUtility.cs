using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimInfoUpload
{
    public class ExeclUtility
    {
        private string fileName;
        public string execlCheckStatus { get; set; }

        public ExeclUtility(string fileName)
        {
            this.fileName = fileName;
        }

        public List<SimInfo> GetDataFromExcelByConn(bool hasTitle = false)
        {
            var filePath = this.fileName;
            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            using (DataSet ds = new DataSet())
            {
                string strCon = string.Format("Provider=Microsoft.{4}.OLEDB.{0}.0;" +
                                "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                "data source={3};",
                                (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filePath, (fileType == ".xls" ? "Jet" : "ACE"));
                string strCom = " SELECT * FROM [Sheet1$]";
                using (OleDbConnection myConn = new OleDbConnection(strCon))
                using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                {
                    myConn.Open();
                    myCommand.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                System.Diagnostics.Debug.Write(ds.Tables[0].Rows[0][0]);

                List<SimInfo> simInfoList = new List<SimInfo>();

                execlCheckStatus = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i ++)
                {
                    SimInfo simInfo = new SimInfo()
                    {
                        num = ds.Tables[0].Rows[i][0].ToString(),
                        ccid = ds.Tables[0].Rows[i][1].ToString(),
                        description = ds.Tables[0].Rows[i][2].ToString()
                    };
                    if (CheckCCID(simInfo.ccid) == false)
                    {
                        execlCheckStatus += String.Format("{1}行： 无效的CCID {0}\r\n", simInfo.ccid, i + 1);
                        continue;
                    }
                    if (simInfoList.Find(q => (q.num == simInfo.num || q.ccid == simInfo.ccid)) == null)
                    {
                        simInfoList.Add(simInfo);
                    }
                    else
                    {
                        execlCheckStatus += String.Format(
                            "{2}行： num {0} ccid {1} 重复的号码或CCID\r\n", 
                            simInfo.num, simInfo.ccid, i + 1);
        
                    }
                }

                return simInfoList;
            }
        }

        private bool CheckCCID(string ccid)
        {
            Regex reg = new Regex("[a-fA-F0-9]{20}");
            return reg.IsMatch(ccid);
        }
    }
}
