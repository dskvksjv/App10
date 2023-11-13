using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

public class ConsultationController : Controller
{
    public IActionResult RegistrationForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistrationForm(ConsultationFormModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Success");
        }

        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
