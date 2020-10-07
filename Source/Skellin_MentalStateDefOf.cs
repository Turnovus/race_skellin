using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;
using RimWorld;

namespace Skellin
{
    [DefOf]
    public static class Skellin_MentalStateDefOf
    {
        public static MentalStateDef SkellinToxic;

        static Skellin_MentalStateDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(MentalStateDefOf));
    }
}
