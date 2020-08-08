
namespace RPG.Stats
{
    public enum StatEnum
    {
        // health of the character
        Health, // both enemies and player

        // how much exp points are they worth upon dying
        ExperienceReward, // only enemies should have this component

        // minimum exp points needed to go from level x to x + 1
        ExperienceNeededToLevelUp, // only player

        // how much damage will the character cause with each level increase
        Damage // both enemies and player
    }
}