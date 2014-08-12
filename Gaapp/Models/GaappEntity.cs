
namespace Gaapp.Models
{
    public abstract class GaappEntity
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
    }
}
