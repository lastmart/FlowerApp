namespace FlowersCareAPI.Models;

public class Page<TEntity>
{
    public Page(IEnumerable<TEntity> entities, int count)
    {
        Count = count;
        Entities = entities;
    }

    public int Count { get; set; }
    public IEnumerable<TEntity> Entities { get; set; }
}