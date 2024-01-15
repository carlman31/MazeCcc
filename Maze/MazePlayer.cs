using Maze.DTO;

namespace Maze
{
    public class MazePlayer
    {
        #region Props
        public MazeConfigurations Configurations { get; set; }
        private Guid MazeId { get; set; }
        private Guid GameId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public APIHelper ApiHelper { get; set; }

        public int SleepTimeOut { get; set; }

        public bool Completed { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }

        public MazeCell[,] MazeCells { get; set; }
        public MazeCell CurrentCell { get; set; }

        public EDirection LastDirection { get; set; }
        public List<MazeCell> CurrentPath { get; set; }
        #endregion

        #region Events
        public event EventHandler? UpdateScreenEvent;
        public event EventHandler? GameFinishedEvent;
        public event EventHandler? GameErrorEvent;
        #endregion

        public MazePlayer(MazeConfigurations mazeConfigurations)
        {
            Configurations = mazeConfigurations;

            Width = Configurations.Width;
            Height = Configurations.Height;

            ApiHelper = new APIHelper(Configurations.BaseUrl, Configurations.APIKey);
            SleepTimeOut = Configurations.SleepTimeOut;

            if (!Configurations.MazeId.HasValue)
            {
                CreateMaze();
                CreateGame();
            }
            else
            {
                MazeId = Configurations.MazeId.Value;
            }
            if (!Configurations.GameId.HasValue)
            {
                CreateGame();
            }
            else
            {
                GameId = Configurations.GameId.Value;
            }

            MazeCells = new MazeCell[Width, Height];
            CurrentPath = new List<MazeCell>();
        }

        public void CreateMaze()
        {
            var createMazeRequestDTO = new CreateMazeRequestDTO
            {
                Height = this.Height,
                Width = this.Width,
            };

            var createMazeResponseDTO = ApiHelper.CreateMaze(createMazeRequestDTO);

            MazeId = createMazeResponseDTO.MazeUid;
        }

        public void CreateGame()
        {
            var createGameRequestDTO = new CreateGameRequestDTO
            {
                Operation = $"Start"
            };

            var createGameResponseDTO = ApiHelper.CreateGame(createGameRequestDTO, MazeId);

            GameId = createGameResponseDTO.GameUid;
            CurrentPositionX = createGameResponseDTO.CurrentPositionX;
            CurrentPositionY = createGameResponseDTO.CurrentPositionY;
            Completed = createGameResponseDTO.Completed;
        }

        public void ResetGame()
        {
            var resetGameRequestDTO = new ResetGameRequestDTO
            {
                Operation = "Start"
            };

            var resetGameResponseDTO = ApiHelper.ResetGame(resetGameRequestDTO, MazeId, GameId);

            Completed = resetGameResponseDTO.Game.Completed;
            CurrentPositionX = resetGameResponseDTO.Game.CurrentPositionX;
            CurrentPositionY = resetGameResponseDTO.Game.CurrentPositionY;

            var northBlocked = resetGameResponseDTO.MazeBlockView.NorthBlocked;
            var southBlocked = resetGameResponseDTO.MazeBlockView.SouthBlocked;
            var westBlocked = resetGameResponseDTO.MazeBlockView.WestBlocked;
            var eastBlocked = resetGameResponseDTO.MazeBlockView.EastBlocked;

            UpdateMazeInfo(CurrentPositionX, CurrentPositionY, northBlocked, southBlocked, westBlocked, eastBlocked, EDirection.None);
        }

        public void Move(EDirection eDirection)
        {
            var moveMazeRequestDTO = new MoveMazeRequestDTO
            {
                Operation = $"Go{Enum.GetName(typeof(EDirection), eDirection)}"
            };

            var mazeMoveResponseDTO = ApiHelper.Move(moveMazeRequestDTO, MazeId, GameId);

            Completed = mazeMoveResponseDTO.Game.Completed;
            CurrentPositionX = mazeMoveResponseDTO.Game.CurrentPositionX;
            CurrentPositionY = mazeMoveResponseDTO.Game.CurrentPositionY;

            var northBlocked = mazeMoveResponseDTO.MazeBlockView.NorthBlocked;
            var southBlocked = mazeMoveResponseDTO.MazeBlockView.SouthBlocked;
            var westBlocked = mazeMoveResponseDTO.MazeBlockView.WestBlocked;
            var eastBlocked = mazeMoveResponseDTO.MazeBlockView.EastBlocked;

            UpdateMazeInfo(CurrentPositionX, CurrentPositionY, northBlocked, southBlocked, westBlocked, eastBlocked, eDirection);

            if (!CurrentPath.Any(cell => cell.PositionX == CurrentCell.PositionX && cell.PositionY == CurrentCell.PositionY))
            {
                CurrentPath.Add(CurrentCell);
            }

            LastDirection = eDirection;
        }

