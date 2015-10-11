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
    public partial class AddForm : Form
    {



        // 下面窗口间传值的时候要用
        public MainForm mForm;


        //public MainForm mainForm;
        public AddForm()
        {
            InitializeComponent();
            // 窗体加载的时候大小不随用户改变而改变
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            // 加载年月日下拉框的值
            // 年
            for (int i = 1980; i < 2020; i++)
            {
                cboYear.Items.Add(i.ToString());
            }
            // 月
            for (int i = 1; i < 13; i++)
            {
                cboMonth.Items.Add(i.ToString());
            }
            // 日
            for (int i = 1; i < 32; i++)
            {
                cboDay.Items.Add(i.ToString());
            }
            // 年月日加载完之后默认选中当前日期
            DateTime dt = DateTime.Today;
            // 注意下面取到的值要转换为字符串格式
            cboYear.SelectedItem = dt.Year.ToString();
            cboMonth.SelectedItem = dt.Month.ToString();
            cboDay.SelectedItem = dt.Day.ToString();
            // 类别收支项目
            cboType.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
        }


        /// <summary>
        /// 点击 "取消" 按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 点击 "添加" 按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            string date = cboYear.Text + '-' + cboMonth.Text + '-' + cboDay.Text;
            string sql = string.Format("insert into consume values('{0}','{1}','{2}',{3},'{4}')",
        date, cboType.Text, cboCategory.Text, txtJinE.Text.Trim(), txtBeizhu.Text.Trim());
            SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            DBHelper.connection.Open();
            // 增删改的语句使用 ExecuteNonQuery()
            int rowCount = cmd.ExecuteNonQuery();
            // 添加成功弹出对话框让用户知道
            if (rowCount > 0)
            {
                MessageBox.Show("添加记录成功");
                mForm.fill();
            }
            else
            {
                MessageBox.Show("添加记录失败");
            }
            DBHelper.connection.Close();
        }


        /// <summary>
        /// 点中"类别"下拉框的时候触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选中收入的时候
            if (cboType.SelectedIndex == 0)
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add("工资");
                cboCategory.Items.Add("其他收入");
            }
            // 选中支出的时候
            else
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add("娱乐");
                cboCategory.Items.Add("餐饮");
                cboCategory.Items.Add("旅游");
                cboCategory.Items.Add("购物");
                cboCategory.Items.Add("其他支出");
            }
            // 无论类别下拉框选中到哪一项的时候  选中收支项目的选项为第一项
            cboCategory.SelectedIndex = 0;
        }


    }
}
