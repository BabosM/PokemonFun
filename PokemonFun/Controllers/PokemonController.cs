using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonFun.Models;
using PokemonFun.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static PokemonFun.Models.SimplePokemonData;

namespace PokemonFun.Controllers
{
    [Route("api/Pokemon")]
    [ApiController]
    public class PokemonController : Controller
    {
        private IHttpClientFactory _fac;
        private IPokeapiService _pokeapiService;

        public PokemonController(IHttpClientFactory fac, IPokeapiService pokeapiService)
        {
            _fac = fac;
            _pokeapiService = pokeapiService;
        }        

        [HttpGet()]
        public async Task<IActionResult> Index(string searchPokemon)
        {
            ViewBag.searchPokemon = searchPokemon;
            IEnumerable<PokemonModel> pokemonModelList = await _pokeapiService.GetPokemonList();
            List<PokemonModel> pokemons = new List<PokemonModel>();

            if (!String.IsNullOrEmpty(searchPokemon))
            {
                pokemonModelList = pokemonModelList.Where(p => p.Name.ToUpper().Contains(searchPokemon.ToUpper())).ToList();
                return View(pokemonModelList);
            }
            return View(pokemonModelList);
        }
        [Route("random")]
        public async Task<ActionResult<PokemonModel>> GetRandomPokemon()
        {
            PokemonModel randomPokeRoot = await _pokeapiService.GetRandomPokemon();
            return View(randomPokeRoot); 
        }
    }
}
