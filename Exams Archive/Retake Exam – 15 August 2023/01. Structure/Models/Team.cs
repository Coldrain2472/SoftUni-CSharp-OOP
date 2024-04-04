using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private readonly List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            PointsEarned = 0;
            players = new List<IPlayer>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }

        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (players.Count == 0)
                    return 0;
                return Math.Round(players.Average(p => p.Rating), 2);
            }
        }

        public IReadOnlyCollection<IPlayer> Players => players;

        public void Draw()
        {
            PointsEarned += 1;
            foreach (var player in players)
            {
                if (player is Goalkeeper)
                {
                    player.IncreaseRating();
                }
            }
        }

        public void Lose()
        {
            foreach (var player in players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player) => players.Add(player);

        public void Win()
        {
            PointsEarned += 3;
            foreach (var player in players)
            {
                player.IncreaseRating();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");

            if (players.Count == 0)
            {
                sb.AppendLine("--Players: none");
            }
            else
            {
                sb.AppendLine($"--Players: {string.Join(", ", players.Select(p => p.Name))}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
