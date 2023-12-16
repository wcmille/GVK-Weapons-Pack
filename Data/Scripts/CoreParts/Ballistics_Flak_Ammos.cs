﻿using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.AoeShape;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaOfDamageDef.Falloff;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.FragmentDef.TimedSpawnDef.PointTypes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Ballistics_AP
        {
            get
            {
                var sk = new SabotKinetic(this, 1500, 6.0f, 105.0f);
                var ag = new GraphicDef
                {
                    ModelName = "",
                    VisualProbability = 1f,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MD_BulletGlowMedRed", //ShipWelderArc
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "Explosion_AmmunitionSmall", //Explosion_AmmunitionLarge  Collision_Sparks  Explosion_Warhead_50
                            ApplyToShield = false,
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1.5f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                        DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 16f,
                            Width = 0.45f,
                            Color = Color(red: 60, green: 30, blue: 10, alpha: 10),
                            Textures = new[] { "AryxBallisticTracer", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Textures = new[] { "WeaponLaser", },
                            DecayTime = 40,
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Back = false,
                            CustomWidth = 0,
                            UseWidthVariance = true,
                            UseColorFade = true,
                        },
                    },
                };
                var aa = new AmmoAudioDef
                {
                    TravelSound = "MD_Artillary_shell_fly",
                    HitSound = "HWR_SmallExplosion",
                    ShieldHitSound = "",
                    PlayerHitSound = "HWR_SmallExplosion",
                    VoxelHitSound = "ArcHeavyImpactSoil",
                    FloatingHitSound = "",
                    HitPlayChance = 1f,
                    HitPlayShield = true,
                };
                return sk.AssembleRound("MediumCalibreAmmo", "Ballistics_AP", ag, aa);
            }
        }

        private AmmoDef Ballistics_Flak => new AmmoDef
        {
            AmmoMagazine = "MediumCalibreAmmo",
            AmmoRound = "Ballistics_Flak",
            BaseDamage = 1000f,
            Mass = 100, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            Fragment = new FragmentDef
            {
                AmmoRound = "Ballistics_Flak_Shrapnel", // AmmoRound field of the ammo to spawn.
                Fragments = 30, // Number of projectiles to spawn.
                Degrees = 60, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards).
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = false, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = true, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 80, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Lead, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 1, // Number of spawns in each group
                    GroupDelay = 1, // Delay between each group.
                },
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                HealthHitModifier = 5, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                Characters = 0.1f,
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = -1f,
                    Heavy = -1f,
                    NonArmor = -1f,
                },
                DamageType = new DamageTypes
                {
                    Base = Kinetic,
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic,
                },
                Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
            },
            AreaOfDamage = new AreaOfDamageDef //This also applies HealthHitModifier damage to projectiles in the area
            {
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 80f,
                    Damage = 1f,
                    Depth = 0f, //NOT OPTIONAL, 0 or -1 breaks the manhattan distance
                    MaxAbsorb = 0f,
                    Falloff = Linear, //.NoFalloff applies the same damage to all blocks in radius
                    //.Linear drops evenly by distance from center out to max radius
                    //.Curve drops off damage sharply as it approaches the max radius
                    //.InvCurve drops off sharply from the middle and tapers to max radius
                    //.Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                    //.Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                    ArmOnlyOnHit = false,
                    MinArmingTime = 20,
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1,
                    CustomParticle = "MD_FlakExplosion",
                    CustomSound = "HWR_FlakExplosion",
                },
            },
            Trajectory = MakeAirburstTrajectory(900),
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = false,
                        Color = Color(red: 1, green: 1f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 5f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 1f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f,
                        Width = 0.4f,
                        Color = Color(red: 80, green: 40, blue: 8, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "AryxBallisticTracer",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",  //MXA_ImpactExplosion
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 1f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };

        private AmmoDef Ballistics_Flak_Shrapnel => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "Ballistics_Flak_Shrapnel",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 500f,
            Mass = 50, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            HardPointUsable = false, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            DamageScales = new DamageScaleDef
            {
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                HealthHitModifier = 5, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                Characters = 0.1f,
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = -1f,
                    Heavy = -1f,
                    NonArmor = -1f,
                },
                DamageType = new DamageTypes
                {
                    Base = Kinetic,
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic,
                },
                Custom = Common_Ammos_DamageScales_WheelsandCockpits_SmallNerf,
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 0f, // Meters
                    Damage = 0f,
                    Depth = 0f,
                    MaxAbsorb = 0f,
                    Falloff = NoFalloff, //.NoFalloff applies the same damage to all blocks in radius
                    //.Linear drops evenly by distance from center out to max radius
                    //.Curve drops off damage sharply as it approaches the max radius
                    //.InvCurve drops off sharply from the middle and tapers to max radius
                    //.Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                    //.Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                    //.Exponential drops off exponentially.  Does not scale to max radius
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 0, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = true,
                    ParticleScale = 1,
                    CustomParticle = "MD_FlakExplosion", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "", // SubtypeID from your Audio SBC, not a filename
                    Shape = Round, // Round or Diamond
                },
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                MaxLifeTime = 120, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                DesiredSpeed = 1200, // voxel phasing if you go above 5100
                MaxTrajectory = 120f, // Max Distance the projectile or beam can Travel.
                SpeedVariance = Random(start: 400, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 80, end: 0), // subtracts value from MaxTrajectory
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 0.15f,
                ShieldHitDraw = true,
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 5f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: -0.25f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5f,
                        Width = 0.25f,
                        Color = Color(red: 40, green: 40, blue: 40, alpha: 1),
                        Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 1f,
                HitPlayShield = true,
            },
        };
    }
}