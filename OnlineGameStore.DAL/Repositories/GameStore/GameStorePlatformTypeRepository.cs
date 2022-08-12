﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStorePlatformTypeRepository : GameStoreGenericRepository<PlatformTypeEntity>, IGameStorePlatformTypeRepository
    {
        public GameStorePlatformTypeRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            var platformTypes = await GetMany(s => types.Contains(s.Type));

            var platformTypesIds = platformTypes.Select(s => s.Id.ToString());

            return platformTypesIds;
        }

        public async Task<PlatformTypeEntity> GetByTypeAsync(string type, bool includeDeleted = false, params string[] includeProperties)
        {
            Expression<Func<PlatformTypeEntity, bool>> predicate = pt => pt.Type == type;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }
    }
}