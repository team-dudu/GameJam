using System;
using System.Collections.Generic;
using System.Globalization;

namespace GameJam
{
    [Serializable]
    public partial class ObjectRessources
    {
        public List<Weapon> weapons { get; set; }

        public List<Consommable> consommables { get; set; }
    }
}