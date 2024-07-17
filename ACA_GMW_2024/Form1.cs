namespace ACA_GMW_2024
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private byte[] _field;

        public Form1()
        {
            InitializeComponent();
            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (sender is not Form) return;
            ResultField.Size = splitContainer.Panel1.Size;
            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);
        }
    }
}
