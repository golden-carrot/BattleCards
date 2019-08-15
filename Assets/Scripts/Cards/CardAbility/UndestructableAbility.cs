using System.Collections;
using BattleCards.Battle;

namespace BattleCards.Cards.CardAbility {
    public class UndestructableAbility : CardAbility
    {
	    public override IEnumerator Action(CardBattleFunctions.BattleFunctionData my)
	    {
			yield break;
	    }
    }
}

