using System;
using Pra.Tamagotchi.Core.Interfaces;
using Pra.Tamagotchi.Core.Enums;

namespace Pra.Tamagotchi.Core.Entities
{
    public class Chick : Tamagotchi, IFeedable
    {
        private int amountOfFood = 0;
        public Chick(TamagotchiStatus status) : base()
        {
            Status = status;
        }
        public void Feed()
        {
            if (Status == TamagotchiStatus.Died)
            {
                throw new InvalidOperationException(CreateDeadTamagotchiMessage());
            }
            amountOfFood++;
            
        }

        public override void Grow()
        {
            if (amountOfFood <= 0 && Status == TamagotchiStatus.Healthy)
            {
                throw new InvalidOperationException($"Je tamagotchi heeft eten nodig om te kunnen groeien");
            }
            
            if (Status == TamagotchiStatus.Died)
            {
                throw new InvalidOperationException(CreateDeadTamagotchiMessage());
            }
            else if (Status == TamagotchiStatus.Sick)
            {
                Health -= 50;
                
            }
            else if (Status == TamagotchiStatus.Healthy)
            {
                Size++;
                amountOfFood--;
            }

        }

        private string CreateDeadTamagotchiMessage()
        {
            string message = $"Je tamagotchi is overleden, je kan geen acties meer uitvoeren";
            return message;
        }

        public override string ToString()
        {
            return $"Kuiken [{Status}] -- Health = {Health} -- Size = {Size} (food: {amountOfFood})";
        }
    }
}
