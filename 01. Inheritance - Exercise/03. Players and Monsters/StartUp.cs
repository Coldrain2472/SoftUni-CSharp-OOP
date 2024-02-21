using System;

namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoulMaster soulMaster = new SoulMaster("Mary", 50);
            MuseElf museElf = new MuseElf("Ajax", 30);
            BladeKnight bladeKnight = new BladeKnight("Fenrir", 100);


            Console.WriteLine($"Username: {soulMaster.Username}, Level: {soulMaster.Level}");
            Console.WriteLine($"Username: {museElf.Username}, Level: {museElf.Level}");
            Console.WriteLine($"Username: {bladeKnight.Username}, Level: {bladeKnight.Level}");
        }
    }
}