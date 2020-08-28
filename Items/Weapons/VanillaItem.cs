using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CrimsonTeksMod.Items.Weapons.GlassCannon;

namespace ProgressionPlus.Items
{
    public class VanillaItem : GlobalItem
    {
        private static int glassCannon;

        public override void SetDefaults(Item item)
        {
            glassCannon = ModContent.ItemType<GlassCannon>();
            base.SetDefaults(item);
        }

        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == glassCannon)
            {
                switch (ammo.type)
                {
                    default:
                    case ItemID.SandBlock:
                        type = ModContent.ProjectileType<GlassProjectile>();
                        break;
                    case ItemID.EbonsandBlock:
                        type = ModContent.ProjectileType<EbonglassProjectile>();
                        break;
                    case ItemID.CrimsandBlock:
                        type = ModContent.ProjectileType<CrimglassProjectile>();
                        break;
                    case ItemID.PearlsandBlock:
                        type = ModContent.ProjectileType<PearlglassProjectile>();
                        break;
                }
            }

            base.PickAmmo(weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
        }
    }
}
