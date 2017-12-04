﻿using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/hearings")]
    public class HearingsController : Controller
    {
        IHearingsManipulation _hearingsManipulation { get; set; }

        public HearingsController(IHearingsManipulation hearingsManipulation)
        {
            _hearingsManipulation = hearingsManipulation;
        }

        [HttpPost]
        public IActionResult Post([FromBody] HearingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _hearingsManipulation.Create(model);
                return Ok("New hearing created");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HearingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _hearingsManipulation.Update(id, model);
                return Ok("Hearing updated");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }

            return BadRequest();
        }
    }
}
