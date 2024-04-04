using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private readonly PlayerRepository players;
        private readonly TeamRepository teams;

        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }

        public string LeagueStandings()
        {
            StringBuilder standings = new StringBuilder();
            standings.AppendLine("***League Standings***");

            foreach (var team in teams.Models
                         .OrderByDescending(t => t.PointsEarned)
                         .ThenByDescending(t => t.OverallRating)
                         .ThenBy(t => t.Name))
            {
                standings.AppendLine(team.ToString());
            }

            return standings.ToString().TrimEnd();
        }


        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, players.GetType().Name);
            }

            if (!teams.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, teams.GetType().Name);
            }

            IPlayer player = players.GetModel(playerName);

            if (player.Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            }

            player.JoinTeam(teamName);

            ITeam team = teams.Models.First(t => t.Name == teamName);
            team.SignContract(player);
            return string.Format(OutputMessages.SignContract, playerName, teamName);
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam teamOne = teams.GetModel(firstTeamName);
            ITeam teamTwo = teams.GetModel(secondTeamName);
            List<ITeam> teamsList = new List<ITeam>()
            {
                teamOne,
                teamTwo
            };

            if (teamOne.OverallRating.Equals(teamTwo.OverallRating))
            {
                teamsList.ForEach(t => t.Draw());
                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }


            teamsList = teamsList.OrderBy(t => t.OverallRating).ToList();
            teamsList[0].Lose();
            teamsList[1].Win();

            return string.Format(OutputMessages.GameHasWinner, teamsList[1].Name, teamsList[0].Name);
        }

        public string NewPlayer(string typeName, string name)
        {
            var children = Assembly.GetAssembly(typeof(Player))
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsClass && typeof(Player).IsAssignableFrom(t))
                .Select(t => t.Name);

            if (!children.Contains(typeName))
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }

            if (players.ExistsModel(name))
            {
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, players.GetType().Name,
                    players.Models.First(p => p.Name == name).GetType().Name);
            }

            IPlayer player;

            if (typeName == nameof(CenterBack))
            {
                 player = new CenterBack(name);
                players.AddModel(player);
            }
            else if (typeName == nameof(ForwardWing))
            {
                player = new ForwardWing(name);
                players.AddModel(player);
            }
            else if (typeName == nameof(Goalkeeper))
            {
                player = new Goalkeeper(name);
                players.AddModel(player);
            }

            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewTeam(string name)
        {
            ITeam team;
            if (teams.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, teams.GetType().Name);
            }
            team = new Team(name);
            teams.AddModel(team);
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, teams.GetType().Name);
        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder statistics = new StringBuilder();
            statistics.AppendLine($"***{teamName}***");
            ITeam team = teams.GetModel(teamName);

            foreach (var player in team.Players
                         .OrderByDescending(p => p.Rating)
                         .ThenBy(p => p.Name))
            {
                statistics.AppendLine(player.ToString());
            }

            return statistics.ToString().TrimEnd();
        }
    }
} // new contract & new player
