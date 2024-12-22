using System.Linq.Expressions;
using AutoMapper;
using FlowerApp.Data;
using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.FlowerModels;
using Microsoft.EntityFrameworkCore;
using Flower = FlowerApp.Domain.Models.FlowerModels.Flower;
using DBFlower = FlowerApp.Data.DbModels.Flowers.Flower;
using ToxicCategory = FlowerApp.Domain.Models.FlowerModels.ToxicCategory;

namespace FlowerApp.Service.Storages;

public class FlowersStorage : IFlowersStorage
{
    private readonly FlowerAppContext flowerAppContext;
    private readonly IMapper mapper;

    public FlowersStorage(FlowerAppContext flowerAppContext, IMapper mapper)
    {
        this.flowerAppContext = flowerAppContext;
        this.mapper = mapper;
    }

    public async Task<Flower?> Get(int id)
    {
        return mapper.Map<Flower>(await flowerAppContext.Flowers.FirstOrDefaultAsync(f => f.Id == id));
    }

    public async Task<IList<Flower>> Get(int[] ids)
    {
        return await flowerAppContext.Flowers
            .Where(f => ids.Contains(f.Id))
            .Select(dbFlower => mapper.Map<Flower>(dbFlower))
            .ToListAsync();
    }

    public async Task<bool> Create(Flower model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            await flowerAppContext.Flowers.AddAsync(mapper.Map<DBFlower>(model));
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Update(Flower model)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            flowerAppContext.Flowers.Update(mapper.Map<DBFlower>(model));
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        await using var transaction = await flowerAppContext.Database.BeginTransactionAsync();
        try
        {
            var flower = await flowerAppContext.Flowers.FindAsync(id);
            if (flower == null) return true;
            flowerAppContext.Flowers.Remove(flower);
            var result = await flowerAppContext.SaveChangesAsync() > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<Flower?> Get(string name)
    {
        return mapper.Map<Flower>(await flowerAppContext.Flowers
            .FirstOrDefaultAsync(f => f.Name == name || f.ScientificName == name));
    }

    public async Task<GetFlowerResponse> Get(
        Pagination pagination,
        FlowerFilterParams? filterParams = null,
        FlowerSortOptions? sortByProperty = null
    )
    {
        var flowers = flowerAppContext.Flowers.Select(dbFlower => mapper.Map<Flower>(dbFlower));
        if (sortByProperty != null)
            flowers = SortFlowers(flowers, sortByProperty);

        if (filterParams != null)
            flowers = FilterFlowers(flowers, filterParams);

        var result = await flowers
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync();

        return new GetFlowerResponse(result.Count, result);
    }

    private IQueryable<Flower> FilterFlowers(IQueryable<Flower> flowers, FlowerFilterParams filterParams)
    {
        if (filterParams.WateringFrequency is not null)
            flowers = flowers.Where(f => filterParams.WateringFrequency.Contains(f.WateringFrequency));

        if (filterParams.Illumination is not null)
            flowers = flowers.Where(f => filterParams.Illumination.Contains(f.Illumination));

        if (filterParams.ToxicCategories != null && filterParams.ToxicCategories.Any())
        {
            var toxicCategory =
                filterParams.ToxicCategories.Aggregate(ToxicCategory.None, (current, category) => current | category);

            flowers = toxicCategory == ToxicCategory.None
                ? flowers.Where(f => f.ToxicCategory[0] == ToxicCategory.None)
                : flowers.Where(f => (f.ToxicCategory[0] & toxicCategory) != 0);
        }

        return flowers;
    }

    private IQueryable<Flower> SortFlowers(IQueryable<Flower> flowers, FlowerSortOptions sortOptions)
    {
        IOrderedQueryable<Flower>? orderedQuery = null;

        foreach (var sortOption in sortOptions.SortOptions)
        {
            var propertySelector = GetPropertySelector(sortOption.SortBy);

            if (orderedQuery == null)
                orderedQuery = sortOption.IsDescending
                    ? flowers.OrderByDescending(propertySelector)
                    : flowers.OrderBy(propertySelector);
            else
                orderedQuery = sortOption.IsDescending
                    ? orderedQuery.ThenByDescending(propertySelector)
                    : orderedQuery.ThenBy(propertySelector);
        }

        return orderedQuery ?? flowers;
    }

    private static Expression<Func<Flower, object>> GetPropertySelector(SortByOption sortBy)
    {
        return sortBy switch
        {
            SortByOption.Name => f => f.Name,
            SortByOption.ScientificName => f => f.ScientificName,
            SortByOption.WateringFrequency => f => f.WateringFrequency,
            SortByOption.IlluminationInSuites => f => f.Illumination,
            SortByOption.IsToxic => f => f.ToxicCategory[0] != ToxicCategory.None,
            _ => throw new ArgumentException("Invalid sort option.")
        };
    }
}