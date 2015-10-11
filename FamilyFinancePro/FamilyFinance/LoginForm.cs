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
    public partial class LoginForm : Form
    {


        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }


        /// <summary>
        /// 登录界面点击注册按钮发生事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegist_Click(object sender, EventArgs e)
        {
            // 点击注册按钮显示注册界面
            RegistForm rForm = new RegistForm();
            // 将当前窗口传递给注册窗口（其实就是把登录窗体作为变量传输给了注册窗口）
            rForm.logForm = this;
            rForm.Show();
        }


        /// <summary>
        /// 该方法接受注册窗口传来的用户名并显示到登录界面的用户名上
        /// </summary>
        /// <param name="name"></param>
        public void getUserName(string name)
        {
            txtName.Text = name;
        }


        /// <summary>
        /// 点击登录按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 判断是否为空 调用下面的判断方法
            bool isNotEmpty = checkEmpty();
            // 下面所做的事情是将用户填写的名字和密码添加到数据库里
            if (isNotEmpty)
            {
                // sql语句插入一条记录 转化为字符串格式
                string sql = string.Format("select count(*) from FamilyUser where userName='{0}' and userPassword='{1}'",
                    txtName.Text.Trim(), txtPassword.Text.Trim());
                // 创建 sqlCommand 对象  传入插入记录和连接数据库字符串
                SqlCommand cmd = new SqlCommand(sql, DBHelper.connection);

                // 打开数据库
                DBHelper.connection.Open();
                // 打开数据库之后直接执行添加操作  这里只检索一个值
                int count = (int)cmd.ExecuteScalar();
                // 执行完语句后关闭数据库
                DBHelper.connection.Close();
                // 检索到记录后打开主界面窗口
                if (count > 0)
                {
                    MainForm mForm = new MainForm();
                    mForm.Show();
                    // 打开主窗口的同时隐藏当前登录窗口
                    // 之所以隐藏而不关闭是因为主界面窗口在这里相当于登陆窗口的子窗口  一旦关闭主窗口子窗口也会随之关闭
                    this.Hide();
                }
                    // 在数据库中检索到的信息记录数目不对的时候则显示错误标签
                else 
                {
                    lblAllError.Visible = true;
                }
            }
        }


        /// <summary>
        /// 登录界面验证不为空方法
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

            //if (txtPassword.Text.Trim() != txtName.Text.Trim())
            //{
            //    lblAllError.Visible = true;
            //    result = false;
            //}

            return result;
        }
    }
}
