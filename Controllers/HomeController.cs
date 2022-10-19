using Evolution.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Evolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static Population population;
        //private static Population? parent=null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public List<object> GetChartPoints()
        {
            var data = new List<object>();
            var labels=new List<string>();
            var values = new List<double>();
            var colors = new List<string>();
            // Берем точки наших особей
            for (int x = -10; x <= 53; x++)
            {
                labels.Add(x.ToString());
                values.Add(Function.GetY(x));
                var random = new Random();
                if (population.Contains(x))
                {
                    colors.Add("red");
                }
                else
                {
                    colors.Add("blue");
                }
                
            }
            data.Add(labels);
            data.Add(values);
            data.Add(colors);
            var individualsStrings = new List<string>();
            population.Individuals.ToList().ForEach(p => individualsStrings.Add(p.ToString()));
            data.Add(individualsStrings);
            return data;
        }

        [HttpPost]
        public void Evolve()
        {
            population = EvolutionAlgorithm.evolvePopulation(population);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public void CreatePopulation(int populationSize)
        {
            population = new Population(populationSize,true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}