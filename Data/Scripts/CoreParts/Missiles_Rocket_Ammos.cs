using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Missiles_Rocket
        {
            get
            {
                var he = new HighExplosive(21.0f);
                return new AmmoDef
                {
                    AmmoMagazine = "Missile200mm",
                    AmmoRound = "Rocket",
                    BaseDamage = 1f,
                    Mass = 200f, // in kilograms
                    Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                    BackKickForce = 100f,
                    HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
                    NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                    Sync = new SynchronizeDef
                    {
                        Full = false, // Be careful, do not use on high fire rate weapons or ammos with many simultaneous fragments. This will send position updates twice per second per projectile/fragment and sync target (grid/block) changes.
                        PointDefense = true, // Server will inform clients of what projectiles have died by PD defense and will trigger destruction.
                        OnHitDeath = true, // Server will inform clients when projectiles die due to them hitting something and will trigger destruction.
                    },
                    DamageScales = he.DamageScales(),
                    AreaOfDamage = he.DamageArea(),
                    Trajectory = new TrajectoryDef
                    {
                        Guidance = None,
                        TargetLossDegree = 1f,
                        TargetLossTime = 1, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        AccelPerSec = 800f,
                        DesiredSpeed = 1500,
                        MaxTrajectory = 3000f,
                        SpeedVariance = Random(start: 0, end: 100), // subtracts value from DesiredSpeed
                        RangeVariance = Random(start: 0, end: 50), // subtracts value from MaxTrajectory
                    },
                    AmmoGraphics = new GraphicDef
                    {
                        ModelName = "\\Models\\Missiles\\MXA_Archer_Missile.mwm",
                        VisualProbability = 1f,
                        Decals = MakeMissileDecal(),
                        Particles = new AmmoParticleDef
                        {
                            Ammo = new ParticleDef
                            {
                                Name = "MD_BulletGlowMedYellow", //Archer_MissileSmokeTrail
                                Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                                Offset = Vector(x: 0, y: 0, z: 0f),
                                Extras = new ParticleOptionDef
                                {
                                    Loop = true,
                                    Restart = false,
                                    MaxDistance = 2000,
                                    MaxDuration = 0,
                                    Scale = 1f,
                                },
                            },
                            Hit = new ParticleDef
                            {
                                Name = "MD_HydraRocketExplosion", //MD_HydraRocketExplosion MD_InstallationExplosion
                                ApplyToShield = false,
                                Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                                Offset = Vector(x: 0, y: 0, z: 0),
                                Extras = new ParticleOptionDef
                                {
                                    Loop = false,
                                    Restart = false,
                                    MaxDistance = 5000,
                                    MaxDuration = 0,
                                    Scale = 1f,
                                    HitPlayChance = 1f,
                                },
                            },
                        },
                        Lines = new LineDef
                        {
                            ColorVariance = Random(start: 0f, end: 2f), // multiply the color by random values within range.
                            WidthVariance = Random(start: -0.2f, end: 0.2f), // adds random value to default width (negatives shrinks width)
                            DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                            Tracer = new TracerBaseDef
                            {
                                Enable = true,
                                Length = 10f,
                                Width = 0.3f,
                                Color = Color(red: 30f, green: 25f, blue: 1.5f, alpha: 1f),
                                Textures = new[] { "MD_MissileThrustFlame", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            },
                            Trail = new TrailDef
                            {
                                Enable = true,
                                Textures = new[] { "WeaponLaser", },
                                DecayTime = 30,
                                Color = Color(red: 1, green: 1, blue: 1, alpha: 1f),
                                Back = false,
                                CustomWidth = 1f,
                                UseWidthVariance = true,
                                UseColorFade = true,
                            },
                        },
                    },
                    AmmoAudio = new AmmoAudioDef
                    {
                        TravelSound = "MXA_Archer_Travel",
                        HitSound = "HWR_LargeExplosion",
                        ShieldHitSound = "",
                        PlayerHitSound = "",
                        VoxelHitSound = "",
                        FloatingHitSound = "",
                        HitPlayChance = 1f,
                        HitPlayShield = true,
                    }, // Don't edit below this line
                    Ejection = Common_Ammos_Ejection_None,
                };
            }
        }
    }
}
