using AutoMapper;
using RainfallReading.Service.DomainModels;
using ModelInfo = RainfallReading.Model;

namespace RainfallReading.Service.MapperConfigs
{
    public class MapperConfig
    {
        public static IMapper InitializeRainfallReadingMapper
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FloodMonitoringReadingsDomain, ModelInfo.RainfallReadingResponse>()
                    .ForMember(dest => dest.Readings, opt => opt.MapFrom(src => src.items));

                    cfg.CreateMap<Item, ModelInfo.RainfallReading>()
                    .ForMember(dest => dest.DateMeasured, opt => opt.MapFrom(src => src.dateTime))
                    .ForMember(dest => dest.AmountMeasured, opt => opt.MapFrom(src => src.value));
                }).CreateMapper();
            }
        }
    }
}
