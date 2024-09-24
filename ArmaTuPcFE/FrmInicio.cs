using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArmaTuPcFE
{
    public partial class FrmInicio : Form
    {
        private List<Procesador> procesadores;
        private List<Componentes> placamadre;
        private List<Componentes> memoria;
        private List<Componentes> almacenamiento;
        private List<Componentes> gpu;
        private List<Componentes> fuente;
        private List<Componentes> gabinete;
        private GestorComponentes gestorcomponentes;

        public FrmInicio()
        {
            InitializeComponent();
            gestorcomponentes = new GestorComponentes();
            CargarComponentes();
        }

        private void CargarComponentes()
        {
            // Carga de datos de cada componente
            procesadores = gestorcomponentes.CargarProcesadores();
            cboTipo.DataSource = procesadores;
            cboTipo.DisplayMember = "Marca";

            memoria = gestorcomponentes.CargarMemoria();
            cboMemoria.DataSource = memoria;
            

            almacenamiento = gestorcomponentes.CargarMemoria();
            cboAlmacenamiento.DataSource = almacenamiento;
            

            gpu = gestorcomponentes.CargarGPU();
            cboGpu.DataSource = gpu;
            

            fuente = gestorcomponentes.CargarFuente();
            cboFuente.DataSource = fuente;
            

            gabinete = gestorcomponentes.CargarGabinete();
            cboGabinete.DataSource = gabinete;

            // Todos los combobox en indice -1 para que a la hora de iniciar el 
            // programa no inicie con un componente
            
            cboModelo.SelectedIndex = -1;
            cboMemoria.SelectedIndex = -1;
            cboAlmacenamiento.SelectedIndex = -1;
            cboGpu.SelectedIndex = -1;
            cboFuente.SelectedIndex = -1;
            cboGabinete.SelectedIndex = -1;
            cboPlaca.SelectedIndex = -1;

        }

        
        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // el Modelo del procesador se carga segun el tipo
            Procesador procesadorElegido = (Procesador)cboTipo.SelectedItem;
            cboModelo.DataSource = procesadorElegido.Modelos;
            cboModelo.DisplayMember = "Nombre";
            
            // placa madre se carga segun el Modelo del procesador
            placamadre = gestorcomponentes.CargarPlacaMadre(procesadorElegido.Marca);
            cboPlaca.DataSource = placamadre;
            cboPlaca.DisplayMember = "Nombre";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // correccion de errores. Debe seleeccionar un item de cada combobox para confirmar el pedido
            if (cboTipo.SelectedIndex == -1 ||
        cboModelo.SelectedIndex == -1 ||
        cboPlaca.SelectedIndex == -1 ||
        cboMemoria.SelectedIndex == -1 ||
        cboAlmacenamiento.SelectedIndex == -1 ||
        cboGpu.SelectedIndex == -1 ||
        cboFuente.SelectedIndex == -1 ||
        cboGabinete.SelectedIndex == -1)
            {
                // mensaje de error
                MessageBox.Show("Por favor, selecciona todos los componentes antes de confirmar el pedido.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Salir del método para evitar procesar la confirmación
            }

            // cargamos los componentes seleccionados por el usuario
            PC pcArmada = new PC
            {
                Procesador = (Componentes)cboModelo.SelectedItem,
                PlacaMadre = (Componentes)cboPlaca.SelectedItem,
                RAM = (Componentes)cboMemoria.SelectedItem,
                GPU = (Componentes)cboGpu.SelectedItem,
                FuentePoder = (Componentes)cboFuente.SelectedItem,
                Gabinete = (Componentes)cboGabinete.SelectedItem,

            };
            // mensaje de la pc armada
            MessageBox.Show(pcArmada.Resumen(), "Configuración de la PC");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            // Para limpiar cada combobox 
            cboModelo.SelectedIndex = -1;
            cboMemoria.SelectedIndex = -1;
            cboAlmacenamiento.SelectedIndex = -1;
            cboGpu.SelectedIndex = -1;
            cboFuente.SelectedIndex = -1;
            cboGabinete.SelectedIndex = -1;
            cboPlaca.SelectedIndex = -1;
        }
    }







}
