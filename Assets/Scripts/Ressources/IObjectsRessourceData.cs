using System.Collections.Generic;

namespace GameJam
{
    public interface IObjectsRessourceData
    {
        List<Weapon> Weapons { get;}

        List<Consommable> Consommables { get; set; }
    }
}