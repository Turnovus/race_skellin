﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;
using RimWorld;

namespace Skellin
{
    [DefOf]
    public static class Skellin_ThingDefOf
    {
        public static ThingDef SkellinRace;

        static Skellin_ThingDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}
