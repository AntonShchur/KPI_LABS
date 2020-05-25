using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Linq;
using System.Collections.Generic;
using WebApp;
using System.Diagnostics.CodeAnalysis;
using DATABASE;
using System.Text;
using System.Linq.Expressions;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Awesome
{
    class Program
    {
        static ITelegramBotClient botClient;
        static Dictionary<long, string> usersCommand = new Dictionary<long, string>();
        static void Main()
        {
            botClient = new TelegramBotClient("1122062389:AAGRmAD4dZCI6G6JM_w--FTiEoc0qDZTeVA");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            
            botClient.StartReceiving();

            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                if (usersCommand.ContainsKey(e.Message.Chat.Id) != true)
                {
                    usersCommand.Add(e.Message.Chat.Id, "START");
                }
                Console.WriteLine(usersCommand[e.Message.Chat.Id]);
                switch (e.Message.Text.Trim().ToLower())
                {
                    case "/start":
                        ShowInfo(e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "/showinfo":
                        ShowInfo(e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "/randomrecipe":
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Wait for random recipe...");
                        GetRandomRecipe(e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "/searchbyname":
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Write a name of recipe (for example: pasta)");
                        usersCommand[e.Message.Chat.Id] = "SEARCH_BY_NAME";
                        return;
                    case "/searchbyingredients":
                        await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Write a ingredient(for example: apple milk flour)");
                        usersCommand[e.Message.Chat.Id] = "SEARCH_BY_INGREDIENTS";
                        return;
                    case "/addrecipetofavorite":
                        await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Write recipe ID");
                        usersCommand[e.Message.Chat.Id] = "ADD_RECIPE_TO_FAVORITE";
                        return;
                    case "/showfavoriterecipes":
                        ShowUsersFavoriteRecipes(e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "/showrecipeinfo":
                        await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Write recipe ID");
                        usersCommand[e.Message.Chat.Id] = "SHOW_RECIPE_INFO";
                        return;
                    case "/deleterecipefromfavorite":
                        await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Write recipe ID");
                        usersCommand[e.Message.Chat.Id] = "DELETE_RECIPE_FROM_FAVORITE";
                        return;
                }
                
                switch (usersCommand[e.Message.Chat.Id])
                {
                    case "NULL":
                        return;
                    case "START":
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return; 
                    case "SEARCH_BY_NAME":
                        SearchRecipeByName(e.Message.Text.Trim(),e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "SEARCH_BY_INGREDIENTS":
                        SearchByIngredient(e.Message.Text.Trim(), e);
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;               
                    case "ADD_RECIPE_TO_FAVORITE":
                        AddRecipeToFavorite(e, e.Message.Text.Trim().ToLower());
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "SHOW_RECIPE_INFO":
                        ShowFavoriteRecipe(e,e.Message.Text.ToLower().Trim());
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                    case "DELETE_RECIPE_FROM_FAVORITE":
                        DeleteRecipeFromFavorite(e, e.Message.Text.ToLower().Trim());
                        usersCommand[e.Message.Chat.Id] = "NULL";
                        return;
                }
            }
        }

        public static async void ShowInfo(MessageEventArgs e)
        {
            await botClient.SendTextMessageAsync(
              chatId: e.Message.Chat,
              text: "Hi! There is a RecipeBOT. I want to help you with cooking.\n" +
              "INSTRUCTION:\n" +
              "Recipe info, add and delete command work with recipe ID, please write correctly, you can add other recipe.\n" +
              "Command list:\n" +
              "/showInfo - Show instructions;\n"+
              "/randomRecipe - Show to you a random recipe;\n" +
              "/searchByName - Searching recipe by name;\n" +
              "/searchByIngredients - Searching recipe by ingredients;\n" +
              "/addRecipeToFavorite - Add recipe to your favorite recipes list;\n" +
              "/showRecipeInfo - Show a recipe by ID;\n" +
              "/showFavoriteRecipes - Show your favorite recipes;\n" +
              "/deleteRecipeFromFavorite - Delete recipe from favorite recipes.\n" +
              "For any question ask me @CorbenDallass"

            );
            usersCommand[e.Message.Chat.Id] = "NULL";
        }

        public static async void ShowError(MessageEventArgs e)
        {
            await botClient.SendTextMessageAsync(
             chatId: e.Message.Chat,
             text: "Something went wrong, please watch instruction and try again");
            usersCommand[e.Message.Chat.Id] = "NULL";
        }
        public static async void ShowSuccess(MessageEventArgs e, string successMessage)
        {
            await botClient.SendTextMessageAsync(
             chatId: e.Message.Chat,
             text: successMessage );
            usersCommand[e.Message.Chat.Id] = "NULL";
        }
        public static async void GetRandomRecipe(MessageEventArgs e)
        {
            try
            {
                string urlForRandomSearch = "http://localhost:50894/api/recipes/random/query=random";
                string response;
                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForRandomSearch);
                FullRecipeInformation inputRandomRecipe = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                ShowRecipe(inputRandomRecipe, e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch (Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            
        }
        public static async void SearchRecipeByName(string name, MessageEventArgs e)
        {
            try
            {
                string urlForSearchByName = "http://localhost:50894/api/recipes/search/query=" + name.Trim();
                string response;
                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForSearchByName);
                FullRecipeInformation inputRecipeByName = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                ShowRecipe(inputRecipeByName, e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch (Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
        }

        public static async void SearchByIngredient(string ingredients,MessageEventArgs e)
        {
            try
            {
                List<string> ingredient = new List<string>();
                foreach (var ingr in ingredients.Trim().Split(' '))
                {
                    ingredient.Add(ingr.Trim());
                }
                ingredients = string.Join(",", ingredient.ToArray());
                string urlForSearchByIngredient = "http://localhost:50894/api/recipes/byingredients/ingredients=" + ingredients;
                string response;
                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForSearchByIngredient);
                FullRecipeInformation inputRecipeByName = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                ShowRecipe(inputRecipeByName, e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch (Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
        }

        static public async void CreateFavoriteRecipesList(MessageEventArgs e)
        {
            try
            {
                dataBaseUser user = new dataBaseUser();
                user.userCode = e.Message.Chat.Id;
                user.name = e.Message.Chat.FirstName.Trim();
                Console.WriteLine(user.name);
                string urlForPost = "http://localhost:50894/api/recipe/adduserslist";
                
                var json = JsonConvert.SerializeObject(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using HttpClient client = new HttpClient();
                var response = await client.PostAsync(urlForPost, data);
                string result = response.Content.ReadAsStringAsync().Result;
                DoneResponse doneResponse = JsonConvert.DeserializeObject<DoneResponse>(result); 
                switch (doneResponse.message.ToLower().Trim())
                {
                    case "done":                     
                        return;
                    case "user already added":    
                        return;
                    case "ooops.. something went wrong":                      
                        return;
                }
                usersCommand[e.Message.Chat.Id] = "NULL";
                
            }
            catch (Exception)
            {               
                usersCommand[e.Message.Chat.Id] = "NULL";
                return;
            }
        }

        static public async void AddRecipeToFavorite(MessageEventArgs e, string recipeId)
        {

            try
            {
                CreateFavoriteRecipesList(e);
                string urlForSearchInformation = "http://localhost:50894/api/recipes/getInformation/recipeid=" + recipeId;
                string urlForPut = "http://localhost:50894/api/recipes/AddRecipe/usercode="+e.Message.Chat.Id; 
                using HttpClient client = new HttpClient();

                var responseForSearch = await client.GetStringAsync(urlForSearchInformation);
                FullRecipeInformation inputRandomRecipe = JsonConvert.DeserializeObject<FullRecipeInformation>(responseForSearch);
                var json = JsonConvert.SerializeObject(inputRandomRecipe);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var responseForPut = await client.PutAsync(urlForPut, data);
                string result = responseForPut.Content.ReadAsStringAsync().Result;
                DoneResponse doneResponse = JsonConvert.DeserializeObject<DoneResponse>(result);
                switch(doneResponse.message.ToLower().Trim())
                {
                    case "recipe added":
                        ShowSuccess(e, "Recipe added!");
                        return;
                    case "recipe already added":
                        ShowSuccess(e, "Recipe aready added!");
                        return;
                    case "user not found or something went wrong":
                        ShowError(e);
                        return;
                }
                usersCommand[e.Message.Chat.Id] = "NULL";

            }
            catch (Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
        }

        static public async void ShowUsersFavoriteRecipes(MessageEventArgs e)
        {
            try
            {
                string message = "";
                string urlForGetUsersFavoriteRecipes = "http://localhost:50894/api/recipes/getUsersRecipe/usercode="
                    +(e.Message.Chat.Id).ToString();
                string response;
                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForGetUsersFavoriteRecipes);              
                List<FullRecipeInformation> usersFavoriteRecipes = JsonConvert.DeserializeObject<List<FullRecipeInformation>>(response);

                if(usersFavoriteRecipes.Count() != 0)
                {
                    foreach (var recipe in usersFavoriteRecipes)
                    {
                        message += $"Name:{recipe.title}\nId:{recipe.id}\n";
                    }
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: message);
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "You does not have any recipes. Try to add it.");
                }
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch (Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
        }

        static public async void ShowFavoriteRecipe(MessageEventArgs e, string recipeId)
        {
            try
            {
                string urlForSearchById = "http://localhost:50894/api/recipes/getInformation/recipeid=" + recipeId.Trim().ToLower();
                string response;
                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForSearchById);
                FullRecipeInformation recipe = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                ShowRecipe(recipe, e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch(Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }

        }
        static public async void DeleteRecipeFromFavorite(MessageEventArgs e, string recipeId)
        {
            try
            {
                string urlForDelete =
                    "http://localhost:50894/api/recipes/deleteRecipe/usercode="
                    + e.Message.Chat.Id.ToString()
                    + ",recipeid=" + recipeId.Trim().ToLower();

                using HttpClient client = new HttpClient();
                var response = await client.DeleteAsync(urlForDelete);
                string result = response.Content.ReadAsStringAsync().Result;
                DoneResponse doneResponse = JsonConvert.DeserializeObject<DoneResponse>(result);
                switch(doneResponse.message.ToLower().Trim())
                {
                    case "recipe deleted":
                        ShowSuccess(e, "Recipe deleted");
                        return;
                    case "recipe not found or recipe already deleted":
                        ShowSuccess(e, "Recipe not found or recipe already deleted");
                        return;
                }
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch(Exception)
            {
                ShowError(e);
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
        }

        static public async void ShowRecipe(FullRecipeInformation inputRecipeInformation, MessageEventArgs e)
        {
            try
            {
                string name;
                int id;
                string sourceImg;
                string instructions;
                string sourseLink;

                name = inputRecipeInformation.title;
                id = inputRecipeInformation.id;
                sourceImg = inputRecipeInformation.image;
                instructions = inputRecipeInformation.instructions;
                sourseLink = inputRecipeInformation.sourceUrl;
                var extendedIngredients = inputRecipeInformation.extendedIngredients;
                List<string> ingredients = new List<string>();
                foreach (var ingredient in extendedIngredients)
                {
                    ingredients.Add(ingredient.originalString);
                }
                string ingred = String.Join("\n", ingredients);
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Hello {e.Message.Chat.FirstName}, let's go to cook!\n");
                if (sourceImg != null)
                {
                    await botClient.SendPhotoAsync(chatId: e.Message.Chat, sourceImg);
                }
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text:
                  $"Name:{name}\n" +
                  $"Id:{id}\n"
                  );
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: $"Ingredients:\n{ingred}");

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text:  
                  $"Instructions:\n{instructions}" +
                  $"See more instructions on a sourse site:\n{sourseLink}"
                );
                usersCommand[e.Message.Chat.Id] = "NULL";
            }
            catch (Exception)
            {
                ShowError(e);
            }
            usersCommand[e.Message.Chat.Id] = "NULL";
        }
    }
}


