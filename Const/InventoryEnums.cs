namespace ErpSystem.Const
{
    public class InventoryEnums
    {
        public enum CalculationMethod
        {
            FIFO = 1,              
            WeightedAverage = 2   
        }

        public enum ItemType
        {
            StockableItem = 1,    
            ServiceItem = 2      
        }

        public enum TransactionType
        {
            Addition = 1,         
            Discharge = 2,       
            Transfer = 3,        
            Adjustment = 4        
        }
    }
}
