using PokemonFun.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PokemonFun.Models.SimplePokemonData;

namespace PokemonFun.Service
{
    public interface IPokeapiService
    {
        Task<PokemonModel> GetPokemonByName(string name);
        Task <List<PokemonModel>> GetPokemonList();
        Task<PokemonModel> GetRandomPokemon();
        Task<Sprites> GetPokemonFrontDefaultPic();
        // toDo
       // Task<List<PokemonModel>> GetPokemonListAsync();
    }
}