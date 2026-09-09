using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Led
{
    public partial class Form1 : Form
    {
        // Comunicación con Arduino
        SerialPort puertoArduino = new SerialPort();

        public Form1()
        {
            InitializeComponent();

            // Asociamos los botones con sus eventos
            btnEncender.Click += btnEncender_Click;
            btnApagar.Click += btnApagar_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // CAMBIA COM3 por el puerto donde esté conectado tu Arduino
                puertoArduino.PortName = "COM3";

                // Debe ser la misma velocidad que utilizaremos en Arduino
                puertoArduino.BaudRate = 9600;

                puertoArduino.DataBits = 8;
                puertoArduino.Parity = Parity.None;
                puertoArduino.StopBits = StopBits.One;
                puertoArduino.Handshake = Handshake.None;

                // Abrir comunicación
                puertoArduino.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo conectar con Arduino.\n\n" +
                    "Verifica el puerto COM.\n\n" +
                    "Detalle: " + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            try
            {
                if (puertoArduino.IsOpen)
                {
                    // Mandamos E = Encender
                    puertoArduino.Write("E");
                }
                else
                {
                    MessageBox.Show("Arduino no está conectado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el comando: " + ex.Message);
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (puertoArduino.IsOpen)
                {
                    // Mandamos A = Apagar
                    puertoArduino.Write("A");
                }
                else
                {
                    MessageBox.Show("Arduino no está conectado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el comando: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cerramos correctamente el puerto serial
            if (puertoArduino != null && puertoArduino.IsOpen)
            {
                puertoArduino.Close();
            }

            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}