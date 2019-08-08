using BattleCards.Battle;

namespace BattleCards.Cards.CardAbility {
    public class AttackNExterminationAbility : AttackAbility
    {
        public override void Action(CardBattleFunctions.BattleFunctionData my) {
            base.Action(my);

            if (_successAttack) {
                my.Health = 0;
                
                var undestructableAbility = my.Card.GetComponent<UndestructableAbility>();
                if (undestructableAbility != null) {
                    DestroyImmediate(undestructableAbility);
                }
            }
        }
    }
}

