﻿using Microsoft.EntityFrameworkCore;
using WebTest.Context;
using WebTest.Contracts;
using WebTest.Models.Players;
using WebTest.ViewModels.Players;

namespace WebTest.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FifaAppContext _appContext;
        public PlayerService(FifaAppContext fifaAppContext)
        {
            _appContext = fifaAppContext;
        }

        public async Task AddPlayerAsync(PlayerCreateDTO playerCreateDTO)
        {
            Player player = new Player
            {
                Name = playerCreateDTO.Name,
                Surname = playerCreateDTO.Surname,
                Nationality = playerCreateDTO.Nationality,
                Overall = playerCreateDTO.Overall
            };

            _appContext.Add(player);
            await _appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerVM>> GetPlayersAsync()
        {
            IEnumerable<PlayerVM> players = await _appContext.Players.Select(p => new PlayerVM
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Nationality = p.Nationality,
                Overall = p.Overall
            }).ToListAsync();

            return players;
        }

        public async Task EditPlayerAsync(int playerId, EditPlayerDTO editPlayerDTO)
        {
            Player playerToBeEdited = await _appContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);

            if (playerToBeEdited != null)
            {
                playerToBeEdited.Name = editPlayerDTO.Name;
                playerToBeEdited.Surname = editPlayerDTO.Surname;
                playerToBeEdited.Nationality = editPlayerDTO.Nationality;
                playerToBeEdited.Overall = editPlayerDTO.Overall;
            }else
            {
                throw new ArgumentException("No player exists with this id!");
            }

            await _appContext.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int playerId)
        {
            Player playerToBeDeleted = await _appContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);

            if (playerToBeDeleted != null)
            {
                _appContext.Players.Remove(playerToBeDeleted);
            }

            await _appContext.SaveChangesAsync();
        }
    }
}
