using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace FamilyFinance
{
    public partial class SearchForm : Form
    {


        public MainForm mainForm;
        public SearchForm()
        {
            InitializeComponent();
            // 用户不可调整窗体大小
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }


        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            // 加载年月日下拉框的所有值
            // 年
            for (int i = 1900; i < 2060; i++)
            {
                cboYear.Items.Add(i.ToString());
                cboEndYear.Items.Add(i.ToString());
            }
            // 月
            for (int i = 1; i < 13; i++)
            {
                cboMonth.Items.Add(i.ToString());
                cboEndMonth.Items.Add(i.ToString());
            }
            // 日
            for (int i = 1; i < 32; i++)
            {
                cboDay.Items.Add(i.ToString());
                cboEndDay.Items.Add(i.ToString());
            }
            // 开始日期默认选择值
            cboYear.SelectedIndex = 0;
            cboMonth.SelectedIndex = 0;
            cboDay.SelectedIndex = 0;
            // 年月日加载完之后默认选中当前日期
            DateTime dt = DateTime.Today;
            // 注意下面取到的值要转换为字符串格式
            cboEndYear.SelectedItem = dt.Year.ToString();
            cboEndMonth.SelectedItem = dt.Month.ToString();
            cboEndDay.SelectedItem = dt.Day.ToString();
        }


        /// <summary>
        /// "时间段""时间点"单选框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDuan_CheckedChanged(object sender, EventArgs e)
        {
            // 选中时间段时让下面的截止年月日下拉框都可用
            if (rdoDuan.Checked)
            {
                cboEndYear.Enabled = true;
                cboEndMonth.Enabled = true;
                cboEndDay.Enabled = true;
            }
            else
            {
                cboEndYear.Enabled = false;
                cboEndMonth.Enabled = false;
                cboEndDay.Enabled = false;
            }
        }


        /// <summary>
        /// "取消"按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuXiao_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// "查询"按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChaXun_Click(object sender, EventArgs e)
        {
            // 拼接查询条件 sql 语句
            string condition = string.Empty;
            // 按时间段查询
            if (rdoDuan.Checked)
            {
                condition += string.Format("ConsumeDate>='{0}' and ConsumeDate<='{1}'",
                    cboYear.Text + "-" + cboMonth.Text + "-" + cboDay.Text,
                    cboEndYear.Text + "-" + cboEndMonth.Text + "-" + cboEndDay.Text);
            }
                // 按时间点查询
            else
            {
                condition += string.Format("ConsumeDate='{0}'",
                    cboYear.Text + "-" + cboMonth.Text + "-" + cboDay.Text);
            }

            // 按类别查询
            if (condition != string.Empty)
            {
                string category = string.Empty;
                if (cbGongZi.Checked)
                {
                    category += string.Format("'{0}',",cbGongZi.Text);
                }
                if (cbQiTaRu.Checked)
                {
                    category += string.Format("'{0}',",cbQiTaRu.Text);
                }
                if (cbYuLe.Checked)
                {
                    category += string.Format("'{0}',", cbYuLe.Text);
                }
                if (cbCanYin.Checked)
                {
                    category += string.Format("'{0}',", cbCanYin.Text);
                }
                if(cbLvYou.Checked)
                {
                    category += string.Format("'{0}',",cbLvYou.Text);
                }
                if(cbGouWu.Checked)
                {
                    category += string.Format("'{0}',",cbGouWu.Text);
                }
                if (cbQiTaChu.Checked)
                {
                    category += string.Format("'{0}',",cbQiTaChu.Text);
                }
                // 上面的字符串结尾都带有逗号  所以在下面去掉
                string categoryFina = category.Substring(0, category.Length - 1);
                condition += string.Format(" and Category in({0})",categoryFina);

                // 按金额范围查询
                double minMoney = 0, maxMoney = 0;
                // 用户输入不为空的验证
                if (txtMin.Text.Trim() == string.Empty)
                {
                    txtMin.Text = "0";
                }
                if(txtMax.Text.Trim() == string.Empty)
                {
                    txtMax.Text = "0";
                }
                try
                {
                    minMoney = Convert.ToDouble(txtMin.Text.Trim());
                    maxMoney = Convert.ToDouble(txtMax.Text.Trim());
                }
                catch(FormatException ex) 
                {
                    MessageBox.Show("请输入正确金额数值");
                    return;
                }
                if (minMoney > 0)
                {
                    condition += string.Format(" and ConsumeMoney>={0}",minMoney);
                }
                if (maxMoney > 0)
                {
                    condition += string.Format(" and ConsumeMoney<={0}",maxMoney);
                }

                // 备注的模糊搜索
                if (txtDescription.Text.Trim() != string.Empty)
                {
                    condition += string.Format(" and Description like '%{0}%'", txtDescription.Text.Trim());
                }
            }

            // 调用 MainForm 中的绑定方法
            //mainForm.bindData(condition);

            // 下面写的都是我自己的尝试罢了
            SqlCommand cmd = new SqlCommand(mainForm.bindData(condition), DBHelper.connection);
            DBHelper.connection.Open();
            int rowCounts = cmd.ExecuteNonQuery();
            if (rowCounts > 0)
            {
                MessageBox.Show("query success");
                mainForm.fill();
            }
            else 
            {
                MessageBox.Show("query failure");
            }
            DBHelper.connection.Close();

            this.Close();
        }
    }
}
