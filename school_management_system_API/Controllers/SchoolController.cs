﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school_management_system_API.Models;
using school_management_system_API.Services;
using System;
using System.Linq;

namespace school_management_system_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {

        private readonly SchoolService _schoolService;

        public SchoolController(SchoolService schoolService)
        {
            this._schoolService = schoolService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_schoolService.GetAll());
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public ActionResult GetById(int id)
        {
            var result = _schoolService.GetById(id);

            if (result.Failure) return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public ActionResult Post([FromBody]School school)
        {
            if(!ModelState.IsValid) return BadRequest();

            var result = _schoolService.Create(school);

            if (result.Failure) return BadRequest(result.Error);

            return Created(new Uri($"{Request.Path}/{result.Value.Id}", UriKind.Relative), result.Value);

        }

        [HttpPut]
        public ActionResult Put([FromBody] School school, int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            school.Id = id;

            var result = _schoolService.Update(school);

            if (result.Failure) return BadRequest(result.Error);


            return Ok();

        }

        [HttpDelete]
        public ActionResult Delete( int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = _schoolService.RemoveById(id);

            if (result.Failure) return BadRequest(result.Error);

            return NoContent();

        }


    }
}
