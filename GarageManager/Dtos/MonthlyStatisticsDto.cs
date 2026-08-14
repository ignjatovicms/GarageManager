namespace GarageManager.Dtos
{
    public class MonthlyStatisticsDto
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }

        public int VehicleOrders { get; set; }
        public int PartSaleOrders { get; set; }

        public decimal PartsRevenue { get; set; }
        public decimal ServicesRevenue { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}