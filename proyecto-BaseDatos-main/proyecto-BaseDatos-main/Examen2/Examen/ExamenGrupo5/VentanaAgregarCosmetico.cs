using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaAgregarCosmetico : Form
    {

        private Cosmetico _cosmetico = null;
        private OracleConexion _conexion = null;
        private bool edita = false;
        private Conexion _conexionServer;

        public VentanaAgregarCosmetico()
        {

            InitializeComponent();
            edita = false;
            _conexion = new OracleConexion(ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString);
            
        }
        public VentanaAgregarCosmetico(Cosmetico cos)
        {
            InitializeComponent();
            _cosmetico = cos;
            CargarDatos();
            edita = true;

            _conexion = new OracleConexion(ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString);
            _conexionServer = new Conexion(ConfigurationManager.ConnectionStrings["serverSystem"].ConnectionString);
        }
        private void CargarDatos()
        {
            this.txtNombre.Text = _cosmetico.Nombre;
            this.txtMarca.Text = _cosmetico.Marca;
            this.spPrecio.Value = (decimal)_cosmetico.PrecioUnitario;
            this.spStock.Value = _cosmetico.StockDisponible;
            this.dtpFecha.Value = _cosmetico.FechaVencimiento;
            this.cbCategoria.SelectedItem = _cosmetico.Categoria;
            this.cbEstado.SelectedItem = _cosmetico.EstadoProducto;
            this.pbImagen.Image = Cargar_Imagen(_cosmetico.Imagen);
        }
        private Image Cargar_Imagen(string rutaImage)
        {
            if (!string.IsNullOrWhiteSpace(rutaImage) && System.IO.File.Exists(rutaImage))
            {
                return Image.FromFile(rutaImage);
            }
            else
            {
                return global::ExamenGrupo5.Properties.Resources.icons8_add_48;
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void btnAgregar(object sender, EventArgs e)
        {
            if (edita != true)
            {
                try
                {
                    List<string> mensajesError = new List<string>();

                    Cosmetico cosmetico = _conexion.BuscarPorNombreCosmetico(txtNombre.Text.Trim());
                    if (cosmetico != null)
                    {
                        throw new Exception("cosmestico ya ha sido registrado, intente con otro cosmetico");


                    }

                    if (string.IsNullOrWhiteSpace(txtMarca.Text))
                    {
                        mensajesError.Add("El campo de la Marca del producto no puede quedar vacio.");
                    }

                    if (spPrecio.Value <= 0)
                    {
                        mensajesError.Add("El precio unitario debe ser mayor a cero.");
                    }

                    if (dtpFecha.Value <= DateTime.Now)
                    {
                        mensajesError.Add("La fecha de vencimiento debe ser una fecha futura.");
                    }

                    if (spStock.Value <= 0)
                    {
                        mensajesError.Add("El stock disponible debe ser mayor a cero.");
                    }

                    if (cbCategoria.SelectedItem == null)
                    {
                        mensajesError.Add("Debe seleccionar una categoría.");
                    }

                    if (cbEstado.SelectedItem == null)
                    {
                        mensajesError.Add("Debe seleccionar un estado del producto.");
                    }

                    if (pbImagen.Image == null)
                    {
                        mensajesError.Add("Debe seleccionar una imagen para el producto.");
                    }

                    if (mensajesError.Count > 0)
                    {
                        throw new Exception(string.Join("\n", mensajesError));
                    }

                    this._cosmetico = new Cosmetico
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Marca = txtMarca.Text.Trim(),
                        PrecioUnitario = (double)spPrecio.Value,
                        FechaVencimiento = dtpFecha.Value,
                        StockDisponible = (int)spStock.Value,
                        Categoria = cbCategoria.SelectedItem.ToString(),
                        EstadoProducto = cbEstado.SelectedItem.ToString(),
                        Imagen = Guardar_Imagen(pbImagen.Image)

                    };


                    _conexion.GuardarCosmetico(this._cosmetico);
                    _conexionServer.GuardarCosmetico(this._cosmetico);
                    MessageBox.Show("Guardado correctamente",
                        "information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    List<string> mensajesError = new List<string>();

                    Cosmetico cosmetico = _conexion.BuscarPorNombreCosmetico(txtNombre.Text.Trim());

                    if (!cosmetico.Nombre.Equals(txtNombre.Text.Trim()))
                    {
                        mensajesError.Add("Cosmético no ha sido registrado, intente con otro.");
                    }
                    if (cosmetico == null || cosmetico.IDCosmetico != _cosmetico.IDCosmetico)
                    {
                        mensajesError.Add("Cosmético no ha sido registrado, intente con otro.");
                    }

                    if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    {
                        mensajesError.Add("El campo de Nombre del producto no puede quedar vacío.");
                    }

                    if (string.IsNullOrWhiteSpace(txtMarca.Text))
                    {
                        mensajesError.Add("El campo de la Marca del producto no puede quedar vacío.");
                    }

                    if (spPrecio.Value <= 0)
                    {
                        mensajesError.Add("El precio unitario debe ser mayor a cero.");
                    }

                    if (dtpFecha.Value <= DateTime.Now)
                    {
                        mensajesError.Add("La fecha de vencimiento debe ser una fecha futura.");
                    }

                    if (spStock.Value <= 0)
                    {
                        mensajesError.Add("El stock disponible debe ser mayor a cero.");
                    }

                    if (cbCategoria.SelectedItem == null)
                    {
                        mensajesError.Add("Debe seleccionar una categoría.");
                    }

                    if (cbEstado.SelectedItem == null)
                    {
                        mensajesError.Add("Debe seleccionar un estado del producto.");
                    }

                    if (pbImagen.Image == null)
                    {
                        mensajesError.Add("Debe seleccionar una imagen para el producto.");
                    }

                    // 💡 *Nueva Validación: Verificar si el cosmético tiene ventas pendientes*
                    if (_conexion.VentasPendientes(cosmetico.IDCosmetico))
                    {
                        mensajesError.Add("No se puede modificar el precio porque existen ventas pendientes.");
                    }

                    if (mensajesError.Count > 0)
                    {
                        throw new Exception(string.Join("\n", mensajesError));
                    }

                    Cosmetico cos = new Cosmetico
                    {
                        IDCosmetico = cosmetico.IDCosmetico,
                        Nombre = txtNombre.Text.Trim(),
                        Marca = txtMarca.Text.Trim(),
                        PrecioUnitario = (double)spPrecio.Value,
                        FechaVencimiento = dtpFecha.Value,
                        StockDisponible = (int)spStock.Value,
                        Categoria = cbCategoria.SelectedItem.ToString(),
                        EstadoProducto = cbEstado.SelectedItem.ToString(),
                        Imagen = Guardar_Imagen(pbImagen.Image)
                    };

                    _conexion.ModificarCosmetico(cos);
                    MessageBox.Show("Modificado correctamente",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }//fin del boton para guardar los cosmeticos

        private string Guardar_Imagen(Image imagen)
        {
            if (imagen == null)
            {
                return null; // Si la imagen es nula, no se guarda
            }

            try
            {
                // Crear la carpeta "Fotos" si no existe
                string carpetaFotos = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotos");
                if (!System.IO.Directory.Exists(carpetaFotos))
                {
                    System.IO.Directory.CreateDirectory(carpetaFotos);
                }

                // Generar un nombre único para la imagen
                string ruta = System.IO.Path.Combine(carpetaFotos, $"{Guid.NewGuid()}.png");

                // Guardar la imagen en formato PNG
                imagen.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);

                return ruta;
            }
            catch (Exception ex)
            {
                // Lanzar una excepción con un mensaje descriptivo
                throw new Exception("Error al guardar la imagen: " + ex.Message, ex);
            }
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label_Click(object sender, EventArgs e)
        {

        }

        private void pbImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Seleccionar imagen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Cargar la imagen seleccionada
                        Image imagenSeleccionada = Image.FromFile(openFileDialog.FileName);

                        // Redimensionar y recortar la imagen en forma de círculo
                        Image imagenRedimensionada = Redimensionar_Y_Recortar_Circular(imagenSeleccionada, 86, 86);

                        // Asignar la imagen al PictureBox
                        pbImagen.Image = imagenRedimensionada;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private Image Redimensionar_Y_Recortar_Circular(Image imagen, int ancho, int alto)
        {
            // Crear un nuevo bitmap con las dimensiones deseadas
            Bitmap bitmapRedimensionado = new Bitmap(ancho, alto);

            using (Graphics g = Graphics.FromImage(bitmapRedimensionado))
            {
                // Configurar la calidad del gráfico
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Crear un camino gráfico en forma de círculo
                using (GraphicsPath path = new GraphicsPath())
                {
                    // Añadir un círculo al camino gráfico
                    path.AddEllipse(0, 0, ancho, alto);

                    // Aplicar el camino como una región de recorte
                    g.SetClip(path);

                    // Calcular el zoom necesario para que la imagen quede cuadrada
                    float ratioImagen = (float)imagen.Width / imagen.Height;
                    float ratioDestino = (float)ancho / alto;

                    RectangleF destino;
                    if (ratioImagen > ratioDestino)
                    {
                        // La imagen es más ancha que alta, hacer zoom en el ancho
                        float escala = (float)alto / imagen.Height;
                        float nuevoAncho = imagen.Width * escala;
                        float offsetX = (nuevoAncho - ancho) / 2;
                        destino = new RectangleF(-offsetX, 0, nuevoAncho, alto);
                    }
                    else
                    {
                        // La imagen es más alta que ancha, hacer zoom en la altura
                        float escala = (float)ancho / imagen.Width;
                        float nuevoAlto = imagen.Height * escala;
                        float offsetY = (nuevoAlto - alto) / 2;
                        destino = new RectangleF(0, -offsetY, ancho, nuevoAlto);
                    }

                    // Dibujar la imagen dentro del círculo
                    g.DrawImage(imagen, destino);
                }
            }

            return bitmapRedimensionado;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void spStock_ValueChanged(object sender, EventArgs e)
        {

        }

        private void spPrecio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_Estado_Click(object sender, EventArgs e)
        {

        }

        private void label_Clave_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pbSalir_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void VentanaAgregarCosmetico_Load(object sender, EventArgs e)
        {

        }
    }
}
