using System;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DATABASE;
using WebApp;
using System.IO;
using System.Data;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    //[Route("api/[controller]")]
    public class RecipeController : Controller
    {
        //api/Recipe/GetRandomRecipe
        [HttpGet]
        [Route("api/recipes/random/query=random")]
        public async Task<JsonResult> GetRandomRecipe()
        {
            try
            {
                string urlForRandomSearch = "https://api.spoonacular.com/recipes/random?number=1&apiKey=a53bfa5a132745ae8bedb31d3d03443e";
                string response;
                using HttpClient client = new HttpClient();

                response = await client.GetStringAsync(urlForRandomSearch);
                InputRecipeByRandom inputRandomRecipe = JsonConvert.DeserializeObject<InputRecipeByRandom>(response);

                var recipeId = (from recipe in inputRandomRecipe.recipes select recipe.id).FirstOrDefault();
                string urlForInformationOfRecipe =
                    "https://api.spoonacular.com/recipes/"
                    + recipeId.ToString()
                    + "/information?apiKey=a53bfa5a132745ae8bedb31d3d03443e";

                response = await client.GetStringAsync(urlForInformationOfRecipe);


                FullRecipeInformation outputRandomRecipe = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                Response.StatusCode = 200;
                return Json(outputRandomRecipe);
            }
            catch (Exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Failure";
                Response.StatusCode = 404;
                return Json(errorResponse);
            }
        }

        //api/Recipe/GetRecipeByName/query=pasta
        [HttpGet]
        [Route("api/recipes/search/query={name}")]
        public async Task<JsonResult> GetRecipeByName(string name)
        { 
            try
            {
                int recipeId = 0;
                string response;
                string urlForSearchByName =
                    "https://api.spoonacular.com/recipes/search?query="
                    + name + "&number=1&apiKey=a53bfa5a132745ae8bedb31d3d03443e";
                string urlForInformationOfRecipe;
                using HttpClient client = new HttpClient();

                response = await client.GetStringAsync(urlForSearchByName);
                InputRecipeByName inputRecipeByName = JsonConvert.DeserializeObject<InputRecipeByName>(response);
                foreach (var info in inputRecipeByName.results)
                    recipeId = info.id;
                urlForInformationOfRecipe =
                    "https://api.spoonacular.com/recipes/"
                    + recipeId.ToString()
                    + "/information?apiKey=a53bfa5a132745ae8bedb31d3d03443e";

                response = await client.GetStringAsync(urlForInformationOfRecipe);
                FullRecipeInformation outputRecipeByName = JsonConvert.DeserializeObject<FullRecipeInformation>(response);

                Response.StatusCode = 200;
                return Json(outputRecipeByName);
            }
            catch (Exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Failure, recipe not found or wrong name";
                Response.StatusCode = 404;
                return Json(errorResponse);
            }
        }
        //api/recipes/byingredients?ingredients={ingr1,ingr2,ingr3...}
        [HttpGet]
        [Route("api/recipes/byingredients/ingredients={ingredients}")]
        public async Task<JsonResult> GetRecipeByIngredients(string ingredients)
        {
            try
            {
                int recipeId;
                string response = "";
                string ingredience = "";

                List<string> ingredient = new List<string>();
                foreach (var ingr in ingredients.Split(','))
                {
                    ingredient.Add(ingr);
                }
                ingredience = string.Join(",+", ingredient.ToArray());
                
                string urlForSearchByIngredients =
                    "https://api.spoonacular.com/recipes/findByIngredients?ingredients="
                    + ingredience +
                    "&number=1&apiKey=a53bfa5a132745ae8bedb31d3d03443e";

                using HttpClient client = new HttpClient();
                response = await client.GetStringAsync(urlForSearchByIngredients);
                
                response = response.Substring(1, response.Length - 2);

                InputRecipeByIngredient recipeByIngredient = JsonConvert.DeserializeObject<InputRecipeByIngredient>(response);
                if (recipeByIngredient != null)
                {
                    recipeId = recipeByIngredient.id;

                    string urlForInformationOfRecipe =
                        "https://api.spoonacular.com/recipes/"
                        + recipeId.ToString()
                        + "/information?apiKey=a53bfa5a132745ae8bedb31d3d03443e";

                    response = await client.GetStringAsync(urlForInformationOfRecipe);
                    FullRecipeInformation outputRecipeByIngredients = JsonConvert.DeserializeObject<FullRecipeInformation>(response);

                    Response.StatusCode = 200;
                    return Json(outputRecipeByIngredients);
                }
                else
                {
                    ErrorResponse errorResponse = new ErrorResponse();
                    errorResponse.code = 404;
                    errorResponse.message = "Failure, recipe not found or wrong ingredients";
                    Response.StatusCode = 404;
                    return Json(errorResponse);
                }
            }
            catch (Exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Failure, recipe not found or wrong ingredients";
                Response.StatusCode = 404;
                return Json(errorResponse);
            }
        }

        [HttpGet]
        [Route("api/recipes/getInformation/recipeid={recipeId}")]
        public async Task<JsonResult> GetInformationOfRecipe(int recipeId)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string response;
                string urlForInformationOfRecipe =
                    "https://api.spoonacular.com/recipes/"
                    + recipeId.ToString()
                    + "/information?apiKey=a53bfa5a132745ae8bedb31d3d03443e";

                response = await client.GetStringAsync(urlForInformationOfRecipe);

                FullRecipeInformation outputInformationOfRecipe = JsonConvert.DeserializeObject<FullRecipeInformation>(response);
                return Json(outputInformationOfRecipe);
            }
            catch (Exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Failure, recipe not found or wrong id";
                Response.StatusCode = 404;
                return Json(errorResponse);
            }  
        }

        [HttpGet]
        [Route("api/recipes/getUsersRecipe/usercode={userCode}")]
        public JsonResult GetUsersRecipe(long userCode)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    int ownerId = (from user in db.Users where user.userCode == userCode select user.id.Value).First();
                    var matchRecipes = from recipes in db.Recipes where recipes.UserId == ownerId select recipes;
                    
                    
                    List<FullRecipeInformation> userRecipes = new List<FullRecipeInformation>();
                    
                    foreach (var userRecipe in matchRecipes)
                    {
                        FullRecipeInformation recipe = new FullRecipeInformation();
                        recipe.id = userRecipe.recipeCode;
                        recipe.title = userRecipe.name;
                        userRecipes.Add(recipe);
                    }      
                    if (userRecipes.Count != 0)
                    {
                        return Json(userRecipes);
                    }
                    else
                    {
                        ErrorResponse errorResponse = new ErrorResponse();
                        errorResponse.code = 404;
                        errorResponse.message = "Failure, recipes not found or somesthing went wrong";
                        Response.StatusCode = 404;
                        return Json(errorResponse);
                    }
                }
            }
            catch (Exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Failure, recipes not found or somesthing went wrong";
                Response.StatusCode = 404;
                return Json(errorResponse);
            }
        }


        // POST api/<controller>
        [HttpPost]
        [Route("api/recipe/adduserslist")]
        public async Task<JsonResult> Post([FromBody] dataBaseUser userInfo)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    dataBaseUser user = new dataBaseUser();
                    user.userCode = userInfo.userCode;
                    user.name = userInfo.name;
                    var usersCodes = from User in db.Users where User.userCode == user.userCode select User.userCode;
                    if (usersCodes.Count() == 0)
                    {
                        await db.Users.AddAsync(user);
                        await db.SaveChangesAsync();
                        Response.StatusCode = 200;
                        DoneResponse doneResponse = new DoneResponse();
                        doneResponse.status = "Done";
                        doneResponse.message = "User added";
                        return Json(doneResponse);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        ErrorResponse errorResponse = new ErrorResponse();
                        errorResponse.code = 404;                     
                        errorResponse.message = "User already added";
                        return Json(errorResponse);
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Ooops.. Something went wrong";
                return Json(errorResponse);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("api/recipes/AddRecipe/usercode={userCode}")]
        public async  Task<JsonResult> Put(long userCode, [FromBody]FullRecipeInformation jsonValue)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    
                    dataBaseRecipe dataBaseRecipe = new dataBaseRecipe();

                    int ownerId = (from user in db.Users where user.userCode == userCode select user.id.Value).First();
                    int recipeMatches = (from r in db.Recipes where r.UserId == ownerId && r.recipeCode == jsonValue.id select r).Count();
                    if(recipeMatches == 0)
                    {
                        dataBaseRecipe.UserId = (from u in db.Users where u.userCode == userCode select u.id.Value).First();
                        dataBaseRecipe.name = jsonValue.title;
                        dataBaseRecipe.recipeCode = jsonValue.id.Value;

                        await db.Recipes.AddAsync(dataBaseRecipe);
                        await db.SaveChangesAsync();

                        Response.StatusCode = 200;
                        DoneResponse doneResponse = new DoneResponse();
                        doneResponse.status = "Done";
                        doneResponse.message = "Recipe added";
                        return Json(doneResponse);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        ErrorResponse errorResponse = new ErrorResponse();
                        errorResponse.code = 404;
                        errorResponse.message = "Recipe already added";
                        return Json(errorResponse);
                    }        
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "User not found or something went wrong";
                return Json(errorResponse);
            }
        }



        // DELETE api/<controller>/5
        [HttpDelete("api/recipes/deleteRecipe/usercode={userCode},recipeid={recipeCode}")]
        public async Task<JsonResult> Delete(int userCode , int recipeCode)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    dataBaseUser User = new dataBaseUser();
                    dataBaseRecipe Recipe = new dataBaseRecipe();


                    int userId = (from user in db.Users where user.userCode == userCode select user.id).First().Value;
                    int recipeId = (from recipe in db.Recipes where recipe.recipeCode == recipeCode
                                    && recipe.UserId == userId select recipe.id).First().Value;
                    
                    Recipe.id = recipeId;
                    db.Recipes.Remove(Recipe);
                    await db.SaveChangesAsync();
                    Response.StatusCode = 200;
                    DoneResponse doneResponse = new DoneResponse();
                    doneResponse.status = "Done";
                    doneResponse.message = "Recipe deleted";
                    return Json(doneResponse);
                }
            }
            catch
            {
                Response.StatusCode = 404;
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.code = 404;
                errorResponse.message = "Recipe not found or recipe already deleted";
                return Json(errorResponse);
            }

        }
    }
}

/*using (StreamWriter streamWriter = new StreamWriter("E:\\C#projects\\WebApi\\WebApp\\bin\\Debug\\netcoreapp3.1\\json.json", false))
{
    streamWriter.Write(ingredient[0]);
}*/
