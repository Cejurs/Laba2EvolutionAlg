using Evolution.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Evolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static Population population=new Population(5,true);

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<object> GerFunctionPoints()
        {
            var data=new List<object>();
            var labels = new List<string>();
            var values = new List<double>();
            var colors = new List<string>();
            for (int x = -10; x <= 53; x++)
            {
                labels.Add(x.ToString());
                values.Add(Function.GetY(x));
                colors.Add("blue");

            }
            data.Add(labels);
            data.Add(values);
            data.Add(colors);
            return data;
        }

        [HttpGet]
        public List<object> GetIndividualPoints()
        {
            var data = new List<object>();
            var points = new List<object>();
            var individualsStrings = new List<string>();
            population.Individuals.ToList().ForEach(p => {
                individualsStrings.Add(p.ToString());
                if(p.Gene>=-10 && p.Gene<=53) points.Add(p.Gene+10);
                });
            data.Add(points);
            data.Add(individualsStrings);
            if (population.Contains(29)) data.Add(true);
            else data.Add(false);
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