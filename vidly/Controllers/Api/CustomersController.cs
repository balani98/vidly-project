﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vidly.Dto;
using vidly.Models;
using System.Data.Entity;

namespace vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private MovieDbContext _context;
        public CustomersController()
        {
            _context = new MovieDbContext();

        }
        //GET /api/customers
        public IEnumerable<CustomerDto> getCustomers()
        {
           
            var customerDtos= _context.Customers.Include(m => m.membershipType).
                                      ToList().
                                      Select(Mapper.Map<Customer,CustomerDto>);
            return customerDtos;
        }
        //GET /api/customers/1
        public IHttpActionResult getcustomer(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));

        }
        //POST api/customers
        [HttpPost]
        public IHttpActionResult createCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
          
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);

        }
        //PUT api/customers/1
        [HttpPut]
        public void updateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerinDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
          
            customerDto.Id = id;
            Mapper.Map(customerDto,customerinDb);
          
            _context.SaveChanges();
        }
        //DELETE api/customers/1
        [HttpDelete]
        public void deleteCustomer(int id)
        {
            var customerinDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerinDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerinDb);
            _context.SaveChanges();
        }
        
    }
}
