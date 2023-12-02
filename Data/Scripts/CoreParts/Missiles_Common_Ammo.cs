using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.DecalDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using System;

namespace Scripts
{
    partial class Parts
    {
        internal DecalDef MakeMissileDecal()
        {
            return new DecalDef
            {
                MaxAge = 7200,
                Map = new[] {
                    new TextureMapDef
                    {
                        HitMaterial = "Metal",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Glass",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Soil",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Wood",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "GlassOpaque",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Stone",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Rock",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Ice",
                        DecalMaterial = "Missile",
                    },
                    new TextureMapDef
                    {
                        HitMaterial = "Soil",
                        DecalMaterial = "Missile",
                    }
                }
            };
        }

        internal struct HighExplosive
        {
            readonly float chemicalEnergy;
            readonly float baseDamage;
            const double minimalDamage = 25.0;
            readonly float damageRadius;

            public HighExplosive(float chemicalEnergyMass)
            {
                const float energyIn1KGTnt = 4.6E6f;
                chemicalEnergy = chemicalEnergyMass * energyIn1KGTnt * 0.5f; //20% efficiency                   
                baseDamage = chemicalEnergy / joulesPerDamage;
                damageRadius = (float)Math.Sqrt(baseDamage / minimalDamage);
            }

            public DamageScaleDef DamageScales()
            {
                return new DamageScaleDef
                {
                    DamageVoxels = false, // Whether to damage voxels.
                    HealthHitModifier = 1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                    Characters = -1f, // Character damage multiplier; defaults to 1 if zero or less.
                                      // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                    Grids = new GridSizeDef
                    {
                        Large = -1f, // Multiplier for damage against large grids.
                        Small = 0.04f, // 1/25
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                        Light = 0.6f, // Multiplier for damage against light armor.
                        Heavy = 0.3f, // Multiplier for damage against heavy armor.
                        NonArmor = -1f, // Multiplier for damage against every else.
                    },
                    DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                    {
                        Base = DamageTypes.Damage.Kinetic, // Base Damage uses this
                        AreaEffect = DamageTypes.Damage.Energy,
                        Detonation = DamageTypes.Damage.Energy,
                        Shield = DamageTypes.Damage.Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                    },
                };
            }
            public AreaOfDamageDef DamageArea()
            {
                return new AreaOfDamageDef
                {
                    EndOfLife = new EndOfLifeDef
                    {
                        Enable = true,
                        Radius = damageRadius, // Radius of AOE effect, in meters.
                        Damage = baseDamage,
                        Depth = damageRadius, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                        MaxAbsorb = baseDamage, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                        Falloff = Falloff.Curve, //.NoFalloff applies the same damage to all blocks in radius
                                                  //.Linear drops evenly by distance from center out to max radius
                                                  //.Curve drops off damage sharply as it approaches the max radius
                                                  //.InvCurve drops off sharply from the middle and tapers to max radius
                                                  //.Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                                  //.Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                                  //.Exponential drops off exponentially.  Does not scale to max radius
                        ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                        MinArmingTime = 30, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                        NoVisuals = false,
                        NoSound = false,
                        ParticleScale = 1,
                        //CustomParticle = "Explosion_AmmunitionMedium", // Particle SubtypeID, from your Particle SBC
                        //CustomSound = "HWR_LargeExplosion", // SubtypeID from your Audio SBC, not a filename
                        Shape = AoeShape.Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                    }
                };
            }
        }
    }
}