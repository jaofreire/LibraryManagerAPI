using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.Models
{
    public class OrderModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public long UserId { get; set; }
        public UserModel? User { get; set; }
        public List<ViewOrderBookDTO> Items { get; set; } = [];
        public double Amount { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
