using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef AutocannonClip 
        {
            get 
            {
                //Modelled from this weapon: https://en.wikipedia.org/wiki/Rheinmetall_Oerlikon_Millennium_Gun
                var sk = new SabotKinetic(this, 1440.0f, 0.45f, 30.0f);
                var ag = new GraphicDef
                {
                    ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                    VisualProbability = 1f, // %
                    Decals = MakeBulletDecal(),
                    Particles = new AmmoParticleDef
                    {
                        Hit = new ParticleDef
                        {
                            Name = "Collision_Sparks",  //MaterialHit_Metal_GatlingGun
                            ApplyToShield = false,
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Scale = 1,
                                HitPlayChance = 0.5f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        ColorVariance = Random(start: 0f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: -0.05f, end: 0.05f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 20f, //
                            Width = 0.15f, //
                            Color = Color(red: 5f, green: 15, blue: 5f, alpha: 1), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                            VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                            VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                            Textures = new[] { "ProjectileTrailLine", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                    },
                };
                var aa = new AmmoAudioDef
                {
                    TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                    HitSound = "AutocannonExplosion", // AutocannonExplosion
                    ShotSound = "",
                    ShieldHitSound = "",
                    PlayerHitSound = "ArcWepShipGatlingImpPlay",
                    VoxelHitSound = "ArcWepShipAutocannonSoil",
                    FloatingHitSound = "",
                    HitPlayChance = 1f,
                    HitPlayShield = true,
                };

                return sk.AssembleRound("AutocannonClip", "AutocannonClip", ag, aa);
            }
        }
    }
}
