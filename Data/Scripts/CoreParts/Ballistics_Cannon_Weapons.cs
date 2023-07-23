﻿using VRageMath;
using System.Collections.Generic;
using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.TargetingDef;
using static Scripts.Structure.WeaponDefinition.TargetingDef.CommunicationDef.Comms;
using static Scripts.Structure.WeaponDefinition.TargetingDef.CommunicationDef.SecurityMode;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;

namespace Scripts
{
    partial class Parts
    {
        const int CannonBaseRoF = 10;

        private TargetingDef Ballistics_Cannons_Targeting_LargeTurret => new TargetingDef
        {
            Threats = new[]
            {
                Grids,
            },
            SubSystems = new[]
            {
                Any,
            },
            ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
            IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
            LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
            MaxTargetDistance = 2400, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
            MinTargetDistance = 20, // 0 = unlimited, Min target distance that targets will be automatically shot at.
            TopTargets = 1, // 0 = unlimited, max number of top targets to randomize between.
            TopBlocks = 1, // 0 = unlimited, max number of blocks to randomize between
            StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
        };

        private TargetingDef Ballistics_Cannons_Targeting_SmallTurret => new TargetingDef
        {
            Threats = new[]
            {
                Grids,
            },
            SubSystems = new[]
            {
                Any,
            },
            ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
            IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
            LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
            MaxTargetDistance = 2400, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
            MinTargetDistance = 10, // 0 = unlimited, Min target distance that targets will be automatically shot at.
            TopTargets = 1, // 0 = unlimited, max number of top targets to randomize between.
            TopBlocks = 1, // 0 = unlimited, max number of blocks to randomize between
            StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
        };

