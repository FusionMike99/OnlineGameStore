using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Models.General;
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
        public async Task<IActionResult> Create([FromForm] EditPlatformTypeViewModel platformType)
        {
            await VerifyPlatformType(platformType);

            if (!ModelState.IsValid)
            {
                return View(platformType);
            }

            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);
            await _platformTypeService.CreatePlatformTypeAsync(mappedPlatformType);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        [HttpGet("update/{platformTypeId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid platformTypeId)
        {
            var platformType = await _platformTypeService.GetPlatformTypeByIdAsync(platformTypeId);

            if (platformType == null)
            {
                return NotFound();
            }

            var editPlatformTypeViewModel = _mapper.Map<EditPlatformTypeViewModel>(platformType);

            return View(editPlatformTypeViewModel);
        }

        [HttpPost("update/{platformTypeId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid platformTypeId, [FromForm] EditPlatformTypeViewModel platformType)
        {
            if (platformTypeId != platformType.Id)
            {
                return NotFound();
            }
            
            await VerifyPlatformType(platformType);

            if (!ModelState.IsValid)
            {
                return View(platformType);
            }

            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);
            await _platformTypeService.EditPlatformTypeAsync(mappedPlatformType);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        [HttpGet("{platformTypeId:guid}")]
        public async Task<IActionResult> GetPlatformTypeById([FromRoute] Guid platformTypeId)
        {
            var platformType = await _platformTypeService.GetPlatformTypeByIdAsync(platformTypeId);

            if (platformType == null)
            {
                return NotFound();
            }

            var platformTypeViewModel = _mapper.Map<PlatformTypeViewModel>(platformType);

            return View("Details", platformTypeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatformTypes()
        {
            var platformTypes = await _platformTypeService.GetAllPlatformTypesAsync();
            var platformTypesViewModel = _mapper.Map<IEnumerable<PlatformTypeViewModel>>(platformTypes);

            return View("Index", platformTypesViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] Guid id)
        {
            await _platformTypeService.DeletePlatformTypeAsync(id);

            return RedirectToAction(nameof(GetPlatformTypes));
        }

        private async Task VerifyPlatformType(EditPlatformTypeViewModel platformType)
        {
            var checkResult = await _platformTypeService.CheckTypeForUniqueAsync(platformType.Id, platformType.Type);

            if (checkResult)
            {
                ModelState.AddModelError(nameof(PlatformTypeViewModel.Type), ErrorMessages.PlatformTypeExist);
            }
        }
    }
}