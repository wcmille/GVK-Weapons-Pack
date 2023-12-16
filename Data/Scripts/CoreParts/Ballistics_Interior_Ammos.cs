using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Ballistics_Interior
        {
            get
            {
                var sk = new SabotKinetic(this, 950.0f, 0.004f, 5.56f);
                var AmmoGraphics = new GraphicDef
                {
                    ModelName = "",
                    VisualProbability = 0.2f,
                    Decals = MakeBulletDecal(),
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //ShipWelderArc
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "Hit_BasicAmmoSmall",
                            ApplyToShield = true,
                            Offset = Vector(x: double.MaxValue, y: double.MaxValue, z: double.MaxValue),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1f,
                                HitPlayChance = 0.75f,
                            },
                        },
                        Eject = new ParticleDef
                        {
                            Name = "Shell_Casings",
                            ApplyToShield = false,
                            Offset = Vector(x: double.MaxValue, y: double.MaxValue, z: double.MaxValue),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        ColorVariance = Random(start: 1f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: -0.01f, end: 0.01f), // adds random value to default width (negatives shrinks width)
                        DropParentVelocity = false, // If set to true will not take on the parents (grid/player) initial velocity when rendering.
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 15f,
                            Width = 0.03f,
                            Color = Color(red: 4.4f, green: 2.8f, blue: 2.0f, alpha: 1),
                            Textures = new[] { "ProjectileTrailLine", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                    },
                };
                var AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    ShieldHitSound = "",
                    PlayerHitSound = "",
                    VoxelHitSound = "",
                    FloatingHitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }; // Don't edit below this line
                return sk.AssembleRound("RapidFireAutomaticRifleGun_Mag_50rd", "Ballistics_Interior", AmmoGraphics, AmmoAudio);
            }
        }
    }
}