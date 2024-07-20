using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACA_GMW_2024
{
    public partial class ShowPredecessor_Window : Form
    {
        private Graphics? _graphics;
        private int[,] _predecessor;
        private int _width;
        private int _height;
        private int _size;
        private int _currentStep;
        private int _stepsBackward;
        private Stack<int[,]> _predecessors;

        public ShowPredecessor_Window(int[,] predecessor, int width, int height, int stepsBackward)
        {
            InitializeComponent();
            _width = width + 4;
            _height = height + 4;
            _predecessor = new int[width + 4, height + 4];
            _currentStep = 0;
            _stepsBackward = stepsBackward;
            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    if (y < 2 || x < 2 || y > height + 1 || x > width + 1)
                    {
                        _predecessor[x, y] = 0;
                        continue;
                    }
                    _predecessor[x, y] = predecessor[x - 2, y - 2] == 1
                        ? 1 : 0;
                }
            _predecessors = new Stack<int[,]>();
            PreviousStep.Enabled = false;
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            PredecessorView.Image = new Bitmap(PredecessorView.Width, PredecessorView.Height);
            _graphics = Graphics.FromImage(PredecessorView.Image);
            _graphics.Clear(Color.Black);

            _size = Math.Min(PredecessorView.Width / (_width - 4), PredecessorView.Height / (_height - 4));
            int borderSize = Math.Abs(PredecessorView.Width - PredecessorView.Height) / 2;
            for (int j = 2; j < _height - 2; j++)
                for (int i = 2; i < _width - 2; i++)
                {
                    if (_predecessor[i, j] == 1)
                    {
                        int x = borderSize + (i - 2) * _size;
                        int y = (j - 2) * _size;
                        _graphics.FillRectangle(Brushes.White, x, y, _size, _size);
                    }
                }

            for (int j = 0; j < PredecessorView.Height; j += _size)
                for (int i = borderSize; i < PredecessorView.Height + borderSize; i += _size)
                {
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(borderSize, j), new Point(PredecessorView.Height + borderSize, j));
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(i, 0), new Point(i, PredecessorView.Height));
                }
        }

        private void NextStep_Click(object sender, EventArgs e)
        {
            if (sender is not Button) return;

            var newPredecessor = new int[_width, _height];
            var predecessorCopy = new int[_width, _height];
            for (int y = 1; y < _height - 1; y++)
                for (int x = 1; x < _width - 1; x++)
                {
                    predecessorCopy[x, y] = _predecessor[x, y];
                    newPredecessor[x, y] = CheckNeighbours(x, y)
                        ? 1 : 0;
                    _predecessor[x, y] = newPredecessor[x, y];
                }

            _predecessors.Push(predecessorCopy);
            _currentStep++;
            if (_currentStep == _stepsBackward)
                NextStep.Enabled = false;
            UpdatePicture();
            if (_predecessors.Count > 0)
                PreviousStep.Enabled = true;
        }

        private void PreviousStep_Click(object sender, EventArgs e)
        {
            if (sender is not Button) return;
            var predecessor = _predecessors.Pop();
            for (int y = 1; y < _height - 1; y++)
                for (int x = 1; x < _width - 1; x++)
                    _predecessor[x, y] = predecessor[x, y];

            _currentStep--;
            if (_currentStep < _stepsBackward)
                NextStep.Enabled = true;
            UpdatePicture();
            if (_predecessors.Count < 1)
                PreviousStep.Enabled = false;
        }

        private bool CheckNeighbours(int x, int y)
        {
            bool isLiving = false;
            int count = 0;
            for (int j = -1; j < 2; j++)
                for (int i = -1; i < 2; i++)
                {
                    if (i == j && i == 0)
                    {
                        isLiving = _predecessor[x, y] == 1;
                        continue;
                    }
                    count += _predecessor[x + i, y + j] == 1
                        ? 1 : 0;
                }

            if (count == 3)
                return true;
            else if (count == 2 && isLiving)
                return true;
            return false;
        }

        private void WindowSizeChanged(object sender, EventArgs e)
        {
            if (sender is not Form) return;

            UpdatePicture();
        }
            
    }
}
