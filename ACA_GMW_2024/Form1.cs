namespace ACA_GMW_2024
{
    public partial class InverseGameOfLife : Form
    {
        private Graphics? _graphics;
        private bool[,] _field;
        private int _width;
        private int _height;
        private int _stepsBackward;
        private List<bool[,]> livingCellProducers;
        private List<bool[,]> deadCellProducers;
        private List<(int, int)> livingCells;

        public InverseGameOfLife()
        {
            InitializeComponent();
            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);
            _width = ResultField.Width / 10;
            _height = ResultField.Height / 10;
            _width *= 3;
            _height *= 3;
            _field = new bool[_width, _height];
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    _field[i, j] = false;
                }
            }
            for (int j = 0; j < ResultField.Height; j += 10)
                for (int i = 0; i < ResultField.Width; i += 10)
                {
                    _graphics.DrawLine(new Pen(Brushes.Gray, 1), new Point(0, j), new Point(ResultField.Width, j));
                    _graphics.DrawLine(new Pen(Brushes.Gray, 1), new Point(i, 0), new Point(i, ResultField.Height));
                }

            livingCellProducers = new List<bool[,]>();
            deadCellProducers = new List<bool[,]>();
            livingCells = new List<(int, int)>();
        }

        private void ResultField_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is not PictureBox) return;

            int x = (e.Location.X / 10) + (_width / 3);
            int y = (e.Location.Y / 10) + (_height / 3);

            if (e.Button is MouseButtons.Left)
            {
                _field[x, y] = true;
                livingCells.Add((x, y));
            }
            else if (e.Button is MouseButtons.Right)
            {
                 _field[x, y] = false;
                livingCells.Remove((x, y));
            }

            UpdateResultField();
        }

        private void UpdateResultField()
        {
            ResultField.Size = splitContainer.Panel1.Size;
            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);

            for (int j = _height / 3; j < 2 * _height / 3; j++)
            {
                for (int i = _width / 3; i < 2 * _width / 3; i++)
                {
                    if (_field[i, j])
                    {
                        int x = i - _width / 3;
                        int y = j - _height / 3;
                        _graphics.FillRectangle(Brushes.White, x * 10, y * 10, 10, 10);
                    }
                }
            }

            for (int j = 0; j < ResultField.Height; j += 10)
                for (int i = 0; i < ResultField.Width; i += 10)
                {
                    _graphics.DrawLine(new Pen(Brushes.Gray, 1), new Point(0, j), new Point(ResultField.Width, j));
                    _graphics.DrawLine(new Pen(Brushes.Gray, 1), new Point(i, 0), new Point(i, ResultField.Height));
                }
                    

            ResultField.Refresh();
        }

        private void IGoL_Window_SizeChanged(object sender, EventArgs e)
        {
            if (sender is not Form) return;
            
            UpdateResultField();
            _width = ResultField.Width / 10;
            _height = ResultField.Height / 10;
            _width *= 3;
            _height *= 3;
            _field = new bool[_width, _height];
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    _field[i, j] = false;
                }
            }
        }

        private void FindPatterns_Click(object sender, EventArgs e)
        {
            if (sender is not Button) return;

            for (int patternNumber = 0; patternNumber < 512; patternNumber++)
            {
                var pattern = new bool[3, 3];
                var currentNumber = patternNumber;
                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < 3; x++)
                    {
                        pattern[x, y] = currentNumber % 2 == 1;
                        currentNumber = (currentNumber - (currentNumber % 2)) / 2;
                    }
                if (CheckPattern(pattern))
                    livingCellProducers.Add(pattern);
                else
                    deadCellProducers.Add(pattern);
            }
        }

        private bool CheckPattern(bool[,] pattern)
        {
            int count = 0;
            bool surviving = false;
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; ++x)
                {
                    if (x == y && x == 1)
                    {
                        surviving = pattern[x, y];
                        continue;
                    }
                    if (!pattern[x, y]) continue;
                    count++;
                }
            if (count == 3) return true;
            if (count == 2 && surviving) return true;
            return false;
        }

        private void StepsBackward_Count_ValueChanged(object sender, EventArgs e)
        {
            if (sender is not NumericUpDown) return;

            _stepsBackward = (int)StepsBackward_Count.Value;
        }
    }
}
