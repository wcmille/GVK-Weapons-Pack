using System;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DeformDef.DeformTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.DecalDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace Scripts
{
    partial class Parts
    {
        const float maxTrajectory = 5000.0f;
        const float scale = 0.75f;
        const float integrityFactor = 1.0f / 600.0f;

        internal DecalDef MakeBulletDecal()
        {
            return new DecalDef
            {
                MaxAge = 3600,
                Map = new[]
                        {
                        new TextureMapDef
                        {
                            HitMaterial = "Metal",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Glass",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Soil",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Wood",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "GlassOpaque",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Stone",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Rock",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Ice",
                            DecalMaterial = "GunBullet",
                        },
                        new TextureMapDef
                        {
                            HitMaterial = "Soil",
                            DecalMaterial = "GunBullet",
                        } }
            };
        }

        struct SabotKinetic
        {
            readonly float desiredSpeed;
            readonly float roundMass;
            readonly float roundDiameter;
            readonly Parts p;
            readonly float ke;

            public SabotKinetic(Parts p, float desiredSpeed, float roundMass, float roundDiameter)
            {
                this.p = p;
                this.desiredSpeed = desiredSpeed;
                this.roundMass = roundMass;
                this.roundDiameter = roundDiameter;
                ke = desiredSpeed * desiredSpeed * roundMass * 0.5f;
            }

            public TrajectoryDef MakeBasicTrajectory()
            {
                float scaledSpeed = Math.Min(scale * desiredSpeed, maxTrajectory);
                return new TrajectoryDef
                {
                    Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                    MaxLifeTime = 4 * 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                    DesiredSpeed = scaledSpeed, // voxel phasing if you go above 5100
                    MaxTrajectory = Math.Min(desiredSpeed * 3.5f, maxTrajectory), // Max Distance the projectile or beam can Travel.
                    SpeedVariance = p.Random(start: 0, end: 20), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                    RangeVariance = p.Random(start: 0, end: 50) // subtracts value from MaxTrajectory
                };
            }
            public DamageScaleDef KineticDamage()
            {

                return new DamageScaleDef
                {
                    //This is additional damage done and does not directly affect the speed that the ammo's health pool depletes.
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with current integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    Characters = 0.25f,
                    // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = 0.75f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = Math.Min(1f, (float)Math.Log10(ke) / 5.0f),
                        Heavy = Math.Min(1f, (float)Math.Log10(ke) / 6.0f),
                        NonArmor = -1f,
                    },
                    DamageType = new DamageTypes
                    {
                        Base = Kinetic,
                        AreaEffect = Kinetic,
                        Detonation = Kinetic,
                        Shield = Kinetic,
                    },
                    Deform = new DeformDef
                    {
                        DeformType = HitBlock,
                        DeformDelay = 30,
                    }
                };
            }

            public Structure.WeaponDefinition.AmmoDef AssembleRound(string ammoMag, string ammoRound, GraphicDef ag, AmmoAudioDef aa)
            {
                return new Structure.WeaponDefinition.AmmoDef
                {
                    AmmoMagazine = ammoMag,
                    AmmoRound = ammoRound,
                    BaseDamage = ke * integrityFactor, // breaks 1 HA or 1 LA cubes in 1 round
                    Mass = roundMass, // in kilograms
                    Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                    BackKickForce = desiredSpeed * roundMass,
                    HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
                    NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                    NoGridOrArmorScaling = false, // If you enable this you can remove the damagescale section entirely.
                    DamageScales = KineticDamage(),
                    Trajectory = MakeBasicTrajectory(),
                    AmmoGraphics = ag,
                    AmmoAudio =aa
                };
            }
        }

        internal TrajectoryDef MakeAirburstTrajectory(int desiredSpeed)
        {
            float scaledSpeed = Math.Min(scale * desiredSpeed, maxTrajectory);
            return new TrajectoryDef
            {
                Guidance = Smart, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                TargetLossDegree = 30f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 4 * 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                DesiredSpeed = scaledSpeed, // voxel phasing if you go above 5100
                MaxTrajectory = Math.Min(desiredSpeed*3.5f, maxTrajectory), // Max Distance the projectile or beam can Travel.
                SpeedVariance = Random(start: -25f, end: 25), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0f, end: 0f), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    MaxTargets = 1, // Number of targets allowed before ending, 0 = unlimited
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    ScanRange = 0, // 0 disables projectile screening, the max range that this projectile will be seen at by defending grids (adds this projectile to defenders lookup database). 
                    NoSteering = true, // this disables target follow and instead travel straight ahead (but will respect offsets).
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
            };
        }
    }
}
