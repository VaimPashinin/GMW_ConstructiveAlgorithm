using System.Linq.Expressions;

namespace ACA_GMW_2024
{
    public partial class InverseGameOfLife : Form
    {
        private Graphics? _graphics;
        private int[,] _field;
        private int _width;
        private int _height;
        private int _stepsBackward;
        private List<int[,]> _livingCellProducers;
        private List<int[,]> _deadCellProducers;
        private List<(int, int)> _livingCells;
        private List<(int[,], int, int)> _predecessors;

        public InverseGameOfLife()
        {
            InitializeComponent();
            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);
            _width = ResultField.Width / 15;
            _height = ResultField.Height / 15;
            _width *= 3;
            _height *= 3;
            _field = new int[_width, _height];
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    _field[i, j] = 0;
                }
            }
            for (int j = 0; j < ResultField.Height; j += 15)
                for (int i = 0; i < ResultField.Width; i += 15)
                {
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(0, j), new Point(ResultField.Width, j));
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(i, 0), new Point(i, ResultField.Height));
                }

            _livingCellProducers = new List<int[,]>();
            _deadCellProducers = new List<int[,]>();
            _livingCells = new List<(int, int)>();
            _predecessors = new List<(int[,], int, int)>();
            _stepsBackward = 1;
        }

        private void ResultField_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is not PictureBox) return;

            int x = (e.Location.X / 15) + (_width / 3);
            int y = (e.Location.Y / 15) + (_height / 3);

            if (e.Button is MouseButtons.Left)
            {
                _field[x, y] = 1;
                _livingCells.Add((x, y));
            }
            else if (e.Button is MouseButtons.Right)
            {
                _field[x, y] = 0;
                _livingCells.Remove((x, y));
            }

            UpdateResultField();
        }

        private void UpdateResultField()
        {
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);

            for (int j = _height / 3; j < 2 * _height / 3; j++)
            {
                for (int i = _width / 3; i < 2 * _width / 3; i++)
                {
                    if (_field[i, j] == 1)
                    {
                        int x = i - _width / 3;
                        int y = j - _height / 3;
                        _graphics.FillRectangle(Brushes.White, x * 15, y * 15, 15, 15);
                    }
                }
            }

            for (int j = 0; j < ResultField.Height; j += 15)
                for (int i = 0; i < ResultField.Width; i += 15)
                {
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(0, j), new Point(ResultField.Width, j));
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(i, 0), new Point(i, ResultField.Height));
                }


            ResultField.Refresh();
        }

        private void IGoL_Window_SizeChanged(object sender, EventArgs e)
        {
            if (sender is not Form) return;

            ResultField.Image = new Bitmap(ResultField.Width, ResultField.Height);
            _graphics = Graphics.FromImage(ResultField.Image);
            _graphics.Clear(Color.Black);
            _width = ResultField.Width / 15;
            _height = ResultField.Height / 15;
            _width *= 3;
            _height *= 3;
            _field = new int[_width, _height];
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    _field[i, j] = 0;
                }
            }
            for (int j = 0; j < ResultField.Height; j += 15)
                for (int i = 0; i < ResultField.Width; i += 15)
                {
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(0, j), new Point(ResultField.Width, j));
                    _graphics.DrawLine(new Pen(Brushes.DarkGray, 1), new Point(i, 0), new Point(i, ResultField.Height));
                }
        }

        private void ConstructPatterns()
        {
            for (int patternNumber = 0; patternNumber < 512; patternNumber++)
            {
                var pattern = new int[3, 3];
                var currentNumber = patternNumber;
                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < 3; x++)
                    {
                        pattern[x, y] = currentNumber % 2 == 1
                            ? 1
                            : 0;
                        currentNumber = (currentNumber - (currentNumber % 2)) / 2;
                    }
                if (CheckPattern(pattern))
                    _livingCellProducers.Add(pattern);
                else _deadCellProducers.Add(pattern);
            }
        }

        private bool CheckPatternCorrespodancy(int[,] pattern, int[,] constructionArea)
        {
            bool result = true;
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 3; i++)
                {
                    if (constructionArea[i, j] == -1) continue;
                    result &= pattern[i, j] == constructionArea[i, j];
                }
            return result;
        }

        private void SearchPredecessor(int[,] result, int step, int width, int height)
        {
            var livingCells = new List<(int, int)>();
            if (step == 1) livingCells.AddRange(_livingCells);
            else
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        if (result[x, y] == 1) livingCells.Add((x, y));
            }
            int minX = width;
            int maxX = 0;
            int minY = height;
            int maxY = 0;
            int newWidth = 0;
            int newHeight = 0;
            foreach (var cell in livingCells)
            {
                if (cell.Item1 < minX) minX = cell.Item1;
                if (cell.Item1 > maxX) maxX = cell.Item1;
                if (cell.Item2 < minY) minY = cell.Item2;
                if (cell.Item2 > maxY) maxY = cell.Item2;
            }
            newWidth = maxX - minX + 7;
            newHeight = maxY - minY + 7;

            var predecessors = new List<int[,]>();
            var potentialpredecessor = new int[newWidth, newHeight];
            for (int y = 0; y < newHeight; y++)
                for (int x = 0; x < newWidth; x++)
                    potentialpredecessor[x, y] = -1;

            var cellsToDeconstruct = new List<(int, int)>
            {
                livingCells.First()
            };
            Deconstruct(predecessors,
                livingCells,
                cellsToDeconstruct,
                result,
                potentialpredecessor,
                0,
                minX,
                minY,
                newWidth,
                newHeight);

            if (step == _stepsBackward)
            {
                foreach (var predecessor in predecessors)
                    _predecessors.Add((predecessor, newWidth, newHeight));
                return;
            }
            foreach (var predecessor in predecessors)
            {
                SearchPredecessor(predecessor, step + 1, newWidth, newHeight);
            }
        }


        private void Deconstruct(IList<int[,]> predecessors,
            IList<(int, int)> livingCells,
            IList<(int, int)> cellsToDeconstrct,
            int[,] result,
            int[,] potentialpredecessor,
            int currentIndex,
            int difX, 
            int difY,
            int width,
            int height)
        {
            if (cellsToDeconstrct.Count == 0 || currentIndex >= cellsToDeconstrct.Count) return;
            int x = cellsToDeconstrct[currentIndex].Item1;
            int y = cellsToDeconstrct[currentIndex].Item2;
            var (newX, newY) = CoordTransition(x, y, difX, difY);

            var constructionArea = new int[3, 3];
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 3; i++)
                    constructionArea[i, j] = potentialpredecessor[newX + i - 1, j + newY - 1];
            
            bool isLiving = result[x, y] == 1;
            var correspondingPatterns = new List<int[,]>();
            if (isLiving)
            {
                correspondingPatterns.AddRange(_livingCellProducers
                    .Where(pattern => CheckPatternCorrespodancy(pattern, constructionArea)));
                livingCells.Remove((x, y));
            }
            else
                correspondingPatterns.AddRange(_deadCellProducers
                    .Where(pattern => CheckPatternCorrespodancy(pattern, constructionArea)));

            var deadNeighbors = new List<(int, int)>();
            var livingNeighbors = new List<(int, int)>();
            if (isLiving)
            {
                for (int j = -1; j < 2; j++)
                    for (int i = -1; i < 2; i++)
                    {
                        if (cellsToDeconstrct.Contains((x + i, y + j))) continue;
                        if (result[x + i, y + j] == 1)
                        {
                            livingNeighbors.Add((x + i, y + j));
                            continue;
                        }
                        deadNeighbors.Add((x + i, y + j));
                        cellsToDeconstrct.Add((x + i, y + j));
                    }
                foreach (var neighbor in livingNeighbors)
                    cellsToDeconstrct.Add(neighbor);
                if (livingNeighbors.Count == 0 && livingCells.Count != 0)
                    cellsToDeconstrct.Add(livingCells.First());
            }

            foreach (var pattern in correspondingPatterns)
            {
                if (predecessors.Count > 0) break;
                for (int j = 0; j < 3; j++)
                    for (int i = 0; i < 3; i++)
                        potentialpredecessor[newX + i - 1, j + newY - 1] = pattern[i, j];
                if (currentIndex < cellsToDeconstrct.Count - 1)
                    Deconstruct(predecessors,
                        livingCells,
                        cellsToDeconstrct,
                        result,
                        potentialpredecessor,
                        currentIndex + 1,
                        difX,
                        difY,
                        width,
                        height);
                else if (livingCells.Count == 0)
                {
                    var predecessor = new int[width, height];
                    for (int j = 0; j < height; j++)
                        for (int i = 0; i < width; i++)
                            predecessor[i, j] = potentialpredecessor[i, j];
                    predecessors.Add(predecessor);
                    break;
                }
            }

            foreach (var neighbor in livingNeighbors)
                cellsToDeconstrct.Remove(neighbor);
            foreach (var neighbor in deadNeighbors)
                cellsToDeconstrct.Remove(neighbor);
            if (isLiving)
                livingCells.Add((x, y));

            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 3; i++)
                    potentialpredecessor[newX + i - 1, j + newY - 1] = constructionArea[i, j];
        }

        private (int, int) CoordTransition(int x, int y, int difX, int difY) =>
            (x - difX + 3, y - difY + 3);

        private (int, int) ReversalCoordTransition(int x, int y, int difX, int difY) =>
            (x + difX - 3, y + difY - 3);

        private bool CheckPattern(int[,] pattern)
        {
            int count = 0;
            bool surviving = false;
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; ++x)
                {
                    if (x == y && x == 1)
                    {
                        surviving = pattern[x, y] == 1;
                        continue;
                    }
                    if (pattern[x, y] == 0) continue;
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

        private void FP_Button_Click(object sender, EventArgs e)
        {
            if (sender is not Button) return;
            _predecessors.Clear();

            if (_livingCells.Count == 0)
            {
                string message = "There're too many possibilities for tracebacking a dead field. Please, add some life :-)";
                string caption = "Error: no living cell at field";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, button);
                return;
            }

            if (_livingCellProducers.Count == 0) ConstructPatterns();

            SearchPredecessor(_field, 1, _width, _height);

            if (_predecessors.Count == 0)
            {
                string message = "It's unable to find any predecessor " + _stepsBackward + " steps backward!";
                string caption = "No predecessors" + _stepsBackward + " steps backward";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, button);
                return;
            }

            var (predecessor, width, height) = _predecessors.First();

            var result = new ShowPredecessor_Window(predecessor, width, height, _stepsBackward);
            result.Show();
        }
    }
}
