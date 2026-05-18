using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ActividadAlgebraRelacional
{
    public partial class FormNorthwind : Form
    {
        public FormNorthwind()
        {
            InitializeComponent();
        }

        // =========================================================================
        // PARTE 1: OPERACIONES UNARIAS (Northwind)
        // =========================================================================

        #region Operaciones Unarias - Selección (σ)

        // Botón 1: Selección - Clientes de México
        private void btnSeleccion1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from C in northwind.Customers
                               where C.Country == "Mexico"
                               select C;

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 2: Selección - Empleados de Londres
        private void btnSeleccion2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from E in northwind.Employee
                               where E.City == "London"
                               select E;

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 3: Selección - Órdenes enviadas a Francia
        private void btnSeleccion3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from O in northwind.Orders
                               where O.ShipCountry == "France"
                               select O;

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        #endregion

        #region Operaciones Unarias - Proyección (π)

        // Botón 4: Proyección - Columnas esenciales de Productos
        private void btnProyeccion1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from P in northwind.Product
                               select new
                               {
                                   P.ProductID,
                                   P.ProductName,
                                   P.UnitPrice
                               };

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 5: Proyección - Contacto básico de Clientes
        private void btnProyeccion2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from C in northwind.Customers
                               select new
                               {
                                   C.CustomerID,
                                   C.ContactName,
                                   C.Phone
                               };

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 6: Proyección - Distribución geográfica de Clientes (Sin Duplicados)
        private void btnProyeccion3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = (from C in northwind.Customers
                                select new
                                {
                                    C.City,
                                    C.Country
                                }).Distinct();

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        #endregion

        #region Operaciones Unarias - Renombramiento (ρ)

        // Botón 7: Renombramiento - Traducción de atributos de Empleados
        private void btnRenombramiento1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from E in northwind.Employee
                               select new
                               {
                                   CodigoEmpleado = E.EmployeeID,
                                   Nombre = E.FirstName,
                                   Apellido = E.LastName
                               };

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 8: Renombramiento - Traducción de atributos de Transportistas
        private void btnRenombramiento2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from S in northwind.Shipper
                               select new
                               {
                                   CodigoEnvio = S.ShipperID,
                                   EmpresaTransporte = S.CompanyName,
                                   NumeroContacto = S.Phone
                               };

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        // Botón 9: Renombramiento - Alias personalizados para Categorías
        private void btnRenombramiento3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta = from C in northwind.Category
                               select new
                               {
                                   CodigoCategoria = C.CategoryID,
                                   NombreCategoria = C.CategoryName,
                                   Detalle = C.Description
                               };

                dgvNorthwind.DataSource = consulta.ToList();
            }
        }

        #endregion

        // =========================================================================
        // PARTE 2: OPERACIONES BINARIAS (Northwind)
        // =========================================================================

        #region Operaciones Binarias - Unión (∪)

        // Botón 10: Unión - Clientes de Alemania y Francia
        private void btnUnion1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from C in northwind.Customers
                                where C.Country == "Germany"
                                select new { C.CustomerID, C.CompanyName, C.Country };

                var consulta2 = from C in northwind.Customers
                                where C.Country == "France"
                                select new { C.CustomerID, C.CompanyName, C.Country };

                var consultaFinal = consulta1.Union(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 11: Unión - Productos de Categorías 1 y 2
        private void btnUnion2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from P in northwind.Product
                                where P.CategoryID == 1
                                select new { P.ProductID, P.ProductName, P.CategoryID };

                var consulta2 = from P in northwind.Product
                                where P.CategoryID == 2
                                select new { P.ProductID, P.ProductName, P.CategoryID };

                var consultaFinal = consulta1.Union(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 12: Unión - Empleados basados en Londres y Seattle
        private void btnUnion3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from E in northwind.Employee
                                where E.City == "London"
                                select new { E.EmployeeID, E.FirstName, E.LastName, E.City };

                var consulta2 = from E in northwind.Employee
                                where E.City == "Seattle"
                                select new { E.EmployeeID, E.FirstName, E.LastName, E.City };

                var consultaFinal = consulta1.Union(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        #endregion

        #region Operaciones Binarias - Diferencia (-)

        // Botón 13: Diferencia - Clientes globales exceptuando los de USA
        private void btnDiferencia1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from C in northwind.Customers
                                select new { C.CustomerID, C.CompanyName, C.Country };

                var consulta2 = from C in northwind.Customers
                                where C.Country == "USA"
                                select new { C.CustomerID, C.CompanyName, C.Country };

                var consultaFinal = consulta1.Except(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 14: Diferencia - Catálogo total exceptuando productos de Categoría 1
        private void btnDiferencia2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from P in northwind.Product
                                select new { P.ProductID, P.ProductName, P.CategoryID };

                var consulta2 = from P in northwind.Product
                                where P.CategoryID == 1
                                select new { P.ProductID, P.ProductName, P.CategoryID };

                var consultaFinal = consulta1.Except(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 15: Diferencia - Órdenes de un Empleado exceptuando envíos a Brasil
        private void btnDiferencia3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consulta1 = from O in northwind.Orders
                                where O.EmployeeID == 5
                                select new { O.OrderID, O.CustomerID, O.ShipCountry };

                var consulta2 = from O in northwind.Orders
                                where O.ShipCountry == "Brazil"
                                select new { O.OrderID, O.CustomerID, O.ShipCountry };

                var consultaFinal = consulta1.Except(consulta2);
                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        #endregion

        #region Operaciones Binarias - Producto Cartesiano (×)

        // Botón 16: Producto Cartesiano - Cruce Completo Empleados × Regiones
        private void btnCartesiano1_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consultaFinal = from E in northwind.Employee
                                    from R in northwind.Region
                                    select new
                                    {
                                        E.EmployeeID,
                                        E.LastName,
                                        R.RegionID,
                                        R.RegionDescription
                                    };

                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 17: Producto Cartesiano - Cruce Completo Categorías × Transportistas
        private void btnCartesiano2_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consultaFinal = from C in northwind.Category
                                    from S in northwind.Shipper
                                    select new
                                    {
                                        C.CategoryID,
                                        C.CategoryName,
                                        S.ShipperID,
                                        S.CompanyName
                                    };

                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 18: Producto Cartesiano - Cruce Segmentado Clientes (México) × Territorios
        private void btnCartesiano3_Click(object sender, EventArgs e)
        {
            using (NorthwindEntities northwind = new NorthwindEntities())
            {
                var consultaFinal = from C in northwind.Customers
                                    where C.Country == "Mexico"
                                    from T in northwind.Territory
                                    select new
                                    {
                                        C.CustomerID,
                                        C.CompanyName,
                                        T.TerritoryID,
                                        T.TerritoryDescription
                                    };

                dgvNorthwind.DataSource = consultaFinal.ToList();
            }
        }

        #endregion
    }
}
