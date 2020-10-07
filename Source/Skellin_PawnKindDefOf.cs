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
    public static class Skellin_PawnKindDefOf
    {
        public static PawnKindDef SkellinShambler;

        static Skellin_PawnKindDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof (PawnKindDefOf));
    }
}
