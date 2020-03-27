﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.Models;

namespace vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer/display
        public ActionResult display()
        {
            var customers = GetCustomers();
            return View(customers);
        }
        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, name = "John Smith" },
                new Customer { Id = 2, name = "Mary Williams" }
            };
        }


    }
}