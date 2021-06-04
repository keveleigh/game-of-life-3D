namespace Life.Logic
{
    public class Simulation
    {
        private readonly int length;
        private readonly int width;
        private readonly Cell[,] cells;
        private readonly int[,] neighbors;

        public Simulation(int gridLength, int gridWidth, Cell[,] cellGrid)
        {
            length = gridLength;
            width = gridWidth;
            cells = cellGrid;
            neighbors = new int[length, width];
        }

        public void Update()
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    neighbors[i, j] = GetNumNeighbors(i, j);
                }
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int neighborCount = neighbors[i, j];
                    Cell cell = cells[i, j];
                    if (neighborCount < 2 || neighborCount > 3)
                    {
                        cell.Die();
                    }
                    else if ((neighborCount == 2 && cell.IsAlive) || neighborCount == 3)
                    {
                        cell.Live();
                    }
                }
            }
        }

        private int GetNumNeighbors(int i, int j)
        {
            int neighbors = 0;

            bool iMaxEdgeValid = i + 1 < length;
            bool jMaxEdgeValid = j + 1 < width;
            bool iMinEdgeValid = i - 1 >= 0;
            bool jMinEdgeValid = j - 1 >= 0;

            if (iMaxEdgeValid)
            {
                if (cells[i + 1, j].IsAlive)
                {
                    neighbors++;
                }

                if (jMaxEdgeValid && cells[i + 1, j + 1].IsAlive)
                {
                    neighbors++;
                }

                if (jMinEdgeValid && cells[i + 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }

            if (iMinEdgeValid)
            {
                if (cells[i - 1, j].IsAlive)
                {
                    neighbors++;
                }

                if (jMaxEdgeValid && cells[i - 1, j + 1].IsAlive)
                {
                    neighbors++;
                }

                if (jMinEdgeValid && cells[i - 1, j - 1].IsAlive)
                {
                    neighbors++;
                }
            }

            if (jMaxEdgeValid && cells[i, j + 1].IsAlive)
            {
                neighbors++;
            }

            if (jMinEdgeValid && cells[i, j - 1].IsAlive)
            {
                neighbors++;
            }

            return neighbors;
        }
    }
}
