using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Models
{
    public class OrderModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public long UserId { get; set; }
        public UserModel? User { get; set; }
        public List<BookModel> Items { get; set; } = [];
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
