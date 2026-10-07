using System.ClientModel;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

// Read the user-secrets key stores
var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

string? apiKey = config["OpenAIKey"];
string? modelName = config["ModelName"];
string? endPoint = config["Endpoint"];

// //start chatclient instantiate
// //defaukt using OpenAI Key
// IChatClient chatClient = new OpenAIClient(apiKey)
//     .GetChatClient(modelName).AsIChatClient();

//using cheaper Omniroute
IChatClient chatClient = new OpenAIClient(
        new ApiKeyCredential(apiKey),
        new OpenAIClientOptions()
        {
            Endpoint = new Uri(endPoint),
        }
    ).GetChatClient(modelName).AsIChatClient();

//start send first AI Prompt
var response = await chatClient.GetResponseAsync("Explain dependency injection in C# in simple terms.");

Console.WriteLine(response);
    