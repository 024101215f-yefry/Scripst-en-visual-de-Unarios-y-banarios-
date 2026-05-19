using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ActividadAlgebraRelacional
{
    public partial class FormPubs : Form
    {
        public FormPubs()
        {
            InitializeComponent();
        }

        // =========================================================================
        // PARTE 1: OPERACIONES UNARIAS (Base de datos: Pubs)
        // =========================================================================

        #region Operaciones Unarias - Selección (σ)

        // Botón 19: Selección - Autores que viven en California ("CA")
        private void btnPubsSeleccion1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from A in pubs.authors
                               where A.state == "CA"
                               select A;

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 20: Selección - Libros de tipo 'business'
        private void btnPubsSeleccion2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from T in pubs.titles
                               where T.type == "business"
                               select T;

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 21: Selección - Tiendas en la ciudad de 'Seattle'
        private void btnPubsSeleccion3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from S in pubs.stores
                               where S.city == "Seattle"
                               select S;

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        #endregion

        #region Operaciones Unarias - Proyección (π)

        // Botón 22: Proyección - Mostrar solo Nombre, Apellido y Teléfono de Autores
        private void btnPubsProyeccion1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from A in pubs.authors
                               select new
                               {
                                   A.au_fname,
                                   A.au_lname,
                                   A.phone
                               };

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 23: Proyección - Columnas ID, Título y Precio de la tabla 'titles'
        private void btnPubsProyeccion2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from T in pubs.titles
                               select new
                               {
                                   T.title_id,
                                   T.title,
                                   T.price
                               };

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 24: Proyección - Ciudades y Países de Editoriales (Sin Duplicados)
        private void btnPubsProyeccion3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = (from P in pubs.publishers
                                select new
                                {
                                    P.city,
                                    P.country
                                }).Distinct();

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        #endregion

        #region Operaciones Unarias - Renombramiento (ρ)

        // Botón 25: Renombramiento - Traducir atributos de la tabla 'titles'
        private void btnPubsRenombramiento1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from T in pubs.titles
                               select new
                               {
                                   CodigoLibro = T.title_id,
                                   NombreLibro = T.title,
                                   PrecioVenta = T.price,
                                   FechaPublicacion = T.pubdate
                               };

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 26: Renombramiento - Personalizar columnas de la tabla 'publishers'
        private void btnPubsRenombramiento2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from P in pubs.publishers
                               select new
                               {
                                   IdEditorial = P.pub_id,
                                   EmpresaPublicadora = P.pub_name,
                                   CiudadSede = P.city
                               };

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        // Botón 27: Renombramiento - Traducir campos informativos de los Empleados
        private void btnPubsRenombramiento3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta = from E in pubs.employee
                               select new
                               {
                                   IdentificadorEmpleado = E.emp_id,
                                   PrimerNombre = E.fname,
                                   ApellidoPaterno = E.lname,
                                   FechaIngreso = E.hire_date
                               };

                dgvPubs.DataSource = consulta.ToList();
            }
        }

        #endregion

        // =========================================================================
        // PARTE 2: OPERACIONES BINARIAS (Base de datos: Pubs)
        // =========================================================================

        #region Operaciones Binarias - Unión (∪)

        // Botón 28: Unión - Autores de California (CA) y Autores de Utah (UT)
        private void btnPubsUnion1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from A in pubs.authors
                                where A.state == "CA"
                                select new { A.au_id, A.city, A.state };

                var consulta2 = from A in pubs.authors
                                where A.state == "UT"
                                select new { A.au_id, A.city, A.state };

                var consultaFinal = consulta1.Union(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 29: Unión - Libros de tipo 'business' y libros de tipo 'psychology'
        private void btnPubsUnion2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from T in pubs.titles
                                where T.type == "business"
                                select new { T.title_id, T.title, T.type };

                var consulta2 = from T in pubs.titles
                                where T.type == "psychology"
                                select new { T.title_id, T.title, T.type };

                var consultaFinal = consulta1.Union(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 30: Unión - Tiendas de California (CA) y Tiendas de Washington (WA)
        private void btnPubsUnion3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from S in pubs.stores
                                where S.state == "CA"
                                select new { S.stor_id, S.stor_name, S.state };

                var consulta2 = from S in pubs.stores
                                where S.state == "WA"
                                select new { S.stor_id, S.stor_name, S.state };

                var consultaFinal = consulta1.Union(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        #endregion

        #region Operaciones Binarias - Diferencia (-)

        // Botón 31: Diferencia - Todos los Libros con precio > 15 exceptuando ventas < 4000
        private void btnPubsDiferencia1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from T in pubs.titles
                                where T.price > 15
                                select new { T.title_id, T.title, T.price };

                var consulta2 = from T in pubs.titles
                                where T.ytd_sales < 4000
                                select new { T.title_id, T.title, T.price };

                var consultaFinal = consulta1.Except(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 32: Diferencia - Todos los Autores exceptuando los que vivan en California (CA)
        private void btnPubsDiferencia2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from A in pubs.authors
                                select new { A.au_id, A.au_fname, A.au_lname, A.state };

                var consulta2 = from A in pubs.authors
                                where A.state == "CA"
                                select new { A.au_id, A.au_fname, A.au_lname, A.state };

                var consultaFinal = consulta1.Except(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 33: Diferencia - Todas las Editoriales globales exceptuando las de USA
        private void btnPubsDiferencia3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consulta1 = from P in pubs.publishers
                                select new { P.pub_id, P.pub_name, P.country };

                var consulta2 = from P in pubs.publishers
                                where P.country == "USA"
                                select new { P.pub_id, P.pub_name, P.country };

                var consultaFinal = consulta1.Except(consulta2);
                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        #endregion

        #region Operaciones Binarias - Producto Cartesiano (×)

        // Botón 34: Producto Cartesiano - Cruce Total Autores × Editoriales
        private void btnPubsCartesiano1_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consultaFinal = from A in pubs.authors
                                    from P in pubs.publishers
                                    select new
                                    {
                                        A.au_id,
                                        A.au_fname,
                                        P.pub_id,
                                        P.pub_name
                                    };

                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 35: Producto Cartesiano - Cruce Total Tiendas × Libros (Catálogo)
        private void btnPubsCartesiano2_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consultaFinal = from S in pubs.stores
                                    from T in pubs.titles
                                    select new
                                    {
                                        S.stor_id,
                                        S.stor_name,
                                        T.title_id,
                                        T.title
                                    };

                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        // Botón 36: Producto Cartesiano - Cruce Total Empleados × Descuentos disponibles
        private void btnPubsCartesiano3_Click(object sender, EventArgs e)
        {
            using (pubslasfuerzasEntities pubs = new pubslasfuerzasEntities())
            {
                var consultaFinal = from E in pubs.employee
                                    from D in pubs.discounts
                                    select new
                                    {
                                        E.emp_id,
                                        E.lname,
                                        D.discounttype,
                                        D.discount
                                    };

                dgvPubs.DataSource = consultaFinal.ToList();
            }
        }

        #endregion
    }
}
