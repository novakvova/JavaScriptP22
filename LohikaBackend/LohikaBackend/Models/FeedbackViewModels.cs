namespace LohikaBackend.Models
{
    //public class OrderItemAddViewModel
    //{
    //    public int ProductId { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal BuyPrice { get; set; }
    //}

    public class FeedbackAddViewModel
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }



    //public class OrderStatusItemViewModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    //public class CartEditViewModel
    //{
    //    public int Id { get; set; }
    //    public int Quantity { get; set; }
    //}

    public class FeedbackItemViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateCreated { get; set; }
        public string Phone { get; set; }
    }

    //public class OrderChangeStatusViewModel
    //{
    //    public int Id { get; set; }
    //    public int StatusId { get; set; }

    //}

    //public class OrderViewModel
    //{
    //    public int Id { get; set; }
    //    public string ConsumerFirstName { get; set; }

    //    public string ConsumerSecondName { get; set; }

    //    public string ConsumerPhone { get; set; }

    //    /// <summary>
    //    /// Дані доставки товару
    //    /// </summary>
    //    public string Region { get; set; }
    //    public string City { get; set; }
    //    public string Street { get; set; }
    //    public string HomeNumber { get; set; }
    //    public string StatusName { get; set; }
    //    public string DateCreated { get; set; }

    //    public List<OrderItemViewModel> Items { get; set; }
    //}
}
