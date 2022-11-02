using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Venta
    {
        //Atributos
        private int nVenta;
        private string articulo;
        private decimal precio;
        private int cantidad;
        private decimal total = 0;
        private int cantTotal = 0;


        //Constructor vacio
        public Venta()
        {

        }
        //constructor con paràmetros
        public Venta(int nVenta, string articulo, decimal precio, int cantidad)
        {
            NVenta = nVenta;
            Articulo = articulo;
            Precio = precio;
            Cantidad = cantidad;
        }

        public int NVenta { get; set; }
        public string Articulo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public int CantVentasSuma()
        {
            int cantidadVentas = 0;

            cantidadVentas = cantidadVentas +1;

            return cantidadVentas;
        }
        private decimal Monto()
        {
            decimal monto = 0;
            monto = Precio * Cantidad;
            return monto;
        }
        public decimal MontoTotal()
        {            
            total = total + Monto();
            return total;
        }
        public int CantArticulos()
        {
            cantTotal = cantTotal + Cantidad;
            return cantTotal;
        }
    }
}
