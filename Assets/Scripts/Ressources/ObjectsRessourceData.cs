using System.Collections.Generic;

namespace GameJam
{
    public class ObjectsRessourceData : IObjectsRessourceData
    {
        public List<Weapon> Weapons
        {
            get
            {
                return this.Weapons;
            }
            set
            {
                Weapons = value;
            }
        }

        public List<Consommable> Consommables
        {
            get
            {
                return this.Consommables;
            }
            set
            {
                Consommables = value;
            }
        }

        public ObjectsRessourceData()
        {
            Weapons = new List<Weapon>
        {
            new Weapon
            {
                damage=3,
                name="arc",
                objectType=GameJam.ObjectType.Weaponrange,
                path="",
                range=5
            },
            new Weapon
            {
                damage=2,
                name="épée",
                objectType=GameJam.ObjectType.Weaponcac,
                path="",
                range=1
            }
        };
            Consommables = new List<Consommable>
        {
            new Consommable
            {
                amount=2,
                name="potion",
                objectType=GameJam.ObjectType.Consommable,
                path="",
                feature=GameJam.Feature.Health
            }
        };
        }

    }
}