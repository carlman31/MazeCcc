using Maze;

namespace WinFormsMaze
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO CCC: fill with readed config, same on MazeConsole app
            var configurations = new MazeConfigurations();

            var mazePlayer = new MazePlayer(configurations);
            mazePlayer.UpdateScreenEvent += MazePlayer_UpdateScreen;
            mazePlayer.GameFinishedEvent += MazePlayerGame_FinishedEvent;
            mazePlayer.GameErrorEvent += MazePlayerGame_GameErrorEvent;

            //TODO CCC: Pass to button the reset
            //mazePlayer.ResetGame();
            mazePlayer.Play();
        }

        public void MazePlayer_UpdateScreen(object sender, EventArgs e)
        {
            MazePlayer mazePlayer = (MazePlayer)sender;
            UpdateScreen(mazePlayer);
        }

        public void MazePlayerGame_FinishedEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Maze finished!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MazePlayerGame_GameErrorEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Error playing maze!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var configurations = new MazeConfigurations();
            var mazePlayer = new MazePlayer(configurations);

            var graphics = pictureBox1.CreateGraphics();

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               12,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            System.Drawing.SolidBrush SolidBlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.SolidBrush SolidRedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            int blockSize = 40;
            var penWidth = 3;
            var hOffset = 10;
            var vOffset = 10;
            var listaVisitados = new List<MazeCell>();

            int contadorVisitados = 0;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    MazeCell currentCell = null;
                    if (currentCell == null)
                    {
                        currentCell = new MazeCell(mazePlayer);
                        currentCell.NorthBlocked = true;
                        currentCell.WestBlocked = true;
                        currentCell.SouthBlocked = true;
                        currentCell.EastBlocked = true;

                        if (contadorVisitados < 20)
                        {
                            listaVisitados.Add(currentCell);
                            contadorVisitados++;
                        }
                    }
                    if (currentCell.NorthBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Blue"), penWidth),
                                 i * blockSize,
                                 j * blockSize,
                                 (i + 1) * (blockSize),
                                 j * blockSize);
                    }
                    if (currentCell.WestBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Red"), penWidth), i * blockSize, j * blockSize, i * blockSize, (j + 1) * blockSize);
                    }
                    if (currentCell.SouthBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Green"), penWidth), i * blockSize, (j + 1) * blockSize, (i + 1) * blockSize, (j + 1) * blockSize);
                    }
                    if (currentCell.EastBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Black"), penWidth), (i + 1) * blockSize, j * blockSize, (i + 1) * blockSize, (j + 1) * blockSize);
                    }
                    graphics.DrawString($"{i},{j}", font, SolidBlackBrush, (i * blockSize) + 4, (j * blockSize) + 4);

                    if (listaVisitados.Contains(currentCell))
                    {
                        graphics.FillEllipse(SolidRedBrush, (i * blockSize) + 20, (j * blockSize) + 20, 15, 15);
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var configurations = new MazeConfigurations();
            var mazePlayer = new MazePlayer(configurations);

            mazePlayer.ResetGame();
        }
        public void UpdateScreen(MazePlayer mazePlayer)
        {
            var graphics = pictureBox1.CreateGraphics();

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 12, FontStyle.Bold, GraphicsUnit.Pixel);

            System.Drawing.SolidBrush SolidBlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.SolidBrush SolidRedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush SolidGreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            System.Drawing.SolidBrush SolidBlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

            int blockSize = 40;
            var penWidth = 3;
            //TODO CCC, use heigth insead of hardcoded 25
            for (int i = 0; i < 25; i++)
            {
                //TODO CCC, use width insead of hardcoded 25
                for (int j = 0; j < 25; j++)
                {
                    var currentPaintCell = mazePlayer.MazeCells[i, j];
                    if (currentPaintCell == null)
                    {
                        currentPaintCell = new MazeCell(mazePlayer);
                    }
                    if (currentPaintCell.NorthBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Blue"), penWidth),
                            i * blockSize,
                            j * blockSize,
                            (i + 1) * blockSize,
                            j * blockSize);
                    }
                    if (currentPaintCell.WestBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Red"), penWidth),
                            i * blockSize, 
                            j * blockSize, 
                            i * blockSize, 
                            (j + 1) * blockSize);
                    }
                    if (currentPaintCell.SouthBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Green"), penWidth),
                            i * blockSize, 
                            (j + 1) * blockSize, 
                            (i + 1) * blockSize, 
                            (j + 1) * blockSize);
                    }
                    if (currentPaintCell.EastBlocked)
                    {
                        graphics.DrawLine(new Pen(Color.FromName("Black"), penWidth),
                            (i + 1) * blockSize, 
                            j * blockSize, 
                            (i + 1) * blockSize, 
                            (j + 1) * blockSize);
                    }
                    graphics.DrawString($"{i},{j}", font, SolidBlackBrush, 
                            (i * blockSize) + 4, 
                            (j * blockSize) + 4);

                    if (mazePlayer.CurrentPath.Any(cell => cell.PositionX == currentPaintCell.PositionX 
                        && cell.PositionY == currentPaintCell.PositionY))
                    {
                        graphics.FillEllipse(SolidRedBrush, 
                            (i * blockSize) + 20, 
                            (j * blockSize) + 20, 
                            15, 
                            15);
                    }

                    if (mazePlayer.CurrentCell != null
                        && mazePlayer.CurrentCell.PositionX == currentPaintCell.PositionX
                        && mazePlayer.CurrentCell.PositionY == currentPaintCell.PositionY)
                    {
                        graphics.FillEllipse(SolidGreenBrush, 
                            (i * blockSize) + 20, 
                            (j * blockSize) + 20, 
                            15, 
                            15);
                    }

                    if (currentPaintCell.Banned)
                    {
                        graphics.DrawString($"Ba", font, SolidBlueBrush, (i * blockSize) + 2, (j * blockSize) + 20);
                    }
                }
            }
        }
    }
}
