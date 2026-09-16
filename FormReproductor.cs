using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Practica2
{
    // Formulario que gestiona la reproducción secuencial de la cola
    public class FormReproductor : Form
    {
        private ColaReproduccion colaCanciones;
        
        // Controles de la interfaz
        private Label lblReproduciendo = null!;
        private Label lblInfoCancion = null!;
        private Label lblTiempoTotal = null!;
        private Button btnReproducir = null!;
        private PictureBox picGrafoCola = null!;

        public FormReproductor(ColaReproduccion cola)
        {
            colaCanciones = cola;
            InitializeComponent();
            ActualizarInterfaz(); // Dibuja el estado inicial de la cola al abrir la ventana
        }

        private void InitializeComponent()
        {
            this.Text = "Reproductor de Música - Cola de Reproducción";
            this.Size = new Size(550, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblReproduciendo = new Label { Text = "Reproduciendo ahora:", Location = new Point(30, 20), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            this.Controls.Add(lblReproduciendo);

            lblInfoCancion = new Label { Text = "Ninguna canción en reproducción.", Location = new Point(30, 55), AutoSize = true, Size = new Size(300, 80), Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lblInfoCancion);

            lblTiempoTotal = new Label { Text = "Tiempo total estimado de la cola: 0 min", Location = new Point(30, 140), AutoSize = true, ForeColor = Color.Blue, Font = new Font("Segoe UI", 10, FontStyle.Italic) };
            this.Controls.Add(lblTiempoTotal);

            btnReproducir = new Button { Text = "REPRODUCIR SIGUIENTE", Location = new Point(320, 55), Size = new Size(180, 50), BackColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            btnReproducir.Click += BtnReproducir_Click;
            this.Controls.Add(btnReproducir);

            picGrafoCola = new PictureBox { Location = new Point(30, 180), Size = new Size(470, 230), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White };
            this.Controls.Add(picGrafoCola);
        }

        private void BtnReproducir_Click(object? sender, EventArgs e)
        {
            // Se asume que tu clase ColaReproduccion tiene el método Desencolar()
            Cancion? cancionExtraida = colaCanciones.Desencolar();

            if (cancionExtraida == null)
            {
                MessageBox.Show("La cola de reproducción está vacía.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblInfoCancion.Text = "Ninguna canción en reproducción.";
            }
            else
            {
                lblInfoCancion.Text = $"Título: {cancionExtraida.Titulo}\nArtista: {cancionExtraida.Artista}\nGénero: {cancionExtraida.Genero}\nDuración: {cancionExtraida.Duracion} min";
            }

            ActualizarInterfaz();
        }

        private void ActualizarInterfaz()
        {
            // Se asume que tu clase ColaReproduccion tiene el método CalcularTiempoEstimado()
            int tiempoTotal = colaCanciones.CalcularTiempoEstimado();
            lblTiempoTotal.Text = $"Tiempo total estimado de la cola: {tiempoTotal} min";

            GraphvizHelper.GenerarGrafoCola(colaCanciones);
            CargarImagenGrafo();
        }

        private void CargarImagenGrafo()
        {
            string rutaImagen = "cola.png";
            if (File.Exists(rutaImagen))
            {
                using (FileStream fs = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read))
                {
                    picGrafoCola.Image = Image.FromStream(fs);
                }
            }
            else
            {
                picGrafoCola.Image = null;
            }
        }
    }
}