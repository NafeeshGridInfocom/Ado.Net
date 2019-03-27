using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GIC_CRUD_with_SP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)//Insert
        {
            con = new SqlConnection(str);
            cmd = new SqlCommand("sp_Insert_addressbook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@address_id", TextBox1.Text);
            cmd.Parameters.AddWithValue("@first_name", TextBox2.Text);
           cmd.Parameters.AddWithValue("@last_name", TextBox3.Text);
            cmd.Parameters.AddWithValue("@email", TextBox4.Text);
            cmd.Parameters.AddWithValue("@phoneno", TextBox5.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            Label1.Text=("Record inserted");
            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)//Update
        {
            con = new SqlConnection(str);
            cmd = new SqlCommand("sp_Update_addressbookk", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@first_name", TextBox2.Text);
            cmd.Parameters.AddWithValue("@last_name", TextBox3.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            //int flag = (int)cmd.Parameters["@flag"].Value;
            //if (flag == 0)
            //    Label1.Text = ("LastName with this record already exists");
            //else
               Label1.Text = ("Record updated");

            con.Close();

        }

        protected void Button3_Click(object sender, EventArgs e)//Delete
        {

            con = new SqlConnection(str);
            cmd = new SqlCommand("sp_Delete_addressbook1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@last_name", TextBox3.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(str);
            cmd = new SqlCommand("sp_read_addressbook1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@address_id",Convert.ToInt32(TextBox1.Text));

      
            SqlParameter p1 = new SqlParameter("@firstname", SqlDbType.VarChar,30);
            SqlParameter p2 = new SqlParameter("@lastname", SqlDbType.VarChar, 70);
            SqlParameter p3 = new SqlParameter("@email", SqlDbType.VarChar, 50);
            SqlParameter p4 = new SqlParameter("@phoneno", SqlDbType.VarChar, 70);
            SqlParameter p5 = new SqlParameter("@flag", SqlDbType.Int);


            p1.Direction = ParameterDirection.Output;
            p2.Direction = ParameterDirection.Output;
            p3.Direction = ParameterDirection.Output;
            p4.Direction = ParameterDirection.Output;
            p5.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);


            con.Open();
            cmd.ExecuteNonQuery();
            int flag = (int)cmd.Parameters["@flag"].Value;
            if(flag==1)
            {

                TextBox2.Text =cmd.Parameters["@firstname"].Value.ToString();
                TextBox3.Text = cmd.Parameters["@lastname"].Value.ToString();
                TextBox4.Text = cmd.Parameters["@email"].Value.ToString();
                TextBox5.Text = cmd.Parameters["@phoneno"].Value.ToString();
            
            }
            con.Close();

        }
    }
    }
