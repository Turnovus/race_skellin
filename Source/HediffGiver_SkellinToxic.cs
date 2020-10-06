using RimWorld;
using Verse;

namespace Skellin
{
  public class HediffGiver_SkellinToxic : HediffGiver
  {
    public override bool OnHediffAdded(Pawn pawn, Hediff hediff)
    {
      if (hediff.def != HediffDefOf.ToxicBuildup)
        return false;
      HealthUtility.AdjustSeverity(pawn, Skellin_HediffDefOf.ToxicFlu, hediff.Severity);
      pawn.health.RemoveHediff(hediff);
      return true;
    }
  }
}
