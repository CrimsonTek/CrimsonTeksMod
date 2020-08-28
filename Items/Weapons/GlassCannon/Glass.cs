using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace CrimsonTeksMod.Items.Weapons.GlassCannon
{
    public static class Glass
    {
        public static void CreateDust(ModProjectile modProjectile, Vector2 position)
        {
            Projectile projectile = modProjectile.projectile;

            GlassID glassType;
            bool shard;

            if (modProjectile is CrimglassProjectile || modProjectile is CrimglassShard)
                glassType = GlassID.CrimGlass;
            else if (modProjectile is EbonglassProjectile || modProjectile is EbonglassShard)
                glassType = GlassID.EbonGlass;
            else if (modProjectile is PearlglassProjectile || modProjectile is PearlglassShard)
                glassType = GlassID.PearlGlass;
            else
                glassType = GlassID.Glass;

            if (modProjectile is GlassProjectile)
                shard = false;
            else
                shard = true;

            if (glassType == GlassID.CrimGlass)
            {
                int count = shard ? 1 : 3;
                for (int i = 0; i < count; i++) // red bloody
                {
                    int dust = Dust.NewDust(position, projectile.width, projectile.height, 25);
                    Main.dust[dust].velocity.X *= 0.4f;
                }
                for (int i = 0; i < count; i++) // black muddy
                {
                    int dust = Dust.NewDust(position, projectile.width, projectile.height, 53);
                    Main.dust[dust].velocity.X *= 0.2f;
                }
                if (Main.rand.NextBool(1, 5)) // ichor
                {
                    float scale = shard ? 1.7f : 1.3f;
                    int dust = Dust.NewDust(position, projectile.width, projectile.height, 87, Scale: scale);
                    Main.dust[dust].velocity.X *= 0.4f;
                }
            }
            else if (glassType == GlassID.EbonGlass)
            {
                int dust = Dust.NewDust(position, projectile.width, projectile.height, 186); // purple corruption stuff
                Main.dust[dust].velocity.X *= 0.4f;

                dust = Dust.NewDust(position, projectile.width, projectile.height, 14); // purple corruption stuff
                Main.dust[dust].velocity.X *= 0.4f;

                if (Main.rand.NextBool(1, 6)) // cursed flame
                {
                    float scale = shard ? 3.25f : 2.5f;
                    dust = Dust.NewDust(position, projectile.width, projectile.height, 75, Scale: scale);
                    Main.dust[dust].velocity.X *= 0.4f;
                }
            }
            else if (glassType == GlassID.PearlGlass)
            {
                if (!shard || Main.rand.NextBool(1, 3)) // pink light
                {
                    int dust = Dust.NewDust(position, projectile.width, projectile.height, 164); // 164, 134
                    Main.dust[dust].velocity.X *= 0.4f;
                }
            }
            else
            {
                for (int i = 0; i < 1; i++) // glass
                {
                    int dust = Dust.NewDust(position, projectile.width, projectile.height, 42);
                    Main.dust[dust].velocity.X *= 0.4f;
                }
            }
        }

        private enum GlassID
        {
            Glass, CrimGlass, EbonGlass, PearlGlass
        }
    }
}
