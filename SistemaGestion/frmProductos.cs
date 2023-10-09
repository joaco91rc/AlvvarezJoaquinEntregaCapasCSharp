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
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void CargarLista()
        {
            dgvData.Rows.Clear();
            List<Producto> listaProducto = new CN_Producto().ListarProductos();
            dgvData.Columns["Costo"].DefaultCellStyle.Format = "N2";
            dgvData.Columns["PrecioVenta"].DefaultCellStyle.Format = "N2";
            foreach (Producto item in listaProducto)
            {
                dgvData.Rows.Add(new object[] { "", item.id, item.descripciones, item.costo, item.precioVenta, item.stock, item.idUsuario });
            }
        }
        private void frmProductos_Load(object sender, EventArgs e)
        {
            lblOperacion.Text = "ALTA NUEVO PRODUCTO";
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
            txtIdProducto.Text = "0";
            txtDescripciones.Text = "";
            txtCosto.Text = "";
            txtPrecioVenta.Text = "";
            txtStock.Text = "";
            txtIdUsuario.Text = "";

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
                    txtIdProducto.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDescripciones.Text = dgvData.Rows[indice].Cells["Descripciones"].Value.ToString();
                    txtCosto.Text = dgvData.Rows[indice].Cells["Costo"].Value.ToString();
                    txtPrecioVenta.Text = dgvData.Rows[indice].Cells["PrecioVenta"].Value.ToString();
                    txtStock.Text = dgvData.Rows[indice].Cells["Stock"].Value.ToString();
                    txtIdUsuario.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();




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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Producto objProducto = new Producto()
            {
                id = Convert.ToInt32(txtIdProducto.Text),
                descripciones = txtDescripciones.Text,
                costo = Convert.ToDecimal(txtCosto.Text),
                precioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                stock = Convert.ToInt32(txtStock.Text),
                idUsuario = Convert.ToInt32(txtIdUsuario.Text)

            };

            if (txtIdProducto.Text == "0")
            {

                CN_Producto.CrearProducto(objProducto);



                dgvData.Rows.Add(new object[] { "", objProducto.id, txtDescripciones.Text, txtCosto.Text, txtPrecioVenta.Text, txtStock.Text, txtIdUsuario.Text });

                Limpiar();



            }
            else
            {

                CN_Producto.ModificarProducto(objProducto);
                if (txtIdProducto.Text != "0")
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtIdProducto.Text;
                    row.Cells["Descripciones"].Value = txtDescripciones.Text;
                    row.Cells["Costo"].Value = txtCosto.Text;
                    row.Cells["PrecioVenta"].Value = txtPrecioVenta.Text;
                    row.Cells["Stock"].Value = txtStock.Text;
                    row.Cells["IdUsuario"].Value = txtIdUsuario.Text;



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

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProducto.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el Producto?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    Producto objProducto = new Producto()
                    {
                        id = Convert.ToInt32(txtIdProducto.Text),

                    };

                    ProductoData.EliminarProducto(objProducto);



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
        //boton limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            foreach (DataGridViewRow row in dgvData.Rows)
                row.Visible = true;
        }

        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

            if (txtIdProducto.Text == "0")
                lblOperacion.Text = "ALTA NUEVO PRODUCTO";
            else
                lblOperacion.Text = "MODIFICAR PRODUCTO";
        }
    }
}
