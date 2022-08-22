using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("publishers")]
    public class PublisherController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService,
            IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        public IActionResult Create()
        {
            var editPublisherViewModel = new EditPublisherViewModel();

            return View(editPublisherViewModel);
        }

        [HttpPost("new")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EditPublisherViewModel publisher)
        {
            await VerifyPublisher(publisher);

            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            var mappedPublisher = _mapper.Map<PublisherModel>(publisher);

            var createdPublisher = await _publisherService.CreatePublisherAsync(mappedPublisher);

            return RedirectToAction(nameof(GetPublisherByCompanyName),
                new { companyName = createdPublisher.CompanyName });
        }

        [HttpGet("update/{companyName}")]
        [AuthorizeByRoles(Permissions.PublisherPermission)]
        public async Task<IActionResult> Update([FromRoute] string companyName)
        {
            if (!User.IsInRoles(Permissions.ManagerPermission) && User.GetPublisherName() != companyName)
            {
                return Forbid();
            }

            var publisher = await _publisherService.GetPublisherByCompanyNameAsync(companyName);
            if (publisher == null)
            {
                return NotFound();
            }

            var editPublisherViewModel = _mapper.Map<EditPublisherViewModel>(publisher);

            return View(editPublisherViewModel);
        }

        [HttpPost("update/{companyName}")]
        [AuthorizeByRoles(Permissions.PublisherPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]string companyName,
            [FromForm] EditPublisherViewModel publisher)
        {
            await VerifyPublisher(publisher);

            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            var mappedPublisher = _mapper.Map<PublisherModel>(publisher);

            var editedPublisher = await _publisherService.EditPublisherAsync(mappedPublisher);

            return RedirectToAction(nameof(GetPublisherByCompanyName),
                new { companyName = editedPublisher.CompanyName });
        }

        [HttpGet("{companyName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublisherByCompanyName([FromRoute] string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return BadRequest();
            }

            var publisher = await _publisherService.GetPublisherByCompanyNameAsync(companyName);

            if (publisher == null)
            {
                return NotFound();
            }

            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            return View("Details", publisherViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();

            var publishersViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publishers);

            return View("Index", publishersViewModel);
        }

        [HttpPost("remove")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] Guid id)
        {
            await _publisherService.DeletePublisherAsync(id);

            return RedirectToAction(nameof(GetPublishers));
        }

        private async Task VerifyPublisher(EditPublisherViewModel publisher)
        {
            var checkResult = await _publisherService.CheckCompanyNameForUniqueAsync(publisher.Id, publisher.CompanyName);
            if (checkResult)
            {
                ModelState.AddModelError(nameof(PublisherViewModel.CompanyName),
                    ErrorMessages.CompanyNameExist);
            }
        }
    }
}