        WeaponDefinition LargeBlockArtilleryTurret => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LargeCalibreTurret", // Block Subtypeid. Your Cubeblocks contain this information
                        SpinPartId = "None", // For weapons with a spinning barrel such as Gatling Guns. Subpart_Boomsticks must be written as Boomsticks.
                        MuzzlePartId = "MissileTurretBarrels", // The subpart where your muzzle empties are located. This is often the elevation subpart. Subpart_Boomsticks must be written as Boomsticks.
                        AzimuthPartId = "MissileTurretBase1", // Your Rotating Subpart, the bit that moves sideways.
                        ElevationPartId = "MissileTurretBarrels",// Your Elevating Subpart, that bit that moves up.
                        DurabilityMod = 0.5f, // GeneralDamageMultiplier, 0.25f = 25% damage taken.
                        IconName = "" // Overlay for block inventory slots, like reactors, refineries, etc.
                    },

                 },
                Muzzles = new[]
                {
                    "muzzle_missile_001", // Where your Projectiles spawn. Use numbers not Letters. IE Muzzle_01 not Muzzle_A
                    "muzzle_missile_002",
                },
                Ejector = "", // Optional; empty from which to eject "shells" if specified.
                Scope = "camera", // Where line of sight checks are performed from. Must be clear of block collision.
            },
            Targeting = Ballistics_Cannons_Targeting_LargeTurret,
            HardPoint = new HardPointDef
            {
                PartName = "155mm Artillery Turret", // name of weapon in terminal
                DeviateShotAngle = 0.05f,
                AimingTolerance = 0.8f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret,
                HardWare = new HardwareDef
                {
                    RotateRate = 0.003f,
                    ElevateRate = 0.003f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -15,
                    MaxElevation = 75,
                    InventorySize = 0.960f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom
                    IdlePower = 0.01f, // Power draw in MW while not charging, or for non-energy weapons. Defaults to 0.001.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 21,
                    MuzzleCheck = false,
                    DisableLosCheck = false, // Do not perform LOS checks at all... not advised for self tracking weapons
                    NoVoxelLosCheck = false, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
                    Debug = false,
                    RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef
                {
                    RateOfFire = CannonBaseRoF * 2, //180 // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    ReloadTime = 120, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    GiveUpAfter = true,
                    MagsToLoad = 2, // Number of physical magazines to consume on reload.
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "MD_VanillaCannonShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "WepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate", // Audio played when turret is moving.
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_MediumCalibre", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[]
            {
                LargeCalibreAmmo,
            },
        };

        WeaponDefinition LargeBlockArtillery => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LargeBlockLargeCalibreGun", // Block Subtypeid. Your Cubeblocks contain this information
                        SpinPartId = "None", // For weapons with a spinning barrel such as Gatling Guns. Subpart_Boomsticks must be written as Boomsticks.
                        MuzzlePartId = "None", // The subpart where your muzzle empties are located. This is often the elevation subpart. Subpart_Boomsticks must be written as Boomsticks.
                        AzimuthPartId = "None", // Your Rotating Subpart, the bit that moves sideways.
                        ElevationPartId = "None",// Your Elevating Subpart, that bit that moves up.
                        DurabilityMod = 0.5f, // GeneralDamageMultiplier, 0.25f = 25% damage taken.
                        IconName = "" // Overlay for block inventory slots, like reactors, refineries, etc.
                    },
                 },
                Muzzles = new[]
                {
                    "muzzle_missile_001", // Where your Projectiles spawn. Use numbers not Letters. IE Muzzle_01 not Muzzle_A
                },
                Ejector = "", // Optional; empty from which to eject "shells" if specified.
                Scope = "muzzle_missile_001", // Where line of sight checks are performed from. Must be clear of block collision.
            },
            Targeting = Common_Weapons_Targeting_Fixed_NoTargeting,
            HardPoint = new HardPointDef
            {
                PartName = "155mm Fixed Cannon", // name of weapon in terminal
                DeviateShotAngle = 0.1f,
                NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,
                HardWare = new HardwareDef
                {
                    InventorySize = 0.960f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
                    IdlePower = 0.001f, // Power draw in MW while not charging, or for non-energy weapons. Defaults to 0.001.
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 21,
                    MuzzleCheck = false,
                    DisableLosCheck = true, // Do not perform LOS checks at all... not advised for self tracking weapons
                    NoVoxelLosCheck = true, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
                    Debug = false,
                    RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef
                {
                    RateOfFire = CannonBaseRoF, //180 // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    ReloadTime = 90, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    GiveUpAfter = true,
                    MagsToLoad = 4, // Number of physical magazines to consume on reload.
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "MD_VanillaCannonShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "WepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate", // Audio played when turret is moving.
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "Muzzle_Flash_MediumCalibre", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false, // Whether to end a looping effect instantly when firing stops.
                            Scale = 1f, // Scale of effect.
                        },
                    },
                },
            },
            Ammos = new[]
            {
                LargeCalibreAmmo,
            },
        };

        //WeaponDefinition VehicleTurret122mm => new WeaponDefinition
        //{

        //    Assignments = new ModelAssignmentsDef
        //    {
        //        MountPoints = new[]
        //        {
        //            new MountPointDef
        //            {
        //                SubtypeId = "OKI122mmVT",
        //                SpinPartId = "Boomsticks", // For weapons with a spinning barrel such as Gatling Guns
        //                MuzzlePartId = "MissileTurretBarrels",
        //                AzimuthPartId = "MissileTurretBase1",
        //                ElevationPartId = "MissileTurretBarrels",
        //                DurabilityMod = 0.5f,
        //                IconName = ""
        //            },
        //        },
        //        Muzzles = new[]
        //        {
        //            "muzzle_projectile_1",
        //        },
        //        Ejector = "",
        //        Scope = "", //Where line of sight checks are performed from must be clear of block collision
        //    },
        //    Targeting = Ballistics_Cannons_Targeting_SmallTurret,
        //    HardPoint = new HardPointDef
        //    {
        //        PartName = "Small 155mm Cannon Turret", // name of weapon in terminal
        //        DeviateShotAngle = 0.2f,
        //        AimingTolerance = 0.5f, // 0 - 180 firing angle
        //        AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
        //        AddToleranceToTracking = false,
        //        CanShootSubmerged = false,
        //        Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
        //        Ai = Common_Weapons_Hardpoint_Ai_BasicTurret,
        //        HardWare = new HardwareDef
        //        {
        //            RotateRate = 0.010f,
        //            ElevateRate = 0.010f,
        //            MinAzimuth = -180,
        //            MaxAzimuth = 180,
        //            MinElevation = -15,
        //            MaxElevation = 75,
        //            InventorySize = 0.480f,
        //            Offset = Vector(x: 0, y: 0, z: 0),
        //            Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
        //            IdlePower = 0.01f, // Power draw in MW while not charging, or for non-energy weapons. Defaults to 0.001.
        //        },
        //        Other = new OtherDef
        //        {
        //            ConstructPartCap = 21,
        //            MuzzleCheck = false,
        //            DisableLosCheck = false, // Do not perform LOS checks at all... not advised for self tracking weapons
        //            NoVoxelLosCheck = false, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
        //            Debug = false,
        //            RestrictionRadius = 0.5f, // Meters, radius of sphere disable this gun if another is present
        //            CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
        //            CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
        //        },
        //        Loading = new LoadingDef
        //        {
        //            RateOfFire = CannonBaseRoF,
        //            BarrelsPerShot = 1,
        //            TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
        //            ReloadTime = 90, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
        //            GiveUpAfter = true, // Whether the weapon should drop its current target and reacquire a new target after finishing its magazine or burst.
        //            MagsToLoad = 1, // Number of physical magazines to consume on reload.
        //        },
        //        Audio = new HardPointAudioDef
        //        {
        //            PreFiringSound = "", // Audio for warmup effect.
        //            FiringSound = "MD_VanillaCannonShot", // Audio for firing.
        //            FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
        //            ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
        //            NoAmmoSound = "WepShipGatlingNoAmmo",
        //            HardPointRotationSound = "WepTurretGatlingRotate", // Audio played when turret is moving.
        //            BarrelRotationSound = "",
        //            FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
        //            FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
        //        },

        //        Graphics = new HardPointParticleDef
        //        {
        //            Effect1 = new ParticleDef
        //            {
        //                Name = "Muzzle_Flash_MediumCalibre", // SubtypeId of muzzle particle effect.
        //                Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
        //                Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
        //                Extras = new ParticleOptionDef
        //                {
        //                    Loop = false, // Set this to the same as in the particle sbc!
        //                    Restart = false, // Whether to end a looping effect instantly when firing stops.
        //                    Scale = 1f, // Scale of effect.
        //                },
        //            },
        //        },
        //    },
        //    Ammos = new[]
        //    {
        //        LargeCalibreAmmo,
        //    },
        //};

        //WeaponDefinition SmallCannon122mm => new WeaponDefinition
        //{
        //    Assignments = new ModelAssignmentsDef
        //    {
        //        MountPoints = new[]
        //        {
        //            new MountPointDef
        //            {
        //                SubtypeId = "OKI122mmSGfixed",
        //                SpinPartId = "Boomsticks", // For weapons with a spinning barrel such as Gatling Guns
        //                MuzzlePartId = "None",
        //                AzimuthPartId = "None",
        //                ElevationPartId = "None",
        //                DurabilityMod = 0.5f,
        //                IconName = ""
        //            },
        //        },
        //        Muzzles = new[]
        //        {
        //            "muzzle_projectile_1",
        //        },
        //        Ejector = "",
        //        Scope = "", //Where line of sight checks are performed from must be clear of block collision
        //    },
        //    Targeting = Common_Weapons_Targeting_Fixed_NoTargeting,
        //    HardPoint = new HardPointDef
        //    {
        //        PartName = "Small 155mm Fixed Cannon", // name of weapon in terminal
        //        DeviateShotAngle = 0.1f,
        //        NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
        //        Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
        //        Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,
        //        HardWare = new HardwareDef
        //        {
        //            InventorySize = 0.270f,
        //            Offset = Vector(x: 0, y: 0, z: 0),
        //            Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
        //            IdlePower = 0.001f, // Power draw in MW while not charging, or for non-energy weapons. Defaults to 0.001.
        //        },
        //        Other = new OtherDef
        //        {
        //            ConstructPartCap = 21,
        //            DisableLosCheck = true, // Do not perform LOS checks at all... not advised for self tracking weapons
        //            NoVoxelLosCheck = true, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
        //            MuzzleCheck = false,
        //            Debug = false,
        //            RestrictionRadius = 0.5f, // Meters, radius of sphere disable this gun if another is present
        //            CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
        //            CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
        //        },
        //        Loading = new LoadingDef
        //        {
        //            RateOfFire = CannonBaseRoF, //180 // visual only, 0 disables and uses RateOfFire
        //            BarrelsPerShot = 1,
        //            TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
        //            ReloadTime = 90, // Measured in game ticks (use 3600/ROF for consistant fire rate).
        //            MagsToLoad = 1, // Number of physical magazines to consume on reload.
        //        },
        //        Audio = new HardPointAudioDef
        //        {
        //            PreFiringSound = "", // Audio for warmup effect.
        //            FiringSound = "MD_VanillaCannonShot", // Audio for firing.
        //            FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
        //            ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
        //            NoAmmoSound = "WepShipGatlingNoAmmo",
        //            HardPointRotationSound = "WepTurretGatlingRotate", // Audio played when turret is moving.
        //            BarrelRotationSound = "",
        //            FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
        //            FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
        //        },
        //        Graphics = new HardPointParticleDef
        //        {
        //            Effect1 = new ParticleDef
        //            {
        //                Name = "Muzzle_Flash_MediumCalibre", // SubtypeId of muzzle particle effect.
        //                Color = Color(red: 0, green: 0, blue: 0, alpha: 1), // Deprecated, set color in particle sbc.
        //                Offset = Vector(x: 0, y: 0, z: 0), // Offsets the effect from the muzzle empty.
        //                Extras = new ParticleOptionDef
        //                {
        //                    Loop = false, // Set this to the same as in the particle sbc!
        //                    Restart = false, // Whether to end a looping effect instantly when firing stops.
        //                    Scale = 1f, // Scale of effect.
        //                },
        //            },
        //        },
        //    },
        //    Ammos = new[]
        //    {
        //        LargeCalibreAmmo,
        //    },
        //};
    }
}