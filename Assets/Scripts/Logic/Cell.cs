namespace Life.Logic
{
    public class Cell
    {
        private int state = 0;

        public void ChangeState() => state = (state + 1) % 2;

        public void Die() => state = 0;

        public void Live() => state = 1;

        public int State => state;

        public bool IsAlive => state == 1;
    }
}
