using ErpSystem.Contract.Inventorey.Warehouse;

namespace ErpSystem.Mapping
{
    public class MappingConfigurations
    {
        public void Register(TypeAdapterConfig config)
        {
     

            config.NewConfig<WarehouseRequest, Warehouse>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.IsActive, src => true);    

            config.NewConfig<UpdateWarehouse, Warehouse>()
                .Ignore(dest => dest.Id)         
                .Ignore(dest => dest.IsDeleted)   
                .Ignore(dest => dest.CreatedAt)   
                .Ignore(dest => dest.CreatedBy);  


            config.NewConfig<Warehouse, WarehouseResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.WarehouseCode, src => src.WarehouseCode)
                .Map(dest => dest.WarehouseName, src => src.WarehouseName)
                .Map(dest => dest.WarehouseLocation, src => src.WarehouseLocation)
                .Map(dest => dest.IsActive, src => src.IsActive);

            




        }
    }
}