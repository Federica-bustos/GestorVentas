using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FE
{
    public partial class Form1 : Form
    {
        DataTable dtVentas = new DataTable() { TableName = "Ventas" };
        DataTable dtCalculos = new DataTable() { TableName = "Calculos" };


        const string DIRECCION_XML = @"C:\Vs2022\GestorVentas";

        public Form1()
        {
            InitializeComponent();

            //Columnas
            dtVentas.Columns.Add("Nº Venta");
            dtVentas.Columns.Add("Articulo");
            dtVentas.Columns.Add("Precio Unitario");
            dtVentas.Columns.Add("Cantidad");
            //----------------------------------------
            dtCalculos.Columns.Add("Cantidad de ventas");
            dtCalculos.Columns.Add("Monto Total");
            dtCalculos.Columns.Add("Cantidad de articulos");



            Leer_DTVentas();
            Leer_DTCalculos();
            dgvVentas.DataSource = dtVentas;
            dgvCalculos.DataSource = dtCalculos;
        }

        private void btRegistrar_Click(object sender, EventArgs e)
        {
            //creamos un objeto del tipo libro
            Venta venta = new Venta();

            //le damos valores a sus atributos
            venta.NVenta = Convert.ToInt32(txtNVenta.Text);
            venta.Articulo = txtArticulo.Text;
            venta.Precio = Convert.ToDecimal(txtPrecio.Text);
            venta.Cantidad = Convert.ToInt32(txtCantidad.Text);

            //cargamos el objeto en el datatable y guardamos los datos
            dtVentas.Rows.Add(new object[] {venta.NVenta, venta.Articulo, venta.Precio, venta.Cantidad});
            dtVentas.WriteXml(DIRECCION_XML + "ventas.xml");

            int cantVentas = venta.CantVentasSuma();
            decimal montoTotal = venta.MontoTotal();
            int totalArticulos = venta.CantArticulos();

            dtCalculos.Rows.Add(new object[] {cantVentas, montoTotal, totalArticulos});
            dtCalculos.WriteXml(DIRECCION_XML + "calculos.xml");

            LimpiarTextos();
        }

        private void btBorrar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow == null)
            {
                MessageBox.Show("Fila no seleccionada");
            }
            else
            {
                int fila = BuscarVenta(dgvVentas.CurrentRow.Cells[0].Value.ToString());

                if (fila != -1)
                {
                    dtVentas.Rows[fila].Delete();
                    dtVentas.WriteXml(DIRECCION_XML + "ventas.xml");
                }
            }
        }

        //metodo para leer el datatable
        private void Leer_DTVentas()
        {
            if (System.IO.File.Exists(DIRECCION_XML + "ventas.xml"))
            {
                dtVentas.ReadXml(DIRECCION_XML + "ventas.xml");
            }
        }
        private void Leer_DTCalculos()
        {
            if (System.IO.File.Exists(DIRECCION_XML + "calculos.xml"))
            {
                dtVentas.ReadXml(DIRECCION_XML + "calculos.xml");
            }
        }

        //Metodo para limpiar los textboxs
        private void LimpiarTextos()
        {
            txtNVenta.Text = "";
            txtArticulo.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        public int BuscarVenta(string code)
        {
            int fila = -1;

            for (int i = 0; i < dtVentas.Rows.Count; i++)
            {
                if (dtVentas.Rows[i]["Nº Venta"].ToString() == code)
                {
                    fila = i;
                    break;
                }
            }
            return fila;
        }


    }
}
