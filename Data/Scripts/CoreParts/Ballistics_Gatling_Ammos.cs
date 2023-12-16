using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.FactionColor;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef NATO_25x184mm
        {
            get
            {
                var sk = new SabotKinetic(this, 1345.0f, 0.1f, 25.0f);
                var ag = new GraphicDef
                {
                    VisualProbability = 0.2f,
                    Decals = MakeBulletDecal(),
                    Particles = new AmmoParticleDef
                    {
                        Hit = new ParticleDef
                        {
                            Name = "MaterialHit_Metal_GatlingGun", //MaterialHit_Metal
                            ApplyToShield = false,
                            Offset = Vector(x: 0, y: 0, z: 0),
                            DisableCameraCulling = true, // If not true will not cull when not in view of camera, be careful with this and only use if you know you need it
                            Extras = new ParticleOptionDef
                            {
                                Scale = 0.75f,
                                HitPlayChance = 0.2f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                        WidthVariance = Random(start: -0.05f, end: 0.05f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 25f,
                            Width = 0.10f,
                            FactionColor = DontUse, // DontUse, Foreground, Background.
                            Color = Color(red: 22f, green: 10f, blue: 10f, alpha: 1),
                            Textures = new[] { "ProjectileTrailLine", },// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        },
                    }
                };
                var aa = new AmmoAudioDef
                {
                    HitSound = "MD_BulletHit",
                    HitPlayChance = 0.15f,
                };
                return sk.AssembleRound("NATO_25x184mm", "NATO_25x184mm", ag, aa);
            }
        }
    }
}