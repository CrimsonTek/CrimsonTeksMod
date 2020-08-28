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
    public class GlassCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Cannon");
        }

        public override void SetDefaults()
        {
            item.damage = 42;
            item.knockBack = 2;
            item.ranged = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ModContent.ProjectileType<GlassProjectile>();
            item.useAmmo = AmmoID.Sand;
            item.shootSpeed = 24;
            item.UseSound = SoundID.Item5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sandgun);
            recipe.AddIngredient(ItemID.Furnace);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }
    }
}
