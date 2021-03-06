﻿using System;
using System.Windows.Forms;
using Cls_BLL.Catalogos.Estados;
using Cls_DAL.Catalogos.Estados;

namespace Cls_PL.Pantallas.Catalogos.Modificar
{
    public partial class Frm_Modificar_Estados : Form
    {
        #region Variables Globales

            public cls_Estados_BLL Obj_Cls_Estado_BLL = new cls_Estados_BLL();
            public cls_Estados_DAL Obj_Cls_Estado_DAL = new cls_Estados_DAL();
            private string sMensajeError;

        #endregion

        public Frm_Modificar_Estados()
        {
            InitializeComponent();
        }

        private void Frm_Modificar_Estados_Load(object sender, EventArgs e)
        {
            CargarDatosClasetipada();
        }  

        private void tlsbtn_Guardar_Click(object sender, EventArgs e)
        {
            if (txt_Codigo_Estado.Text == string.Empty || txt_Descripcion.Text == string.Empty )
            {
                MessageBox.Show("El esado no fue guardado. Hay datos incompletos.",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                Obj_Cls_Estado_DAL.chrIdEstado = Convert.ToChar(txt_Codigo_Estado.Text.Trim());
                Obj_Cls_Estado_DAL.sDescripcion = txt_Descripcion.Text.Trim();

                if (Obj_Cls_Estado_DAL.bAccion == true)
                {
                    Guardar();
                    CargarDatosClasetipada();
                }
                else
                {
                    Modificar();
                    CargarDatosClasetipada();
                }
            }
        }

        private void tlsbtn_Salir_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Metodos de los botones
        private void CargarDatosClasetipada()
        {
            if (Obj_Cls_Estado_DAL.bAccion) //  == true
            {
                // No hay accion a realizar
                txt_Codigo_Estado.Enabled = true;
                txt_Codigo_Estado.Text = string.Empty;
                txt_Descripcion.Text = string.Empty;
            }
            else
            {
                // Cargar los datos del objeto en pantalla
                txt_Codigo_Estado.Enabled = false;
                txt_Codigo_Estado.Text = Obj_Cls_Estado_DAL.chrIdEstado.ToString().Trim();
                txt_Descripcion.Text = Obj_Cls_Estado_DAL.sDescripcion.Trim();
            }
        }

        private void Guardar()
        {
            Obj_Cls_Estado_BLL.Insertar_Estados_SP(ref Obj_Cls_Estado_DAL, ref sMensajeError);

            if (Obj_Cls_Estado_DAL.bEstado_Ejec == true)
            {
                MessageBox.Show("El estado fue guardado correctamente.",
                              "Confirmación",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);

                Obj_Cls_Estado_DAL.bAccion = false;// update
            }
            else
            {
                MessageBox.Show("El estado nuevo no fue guardado, hubo un error en la ejecucíón.\n" +
                               "Mensaje del SQL: " + sMensajeError,
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);

                Obj_Cls_Estado_DAL.bAccion = true; // insert
            }
        }

        private void Modificar()
        {
            Obj_Cls_Estado_BLL.ModificarEstados_SP(ref Obj_Cls_Estado_DAL, ref sMensajeError);

            if (Obj_Cls_Estado_DAL.bEstado_Ejec == true)
            {
                MessageBox.Show("El estado fue actualizado correctamente.",
                              "Confirmación",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);

                Obj_Cls_Estado_DAL.bAccion = false;
            }
            else
            {
                MessageBox.Show("El estado no se pudo actualizar, hubo un error en la ejecucíón.\n" +
                               "Mensaje error: " + sMensajeError,
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);

                Obj_Cls_Estado_DAL.bAccion = true;
            }
        }

        private void Frm_Modificar_Estados_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Se perderán los datos no salvados. ¿Desea salir?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
