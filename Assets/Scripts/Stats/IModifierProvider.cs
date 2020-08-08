
using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
         IEnumerable<int> GetAdditiveModifier(StatEnum stat);
         IEnumerable<float> GetPercentageModifiers(StatEnum stat);
    }
}