﻿using System;
using System.Data;
using System.Windows.Forms;
using Cls_BLL.Catalogos.Estados;
using Cls_BLL.Catalogos.Tipo_Usuario;
using Cls_DAL.Catalogos.Estados;
using Cls_DAL.Catalogos.Log_in;
using Cls_DAL.Catalogos.Tipo_Usuario;
using Cls_PL.Pantallas.Catalogos.Modificar;

namespace Cls_PL
{
    public partial class Frm_Listar_Tipo_Usuario : Form
    {
        #region Variables

        private Frm_Modificar_Tipo_Usuario Obj_Pant_Mod_Tipo_Usuario = new Frm_Modificar_Tipo_Usuario();
        cls_Tipo_Usuario_DAL Obj_Cls_Tipo_Usuario_DAL = new cls_Tipo_Usuario_DAL();
        cls_Tipo_Usuario_BLL Obj_Cls_Tipo_Usuario_BLL = new cls_Tipo_Usuario_BLL();
        public Cls_Tabla_LogIn_DAL Obj_Login_DAL = new Cls_Tabla_LogIn_DAL();
        cls_Estados_BLL Obj_Estados_BLL = new cls_Estados_BLL();
        DataTable dtEstados = new DataTable();
        private string sMensajeError;
       
        #endregion 

        public Frm_Listar_Tipo_Usuario()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Opciones();
            Cargar();
        }

        private void tlsbtn_Nuevo_Click(object sender, EventArgs e)
        {
            Obj_Cls_Tipo_Usuario_DAL.bAccion = true; // Es un insert
            Obj_Cls_Tipo_Usuario_DAL.chrId_Estado = ' ';
            Obj_Cls_Tipo_Usuario_DAL.sDescripcion = string.Empty;
            Obj_Cls_Tipo_Usuario_DAL.iId_Tipo_Usuario = 0;
            Obj_Pant_Mod_Tipo_Usuario.Obj_Cls_Tipo_Empleado_DAL = Obj_Cls_Tipo_Usuario_DAL;
            Obj_Pant_Mod_Tipo_Usuario.ShowDialog();
        }

        private void tlsbtn_Editar_Click(object sender, EventArgs e)
        {
            if (dgv_Tipo_Usuarios.RowCount == 0)
            {
                MessageBox.Show("No ha seleccionado un Tipo de Usuario para ser modificado.",
                                "Información",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                Cargar_Fila();
            }
        }

        private void tlsbtn_Salir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tlsbtn_Refrescar_Click(object sender, EventArgs e)
        {
            Cargar();
        }

        private void dgv_Estados_DoubleClick(object sender, EventArgs e)
        {
            if (dgv_Tipo_Usuarios.RowCount == 0)
            {
                MessageBox.Show("No ha seleccionado un Tipo de Usuario para ser modificado.",
                                "Información",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                Cargar_Fila();
            }
        }

        // Metodos de cada boton
        private void Eliminar_Tipo_Usario()
        {
            if (dgv_Tipo_Usuarios.SelectedRows.Count > 0)
            {
                Obj_Cls_Tipo_Usuario_DAL.bEstado_Ejec = true;
                Obj_Cls_Tipo_Usuario_DAL.iId_Tipo_Usuario = Convert.ToInt32(dgv_Tipo_Usuarios.SelectedRows[0].Cells[0].Value.ToString());
                Obj_Cls_Tipo_Usuario_DAL.sDescripcion = dgv_Tipo_Usuarios.SelectedRows[0].Cells[1].Value.ToString().Trim();
                Obj_Cls_Tipo_Usuario_DAL.chrId_Estado = Convert.ToChar(dgv_Tipo_Usuarios.SelectedRows[0].Cells[2].Value.ToString().Trim());
                if (MessageBox.Show("Está seguro de que desea eliminar el registro", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Obj_Cls_Tipo_Usuario_BLL.Eliminar_Tipo_Usuario_SP(ref Obj_Cls_Tipo_Usuario_DAL, ref sMensajeError);

                    if (Obj_Cls_Tipo_Usuario_DAL.bEstado_Ejec == true)
                    {
                        MessageBox.Show("El registro a sido eliminado satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cargar();
                    }
                    else
                    {
                        MessageBox.Show("Problemas a la hora de la eliminar de la información.\n" +
                               "Mensaje del SQL: " + sMensajeError,
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un Row antes de modificar un dato ", "Adevertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Cargar()
        {
            DataTable dt = new DataTable();

            if (Obj_Cls_Tipo_Usuario_BLL.Listar_Tipo_Usuario_SP(ref dt, ref sMensajeError))
            {
                dgv_Tipo_Usuarios.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Problemas a la hora de la carga de la información.\n" +
                                "Mensaje del SQL: " + sMensajeError,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Cargar_Fila()
        {
            Obj_Cls_Tipo_Usuario_DAL.bAccion = false; //Update
            //Captura los valores del row para cargarlos en un objeto
            Obj_Cls_Tipo_Usuario_DAL.iId_Tipo_Usuario = Convert.ToInt32(dgv_Tipo_Usuarios.SelectedRows[0].Cells[0].Value.ToString().Trim());
            Obj_Cls_Tipo_Usuario_DAL.sDescripcion = dgv_Tipo_Usuarios.SelectedRows[0].Cells[1].Value.ToString().Trim();
            Obj_Cls_Tipo_Usuario_DAL.chrId_Estado = Convert.ToChar(dgv_Tipo_Usuarios.SelectedRows[0].Cells[2].Value.ToString().Trim());
            // Carga los valores del row en un objeto y los envia a la pantalla de edicion
            Obj_Pant_Mod_Tipo_Usuario.Obj_Cls_Tipo_Empleado_DAL = Obj_Cls_Tipo_Usuario_DAL;
            Obj_Pant_Mod_Tipo_Usuario.ShowDialog();
        }

        private void tlsbtn_Eliminar_Click_1(object sender, EventArgs e)
        {
            Eliminar_Tipo_Usario();
        }

        private void tlstrp_txt_Busqueda_TextChanged(object sender, EventArgs e)
        {
            DataTable dt_f = new DataTable();

            if (Obj_Cls_Tipo_Usuario_BLL.Filtrar_Tipo_Usuarios_SP(ref dt_f, tlstrp_txt_Busqueda.Text, ref sMensajeError))
            {
                dgv_Tipo_Usuarios.DataSource = dt_f;
            }
            else
            {
                MessageBox.Show("Problemas a la hora de la carga de la información.\n" +
                                "Mensaje error: " + sMensajeError,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Opciones()
        {
            switch (Obj_Login_DAL.iRol)
            {
                case 1:// administrador

                    tlsbtn_Editar.Enabled = true;
                    tlsbtn_Nuevo.Enabled = true;
                    tlsbtn_Eliminar.Enabled = true;
                    break;

                case 2:// Medio

                    tlsbtn_Editar.Enabled = true;
                    tlsbtn_Nuevo.Enabled = true;
                    tlsbtn_Eliminar.Enabled = false;
                    break;

                case 3: // solo lectura

                    tlsbtn_Editar.Enabled = false;
                    tlsbtn_Nuevo.Enabled = false;
                    tlsbtn_Eliminar.Enabled = false;
                    break;

                default:

                    tlsbtn_Editar.Enabled = false;
                    tlsbtn_Nuevo.Enabled = false;
                    tlsbtn_Eliminar.Enabled = false;
                    break;
            }
        }
    }
}