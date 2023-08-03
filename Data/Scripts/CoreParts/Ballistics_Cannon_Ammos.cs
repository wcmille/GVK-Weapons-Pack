using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;

namespace Scripts
{
    partial class Parts
    {
        private AmmoDef LargeCalibreAmmo
        {
            get
            {
                float vel = 1500.0f;
                float roundMass = 10.0f;
                var kd = new SabotKinetic(this, vel, roundMass, 155.0f);
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
                            Color = Color(red: 60, green: 20, blue: 5, alpha: 10),
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
                return kd.AssembleRound("LargeCalibreAmmo", "LargeCalibreAmmo", ag, aa);
            }
        }
    }
}