using System;
using System.Collections.Generic;
using System.Globalization;

namespace GameJam
{
    [Serializable]
    public partial class ObjectResources
    {
        public Weapon[] Weapons { get; set; }

        public Consommable[] Consommables { get; set; }
    }
}