namespace Maze
{
    //TODO CCC Delete hardcoded values
    public class MazeConfigurations
    {
        public Guid? MazeId { get; set; } = Guid.Parse("26d52c2b-dd09-4553-ad37-4be6ee06f16b");
        public Guid? GameId { get; set; } = Guid.Parse("4dd7da3c-2824-4d56-8f77-bad7930f5f49");
        public int Width { get; set; } = 25; 
        public int Height { get; set; } = 25;
        public string BaseUrl { get; set; } = "https://mazerunnerapi6.azurewebsites.net/api/";
        public string APIKey { get; set; } = "CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==";
        public int SleepTimeOut { get; set; } = 10;
       
    }
}
