using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CrimsonTeksMod.Items.Weapons.GlassCannon
{
    public class GlassShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.ai[1] = 0;
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.hostile = false;

            if (this is EbonglassShard)
            {
                projectile.knockBack = 0f;
                projectile.penetrate = 1;
            }
            else if (this is CrimglassShard)
            {
                projectile.knockBack = 0.15f;
                projectile.penetrate = 1;
            }
            else if (this is PearlglassShard)
            {
                projectile.knockBack = 0.1f;
                projectile.penetrate = 3;
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 10;
            }
            else
            {
                projectile.knockBack = 0.1f;
                projectile.penetrate = 1;
            }
        }

        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0] % 4;
            projectile.ai[1]++;

            if (Main.rand.Next(10) == 0)
                Glass.CreateDust(this, projectile.Center);

            if (projectile.velocity.Y <= 18)
                projectile.velocity.Y += 0.1f + projectile.frame * 0.02f;

            if (projectile.velocity.X > 0.04f)
                projectile.velocity.X -= 0.03f;
            else if (projectile.velocity.X < -0.04)
                projectile.velocity.X += 0.03f;
            else
                projectile.velocity.X = 0f;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (this is EbonglassShard)
                target.AddBuff(BuffID.CursedInferno, 180);
            else if (this is CrimglassShard)
                target.AddBuff(BuffID.Ichor, 180);

            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[1] < 3)
                return false;
            return base.OnTileCollide(oldVelocity);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 2;
            height = 2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void Kill(int timeLeft)
        {
            int rnd = Main.rand.Next(1, 4);
            for (int i = 0; i < rnd; i++)
            {
                Glass.CreateDust(this, projectile.Center);
            }

            Terraria.Audio.LegacySoundStyle glass = SoundID.Item27;
            glass.WithVolume(0.4f);
            Main.PlaySound(glass, projectile.position);
        }
    }
}
