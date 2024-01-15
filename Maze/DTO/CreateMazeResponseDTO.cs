namespace Maze.DTO
{
    public class CreateMazeResponseDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Guid MazeUid { get; set; }
    }
}
