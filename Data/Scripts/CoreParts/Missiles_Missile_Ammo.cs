﻿using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Missiles_Missile
        {
            get
            {
                var he = new HighExplosive(10.0f);
                return new AmmoDef
                {
                    Shape = new ShapeDef()
                    {
                        Shape = ShapeDef.Shapes.SphereShape,
                        Diameter = 0.5,
                    },
                    AmmoMagazine = "Missiles_Missile",
                    AmmoRound = "Missile",
                    BaseDamage = 1f,
                    Mass = 200f, // in kilograms
                    Health = 1f, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                    BackKickForce = 50f,
                    HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
                    NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                    NoGridOrArmorScaling = false, // If you enable this you can remove the damagescale section entirely.
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
                        Guidance = Smart,
                        TargetLossDegree = 80,
                        TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        MaxLifeTime = 1200, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        AccelPerSec = 300f,
                        DesiredSpeed = 450,
                        MaxTrajectory = 5000f,
                        SpeedVariance = Random(start: 0, end: 20), // subtracts value from DesiredSpeed
                        RangeVariance = Random(start: 0, end: 100), // subtracts value from MaxTrajectory
                        TotalAcceleration = 0, // 0 means no limit, something to do due with a thing called delta and something called v.
                        Smarts = new SmartsDef
                        {
                            SteeringLimit = 0, // 0 means no limit, value is in degrees, good starting is 150.  This enable advanced smart "control", cost of 3 on a scale of 1-5, 0 being basic smart.
                            Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                            Aggressiveness = 3f, // controls how responsive tracking is, recommended value 3-5.
                            MaxLateralThrust = 0.5, // controls how sharp the projectile may turn, this is the cheaper but less realistic version of SteeringLimit, cost of 2 on a scale of 1-5, 0 being basic smart.
                            NavAcceleration = 0, // helps influence how the projectile steers, 0 defaults to 1/2 Aggressiveness value or 0 if its 0, a value less than 0 disables this feature. 
                            TrackingDelay = 60, // Measured in Shape diameter units traveled.
                            AccelClearance = false, // Setting this to true will prevent smart acceleration until it is clear of the grid and tracking delay has been met (free fall).
                            MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                            OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                            CheckFutureIntersection = false, // Utilize obstacle avoidance for drones/smarts
                            FutureIntersectionRange = 0, // Range in front of the projectile at which it will detect obstacle.  If set to zero it defaults to DesiredSpeed + Shape Diameter
                            MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                            NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                            Roam = false, // Roam current area after target loss
                            KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
                            OffsetRatio = 0.5f, // The ratio to offset the random direction (0 to 1) 
                            OffsetTime = 30, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                            OffsetMinRange = 200, // The range from target at which offsets are no longer active
                            FocusOnly = false, // only target the constructs Ai's focus target. Don't use with OverideTarget.
                            FocusEviction = false, // If FocusOnly and this to true will force smarts to lose target when there is no focus target
                            ScanRange = 1200, // 0 disables projectile screening, the max range that this projectile will be seen at by defending grids (adds this projectile to defenders lookup database). 
                            NoSteering = false, // this disables target follow and instead travel straight ahead (but will respect offsets).
                            MinTurnSpeed = 100, // set this to a reasonable value to avoid projectiles from spinning in place or being too aggressive turing at slow speeds 
                            NoTargetApproach = false, // If true approaches can begin prior to the projectile ever having had a target.
                            AltNavigation = false, // If true this will swap the default navigation algorithm from ProNav to ZeroEffort Miss.  Zero effort is more direct/precise but less cinematic 
                        },
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
                                Name = "Archer_MissileSmokeTrail", //Archer_MissileSmokeTrail MD_BulletGlowMedRed
                                Offset = Vector(x: 0, y: 0, z: 0f),
                                DisableCameraCulling = false,// If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                                Extras = new ParticleOptionDef
                                {
                                    Scale = 0.5f,
                                },
                            },
                            Hit = new ParticleDef
                            {
                                Name = "MD_FlakExplosion", //MXA_MissileExplosion
                                Offset = Vector(x: 0, y: 0, z: 0),
                                DisableCameraCulling = false,// If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                                Extras = new ParticleOptionDef
                                {
                                    Scale = 1f,
                                    HitPlayChance = 1f,
                                },
                            },
                        },
                        Lines = new LineDef
                        {
                            ColorVariance = Random(start: 0f, end: 5f), // multiply the color by random values within range.
                            WidthVariance = Random(start: -0.2f, end: 0.2f), // adds random value to default width (negatives shrinks width)
                            DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                            Tracer = new TracerBaseDef
                            {
                                Enable = true,
                                Length = 10f,
                                Width = 0.3f,
                                Color = Color(red: 30f, green: 6f, blue: 1.5f, alpha: 1f),
                                AlwaysDraw = false, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                                Textures = new[] { "MD_MissileThrustFlame", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            },
                            Trail = new TrailDef
                            {
                                Enable = true,
                                AlwaysDraw = true, // Prevents this tracer from being culled.  Only use if you have a reason too (very long tracers/trails).
                                Textures = new[] {
                            "WeaponLaser",
                        },
                                TextureMode = Normal,
                                DecayTime = 150,
                                Color = Color(red: 1.1f, green: 1.01f, blue: 1, alpha: 1f),
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
                        HitSound = "HWR_SmallExplosion",
                        ShieldHitSound = "",
                        PlayerHitSound = "",
                        VoxelHitSound = "",
                        FloatingHitSound = "",
                        HitPlayChance = 1f,
                        HitPlayShield = true,
                    }
                };
            }
        }

        AmmoDef Missiles_Missile_NPC
        {
            get
            {
                var ammo = Missiles_Missile;
                ammo.AmmoRound = "Missiles_Missile_NPC";
                ammo.Trajectory.Smarts.OverideTarget = true;
                return ammo;
            }
        }
    }
}