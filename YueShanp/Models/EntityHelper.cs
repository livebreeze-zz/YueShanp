using System;

namespace YueShanp.Models
{
    public static class EntityHelper<T>
        where T : BaseEntity<int>
    {
        public static void CreateBaseEntity(T instance, string user, EntityStatus entityStatus = EntityStatus.Enabled)
        {
            instance.Creator = user;
            instance.CreateTime = DateTime.Now;
            EditBaseEntity(instance, user, entityStatus);
        }

        public static void EditBaseEntity(T instance, string user, EntityStatus entityStatus = EntityStatus.Enabled)
        {
            instance.LastEditor = user;
            instance.LastEditTime = DateTime.Now;
            instance.EntityStatus = entityStatus;
        }
    }
}