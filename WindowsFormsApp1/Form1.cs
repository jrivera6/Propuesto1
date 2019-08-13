using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //DB
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        public void ListaClientes()
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_Listaclientes_Neptuno", conn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using(DataSet da = new DataSet())
                {
                    df.Fill(da, "Clientes");
                    dgClientes.DataSource = da.Tables["Clientes"];
                    lblTotal.Text = da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes();
        }

        public void filtradoClientes()
        {

            string id = txtNombre.Text.ToString();
            //using (SqlDataAdapter df = new SqlDataAdapter("select * from clientes where idCliente = @nombre ", conn))
            using (SqlDataAdapter df = new SqlDataAdapter("usp_filtroclientes_nombre", conn))
            {


                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                df.SelectCommand.Parameters.AddWithValue("@idCliente", id);



                using (DataSet da = new DataSet())
                {
                    df.Fill(da, "Clientes");
                    dgClientes.DataSource = da.Tables["Clientes"];
                    lblTotal.Text = da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {


            filtradoClientes();

        }
    }
}
