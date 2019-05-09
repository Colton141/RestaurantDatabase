using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using RestaurantList.Models;

namespace RestaurantList.Controllers
{
  public class CuisinesController : Controller
  {

    [HttpGet("/cuisines")]
    public ActionResult Index()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();
      return View(allCuisines);
    }

    [HttpGet("/cuisines/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/cuisines")]
    public ActionResult Create(string cuisineName)
    {
      Cuisine newCuisine = new Cuisine(cuisineName);
      List<Cuisine> allCuisines = Cuisine.GetAll();
      newCuisine.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/cuisines/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine selectedCuisine = Cuisine.Find(id);
      List<Item> cuisineItems = selectedCuisine.GetRestaurants;
      model.Add("cuisine", selectedCuisine);
      model.Add("cuisineItems", cuisineItems);
      return View(selectedCuisine);
    }

    [HttpPost("/cuisines/{cuisineId}/items")]
    public ActionResult Create(int cuisineId, string restaurantName)
    {
      Cuisine foundCuisine = Cuisine.Find(cuisineId);
      Item newItem = new Item(restaurantName, cuisineId);
      newItem.Save();
      foundCuisine.GetRestaurants;
      // List<Item> cuisineItems = foundCuisine.GetRestaurants;
      // model.Add("items", cuisineItems);
      // model.Add("cuisine", foundCuisine);
      return View("Show", foundCuisine);
    }

    [HttpPost("/cuisines/{cuisineId}/delete")]
    public ActionResult Delete(int cuisineId)
    {
      Cuisine cuisine = Cuisine.Find(cuisineId);
      cuisine.DeleteCuisine();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("cuisine", cuisine);
      return RedirectToAction("Index");
    }

  }
}
