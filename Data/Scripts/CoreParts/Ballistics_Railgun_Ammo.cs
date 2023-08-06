using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        //const float railGunStandardConstant = 600.0f * 2.0f; //600J / damage * 50% efficiency * 1 MW/1000W
        const float railGunStandardConstant = joulesPerDamage * 2f * 0.000001f * 6f;
        const float joulesPerDamage = 600f;
        private AmmoDef SmallRailgunAmmo
        {
            get
            { 
            //AmmoMagazine = "SmallRailgunAmmo",
            //AmmoRound = "SmallRailgunAmmo",
            //BaseDamage = 66500f, //slightly more than 4 heavy armor cubes
            //Mass = 2000, // in kilograms
            //Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            //BackKickForce = 400000f,
            //HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            //NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
            //DamageScales = KineticDamage(3000.0f*3000.0f*5.0f),
            //Trajectory = MakeBasicTrajectory(4000),
            
                var sk = new SabotKinetic(this, 1550.0f, 2.5f, 20.0f);
                var AmmoGraphics = new GraphicDef
                {
                    ModelName = "",
                    VisualProbability = 1f,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MD_BulletGlowBigBlue", //ShipWelderArc
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "RailgunHitBlue",
                            ApplyToShield = false,
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
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
                        ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                        DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 100f,
                            Width = 0.01f,
                            Color = Color(red: 22, green: 32, blue: 50, alpha: 1),
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            DecayTime = 50,
                            Color = Color(red: 22, green: 32, blue: 50, alpha: 1),
                            Back = false,
                            CustomWidth = 0.25f,
                            UseWidthVariance = true,
                            UseColorFade = true,
                        },
                    },
                };
                var AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "HWR_LargeExplosion",
                    ShieldHitSound = "",
                    PlayerHitSound = "",
                    VoxelHitSound = "",
                    FloatingHitSound = "",
                    HitPlayChance = 1,
                    HitPlayShield = true,
                }; // Don't edit below this line
                var round = sk.AssembleRound("SmallRailgunAmmo", "SmallRailgunAmmo", AmmoGraphics, AmmoAudio);
                round.HybridRound = true; //AmmoMagazine based weapon with energy cost
                round.EnergyCost = 6.0f*100.0f*railGunStandardConstant /3.85f/ 2.34f * (9.0f/11.0f); //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel

                return round;
            }
        }

        private AmmoDef LargeRailgunAmmo_Apollo
        {
            //AmmoMagazine = "SmallRailgunAmmo",
            //AmmoRound = "SmallRailgunAmmo",
            //HybridRound = true, //AmmoMagazine based weapon with energy cost
            //EnergyCost = 0.09067669173f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            //BaseDamage = 33250f, //slightly more than 4 heavy armor cubes
            //Mass = 2000, // in kilograms
            //Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            //BackKickForce = 200000f,
            //EnergyMagazineSize = 0,
            //HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            //NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
            //DamageScales = KineticDamage(3500.0f*3500.0f*5.0f),
            //Trajectory = MakeBasicTrajectory(4500),
            get
            {
                var sk = new SabotKinetic(this, 1976.42f, 30.0f, 46.0f);
                var AmmoGraphics = new GraphicDef
                {
                    ModelName = "",
                    VisualProbability = 1f,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MD_BulletGlowBigBlue", //ShipWelderArc
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "RailgunHitBlue",
                            ApplyToShield = false,
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
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
                        ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                        DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 50f,
                            Width = 0.01f,
                            Color = Color(red: 11, green: 16, blue: 25, alpha: 1),
                            VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                            VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            DecayTime = 50,
                            Color = Color(red: 11, green: 16, blue: 25, alpha: 1),
                            Back = false,
                            CustomWidth = 0.25f,
                            UseWidthVariance = true,
                            UseColorFade = true,
                        },
                    },
                };
                var AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "HWR_LargeExplosion",
                    ShieldHitSound = "",
                    PlayerHitSound = "",
                    VoxelHitSound = "",
                    FloatingHitSound = "",
                    HitPlayChance = 1,
                    HitPlayShield = true,
                }; // Don't edit below this line
                var round = sk.AssembleRound("LargeRailgunAmmo", "LargeRailgunAmmo", AmmoGraphics, AmmoAudio);
                round.HybridRound = true; //AmmoMagazine based weapon with energy cost
                round.EnergyCost = 702.0f*railGunStandardConstant/1.1715625f/216.0f; //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                return round;
            }
        }

        private AmmoDef LargeRailgunAmmo
        {
            get
            {
                //AmmoMagazine = "LargeRailgunAmmo",
                //AmmoRound = "LargeRailgunAmmo",
                //HybridRound = true, //AmmoMagazine based weapon with energy cost
                //EnergyCost = 0.08030075188f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                //BaseDamage = 66500f, //slightly more than 4 heavy armor cubes
                //Mass = 2000, // in kilograms
                //Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                //BackKickForce = 400000f,
                //HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
                //NpcSafe = true, // This is you tell npc moders that your ammo was designed with them in mind, if they tell you otherwise set this to false.
                //DamageScales = KineticDamage(4000.0f * 4000.0f * 5.0f),
                //Trajectory = MakeBasicTrajectory(5000),
                var sk = new SabotKinetic(this, 5000.0f, 30.0f, 40.0f);
                var AmmoGraphics = new GraphicDef
                {
                    ModelName = "",
                    VisualProbability = 1f,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MD_BulletGlowBigBlue", //ShipWelderArc
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "RailgunHitBlue",
                            ApplyToShield = false,
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
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
                        ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                        DropParentVelocity = true, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 100f,
                            Width = 0.01f,
                            Color = Color(red: 22, green: 32, blue: 50, alpha: 1),
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Textures = new[] { "WeaponLaser", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            DecayTime = 50,
                            Color = Color(red: 22, green: 32, blue: 50, alpha: 1),
                            Back = false,
                            CustomWidth = 0.25f,
                            UseWidthVariance = true,
                            UseColorFade = true,
                        },
                    },
                };
                var AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "HWR_LargeExplosion",
                    ShieldHitSound = "",
                    PlayerHitSound = "",
                    VoxelHitSound = "",
                    FloatingHitSound = "",
                    HitPlayChance = 1,
                    HitPlayShield = true,
                }; // Don't edit below this line

                var round = sk.AssembleRound("LargeRailgunAmmo", "LargeRailgunAmmo", AmmoGraphics, AmmoAudio);
                round.HybridRound = true; //AmmoMagazine based weapon with energy cost
                round.EnergyCost = railGunStandardConstant/2.34f; //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                return round;
            }
        }
    }
}