using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using Verse.AI.Group;
using RimWorld;

namespace Skellin
{
    class IncidentWorker_SkellinShambler : IncidentWorker //note: a lot of this is borrowed from IncidentWorker_WildManWandersIn.cs and IncidentWorker_ThrumboPasses.cs
    {
        public virtual Pawn GeneratePawn()
        {
            Gender? fixedGender = new Gender?();
            if (this.def.pawnFixedGender != Gender.None)
                fixedGender = new Gender?(this.def.pawnFixedGender);
            this.def.pawnKind = Skellin_PawnKindDefOf.SkellinShambler; //Use SkellinShambler PawnKind
            return PawnGenerator.GeneratePawn(new PawnGenerationRequest(this.def.pawnKind, Faction.OfAncients, forceGenerateNewPawn: true, mustBeCapableOfViolence: this.def.pawnMustBeCapableOfViolence, colonistRelationChanceFactor: 20f, fixedGender: fixedGender));
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms) || !this.TryFindFormerFaction(out Faction _))
                return false;
            Map target = (Map)parms.target;
            return !target.GameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) && target.mapTemperature.SeasonAcceptableFor(Skellin_ThingDefOf.SkellinRace) && this.TryFindEntryCell(target, out IntVec3 _);
        }

        protected override bool TryExecuteWorker(IncidentParms parms) //don't bother asking me what any of this means
        {
            Map target = (Map)parms.target;
            IntVec3 cell;
            if (!this.TryFindEntryCell(target, out cell))
                return false;
            PawnKindDef shambler = Skellin_PawnKindDefOf.SkellinShambler;
            int num1 = Mathf.Clamp(GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow((IIncidentTarget)target) / shambler.combatPower), 2, Rand.RangeInclusive(2, 5));
            int num2 = Rand.RangeInclusive(90000, 150000);
            IntVec3 result = IntVec3.Invalid;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, target, 10f, out result))
                result = IntVec3.Invalid;
            Lord lord = LordMaker.MakeNewLord(Faction.OfAncients, (LordJob)new LordJob_AssaultColony(Faction.OfAncients, canTimeoutOrFlee: false, canSteal: false), target);
            Pawn pawn = (Pawn) null;
            for (int index = 0; index < num1; ++index)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(cell, target, 10);
                pawn = GeneratePawn();
                Rot4 rot = Rot4.FromAngleFlat((target.Center - result).AngleFlat);
                GenSpawn.Spawn((Thing)pawn, loc, target, rot);
                pawn.health.AddHediff(Skellin_HediffDefOf.ToxicFever);
                pawn.mindState.mentalStateHandler.TryStartMentalState(Skellin_MentalStateDefOf.SkellinToxic);
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num2;
                lord.AddPawn(pawn);
                if (result.IsValid)
                    pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(result, target, 10);
            }
            this.SendStandardLetter(parms, (LookTargets)pawn, (NamedArgument[])Array.Empty<NamedArgument>());
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }

        private bool TryFindEntryCell(Map map, out IntVec3 cell) => CellFinder.TryFindRandomEdgeCellWith((Predicate<IntVec3>)(c => map.reachability.CanReachColony(c)), map, CellFinder.EdgeRoadChance_Ignore, out cell);

        private bool TryFindFormerFaction(out Faction formerFaction) => Find.FactionManager.TryGetRandomNonColonyHumanlikeFaction_NewTemp(out formerFaction, false, true);
    }
}
