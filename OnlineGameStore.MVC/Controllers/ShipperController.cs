using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    public class ShipperController : Controller
    {
        private readonly IShipperService _shipperService;
        private readonly IMapper _mapper;

        public ShipperController(IShipperService shipperService,
            IMapper mapper)
        {
            _shipperService = shipperService;
            _mapper = mapper;
        }

        [HttpGet("shippers")]
        public async Task<IActionResult> GetShippers()
        {
            var shippers = await _shipperService.GetAllShippersAsync();

            var shippersViewModel = _mapper.Map<IEnumerable<ShipperViewModel>>(shippers);

            return View("Index", shippersViewModel);
        }
    }
}