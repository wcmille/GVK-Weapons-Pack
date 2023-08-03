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
                var sk = new SabotKinetic(this, 1100.0f, 0.1f, 30.0f);
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

            //AmmoMagazine = "AutocannonClip", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            //AmmoRound = "AutocannonClip", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            //BaseDamage = 500f, // Direct damage; one steel plate is worth 100.
            //Mass = 10f, // In kilograms; how much force the impact will apply to the target.
            //Health = 0, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            //BackKickForce = 1000f, // Recoil. This is applied to the Parent Grid.
            //HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            //DamageScales = KineticDamage(60E3f),
            //Trajectory = MakeBasicTrajectory(1100),
        }
    }
}
