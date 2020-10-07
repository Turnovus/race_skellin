using Verse;
using Verse.AI;
using RimWorld;

namespace Skellin
{
    public class MentalState_SkellinToxic : MentalState
    {
        public override bool ForceHostileTo(Thing t)
        {   
            //only attack non-broken pawns
            if (t is Pawn)
            {
                var p = t as Pawn;
                return !p.mindState.mentalStateHandler.InMentalState;
            }
            return true;
        }

        //public override bool ForceHostileTo(Faction f) => true;

        public override RandomSocialMode SocialModeMax() => RandomSocialMode.Off;
    }
}
