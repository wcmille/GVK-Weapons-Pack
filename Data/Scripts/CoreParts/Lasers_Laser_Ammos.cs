﻿using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        const float laserScale = 0.4f;
        const float laserStandardConstant = joulesPerDamage * 2.0f * 0.001f / laserScale; //600J / damage * 50% efficiency * 1 MW/1000W

        private AmmoDef Lasers_Laser_Small => new AmmoDef //Blue Receptor laser
        {
            AmmoMagazine = "Energy",
            AmmoRound = "Lasers_Laser_Small",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = laserStandardConstant, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel    (15 * 0.05 * 3600/60/60 = 0.75MW per tick)
            BaseDamage = 75 * laserScale,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            HardPointUsable = true,
            NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.			
            Fragment = new FragmentDef
            {
                AmmoRound = "Lasers_Laser_Small_Shrapnel", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
            },
            DamageScales = new DamageScaleDef
            {
                DamageVoxels = false, // Whether to damage voxels.
                HealthHitModifier = 1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                Characters = 0.25f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = 0.75f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.8f, // Multiplier for damage against light armor.
                    Heavy = 0.6f, // Multiplier for damage against heavy armor.
                    NonArmor = -1f, // Multiplier for damage against every else.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
            },
            Beams = new BeamDef
            {
                Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
                FakeVoxelHitTicks = 30, // If this beam hits/misses a voxel it assumes it will continue to do so for this many ticks at the same hit length and not extend further within this window.  This can save up to n times worth of cpu.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                MaxTrajectory = 1100f,
                RangeVariance = Random(start: 0, end: 50), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 10, // How long the weapon must fire before it reaches MaxTrajectory.
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "MD_BulletGlowBigBlue", //Archer_MissileSmokeTrail
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0f),
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "Lasers_Laser_BlueHit",//MD_BulletGlowBigBlue
                        ApplyToShield = false,
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1f),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Scale = 1,
                            HitPlayChance = 1,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: -50f, end: 50f), // multiply the color by random values within range.
                    WidthVariance = Random(start: -.1f, end: 0.1f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5f,
                        Width = .15f,
                        Color = Color(red: 6, green: 15, blue: 60, alpha: 1f),
                        Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    },
                },
            },
        };

        private AmmoDef Lasers_Laser_Large => new AmmoDef // Your ID, for slotting into the Weapon CS
        {
            AmmoMagazine = "Energy", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "Lasers_Laser_Large", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = laserStandardConstant, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 150f * laserScale, // Direct damage; one steel plate is worth 100.
            Health = 0, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.			
            //Fragment = new FragmentDef
            //{
            //    AmmoRound = "Lasers_Laser_Large_Shrapnel", // AmmoRound field of the ammo to spawn.
            //    Fragments = 1, // Number of projectiles to spawn.
            //    Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
            //    Reverse = false, // Spawn projectiles backward instead of forward.
            //    DropVelocity = false, // fragments will not inherit velocity from parent.
            //    Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
            //    Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
            //    MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
            //    IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
            //    AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
            //},
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = -1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = -1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 0.25f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = 0.75f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.8f, // Multiplier for damage against light armor.
                    Heavy = 0.6f, // Multiplier for damage against heavy armor.
                    NonArmor = -1f, // Multiplier for damage against every else.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
            },
            Beams = new BeamDef
            {
                Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
                FakeVoxelHitTicks = 30, // If this beam hits/misses a voxel it assumes it will continue to do so for this many ticks at the same hit length and not extend further within this window.  This can save up to n times worth of cpu.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                MaxLifeTime = 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                MaxTrajectory = 1800f, // Max Distance the projectile or beam can Travel.
                RangeVariance = Random(start: 0, end: 200), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 10, // How long the weapon must fire before it reaches MaxTrajectory.
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1f, // %
                Particles = new AmmoParticleDef
                {
                    Hit = new ParticleDef
                    {
                        Name = "Lasers_Laser_RedHit",
                        ApplyToShield = false,
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
                    ColorVariance = Random(start: 0.5f, end: 1f), // multiply the color by random values within range.
                    WidthVariance = Random(start: -0.1f, end: 0.1f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f, //
                        Width = 0.4f, //
                        Color = Color(red: 60, green: 10, blue: 10f, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    },
                },
            },
        };

        private AmmoDef Lasers_Laser_Dual => new AmmoDef //Dual Large Spartan Laser
        {
            AmmoMagazine = "Energy", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "Lasers_Laser_Dual", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = laserStandardConstant, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 150f * laserScale, // Direct damage; one steel plate is worth 100.
            Health = 0, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.			
            //Fragment = new FragmentDef
            //{
            //    AmmoRound = "Lasers_Laser_Large_Shrapnel", // AmmoRound field of the ammo to spawn.
            //    Fragments = 1, // Number of projectiles to spawn.
            //    Degrees = 0, // Cone in which to randomize direction of spawned projectiles.
            //    Reverse = false, // Spawn projectiles backward instead of forward.
            //    DropVelocity = false, // fragments will not inherit velocity from parent.
            //    Offset = 0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
            //    Radial = 0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
            //    MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
            //    IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
            //    AdvOffset = Vector(x: 0, y: 0, z: 0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
            //},
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                HealthHitModifier = -1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                Characters = 0.25f, // Character damage multiplier; defaults to 1 if zero or less.
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = 0.75f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.8f, // Multiplier for damage against light armor.
                    Heavy = 0.6f, // Multiplier for damage against heavy armor.
                    NonArmor = -1f, // Multiplier for damage against every else.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
            },
            Beams = new BeamDef
            {
                Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = true, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
                FakeVoxelHitTicks = 30, // If this beam hits/misses a voxel it assumes it will continue to do so for this many ticks at the same hit length and not extend further within this window.  This can save up to n times worth of cpu.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed
                MaxLifeTime = 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                MaxTrajectory = 1800f, // Max Distance the projectile or beam can Travel.
                RangeVariance = Random(start: 0, end: 200), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 10, // How long the weapon must fire before it reaches MaxTrajectory.
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1f, // %
                ShieldHitDraw = false,
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.5f, end: 1f), // multiply the color by random values within range.
                    WidthVariance = Random(start: -0.3f, end: 0.01f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f, //
                        Width = 0.5f, //
                        Color = Color(red: 20, green: 16, blue: 50f, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        DecayTime = 60, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 5, green: 4, blue: 10f, alpha: 0.5f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = true,
                        UseColorFade = true,
                    },
                },
            },
        };

        //private AmmoDef Lasers_Laser_Large_Shrapnel => new AmmoDef //Red PDX laser Shrapnel
        //{
        //    AmmoMagazine = "Energy",
        //    AmmoRound = "Lasers_Laser_Large_Shrapnel",
        //    HybridRound = false, //AmmoMagazine based weapon with energy cost
        //    EnergyCost = 0.0000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel    (15 * 0.05 * 3600/60/60 = 0.75MW per tick)
        //    BaseDamage = 150f,
        //    HardPointUsable = false,
        //    DamageScales = new DamageScaleDef
        //    {
        //        MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
        //        DamageVoxels = false, // Whether to damage voxels.
        //        SelfDamage = false, // Whether to damage the weapon's own grid.
        //        HealthHitModifier = -1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
        //        VoxelHitModifier = -1, // Voxel damage multiplier; defaults to 1 if zero or less.
        //        Characters = 0.25f, // Character damage multiplier; defaults to 1 if zero or less.
        //        // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
        //        Grids = new GridSizeDef
        //        {
        //            Large = -1f, // Multiplier for damage against large grids.
        //            Small = 0.75f, // Multiplier for damage against small grids.
        //        },
        //        Armor = new ArmorDef
        //        {
        //            Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
        //            Light = 0.8f, // Multiplier for damage against light armor.
        //            Heavy = 0.6f, // Multiplier for damage against heavy armor.
        //            NonArmor = -1f, // Multiplier for damage against every else.
        //        },
        //        DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
        //        {
        //            Base = Energy, // Base Damage uses this
        //            AreaEffect = Energy,
        //            Detonation = Energy,
        //            Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
        //        },
        //        Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
        //    },
        //    Beams = new BeamDef
        //    {
        //        Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
        //        VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
        //        ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
        //        RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
        //        OneParticle = true, // Only spawn one particle hit per beam weapon.
        //        FakeVoxelHitTicks = 30, // If this beam hits/misses a voxel it assumes it will continue to do so for this many ticks at the same hit length and not extend further within this window.  This can save up to n times worth of cpu.
        //    },
        //    Trajectory = new TrajectoryDef
        //    {
        //        Guidance = None,
        //        MaxTrajectory = 10f,
        //        RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
        //        DesiredSpeed = 1000, // voxel phasing if you go above 5100
        //    },
        //};

    //    private AmmoDef Lasers_Laser_Small_Shrapnel => new AmmoDef //Blue Receptor laser Shrapnel
    //    {
    //        AmmoMagazine = "Energy",
    //        AmmoRound = "Lasers_Laser_Small_Shrapnel",
    //        HybridRound = false, //AmmoMagazine based weapon with energy cost
    //        EnergyCost = 0.0000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel    (15 * 0.05 * 3600/60/60 = 0.75MW per tick)
    //        BaseDamage = 75f,
    //        Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
    //        HardPointUsable = false,
    //        DamageScales = new DamageScaleDef
    //        {
    //            MaxIntegrity = 0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
    //            DamageVoxels = false, // Whether to damage voxels.
    //            HealthHitModifier = -1, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
    //            Characters = 0.25f, // Character damage multiplier; defaults to 1 if zero or less.
    //            // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
    //            Grids = new GridSizeDef
    //            {
    //                Large = -1f, // Multiplier for damage against large grids.
    //                Small = 0.75f, // Multiplier for damage against small grids.
    //            },
    //            Armor = new ArmorDef
    //            {
    //                Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
    //                Light = 0.8f, // Multiplier for damage against light armor.
    //                Heavy = 0.6f, // Multiplier for damage against heavy armor.
    //                NonArmor = -1f, // Multiplier for damage against every else.
    //            },
    //            DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
    //            {
    //                Base = Energy, // Base Damage uses this
    //                AreaEffect = Energy,
    //                Detonation = Energy,
    //                Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
    //            },
    //            Custom = Common_Ammos_DamageScales_Cockpits_SmallNerf,
    //        },
    //        Beams = new BeamDef
    //        {
    //            Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
    //            VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
    //            ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
    //            RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
    //            OneParticle = true, // Only spawn one particle hit per beam weapon.
    //            FakeVoxelHitTicks = 30, // If this beam hits/misses a voxel it assumes it will continue to do so for this many ticks at the same hit length and not extend further within this window.  This can save up to n times worth of cpu.
    //        },
    //        Trajectory = new TrajectoryDef
    //        {
    //            Guidance = None,
    //            MaxTrajectory = 10f,
    //            RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
    //            DesiredSpeed = 1000, // voxel phasing if you go above 5100
    //        },
    //    };
    }
}