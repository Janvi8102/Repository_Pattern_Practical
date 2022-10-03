using AutoMapper;
using DomainModels;
using DomainModels.InPutModel;
using DomainModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public StateRepository(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<State> AddState(State state)
        {
            if(_Context.States.Any(r=> r.StateName == state.StateName))
            {
                throw new Exception();
            }
            else if(_Context.Countries.Any(r => r.CountryId == state.CountryId))
            {
                var result = await _Context.States.AddAsync(state);
                await _Context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new KeyNotFoundException();
            }
            
        }

        public async Task<State> DeleteState(int stateId)
        {
            var result = await _Context.States.Where(a => a.StateId == stateId).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.States.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<StateModel>> GetAllState()
        {
            var states = await _Context.States.Include(x => x.Country).ToListAsync();
            return _mapper.Map<List<StateModel>>(states);

        }

        public async Task<StateModel> GetStateById(int stateId)
        {
            var records = await _Context.States.Include(x => x.Country).FirstOrDefaultAsync(a => a.StateId == stateId);
            return _mapper.Map<StateModel>(records);
        }

        public async Task<State> UpdateState(State state)
        {
            var result = await _Context.States.FirstOrDefaultAsync(a => a.StateId == state.StateId);
            if (result != null)
            {
                result.StateName = state.StateName;
                result.CountryId = state.CountryId;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }


    }
}
