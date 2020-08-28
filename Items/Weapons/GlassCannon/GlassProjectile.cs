using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CrimsonTeksMod.Items.Weapons.GlassCannon
{
    public class GlassProjectile : ModProjectile
    {
        protected int shardType;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Projectile");
            ProjectileID.Sets.ForcePlateDetection[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.hostile = false;
            if (this is EbonglassProjectile)
            {
                projectile.damage = 16;
                projectile.knockBack = 0.4f;
                projectile.penetrate = 4;

                shardType = ModContent.ProjectileType<EbonglassShard>();
            }
            else if (this is CrimglassProjectile)
            {
                projectile.damage = 15;
                projectile.knockBack = 0.6f;
                projectile.penetrate = 4;

                shardType = ModContent.ProjectileType<CrimglassShard>();
            }
            else if (this is PearlglassProjectile)
            {
                projectile.damage = 14;
                projectile.knockBack = 0.3f;
                projectile.penetrate = 6;

                shardType = ModContent.ProjectileType<PearlglassShard>();
            }
            else
            {
                projectile.damage = 11;
                projectile.knockBack = 0.5f;
                projectile.penetrate = 4;

                shardType = ModContent.ProjectileType<GlassShard>();
            }
        }

        public override void AI()
        {
            if (Main.rand.Next(5) == 0)
                Glass.CreateDust(this, projectile.Center);

            if (projectile.velocity.Y <= 24)
                projectile.velocity.Y += 0.2f;

            if (projectile.velocity.X > 0.04f)
                projectile.velocity.X -= 0.03f;
            else if (projectile.velocity.X < -0.04)
                projectile.velocity.X += 0.03f;
            else
                projectile.velocity.X = 0f;

            projectile.rotation += 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (this is EbonglassProjectile)
                target.AddBuff(BuffID.CursedInferno, 420);
            else if (this is CrimglassProjectile)
                target.AddBuff(BuffID.Ichor, 420);
            else if (this is PearlglassProjectile && projectile.penetrate > 1)
                if (Main.rand.Next(3) != 0)
                    Shatter(projectile.oldVelocity, false);

            if (projectile.penetrate == 1)
            {
                Shatter(projectile.oldVelocity);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Shatter(oldVelocity);

            Terraria.Audio.LegacySoundStyle glass = SoundID.Item27;
            glass.WithVolume(0.8f);
            Main.PlaySound(glass, projectile.position);

            return base.OnTileCollide(oldVelocity);
        }

        private void Shatter() => Shatter(projectile.oldVelocity, true);
        private void Shatter(Vector2 oldVelocity) => Shatter(oldVelocity, true);
        /// <param name="kill">Whether the projectile is "shattered" because it hit a wall or an enemy.</param>
        private void Shatter(Vector2 oldVelocity, bool kill)
        {
            int rnd = Main.rand.Next(3, 6);
            if (this is PearlglassProjectile)
            {
                if (kill)
                    rnd *= 2;
                else
                    rnd /= 2;
            }
            float speed = oldVelocity.Length() / 3;
            Vector2 nextPosition = projectile.position + oldVelocity;
            for (int i = 0; i < rnd; i++)
            {
                float rot = oldVelocity.ToRotation();
                rot += (rnd / 2 - i + Main.rand.NextFloat(-3, 3)) * 0.12f;
                Vector2 velocity = -rot.ToRotationVector2() * speed;

                int type = Main.rand.Next(4);
                int damage = projectile.damage / 2 + type * 3;
                if (this is PearlglassProjectile)
                    damage = projectile.damage * 2 / 3 + type * 3;
                float knockBack = projectile.knockBack / 5 + type / 5;
                Projectile.NewProjectile(projectile.position, velocity, shardType, damage, knockBack, projectile.owner, type);

                Glass.CreateDust(this, nextPosition);
            }
        }

        private void CreateDust(Vector2 position)
        {
            int type = 42;
            if (this is EbonglassProjectile)
                type = 186;
            else if (this is CrimglassProjectile)
                type = 25;
            else if (this is PearlglassProjectile)
                type = 164;

            if (!(this is CrimglassProjectile) || Main.rand.NextBool(2, 3))
            {
                int dust = Dust.NewDust(position, projectile.width, projectile.height, type);
                Main.dust[dust].velocity.X *= 0.4f;
            }

            if (this is CrimglassProjectile && Main.rand.NextBool(1, 3))
            {
                int dust = Dust.NewDust(position, projectile.width, projectile.height, 87);
                Main.dust[dust].velocity.X *= 0.4f;
            }
        }
    }
}
