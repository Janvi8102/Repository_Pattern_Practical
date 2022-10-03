using DomainModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace UserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllState()
        {
            try
            {
                var states = await _stateRepository.GetAllState();
                return Ok(states);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StateModel>> GetStateById(int id)
        {
            try
            {
                var result = await _stateRepository.GetStateById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<State>> AddState(State state)
        {
            try
            {
                await _stateRepository.AddState(state);
                return CreatedAtAction("GetStateById", new { id = state.StateId }, state);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Country doesn't Exist");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status409Conflict,"State already Exist");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<State>> UpdateState(int id, State State)
        {
            try
            {
                if (id != State.StateId)
                {
                    return BadRequest("Id is Mismatched");
                }
                var stateUpdate = await _stateRepository.GetStateById(id);

                if (stateUpdate == null)
                {
                    return NotFound($"State Id = {id} not found");
                }

                return await _stateRepository.UpdateState(State);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); 
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<State>> DeleteState(int id)
        {
            try
            {
                var stateDelete = await _stateRepository.GetStateById(id);

                if (stateDelete == null)
                {
                    return NotFound($"State Id = {id} not found");
                }

                return await _stateRepository.DeleteState(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data from Database"); ;
            }
        }
    }
}
