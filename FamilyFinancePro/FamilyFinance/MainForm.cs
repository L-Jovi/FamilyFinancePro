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
    public partial class MainForm : Form
    {


        //// 当前窗口高度
        //private int formHeight = 0;
        //// 当前窗口宽度
        //private int formWidth = 0;


        public MainForm()
        {
            InitializeComponent();
            // 禁止用户改变尺寸
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            AddForm addForm = new AddForm();
            addForm.mForm = this;
        }


        /// <summary>
        /// 窗口加载时调用的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.mForm = this;
            // TODO: This line of code loads data into the 'familyDataSet.Consume' table. You can move, or remove it, as needed.
            // 我们这里不需要这样的刷新信息到表控件的代码 直接通过手写代码查询 下方代码的作用我们用绑定同样能够实现
            this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
            //this.bindData(string.Empty);

            // 默认选中第一项
            cboDirection.SelectedIndex = 0;
            cboSort.SelectedIndex = 0;
            cboType.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            // 隐藏下面大面板
            openOrClosePnlButtom(true);

            // 加载窗体的时候就开始计时  这样可以保证进入的时候当前时间是正确的
            DateTime dt = DateTime.Now;
            // 获得年月日部分
            string date = dt.ToLongDateString();
            // 获得时间部分
            string time = dt.ToLongTimeString();
            lblTime.Text = date + time;

            // 加载年月日下拉框的所有值
            // 年
            for (int i = 1900; i < 2060; i++)
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

            //// 下面的是判定日显示天数的方法  但并不正确
            //if (Convert.ToInt32(cboMonth.Text) == 1 || Convert.ToInt32(cboMonth.Text) == 3 || Convert.ToInt32(cboMonth.Text) == 5 || Convert.ToInt32(cboMonth.Text) == 7 || Convert.ToInt32(cboMonth.Text) == 8 || Convert.ToInt32(cboMonth.Text) == 10 || Convert.ToInt32(cboMonth.Text) == 12)
            //{
            //    for (int i = 1; i < 32; i++)
            //    {
            //        cboDay.Items.Add(i.ToString());
            //    }
            //}
            //else if (Convert.ToInt32((cboMonth.Text)) == 4 || Convert.ToInt32(cboMonth.Text) == 6 || Convert.ToInt32(cboMonth.Text) == 9 || Convert.ToInt32(cboMonth.Text) == 11)
            //{
            //    for (int i = 1; i < 31; i++)
            //    {
            //        cboDay.Items.Add(i.ToString());
            //    }
            //}
            //else
            //{
            //    if ((Convert.ToInt32(cboYear.Text) % 4 == 0 && Convert.ToInt32(cboYear.Text) % 100 != 0) || Convert.ToInt32(cboYear.Text) % 400 != 0)
            //    {
            //        for (int i = 1; i < 30; i++)
            //        {
            //            cboDay.Items.Add(i);
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 1; i < 29; i++)
            //        {
            //            cboDay.Items.Add(i);
            //        }
            //    }
            //}

            // 取得总收入和总支出
            double shouRu, zhiChu;
            shouRu = this.getTotalMoney("收入");
            zhiChu = this.getTotalMoney("支出");
            // 把上面获得的值显示在 收入 支出 差额 textbox 上
            txtRu.Text = shouRu.ToString();
            txtChu.Text = zhiChu.ToString();
            //这里打印差额的时候做一个计算  如果为正值就显示金黄色  负值显示红色  相等就显示黑色
            if (shouRu > zhiChu)
            {
                txtCha.ForeColor = Color.Yellow;
            }
            else if(shouRu == zhiChu)
            {
                txtCha.ForeColor = Color.Black;
            }
            else
            {
                txtCha.ForeColor = Color.Red;
            }
            txtCha.Text = (shouRu - zhiChu).ToString();          
        }


        /// <summary>
        /// 查询记录并绑定到 dataGridView 控件上的方法
        /// </summary>
        public String bindData(string condition)
        {
            string sql = string.Format("select * from consume ");
            if (condition != string.Empty)
            {
                sql = sql + "where " + condition;
            }

            return sql;

            // 这里查询使用离线数据库 所有不需要连接数据库的 open 方法和执行 sql 语句的 cmd.ExecuteNonQuery() 方法
            // 注意 SqlDataAdapter 类的使用
            //SqlDataAdapter adp = new SqlDataAdapter(sql, DBHelper.connection);
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "consume");
            //dgvHome.DataSource = ds.Tables["consume"].DefaultView;

            
            //SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            //DBHelper.connection.Open();
            //int cc = cmd.ExecuteNonQuery();
            //this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
            //DBHelper.connection.Close();
        }


        // 显示当前时间的方法
        private void tmData_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            // 获得年月日部分
            string date = dt.ToLongDateString();
            // 获得时间部分
            string time = dt.ToLongTimeString();
            lblTime.Text = date + time;
        }


        // 点击"打开" "关闭"按钮对整个窗体作出调整
        private void btnGuanBi_Click(object sender, EventArgs e)
        {
            // 按钮名字显示为"打开"的时候调用显示面板的方法  也就是 hide 为假的方法
            if (btnGuanBi.Text == "打开")
            {
                openOrClosePnlButtom(false);
            }
            else
            {
                openOrClosePnlButtom(true);
            }
        }


        /// <summary>
        /// "打开""关闭"按钮对应下方大面板显示隐藏的方法
        /// </summary>
        private void openOrClosePnlButtom(bool hide)
        {
            // hide 为真便隐藏 
            if (hide)
            {
                // 根据当前窗体高度作出点击按钮之后的窗体高度改变
                this.Size = new Size(this.Size.Width, 571 - 102);
                // 下面的 panel 显示出来
                pnlBottom.Visible = false;
                // 按钮 y 坐标减少  这样在隐藏 panel 的时候提高了按钮的位置
                this.btnGuanBi.Location = new Point(this.btnGuanBi.Location.X, this.btnGuanBi.Location.Y - 105);
                // 同时按钮的名字随之改变
                this.btnGuanBi.Text = "打开";
            }
            // hide 为假便显示  
            else
            {
                this.Size = new Size(this.Size.Width, 571);
                pnlBottom.Visible = true;
                // 显示面板后让其可用
                pnlBottom.Enabled = true;
                this.btnGuanBi.Location = new Point(this.btnGuanBi.Location.X, this.btnGuanBi.Location.Y + 105);
                this.btnGuanBi.Text = "关闭";
            }
        }


        /// <summary>
        /// gridView 里面的单元格单击触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 首先使大面板可用
            //pnlBottom.Enabled = true;

            // 点击单元格的时候先使左边 "删除记录" "修改记录" 按钮有效
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            // 将表格中的数据显示在 textbox 上面  cells为对应行列的单元格
            // 流水号
            txtLiuShui.Text = dgvHome.SelectedRows[0].Cells[0].Value.ToString();
            // 日期
            string cData = dgvHome.SelectedRows[0].Cells[1].Value.ToString();
            // 将日期的值分拆为年月日  首先将字符串强转为日期型
            DateTime dt = Convert.ToDateTime(cData);
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            // 将上面的值打印在 combox 的下拉列表中显示
            cboYear.SelectedItem = year.ToString();
            cboMonth.SelectedItem = month.ToString();
            cboDay.SelectedItem = day.ToString();

            // 将 gridView 中的类别显示在 combox 上面
            cboType.SelectedItem = dgvHome.SelectedRows[0].Cells[2].Value.ToString().Trim();
            // 将 gridView 中的收支项目显示在 combox 上面
            cboCategory.SelectedItem = dgvHome.SelectedRows[0].Cells[3].Value.ToString().Trim();
            // 将 gridView 中的金额显示在 textbox 上面
            txtJinE.Text = dgvHome.SelectedRows[0].Cells[4].Value.ToString().Trim();
            // 将 gridView 中的备注显示在 textbox 上面
            txtBeizhu.Text = dgvHome.SelectedRows[0].Cells[5].Value.ToString().Trim();
        }


        /// <summary>
        /// 点击"类别"下拉框触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选中收入的时候
            if (cboType.SelectedIndex == 0)
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add("工资收入");
                cboCategory.Items.Add("其他收入");
            }
                // 选中支出的时候
            else
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add("娱乐支出");
                cboCategory.Items.Add("餐饮支出");
                cboCategory.Items.Add("旅游支出");
                cboCategory.Items.Add("购物支出");
                cboCategory.Items.Add("其他支出");
            }
            // 无论类别下拉框选中到哪一项的时候  选中收支项目的选项为第一项
            cboCategory.SelectedIndex = 0;
        }


        /// <summary>
        /// 取得总数目  这里就是一个计算收入支出金额的方法
        /// 我们这里需要连接数据库
        /// </summary>
        private double getTotalMoney(string type)
        {
            double total=0;
            string sql = string.Format("select sum(ConsumeMoney) as '{0}' from consume where type='{0}'",type);
            SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            DBHelper.connection.Open();
            // 这里 查找 总收支金额  语句使用 ExecuteScalar()  返回一个 int
            object obj = cmd.ExecuteScalar();
            // 为防止计算出收入支出的金额为空  我们在这里做判断  不为空的时候再转换格式
            if(obj.ToString() != "")
            {
                // 注意下面的强制转换使用
                total = Convert.ToDouble(obj);
            }
            DBHelper.connection.Close();
            return total;
        }


        /// <summary>
        /// 下方大面板中"修改"按钮的事件
        /// 这里同样需要连接数据库  修改一条记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnXiuGai_Click(object sender, EventArgs e)
        {
            // 下面要用到日期  所以先在这里把年月日拼接起来
            string date = cboYear.Text + '-' + cboMonth.Text + '-' + cboDay.Text;
            string sql = string.Format("update consume set ConsumeDate='{0}',Type='{1}',Category='{2}',ConsumeMoney={3},Description='{4}' where Id={5}",
                date, cboType.Text, cboCategory.Text, txtJinE.Text.Trim(), txtBeizhu.Text.Trim(), txtLiuShui.Text);
            SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            DBHelper.connection.Open();
            // 增删改的语句使用 ExecuteNonQuery()
            int rowCount = cmd.ExecuteNonQuery();
            // 修改成功弹出对话框让用户知道
            if(rowCount > 0)
            {
                MessageBox.Show("修改成功");
                // 修改成功后同步更新到控件 dataGridView 上
                this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
            }
            else
            {
                MessageBox.Show("修改失败");
            }
            DBHelper.connection.Close();
        }


        /// <summary>
        /// 这是我们伟大的同步数据表的方法
        /// </summary>
        public void fill() {
            AddForm addForm = new AddForm();
            addForm.mForm = this;
            this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
        }


        /// <summary>
        /// 点击左边"修改记录"按钮发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 选中一条记录的时候打开面板
            if (dgvHome.SelectedRows.Count != 0)
            {
                openOrClosePnlButtom(false);
            }
        }


        /// <summary>
        /// 点击左边"删除记录"按钮发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from consume where Id={0}", txtLiuShui.Text);
            SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            DBHelper.connection.Open();
            // 增删改的语句使用 ExecuteNonQuery()
            int rowCount = cmd.ExecuteNonQuery();
            // 修改成功弹出对话框让用户知道
            if (rowCount > 0)
            {
                MessageBox.Show("删除成功");
                // 删除成功后同步更新到控件 dataGridView 上
                this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            DBHelper.connection.Close();
        }


        /// <summary>
        /// 点击 "新增收支" 按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.mForm = this;
            addForm.Show();
        }


        /// <summary>
        /// 点击"排序规则"下拉框触发的事件
        /// 这里让后面的下拉框 cboDirection 执行的方法名字也为  cboSort_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        ///  菜单栏所有按钮实现功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 增
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            //addForm.mainForm = this;
            addForm.Show();
        }
        // 删
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from consume where Id={0}", txtLiuShui.Text);
            SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);
            DBHelper.connection.Open();
            // 增删改的语句使用 ExecuteNonQuery()
            int rowCount = cmd.ExecuteNonQuery();
            // 修改成功弹出对话框让用户知道
            if (rowCount > 0)
            {
                MessageBox.Show("删除成功");
                // 删除成功后同步更新到控件 dataGridView 上
                this.consumeTableAdapter.Fill(this.familyDataSet.Consume);
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            DBHelper.connection.Close();

            //SqlDataAdapter adp = new SqlDataAdapter(sql, DBHelper.connection);
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "consume");
            //dgvHome.DataSource = ds.Tables["consume"].DefaultView;
        }
        // 改
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // 选中一条记录的时候打开面板
            if (dgvHome.SelectedRows.Count != 0)
            {
                openOrClosePnlButtom(false);
            }
        }
        // "关于我们"按钮触发事件
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AboutUsForm about = new AboutUsForm();
            about.Show();
        }
        // "收支查询"按钮触发事件
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SearchForm search = new SearchForm();
            // 当前窗口实例传递到另一个窗口的变量
            search.mainForm = this;
            search.Show();
        }


        /// <summary>
        /// "收支查询"按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SearchForm search = new SearchForm();
            // 当前窗口实例传递到另一个窗口的变量
            search.mainForm = this;
            search.Show();
        }


    }
}
