using Microsoft.EntityFrameworkCore;
using Server.EntityFramework;
using Server.EntityFramework.Models;
using Server.Models.DTO.Inventory;
using Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Models.Utilities.Slots;

namespace Server.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbUserContext dbContext;

        public ItemRepository(DbUserContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<ItemEF>> GetInventory(string name)
        {
            return await dbContext.Items
                .AsNoTracking()
                .Where(x => x.Character.Name == name)
                .ToListAsync();
        }

        public virtual async Task EquipItem(DressingDTO dto)
        {
            var item = await dbContext.Items
                .FirstOrDefaultAsync(x => x.Id == dto.Id && x.Character.Name == dto.Name);
            if (item != null)
            {
                item.IsEquipped = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task UnEquipItem(DressingDTO dto)
        {
            var item = await dbContext.Items
                .FirstOrDefaultAsync(x => x.Id == dto.Id && x.Character.Name == dto.Name);
            if (item != null)
            {
                item.IsEquipped = false;
                await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ItemExists(DressingDTO dto)
        {
            return await dbContext.Items
                .AsNoTracking()
                .AnyAsync(x => x.Id == dto.Id && x.Character.Name == dto.Name);
        }

        public async Task<Item?> AddItem(string name, ItemType type)
        {
            var character = await dbContext.Characters
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Name == name);
            var itemEF = new ItemEF() { Type = type };
            if (character == null) return null;

            character.Items.Add(itemEF);
            await dbContext.SaveChangesAsync();

            var item = ItemFactory.Get(itemEF);
            if (item != null)
            {
                item.Id = itemEF.Id;
                return item;
            }
            return null;
        }

        public async Task DestroyItem(DressingDTO dto)
        {
            var items = await dbContext.Items
                .Where(x => x.Character.Name == dto.Name)
                .ToListAsync();
            if (!items.Any()) return;

            var item = items.FirstOrDefault(x => x.Id == dto.Id);
            if (item != null)
            {
                dbContext.Items.Remove(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

public interface IItemRepository
{
    Task<bool> ItemExists(DressingDTO dto);
}