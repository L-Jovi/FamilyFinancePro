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
    public partial class RegistForm : Form
    {


        // 改变量相当于保存登录窗口
        public LoginForm logForm;


        public RegistForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }


        /// <summary>
        /// 点击注册按钮发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegist_Click(object sender, EventArgs e)
        {
            // 判断是否为空 调用下面的判断方法
            bool isNotEmpty = checkEmpty();
            // 下面所做的事情是将用户填写的名字和密码添加到数据库里
            if (isNotEmpty)
            {
                // sql语句插入一条记录 转化为字符串格式
                string sql = string.Format("insert into FamilyUser values('{0}','{1}')",
                    txtName.Text.Trim(),txtPassword.Text.Trim());
                // 创建 sqlCommand 对象  传入插入记录和连接数据库字符串
                SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);

                // 打开数据库
                DBHelper.connection.Open();
                // 打开数据库之后直接执行添加操作
                int count = cmd.ExecuteNonQuery();
                // 执行完语句后关闭数据库
                DBHelper.connection.Close();
                // 添加数据到数据库成功后反馈信息让用户知晓  此时数据库里拥有一条记录
                // 这里的logForm作为注册窗口类的一个变量接收到了来自登录窗口的对象 
                // 因此我们便可以在注册窗口里操作登录窗口了
                if (count == 1)
                {
                    MessageBox.Show("注册成功");
                    logForm.getUserName(txtName.Text.Trim());
                    this.Close();
                }
            }
        }


        /// <summary>
        /// 提供注册信息验证空值的方法
        /// </summary>
        /// <returns></returns>
        private bool checkEmpty()
        {
            // 默认检查通过 也就是不为空
            bool result = true;
            // 验证姓名
            if (txtName.Text.Trim() == string.Empty)
            {
                // 使错误标签信息显示 下面两个方法判断类同
                lblNameError.Visible = true;
                result = false;
            }
            else
            {
                lblNameError.Visible = false;
            }

            // 验证密码
            if (txtPassword.Text.Trim() == string.Empty)
            {
                lblPasswordError.Visible = true;
                result = false;
            }
            else
            {
                lblPasswordError.Visible = false;
            }

            // 验证确认密码
            if (txtConfirm.Text.Trim() == string.Empty)
            {
                lblConfirmError.Visible = true;
                result = false;
            }
            else
            {
                lblConfirmError.Visible = false;
                // 确认密码部分不为空的时候再判断是否和上面的密码一致
                // 这样做的目的是在确认密码部分未填写的时候能够出现报错标签
                if (txtPassword.Text.Trim() != txtConfirm.Text.Trim())
                {
                    lblConfirmError.Text = "   两次密码不一致";
                    lblConfirmError.Visible = true;
                    result = false;
                }
            }

            return result;
        }


        /// <summary>
        /// 取消按钮关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
