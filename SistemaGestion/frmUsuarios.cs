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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void CargarLista()
        {
            dgvData.Rows.Clear();
            List<Usuario> listaUsuario = new CN_Usuario().ListarUsuarios();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { "", item.id, item.nombre, item.apellido, item.nombreUsuario, item.contrasenia, item.mail });
            }
        }
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text == "0")
                lblOperacion.Text = "ALTA NUEVO USUARIO";
            

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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtUsuarioSeleccionado.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString() + "  " + dgvData.Rows[indice].Cells["Apellido"].Value.ToString(); 
                    txtIdUsuario.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtNombreUsuario.Text = dgvData.Rows[indice].Cells["NombreUsuario"].Value.ToString();
                    txtContrasenia.Text = dgvData.Rows[indice].Cells["Contrasenia"].Value.ToString();
                    txtMail.Text = dgvData.Rows[indice].Cells["Mail"].Value.ToString();

                    


                }

            }
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdUsuario.Text = "0";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNombreUsuario.Text = "";
            txtContrasenia.Text = "";
            txtMail.Text = "";
            
            txtNombre.Select();
        }

        private void btnLimpiarDatos_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Usuario objUsuario = new Usuario()
            {
                id = Convert.ToInt32(txtIdUsuario.Text),
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                nombreUsuario = txtNombreUsuario.Text,
                contrasenia = txtContrasenia.Text,
                mail= txtMail.Text
                
            };

            if (txtIdUsuario.Text == "0")
            {
                CN_Usuario.CrearUsuario(objUsuario);
                 


                
                    dgvData.Rows.Add(new object[] { "",objUsuario.id,txtNombre.Text,txtApellido.Text,txtNombreUsuario.Text,txtContrasenia.Text, txtMail.Text});

                    Limpiar();
               


            }
            else
            {
                

                if (txtIdUsuario.Text !="0")
                {
                    CN_Usuario.ModificarUsuario(objUsuario);
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtIdUsuario.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["NombreUsuario"].Value = txtNombreUsuario.Text;
                    row.Cells["Contrasenia"].Value = txtContrasenia.Text;
                    row.Cells["Mail"].Value = txtMail.Text;
                    


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
            if (Convert.ToInt32(txtIdUsuario.Text) != 0)
            {

                if (MessageBox.Show("Desea eliminar el usuario?", "Confirmar Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    Usuario objUsuario = new Usuario()
                    {
                        id = Convert.ToInt32(txtIdUsuario.Text),

                    };

                    CN_Usuario.EliminarUsuario(objUsuario);

                     

                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        txtIndice.Text = "-1";
                        txtIdUsuario.Text = "0";
                    

                    

                       


                    




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

        private void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text == "0")
                lblOperacion.Text = "ALTA NUEVO USUARIO";
            else
                lblOperacion.Text = "MODIFICAR USUARIO";
        }
    }
}