        public void TakeALook()
        {
            var takeALookMazeResponseDTO = ApiHelper.TakeALook(MazeId, GameId);

            Completed = takeALookMazeResponseDTO.Game.Completed;
            CurrentPositionX = takeALookMazeResponseDTO.Game.CurrentPositionX;
            CurrentPositionY = takeALookMazeResponseDTO.Game.CurrentPositionY;

            var northBlocked = takeALookMazeResponseDTO.MazeBlockView.NorthBlocked;
            var southBlocked = takeALookMazeResponseDTO.MazeBlockView.SouthBlocked;
            var westBlocked = takeALookMazeResponseDTO.MazeBlockView.WestBlocked;
            var eastBlocked = takeALookMazeResponseDTO.MazeBlockView.EastBlocked;

            UpdateMazeInfo(CurrentPositionX, CurrentPositionY, northBlocked, southBlocked, westBlocked, eastBlocked, EDirection.None);

            if (!CurrentPath.Any())
            {
                CurrentPath.Add(CurrentCell);
            }
        }

        private void UpdateMazeInfo(int PositionX, int PositionY, bool northBlocked, bool southBlocked, bool westBlocked, bool eastBlocked, EDirection newDirection)
        {
            if (CurrentCell != null)
            {
                CurrentCell.NewDirection = newDirection;
            }
            CurrentCell = MazeCells[CurrentPositionX, CurrentPositionY];

            if (CurrentCell == null)
            {
                MazeCells[CurrentPositionX, CurrentPositionY] = new MazeCell(this);
                CurrentCell = MazeCells[CurrentPositionX, CurrentPositionY];
            }

            CurrentCell.PositionX = PositionX;
            CurrentCell.PositionY = PositionY;
            CurrentCell.NorthBlocked = northBlocked;
            CurrentCell.SouthBlocked = southBlocked;
            CurrentCell.WestBlocked = westBlocked;
            CurrentCell.EastBlocked = eastBlocked;
        }

        public void Play()
        {
            try
            {
                TakeALook();
                UpdateScreen();

                while (!Completed)
                {
                    var newDirection = CalculateNewDirection(CurrentCell);
                    UpdateScreen();

                    Move(newDirection);

                    UpdateScreen();

                    System.Threading.Thread.Sleep(SleepTimeOut);
                }

                GameFinishedEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                GameErrorEvent?.Invoke(this, EventArgs.Empty);
            }

        }

        private EDirection CalculateNewDirection(MazeCell mazeCell)
        {
            var newDirection = EDirection.None;

            if (CurrentCell.CanGoEast())
            {
                newDirection = EDirection.East;
            }
            else if (CurrentCell.CanGoSouth())
            {
                newDirection = EDirection.South;
            }
            else if (CurrentCell.CanGoWest())
            {
                newDirection = EDirection.West;
            }
            else if (CurrentCell.CanGoNorth())
            {
                newDirection = EDirection.North;
            }

            if (newDirection == EDirection.None)
            {
                mazeCell.Banned = true;
                CurrentPath.RemoveAll(x => x.Banned);
                var lastNonBannedCell = CurrentPath.LastOrDefault();

                CurrentPath.Remove(CurrentPath.Last());
                if (lastNonBannedCell != null)
                {
                    if (lastNonBannedCell.NewDirection == EDirection.North) return EDirection.South;
                    if (lastNonBannedCell.NewDirection == EDirection.South) return EDirection.North;
                    if (lastNonBannedCell.NewDirection == EDirection.East) return EDirection.West;
                    if (lastNonBannedCell.NewDirection == EDirection.West) return EDirection.East;
                }
                else
                {
                    return EDirection.West;
                }
            }

            return newDirection;
        }

        public void UpdateScreen()
        {
            UpdateScreenEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
