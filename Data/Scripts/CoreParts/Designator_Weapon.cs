using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;

namespace Scripts 
{   
    partial class Parts 
	{
        // Don't edit above this line
		WeaponDefinition LaserDesignator => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef 
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "LaserDesignator",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.25f,
                        IconName = "None"
                    },
                },
                Muzzles = new [] {
					"muzzle_1",
                },
                Ejector = "",
                Scope = "", //Where line of sight checks are performed from must be clear of block collision
            },
            Targeting = new TargetingDef  
            {
                Threats = new[] {
                   Grids, Projectiles, Characters, Meteors,
                },
                SubSystems = new[] {
                    Thrust, Utility, Offense, Power, Production, Any,
                },
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 0, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 0, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef 
            {
                PartName = "Laser Designator", // name of weapon in terminal
                DeviateShotAngle = 0,
                AimingTolerance = 180, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = new UiDef {
                    RateOfFire = false,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef {
                    TrackTargets = false,
                    TurretAttached = false,
                    TurretController = false,
                    PrimaryTracking = false,
                    LockOnFocus = false,
                    SuppressFire = false,
                },
                HardWare = new HardwareDef {
                    RotateRate = 0,
                    ElevateRate = 0,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1000f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                    FixedOffset = false, // Deprecated.
                    Offset = Vector(x: 0, y: 0, z: 0), // Offsets the aiming/firing line of the weapon, in metres.
                    Type = BlockWeapon, // What type of weapon this is; BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour.
                        DefaultArmedTimer = 120, // Sets default countdown duration.
                        PreArmed = false, // Whether the warhead is armed by default when placed. Best left as false.
                        TerminalControls = true, // Whether the warhead should have terminal controls for arming and detonation.
                        AmmoRound = "AmmoType2", // Optional. If specified, the warhead will always use this ammo on detonation rather than the currently selected ammo.
                    },
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 2,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 3600,
                    
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
					DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1200, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .16667f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 300, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFull = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "", // WepShipGatlingShot
                    FiringSoundPerShot = false,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef //FIXTHIS
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 20, green: 20, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                    Effect2 = new ParticleDef //FIXTHIS
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 5, green: 40, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                },
            },

            Ammos = new[] { DesignatorLaser },
             //Animations= Raptor_Reload
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		WeaponDefinition LaserDesignatorTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LaserDesignatorTurret",
                        
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Muzzles = new []
                {
					"muzzle_missile_001",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 15000,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                PartName = "Laser Designator Turret", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = false,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 1000f, // Inventory capacity in kL.
                    IdlePower = 0.25f, // Constant base power draw in MW.
                    FixedOffset = false, // Deprecated.
                    Offset = Vector(x: 0, y: 0, z: 0), // Offsets the aiming/firing line of the weapon, in metres.
                    Type = BlockWeapon, // What type of weapon this is; BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour.
                        DefaultArmedTimer = 120, // Sets default countdown duration.
                        PreArmed = false, // Whether the warhead is armed by default when placed. Best left as false.
                        TerminalControls = true, // Whether the warhead should have terminal controls for arming and detonation.
                        AmmoRound = "AmmoType2", // Optional. If specified, the warhead will always use this ammo on detonation rather than the currently selected ammo.
                    },
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 2,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 3600,
                    
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
					DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1200, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .16667f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 300, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFull = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "", // WepShipGatlingShot
                    FiringSoundPerShot = false,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef //FIXTHIS
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 20, green: 20, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                    Effect2 = new ParticleDef //FIXTHIS
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 5, green: 40, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                },
            },

            Ammos = new[] { DesignatorLaser },
             //Animations= Raptor_Reload
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		
	}
}