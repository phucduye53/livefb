namespace livefb.Repository.Entity
{
    public interface IEntity<T>
    {
         T Id { get; set; }
    }
}