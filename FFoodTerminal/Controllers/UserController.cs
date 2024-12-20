﻿using FFoodTerminal.Domain.Helpers;
using FFoodTerminal.Domain.ViewModels.User;
using FFoodTerminal.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FFoodTerminal.Controllers
{
        public class UserController : Controller
        {
            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<IActionResult> GetUsers()
            {
                var response = await _userService.GetUsers();
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(response.Data);
                }
                return RedirectToAction("Index", "Home");
            }

            public async Task<IActionResult> DeleteUser(long id)
            {
                var response = await _userService.DeleteUser(id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetUsers");
                }
                return RedirectToAction("Index", "Home");
            }

            public IActionResult Save() => PartialView();

            [HttpPost]
            public async Task<IActionResult> Save(UserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var response = await _userService.Create(model);
                    if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return Json(new { description = response.DescriptionError });
                    }
                    return BadRequest(new { errorMessage = response.DescriptionError });
                }
                var errorMessage = ModelState.Values
                    .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
                return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
            }

            [HttpPost]
            public JsonResult GetRoles()
            {
                var types = _userService.GetRoles();
                return Json(types.Data);
            }
        }
    
}
