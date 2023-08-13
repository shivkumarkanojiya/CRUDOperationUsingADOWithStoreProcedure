using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRUDOperationUsingADOWithStoreProcedure.Models;

namespace CRUDOperationUsingADOWithStoreProcedure.DAL
{
    public class ProductDAL
    {
        string constring = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //get all list products
        public List<Product> GetAllProducts()
        {
            List<Product> Productlist = new List<Product>();
          
            using (SqlConnection con = new SqlConnection(constring))
            {
                
                SqlCommand cmd = new SqlCommand("SpSelTblProductMaster",con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Product p = new Product();
                    p.productID = Convert.ToInt32(dr["productID"]);
                    p.productName = dr["productName"].ToString();
                    p.price = Convert.ToInt32(dr["price"]);
                    p.Qty = Convert.ToDecimal(dr["Qty"]);
                    p.Remarks = dr["Remarks"].ToString();
                    Productlist.Add(p);

                }
                
            }
            return Productlist;
        }
        //Inser Product
        public bool SaveDataProductmast(Product pr)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {

                SqlCommand cmd = new SqlCommand("SpInsTblProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("productName", pr.productName);
                cmd.Parameters.AddWithValue("price", pr.price);
                cmd.Parameters.AddWithValue("Qty", pr.Qty);
                cmd.Parameters.AddWithValue("Remarks", pr.Remarks);

                con.Open();
               id= cmd.ExecuteNonQuery();
                con.Close();

                
            }
            if(id>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //get data by id
        public List<Product> GetProductsbyid( int productID)
        {
            List<Product> Productlist = new List<Product>();

            using (SqlConnection con = new SqlConnection(constring))
            {

                SqlCommand cmd = new SqlCommand("SpSelbyidTblProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("productID", productID);
                SqlDataAdapter sad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                sad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Product p = new Product();
                    p.productID = Convert.ToInt32(dr["productID"]);
                    p.productName = dr["productName"].ToString();
                    p.price = Convert.ToInt32(dr["price"]);
                    p.Qty = Convert.ToDecimal(dr["Qty"]);
                    p.Remarks = dr["Remarks"].ToString();
                    Productlist.Add(p);

                }

            }
            return Productlist;
        }
        //Update record product master
        public bool UpdateDataProductmast(Product pr)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {

                SqlCommand cmd = new SqlCommand("SpUpdateTblProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("productID", pr.productID);
                cmd.Parameters.AddWithValue("productName", pr.productName);
                cmd.Parameters.AddWithValue("price", pr.price);
                cmd.Parameters.AddWithValue("Qty", pr.Qty);
                cmd.Parameters.AddWithValue("Remarks", pr.Remarks);

                con.Open();
                id = cmd.ExecuteNonQuery();
                con.Close();


            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete record product master
        public bool DeleteDataProductmast(int id)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {

                SqlCommand cmd = new SqlCommand("SpDeleteTblProductMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("productID",id);
               

                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();


            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}