using DomainModels.InPutModel;
using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repositories.IRepositories
{
   public interface IStateRepository
    {
        Task<IEnumerable<StateModel>> GetAllState();
        Task<StateModel> GetStateById(int StateId);
        Task<State> AddState(State State);
        Task<State> UpdateState(State State);
        Task<State> DeleteState(int StateId);
    }
}
