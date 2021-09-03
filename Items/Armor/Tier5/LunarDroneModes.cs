using System;
namespace MechArmor.Items.Armor.Tier5
{
    public enum LunarDroneModes
    {
        Idle,                // Nothing
                    // Modes for proto & full lunar
        FollowSwing,         // Melee 1
        JetpackWings,        // Melee 2
        Multishot,           // ranged 1
        ProjectileBarrier,   // ranged 2
        ManaAmplifier,       // magic 1
        ManaLifeSteal,       // magic 2
        SummonBoost,         // summon 1
        Confusion,           // summon 2
                    // Modes only for full lunar
        ProjectileShield,    // Melee 3
        ManaShield,          // Magic 3
        JetpackPlatform      // Summon 3

    }
}
