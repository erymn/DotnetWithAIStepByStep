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

// //01. start send first AI Prompt (SINGLE PROMPT)
// var response = await chatClient.GetResponseAsync("Explain dependency injection in C# in simple terms.");

//02. Adding a System Instruction
//An AI model can receive more than a single user prompt.
//We can provide system-level instructions that describe the role or behavior we want.
var messages = new List<ChatMessage>
{
    new ChatMessage(ChatRole.System, "You are a helpful C# programming assistant."),
    new ChatMessage(ChatRole.User, "Explain async and await in C#.")
};

var response = await chatClient.GetResponseAsync(messages);

Console.WriteLine(response);
    