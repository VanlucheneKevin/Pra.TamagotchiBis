using Pra.Tamagotchi.Core.Entities;
using Pra.Tamagotchi.Core.Interfaces;
using System.Collections.Generic;

namespace Pra.Tamagotchi.Core.Services
{
    public class TamagotchiCollection
    {
        public List<ITamagotchi> Tamagotchis { get; }

        public TamagotchiCollection()
        {
            Tamagotchis = new List<ITamagotchi>();
            AddEggs(3);
        }

        public void AddEggs(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Egg newEgg = new Egg();
                Tamagotchis.Add(newEgg);

            } 
            
        }

        public void Hatch(Egg egg)
        {
            
            for (int i = Tamagotchis.IndexOf(egg); i < Tamagotchis.Count; i++)
            {
                Tamagotchis.RemoveAt(i);
                Tamagotchis[i] = egg.Hatch();
            }
        }
    }
}
