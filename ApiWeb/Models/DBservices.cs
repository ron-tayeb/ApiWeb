using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ApiWeb.Models
{
    public class DBservices
    {
        public DBservices() { }

        public SqlConnection connect()
        {
            string conStr = @"Data Source=DESKTOP-4VJ698M\GJF;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            return con;
        }
        public List<Users> getUsers()
        {
            List<Users> users = new List<Users>();
            Users u = null;
            SqlConnection con = connect();
            SqlCommand comm = new SqlCommand($"SELECT * FROM dbo.TBUser", con);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                u = new Users(Convert.ToString(reader["user_name"]), Convert.ToString(reader["password"]), Convert.ToString(reader["email"]));
                users.Add(u);
            }
            con.Close();
            return users;
        }
        public Users getUser(string id)
        {
            Users u = null;
            SqlConnection con = connect();
            SqlCommand comm = new SqlCommand($"SELECT * FROM dbo.TBUser WHERE password = '{id}'", con);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                u = new Users(Convert.ToString(reader["user_name"]), Convert.ToString(reader["password"]), Convert.ToString(reader["email"]));

            }

            con.Close();
            return u;
        }

        public int DetleteUser(string pass)
        {
            int res = 0;
            SqlConnection con = connect();
            SqlCommand comm = new SqlCommand($"Delete From dbo.TBUser WHERE password = '{pass}'", con);
            res = comm.ExecuteNonQuery();
            con.Close();
            if (res == 0)
            {
                throw new Exception("Not Exist");
            }
            return res;
        }
        public int CreatNewUser(Users user)
        {
            SqlConnection con = connect();
            SqlCommand comm = new SqlCommand($"INSERT INTO dbo.TBUser (user_name, password, email) " +
                                             $"VALUES('{user.user_name}', '{user.password}', '{user.email}')", con);
            int res = comm.ExecuteNonQuery();
            return res;

        }

        public int EditUser(string pass, Users user)
        {
            int res = 0;
            SqlConnection con = connect();
            List<Users> users = getUsers();
            Users u = users.SingleOrDefault(us => us.password == pass);

            string qu = $"Update dbo.TBUser Set user_name='{user.user_name}', password='{user.password}', email='{user.email}' where password='{pass}'";
            con.Close();
            SqlCommand updateCom = new SqlCommand(qu, con);
            con.Open();
            res = updateCom.ExecuteNonQuery();
            con.Close();
            return res;
            //throw new Exception("Not Exsit");
        }
    }
}