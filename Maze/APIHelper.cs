using System.Text.Json;
using Maze.DTO;

namespace Maze
{
    public class APIHelper
    {
        public string UrlBase { get; set; }
        private string ApiKey { get; set; }
        public APIHelper(string urlBase, string apiKey)
        {
            UrlBase = urlBase;
            ApiKey = apiKey;
        }

        public CreateMazeResponseDTO CreateMaze(CreateMazeRequestDTO createMazeRequestDTO)
        {
            //TODO CCC: Check recycle techniques for httpclient object
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UrlBase}Maze{GetApiKeyParameter()}");
            request.Content = new StringContent(JsonSerializer.Serialize(createMazeRequestDTO), null, "text/plain");

            var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            CreateMazeResponseDTO? createMazeResponseDTO = JsonSerializer.Deserialize<CreateMazeResponseDTO>(response.Content.ReadAsStringAsync().ToString());

            return createMazeResponseDTO == null ? throw new Exception("Maze API is failing") : createMazeResponseDTO;
        }

        public CreateGameResponseDTO CreateGame(CreateGameRequestDTO createGameRequestDTO, Guid MazeId)
        {
            //TODO CCC: Check recycle techniques for httpclient object
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UrlBase}Game/{MazeId}{GetApiKeyParameter()}");
            request.Content = new StringContent(JsonSerializer.Serialize(createGameRequestDTO), null, "text/plain");

            var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            CreateGameResponseDTO? createGameResponseDTO = JsonSerializer.Deserialize<CreateGameResponseDTO>(response.Content.ReadAsStringAsync().Result);
            return createGameResponseDTO == null ? throw new Exception("Maze API is failing") : createGameResponseDTO;
        }

        public ResetGameResponseDTO ResetGame(ResetGameRequestDTO resetGameRequestDTO, Guid MazeId, Guid GameId)
        {
            //TODO CCC: Check recycle techniques for httpclient object
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UrlBase}Game/{MazeId}/{GameId}{GetApiKeyParameter()}");
            request.Content = new StringContent(JsonSerializer.Serialize(resetGameRequestDTO), null, "text/plain");

            var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            ResetGameResponseDTO? resetGameResponseDTO = JsonSerializer.Deserialize<ResetGameResponseDTO>(response.Content.ReadAsStringAsync().Result);
            return resetGameResponseDTO == null ? throw new Exception("Maze API is failing") : resetGameResponseDTO;
        }

        public MoveMazeResponseDTO Move(MoveMazeRequestDTO moveMazeRequestDTO, Guid MazeId, Guid GameId)
        {
            //TODO CCC: Check recycle techniques for httpclient object
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UrlBase}Game/{MazeId}/{GameId}{GetApiKeyParameter()}");
            request.Content = new StringContent(JsonSerializer.Serialize(moveMazeRequestDTO), null, "text/plain");

            var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            MoveMazeResponseDTO? mazeMoveResponseDTO = JsonSerializer.Deserialize<MoveMazeResponseDTO>(response.Content.ReadAsStringAsync().Result);
            return mazeMoveResponseDTO == null ? throw new Exception("Maze API is failing") : mazeMoveResponseDTO;
        }

        public TakeALookMazeResponseDTO TakeALook(Guid MazeId, Guid GameId)
        {
            //TODO CCC: Check recycle techniques for httpclient object
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{UrlBase}Game/{MazeId}/{GameId}{GetApiKeyParameter()}");

            var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            TakeALookMazeResponseDTO? takeALookMazeResponseDTO = JsonSerializer.Deserialize<TakeALookMazeResponseDTO>(response.Content.ReadAsStringAsync().Result);
            return takeALookMazeResponseDTO == null ? throw new Exception("Maze API is failing") : takeALookMazeResponseDTO;
        }

        private string GetApiKeyParameter()
        {
            if (ApiKey == null)
            {
                ApiKey = "Setting not found";
            }
            return $"?code={ApiKey}";
        }
    }
}
