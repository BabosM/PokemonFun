using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokemonFun.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using static PokemonFun.Models.SimplePokemonData;

namespace PokemonFun.Service
{
    public class PokeapiService : IPokeapiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _clientFactory;

        public PokeapiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;    
            _httpClient = _clientFactory.CreateClient("PokemonApi");
            _httpClient.DefaultRequestHeaders.Clear();
            
        }

        public async Task<List<PokemonRoot>> GetPokemonRootList() {
            List<PokemonRoot> PokemonRootList = new List<PokemonRoot>();
            var response = await _httpClient.GetAsync("pokemon?limit=-1");
            var content = await response.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<SimplePokemonData>(content);
            var resultPokemon = pokemon.results;
            PokemonRootList = resultPokemon.ToList();
            return PokemonRootList;
        }
        public async Task<List<PokemonModel>> GetPokemonList()
        {
            List<PokemonRoot> PokemonRootList = await GetPokemonRootList();
            List<PokemonModel> PokemonModelList = new List<PokemonModel>();
  
            for (int i = 0; i < 20; i++)
            {
                var response = await _httpClient.GetAsync(PokemonRootList[i].url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var pokemon = JsonConvert.DeserializeObject<PokemonModel>(content);
                PokemonModelList.Add(pokemon);
            }
            return PokemonModelList;
        }

        public async  Task<PokemonModel> GetPokemonByName(string name)
        {  
            var response = await _httpClient.GetAsync("pokemon/" + name);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<PokemonModel>(content);
            return pokemon;
        }

        public async Task<PokemonModel> GetRandomPokemon()
        {
            var random = new Random();
            // pobrac counta z pokeomonow
            // z counta zrobic random
            // dokleic random do sciezki 
            // 
            var response = await _httpClient.GetAsync("pokemon/" + random);
            List<PokemonModel> PokemonRootList = new List<PokemonModel>();
            PokemonRootList = await GetPokemonList();
            int index = random.Next(PokemonRootList.Count);
            var RandomPokemon = PokemonRootList[index];
            return RandomPokemon;
        }

        public Task<Sprites> GetPokemonFrontDefaultPic(string frontDefault)
        {
            throw new NotImplementedException();
        }

        public Task<Sprites> GetPokemonFrontDefaultPic()
        {
            throw new NotImplementedException();
        }
    }
}
