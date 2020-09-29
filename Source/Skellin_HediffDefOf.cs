using RimWorld;
using Verse;

namespace Skellin
{
  [DefOf]
  public static class Skellin_HediffDefOf
  {
    public static HediffDef ToxicHangover;

    static Skellin_HediffDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof (Skellin_HediffDefOf));
  }
}
