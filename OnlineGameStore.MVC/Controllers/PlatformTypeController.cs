using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("platform-types")]
    public class PlatformTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlatformTypeService _platformTypeService;

        public PlatformTypeController(IPlatformTypeService platformTypeService,
            IMapper mapper)
        {
            _platformTypeService = platformTypeService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public IActionResult Create()
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

        [HttpGet("update/{platformTypeId:int}")]
        public IActionResult Update([FromRoute] int? platformTypeId)
        {
            if (!platformTypeId.HasValue)
            {
                return BadRequest();
            }

            var platformType = _platformTypeService.GetPlatformTypeById(platformTypeId.Value);

            if (platformType == null)
            {
                return NotFound();
            }

            var editPlatformTypeViewModel = _mapper.Map<EditPlatformTypeViewModel>(platformType);

            return View(editPlatformTypeViewModel);
        }

        [HttpPost("update/{platformTypeId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int platformTypeId, [FromForm] EditPlatformTypeViewModel platformType)
        {
            if (platformTypeId != platformType.Id)
            {
                return NotFound();
            }
            
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
                return BadRequest();
            }

            var platformType = _platformTypeService.GetPlatformTypeById(platformTypeId.Value);

            if (platformType == null)
            {
                return NotFound();
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
                return BadRequest();
            }

            _platformTypeService.DeletePlatformType(id.Value);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        private void VerifyPlatformType(EditPlatformTypeViewModel platformType)
        {
            var checkResult = _platformTypeService.CheckTypeForUnique(platformType.Id, platformType.Type);

            if (checkResult)
            {
                ModelState.AddModelError(nameof(PlatformTypeViewModel.Type), ErrorMessages.PlatformTypeExist);
            }
        }
    }
}