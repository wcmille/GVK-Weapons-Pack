﻿using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;

namespace Scripts
{
    partial class Parts
    {
        // Don't edit above this line


        WeaponDefinition LargeBlockRailgun => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LargeRailgun", // Block Subtypeid. Your Cubeblocks contain this information
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
                    "barrel_001", // Where your Projectiles spawn. Use numbers not Letters. IE Muzzle_01 not Muzzle_A
                },
                Ejector = "", // Optional; empty from which to eject "shells" if specified.
                Scope = "barrel_001", // Where line of sight checks are performed from. Must be clear of block collision.
            },
            Targeting = Common_Weapons_Targeting_Fixed_NoTargeting,
            HardPoint = new HardPointDef
            {
                PartName = "Large Railgun", // Name of the weapon in terminal, should be unique for each weapon definition that shares a SubtypeId (i.e. multiweapons).
                DeviateShotAngle = 0.04f, // Projectile inaccuracy in degrees.
                NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,
                HardWare = new HardwareDef
                {
                    InventorySize = 0.4f, // Inventory capacity in kL.
                    IdlePower = 0.001f, // Constant base power draw in MW.
                    Offset = Vector(x: 0, y: 0, z: 0), // Offsets the aiming/firing line of the weapon, in metres.
                    Type = BlockWeapon, // What type of weapon this is; BlockWeapon, HandWeapon, Phantom 
                },
                Other = new OtherDef
                {
                    //ConstructPartCap = 21, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    MuzzleCheck = false, // Whether the weapon should check LOS from each individual muzzle in addition to the scope.
                    DisableLosCheck = true, // Do not perform LOS checks at all... not advised for self tracking weapons
                    NoVoxelLosCheck = true, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
                    Debug = false, // Force enables debug mode.
                    //RestrictionRadius = 0, // Prevents other blocks of this type from being placed within this distance of the centre of the block.
                    //CheckInflatedBox = false, // If true, the above distance check is performed from the edge of the block instead of the centre.
                    //CheckForAnyWeapon = false, // If true, the check will fail if ANY weapon is present, not just weapons of the same subtype.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    TrajectilesPerBarrel = 1, // Number of projectiles per muzzle per fire event.
                    ReloadTime = 60*23+26, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 120, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = false, // Will start recharging whenever power cap is not full.                   
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "ArcWepRailgunLargeCharge", // Audio for warmup effect.
                    FiringSound = "ArcWepRailgunLargeShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "ArcWepShipRailgunNoAmmo",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
            },
            Ammos = new[]
            {
                LargeRailgunAmmo,
            },
            Animations = LargeRailgunAnimation,
        };

        WeaponDefinition SmallBlockRailgun => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallRailgun", // Block Subtypeid. Your Cubeblocks contain this information
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
                    "barrel_001", // Where your Projectiles spawn. Use numbers not Letters. IE Muzzle_01 not Muzzle_A
                },
                Ejector = "", // Optional; empty from which to eject "shells" if specified.
                Scope = "barrel_001", // Where line of sight checks are performed from. Must be clear of block collision.
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Jumping, Steering, Any
                },
                ClosestFirst = false, // Tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                TopTargets = 1, // Maximum number of targets to randomize between; 0 = unlimited.
                TopBlocks = 1, // Maximum number of blocks to randomize between; 0 = unlimited.
                StopTrackingSpeed = 1000, // Do not track threats traveling faster than this speed; 0 = unlimited.
            },
            HardPoint = new HardPointDef
            {
                PartName = "Small Railgun", // Name of the weapon in terminal, should be unique for each weapon definition that shares a SubtypeId (i.e. multiweapons).
                DeviateShotAngle = 0.04f, // Projectile inaccuracy in degrees.
                NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                Ui = Common_Weapons_Hardpoint_Ui_FullDisable,
                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,
                HardWare = new HardwareDef
                {
                    InventorySize = 0.050f, // Inventory capacity in kL.
                    IdlePower = 0.001f, // Constant base power draw in MW.
                    Offset = Vector(x: 0, y: 0, z: 0), // Offsets the aiming/firing line of the weapon, in metres.
                    Type = BlockWeapon, // What type of weapon this is; BlockWeapon, HandWeapon, Phantom 
                },
                Other = new OtherDef
                {
                    //ConstructPartCap = 21, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    MuzzleCheck = false, // Whether the weapon should check LOS from each individual muzzle in addition to the scope.
                    DisableLosCheck = true, // Do not perform LOS checks at all... not advised for self tracking weapons
                    NoVoxelLosCheck = true, // If set to true this ignores voxels for LOS checking.. which means weapons will fire at targets behind voxels.  However, this can save cpu in some situations, use with caution.
                    Debug = false, // Force enables debug mode.
                    //RestrictionRadius = 0, // Prevents other blocks of this type from being placed within this distance of the centre of the block.
                    //CheckInflatedBox = false, // If true, the above distance check is performed from the edge of the block instead of the centre.
                    //CheckForAnyWeapon = false, // If true, the check will fail if ANY weapon is present, not just weapons of the same subtype.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 60, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    TrajectilesPerBarrel = 1, // Number of projectiles per muzzle per fire event.
                    ReloadTime = 11, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 30, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    StayCharged = false, // Will start recharging whenever power cap is not full.
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "WepRailgunSmallCharge", // Audio for warmup effect.
                    FiringSound = "WepRailgunSmallShot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "WepShipGatlingNoAmmo",
                    HardPointRotationSound = "", // Audio played when turret is moving.
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = true, // Don't stop firing sound from looping when delaying after burst.
                },
            },
            Ammos = new[]
            {
                SmallRailgunAmmo,
            },
            Animations = SmallRailgunAnimation,
        };
    }
}