using SQLite;

namespace EasyCarsMaintenance.Models
{
    [Table("Cars")]
    internal class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(10)]
        public string Plate { get; set; } = string.Empty;
        
        [NotNull]
        public int OwnerId { get; set; }
        
        [NotNull, MaxLength(100)]
        public string Brand { get; set; } = string.Empty;
        
        [NotNull, MaxLength(150)]
        public string ModelCar { get; set; } = string.Empty;

        [NotNull, MaxLength(4)]
        public int Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
