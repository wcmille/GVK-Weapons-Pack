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
                round.EnergyCost = 6.0f * 100.0f * railGunStandardConstant / 3.85f / 2.34f * (9.0f / 11.0f); //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                round.BaseDamageCutoff = 8000f;

                return round;
            }
        }


        private AmmoDef LargeRailgunAmmo
        {
            get
            {
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
                round.EnergyCost = railGunStandardConstant / 2.34f; //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                round.BaseDamageCutoff = 8000f; // This is the damage at which the round will stop doing damage, it is not a hard limit, but a cutoff point where the damage will be reduced to 0.

                return round;
            }
        }
    }
}