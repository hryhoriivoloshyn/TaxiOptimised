using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxiOptimised.Models;
using TaxiOptimised.ViewModels;

namespace TaxiOptimised.Controllers
{
    public class GreedyAlgorithmController : Controller
    {
        private TaxiOptDBContext db;
        private GreedyAlgorithmViewModel viewModel;

        public GreedyAlgorithmController(TaxiOptDBContext context)
        {
            db = context;
            viewModel = new GreedyAlgorithmViewModel()
            {
                Drivers = db.Drivers.ToList(),
                Orders = db.Orders.ToList(),
                DriverOrders = db.DriverOrders.ToList()
            };
        }

        private class CalculationResult
        {
            public IEnumerable<DriverOrder> ResultedSequence { get; set; }
            public double Result { get; set; }
        }
        private async Task<CalculationResult> CalculateGreedyAlgorithm(GreedyAlgorithmViewModel viewModel)
        {
          
            List<DriverOrder> driverOrders = viewModel.DriverOrders.ToList();

            double maxProfitRatio = 0;
            DriverOrder bestOption = new DriverOrder();
            double profitRatio;
            List<DriverOrder> resultedSequence=new List<DriverOrder>();
            double greedyResult = 0;
            while (driverOrders.Count!=0)
            {
                maxProfitRatio = 0;
                
                foreach (var drOrders in driverOrders)
                {

                    profitRatio = (1 / drOrders.DistanceToDriver + (double)drOrders.Order.Distance);
                    if (profitRatio > maxProfitRatio)
                    {
                        maxProfitRatio = profitRatio;
                        bestOption = drOrders;
                    }
                }
                resultedSequence.Add(bestOption);
                greedyResult += maxProfitRatio;
                
                driverOrders.RemoveAll(p=>p.DriverId==bestOption.DriverId);
                driverOrders.RemoveAll(p => p.OrderId == bestOption.OrderId);
            }

            CalculationResult result = new CalculationResult()
            {
                ResultedSequence = resultedSequence,
                Result = greedyResult
            };
            return result;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            foreach (var driverOrder in viewModel.DriverOrders)
            {
                driverOrder.Driver = await db.Drivers.FirstOrDefaultAsync(d => d.DriverId == driverOrder.DriverId);
                driverOrder.Order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == driverOrder.OrderId);

            }

            CalculationResult algResult = await CalculateGreedyAlgorithm(viewModel);
            viewModel.ResultedSequence = algResult.ResultedSequence;
            viewModel.Result = algResult.Result;
            return View(viewModel);
        }
    }
}
