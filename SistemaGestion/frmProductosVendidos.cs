using CapaDatos;
using CapaEntidad;
using SistemaGestion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemaGestion
{
    public partial class frmProductosVendidos : Form
    {
        public frmProductosVendidos()
        {
            InitializeComponent();
        }

        private void CargarLista()
        {
            dgvData.Rows.Clear();
            List<ProductoVendido> listaProductosVendidos = new CN_ProductoVendido().ListarProductoVendidos();

            dgvData.Columns["PrecioVenta"].DefaultCellStyle.Format = "N2";
            foreach (ProductoVendido item in listaProductosVendidos)
            {
                
                Producto producto = CN_Producto.ObtenerProducto(item.productoId);
                dgvData.Rows.Add(new object[] { "", item.id, item.productoId, producto.descripciones, item.stock, producto.precioVenta, item.ventaId });
            }
        }

        private void frmProductosVendidos_Load(object sender, EventArgs e)
        {
            lblOperacion.Text = "ALTA NUEVO PRODUCTO VENDIDO";
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

                }


            }

            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
            CargarLista();
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdProductoVendido.Text = "0";
            txtDescripciones.Text = "";
            txtStock.Text = "";
            txtPrecioVenta.Text = "";
            txtIdVenta.Text = "";
            txtIdProducto.Text = "";

            txtDescripciones.Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtProductoSeleccionado.Text = dgvData.Rows[indice].Cells["Descripciones"].Value.ToString();
                    txtIdProductoVendido.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIdProducto.Text = dgvData.Rows[indice].Cells["IdProducto"].Value.ToString();
                    txtDescripciones.Text = dgvData.Rows[indice].Cells["Descripciones"].Value.ToString();
                    txtStock.Text = dgvData.Rows[indice].Cells["Stock"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["PrecioVenta"].Value.ToString();
                    txtIdVenta.Text = dgvData.Rows[indice].Cells["IdVenta"].Value.ToString();





                }

            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Width - h) / 2;
                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void txtIdProductoVendido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto producto = CN_Producto.ObtenerProducto(Convert.ToInt32(txtIdProducto.Text));
                txtDescripciones.Text = producto.descripciones;
                txtPrecioVenta.Value = producto.precioVenta;
                txtStock.Select();

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            ProductoVendido objProductoVendido = new ProductoVendido()
            {
                id = Convert.ToInt32(txtIdProductoVendido.Text),
                stock = Convert.ToInt32(txtStock.Text),
                productoId = Convert.ToInt32(txtIdProducto.Text),
                ventaId = Convert.ToInt32(txtIdVenta.Text)




            };

            if (txtIdProductoVendido.Text == "0")
            {

                CN_ProductoVendido.CrearProductoVendido(objProductoVendido);



                dgvData.Rows.Add(new object[] { "", objProductoVendido.id, txtDescripciones.Text, txtStock.Text, txtPrecioVenta.Text, txtIdVenta.Text });

                Limpiar();



            }
            else
            {

                CN_ProductoVendido.ModificarProductoVendido(objProductoVendido);
                if (txtIdProductoVendido.Text != "0")
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtIdProductoVendido.Text;
                    row.Cells["Descripciones"].Value = txtDescripciones.Text;
                    row.Cells["Stock"].Value = txtStock.Text;
                    row.Cells["PrecioVenta"].Value = txtPrecioVenta.Text;
                    row.Cells["IdVenta"].Value = txtIdVenta.Text;



                    Limpiar();
                    MessageBox.Show("Datos Modificados.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                else
                {

                    MessageBox.Show(mensaje);
                }
            }
            CargarLista();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProducto.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Producto Vendido?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    ProductoVendido objProducto = new ProductoVendido()
                    {
                        id = Convert.ToInt32(txtIdProducto.Text),
                        
                    };

                    CN_ProductoVendido.EliminarProductoVendido(objProducto);



                    dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    txtIndice.Text = "-1";
                    txtIdProducto.Text = "0";












                }

            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;


                }

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
        }

        private void txtIdProductoVendido_TextChanged(object sender, EventArgs e)
        {

            if (txtIdProductoVendido.Text == "0")
                lblOperacion.Text = "ALTA NUEVO PRODUCTO VENIDO";
            else
                lblOperacion.Text = "MODIFICAR PRODUCTO VENDIDO";
        }
    }
}
