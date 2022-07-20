using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
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
        public IActionResult Create()
        {
            var editPublisherViewModel = new EditPublisherViewModel();

            return View(editPublisherViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] EditPublisherViewModel publisher)
        {
            VerifyPublisher(publisher);

            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            var mappedPublisher = _mapper.Map<Publisher>(publisher);

            var createdPublisher = _publisherService.CreatePublisher(mappedPublisher);

            return RedirectToAction(nameof(GetPublisherByCompanyName),
                new { companyName = createdPublisher.CompanyName });
        }

        [HttpGet("update/{companyName}")]
        public IActionResult Update([FromRoute] string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return BadRequest();
            }

            var publisher = _publisherService.GetPublisherByCompanyName(companyName);

            if (publisher == null)
            {
                return NotFound();
            }

            var editPublisherViewModel = _mapper.Map<EditPublisherViewModel>(publisher);

            return View(editPublisherViewModel);
        }

        [HttpPost("update/{companyName}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]string companyName, [FromForm] EditPublisherViewModel publisher)
        {
            VerifyPublisher(publisher);

            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            var mappedPublisher = _mapper.Map<Publisher>(publisher);

            var editedPublisher = _publisherService.EditPublisher(companyName, mappedPublisher);

            return RedirectToAction(nameof(GetPublisherByCompanyName),
                new { companyName = editedPublisher.CompanyName });
        }

        [HttpGet("{companyName}")]
        public IActionResult GetPublisherByCompanyName([FromRoute] string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return BadRequest();
            }

            var publisher = _publisherService.GetPublisherByCompanyName(companyName);

            if (publisher == null)
            {
                return NotFound();
            }

            var publisherViewModel = _mapper.Map<PublisherViewModel>(publisher);

            return View("Details", publisherViewModel);
        }

        [HttpGet]
        public IActionResult GetPublishers()
        {
            var publishers = _publisherService.GetAllPublishers();

            var publishersViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publishers);

            return View("Index", publishersViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            _publisherService.DeletePublisher(id.Value);

            return RedirectToAction(nameof(GetPublishers));
        }

        private void VerifyPublisher(EditPublisherViewModel publisher)
        {
            var checkResult = _publisherService.CheckCompanyNameForUnique(publisher.Id, publisher.CompanyName);

            if (checkResult)
            {
                ModelState.AddModelError(nameof(PublisherViewModel.CompanyName),
                    ErrorMessages.CompanyNameExist);
            }
        }
    }
}