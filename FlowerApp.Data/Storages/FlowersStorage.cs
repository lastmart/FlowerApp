using System.Linq.Expressions;
using FlowerApp.Domain.Common;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data.Storages;

public class FlowersStorage : IFlowersStorage
{
    private readonly FlowerAppContext flowerAppContext;

    public FlowersStorage(FlowerAppContext flowerAppContext)
    {
        this.flowerAppContext = flowerAppContext;
    }

    public Flower? Get(int id)
    {
        return flowerAppContext.Flowers
            .Include(f => f.LightParameters)
            .FirstOrDefault(f => f.Id == id);
    }

    public IEnumerable<Flower> Get(int[] ids)
    {
        return flowerAppContext.Flowers
            .Where(f => ids.Contains(f.Id))
            .Include(f => f.LightParameters)
            .ToList();
    }

    public async Task<bool> Create(Flower model)
    {
        await flowerAppContext.Flowers.AddAsync(model);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Flower model)
    {
        flowerAppContext.Flowers.Update(model);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        var flower = await flowerAppContext.Flowers.FindAsync(id);
        if (flower == null) return false;

        flowerAppContext.Flowers.Remove(flower);
        return await flowerAppContext.SaveChangesAsync() > 0;
    }

    public Flower? GetByName(string name)
    {
        return flowerAppContext.Flowers
            .Include(f => f.LightParameters)
            .FirstOrDefault(f => f.Name == name);
    }

    public Flower? GetByScientificName(string scientificName)
    {
        return flowerAppContext.Flowers
            .Include(f => f.LightParameters)
            .FirstOrDefault(f => f.ScientificName == scientificName);
    }


    public async Task<SearchFlowersResult<Flower>> GetAll(Pagination pagination, string? sortByProperty)
    {
        if (string.IsNullOrEmpty(sortByProperty))
        {
            sortByProperty = nameof(Flower.Id);
        }

        var flowers = await flowerAppContext.Flowers
            .Include(f => f.LightParameters)
            .OrderBy(flower => flower.Id)
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToListAsync();
        var count = await flowerAppContext.Flowers.CountAsync();

        return new SearchFlowersResult<Flower>(count, flowers);
    }

    public async Task<IEnumerable<Flower>> FilterFlowers(FlowerFilter filter)
    {
        var query = flowerAppContext.Flowers.Include(f => f.LightParameters).AsQueryable();

        if (filter.WateringFrequency.HasValue)
            query = query.Where(f => f.WateringFrequency.Date == filter.WateringFrequency.Value.Date);

        if (filter.TransplantFrequency.HasValue)
            query = query.Where(f => f.TransplantFrequency.Date == filter.TransplantFrequency.Value.Date);

        if (filter.IlluminationInSuites.HasValue)
        {
            const double epsilon = 0.0001;
            query = query.Where(f =>
                Math.Abs(f.LightParameters.IlluminationInSuites - filter.IlluminationInSuites.Value) < epsilon);
        }

        if (filter.DurationInHours.HasValue)
            query = query.Where(f => f.LightParameters.DurationInHours == filter.DurationInHours.Value);

        if (filter.ToxicCategories != null && filter.ToxicCategories.Any())
        {
            var combinedCategories = filter.ToxicCategories.Aggregate((a, b) => a | b);

            query = combinedCategories == ToxicCategory.None ? query.Where(f => f.ToxicCategory == ToxicCategory.None) : query.Where(f => (f.ToxicCategory & combinedCategories) != 0);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Flower>> SortFlowers(FlowerSortOptions sortOptions)
    {
        var query = flowerAppContext.Flowers.Include(f => f.LightParameters).AsQueryable();

        IOrderedQueryable<Flower> orderedQuery = null;

        foreach (var sortOption in sortOptions.SortOptions)
        {
            var propertySelector = GetPropertySelector(sortOption.SortBy);

            if (orderedQuery == null)
                orderedQuery = sortOption.IsDescending
                    ? query.OrderByDescending(propertySelector)
                    : query.OrderBy(propertySelector);
            else
                orderedQuery = sortOption.IsDescending
                    ? orderedQuery.ThenByDescending(propertySelector)
                    : orderedQuery.ThenBy(propertySelector);
        }

        return await orderedQuery.ToListAsync();
    }

    private Expression<Func<Flower, object>> GetPropertySelector(string sortBy)
    {
        return sortBy.ToLower() switch
        {
            "name" => f => f.Name,
            "scientificname" => f => f.ScientificName,
            "wateringfrequency" => f => f.WateringFrequency,
            "transplantfrequency" => f => f.TransplantFrequency,
            "illuminationinsuites" => f => f.LightParameters.IlluminationInSuites,
            "durationinhours" => f => f.LightParameters.DurationInHours,
            "istoxic" => f => f.ToxicCategory != ToxicCategory.None,
            _ => throw new ArgumentException("Invalid sort option.")
        };
    }
}