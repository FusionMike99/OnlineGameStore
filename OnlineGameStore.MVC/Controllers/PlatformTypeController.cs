using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("platform-types")]
    public class PlatformTypeController : Controller
    {
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IMapper _mapper;

        public PlatformTypeController(IPlatformTypeService platformTypeService,
            IMapper mapper)
        {
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public ViewResult Create()
        {
            var editPlatformTypeViewModel = new EditPlatformTypeViewModel();

            return View(editPlatformTypeViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] EditPlatformTypeViewModel platformType)
        {
            VerifyPlatformType(platformType);

            if (!ModelState.IsValid)
            {
                return View(platformType);
            }

            var mappedPlatformType = _mapper.Map<PlatformType>(platformType);

            _platformTypeService.CreatePlatformType(mappedPlatformType);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        [HttpGet("update/{platformTypeId}")]
        public IActionResult Update([FromRoute] int? platformTypeId)
        {
            if (!platformTypeId.HasValue)
            {
                return BadRequest("Need to pass platform type id");
            }

            var platformType = _platformTypeService.GetPlatformTypeById(platformTypeId.Value);

            if (platformType == null)
            {
                return NotFound("Platform type has not been found");
            }

            var editPlatformTypeViewModel = _mapper.Map<EditPlatformTypeViewModel>(platformType);

            return View(editPlatformTypeViewModel);
        }

        [HttpPost, Route("update", Name = "platformtypeupdate")]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] EditPlatformTypeViewModel platformType)
        {
            VerifyPlatformType(platformType);

            if (!ModelState.IsValid)
            {
                return View(platformType);
            }

            var mappedPlatformType = _mapper.Map<PlatformType>(platformType);

            _platformTypeService.EditPlatformType(mappedPlatformType);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        [HttpGet("{platformTypeId}")]
        public IActionResult GetPlatformTypeById([FromRoute] int? platformTypeId)
        {
            if (!platformTypeId.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            var platformType = _platformTypeService.GetPlatformTypeById(platformTypeId.Value);

            if (platformType == null)
            {
                return NotFound("Platform type has not been found");
            }

            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformType);

            return View("Details", platformTypeViewModel);
        }

        [HttpGet]
        public IActionResult GetPlatformTypes()
        {
            var platformTypes = _platformTypeService.GetAllPlatformTypes();

            var platformTypesViewModel = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(platformTypes);

            return View("Index", platformTypesViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            _platformTypeService.DeletePlatformType(id.Value);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        private void VerifyPlatformType(EditPlatformTypeViewModel platformType)
        {
            if (_platformTypeService.CheckTypeForUniqueness(platformType.Id, platformType.Type))
            {
                ModelState.AddModelError("Type", "Type with same value exist.");
            }
        }
    }
}
