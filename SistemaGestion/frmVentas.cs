using CapaDatos;
using CapaEntidad;
using CapaNegocio;
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

namespace SistemaGestion
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }

        private void CargarLista()
        {
            dgvData.Rows.Clear();
            List<Venta> listaVentas = new CN_Venta().ListarVentas();
            
            foreach (Venta item in listaVentas)
            {
                Usuario usuario = CN_Usuario.ObtenerUsuario(item.idUsuario);
                dgvData.Rows.Add(new object[] { "", item.id, usuario.nombreUsuario,item.idUsuario, item.comentarios });
            }
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            lblOperacion.Text = "ALTA NUEVA VENTA";

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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtVentaSeleccionada.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIdVenta.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtComentarios.Text = dgvData.Rows[indice].Cells["Comentarios"].Value.ToString();                    
                    txtNombreUsuario.Text = dgvData.Rows[indice].Cells["NombreUsuario"].Value.ToString();
                    txtIdUsuario.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();




                }

            }
        }
        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdVenta.Text = "0";
            txtComentarios.Text = "";
            txtNombreUsuario.Text = "";
            txtIdUsuario.Text = "";
           

            txtComentarios.Select();
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
            Venta objVenta = new Venta()
            {
                id = Convert.ToInt32(txtIdVenta.Text),
                comentarios = txtComentarios.Text,
                idUsuario = Convert.ToInt32(txtIdUsuario.Text)

            };

            if (txtIdVenta.Text == "0")
            {

                CN_Venta.CrearVenta(objVenta);

                Usuario usuario = CN_Usuario.ObtenerUsuario(objVenta.idUsuario);

                dgvData.Rows.Add(new object[] { "", txtIdVenta.Text, txtNombreUsuario.Text, txtIdUsuario.Text, txtComentarios.Text });

                Limpiar();



            }
            else
            {

                CN_Venta.ModificarVenta(objVenta);
                if (txtIdVenta.Text != "0")
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtIdVenta.Text;
                    row.Cells["Comentarios"].Value = txtComentarios.Text;
                    row.Cells["NombreUsuario"].Value = txtNombreUsuario.Text;
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

        private void txtIdUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Usuario usuario = CN_Usuario.ObtenerUsuario(Convert.ToInt32(txtIdUsuario.Text));
                txtNombreUsuario.Text =usuario.nombreUsuario;
                

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

        private void txtIdVenta_TextChanged(object sender, EventArgs e)
        {

            if (txtIdVenta.Text == "0")
                lblOperacion.Text = "ALTA NUEVA VENTA";
            else
                lblOperacion.Text = "MODIFICAR VENTA";
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
    }

